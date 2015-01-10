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
	/// The manager for all of the post processing effects.
	/// Draws them all appropriately.
	/// </summary>
    public class PPEMgr : MgrTemplate<PostProcessingEffect>
    {
		/// <summary>
		/// A pointer to the scene renderer's sprite batch.
		/// The 2D sprite batch renderer used by all of the post processing effect.
		/// </summary>
        private SpriteBatch p_spriteBatch = null;
		/// <summary>
		/// The Fast Approximate Anti-Alaising effect which is used seperately from the rest of the post processing effects.
		/// </summary>
        private FxaaPPE _fxaa;

		/// <summary>
		/// Whether the anti alaising is enabled.
		/// This should generally be enabled.
		/// </summary>
        public bool AntiAliasingEnabled
        {
            get { return _fxaa.Enabled; }
        }

		/// <summary>
		/// The Fast Approximate Anti-Alaising effect which is used seperately from the rest of the post processing effects.
		/// </summary>
        public FxaaPPE AntiAliasing
        {
            get { return _fxaa; }
        }

		/// <summary>
		/// Constructs the post processing effect manager loading the FXAA.
		/// </summary>
		/// <param name="pSpriteBatch">A pointer to the renderer's sprite batch.</param>
		/// <param name="device">The graphics device.</param>
		/// <param name="content">The content manager.</param>
        public PPEMgr(SpriteBatch pSpriteBatch, GraphicsDevice device, ContentManager content)
        {
            p_spriteBatch = pSpriteBatch;

            _fxaa = new FxaaPPE();
            _fxaa.LoadContent(device, content);
            _fxaa.SetSpriteBatch(p_spriteBatch);
        }

		/// <summary>
		/// Creat the post processing effect manager editor form.
		/// Allows the user to modify the order and data of the post processing effects along with adding and deleting them.
		/// </summary>
		/// <param name="onDlgQuit"></param>
        public void CreatePostProcessMgrDlg(Action onDlgQuit)
        {
            Forms.PPEMgrForm ppeMgrForm = new Forms.PPEMgrForm(this);
            ppeMgrForm.OnDlgQuit += onDlgQuit;
            ppeMgrForm.ShowDialog();
        }

		/// <summary>
		/// Gets the post processing effect given the identifier of the post processing effect.
		/// </summary>
		/// <param name="name">Identifier of the post processing effect.</param>
		/// <returns>The found post processing effect.</returns>
        public PostProcessingEffect GetPostProcessingEffect(string name)
        {
            foreach (PostProcessingEffect ppe in _dataElements)
            {
                if (ppe.EffectName == name)
                    return ppe;
            }

            return null;
        }

		/// <summary>
		/// Add a new post processing effect.
		/// </summary>
		/// <param name="data"></param>
        public override void AddToList(PostProcessingEffect data)
        {
            data.SetSpriteBatch(p_spriteBatch);

            base.AddToList(data);
        }

		/// <summary>
		/// Draw the geometry phase of all the post processing effects.
		/// </summary>
		/// <param name="device"></param>
		/// <param name="view"></param>
		/// <param name="proj"></param>
        public void DrawGeometryPhase(GraphicsDevice device, Matrix view, Matrix proj)
        {
            foreach (PostProcessingEffect ppe in _dataElements)
                ppe.DrawGeometryPhase(view, proj, device);
        }

		/// <summary>
		/// Draw the anti-alaising. The render target is drawn over.
		/// </summary>
		/// <param name="inputRT"></param>
		/// <param name="device"></param>
		/// <param name="renderer"></param>
        private void DrawAA(RenderTarget2D inputRT, GraphicsDevice device, RenderingSystem.RendererImpl.Renderer renderer)
        {
            if (_fxaa.Enabled)
                _fxaa.DrawEffect(device, inputRT, renderer);
        }

		/// <summary>
		/// Set the per frame scene data.
		/// This will be the lighting data.
		/// </summary>
		/// <param name="lights">The list of this frame's lights.</param>
		/// <param name="cam">The scene camera.</param>
        public void SetSceneInfo(List<RendererImpl.GeneralLight> lights, ICamera cam)
        {
            foreach (PostProcessingEffect ppe in _dataElements)
                ppe.SetFrameData(lights, cam);
        }

        public RenderTarget2D DrawEffects(GraphicsDevice device, RenderTarget2D renderTarget, 
            RenderingSystem.RendererImpl.Renderer renderer)
        {
            foreach (PostProcessingEffect ppe in _dataElements)
            {
                renderTarget = ppe.DrawEffect(device, renderTarget, renderer);
            }

            DrawAA(renderTarget, device, renderer);

            return renderTarget;
        }
    }
}
