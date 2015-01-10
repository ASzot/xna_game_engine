#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RenderingSystem;

using GameBillboard = RenderingSystem.RendererImpl.DrawableBillboard;

namespace BaseLogic.Manager
{
    /// <summary>
    /// Manages all of the lights in the scene.
    /// </summary>
    public class LightManager : MgrTemplate<GameLight>
    {
        /// <summary>
        /// The directional light icon filename.
        /// </summary>
        private const string DIR_ICON_FILENAME = "Default/directionalLightIcon";

        /// <summary>
        /// The point light icon filename.
        /// </summary>
        private const string POINT_ICON_FILENAME = "Default/pointLightIcon";

        /// <summary>
        /// The spot light icon filename.
        /// </summary>
        private const string SPOT_ICON_FILENAME = "Default/spotLightIcon";

        /// <summary>
        /// The directional light icon texture.
        /// </summary>
        private Texture2D _dirIconTex;

        /// <summary>
        /// The point light icon texture.
        /// </summary>
        private Texture2D _pointIconTex;

        /// <summary>
        /// The spot light icon texture.
        /// </summary>
        private Texture2D _spotIconTex;

        /// <summary>
        /// Initializes a new instance of the <see cref="LightManager"/> class.
        /// </summary>
        public LightManager()
        {
            LoadTextures();
        }

        /// <summary>
        /// Gets the drawable billboards of the light icons.
        /// The light icons are used in debug mode to show where the lights are.
        /// </summary>
        /// <returns></returns>
        public List<RenderingSystem.RendererImpl.DrawableBillboard> GetDrawableBillboards()
        {
            List<GameBillboard> billboards = new List<GameBillboard>();

            foreach (GameLight light in _dataElements)
            {
                if (light is SpotLight)
                {
                    billboards.Add(new GameBillboard(_spotIconTex, (light as SpotLight).Position));
                }
                else if (light is DirLight)
                {
                    // Currently 
                    DirLight dirLight = light as DirLight;
                }
                else if (light is PointLight)
                {
                    billboards.Add(new GameBillboard(_pointIconTex, (light as PointLight).Position));
                }
            }

            return billboards;
        }

        /// <summary>
        /// Gets a light by the identifier.
        /// </summary>
        /// <param name="id">The identifier of the light to get.</param>
        /// <returns></returns>
        public GameLight GetLight(string id)
        {
            foreach (GameLight light in _dataElements)
            {
                if (id == light.LightID)
                    return light;
            }
            return null;
        }

        /// <summary>
        /// Index in the editor of the light.
        /// </summary>
        /// <param name="gameLight">The game light.</param>
        /// <returns></returns>
        public int IndexOfLight(GameLight gameLight)
        {
            for (int i = 0; i < _dataElements.Count(); ++i)
            {
                GameLight crntLight = _dataElements[i];
                if (crntLight.LightID == gameLight.LightID)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Checks if a light ID exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool LightIDExists(string id)
        {
            foreach (GameLight light in _dataElements)
            {
                if (light.LightID == id)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the data element.
        /// </summary>
        /// <param name="data">The data.</param>
        public override void RemoveDataElement(GameLight data)
        {
            DestroyAllProcsAssociatedWith(data);

            base.RemoveDataElement(data);
        }

        /// <summary>
        /// Removes the data element.
        /// </summary>
        /// <param name="i">The i.</param>
        public override void RemoveDataElement(int i)
        {
            DestroyAllProcsAssociatedWith(_dataElements[i]);

            base.RemoveDataElement(i);
        }

        /// <summary>
        /// Updates the lights.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void UpdateLights(GameTime gameTime)
        {
            if (!b_update)
                return;

            foreach (GameLight light in _dataElements)
            {
                light.Update(gameTime);
            }
        }

        /// <summary>
        /// Destroys all procs associated with.
        /// </summary>
        /// <param name="data">The data.</param>
        private void DestroyAllProcsAssociatedWith(GameLight data)
        {
            var procs = GameSystem.GameSys_Instance.ProcessMgr.GameProcesses;
            var lightModifiers = from proc in procs
                                 where proc is Process.LightModifier
                                 select proc as Process.LightModifier;

            foreach (var lightModifier in lightModifiers)
            {
                if (lightModifier.GetModifyingLight().LightID == data.LightID)
                    (lightModifier as Process.GameProcess).Kill = true;
            }
        }

        /// <summary>
        /// Loads the textures.
        /// </summary>
        private void LoadTextures()
        {
            _dirIconTex = ResourceMgr.LoadTexture(DIR_ICON_FILENAME);
            _pointIconTex = ResourceMgr.LoadTexture(POINT_ICON_FILENAME);
            _spotIconTex = ResourceMgr.LoadTexture(SPOT_ICON_FILENAME);
        }
    }
}