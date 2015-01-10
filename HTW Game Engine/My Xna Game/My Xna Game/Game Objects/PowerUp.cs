#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using BaseLogic;

using Microsoft.Xna.Framework;

namespace My_Xna_Game.Game_Objects
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class PowerUp : StaticObj
    {
        protected float f_bounceAmplitude = 1f;
        protected float f_midLine = 3f;
        protected float f_radiusSq = 25f;
        protected float f_rotSpeed = 2f;

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerUp"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public PowerUp(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Gets or sets the bounce amplitude.
        /// </summary>
        /// <value>
        /// The bounce amplitude.
        /// </value>
        public float BounceAmplitude
        {
            get { return f_bounceAmplitude; }
            set { f_bounceAmplitude = value; }
        }

        /// <summary>
        /// Gets or sets the mid line.
        /// </summary>
        /// <value>
        /// The mid line.
        /// </value>
        public float MidLine
        {
            get { return f_midLine; }
            set { f_midLine = value; }
        }

        /// <summary>
        /// Gets the load filename.
        /// </summary>
        /// <returns></returns>
        public abstract string GetLoadFilename();

        /// <summary>
        /// Determines whether [is player in range] [the specified user player].
        /// </summary>
        /// <param name="userPlayer">The user player.</param>
        /// <returns></returns>
        public virtual bool IsPlayerInRange(GameUserPlayer userPlayer)
        {
            float distanceToPlayer = userPlayer.DistanceSqTo(this);

            return distanceToPlayer < f_radiusSq;
        }

        /// <summary>
        /// Called when [player interact].
        /// </summary>
        /// <param name="userPlayer">The user player.</param>
        public virtual void OnPlayerInteract(GameUserPlayer userPlayer)
        {
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            float t = (float)gameTime.TotalGameTime.TotalSeconds;
            float r = f_rotSpeed * t;

            Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, r);

            float y = (f_bounceAmplitude * (float)Math.Sin(t)) + f_midLine;
            Position = new Vector3(Position.X, y, Position.Z);
        }
    }

    /// <summary>
    /// Picked up by the user some of the health is restored.
    /// </summary>
    internal class HealthPowerUp : PowerUp
    {
        private const float HEALTH_INC_AMOUNT = 5f;
        private const string LOAD_FILENAME = "cube";

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthPowerUp"/> class.
        /// </summary>
        public HealthPowerUp()
            : base("Health Power Up: " + Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// Gets the load filename for the model to use as the representation for this object.
        /// </summary>
        /// <returns></returns>
        public override string GetLoadFilename()
        {
            return LOAD_FILENAME;
        }

        /// <summary>
        /// Determines whether [is player in range] [the specified user player].
        /// </summary>
        /// <param name="userPlayer">The user player.</param>
        /// <returns></returns>
        public override bool IsPlayerInRange(GameUserPlayer userPlayer)
        {
            bool playerHasFullHealth = userPlayer.Health == userPlayer.MaxHealth;
            return (base.IsPlayerInRange(userPlayer) && !playerHasFullHealth);
        }

        /// <summary>
        /// Called when [player interact].
        /// </summary>
        /// <param name="userPlayer">The user player.</param>
        public override void OnPlayerInteract(GameUserPlayer userPlayer)
        {
            userPlayer.Health = Math.Min(userPlayer.Health + HEALTH_INC_AMOUNT, userPlayer.MaxHealth);
        }
    }
}