#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using Microsoft.Xna.Framework;

namespace BaseLogic.Object
{
    /// <summary>
    ///
    /// </summary>
    public class WeaponObj
    {
        /// <summary>
        /// The automatic
        /// </summary>
        public bool Automatic = true;

        /// <summary>
        /// The f_damage
        /// </summary>
        protected float f_damage = 1;

        /// <summary>
        /// The i_clip size
        /// </summary>
        protected int i_clipSize = 10;

        /// <summary>
        /// The i_current ammo
        /// </summary>
        protected int i_currentAmmo = 10;

        /// <summary>
        /// The i_total ammo
        /// </summary>
        protected int i_totalAmmo = 100;

        /// <summary>
        /// The p_holder
        /// </summary>
        protected Player.GamePlayer p_holder;

        /// <summary>
        /// The v_fire offset
        /// </summary>
        protected Vector3 v_fireOffset = Vector3.Zero;

        /// <summary>
        /// The _fire rate
        /// </summary>
        private TimeSpan _fireRate = TimeSpan.FromMilliseconds(100);

        /// <summary>
        /// The _last fire time
        /// </summary>
        private TimeSpan _lastFireTime = TimeSpan.Zero;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeaponObj"/> class.
        /// </summary>
        /// <param name="pHolder">The p holder.</param>
        public WeaponObj(Player.GamePlayer pHolder)
            : base()
        {
            p_holder = pHolder;
        }

        /// <summary>
        /// Gets or sets the size of the clip.
        /// </summary>
        /// <value>
        /// The size of the clip.
        /// </value>
        /// <exception cref="System.ArgumentException">Bullets in the clip are greater than the clip size.</exception>
        public int ClipSize
        {
            get { return i_clipSize; }
            set
            {
                if (value < i_currentAmmo)
                    throw new ArgumentException("Bullets in the clip are greater than the clip size.");
                i_clipSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the current ammo.
        /// </summary>
        /// <value>
        /// The current ammo.
        /// </value>
        /// <exception cref="System.ArgumentException">Bullets in the clip is greater than the clip size.</exception>
        public int CurrentAmmo
        {
            get { return i_currentAmmo; }
            set
            {
                if (value > i_clipSize)
                    throw new ArgumentException("Bullets in the clip is greater than the clip size.");
                i_currentAmmo = value;
            }
        }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        /// <value>
        /// The damage.
        /// </value>
        public float Damage
        {
            get { return f_damage; }
            set { f_damage = value; }
        }

        /// <summary>
        /// Gets or sets the fire offset.
        /// </summary>
        /// <value>
        /// The fire offset.
        /// </value>
        public Vector3 FireOffset
        {
            get { return v_fireOffset; }
            set { v_fireOffset = value; }
        }

        /// <summary>
        /// Gets or sets the fire rate.
        /// </summary>
        /// <value>
        /// The fire rate.
        /// </value>
        public TimeSpan FireRate
        {
            get { return _fireRate; }
            set { _fireRate = value; }
        }

        /// <summary>
        /// Gets or sets the total ammo.
        /// </summary>
        /// <value>
        /// The total ammo.
        /// </value>
        public int TotalAmmo
        {
            get { return i_totalAmmo; }
            set { i_totalAmmo = value; }
        }

        /// <summary>
        /// Determines whether this instance can fire the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <returns></returns>
        public bool CanFire(GameTime gameTime)
        {
            if (i_currentAmmo <= 0)
                return false;

            if (gameTime.TotalGameTime - _lastFireTime > _fireRate)
                return true;
            return false;
        }

        /// <summary>
        /// Inflicts the damage.
        /// </summary>
        /// <param name="player">The player.</param>
        public void InflictDamage(Player.GamePlayer player)
        {
            player.Health -= f_damage;
        }

        /// <summary>
        /// Needses the reload.
        /// </summary>
        /// <returns></returns>
        public bool NeedsReload()
        {
            if (i_totalAmmo <= 0)
                return false;
            return i_currentAmmo == 0;
        }

        /// <summary>
        /// Reloads this instance.
        /// </summary>
        public void Reload()
        {
            if (i_totalAmmo < i_clipSize)
            {
                i_currentAmmo = i_totalAmmo;
                i_totalAmmo = 0;
            }
            else
            {
                i_currentAmmo = i_clipSize;
                i_totalAmmo = i_totalAmmo - i_clipSize;
            }
        }

        /// <summary>
        /// Shoots the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void Shoot(GameTime gameTime)
        {
            i_currentAmmo--;

            _lastFireTime = gameTime.TotalGameTime;
        }

        //public void Shoot(GameTime gameTime, Vector3 shotPos, Quaternion gunRot)
        //{
        //    Shoot(gameTime);

        //    GameSystem.GameSys.ProcessMgr.AddProcess(new Process.ParticleEffectProcess(_fireTime, Guid.NewGuid().ToString(), shotPos, gunRot));
        //}
    }
}