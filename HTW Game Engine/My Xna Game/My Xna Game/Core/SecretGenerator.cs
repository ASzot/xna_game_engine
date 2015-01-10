#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;

using BaseLogic.Core;

namespace My_Xna_Game.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class SecretGenerator
    {
        private const int PitGeneralLocation = 4;
        private const int PitSpecificLocation = 5;
        private const int TeleporterGeneralLocation = 2;
        private const int TeleporterSpecificLocation = 3;
        private const int WumpusGeneralLocation = 0;
        private const int WumpusSpecificLocation = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretGenerator"/> class.
        /// </summary>
        public SecretGenerator()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private enum SecretType { WumpusLocationGeneral, WumpusLocationSpecific, TeleporterLocation, };

        /// <summary>
        /// Gets the secret MSG string.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public string GetSecretMsgStr(XnaGame game)
        {
            int randomNum = ThreadSafeRandom.ThisThreadsRandom.Next(0, 6);

            string msgStr = null;

            var userPlayer = game.GetGameUserPlayer();

            List<int> pitHazardRoomIndices;
            int randomIndex;
            int randomPitHazardIndex;
            Game_Objects.Orblet orblet;
            float distance;
            Microsoft.Xna.Framework.Vector3 randomRoomPos;
            Microsoft.Xna.Framework.Vector3 wumpusPos;
            int wumpusRoomIndex;

            switch (randomNum)
            {
                case WumpusGeneralLocation:
                    wumpusPos = game.WumpusPlayer.Position;
                    distance = userPlayer.DistanceTo(wumpusPos);

                    msgStr = String.Format("The wumpus is {0} feet away.", distance);
                    break;

                case WumpusSpecificLocation:
                    wumpusPos = game.WumpusPlayer.Position;
                    wumpusRoomIndex = game.MapMgr.GetRoomNumOf(wumpusPos);

                    msgStr = String.Format("The wumpus is in room {0}.", wumpusRoomIndex + 1);
                    break;

                case TeleporterGeneralLocation:
                    orblet = game.OrbletMgr.GetRandomTeleporterOrblet();
                    distance = userPlayer.DistanceTo(orblet);

                    msgStr = String.Format("A teleporter orblet is {0} feet away.", distance);
                    break;

                case TeleporterSpecificLocation:
                    orblet = game.OrbletMgr.GetRandomTeleporterOrblet();
                    int roomNum = game.MapMgr.GetRoomNumOf(orblet);

                    msgStr = String.Format("There is a teleporter orblet in room {0}.", roomNum);
                    break;

                case PitGeneralLocation:
                    pitHazardRoomIndices = game.MapMgr.GetPitHazardRoomIndices();
                    randomIndex = BaseLogic.Core.ThreadSafeRandom.NextInt(0, pitHazardRoomIndices.Count);
                    randomPitHazardIndex = pitHazardRoomIndices[randomIndex];
                    randomRoomPos = game.MapMgr.GetRoomPos(randomPitHazardIndex);
                    distance = userPlayer.DistanceTo(randomRoomPos);

                    msgStr = String.Format("There is a pit hazard {0} feet away.", distance);
                    break;

                case PitSpecificLocation:
                    pitHazardRoomIndices = game.MapMgr.GetPitHazardRoomIndices();
                    randomIndex = BaseLogic.Core.ThreadSafeRandom.NextInt(0, pitHazardRoomIndices.Count);
                    randomPitHazardIndex = pitHazardRoomIndices[randomIndex];

                    msgStr = String.Format("There is a pit hazard in room {0}.", randomPitHazardIndex + 1);
                    break;
            }

            return msgStr;
        }
    }
}