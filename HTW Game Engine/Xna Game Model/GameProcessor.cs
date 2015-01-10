//-----------------------------------------------------------------------------
//Based on...
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
// http://create.msdn.com/en-US/education/catalog/sample/normal_mapping
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Adapted from the code by...
// Jorge Adriano Luna
// http://jcoluna.wordpress.com
//-----------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.IO;

using RenderingSystem.RendererImpl;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

using Henge3D.Physics;
using Henge3D;
using Henge3D.Pipeline;

namespace Xna_Game_Model
{
    /// <summary>
    /// Code based upon:
    /// http://create.msdn.com/en-US/education/catalog/sample/normal_mapping
    /// </summary>
    [ContentProcessor(DisplayName = "Xna Game Model")]
    public class GameProcessor : ModelProcessor
    {
        protected bool _isSkinned = false;
        // These are the keys for the maps in the model file.
        // It was a solid idea although I don't know how it works with blender.
        public const string NormalMapKey = "NormalMap";
        public const string DiffuseMapKey = "DiffuseMap";
        public const string SpecularMapKey = "SpecularMap";
        public const string EmissiveMapKey = "EmissiveMap";
        public const string SecondNormalMapKey = "SecondNormalMap";
        public const string SecondDiffuseMapKey = "SecondDiffuseMap";
        public const string SecondSpecularMapKey = "SecondSpecularMap";
        public const string ReflectionMapKey = "ReflectionMap";


        const string TYPE_ATTR_NAME = "type";
        const string ELASTICITY_ATTR_NAME = "elasticity";
        const string ROUGHNESS_ATTR_NAME = "roughness";
        const string DENSITY_ATTR_NAME = "density";
        const string SHAPE_ATTR_NAME = "shape";

        #region Physics Stuff
        private PhysicalShape _defaultShape = PhysicalShape.Polyhedron;
        private float _defaultDensity = 1f, _defaultElasticity = 0f, _defaultRoughness = 0.5f;
        private WindingOrder _windingOrder = WindingOrder.Clockwise;
        protected bool _processPhysics = true;

        [DefaultValue(PhysicalShape.Polyhedron)]
        [DisplayName("Default Shape")]
        [Description("The default shape type to use for meshes without a shape attribute.")]
        public PhysicalShape DefaultShape { get { return _defaultShape; } set { _defaultShape = value; } }

        [DefaultValue(1f)]
        [DisplayName("Default Density")]
        [Description("The default density of all meshes, used when calculating mass properties.")]
        public float DefaultDensity { get { return _defaultDensity; } set { _defaultDensity = value; } }

        [DefaultValue(0f)]
        [DisplayName("Default Elasticity")]
        [Description("The default elasticity of all meshes, used when calculating collision response.")]
        public float DefaultElasticity { get { return _defaultElasticity; } set { _defaultElasticity = value; } }

        [DefaultValue(0.5f)]
        [DisplayName("Default Roughness")]
        [Description("The default roughness of all meshes, used when calculating friction.")]
        public float DefaultRoughness { get { return _defaultRoughness; } set { _defaultRoughness = value; } }

        [DefaultValue(WindingOrder.Clockwise)]
        [DisplayName("Vertex Winding Order")]
        [Description("The winding order that the processor should expect (after SwapWindingOrder is applied, if set to true).")]
        public WindingOrder WindingOrder { get { return _windingOrder; } set { _windingOrder = value; } }

        [DefaultValue(true)]
        [DisplayName("Process Physics")]
        [Description("Whether to process for physics")]
        public virtual bool ProcessPhysics { get { return _processPhysics; } set { _processPhysics = value; } }
        #endregion

        private string s_customFx = "";

        private List<string> _normalMapFilenames = new List<string>();
        private List<string> _diffuseMapFilenames = new List<string>();
        private List<string> _specularMapFilenames = new List<string>();

        private RenderingSystem.RendererImpl.MeshData.RenderQueueType _renderQueue = MeshData.RenderQueueType.Default;

