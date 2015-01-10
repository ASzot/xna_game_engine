#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using Microsoft.Xna.Framework;

namespace BaseLogic.Player
{
    /// <summary>
    ///
    /// </summary>
    public class UserPlayer : GamePlayer
    {
        /// <summary>
        /// The use r_ playe r_ identifier
        /// </summary>
        public const string USER_PLAYER_ID = "user player";

        public static float JUMP_POWER = 5f;

        /// <summary>
        /// The local rot
        /// </summary>
        public Quaternion LocalRot = Quaternion.Identity;

        /// <summary>
        /// The local trans
        /// </summary>
        public Vector3 LocalTrans = Vector3.Zero;

        /// <summary>
        /// The p_hands
        /// </summary>
        protected Object.PhasedAnimObj p_hands;

        /// <summary>
        /// The _audio listener
        /// </summary>
        private Microsoft.Xna.Framework.Audio.AudioListener _audioListener;

        /// <summary>
        /// The b_weapon aimed
        /// </summary>
        private bool b_weaponAimed = false;

        /// <summary>
        /// The b_weapon raised
        /// </summary>
        private bool b_weaponRaised = false;

        /// <summary>
        /// The p_camera
        /// </summary>
        private Camera.FreeLookCamera p_camera;

        /// <summary>
        /// The Q_CC cam rot
        /// </summary>
        private Quaternion q_ccCamRot;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserPlayer"/> class.
        /// </summary>
        public UserPlayer()
            : base(USER_PLAYER_ID)
        {
            _audioListener = new Microsoft.Xna.Framework.Audio.AudioListener();
        }

        /// <summary>
        /// Gets the audio listener.
        /// </summary>
        /// <value>
        /// The audio listener.
        /// </value>
        public Microsoft.Xna.Framework.Audio.AudioListener AudioListener
        {
            get { return _audioListener; }
        }

