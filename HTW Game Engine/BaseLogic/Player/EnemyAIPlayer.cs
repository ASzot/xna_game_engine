#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using Microsoft.Xna.Framework;

namespace BaseLogic.Player
{
    /// <summary>
    ///
    /// </summary>
    public class EnemyAIPlayer : GameAIPlayer
    {
        /// <summary>
        /// The _memory
        /// </summary>
        protected AIMemory _memory = new AIMemory();

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyAIPlayer"/> class.
        /// </summary>
        /// <param name="startingState">State of the starting.</param>
        /// <param name="gamePlayer">The game player.</param>
        public EnemyAIPlayer(AI.AIState startingState, PhysObj gamePlayer)
            : this(Guid.NewGuid().ToString(), startingState, gamePlayer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyAIPlayer"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="startingState">State of the starting.</param>
        /// <param name="gamePlayer">The game player.</param>
        public EnemyAIPlayer(string id, AI.AIState startingState, PhysObj gamePlayer)
            : base(id)
        {
            this.SetGameObjPtr(gamePlayer);
            _stateMachine.ChangeState(startingState);
        }

        /// <summary>
        /// Gets the memory.
        /// </summary>
        /// <value>
        /// The memory.
        /// </value>
        public AIMemory Memory
        {
            get { return _memory; }
        }

        /// <summary>
        /// Recieves the MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="gameTime">The game time.</param>
        public override void RecieveMsg(PlayerMessage msg, GameTime gameTime)
        {
            base.RecieveMsg(msg, gameTime);

            switch (msg.Type)
            {
                case PlayerMessageType.InflictDamage:
                    Object.WeaponObj weapon = msg.Sender.WeaponObj;
                    _memory.RememberDamageTaken(weapon.Damage);
                    break;
            }
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Updates the ai memory.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void UpdateAIMemory(GameTime gameTime)
        {
            _memory.Update(gameTime);
        }
    }
}