        public RenderingSystem.RendererImpl.MeshData.RenderQueueType RenderQueue
        {
            get { return _renderQueue; }
            set { _renderQueue = value; }
        }

        private bool _castShadows = true;

        /// <summary>
        /// The name of the custom fx file the model wants to use. 
        /// If there is one.
        /// </summary>
        public string CustomFx
        {
            get { return s_customFx; }
            set { s_customFx = value; }
        }

        public bool CastShadows
        {
            get { return _castShadows; }
            set { _castShadows = value; }
        }

        public override ModelContent Process(NodeContent input, ContentProcessorContext context)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            //we always want to generate tangent frames, as we use tangent space normal mapping
            GenerateTangentFrames = true;

            //merge transforms
            MeshHelper.TransformScene(input, input.Transform);
            input.Transform = Matrix.Identity;

            if (!_isSkinned)
                MergeTransforms(input);

            if (_processPhysics)
            {
                // This type of mesh is ment for a static object.

                #region Physics Loading
                var attributes = input.Children.ToDictionary(n => n.Name, n => n.OpaqueData);

                var nodesToRemove = (from node in input.Children
                                     where node.OpaqueData.GetAttribute(TYPE_ATTR_NAME, MeshType.Both) == MeshType.Physical
                                     select node).ToArray();

                MeshHelper.TransformScene(input, input.Transform);
                input.Transform = Matrix.Identity;
                MergeTransforms(input);

                ModelContent model = base.Process(input, context);
                var parts = new List<CompiledPart>();
                var materials = new List<Material>();
                var mass = new MassProperties();
                var centerOfMass = Vector3.Zero;

                foreach (var mesh in model.Meshes)
                {
                    MeshType type = MeshType.Both;
                    PhysicalShape shape = PhysicalShape.Mesh;
                    float elasticity = _defaultElasticity, roughness = _defaultRoughness, density = _defaultDensity;

                    if (attributes.ContainsKey(mesh.Name))
                    {
                        type = attributes[mesh.Name].GetAttribute(TYPE_ATTR_NAME, MeshType.Both);
                        if (type == MeshType.Visual)
                            continue;
                        elasticity = attributes[mesh.Name].GetAttribute(ELASTICITY_ATTR_NAME, _defaultElasticity);
                        roughness = attributes[mesh.Name].GetAttribute(ROUGHNESS_ATTR_NAME, _defaultRoughness);
                        density = attributes[mesh.Name].GetAttribute(DENSITY_ATTR_NAME, _defaultDensity);
                        shape = attributes[mesh.Name].GetAttribute(SHAPE_ATTR_NAME, _defaultShape);
                    }

                    var meshCenterOfMass = Vector3.Zero;
                    var meshMass = MassProperties.Immovable;
                    CompiledPart meshPart = null;

                    if (mesh.MeshParts.Count < 1)
                    {
                        continue;
                    }

                    int[] indices = mesh.MeshParts[0].IndexBuffer.Skip(mesh.MeshParts[0].StartIndex).Take(mesh.MeshParts[0].PrimitiveCount * 3).ToArray();
                    Vector3[] vertices = MeshToVertexArray(context.TargetPlatform, mesh);



                    if (_windingOrder == WindingOrder.Clockwise)
                    {
                        ReverseWindingOrder(indices);
                    }

                    switch (shape)
                    {
                        case PhysicalShape.Mesh:
                            {
                                meshPart = new CompiledMesh(vertices, indices);
                                meshMass = MassProperties.Immovable;
                                meshCenterOfMass = GetMeshTranslation(mesh);
                            }
                            break;
                        case PhysicalShape.Polyhedron:
                            {
                                var hull = new ConvexHull3D(vertices);
                                meshPart = hull.ToPolyhedron();
                                meshMass = MassProperties.FromTriMesh(density, vertices, indices, out meshCenterOfMass);
                            }
                            break;
                        case PhysicalShape.Sphere:
                            {
                                Sphere s;
                                Sphere.Fit(vertices, out s);
                                meshPart = new CompiledSphere(s.Center, s.Radius);
                                meshMass = MassProperties.FromSphere(density, s.Center, s.Radius);
                                meshCenterOfMass = s.Center;
                            }
                            break;
                        case PhysicalShape.Capsule:
                            {
                                Capsule c;
                                Capsule.Fit(vertices, out c);
                                meshPart = new CompiledCapsule(c.P1, c.P2, c.Radius);
                                meshMass = MassProperties.FromCapsule(density, c.P1, c.P2, c.Radius, out meshCenterOfMass);
                            }
                            break;
                    }
                    parts.Add(meshPart);
                    materials.Add(new Material(elasticity, roughness));
                    Vector3.Multiply(ref meshCenterOfMass, meshMass.Mass, out meshCenterOfMass);
                    Vector3.Add(ref centerOfMass, ref meshCenterOfMass, out centerOfMass);
                    mass.Mass += meshMass.Mass;
                    meshMass.Inertia.M44 = 0f;
                    Matrix.Add(ref mass.Inertia, ref meshMass.Inertia, out mass.Inertia);
                }

                // compute mass properties
                Vector3.Divide(ref centerOfMass, mass.Mass, out centerOfMass);
                mass.Inertia.M44 = 1f;
                MassProperties.TranslateInertiaTensor(ref mass.Inertia, -mass.Mass, centerOfMass, out mass.Inertia);
                if (centerOfMass.Length() >= Constants.Epsilon)
                {
                    var transform = Matrix.CreateTranslation(-centerOfMass.X, -centerOfMass.Y, -centerOfMass.Z);
                    foreach (var p in parts)
                    {
                        p.Transform(ref transform);
                    }

                    transform = model.Root.Transform;
                    transform.M41 -= centerOfMass.X;
                    transform.M42 -= centerOfMass.Y;
                    transform.M43 -= centerOfMass.Z;
                    model.Root.Transform = transform;
                }

                mass = new MassProperties(mass.Mass, mass.Inertia);

                //rbi.MassProperties = mass;
                //rbi.Materials = materials.ToArray();
                //rbi.Parts = parts.ToArray();

                // remove non-visual nodes
                if (nodesToRemove.Length > 0)
                {
                    foreach (var node in nodesToRemove)
                        input.Children.Remove(node);
                    model = base.Process(input, context);
                }
                #endregion
                BaseLogic.RigidBodyInfo rbi = new BaseLogic.RigidBodyInfo();
                rbi.MassProperties = mass;
                rbi.Materials = materials.ToArray();
                rbi.Parts = parts.ToArray();
                MeshData metadata = new MeshData();
                BoundingBox aabb = new BoundingBox();
                metadata.BoundingBox = ComputeBoundingBox(input, ref aabb, metadata);
                rbi.Metadata = metadata;
                if (_normalMapFilenames.Count != _diffuseMapFilenames.Count || _normalMapFilenames.Count != _specularMapFilenames.Count)
                    throw new ArgumentException("Number of normal, diffuse, and specular maps don't match up.");
                rbi.DiffuseMapFilenames = _diffuseMapFilenames.ToArray();
                rbi.NormalMapFilenames = _normalMapFilenames.ToArray();
                rbi.SpecularMapFilenames = _specularMapFilenames.ToArray();
                //assign it to our Tag
                model.Tag = (object)rbi;
                return model;
            }
            else
            {
                // While this type of mesh is ment for the mesh class in the rendering system implementation.

                ModelContent model = base.Process(input, context);
                MeshData metadata = new MeshData();
                BoundingBox aabb = new BoundingBox();
                metadata.BoundingBox = ComputeBoundingBox(input, ref aabb, metadata);
                model.Tag = (object)metadata;
                return model;
            }
        }