        /// <summary>
        /// Gets or sets the cached cam rot.
        /// </summary>
        /// <value>
        /// The cached cam rot.
        /// </value>
        public Quaternion CachedCamRot
        {
            get { return q_ccCamRot; }
            set { q_ccCamRot = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has weapon aimed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has weapon aimed; otherwise, <c>false</c>.
        /// </value>
        public bool HasWeaponAimed
        {
            get { return b_weaponAimed; }
            set { b_weaponAimed = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has weapon raised.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has weapon raised; otherwise, <c>false</c>.
        /// </value>
        public bool HasWeaponRaised
        {
            get { return b_weaponRaised; }
            set { b_weaponRaised = value; }
        }

        /// <summary>
        /// Bashes the weapon.
        /// </summary>
        public void BashWeapon()
        {
            DoHandAnimation("raise weapon->bash");
        }

        /// <summary>
        /// Does the hand animation.
        /// </summary>
        /// <param name="animName">Name of the anim.</param>
        public void DoHandAnimation(string animName)
        {
            p_hands.SwitchAnimations(animName);
            AnimationSpan animSpan = p_hands.GetCurrentAnimSpan();
            LocalTrans = animSpan.AnimOffset;
        }

        /// <summary>
        /// Gets the hand transform.
        /// </summary>
        /// <returns></returns>
        public override Matrix GetHandTransform()
        {
            return p_hands.GetBoneTransform("hand_R");
        }

        /// <summary>
        /// Gets the rotation.
        /// </summary>
        /// <returns></returns>
        public override Quaternion GetRotation()
        {
            return q_ccCamRot;
        }

        /// <summary>
        /// Reloads this instance.
        /// </summary>
        public virtual void Reload()
        {
            if (_weapon.CurrentAmmo == _weapon.ClipSize || _weapon.TotalAmmo == 0)
                return;
            DoHandAnimation("raise weapon->reload");
            _weapon.Reload();
        }

        /// <summary>
        /// Sets the camera.
        /// </summary>
        /// <param name="fpc">The FPC.</param>
        public void SetCamera(Camera.FreeLookCamera fpc)
        {
            p_camera = fpc;
        }

        /// <summary>
        /// Sets the hands model.
        /// </summary>
        /// <param name="hands">The hands.</param>
        public void SetHandsModel(Object.PhasedAnimObj hands)
        {
            p_hands = hands;
            const float scaleFactor = 6f;
            p_hands.Scale = p_gameObj.Scale / scaleFactor;
        }

        /// <summary>
        /// Shoots the weapon.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void ShootWeapon(GameTime gameTime)
        {
            // Do we even have a weapon.
            if (_weapon == null)
                return;

            Matrix transformMat = Matrix.CreateFromQuaternion(GetRotation()) * Matrix.CreateTranslation(Position);
            Vector3 start = new Vector3(0f, f_yRayFireOffset, f_zRayStart);
            Vector3 end = new Vector3(0f, f_yRayFireOffset, f_zRayDistance);
            start = Vector3.Transform(start, transformMat);
            end = Vector3.Transform(end, transformMat);

            Manager.RaytraceIntersectionInfo raytraceIntersection = RaytraceFunc(new Manager.RaytraceFireInfo(start, end));
            if (raytraceIntersection != null)
            {
                // We actually hit something.
                Henge3D.Physics.RigidBody rigidBody = raytraceIntersection.IntersectionBody;
                if (rigidBody is GameRigidBody)
                {
                    GameRigidBody gameRigidBody = rigidBody as GameRigidBody;
                    // Check if there is a controller for this object.
                    GamePlayer hitPlayer = p_PlayerMgr.GetGamePlayerForGameRB(gameRigidBody);
                    if (hitPlayer != null)
                    {
                        s_lastAggressorId = hitPlayer.ActorID;
                        hitPlayer.RecieveMsg(new PlayerMessage(this, PlayerMessageType.InflictDamage), gameTime);
                    }
                }
            }
            _weapon.Shoot(gameTime);

            FireWeapon();
        }

        /// <summary>
        /// Toggles the aim weapon.
        /// </summary>
        public virtual void ToggleAimWeapon()
        {
            b_weaponAimed = !b_weaponAimed;

            if (b_weaponAimed)
                DoHandAnimation("raise weapon->aim weapon");
            else if (!b_weaponAimed)
                DoHandAnimation("aim weapon->raise weapon");
        }

        /// <summary>
        /// Toggles the weapon raised.
        /// </summary>
        public virtual void ToggleWeaponRaised()
        {
            b_weaponRaised = !b_weaponRaised;

            if (b_weaponRaised)
            {
                if (_weapon.CurrentAmmo > 0)
                {
                    DoHandAnimation("rest->raise weapon");
                }
                else
                {
                    DoHandAnimation("unloaded rest->raise weapon");
                }
            }
            else if (!b_weaponRaised)
                DoHandAnimation("raise weapon->rest");
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var cam = GameSystem.GameSys_Instance.GameCamera;

            _audioListener.Up = cam.UpAxis;
            _audioListener.Forward = cam.Direction;
            _audioListener.Velocity = this.GetVelocity();
            _audioListener.Position = this.Position;

            if (p_hands != null)
            {
                Matrix rot = Matrix.CreateRotationX(p_camera.Pitch) * Matrix.CreateRotationY(p_camera.Yaw);
                Vector3 localPos = Vector3.Transform(LocalTrans, LocalRot);
                Vector3 world = Vector3.Transform(LocalTrans, rot);
                world = Vector3.Transform(world, Matrix.CreateTranslation(p_camera.Position));

                p_hands.Position = world;
                p_hands.Rotation = Quaternion.CreateFromRotationMatrix(rot) * LocalRot;
            }
        }

        /// <summary>
        /// Fires the weapon.
        /// </summary>
        protected virtual void FireWeapon()
        {
            if (b_weaponAimed)
                DoHandAnimation("aim weapon->fire");
            else
                DoHandAnimation("raise weapon->fire");
        }
    }
}