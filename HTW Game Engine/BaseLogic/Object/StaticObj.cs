#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Henge3D;
using Henge3D.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RenderingSystem;

namespace BaseLogic
{
    /// <summary>
    ///
    /// </summary>
    public class RigidBodyInfo
    {
        /// <summary>
        /// The diffuse map filenames
        /// </summary>
        public string[] DiffuseMapFilenames;

        /// <summary>
        /// The mass properties
        /// </summary>
        public MassProperties MassProperties;

        /// <summary>
        /// The materials
        /// </summary>
        public Material[] Materials;

        /// <summary>
        /// The metadata
        /// </summary>
        public RenderingSystem.RendererImpl.MeshData Metadata;

        /// <summary>
        /// The normal map filenames
        /// </summary>
        public string[] NormalMapFilenames;

        /// <summary>
        /// The parts
        /// </summary>
        public CompiledPart[] Parts;

        /// <summary>
        /// The specular map filenames
        /// </summary>
        public string[] SpecularMapFilenames;

        /// <summary>
        /// To the rigid body model.
        /// </summary>
        /// <returns></returns>
        public Henge3D.Pipeline.RigidBodyModel ToRigidBodyModel()
        {
            return new Henge3D.Pipeline.RigidBodyModel(MassProperties, Parts, Materials);
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class StaticObj : PhysObj
    {
        /// <summary>
        /// The _rendering system mesh
        /// </summary>
        protected RenderingSystem.RendererImpl.Mesh _renderingSystemMesh;

        /// <summary>
        /// The _subset materials
        /// </summary>
        protected List<SubsetMaterial> _subsetMaterials = new List<SubsetMaterial>();

        /// <summary>
        /// The _tex transform
        /// </summary>
        protected Matrix _texTransform = Matrix.Identity;

        /// <summary>
        /// The b_enabled
        /// </summary>
        protected bool b_enabled = true;

        /// <summary>
        /// The b_mass modified
        /// </summary>
        protected bool b_massModified = false;

        /// <summary>
        /// The b_serilize object
        /// </summary>
        protected bool b_serilizeObj = true;

        /// <summary>
        /// The s_filename
        /// </summary>
        protected string s_filename;

        /// <summary>
        /// The s_physics filename
        /// </summary>
        protected string s_physicsFilename = null;

        /// <summary>
        /// The s_tag
        /// </summary>
        protected string s_tag = "";

        /// <summary>
        /// The physic s_ dat a_ foldername
        /// </summary>
        private const string PHYSICS_DATA_FOLDERNAME = "Physics Data\\";

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticObj"/> class.
        /// </summary>
        /// <param name="actorID">The actor identifier.</param>
        public StaticObj(string actorID)
            : base(actorID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticObj"/> class.
        /// </summary>
        /// <param name="sosd">The sosd.</param>
        public StaticObj(Object.StaticObjSaveData sosd)
            : base(sosd.ActorID)
        {
            FromStaticObjSaveData(sosd);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="StaticObj"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled
        {
            get { return b_enabled; }
            set { b_enabled = value; }
        }

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        /// <value>
        /// The filename.
        /// </value>
        public string Filename
        {
            get { return s_filename; }
            set { s_filename = value; }
        }

        /// <summary>
        /// Dictates whether the objects mass should be serilized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [mass modified]; otherwise, <c>false</c>.
        /// </value>
        public bool MassModified
        {
            get { return b_massModified; }
        }

        /// <summary>
        /// Gets the physics filename.
        /// </summary>
        /// <value>
        /// The physics filename.
        /// </value>
        public string PhysicsFilename
        {
            get { return s_physicsFilename; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [serilize object].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [serilize object]; otherwise, <c>false</c>.
        /// </value>
        public bool SerilizeObj
        {
            get { return b_serilizeObj; }
            set { b_serilizeObj = value; }
        }

        /// <summary>
        /// Gets or sets the subset materials.
        /// </summary>
        /// <value>
        /// The subset materials.
        /// </value>
        public List<SubsetMaterial> SubsetMaterials
        {
            get { return _subsetMaterials; }
            set { _subsetMaterials = value; }
        }

        /// <summary>
        /// Gets the subset world matricies.
        /// </summary>
        /// <value>
        /// The subset world matricies.
        /// </value>
        public Matrix[] SubsetWorldMatricies
        {
            get
            {
                var subsetMats = from sm in _renderingSystemMesh.SubMeshes
                                 select sm.World;

                return subsetMats.ToArray();
            }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override GameObj Clone()
        {
            StaticObj staticObj = new StaticObj(Guid.NewGuid().ToString());
            staticObj.LoadContent(GameSystem.GameSys_Instance.Content, s_filename);
            staticObj.Position = Position;
            staticObj.Rotation = Rotation;
            staticObj.Scale = Scale;
            staticObj.SubsetMaterials = SubsetMaterials;
            staticObj.SerilizeObj = SerilizeObj;
            staticObj._rigidBody.MassProperties = _rigidBody.MassProperties;
            staticObj.b_massModified = b_massModified;

            return staticObj;
        }

        /// <summary>
        /// Froms the static object save data.
        /// </summary>
        /// <param name="sosd">The sosd.</param>
        /// <exception cref="System.ArgumentException">Invalid physics file data!</exception>
        public void FromStaticObjSaveData(Object.StaticObjSaveData sosd)
        {
            LoadContent(ResourceMgr.Content, sosd.Filename);
            Position = sosd.Position;
            Rotation = sosd.Rotation;
            foreach (SubsetMaterial sm in sosd.SubsetMaterials)
            {
                if (sm.NormalMap.Filename != null && sm.NormalMap.Filename != "")
                    sm.NormalMap = new GameTexture(sm.NormalMap.Filename);
                else
                    sm.NormalMap = GameTexture.Null;

                if (sm.DiffuseMap.Filename != null && sm.DiffuseMap.Filename != "")
                    sm.DiffuseMap = new GameTexture(sm.DiffuseMap.Filename);
                else
                    sm.DiffuseMap = GameTexture.Null;

                if (sm.SpecularMap.Filename != null && sm.SpecularMap.Filename != "")
                    sm.SpecularMap = new GameTexture(sm.SpecularMap.Filename);
                else
                    sm.SpecularMap = GameTexture.Null;

                if (sm.EmmisiveMap.Filename != null && sm.EmmisiveMap.Filename != "")
                    sm.EmmisiveMap = new GameTexture(sm.EmmisiveMap.Filename);
                else
                    sm.EmmisiveMap = GameTexture.Null;
            }
            SubsetMaterials = sosd.SubsetMaterials.ToList();
            Scale = sosd.Scale;
            if (sosd.PhysDataFilename != "" && sosd.PhysDataFilename != null)
            {
                string contentFoldername = Object.FileHelper.GetContentSaveLocation();
                string physicsContentFoldername = contentFoldername + PHYSICS_DATA_FOLDERNAME;
                string physicsDataFilename = physicsContentFoldername + sosd.PhysDataFilename;

                System.IO.Stream stream = System.IO.File.Open(physicsDataFilename, System.IO.FileMode.Open);
                if (!LoadPhysicsFromFile(stream, physicsDataFilename))
                    throw new ArgumentException("Invalid physics file data!");
            }
            if (sosd.ObjMass != Object.StaticObjSaveData.INVALID_MASS)
            {
                if (sosd.ObjMass == Henge3D.Physics.MassProperties.Immovable.Mass)
                    SetMass(Henge3D.Physics.MassProperties.Immovable);
                else
                {
                    Henge3D.Physics.MassProperties massProps = new Henge3D.Physics.MassProperties(sosd.ObjMass, sosd.Inertia);
                    SetMass(massProps);
                }
            }
        }

        /// <summary>
        /// Gets the static object save data.
        /// </summary>
        /// <returns></returns>
        public Object.StaticObjSaveData GetStaticObjSaveData()
        {
            Object.StaticObjSaveData sosd;
            sosd.Position = this.Position;
            sosd.Rotation = this.Rotation;
            sosd.Scale = this.Scale;
            sosd.ActorID = this.ActorID;
            sosd.Filename = this.Filename;
            if (this.PhysicsFilename != null)
            {
                sosd.PhysDataFilename = this.PhysicsFilename;
            }
            else
            {
                sosd.PhysDataFilename = "";
            }
            sosd.SubsetMaterials = this.SubsetMaterials.ToArray();

            if (this.MassModified)
            {
                sosd.ObjMass = this.RigidBody.MassProperties.Mass;
                sosd.Inertia = this.RigidBody.MassProperties.Inertia;
            }
            else
            {
                sosd.ObjMass = Object.StaticObjSaveData.INVALID_MASS;
                sosd.Inertia = Matrix.Identity;
            }

            return sosd;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public bool LoadContent(Model model, string filename)
        {
            s_filename = filename;

            SetModel(model);

            _renderingSystemMesh = new RenderingSystem.RendererImpl.Mesh();
            _renderingSystemMesh.SetModel(model);
            RigidBodyInfo rbi = model.Tag as RigidBodyInfo;
            _renderingSystemMesh.SetMetadata(rbi.Metadata);

            LoadMaterials(model);

            return true;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="contentMgr">The content MGR.</param>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public bool LoadContent(ContentManager contentMgr, string filename)
        {
            Model model = ResourceMgr.LoadModel(filename);
            if (model == null)
                return false;

            return LoadContent(model, filename);
        }

        /// <summary>
        /// Loads the physics from file.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public bool LoadPhysicsFromFile(System.IO.Stream stream, string filename)
        {
            var transform = _rigidBody.Transform;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(stream);

                // Remove the rigid body to make way for a new one.
                GameSystem.GameSys_Instance.PhysicsMgr.RemoveRigidBody(_rigidBody);
                _rigidBody = new RigidBody();

                XmlNode boundingBoxes = doc.ChildNodes[1];
                foreach (XmlNode node in boundingBoxes)
                {
                    if (node.Name == "BoundingBox")
                    {
                        XmlNode minNode = node.ChildNodes[0];
                        XmlNode maxNode = node.ChildNodes[1];

                        string xValMin = minNode.ChildNodes[0].InnerText;
                        string yValMin = minNode.ChildNodes[1].InnerText;
                        string zValMin = minNode.ChildNodes[2].InnerText;

                        string xValMax = maxNode.ChildNodes[0].InnerText;
                        string yValMax = maxNode.ChildNodes[1].InnerText;
                        string zValMax = maxNode.ChildNodes[2].InnerText;

                        float minX, minY, minZ, maxX, maxY, maxZ;

                        if (!float.TryParse(xValMin, out minX))
                            return false;
                        if (!float.TryParse(yValMin, out minY))
                            return false;
                        if (!float.TryParse(zValMin, out minZ))
                            return false;
                        if (!float.TryParse(xValMax, out maxX))
                            return false;
                        if (!float.TryParse(yValMax, out maxY))
                            return false;
                        if (!float.TryParse(zValMax, out maxZ))
                            return false;

                        Vector3 min = new Vector3(minX, minY, minZ);
                        Vector3 max = new Vector3(maxX, maxY, maxZ);

                        BoundingBox bb = new BoundingBox(min, max);
                        Vector3[] corners = bb.GetCorners();

                        ConvexHull3D hull = new ConvexHull3D(corners);
                        CompiledPolyhedron compiled = hull.ToPolyhedron();

                        PolyhedronPart polyPart = new PolyhedronPart(compiled);

                        _rigidBody.Skin.Add(polyPart, new Henge3D.Physics.Material(0f, 0.5f));

                        SetMass(MassProperties.Immovable);

                        GameSystem.GameSys_Instance.PhysicsMgr.AddRigidBody(_rigidBody);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            _rigidBody.SetWorld(transform.Scale, transform.Position, transform.Orientation);
            s_physicsFilename = filename;
            return true;
        }

        /// <summary>
        /// Sets the diffuse map.
        /// </summary>
        /// <param name="diffuseMap">The diffuse map.</param>
        public void SetDiffuseMap(GameTexture diffuseMap)
        {
            for (int i = 0; i < _subsetMaterials.Count; ++i)
                SetDiffuseMap(diffuseMap, i);
        }

        /// <summary>
        /// Sets the diffuse map.
        /// </summary>
        /// <param name="diffuseMap">The diffuse map.</param>
        /// <param name="subset">The subset.</param>
        public void SetDiffuseMap(GameTexture diffuseMap, int subset)
        {
            _subsetMaterials.ElementAt(subset).DiffuseMap = diffuseMap;
        }

        /// <summary>
        /// Sets the mass.
        /// </summary>
        /// <param name="massProps">The mass props.</param>
        public void SetMass(MassProperties massProps)
        {
            base._rigidBody.MassProperties = massProps;
            b_massModified = true;
        }

        /// <summary>
        /// Sets the mass.
        /// </summary>
        /// <param name="mass">The mass.</param>
        public void SetMass(float mass)
        {
            MassProperties mp = this.RigidBody.MassProperties;
            if (float.IsInfinity(mass))
                SetMass(MassProperties.Immovable);
            else
                SetMass(new MassProperties(mass, mp.Inertia));
        }

        /// <summary>
        /// Sets the mass.
        /// </summary>
        /// <param name="mass">The mass.</param>
        /// <param name="inertia">The inertia.</param>
        public void SetMass(float mass, Matrix inertia)
        {
            if (float.IsInfinity(mass))
                SetMass(MassProperties.Immovable);
            else
                SetMass(new MassProperties(mass, inertia));
        }

        /// <summary>
        /// Sets the normal map.
        /// </summary>
        /// <param name="normalMap">The normal map.</param>
        public void SetNormalMap(GameTexture normalMap)
        {
            for (int i = 0; i < _subsetMaterials.Count; ++i)
                SetNormalMap(normalMap, i);
        }

        /// <summary>
        /// Sets the normal map.
        /// </summary>
        /// <param name="normalMap">The normal map.</param>
        /// <param name="subset">The subset.</param>
        public void SetNormalMap(GameTexture normalMap, int subset)
        {
            _subsetMaterials.ElementAt(subset).NormalMap = normalMap;
        }

        /// <summary>
        /// Sets the specular map.
        /// </summary>
        /// <param name="specularMap">The specular map.</param>
        public void SetSpecularMap(GameTexture specularMap)
        {
            for (int i = 0; i < _subsetMaterials.Count; ++i)
                SetSpecularMap(specularMap, i);
        }

        /// <summary>
        /// Sets the specular map.
        /// </summary>
        /// <param name="specMap">The spec map.</param>
        /// <param name="subset">The subset.</param>
        public void SetSpecularMap(GameTexture specMap, int subset)
        {
            _subsetMaterials.ElementAt(subset).SpecularMap = specMap;
        }

        /// <summary>
        /// Sets the texture scaling.
        /// </summary>
        /// <param name="scale">The scale.</param>
        public void SetTextureScaling(float scale)
        {
            for (int i = 0; i < _subsetMaterials.Count; ++i)
                SetTextureScaling(new Vector3(scale), i);
        }

        /// <summary>
        /// Sets the texture scaling.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <param name="subset">The subset.</param>
        public void SetTextureScaling(Vector3 scale, int subset)
        {
            _subsetMaterials.ElementAt(subset).TexTransform = Matrix.CreateScale(scale);
        }

        /// <summary>
        /// To the renderer mesh.
        /// </summary>
        /// <returns></returns>
        public override RenderingSystem.RendererImpl.Mesh ToRendererMesh()
        {
            _renderingSystemMesh.Transform = _rigidBody.Transform.Combined;
            _renderingSystemMesh.SubsetMaterials = _subsetMaterials.ToArray();

            // Set the rendering technique appropriately.
            for (int i = 0; i < _subsetMaterials.Count; ++i)
            {
                SubsetMaterial sm = _subsetMaterials[i];

                if (sm.UseAlphaMask)
                    _renderingSystemMesh.SubMeshes[i].RenderQueue = RenderingSystem.RendererImpl.MeshData.RenderQueueType.Blend;
                else
                    _renderingSystemMesh.SubMeshes[i].RenderQueue = RenderingSystem.RendererImpl.MeshData.RenderQueueType.Default;
            }

            _renderingSystemMesh.Enabled = b_enabled;

            return _renderingSystemMesh;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ActorID + ": " + Position.ToString();
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            foreach (SubsetMaterial sm in _subsetMaterials)
            {
                sm.UnloadContent();
            }
        }

        /// <summary>
        /// Loads the materials.
        /// </summary>
        /// <param name="model">The model.</param>
        private void LoadMaterials(Model model)
        {
            string[] diffuseFilenames = (model.Tag as RigidBodyInfo).DiffuseMapFilenames;
            string[] normalFilenames = (model.Tag as RigidBodyInfo).NormalMapFilenames;
            string[] specularFilenames = (model.Tag as RigidBodyInfo).SpecularMapFilenames;

            //var gameTexturesTmp = from df in diffuseFilenames
            //                      let truePath = Object.LevelSerilizer.RemoveFileType(Object.LevelSerilizer.NavigateDownLevel(df, 7))
            //                      select new GameTexture(truePath);

            //Dictionary<int, GameTexture> subsetTextures = new Dictionary<int, GameTexture>();

            //RenderingSystem.RendererImpl.MeshMetadata metadata = (model.Tag as RigidBodyInfo).Metadata;
            //int subsetIndex = 0;
            //foreach (ModelMesh mesh in model.Meshes)
            //{
            //    foreach (ModelMeshPart part in mesh.MeshParts)
            //    {
            //        Effect effect = part.Effect;
            //        Texture2D diffuseMap = effect.Parameters["DiffuseMap"].GetValueTexture2D();
            //        foreach (GameTexture gt in gameTexturesTmp)
            //        {
            //            if (gt.Texture.Equals(diffuseMap))
            //            {
            //                int x = 0;
            //            }
            //        }

            //    }
            //    ++subsetIndex;
            //}

            //if (diffuseFilenames.Count() != materialCount && specularFilenames.Count() != materialCount && normalFilenames.Count() != materialCount)
            //    throw new ArgumentException("Filenames of diffuse, specular, and normals don't match.");

            for (int i = 0; i < _renderingSystemMesh.SubMeshes.Count; ++i)
            {
                SubsetMaterial sm = new SubsetMaterial();
                //sm.NormalMap = new GameTexture(normalFilenames[i]);
                //sm.SpecularMap = new GameTexture(specularFilenames[i]);
                //sm.DiffuseMap = new GameTexture(diffuseFilenames[i]);
                sm.DiffuseMap = GameTexture.Null;
                sm.NormalMap = GameTexture.Null;
                sm.SpecularMap = GameTexture.Null;
                sm.EmmisiveMap = GameTexture.Null;
                sm.TexTransform = Matrix.Identity;

                _subsetMaterials.Add(sm);
            }
        }
    }
}