        private void MergeTransforms(NodeContent input)
        {
            if (input is MeshContent)
            {
                MeshContent mc = (MeshContent) input;
                MeshHelper.TransformScene(mc, mc.Transform);
                mc.Transform = Matrix.Identity;
                MeshHelper.OptimizeForCache(mc);
            }
            foreach (NodeContent c in input.Children)
            {
                MergeTransforms(c);
            }
        }

        private BoundingBox ComputeBoundingBox(NodeContent input, ref BoundingBox aabb, MeshData metadata)
        {
            BoundingBox boundingBox;
            if (input is MeshContent)
            {
                MeshContent mc = (MeshContent)input;
                MeshHelper.TransformScene(mc, mc.Transform);
                mc.Transform = Matrix.Identity;

                boundingBox = BoundingBox.CreateFromPoints(mc.Positions);
                //create sub mesh information
                MeshData.SubMeshData subMeshMetadata = new MeshData.SubMeshData();
                subMeshMetadata.BoundingBox = boundingBox;
                subMeshMetadata.RenderQueue = _renderQueue;
                subMeshMetadata.CastShadows = CastShadows;
                metadata.AddSubMeshData(subMeshMetadata);
                if (metadata.SubMeshesMetadata.Count > 1)
                    boundingBox = BoundingBox.CreateMerged(boundingBox, aabb);
            }
            else
            {
                boundingBox = aabb;
            }

            foreach (NodeContent c in input.Children)
            {
                boundingBox = BoundingBox.CreateMerged(boundingBox, ComputeBoundingBox(c, ref boundingBox, metadata));
            }
            return boundingBox;
        }

