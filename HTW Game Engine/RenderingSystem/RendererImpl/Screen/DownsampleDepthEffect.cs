#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem.RendererImpl
{
    /// <summary>
    /// Downsample a texture to a lower resolution used to bring down the resolution of the scene render.
    /// Useful for all the effects which need to do passes on the final scene render.
    /// </summary>
    internal class DownsampleDepthEffect
    {
        public EffectParameter DepthBuffer;
        public EffectParameter HalfPixel;
        public EffectParameter PixelSize;
        protected ScreenQuadRenderer _quadRenderer;
        private Effect _effect;

        /// <summary>
        /// Construct the downsample depth effect creating a screen quad render.
        /// </summary>
        public DownsampleDepthEffect()
        {
            _quadRenderer = new ScreenQuadRenderer();
        }

        /// <summary>
        /// Load resource related content.
        /// Load effect and get fx variables.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="renderer"></param>
        public void LoadContent(ContentManager content, Renderer renderer)
        {
            try
            {
                _effect = content.Load<Effect>("shaders/DownsampleDepth");
                LoadParameters();
                Vector2 pixelSize = new Vector2(1.0f / (float)renderer.DepthBuffer.Width, 1.0f / (float)renderer.DepthBuffer.Height);
                PixelSize.SetValue(pixelSize);
                HalfPixel.SetValue(pixelSize * 0.5f);
                DepthBuffer.SetValue(renderer.DepthBuffer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading downsample depth efect: " + ex.ToString());
            }
        }

        /// <summary>
        /// Get fx parameters.
        /// </summary>
        public void LoadParameters()
        {
            PixelSize = _effect.Parameters["PixelSize"];
            DepthBuffer = _effect.Parameters["DepthBuffer"];
            HalfPixel = _effect.Parameters["HalfPixel"];
        }

        /// <summary>
        /// Render the effect, effectively downsizing the render target.
        /// Then render this downsized render target to the back buffer.
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="device"></param>
        public void RenderEffect(Renderer renderer, GraphicsDevice device)
        {
            RenderTarget2D half0 = renderer.HalfDepth;
            //render to a half-res buffer
            device.SetRenderTarget(half0);

            Apply();

            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.None;
            _quadRenderer.RenderQuad(device, -Vector2.One, Vector2.One);
        }

        /// <summary>
        /// Apply the fx variable changes.
        /// </summary>
        private void Apply()
        {
            _effect.CurrentTechnique.Passes[0].Apply();
        }
    }
}