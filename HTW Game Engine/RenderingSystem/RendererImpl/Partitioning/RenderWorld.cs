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
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RenderingSystem.RendererImpl
{
    /// <summary>
    /// Controls which objects should be rendered.
    /// Keeps the lists of the renderable objects for the
    /// renderer to actually render.
    /// </summary>
    public class RenderController
    {
        /// <summary>
        /// Called on a mesh to render it.
        /// </summary>
        /// <param name="subMesh"></param>
        public delegate void VisitSubMesh(Mesh.SubMesh subMesh);
        /// <summary>
        /// Called on a light to render it.
        /// </summary>
        /// <param name="light"></param>
        public delegate void VisitLight(GeneralLight light);
        

        private List<Mesh.SubMesh> _worldSubMeshes = new List<Mesh.SubMesh>(100);
        private List<GeneralLight> _worldLights = new List<GeneralLight>(20);
        private List<ParticleSystem> _worldParticleSystems = new List<ParticleSystem>(10);

        private List<BoundingBox> _physicsBoundingBoxes = new List<BoundingBox>();
		private List<Mesh> _meshes = new List<Mesh>();

        private IEnumerable<Vector3[]> _linesToRender = new List<Vector3[]>();
        private IEnumerable<string> _textToRender = new List<string>();
        private IEnumerable<DrawableBillboard> _billboardsToRender = new List<DrawableBillboard>();

        /// <summary>
        /// The list of billboards to render.
        /// </summary>
        public IEnumerable<DrawableBillboard> BillboardsToRender
        {
            get { return _billboardsToRender; }
            set { _billboardsToRender = value; }
        }
        
        /// <summary>
        /// The list of lines to render.
        /// </summary>
        public IEnumerable<Vector3[]> LinesToRender
        {
            get { return _linesToRender; }
            set { _linesToRender = value; }
        }

        /// <summary>
        /// The list of texts to render.
        /// </summary>
        public IEnumerable<string> TextToRender
        {
            get { return _textToRender; }
            set { _textToRender = value; }
        }

        /// <summary>
        /// The list of meshes to render.
        /// </summary>
		public List<Mesh> Meshes
		{
			get { return _meshes; }
		}



        public RenderController()
        {
            
        }

        /// <summary>
        /// Clear all of the data. 
        /// Remove everything from the render queue.
        /// </summary>
        public void ClearData()
        {
            _worldSubMeshes.Clear();
            _worldLights.Clear();
            _worldParticleSystems.Clear();
            _physicsBoundingBoxes.Clear();
			_meshes.Clear();
        }

        /// <summary>
        /// This is a visitor design pattern implementation. The render world loops through
        /// all submeshes and call the delegate over each one of them. 
        /// </summary>
        /// <param name="visitor"></param>
        public void Visit(VisitSubMesh visitor)
        {
            foreach (Mesh.SubMesh subMesh in _worldSubMeshes)
            {
                visitor(subMesh);
            }
        }
        /// <summary>
        /// This is a visitor design pattern implementation. The render world loops through
        /// all lights and call the delegate over each one of them. 
        /// </summary>
        /// <param name="visitor"></param>
        public void Visit(VisitLight visitor)
        {
            foreach (GeneralLight light in _worldLights)
            {
                visitor(light);
            }
        }

        /// <summary>
        /// Get the physics bounding boxes.
        /// This is used for debug purposes in visualizing the physics data.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BoundingBox> GetPhysicsBoundingBoxes()
        {
            return _physicsBoundingBoxes;
        }

        /// <summary>
        /// Add a mesh to be rendered.
        /// Culling will be applied later.
        /// </summary>
        /// <param name="mesh"></param>
        public void AddMesh(Mesh mesh)
        {
            if (mesh.PhysicsBoundingBox != null)
                _physicsBoundingBoxes.Add(mesh.PhysicsBoundingBox.Value);

            if (mesh is WaterPlane)
            {
                WaterPlane waterPlane = mesh as WaterPlane;
                if (!waterPlane.Enabled)
                    return;
            }

            for (int index = 0; index < mesh.SubMeshes.Count; index++)
            {
                Mesh.SubMesh subMesh = mesh.SubMeshes[index];
                AddSubMesh(subMesh);
            }
            if (mesh.SubMeshes.Count == 0)
                _meshes.Add(mesh);
        }

        /// <summary>
        /// Add a sub mesh apart of a overall mesh to be rendered.
        /// </summary>
        /// <param name="subMesh"></param>
        protected void AddSubMesh(Mesh.SubMesh subMesh)
        {
            _worldSubMeshes.Add(subMesh);
        }

        /// <summary>
        /// Remove a sub mesh which was added.
        /// </summary>
        /// <param name="subMesh"></param>
        public void RemoveSubMesh(Mesh.SubMesh subMesh)
        {
            _worldSubMeshes.Remove(subMesh);
        }

        /// <summary>
        /// Add a light to be rendered.
        /// </summary>
        /// <param name="light"></param>
        public void AddLight(GeneralLight light)
        {
            _worldLights.Add(light);
        }

        /// <summary>
        /// Remove a light which was been added.
        /// </summary>
        /// <param name="light"></param>
        public void RemoveLight(GeneralLight light)
        {
            _worldLights.Remove(light);
        }

        /// <summary>
        /// Add a particle system to be rendered.
        /// </summary>
        /// <param name="particleSystem"></param>
        public void AddParticleSystem(ParticleSystem particleSystem)
        {
            _worldParticleSystems.Add(particleSystem);
        }

        /// <summary>
        /// Remove a particle system which has been added.
        /// </summary>
        /// <param name="particleSystem"></param>
        public void RemoveParticleSystem(ParticleSystem particleSystem)
        {
            _worldParticleSystems.Remove(particleSystem);
        }

        /// <summary>
        /// Cull the not visible meshes. This needs to be called every render.
        /// </summary>
        /// <param name="frustum">The view frustum of the camera.</param>
        /// <param name="visibleSubMeshes">The list cotaining all of the visible meshes.</param>
        public void GetVisibleMeshes(BoundingFrustum frustum, List<Mesh.SubMesh>[] visibleSubMeshes)
        {
            for (int index = 0; index < _worldSubMeshes.Count; index++)
            {
                Mesh.SubMesh subMesh = _worldSubMeshes[index];
                if (subMesh.Enabled && 
                    frustum.Intersects(subMesh.GlobalBoundingSphere) &&
                    frustum.Intersects(subMesh.GlobalBoundingBox))
                {
                    visibleSubMeshes[(int)subMesh.RenderQueue].Add(subMesh);
                }
            }
        }
        
        /// <summary>
        /// Cull the not visible meshes. This needs to be called every render.
        /// </summary>
        /// <param name="frustum">The view frustum of the camera.</param>
        /// <param name="visibleSubMeshes">The list containing all of the visible meshes.</param>
        public void GetVisibleMeshes(BoundingFrustum frustum, List<Mesh.SubMesh> visibleSubMeshes)
        {
            for (int index = 0; index < _worldSubMeshes.Count; index++)
            {
                Mesh.SubMesh subMesh = _worldSubMeshes[index];
                if (subMesh.Enabled && 
                    frustum.Intersects(subMesh.GlobalBoundingSphere) &&
                    frustum.Intersects(subMesh.GlobalBoundingBox))
                {
                    visibleSubMeshes.Add(subMesh);
                }
            }
        }

        /// <summary>
        /// Cull the not visible particle systems. Called independantly of GetVisibleMeshes(...)
        /// </summary>
        /// <param name="frustum">The view frustum of the camera.</param>
        /// <param name="visibleParticleSystems">The list of particle systems which are visible.</param>
        public void GetVisibleParticleSystems(BoundingFrustum frustum, List<ParticleSystem> visibleParticleSystems)
        {
            foreach (ParticleSystem particleSystem in _worldParticleSystems)
            {
                if (particleSystem.Enabled && frustum.Intersects(particleSystem.GlobalBoundingBox))
                {
                    visibleParticleSystems.Add(particleSystem);
                }
            }    
        }

        public void GetShadowCasters(BoundingFrustum frustum, List<Mesh.SubMesh> visibleSubMeshes)
        {
            for (int index = 0; index < _worldSubMeshes.Count; index++)
            {
                Mesh.SubMesh subMesh = _worldSubMeshes[index];
                if (subMesh.Enabled && subMesh.CastShadows && 
                    frustum.Intersects(subMesh.GlobalBoundingSphere) &&
                    frustum.Intersects(subMesh.GlobalBoundingBox))
                {
                    visibleSubMeshes.Add(subMesh);
                }
            }   
        }

		/// <summary>
		/// Used by cascade maps gets the meshes visible to the directional light based on the frustum and the specified cascade division planes.
		/// </summary>
		/// <param name="frustum">The frustum of the directional light.</param>
		/// <param name="additionalPlanes">The directional light clipping planes.</param>
		/// <param name="visibleSubMeshes">A list for the visible meshes to be stores in.</param>
        public void GetShadowCasters(BoundingFrustum frustum, Plane[] additionalPlanes, List<Mesh.SubMesh> visibleSubMeshes)
        {
            for (int index = 0; index < _worldSubMeshes.Count; index++)
            {
                Mesh.SubMesh subMesh = _worldSubMeshes[index];
                if (subMesh.Enabled && subMesh.CastShadows)
                { 
                    // Cull meshes outside of the sub frustum.
                    bool outside = false;
                    for (int p = 0; p < 6; p++)
                    {
                        PlaneIntersectionType intersectionType;
                        additionalPlanes[p].Intersects(ref subMesh.GlobalBoundingSphere, out intersectionType);
                        if (intersectionType == PlaneIntersectionType.Front)
                        {
                            outside = true;
                            break;
                        }
                        additionalPlanes[p].Intersects(ref subMesh.GlobalBoundingBox, out intersectionType);
                        if (intersectionType == PlaneIntersectionType.Front)
                        {
                            outside = true;
                            break;
                        }
                    }
                    if (outside)
                        continue;
                    visibleSubMeshes.Add(subMesh);
                }
            }
        }

        /// <summary>
        /// Gets the lights that are visible in the users view frustum.
        /// </summary>
        /// <param name="frustum">The users's view frustum.</param>
        /// <param name="visibleLights">The list for the visible lights to be placed in.</param>
        public void GetVisibleLights(BoundingFrustum frustum, List<GeneralLight> visibleLights)
        {
            foreach (GeneralLight light in _worldLights)
            {
                if (light.Enabled)
                {
                    switch (light.LightType)
                    {
                        case GeneralLight.Type.Point:
                            if (frustum.Intersects(light.BoundingSphere))
                                visibleLights.Add(light);
                            break;
                        case GeneralLight.Type.Directional:
                            visibleLights.Add(light);
                            break;
                        case GeneralLight.Type.Spot:
                            if (frustum.Intersects(light.BoundingSphere) &&
                                frustum.Intersects(light.Frustum))
                                visibleLights.Add(light);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }
    }
}