        protected override MaterialContent ConvertMaterial(MaterialContent material,
           ContentProcessorContext context)
        {
            EffectMaterialContent lppMaterial = new EffectMaterialContent();

            OpaqueDataDictionary processorParameters = new OpaqueDataDictionary();
            processorParameters["ColorKeyColor"] = this.ColorKeyColor;
            processorParameters["ColorKeyEnabled"] = false;
            processorParameters["TextureFormat"] = this.TextureFormat;
            processorParameters["GenerateMipmaps"] = this.GenerateMipmaps;
            processorParameters["ResizeTexturesToPowerOfTwo"] = this.ResizeTexturesToPowerOfTwo;
            processorParameters["PremultiplyTextureAlpha"] = false;
            processorParameters["ColorKeyEnabled"] = false;

            lppMaterial.Effect = new ExternalReference<EffectContent>(s_customFx.Length == 0 ? "shaders/MainEffect.fx" : s_customFx);
            lppMaterial.CompiledEffect = context.BuildAsset<EffectContent, CompiledEffectContent>(lppMaterial.Effect, "EffectProcessor");

            // copy the textures in the original material to the new lpp
            // material
            ExtractTextures(lppMaterial, material);
            //extract the extra parameters
            ExtractDefines(lppMaterial, material, context);

            // and convert the material using the NormalMappingMaterialProcessor,
            // who has something special in store for the normal map.
            return context.Convert<MaterialContent, MaterialContent>
                (lppMaterial, typeof(GameMaterialProcessor).Name, processorParameters);
        }

