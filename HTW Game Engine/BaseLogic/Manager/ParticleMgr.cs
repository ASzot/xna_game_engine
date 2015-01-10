#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using Microsoft.Xna.Framework;
using RenderingSystem;

namespace BaseLogic.Manager
{
    /// <summary>
    /// Manages all of the particle systems in the scene.
    /// </summary>
    public class ParticleMgr : MgrTemplate<GameParticleSystem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleMgr"/> class.
        /// </summary>
        public ParticleMgr()
        {
        }

        /// <summary>
        /// Actors the identifier exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool ActorIDExists(string id)
        {
            foreach (GameActor gameObj in _dataElements)
            {
                if (gameObj.ActorID == id)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the particle system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public GameParticleSystem GetParticleSystem(string id)
        {
            foreach (var particleSystem in _dataElements)
            {
                if (particleSystem.ActorID == id)
                    return particleSystem;
            }
            return null;
        }

        /// <summary>
        /// Updates the systems.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void UpdateSystems(GameTime gameTime)
        {
            if (!b_update)
                return;

            foreach (GameParticleSystem psys in _dataElements)
            {
                psys.Update(gameTime);
            }
        }
    }
}