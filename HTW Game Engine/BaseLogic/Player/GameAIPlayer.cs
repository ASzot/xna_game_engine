#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using BaseLogic.Player.AI;

namespace BaseLogic.Player
{
    /// <summary>
    ///
    /// </summary>
    public class GameAIPlayer : GamePlayer
    {
        /// <summary>
        /// The _state machine
        /// </summary>
        protected StateMachine _stateMachine;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameAIPlayer"/> class.
        /// </summary>
        public GameAIPlayer()
            : this(Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameAIPlayer"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public GameAIPlayer(string id)
            : base(id)
        {
            _stateMachine = new StateMachine(this);
        }

        /// <summary>
        /// Changes the state.
        /// </summary>
        /// <param name="state">The state.</param>
        public void ChangeState(AIState state)
        {
            _stateMachine.ChangeState(state);
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            _stateMachine.Update(gameTime);
        }
    }
}