        /// <summary>
        /// Extract any defines we need from the original material, like alphaMasked, fresnel, reflection, etc, and pass it into
        /// the opaque data
        /// </summary>
        /// <param name="lppMaterial"></param>
        /// <param name="material"></param>
        /// <param name="context"></param>
        private void ExtractDefines(EffectMaterialContent lppMaterial, MaterialContent material, ContentProcessorContext context)
        {
            string defines = "";

            if (material.OpaqueData.ContainsKey("alphaMasked") && material.OpaqueData["alphaMasked"].ToString() == "True")
            {
                context.Logger.LogMessage("Alpha masked material found");
                lppMaterial.OpaqueData.Add("AlphaReference", (float)material.OpaqueData["AlphaReference"]);
                defines += "ALPHA_MASKED;";
            }

            if (material.OpaqueData.ContainsKey("reflectionEnabled") && material.OpaqueData["reflectionEnabled"].ToString() == "True")
            {
                context.Logger.LogMessage("Reflection enabled");
                defines += "REFLECTION_ENABLED;";
            }

            if (_isSkinned)
            {
                context.Logger.LogMessage("Skinned mesh found");
                defines += "SKINNED_MESH;";
            }

            if (material.OpaqueData.ContainsKey("dualLayerEnabled") && material.OpaqueData["dualLayerEnabled"].ToString() == "True")
            {
                context.Logger.LogMessage("Dual layer material found");
                defines += "DUAL_LAYER;";
            }

            if (!String.IsNullOrEmpty(defines))
                lppMaterial.OpaqueData.Add("Defines", defines);

        }
        private void ExtractTextures(EffectMaterialContent lppMaterial, MaterialContent material)
        {
            string diffuseMapFilename = null, specularMapFilename = null, normalMapFilename = null;
            
            foreach (KeyValuePair<String, ExternalReference<TextureContent>> texture
                in material.Textures)
            {
                if (texture.Key.ToLower().Equals("diffusemap") || texture.Key.ToLower().Equals("texture"))
                {
                    lppMaterial.Textures.Add(DiffuseMapKey, texture.Value);
                    diffuseMapFilename = texture.Value.Filename;
                }
                else if (texture.Key.ToLower().Equals("normalmap"))
                {
                    lppMaterial.Textures.Add(NormalMapKey, texture.Value);
                    normalMapFilename = texture.Value.Filename;
                }
                else if (texture.Key.ToLower().Equals("specularmap"))
                {
                    lppMaterial.Textures.Add(SpecularMapKey, texture.Value);
                    specularMapFilename = texture.Value.Filename;
                }
                else if (texture.Key.ToLower().Equals("emissivemap"))
                    lppMaterial.Textures.Add(EmissiveMapKey, texture.Value);
                else if (texture.Key.ToLower().Equals("seconddiffusemap"))
                    lppMaterial.Textures.Add(SecondDiffuseMapKey, texture.Value);
                else if (texture.Key.ToLower().Equals("secondnormalmap"))
                    lppMaterial.Textures.Add(SecondNormalMapKey, texture.Value);
                else if (texture.Key.ToLower().Equals("secondspecularmap"))
                    lppMaterial.Textures.Add(SecondSpecularMapKey, texture.Value);
                else if (texture.Key.ToLower().Equals("reflectionmap"))
                    lppMaterial.Textures.Add(ReflectionMapKey, texture.Value);
            }
           
            ExternalReference<TextureContent> externalRef;
            if (!lppMaterial.Textures.TryGetValue(DiffuseMapKey, out externalRef))
            {
                lppMaterial.Textures[DiffuseMapKey] = new ExternalReference<TextureContent>("images/default_diffuse.tga");
            }
            if (!lppMaterial.Textures.TryGetValue(NormalMapKey, out externalRef))
            {
                lppMaterial.Textures[NormalMapKey] = new ExternalReference<TextureContent>("images/default_normal.tga");
            }
            if (!lppMaterial.Textures.TryGetValue(SpecularMapKey, out externalRef))
            {
                lppMaterial.Textures[SpecularMapKey] = new ExternalReference<TextureContent>("images/default_specular.tga");
            }
            if (!lppMaterial.Textures.TryGetValue(EmissiveMapKey, out externalRef))
            {
                lppMaterial.Textures[EmissiveMapKey] = new ExternalReference<TextureContent>("images/default_emissive.tga");
            }

            _diffuseMapFilenames.Add(diffuseMapFilename);
            _specularMapFilenames.Add(specularMapFilename);
            _normalMapFilenames.Add(normalMapFilename);
        }

