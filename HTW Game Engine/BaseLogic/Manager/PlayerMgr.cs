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
using BaseLogic.Player;
using Microsoft.Xna.Framework;
using RenderingSystem;

namespace BaseLogic.Manager
{
    /// <summary>
    /// Manages all of the players in the scene.
    /// </summary>
    public class PlayerMgr : MgrTemplate<GamePlayer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerMgr"/> class.
        /// </summary>
        public PlayerMgr()
        {
            GamePlayer.p_PlayerMgr = this;
        }

        /// <summary>
        /// Gets the game player for a game physics body.
        /// </summary>
        /// <param name="gameRigidBody">The game rigid body.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public GamePlayer GetGamePlayerForGameRB(GameRigidBody gameRigidBody)
        {
            string id = gameRigidBody.Name;
            IEnumerable<GamePlayer> results = from p in _dataElements
                                              where p.GameObjID == id
                                              select p;
            if (results.Count() != 0)
            {
                if (results.Count() > 1)
                    throw new ArgumentException();
                GamePlayer gamePlayer = results.ElementAt(0);
                return gamePlayer;
            }
            return null;
        }

        /// <summary>
        /// Gets the player based on the player's identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public GamePlayer GetPlayerOfId(string id)
        {
            foreach (GamePlayer gamePlayer in _dataElements)
            {
                if (gamePlayer.ActorID == id)
                    return gamePlayer;
            }
            return null;
        }

        /// <summary>
        /// Kills all the players in the scene.
        /// </summary>
        public void KillAll()
        {
            foreach (GamePlayer gamePlayer in _dataElements)
            {
                gamePlayer.Kill = true;
            }
        }

        /// <summary>
        /// Updates the players.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            if (!b_update)
                return;

            for (int i = 0; i < _dataElements.Count; ++i)
            {
                GamePlayer gamePlayer = _dataElements.ElementAt(i);
                if (gamePlayer.Kill)
                {
                    // Terminate the existence of this object.
                    RemoveDataElement(i);
                    // So we don't skip one of the elements in the array.
                    --i;
                    continue;
                }

                gamePlayer.Update(gameTime);
            }

            var enemyPlayers = from de in _dataElements
                               where de is EnemyAIPlayer
                               select de;
            foreach (EnemyAIPlayer enemy in enemyPlayers)
                enemy.UpdateAIMemory(gameTime);
        }
    }
}