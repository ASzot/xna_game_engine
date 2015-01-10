#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using Microsoft.Xna.Framework;

namespace My_Xna_Game.Game_Objects
{
    public class PowerUpMgr : RenderingSystem.MgrTemplate<PowerUp>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PowerUpMgr"/> class.
        /// </summary>
        public PowerUpMgr()
        {
        }

        /// <summary>
        /// Places the power up.
        /// </summary>
        /// <param name="roomIndex">Index of the room.</param>
        /// <param name="gameSystem">The game system.</param>
        /// <param name="mapMgr">The map MGR.</param>
        /// <param name="powerUp">The power up.</param>
        public void PlacePowerUp(int roomIndex, BaseLogic.GameSystem gameSystem, Map.MapMgr mapMgr, PowerUp powerUp)
        {
            powerUp = (PowerUp)gameSystem.CreateStaticObj(powerUp, powerUp.GetLoadFilename());
            powerUp.Scale = 0.3f;
            powerUp.SubsetMaterials[0].DiffuseMap = new RenderingSystem.GameTexture("images/DiffuseMaps/HealthBoxDiffuseAlpha");
            powerUp.SubsetMaterials[0].UseAlphaMask = true;
            powerUp.SetMass(float.PositiveInfinity);
            Vector3 placementPos = mapMgr.GetRoomPos(roomIndex);
            placementPos.Y += 3f;
            powerUp.Position = placementPos;
            powerUp.SerilizeObj = false;
            powerUp.MidLine = placementPos.Y;

            AddToList(powerUp);
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="gameUserPlayer">The game user player.</param>
        public void Update(GameTime gameTime, GameUserPlayer gameUserPlayer)
        {
            if (gameUserPlayer == null)
                return;

            foreach (PowerUp powerUp in _dataElements)
            {
                if (powerUp.IsPlayerInRange(gameUserPlayer))
                {
                    powerUp.OnPlayerInteract(gameUserPlayer);
                    powerUp.Kill = true;
                }
            }

            for (int i = 0; i < _dataElements.Count; ++i)
            {
                if (_dataElements[i].Kill)
                    _dataElements.RemoveAt(i--);
            }
        }
    }
}