        public static float GetMaxDistance(Vector3 center, IList<Vector3> vertices)
        {
            float maxDist = 0f;
            for (int i = 0; i < vertices.Count; i++)
            {
                var p = vertices[i];
                float dist;
                Vector3.DistanceSquared(ref center, ref p, out dist);
                if (dist > maxDist)
                    maxDist = dist;
            }
            return (float)Math.Sqrt(maxDist);
        }

        public static Vector3 GetMeshTranslation(ModelMeshContent mesh)
        {
            var pos = Vector3.Zero;
            var bone = mesh.ParentBone;
            while (bone != null)
            {
                pos += bone.Transform.Translation;
                bone = bone.Parent;
            }
            return pos;
        }

        public static Vector3[] MeshToVertexArray(TargetPlatform platform, ModelMeshContent mesh)
        {
            MemoryStream ms;
            var buffer = mesh.MeshParts[0].VertexBuffer;

            //			if (platform == TargetPlatform.Xbox360)
            //			{
            //				ms = new MemoryStream(ReverseByteOrder(buffer.VertexData));
            //			}
            //			else
            //			{
            ms = new MemoryStream(buffer.VertexData);
            //			}
            BinaryReader reader = new BinaryReader(ms);

            var elems = buffer.VertexDeclaration.VertexElements;
            int count = mesh.MeshParts[0].NumVertices;

            ms.Seek(mesh.MeshParts[0].VertexOffset * buffer.VertexDeclaration.VertexStride.Value, SeekOrigin.Begin);

            var vertices = new Vector3[count];
            for (int i = 0; i < count; i++)
            {
                foreach (var elType in elems)
                {
                    if (elType.VertexElementUsage == VertexElementUsage.Position)
                    {
                        vertices[i].X = reader.ReadSingle();
                        vertices[i].Y = reader.ReadSingle();
                        vertices[i].Z = reader.ReadSingle();
                    }
                    else
                    {
                        switch (elType.VertexElementFormat)
                        {
                            case VertexElementFormat.Color:
                                reader.ReadUInt32();
                                break;
                            case VertexElementFormat.Vector2:
                                reader.ReadSingle();
                                reader.ReadSingle();
                                break;
                            case VertexElementFormat.Vector3:
                                reader.ReadSingle();
                                reader.ReadSingle();
                                reader.ReadSingle();
                                break;
                            case VertexElementFormat.Single:
                                reader.ReadSingle();
                                break;
                            case VertexElementFormat.Byte4:
                                reader.ReadBytes(4);
                                break;
                            case VertexElementFormat.Vector4:
                                reader.ReadSingle();
                                reader.ReadSingle();
                                reader.ReadSingle();
                                reader.ReadSingle();
                                break;
                            default:
                                throw new InvalidContentException("Unrecognized element type in vertex buffer: " + elType.ToString());
                        }
                    }
                }
            }

            var transforms = new Stack<Matrix>();
            var bone = mesh.ParentBone;
            while (bone != null)
            {
                transforms.Push(bone.Transform);
                bone = bone.Parent;
            }

            var transform = Matrix.Identity;
            while (transforms.Count > 0)
            {
                transform *= transforms.Pop();
            }

            Vector3.Transform(vertices, ref transform, vertices);
            return vertices;
        }

        public static byte[] ReverseByteOrder(byte[] source)
        {
            byte[] dest = new byte[source.Length];
            for (int i = 0; i < source.Length; i += 4)
            {
                dest[i] = source[i + 3];
                dest[i + 1] = source[i + 2];
                dest[i + 2] = source[i + 1];
                dest[i + 3] = source[i];
            }
            return dest;
        }

        public static void ReverseWindingOrder(int[] indices)
        {
            for (int i = 0; i < indices.Length; i += 3)
            {
                indices[i + 1] = indices[i + 1] ^ indices[i + 2];
                indices[i + 2] = indices[i + 1] ^ indices[i + 2];
                indices[i + 1] = indices[i + 1] ^ indices[i + 2];
            }
        }
    }
}