#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RenderingSystem;

namespace BaseLogic.Camera
{
    /// <summary>
    /// A free look camera which is not constrained by any bounding box.
    /// </summary>
    public class FreeLookCamera : ICamera
    {
        /// <summary>
        /// The _aspect
        /// </summary>
        protected float _aspect = 1;

        /// <summary>
        /// The _far clip
        /// </summary>
        protected float _farClip = 100;

        /// <summary>
        /// The _forward axis
        /// </summary>
        protected Vector3 _forwardAxis = -Vector3.UnitZ;

        /// <summary>
        /// The _fov y
        /// </summary>
        protected float _fovY = MathHelper.PiOver4;

        /// <summary>
        /// The _frustum
        /// </summary>
        protected BoundingFrustum _frustum = new BoundingFrustum(Matrix.Identity);

        /// <summary>
        /// The _max pitch
        /// </summary>
        protected float _maxPitch;

        /// <summary>
        /// The _min pitch
        /// </summary>
        protected float _minPitch;

        /// <summary>
        /// The _near clip
        /// </summary>
        protected float _nearClip = 1;

        /// <summary>
        /// The _pitch
        /// </summary>
        protected float _pitch = 0f;

        /// <summary>
        /// The _position
        /// </summary>
        protected Vector3 _position = Vector3.Zero;

        /// <summary>
        /// The _projection
        /// </summary>
        protected Matrix _projection = Matrix.Identity;

        /// <summary>
        /// The _side axis
        /// </summary>
        protected Vector3 _sideAxis = Vector3.UnitX;

        /// <summary>
        /// The _tan fovy
        /// </summary>
        protected float _tanFovy = (float)Math.Tan((double)MathHelper.PiOver4 / 2.0);

        /// <summary>
        /// The _up axis
        /// </summary>
        protected Vector3 _upAxis = Vector3.Up;

        /// <summary>
        /// The _view
        /// </summary>
        protected Matrix _view = Matrix.Identity;

        /// <summary>
        /// The _view proj
        /// </summary>
        protected Matrix _viewProj = Matrix.Identity;

        /// <summary>
        /// The _yaw
        /// </summary>
        protected float _yaw = 0f;

        /// <summary>
        /// The _transform
        /// </summary>
        private Matrix _transform = Matrix.Identity;

        /// <summary>
        /// The b_kill
        /// </summary>
        private bool b_kill;

        /// <summary>
        /// The s_actor identifier
        /// </summary>
        private string s_actorID;

