#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using Henge3D.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RenderingSystem;

namespace BaseLogic
{
    /// <summary>
    /// This class just allows each rigid body to have a name.
    /// </summary>
    public class GameRigidBody : RigidBody
    {
        /// <summary>
        /// The name
        /// </summary>
        public string Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRigidBody"/> class.
        /// </summary>
        /// <param name="idenStr">The iden string.</param>
        public GameRigidBody(string idenStr)
        {
            Name = idenStr;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRigidBody"/> class.
        /// </summary>
        /// <param name="rbModel">The rb model.</param>
        /// <param name="idenStr">The iden string.</param>
        public GameRigidBody(Henge3D.Pipeline.RigidBodyModel rbModel, string idenStr)
            : base(rbModel)
        {
            Name = idenStr;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRigidBody"/> class.
        /// </summary>
        /// <param name="skin">The skin.</param>
        /// <param name="idenStr">The iden string.</param>
        public GameRigidBody(BodySkin skin, string idenStr)
            : base(skin)
        {
            Name = idenStr;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// A static object that has a physics body.
    /// </summary>
    public class PhysObj : GameObj
    {
        /// <summary>
        /// The transformations of each of the bones.
        /// </summary>
        protected Matrix[] _boneTransforms;

        /// <summary>
        /// The _model
        /// </summary>
        protected Model _model;

        /// <summary>
        /// The _rigid body
        /// </summary>
        protected RigidBody _rigidBody;

        /// <summary>
        /// Remove the object from the manager?
        /// </summary>
        private bool b_kill = false;

        /// <summary>
        /// The fc_scale
        /// </summary>
        private float fc_scale = 1.0f;

        /// <summary>
        /// The s_actor identifier
        /// </summary>
        private string s_actorID = "static obj";

        /// <summary>
        /// Initializes a new instance of the <see cref="PhysObj"/> class.
        /// </summary>
        /// <param name="actorID">The actor identifier.</param>
        public PhysObj(string actorID)
        {
            s_actorID = actorID;
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
            get { return _rigidBody.Position; }
            set { _rigidBody.SetWorld(value); }
        }

        /// <summary>
        /// Gets the rigid body.
        /// </summary>
        /// <value>
        /// The rigid body.
        /// </value>
        public RigidBody RigidBody
        {
            get { return _rigidBody; }
        }

        /// <summary>
        /// Rotation of the object.
        /// </summary>
        public Quaternion Rotation
        {
            get { return _rigidBody.Orientation; }
            set { _rigidBody.SetWorld(Position, value); }
        }

        /// <summary>
        /// Uniform scaling of the object.
        /// </summary>
        public float Scale
        {
            get { return fc_scale; }
            set { fc_scale = value; _rigidBody.SetWorld(fc_scale, Position, Rotation); }
        }

        /// <summary>
        /// Gets the world.
        /// </summary>
        /// <value>
        /// The world.
        /// </value>
        public Matrix World
        {
            get { return _rigidBody.Transform.Combined; }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual GameObj Clone()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the linear velocity.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetLinearVelocity()
        {
            return _rigidBody.LinearVelocity;
        }

        /// <summary>
        /// Determines whether [has registered rb].
        /// </summary>
        /// <returns></returns>
        public bool HasRegisteredRB()
        {
            return _rigidBody != null;
        }

        /// <summary>
        /// Registers the rb.
        /// </summary>
        /// <param name="physicsMgr">The physics MGR.</param>
        public void RegisterRB(PhysicsManager physicsMgr)
        {
            physicsMgr.Add(_rigidBody);
        }

        /// <summary>
        /// Sets the linear velocity.
        /// </summary>
        /// <param name="vel">The vel.</param>
        public void SetLinearVelocity(Vector3 vel)
        {
            _rigidBody.IsActive = true;
            _rigidBody.SetVelocity(vel, _rigidBody.AngularVelocity);
        }

        /// <summary>
        /// Sets the rotation deg.
        /// </summary>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="rz">The rz.</param>
        public void SetRotationDeg(float rx, float ry, float rz)
        {
            rx = MathHelper.ToRadians(rx);
            ry = MathHelper.ToRadians(ry);
            rz = MathHelper.ToRadians(rz);
            Matrix rotMat = Matrix.CreateRotationX(rx) * Matrix.CreateRotationY(ry) * Matrix.CreateRotationZ(rz);

            Rotation = Quaternion.CreateFromRotationMatrix(rotMat);
        }

        /// <summary>
        /// Sets the rotation dir.
        /// </summary>
        /// <param name="lookAt">The look at.</param>
        public void SetRotationDir(Vector3 lookAt)
        {
            Matrix rotMat = Matrix.CreateLookAt(Vector3.Zero, lookAt, Vector3.Up);
            Quaternion rotQuat = Quaternion.CreateFromRotationMatrix(rotMat);

            Rotation = rotQuat;
        }

        /// <summary>
        /// To the renderer mesh.
        /// </summary>
        /// <returns></returns>
        public virtual RenderingSystem.RendererImpl.Mesh ToRendererMesh()
        {
            return null;
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public virtual void UnloadContent()
        {
        }

        /// <summary>
        /// Update the object.
        /// Called every frame by the manager.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="model">The model.</param>
        protected void SetModel(Model model)
        {
            _model = model;

            _boneTransforms = new Matrix[_model.Bones.Count];
            _model.CopyAbsoluteBoneTransformsTo(_boneTransforms);

            // Load the physics.
            Henge3D.Pipeline.RigidBodyModel rbm = ((RigidBodyInfo)_model.Tag).ToRigidBodyModel();
            _rigidBody = new GameRigidBody(rbm, ActorID);
        }
    }
}