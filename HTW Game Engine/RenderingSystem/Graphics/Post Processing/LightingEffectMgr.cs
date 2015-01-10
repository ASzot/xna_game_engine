#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem.Graphics
{
	/// <summary>
	/// Manages the lighting effects which are primarily flares.
	/// This is the post processing effect which is used in the post processing effect manager not the lens flare itself.
	/// </summary>
    class LightingEffectMgr : PostProcessingEffect
    {
		/// <summary>
		/// The scene supports a maximum number of flare effects at once.
		/// </summary>
        private const int MAX_FLARE_EFFECTS = 10;

		/// <summary>
		/// The number of lights which are using the flare effect the last update frame.
		/// </summary>
        private int i_frameLightCount;
		/// <summary>
		/// The list containing all lens flare effects for the scene. These are kept in sync with the lights using the lens flare effect.
		/// </summary>
        private List<LensFlareEffect> _lensFlareEffects = new List<LensFlareEffect>();

        public LightingEffectMgr()
            : base("Lighting Effects")
        {
            
        }
		 
		/// <summary>
		/// Load all resources which require the grahics device.
		/// </summary>
		/// <param name="device">The graphics device.</param>
		/// <param name="content">The content manager.</param>
        public override void LoadContent(GraphicsDevice device, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            for (int i = 0; i < MAX_FLARE_EFFECTS; ++i)
            {
                LensFlareEffect lensFlareEffect = new LensFlareEffect();
                lensFlareEffect.LoadContent(device);
                _lensFlareEffects.Add(lensFlareEffect);
            }
        }

		/// <summary>
		/// Update the flare effects based on this frames light batch.
		/// </summary>
		/// <param name="frameLights">The list of lights for this frame.</param>
		/// <param name="cam">The camera for this scene.</param>
        public override void SetFrameData(List<RendererImpl.GeneralLight> frameLights, ICamera cam)
        {
            IEnumerable<RenderingSystem.RendererImpl.GeneralLight> flareLights = from fl in frameLights
                                                                          where fl.FlareEnabled
                                                                          select fl;

            i_frameLightCount = Math.Min(MAX_FLARE_EFFECTS, flareLights.Count());

            for (int i = 0; i < i_frameLightCount; ++i)
            {
                RenderingSystem.RendererImpl.GeneralLight light = flareLights.ElementAt(i);
                LensFlareEffect lfe = _lensFlareEffects.ElementAt(i);
                if (light.LightType == RendererImpl.GeneralLight.Type.Directional)
                {
                    lfe.LightPos = Vector3.TransformNormal(Vector3.UnitZ, light.Transform);
                    lfe.InfiniteViewPlane = true;
                }
                else
                {
                    lfe.LightPos = light.Transform.Translation;
                    lfe.InfiniteViewPlane = false;
                }
                
                
                lfe.GlowSize = light.GlowSize;
                lfe.QuerySize = light.QuerySize;

            }
        }


		/// <summary>
		/// Draw the geometry phase for this effect.
		/// THIS IS NECISSARY.
		/// </summary>
		/// <param name="view">The view for the camera.</param>
		/// <param name="proj">The projection for the camera.</param>
		/// <param name="device">The graphics device.</param>
        public override void DrawGeometryPhase(Matrix view, Matrix proj, GraphicsDevice device)
        {
            for (int i = 0; i < i_frameLightCount; ++i)
            {
                LensFlareEffect lfe = _lensFlareEffects.ElementAt(i);
                lfe.UpdateOcclusion(view, proj, device);
            }
        }

		/// <summary>
		/// Draw the effect using the input render target which should be the "back buffer" render target.
		/// </summary>
		/// <param name="device">The graphics device.</param>
		/// <param name="renderTarget">The final scene render target.</param>
		/// <param name="renderer">The scene renderer.</param>
		/// <returns></returns>
        public override RenderTarget2D DrawEffect(GraphicsDevice device, RenderTarget2D renderTarget, RenderingSystem.RendererImpl.Renderer renderer)
        {
            device.SetRenderTarget(renderTarget);
            for (int i = 0; i < i_frameLightCount; ++i)
            {
                LensFlareEffect lfe = _lensFlareEffects.ElementAt(i);
                lfe.Draw(device, p_spriteBatch);
            }

            return renderTarget;
        }
    }
}
