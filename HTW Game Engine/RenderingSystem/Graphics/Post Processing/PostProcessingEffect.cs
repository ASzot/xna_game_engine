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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem.Graphics
{
	/// <summary>
	/// The base for all post processing effects to inherit from.
	/// Defines base functionality for the post processing effect manager to use.
	/// </summary>
    public class PostProcessingEffect
    {
		/// <summary>
		/// The name of the post processing effect.
		/// Used for displaying in the post processing effect editor form.
		/// </summary>
        protected string s_name;
		/// <summary>
		/// A pointer to the post processing effect manager's sprite batch.
		/// </summary>
        protected SpriteBatch p_spriteBatch;

		/// <summary>
		/// Whether this post processing effect actaully gets drawn.
		/// </summary>
        protected bool b_enabled = true;
        public bool Enabled
        {
            get { return b_enabled; }
            set { b_enabled = value; }
        }

		/// <summary>
		/// The name of the effect.
		/// Used for displaying in the post processing effect editor form.
		/// </summary>
        public string EffectName
        {
            get { return s_name; }
        }

		/// <summary>
		/// Construct a post processing effect with the following name.
		/// </summary>
		/// <param name="name">The effect identifier.</param>
        public PostProcessingEffect(string name)
        {
            s_name = name;
        }

		/// <summary>
		/// Set the pointer to the sprite batch.
		/// This sprite batch should come from post processing effect manager.
		/// </summary>
		/// <param name="pSpriteBatch"></param>
        public void SetSpriteBatch(SpriteBatch pSpriteBatch)
        {
            p_spriteBatch = pSpriteBatch;
        }

		/// <summary>
		/// Draw a quad to the screen.
		/// Handles the format of the texture relative to the render target appropriately.
		/// </summary>
		/// <param name="texture">Texture to render.</param>
		/// <param name="width">The height to render to.</param>
		/// <param name="height">The width to render to.</param>
		/// <param name="effect">The effect to render with.</param>
        protected void DrawQuad(Texture2D texture, int width, int height, Effect effect)
        {
            if (texture.Format == SurfaceFormat.Single || texture.Format == SurfaceFormat.HdrBlendable)
            {
                p_spriteBatch.Begin(0, BlendState.Opaque, SamplerState.PointClamp, null, null, effect);
                p_spriteBatch.Draw(texture, new Rectangle(0, 0, width, height), Color.White);
                p_spriteBatch.End();
            }
            else
            {
                p_spriteBatch.Begin(0, BlendState.Opaque, null, null, null, effect);
                p_spriteBatch.Draw(texture, new Rectangle(0, 0, width, height), Color.White);
                p_spriteBatch.End();
            }
            
        }

		/// <summary>
		/// Draw a fullscreen quad to the specified render target.
		/// The texture is drawn to the full dimensions of the render target.
		/// </summary>
		/// <param name="texture">Texture to draw.</param>
		/// <param name="renderTarget">Render target to draw to.</param>
		/// <param name="effect">Effect to draw with.</param>
		/// <param name="device">Graphics device used to set the render target.</param>
        protected void DrawQuad(Texture2D texture, RenderTarget2D renderTarget, Effect effect, GraphicsDevice device)
        {
            device.SetRenderTarget(renderTarget);

            DrawQuad(texture, renderTarget.Width, renderTarget.Height, effect);
        }

		/// <summary>
		/// Draw a fullscreen quad using a specified sampler state.
		/// The render target is set using the device.
		/// The texture is drawn to the full dimensions of the render target.
		/// </summary>
		/// <param name="texture">The texture to draw.</param>
		/// <param name="renderTarget">The render target to draw to.</param>
		/// <param name="effect">The effect to draw with.</param>
		/// <param name="samplerState">The sampler state to use.</param>
		/// <param name="device"></param>
        protected void DrawQuad(Texture2D texture, RenderTarget2D renderTarget, Effect effect, SamplerState samplerState, GraphicsDevice device)
        {
            device.SetRenderTarget(renderTarget);

            int width = texture.Width;
            int height = texture.Height;

            p_spriteBatch.Begin(0, BlendState.Opaque, samplerState, null, null, effect);
            p_spriteBatch.Draw(texture, new Rectangle(0, 0, width, height), Color.White);
            p_spriteBatch.End();
        }

		/// <summary>
		/// Create the editor form for this effect.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="e"></param>
        public virtual void CreateSettingsDlg(object obj, EventArgs e)
        {

        }

		/// <summary>
		/// Get the save data for this effect.
		/// A type of the effect's save data structure will be returned.
		/// </summary>
		/// <returns></returns>
        public virtual object GetSettingsSaveData()
        {
            return null;
        }

		/// <summary>
		/// Set the save data for this effect.
		/// A type of the effect's save data structure must be used.
		/// </summary>
		/// <param name="saveData"></param>
        public virtual void SetSettingsSaveData(object saveData)
        {

        }

		/// <summary>
		/// Load all resources.
		/// </summary>
		/// <param name="device">The graphics device.</param>
		/// <param name="content">The content manager.</param>
        public virtual void LoadContent(GraphicsDevice device, ContentManager content)
        {

        }

		/// <summary>
		/// Unload all of the resources which are pointers to unmanaged memory.
		/// </summary>
        public virtual void UnloadContent()
        {

        }

		/// <summary>
		/// Set the per frame data of this effect save data.
		/// </summary>
		/// <param name="frameLights">The lights for this frame.</param>
		/// <param name="cam">The camera for the scene.</param>
        public virtual void SetFrameData(List<RenderingSystem.RendererImpl.GeneralLight> frameLights, ICamera cam)
        {

        }

		/// <summary>
		/// Called in the geometry rendering phase of the GBuffer rendering.
		/// </summary>
		/// <param name="view">The view matrix of the scene camera.</param>
		/// <param name="proj">The projection matrix of the scene camera.</param>
		/// <param name="device">The graphics device.</param>
        public virtual void DrawGeometryPhase(Matrix view, Matrix proj, GraphicsDevice device)
        {

        }

		/// <summary>
		/// Draw the effect using the input final scene render target.
		/// </summary>
		/// <param name="device">The graphics device.</param>
		/// <param name="renderTarget">The final scene render target.</param>
		/// <param name="renderer">The scene renderer.</param>
		/// <returns></returns>
        public virtual RenderTarget2D DrawEffect(GraphicsDevice device, RenderTarget2D renderTarget, RenderingSystem.RendererImpl.Renderer renderer)
        {
            return null;
        }
    }
}
