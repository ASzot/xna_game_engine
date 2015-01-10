#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using Microsoft.Xna.Framework;

namespace BaseLogic.Player.AI
{
    /// <summary>
    ///
    /// </summary>
    public class StateMachine
    {
        /// <summary>
        /// The _current state
        /// </summary>
        private AIState _currentState;

        /// <summary>
        /// The _global state
        /// </summary>
        private AIState _globalState;

        /// <summary>
        /// The _previous state
        /// </summary>
        private AIState _previousState;

        /// <summary>
        /// The p_owner
        /// </summary>
        private GameAIPlayer p_owner;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateMachine"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public StateMachine(GameAIPlayer owner)
        {
            p_owner = owner;
        }

        /// <summary>
        /// Gets or sets the state of the current.
        /// </summary>
        /// <value>
        /// The state of the current.
        /// </value>
        public AIState CurrentState
        {
            get { return _currentState; }
            set { _currentState = value; }
        }

        /// <summary>
        /// Gets or sets the state of the global.
        /// </summary>
        /// <value>
        /// The state of the global.
        /// </value>
        public AIState GlobalState
        {
            get { return _globalState; }
            set { _globalState = value; }
        }

        /// <summary>
        /// Gets or sets the state of the previous.
        /// </summary>
        /// <value>
        /// The state of the previous.
        /// </value>
        public AIState PreviousState
        {
            get { return _previousState; }
            set { _previousState = value; }
        }

        /// <summary>
        /// Changes the state.
        /// </summary>
        /// <param name="state">The state.</param>
        public void ChangeState(AIState state)
        {
            if (_currentState != null)
            {
                _previousState = _currentState.Clone();
                _currentState.Exit(p_owner);
            }

            _currentState = state;
            if (_currentState != null)
            {
                _currentState.AIMgr = (GameSystem.GameSys_Instance.AIMgr);
                _currentState.Enter(p_owner);
            }
        }

        /// <summary>
        /// Reverts the state of to previous.
        /// </summary>
        public void RevertToPreviousState()
        {
            ChangeState(_previousState.Clone());
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            if (_globalState != null)
                _globalState.Update(p_owner, gameTime);
            if (_currentState != null)
                _currentState.Update(p_owner, gameTime);
        }
    }
}