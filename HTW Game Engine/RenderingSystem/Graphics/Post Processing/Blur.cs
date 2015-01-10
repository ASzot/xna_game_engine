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
	/// A gausian blur post processing effect.
	/// </summary>
    class BlurPPE : PostProcessingEffect
    {
		/// <summary>
		/// A effect resource for the gausian blur fx file.
		/// </summary>
        private Effect _gaussianBlurEffect;
		/// <summary>
		/// A intermediate render target.
		/// </summary>
        private RenderTarget2D _tempRT;
		/// <summary>
		/// The intensity of the blur.
		/// </summary>
        private float f_blurAmount = 2f;

		/// <summary>
		/// The intensity of the blur.
		/// </summary>
        public float BlurAmount
        {
            get { return f_blurAmount; }
            set { f_blurAmount = value; }
        }

		/// <summary>
		/// Construct the blur post processing effect.
		/// </summary>
        public BlurPPE()
            : base("blur")
        {

        }

		/// <summary>
		/// Load the resource related content.
		/// </summary>
		/// <param name="device">The graphics device.</param>
		/// <param name="content">The content manager.</param>
        public override void LoadContent(GraphicsDevice device, ContentManager content)
        {
 	        base.LoadContent(device, content);

            _gaussianBlurEffect = content.Load<Effect>("shaders/GaussianBlur");

            PresentationParameters pp = device.PresentationParameters;
            _tempRT = new RenderTarget2D(device, pp.BackBufferWidth, pp.BackBufferHeight, false,
                pp.BackBufferFormat, pp.DepthStencilFormat);
        }

		/// <summary>
		/// Render the blur to a seperate render target based on the original image.
		/// </summary>
		/// <param name="device">The graphics device.</param>
		/// <param name="renderTarget">The "Back buffer" render target</param>
		/// <param name="renderer">The scene renderer.</param>
		/// <returns></returns>
        public override RenderTarget2D DrawEffect(GraphicsDevice device, RenderTarget2D renderTarget, RenderingSystem.RendererImpl.Renderer renderer)
        {
            _tempRT = new RenderTarget2D(device, renderTarget.Width, renderTarget.Height, false, renderTarget.Format, renderTarget.DepthStencilFormat);

            SetBlurEffectParameters(1.0f / (float)renderTarget.Width, 0);

            DrawQuad(renderTarget, _tempRT, _gaussianBlurEffect, SamplerState.PointClamp, device);

            SetBlurEffectParameters(0f, 1.0f / (float)renderTarget.Height);

            DrawQuad(_tempRT, renderTarget, _gaussianBlurEffect, SamplerState.PointClamp, device);

            return renderTarget;
        }

		/// <summary>
		/// Set the settings of the blur fx.
		/// </summary>
		/// <param name="dx">The horizontal component of the blur.</param>
		/// <param name="dy">The vertical component of the blur.</param>
        private void SetBlurEffectParameters(float dx, float dy)
        {
            // Look up the sample weight and offset effect parameters.
            EffectParameter weightsParameter, offsetsParameter;

            weightsParameter = _gaussianBlurEffect.Parameters["SampleWeights"];
            offsetsParameter = _gaussianBlurEffect.Parameters["SampleOffsets"];

            // Look up how many samples our gaussian blur effect supports.
            int sampleCount = weightsParameter.Elements.Count;

            // Create temporary arrays for computing our filter settings.
            float[] sampleWeights = new float[sampleCount];
            Vector2[] sampleOffsets = new Vector2[sampleCount];

            // The first sample always has a zero offset.
            sampleWeights[0] = ComputeGaussian(0);
            sampleOffsets[0] = new Vector2(0);

            // Maintain a sum of all the weighting values.
            float totalWeights = sampleWeights[0];

            // Add pairs of additional sample taps, positioned
            // along a line in both directions from the center.
            for (int i = 0; i < sampleCount / 2; i++)
            {
                // Store weights for the positive and negative taps.
                float weight = ComputeGaussian(i + 1);

                sampleWeights[i * 2 + 1] = weight;
                sampleWeights[i * 2 + 2] = weight;

                totalWeights += weight * 2;

                // To get the maximum amount of blurring from a limited number of
                // pixel shader samples, we take advantage of the bilinear filtering
                // hardware inside the texture fetch unit. If we position our texture
                // coordinates exactly halfway between two texels, the filtering unit
                // will average them for us, giving two samples for the price of one.
                // This allows us to step in units of two texels per sample, rather
                // than just one at a time. The 1.5 offset kicks things off by
                // positioning us nicely in between two texels.
                float sampleOffset = i * 2 + 1.5f;

                Vector2 delta = new Vector2(dx, dy) * sampleOffset;

                // Store texture coordinate offsets for the positive and negative taps.
                sampleOffsets[i * 2 + 1] = delta;
                sampleOffsets[i * 2 + 2] = -delta;
            }

            // Normalize the list of sample weightings, so they will always sum to one.
            for (int i = 0; i < sampleWeights.Length; i++)
            {
                sampleWeights[i] /= totalWeights;
            }

            // Tell the effect about our new filter settings.
            weightsParameter.SetValue(sampleWeights);
            offsetsParameter.SetValue(sampleOffsets);
        }


        /// <summary>
        /// Evaluates a single point on the gaussian falloff curve.
        /// Used for setting up the blur filter weightings.
        /// </summary>
        private float ComputeGaussian(float n)
        {
            float theta = f_blurAmount;

            return (float)((1.0 / Math.Sqrt(2 * Math.PI * theta)) *
                           Math.Exp(-(n * n) / (2 * theta * theta)));
        }
    }
}
