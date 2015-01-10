#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using BaseLogic;
using BaseLogic.Core;
using BaseLogic.Object;
using Microsoft.Xna.Framework;
using RenderingSystem;

namespace My_Xna_Game.Game_Objects
{
    /// <summary>
    /// A fun holdable object to show off the lighting of the graphics engine.
    /// </summary>
    internal class DancePartyBtn : HoldableObj
    {
        private List<GameLight> _lights = new List<GameLight>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DancePartyBtn"/> class.
        /// The light center will be the center of the room of the staticObj.
        /// This constructor can only be called when MapMgr is not null.
        /// </summary>
        /// <param name="staticObj">The static object.</param>
        /// <param name="game">The game.</param>
        public DancePartyBtn(StaticObj staticObj, XnaGame game)
            : base(staticObj, game.GameSystem.ObjMgr)
        {
            int roomIndex = game.MapMgr.GetRoomNumOf(staticObj) - 1;
            Vector3 roomPos = game.MapMgr.GetRoomPos(roomIndex);
            LoadContent(game.GameSystem, roomPos);
        }

        /// <summary>
        /// The light center will be the parameter specified.
        /// MapMgr is not used to determine any positions.
        /// </summary>
        /// <param name="staticObj"></param>
        /// <param name="game"></param>
        /// <param name="lightCenter"></param>
        public DancePartyBtn(StaticObj staticObj, XnaGame game, Vector3 lightCenter)
            : base(staticObj, game.GameSystem.ObjMgr)
        {
            LoadContent(game.GameSystem, lightCenter);
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <returns></returns>
        public override string GetIdentifier()
        {
            return "Dance party button";
        }

        /// <summary>
        /// Called when [interaction].
        /// </summary>
        public override void OnInteraction()
        {
            base.OnInteraction();

            ToggleLights();
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        /// <param name="centerPos">The center position.</param>
        private void LoadContent(GameSystem gameSystem, Vector3 centerPos)
        {
            int numberOfLights = 20;
            float lightStep = 0.5f;
            float radius = 1f;

            for (int i = 0; i < numberOfLights; ++i)
            {
                float randomStart = (float)(ThreadSafeRandom.ThisThreadsRandom.NextDouble() * Math.PI / 2.0);
                float randomSpeed = (float)ThreadSafeRandom.NextDouble(1, 3);
                Color randomColor = ColorHelper.GetRandomColor();

                RotatingLightPL rotatingLight = new RotatingLightPL(randomStart, radius, centerPos, randomSpeed, randomColor, 8f, 0.5f, 0.5f);
                rotatingLight.Enabled = false;
                rotatingLight.Serilize = false;
                _lights.Add(rotatingLight);
                gameSystem.LightMgr.AddToList(rotatingLight);

                radius += lightStep;
            }
        }

        /// <summary>
        /// Toggles the lights.
        /// </summary>
        private void ToggleLights()
        {
            foreach (GameLight light in _lights)
                light.Enabled = !light.Enabled;
        }
    }
}