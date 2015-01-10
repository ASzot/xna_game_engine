#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

namespace BaseLogic.Player.AI
{
    /// <summary>
    ///
    /// </summary>
    public abstract class AIState
    {
        /// <summary>
        /// The p_ai MGR
        /// </summary>
        protected AIMgr p_aiMgr;

        /// <summary>
        /// Sets the ai MGR.
        /// </summary>
        /// <value>
        /// The ai MGR.
        /// </value>
        public AIMgr AIMgr
        {
            set { p_aiMgr = value; }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public abstract AIState Clone();

        /// <summary>
        /// Enters the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public abstract void Enter(GameAIPlayer entity);

        /// <summary>
        /// Exits the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public abstract void Exit(GameAIPlayer entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="gameTime">The game time.</param>
        public abstract void Update(GameAIPlayer entity, Microsoft.Xna.Framework.GameTime gameTime);
    }
}