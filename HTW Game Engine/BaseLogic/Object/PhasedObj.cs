#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RenderingSystem;

namespace BaseLogic.Object
{
    // A physics ghost.
    /// <summary>
    ///
    /// </summary>
    public class PhasedObj : GameObj
    {
        /// <summary>
        /// The _model
        /// </summary>
        private Model _model;

        /// <summary>
        /// The _rendering system mesh
        /// </summary>
        private RenderingSystem.RendererImpl.Mesh _renderingSystemMesh;

        /// <summary>
        /// The _subset materials
        /// </summary>
        private List<SubsetMaterial> _subsetMaterials = new List<SubsetMaterial>();

        /// <summary>
        /// The b_kill
        /// </summary>
        private bool b_kill = false;

        /// <summary>
        /// The f_scale
        /// </summary>
        private float f_scale = 1f;

        /// <summary>
        /// The q_rot
        /// </summary>
        private Quaternion q_rot = Quaternion.Identity;

        /// <summary>
        /// The s_actor identifier
        /// </summary>
        private string s_actorID;

        /// <summary>
        /// The s_filename
        /// </summary>
        private string s_filename;

        /// <summary>
        /// The v_pos
        /// </summary>
        private Vector3 v_pos = Vector3.Zero;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhasedObj"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public PhasedObj(string id)
        {
            this.s_actorID = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhasedObj"/> class.
        /// </summary>
        public PhasedObj()
            : this(Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// The unique identification of the actor.
        /// </summary>
        public string ActorID
        {
            get { return s_actorID; }
            set { s_actorID = value; }
        }

        /// <summary>
        /// Setting to true will remove the object from the manager in charge of it.
        /// </summary>
        public bool Kill
        {
            get { return b_kill; }
            set { b_kill = value; }
        }

        /// <summary>
        /// The position of the object in the 3D scene.
        /// </summary>
        public Vector3 Position
        {
            get { return v_pos; }
            set { v_pos = value; }
        }

        /// <summary>
        /// Rotation of the object.
        /// </summary>
        public Quaternion Rotation
        {
            get { return q_rot; }
            set { q_rot = value; }
        }

        /// <summary>
        /// Uniform scaling of the object.
        /// </summary>
        public float Scale
        {
            get { return f_scale; }
            set { f_scale = value; }
        }

        /// <summary>
        /// Gets the transform.
        /// </summary>
        /// <value>
        /// The transform.
        /// </value>
        public Matrix Transform
        {
            get
            {
                Matrix trans = Matrix.CreateTranslation(v_pos);
                Matrix rot = Matrix.CreateFromQuaternion(q_rot);
                Matrix scale = Matrix.CreateScale(f_scale);

                return scale * rot * trans;
            }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public GameObj Clone()
        {
            PhasedObj phasedObj = new PhasedObj(Guid.NewGuid().ToString());
            phasedObj.LoadContent(s_filename);
            phasedObj.Position = v_pos;
            phasedObj.Rotation = q_rot;
            phasedObj.Scale = f_scale;
            phasedObj._subsetMaterials = _subsetMaterials;

            return phasedObj;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="absoluteFilename">The absolute filename.</param>
        /// <returns></returns>
        public bool LoadContent(string absoluteFilename)
        {
            s_filename = absoluteFilename;
            _model = ResourceMgr.LoadModel(absoluteFilename);
            if (_model == null)
                return false;

            _renderingSystemMesh = new RenderingSystem.RendererImpl.Mesh();
            _renderingSystemMesh.SetModel(_model);
            RenderingSystem.RendererImpl.MeshData metadata = _model.Tag as RenderingSystem.RendererImpl.MeshData;
            _renderingSystemMesh.SetMetadata(metadata);

            for (int i = 0; i < _renderingSystemMesh.SubMeshes.Count; ++i)
            {
                SubsetMaterial sm = new SubsetMaterial();
                sm.DiffuseMap = GameTexture.Null;
                sm.NormalMap = GameTexture.Null;
                sm.SpecularMap = GameTexture.Null;
                sm.TexTransform = Matrix.Identity;

                _subsetMaterials.Add(sm);
            }

            return true;
        }

        /// <summary>
        /// To the renderer mesh.
        /// </summary>
        /// <returns></returns>
        public virtual RenderingSystem.RendererImpl.Mesh ToRendererMesh()
        {
            _renderingSystemMesh.Transform = this.Transform;
            _renderingSystemMesh.SubsetMaterials = _subsetMaterials.ToArray();
            return _renderingSystemMesh;
        }

        /// <summary>
        /// Update the object.
        /// Called every frame by the manager.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
        }
    }
}