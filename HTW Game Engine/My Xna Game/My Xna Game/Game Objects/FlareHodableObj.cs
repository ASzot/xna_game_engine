#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using BaseLogic;
using BaseLogic.Object;
using Microsoft.Xna.Framework;
using RenderingSystem;

namespace My_Xna_Game.Game_Objects
{
    /// <summary>
    /// 
    /// </summary>
    internal class FlareHodableObj : HoldableObj
    {
        private PointLight p_flareLight;
        private GameParticleSystem p_flareParticleSys;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlareHodableObj"/> class.
        /// </summary>
        /// <param name="staticObj">The static object.</param>
        /// <param name="gameSystem">The game system.</param>
        public FlareHodableObj(StaticObj staticObj, GameSystem gameSystem)
            : base(staticObj, gameSystem.ObjMgr)
        {
            LoadContent(gameSystem);
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <returns></returns>
        public override string GetIdentifier()
        {
            return "Flare";
        }

        /// <summary>
        /// Called when [interaction].
        /// </summary>
        public override void OnInteraction()
        {
            base.OnInteraction();

            ToggleFlare();
        }

        /// <summary>
        /// Toggles the flare.
        /// </summary>
        public void ToggleFlare()
        {
            p_flareParticleSys.Enabled = !p_flareParticleSys.Enabled;
            p_flareLight.Enabled = !p_flareLight.Enabled;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector3 particleSysOffset = new Vector3(0f, 0.4f, 0f);

            p_flareParticleSys.Rotation = this.Rotation;

            Vector3 particleSysWorldPos = Vector3.Transform(particleSysOffset, Rotation);
            particleSysWorldPos += Position;

            p_flareParticleSys.Position = particleSysWorldPos;

            Vector3 lightOffset = new Vector3(0f, 0.5f, 0.0f);
            p_flareLight.Position = Vector3.Transform(lightOffset, Rotation) + Position;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        private void LoadContent(GameSystem gameSystem)
        {
            p_flareParticleSys = gameSystem.CreateParticleSystem("FlareSettings", "flare psys " + Guid.NewGuid().ToString());
            p_flareParticleSys.Enabled = false;

            p_flareLight = new PointLight(Vector3.Zero, 10f, 3f, 3f, Color.Red);
            gameSystem.LightMgr.AddToList(p_flareLight);
            p_flareLight.Enabled = false;
            p_flareLight.Serilize = false;
        }
    }
}