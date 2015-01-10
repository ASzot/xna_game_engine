#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using Microsoft.Xna.Framework;

namespace BaseLogic.Object.Holdable
{
    /// <summary>
    ///
    /// </summary>
    public class ProjectileWeaponObj : WeaponObj
    {
        /// <summary>
        /// The distance the weapon is in front.
        /// </summary>
        private float f_distanceInFront;

        /// <summary>
        /// The speed of the projectile.
        /// </summary>
        private float f_projectileSpeed;

        /// <summary>
        /// The p_projectile
        /// </summary>
        private StaticObj p_projectile;

        /// <summary>
        /// The q_projectile rot
        /// </summary>
        private Quaternion q_projectileRot;

        /// <summary>
        /// The ui_projectile remain time
        /// </summary>
        private uint ui_projectileRemainTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileWeaponObj"/> class.
        /// </summary>
        /// <param name="pHolder">The p holder.</param>
        /// <param name="projectile">The projectile.</param>
        /// <param name="speed">The speed.</param>
        /// <param name="projectileRemainTime">The projectile remain time.</param>
        /// <param name="distanceInFront">The distance in front.</param>
        /// <param name="appliedRot">The applied rot.</param>
        public ProjectileWeaponObj(Player.GamePlayer pHolder, StaticObj projectile,
            float speed, uint projectileRemainTime, float distanceInFront, Quaternion appliedRot)
            : base(pHolder)
        {
            q_projectileRot = appliedRot;
            p_projectile = projectile;
            f_projectileSpeed = speed;
            ui_projectileRemainTime = projectileRemainTime;
            f_distanceInFront = distanceInFront;
        }

        /// <summary>
        /// Shoots the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Shoot(GameTime gameTime)
        {
            base.Shoot(gameTime);

            RenderingSystem.ICamera cam = GameSystem.GameSys_Instance.GameCamera;

            Vector3 pos = cam.Position + cam.Direction * f_distanceInFront;

            Matrix camTransform = cam.Transform;
            Vector3 scale, translation;
            Quaternion rotation;
            camTransform.Decompose(out scale, out rotation, out translation);

            Quaternion rot = rotation * q_projectileRot;
            Vector3 vel = cam.Direction * f_projectileSpeed;

            Process.ProjectileProccess projectileProccess = new Process.ProjectileProccess(ui_projectileRemainTime, p_projectile,
                vel, pos, rot);
            GameSystem.GameSys_Instance.AddGameProcess(projectileProccess);
        }
    }
}