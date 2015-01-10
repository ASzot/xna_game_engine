#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using Microsoft.Xna.Framework;

namespace BaseLogic.Camera
{
    /// <summary>
    ///
    /// </summary>
    public class FirstPersonCamera : FreeLookCamera
    {
        /// <summary>
        /// The _eye offset
        /// </summary>
        private Vector3 _eyeOffset = new Vector3(0f, 3f, 0f);

        /// <summary>
        /// The _player
        /// </summary>
        private Player.UserPlayer _player;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstPersonCamera"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        public FirstPersonCamera(Player.UserPlayer player)
        {
            _player = player;
        }

        /// <summary>
        /// Gets or sets the eye offset.
        /// </summary>
        /// <value>
        /// The eye offset.
        /// </value>
        public Vector3 EyeOffset
        {
            get { return _eyeOffset; }
            set { _eyeOffset = value; }
        }

        /// <summary>
        /// Moves the specified delta.
        /// </summary>
        /// <param name="delta">The delta.</param>
        /// <param name="speed">The speed.</param>
        /// <param name="dt">The dt.</param>
        public override void Move(Vector3 delta, float speed, float dt)
        {
            if (delta != Vector3.Zero)
            {
                delta = -delta;

                Vector3 sideAxis;
                Vector3.Cross(ref _forwardAxis, ref _upAxis, out sideAxis);
                delta = Vector3.Transform(delta, Matrix.CreateFromAxisAngle(_upAxis, _yaw));

                delta.Normalize();
                delta.Y = 0f;
                delta *= speed;

                _player.SetVelocity(delta);
            }
            else
            {
                if (!_player.IsFalling())
                    _player.SetVelocity(new Vector3(0.001f, 0f, 0f));
            }
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Quaternion yawRot = Quaternion.CreateFromAxisAngle(Vector3.UnitY, _yaw);
            Vector3 transformedEyePos = Vector3.Transform(_eyeOffset, yawRot);
            this.Position = _player.Position + transformedEyePos;
            Vector3 dir = Vector3.UnitZ;
            Quaternion rot = Quaternion.CreateFromYawPitchRoll(_yaw, _pitch, 0f);
            _player.CachedCamRot = rot;

            base.Update(gameTime);
        }
    }
}