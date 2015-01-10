#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem.RendererImpl
{
	/// <summary>
	/// A renderable game mesh.
	/// </summary>
    public class Mesh
    {
		/// <summary>
		/// The physics bounding box for this mesh as a whole used for debugging purposes.
		/// </summary>
        public BoundingBox? PhysicsBoundingBox = null;

		/// <summary>
		/// All the subset materials applied to the subset meshes in the respective order.
		/// </summary>
        public SubsetMaterial[] SubsetMaterials;

		/// <summary>
		/// Whether the mesh will be included in the generatin of shadow maps.
		/// </summary>
        protected bool b_castShadows = true;

		/// <summary>
		/// The render clip bounding box for this mesh in global space.
		/// Used for culling.
		/// </summary>
        protected BoundingBox _globalBoundingBox;

		/// <summary>
		/// The render clip boudning box for this mesh in local space.
		/// Used for culling.
		/// </summary>
        protected BoundingBox _localBoundingBox;

		/// <summary>
		/// The loaded metadata for the mesh.
		/// </summary>
        protected MeshData _metadata;

		/// <summary>
		/// The loaded XNA Model for the mesh.
		/// </summary>
        protected Model _model;

        protected Matrix _transform = Matrix.Identity;

        protected Matrix[] _transforms;

        private List<SubMesh> _subMeshes = new List<SubMesh>();

        public virtual Matrix[] BoneMatrixes
        {
            get { return null; }
            set { }
        }

        public bool Enabled
        {
            set
            {
                foreach (SubMesh subMesh in _subMeshes)
                {
                    subMesh.Enabled = value;
                }
            }
        }

        public BoundingBox GlobalBoundingBox
        {
            get { return _globalBoundingBox; }
        }

        public List<SubMesh> SubMeshes
        {
            get { return _subMeshes; }
        }

		/// <summary>
		/// The world transform for the mesh.
		/// </summary>
        public Matrix Transform
        {
            get { return _transform; }
            set
            {
                _transform = value;
                UpdateSubMeshes();
            }
        }


        public Matrix[] Transforms
        {
            get { return _transforms; }
        }

        public Matrix GetCompleteBoneTransform(string boneName)
        {
            ModelBone foundBone = SearchForBone(_model.Bones, boneName);
            if (foundBone == null)
                throw new ArgumentException("The desired bone does not exist!");
            ModelBone currentBone = foundBone;
            List<Matrix> transformList = new List<Matrix>();
            transformList.Add(BoneMatrixes[foundBone.Index]);
            while (currentBone.Parent != null)
            {
                Matrix transform = BoneMatrixes[currentBone.Parent.Index];
                transformList.Add(transform);
                currentBone = currentBone.Parent;
            }
            transformList.Add(BoneMatrixes[foundBone.Index]);

            Matrix finalTransform = Matrix.Identity;
            foreach (Matrix transform in transformList)
            {
                finalTransform = finalTransform * transform;
            }

            return finalTransform;
        }

        public virtual void SetMetadata(MeshData metadata)
        {
            _metadata = metadata;
            _localBoundingBox = _metadata.BoundingBox;
            _transforms = new Matrix[_model.Bones.Count];
            _model.CopyAbsoluteBoneTransformsTo(_transforms);

            CreateSubMeshList();
            UpdateSubMeshes();
        }

        public virtual void SetModel(Model model)
        {
            _subMeshes.Clear();
            _model = model;
        }

        protected void CreateSubMeshList()
        {
            int idx = 0;
            for (int i = 0; i < _model.Meshes.Count; i++)
            {
                ModelMesh modelMesh = _model.Meshes[i];
                for (int index = 0; index < modelMesh.MeshParts.Count; index++)
                {
                    ModelMeshPart modelMeshPart = modelMesh.MeshParts[index];
                    SubMesh subMesh = new SubMesh(this);
                    subMesh._meshPart = modelMeshPart;
                    subMesh._modelIndex = idx;
                    subMesh.Effect = modelMeshPart.Effect;
                    subMesh.RenderQueue = _metadata.SubMeshesMetadata[idx].RenderQueue;
                    subMesh._metadata = _metadata.SubMeshesMetadata[idx];
                    _subMeshes.Add(subMesh);
                }
                idx++;
            }
            UpdateSubMeshes();
        }

        private ModelBone SearchForBone(ModelBoneCollection mbc, string name)
        {
            foreach (ModelBone mb in mbc)
            {
                if (mb.Name == name)
                    return mb;
            }

            return null;
        }

        private void UpdateSubMeshes()
        {
            Helpers.TransformBoundingBox(ref _localBoundingBox, ref _transform, out _globalBoundingBox);
            if (_model != null)
            {
                for (int i = 0; i < _model.Bones.Count; i++)
                {
                    _transforms[i] = _model.Bones[i].Transform * _transform;
                }
                for (int i = 0; i < _subMeshes.Count; i++)
                {
                    SubMesh subMesh = _subMeshes[i];
                    if (SubsetMaterials != null)
                        subMesh.SubsetMaterial = SubsetMaterials[i];
                    subMesh.World = _transforms[_model.Meshes[subMesh._modelIndex].ParentBone.Index];
                    MeshData.SubMeshData metadata = subMesh._metadata;
                    BoundingBox source = metadata.BoundingBox;
                    Helpers.TransformBoundingBox(ref source, ref _transform, out subMesh.GlobalBoundingBox);
                    subMesh.GlobalBoundingSphere = BoundingSphere.CreateFromBoundingBox(subMesh.GlobalBoundingBox);
                }
            }
        }

		/// <summary>
		/// A subset of a mesh.
		/// A mesh is constructed of a list of these.
		/// </summary>
        public class SubMesh
        {
			/// <summary>
			/// The subset of the XNA model.
			/// </summary>
            public ModelMeshPart _meshPart;

			/// <summary>
			/// The loaded subset data.
			/// </summary>
            public MeshData.SubMeshData _metadata;

			/// <summary>
			/// The index of this subset in the overall mesh.
			/// </summary>
            public int _modelIndex = 0;

			/// <summary>
			/// Whether this subset should be rendered.
			/// </summary>
            public bool Enabled = true;

            public BoundingBox GlobalBoundingBox;

            public BoundingSphere GlobalBoundingSphere;

            public Matrix World = Matrix.Identity;

			/// <summary>
			/// The mesh this sub mesh belongs to.
			/// </summary>
            private Mesh _parent;

            private BaseRenderEffect _renderEffect;

            private MeshData.RenderQueueType _renderQueue = MeshData.RenderQueueType.Default;

            private SubsetMaterial _subsetMaterial = null;

            public SubMesh(Mesh parent)
            {
                _parent = parent;
            }

            public bool CastShadows
            {
                get { return _metadata.CastShadows && _metadata.RenderQueue != MeshData.RenderQueueType.Blend; }
            }

            public Effect Effect
            {
                set
                {
                    _renderEffect = new BaseRenderEffect(value.Clone());
                }
            }

            public BaseRenderEffect RenderEffect
            {
                get { return _renderEffect; }
            }

            public MeshData.RenderQueueType RenderQueue
            {
                get { return _renderQueue; }
                set { _renderQueue = value; }
            }

            public SubsetMaterial SubsetMaterial
            {
                get { return _subsetMaterial; }
                set { _subsetMaterial = value; }
            }

            /// <summary>
            /// Does a straight forward render using the first technique.
            /// </summary>
            /// <param name="camera">The camera of the scene</param>
            /// <param name="device">The graphics device</param>
            public void SimpleRender(ICamera camera, GraphicsDevice device)
            {
                RenderEffect.SetMatrixData(World, camera.View, camera.Proj);

                if (_subsetMaterial != null && _subsetMaterial.UseAlphaMask)
                {
                    RenderEffect.AlphaReferenceParameter.SetValue(_subsetMaterial.AlphaReference);
                    RenderEffect.SetCurrentTech("ReconstructShadingAlpha");

                    RenderEffect.TexTransformParameter.SetValue(_subsetMaterial.TexTransform);

                    RenderEffect.UseDiffuseMaterial.SetValue(_subsetMaterial.UseDiffuseMat);
                    RenderEffect.UseSpecularMaterial.SetValue(_subsetMaterial.UseSpecularMat);

                    if (!_subsetMaterial.UseDiffuseMat)
                    {
                        if (_subsetMaterial.DiffuseMap.Texture != null)
                            RenderEffect.DiffuseMapParameter.SetValue(_subsetMaterial.DiffuseMap.Texture);
                    }
                    else
                    {
                        RenderEffect.DiffuseMatParameter.SetValue(_subsetMaterial.DiffuseMat);
                    }

                    if (!_subsetMaterial.UseSpecularMat)
                    {
                        if (_subsetMaterial.SpecularMap.Texture != null)
                            RenderEffect.SpecularMapParameter.SetValue(_subsetMaterial.SpecularMap.Texture);
                    }
                    else
                    {
                        RenderEffect.SpecularMatParameter.SetValue(_subsetMaterial.SpecularMat);
                    }

                    if (_subsetMaterial.EmmisiveMap.Texture != null)
                        RenderEffect.EmissiveMapParameter.SetValue(_subsetMaterial.EmmisiveMap.Texture);
                }

                if (_parent.BoneMatrixes != null)
                    RenderEffect.SetBones(_parent.BoneMatrixes);

                RenderEffect.Apply();

                device.SetVertexBuffer(_meshPart.VertexBuffer, _meshPart.VertexOffset);
                device.Indices = _meshPart.IndexBuffer;

                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _meshPart.NumVertices, _meshPart.StartIndex, _meshPart.PrimitiveCount);
            }

            public void GetCompleteTransform()
            {
            }

            public void ReconstructShading(ICamera camera, GraphicsDevice graphicsDevice)
            {
                //this pass uses the light diffuse and specular accumulation texture (already bound in the setup stage) and reconstruct the mesh's shading
                //our parameters were already filled in the first pass
                if (_subsetMaterial != null && _subsetMaterial.UseAlphaMask)
                {
                    RenderEffect.SetCurrentTech("ReconstructShadingAlpha");
                    RenderEffect.AlphaReferenceParameter.SetValue(_subsetMaterial.AlphaReference);
                }
                else
                {
                    RenderEffect.SetCurrentTech(1);
                }

                //we don't need to do this again, it was done on the previous step

                //_renderEffect.SetMatrices(GlobalTransform, camera.EyeTransform, camera.ProjectionTransform);
                // if (_parent.BoneMatrixes != null)
                //    _renderEffect.SetBones(_parent.BoneMatrixes);

                if (_subsetMaterial != null)
                {
                    RenderEffect.TexTransformParameter.SetValue(_subsetMaterial.TexTransform);

                    RenderEffect.UseDiffuseMaterial.SetValue(_subsetMaterial.UseDiffuseMat);
                    RenderEffect.UseSpecularMaterial.SetValue(_subsetMaterial.UseSpecularMat);

                    if (!_subsetMaterial.UseDiffuseMat)
                    {
                        if (_subsetMaterial.DiffuseMap.Texture != null)
                            RenderEffect.DiffuseMapParameter.SetValue(_subsetMaterial.DiffuseMap.Texture);
                    }
                    else
                    {
                        RenderEffect.DiffuseMatParameter.SetValue(_subsetMaterial.DiffuseMat);
                    }

                    if (!_subsetMaterial.UseSpecularMat)
                    {
                        if (_subsetMaterial.SpecularMap.Texture != null)
                            RenderEffect.SpecularMapParameter.SetValue(_subsetMaterial.SpecularMap.Texture);
                    }
                    else
                    {
                        RenderEffect.SpecularMatParameter.SetValue(_subsetMaterial.SpecularMat);
                    }

                    if (_subsetMaterial.EmmisiveMap.Texture != null)
                        RenderEffect.EmissiveMapParameter.SetValue(_subsetMaterial.EmmisiveMap.Texture);
                }

                RenderEffect.Apply();

                graphicsDevice.SetVertexBuffer(_meshPart.VertexBuffer, _meshPart.VertexOffset);
                graphicsDevice.Indices = _meshPart.IndexBuffer;

                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _meshPart.NumVertices, _meshPart.StartIndex, _meshPart.PrimitiveCount);
            }

            public void ReconstructShading(ICamera camera, GraphicsDevice graphicsDevice, Plane clipPlane)
            {
                //this pass uses the light diffuse and specular accumulation texture (already bound in the setup stage) and reconstruct the mesh's shading
                //our parameters were already filled in the first pass
                RenderEffect.SetCurrentTech(1);
                RenderEffect.ClippingParameter.SetValue(true);
                RenderEffect.ClipPlaneParameter.SetValue(new Vector4(clipPlane.Normal, clipPlane.D));

                //we don't need to do this again, it was done on the previous step

                //_renderEffect.SetMatrices(GlobalTransform, camera.EyeTransform, camera.ProjectionTransform);
                // if (_parent.BoneMatrixes != null)
                //    _renderEffect.SetBones(_parent.BoneMatrixes);

                RenderEffect.Apply();

                graphicsDevice.SetVertexBuffer(_meshPart.VertexBuffer, _meshPart.VertexOffset);
                graphicsDevice.Indices = _meshPart.IndexBuffer;

                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _meshPart.NumVertices, _meshPart.StartIndex, _meshPart.PrimitiveCount);

                RenderEffect.ClippingParameter.SetValue(false);
            }

            public virtual void RenderShadowMap(ref Matrix viewProj, GraphicsDevice graphicsDevice)
            {
                if (_subsetMaterial != null && _subsetMaterial.UseAlphaMask)
                {
                    RenderEffect.SetCurrentTech("OutputShadowAlpha");
                    RenderEffect.AlphaReferenceParameter.SetValue(_subsetMaterial.AlphaReference);
                }
                else
                {
                    RenderEffect.SetCurrentTech(2);
                }

                RenderEffect.SetLightViewProj(viewProj);

                //we need to set this every frame, there are situations where the object is not on screen but it still cast shadows
                _renderEffect.SetWorld(World);
                if (_parent.BoneMatrixes != null)
                    _renderEffect.SetBones(_parent.BoneMatrixes);

                RenderEffect.Apply();

                graphicsDevice.SetVertexBuffer(_meshPart.VertexBuffer, _meshPart.VertexOffset);
                graphicsDevice.Indices = _meshPart.IndexBuffer;

                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _meshPart.NumVertices, _meshPart.StartIndex, _meshPart.PrimitiveCount);
            }

            public virtual void RenderToGBuffer(ICamera camera, GraphicsDevice graphicsDevice, Plane clipPlane)
            {
                RenderEffect.SetCurrentTech(0);
                RenderEffect.SetMatrixData(World, camera.View, camera.Proj);
                //our first pass is responsible for rendering into GBuffer
                RenderEffect.SetFarClip(camera.FarClip);
                RenderEffect.ClippingParameter.SetValue(true);
                RenderEffect.ClipPlaneParameter.SetValue(new Vector4(clipPlane.Normal, clipPlane.D));

                if (_parent.BoneMatrixes != null)
                    RenderEffect.SetBones(_parent.BoneMatrixes);

                if (_subsetMaterial != null)
                {
                    RenderEffect.TexTransformParameter.SetValue(_subsetMaterial.TexTransform);
                    if (_subsetMaterial.DiffuseMap.Texture != null)
                        RenderEffect.DiffuseMapParameter.SetValue(_subsetMaterial.DiffuseMap.Texture);
                    //else
                    //    RenderEffect.DiffuseMapParameter.SetValue(GameTexture.DefaultDiffuse.Texture);

                    if (_subsetMaterial.SpecularMap.Texture != null)
                        RenderEffect.SpecularMapParameter.SetValue(_subsetMaterial.SpecularMap.Texture);
                    //else
                    //    RenderEffect.SpecularMapParameter.SetValue(GameTexture.DefaultSpecular.Texture);

                    if (_subsetMaterial.NormalMap.Texture != null)
                        RenderEffect.NormalMapParameter.SetValue(_subsetMaterial.NormalMap.Texture);
                    //else
                    //    RenderEffect.NormalMapParameter.SetValue(GameTexture.DefaultNormal.Texture);
                }

                RenderEffect.Apply();

                graphicsDevice.SetVertexBuffer(_meshPart.VertexBuffer, _meshPart.VertexOffset);
                graphicsDevice.Indices = _meshPart.IndexBuffer;

                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _meshPart.NumVertices, _meshPart.StartIndex, _meshPart.PrimitiveCount);

                RenderEffect.ClippingParameter.SetValue(false);
            }

            public virtual void RenderToGBuffer(ICamera camera, GraphicsDevice graphicsDevice)
            {
                if (_subsetMaterial != null && _subsetMaterial.UseAlphaMask)
                {
                    RenderEffect.SetCurrentTech("RenderToGBufferAlpha");
                    RenderEffect.AlphaReferenceParameter.SetValue(_subsetMaterial.AlphaReference);
                }
                else
                {
                    RenderEffect.SetCurrentTech(0);
                }

                RenderEffect.SetMatrixData(World, camera.View, camera.Proj);
                //our first pass is responsible for rendering into GBuffer
                RenderEffect.SetFarClip(camera.FarClip);

                if (_parent.BoneMatrixes != null)
                    RenderEffect.SetBones(_parent.BoneMatrixes);

                if (_subsetMaterial != null && _subsetMaterial.NormalMap.Texture != null)
                    RenderEffect.NormalMapParameter.SetValue(_subsetMaterial.NormalMap.Texture);

                RenderEffect.Apply();

                graphicsDevice.SetVertexBuffer(_meshPart.VertexBuffer, _meshPart.VertexOffset);
                graphicsDevice.Indices = _meshPart.IndexBuffer;

                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _meshPart.NumVertices, _meshPart.StartIndex, _meshPart.PrimitiveCount);
            }
        }
    }
}