        /// <summary>
        /// Initializes a new instance of the <see cref="FreeLookCamera"/> class.
        /// </summary>
        public FreeLookCamera()
        {
            ComputeProjectionTransform();
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
        /// Gets or sets the aspect.
        /// </summary>
        /// <value>
        /// The aspect.
        /// </value>
        public float Aspect
        {
            get { return _aspect; }
            set
            {
                _aspect = value;
                ComputeProjectionTransform();
            }
        }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public Vector3 Direction
        {
            get
            {
                return Vector3.TransformNormal(
                    _forwardAxis,
                    Matrix.CreateFromAxisAngle(_sideAxis, _pitch) * Matrix.CreateFromAxisAngle(_upAxis, _yaw));
            }
            set
            {
                this.Pitch = (float)Math.Asin(Vector3.Dot(_upAxis, value));
                this.Yaw = (float)Math.Atan2(
                    Vector3.Dot(_forwardAxis, value), Vector3.Dot(-_sideAxis, value));
            }
        }

        /// <summary>
        /// Gets or sets the far clip.
        /// </summary>
        /// <value>
        /// The far clip.
        /// </value>
        public float FarClip
        {
            get { return _farClip; }
            set
            {
                _farClip = value;
                ComputeProjectionTransform();
            }
        }

        /// <summary>
        /// Gets or sets the forward axis.
        /// </summary>
        /// <value>
        /// The forward axis.
        /// </value>
        public Vector3 ForwardAxis
        {
            get { return _forwardAxis; }
            set { _forwardAxis = value; }
        }

        /// <summary>
        /// Gets the frustum.
        /// </summary>
        /// <value>
        /// The frustum.
        /// </value>
        public BoundingFrustum Frustum
        {
            get { return _frustum; }
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
        /// Gets or sets the maximum pitch.
        /// </summary>
        /// <value>
        /// The maximum pitch.
        /// </value>
        public float MaxPitch
        {
            get { return _maxPitch; }
            set { _maxPitch = value; }
        }

        /// <summary>
        /// Gets or sets the minimum pitch.
        /// </summary>
        /// <value>
        /// The minimum pitch.
        /// </value>
        public float MinPitch
        {
            get { return _minPitch; }
            set { _minPitch = value; }
        }

        /// <summary>
        /// Gets or sets the near clip.
        /// </summary>
        /// <value>
        /// The near clip.
        /// </value>
        public float NearClip
        {
            get { return _nearClip; }
            set
            {
                _nearClip = value;
                ComputeProjectionTransform();
            }
        }

        /// <summary>
        /// Gets or sets the pitch.
        /// </summary>
        /// <value>
        /// The pitch.
        /// </value>
        public float Pitch
        {
            get { return _pitch; }
            set
            {
                _pitch = MathHelper.Clamp(value, _minPitch, _maxPitch); ;
                UpdateView();
            }
        }

        /// <summary>
        /// The position of the object in the 3D scene.
        /// </summary>
        public Vector3 Position
        {
            set { _position = value; UpdateView(); }
            get { return _position; }
        }

        /// <summary>
        /// Gets the proj.
        /// </summary>
        /// <value>
        /// The proj.
        /// </value>
        public Matrix Proj
        {
            get { return _projection; }
        }

        /// <summary>
        /// Gets or sets the side axis.
        /// </summary>
        /// <value>
        /// The side axis.
        /// </value>
        public Vector3 SideAxis
        {
            set { _sideAxis = value; }
            get { return _sideAxis; }
        }

        /// <summary>
        /// Gets the tan fovy.
        /// </summary>
        /// <value>
        /// The tan fovy.
        /// </value>
        public float TanFovy
        {
            get { return _tanFovy; }
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
                Matrix transform = Matrix.CreateRotationX(_pitch) * Matrix.CreateRotationY(_yaw);
                transform = transform * Matrix.CreateTranslation(_position);
                return transform;
            }
        }

        /// <summary>
        /// Gets or sets up axis.
        /// </summary>
        /// <value>
        /// Up axis.
        /// </value>
        public Vector3 UpAxis
        {
            set { _upAxis = value; }
            get { return _upAxis; }
        }

        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <value>
        /// The view.
        /// </value>
        public Matrix View
        {
            get { return _view; }
        }

        /// <summary>
        /// Gets the view proj.
        /// </summary>
        /// <value>
        /// The view proj.
        /// </value>
        public Matrix ViewProj
        {
            get { return _view * _projection; }
        }

        /// <summary>
        /// Gets or sets the yaw.
        /// </summary>
        /// <value>
        /// The yaw.
        /// </value>
        public float Yaw
        {
            get { return _yaw; }
            set
            {
                _yaw = (value >= MathHelper.TwoPi) ? value % MathHelper.TwoPi : value; ;
                UpdateView();
            }
        }

        /// <summary>
        /// Computes the projection transform.
        /// </summary>
        public void ComputeProjectionTransform()
        {
            Matrix.CreatePerspectiveFieldOfView(_fovY,
                        Aspect, _nearClip, _farClip, out _projection);
            Matrix.Multiply(ref _view, ref _projection, out _viewProj);
        }

        /// <summary>
        /// Moves the specified delta.
        /// </summary>
        /// <param name="delta">The delta.</param>
        /// <param name="speed">The speed.</param>
        /// <param name="dt">The dt.</param>
        public virtual void Move(Vector3 delta, float speed, float dt)
        {
            if (delta != Vector3.Zero)
            {
                delta = -delta * speed * dt;
                Vector3 sideAxis;
                Vector3.Cross(ref _forwardAxis, ref _upAxis, out sideAxis);
                _position += Vector3.Transform(
                    delta,
                    Matrix.CreateFromAxisAngle(Vector3.UnitX, _pitch) * Matrix.CreateFromAxisAngle(Vector3.UnitY, _yaw));
            }
        }

        /// <summary>
        /// Sets the default.
        /// </summary>
        /// <param name="vp">The vp.</param>
        public void SetDefault(Viewport vp)
        {
            FarClip = 1000f;
            NearClip = 0.1f;
            Aspect = (float)vp.Width / (float)vp.Height;
            _position = Vector3.Zero;
            _pitch = 0f;
            _yaw = 0f;
            MinPitch = MathHelper.ToRadians(-89.9f);
            MaxPitch = MathHelper.ToRadians(89.9f);
            UpdateView();
        }

        /// <summary>
        /// Sets the projection.
        /// </summary>
        /// <param name="planeNear">The plane near.</param>
        /// <param name="planeFar">The plane far.</param>
        /// <param name="fov">The fov.</param>
        /// <param name="aspectRatio">The aspect ratio.</param>
        public void SetProjection(float planeNear, float planeFar, float fov, float aspectRatio)
        {
            this.NearClip = planeNear;
            this.FarClip = planeFar;
            this.Aspect = aspectRatio;

            ComputeProjectionTransform();
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void Update(GameTime gameTime)
        {
            UpdateView();
        }

        /// <summary>
        /// Updates the view.
        /// </summary>
        private void UpdateView()
        {
            Matrix transform = Transform;
            Matrix.Invert(ref transform, out _view);
            Matrix.Multiply(ref _view, ref _projection, out _viewProj);

            _frustum.Matrix = _viewProj;
        }
    }
}