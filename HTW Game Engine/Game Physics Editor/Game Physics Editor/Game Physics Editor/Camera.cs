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
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Physics_Editor
{
    /// <summary>
    /// The camera class from HTW Game Engine.
    /// </summary>
    class Camera
    {
        private Matrix _transform = Matrix.Identity;
        protected Matrix _view = Matrix.Identity;
        protected Matrix _projection = Matrix.Identity;
        protected Matrix _viewProj = Matrix.Identity;
        protected BoundingFrustum _frustum = new BoundingFrustum(Matrix.Identity);
        protected float _farClip = 100;
        protected float _nearClip = 1;
        protected float _fovY = MathHelper.PiOver4;
        protected float _tanFovy = (float)Math.Tan((double)MathHelper.PiOver4 / 2.0);
        protected float _aspect = 1;
        protected Vector3 _position = Vector3.Zero;
        protected float _pitch = 0f;
        protected float _yaw = 0f;
        protected Vector3 _upAxis = Vector3.Up;
        protected Vector3 _forwardAxis = -Vector3.UnitZ;
        protected Vector3 _sideAxis = Vector3.UnitX;
        protected float _minPitch;
        protected float _maxPitch;

        private bool b_kill;
        public bool Kill
        {
            get { return b_kill; }
            set { b_kill = value; }
        }

        public float MaxPitch
        {
            get { return _maxPitch; }
            set { _maxPitch = value; }
        }

        public float MinPitch
        {
            get { return _minPitch; }
            set { _minPitch = value; }
        }

        public float Pitch
        {
            get { return _pitch; }
            set
            {
                _pitch = MathHelper.Clamp(value, _minPitch, _maxPitch); ;
                UpdateView();
            }
        }

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

        public Vector3 UpAxis
        {
            set { _upAxis = value; }
            get { return _upAxis; }
        }

        public Vector3 SideAxis
        {
            set { _sideAxis = value; }
            get { return _sideAxis; }
        }

        public Vector3 ForwardAxis
        {
            get { return _forwardAxis; }
            set { _forwardAxis = value; }
        }

        public float Yaw
        {
            get { return _yaw; }
            set
            {
                _yaw = (value >= MathHelper.TwoPi) ? value % MathHelper.TwoPi : value; ;
                UpdateView();
            }
        }

        public Vector3 Position
        {
            set { _position = value; UpdateView(); }
            get { return _position; }
        }

        private void UpdateView()
        {
            Matrix transform = Transform;
            Matrix.Invert(ref transform, out _view);
            Matrix.Multiply(ref _view, ref _projection, out _viewProj);

            _frustum.Matrix = _viewProj;
        }

        public Matrix Transform
        {
            get
            {
                Matrix transform = Matrix.CreateRotationX(_pitch) * Matrix.CreateRotationY(_yaw);
                transform = transform * Matrix.CreateTranslation(_position);
                return transform;
            }
        }

        public Matrix ViewProj
        {
            get { return _view * _projection; }
        }

        public Camera()
        {
            ComputeProjectionTransform();
        }

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

        public virtual void Update(GameTime gameTime)
        {
            UpdateView();
        }
        
        public Matrix View
        {
            get { return _view; }
        }

        public Matrix Proj
        {
            get { return _projection; }
        }


        public BoundingFrustum Frustum
        {
            get { return _frustum; }
        }

        public float FarClip
        {
            get { return _farClip; }
            set
            {
                _farClip = value;
                ComputeProjectionTransform();
            }
        }

        public float NearClip
        {
            get { return _nearClip; }
            set
            {
                _nearClip = value;
                ComputeProjectionTransform();
            }
        }

        public float Aspect
        {
            get { return _aspect; }
            set
            {
                _aspect = value;
                ComputeProjectionTransform();
            }
        }

        public float TanFovy
        {
            get { return _tanFovy; }
        }

        public void SetProjection(float planeNear, float planeFar, float fov, float aspectRatio)
        {
            this.NearClip = planeNear;
            this.FarClip = planeFar;
            this.Aspect = aspectRatio;

            ComputeProjectionTransform();
        }

        public void ComputeProjectionTransform()
        {
            Matrix.CreatePerspectiveFieldOfView(_fovY,
                        Aspect, _nearClip, _farClip, out _projection);
            Matrix.Multiply(ref _view, ref _projection, out _viewProj);
        }
    }
}
