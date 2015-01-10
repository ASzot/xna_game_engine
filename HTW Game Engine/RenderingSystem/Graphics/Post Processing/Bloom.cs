#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem.Graphics
{
	/// <summary>
	/// The settings of the bloom effect.
	/// </summary>
	[Serializable]
    public class BloomSettings
    {
		/// <summary>
		/// Some default data for the bloom settings.
		/// These are used for testing.
		/// </summary>
        [NonSerialized]
        public static BloomSettings[] PresetSettings =
        {
            //                Name           Thresh  Blur Bloom  Base  BloomSat BaseSat
            new BloomSettings("Default",     0.25f,  4,   1.25f, 1,    1,       1),
            new BloomSettings("Soft",        0,      3,   1,     1,    1,       1),
            new BloomSettings("Desaturated", 0.5f,   8,   2,     1,    0,       1),
            new BloomSettings("Saturated",   0.25f,  4,   2,     1,    2,       0),
            new BloomSettings("Blurry",      0,      2,   1,     0.1f, 1,       1),
            new BloomSettings("Subtle",      0.5f,   2,   1,     1,    1,       1),
        };

		/// <summary>
		/// The intensity of the base image in the final combine.
		/// </summary>
        public float BaseIntensity;

		/// <summary>
		/// The saturation of the base image in the final combine.
		/// </summary>
        public float BaseSaturation;

        /// <summary>
        /// The intensity of the bloom. Range is normally from 0 to 1.
        /// </summary>
        public float BloomIntensity;

        /// <summary>
        /// The saturation of the bloom image produced. 0 is unsaturated, 1 is completely saturated.
        /// </summary>
        public float BloomSaturation;

        /// <summary>
        /// Controls which pixels are actually selected for sampling.
        /// </summary>
        public float BloomThreshold;

        /// <summary>
        /// The blurness of the final processed image produced.
        /// </summary>
        public float BlurAmount;

		/// <summary>
		/// The name of the setting used by the post processing manager.
		/// </summary>
        public string SettingsName;

        /// <summary>
        /// Constructs a new bloom settings descriptor.
        /// </summary>
        public BloomSettings(string name, float bloomThreshold, float blurAmount,
                             float bloomIntensity, float baseIntensity,
                             float bloomSaturation, float baseSaturation)
        {
            SettingsName = name;
            BloomThreshold = bloomThreshold;
            BlurAmount = blurAmount;
            BloomIntensity = bloomIntensity;
            BaseIntensity = baseIntensity;
            BloomSaturation = bloomSaturation;
            BaseSaturation = baseSaturation;
        }
    }

	/// <summary>
	/// A post processing effect which produces a bloom over the image given a number of parameters.
	/// </summary>
    public class BloomPPE : PostProcessingEffect
    {
		/// <summary>
		/// Identifier for the bloom for the post processing manager.
		/// </summary>
        public const string BLOOM_ID = "Bloom";

		/// <summary>
		/// Effect resource for combining the bloom and base image.
		/// </summary>
        private Effect _bloomCombineEffect;

		/// <summary>
		/// Effect resource for extracting the bloom image from the base image.
		/// </summary>
        private Effect _bloomExtractEffect;
		/// <summary>
		/// A basic gausian vertical and horizontal blur over the blur image.
		/// </summary>
        private Effect _gaussianBlurEffect;
		/// <summary>
		/// A temporary render target.
		/// </summary>
        private RenderTarget2D _renderTarget1;
		/// <summary>
		/// Another temporary temporary render target.
		/// </summary>
        private RenderTarget2D _renderTarget2;
		
		/// <summary>
		/// The settings for this bloom effect.
		/// </summary>
        private BloomSettings _settings = BloomSettings.PresetSettings[1];
		/// <summary>
		/// The sprite batch used for 2D rendering.
		/// </summary>
        private SpriteBatch spriteBatch;

        // Choose what display settings the bloom should use.
        public BloomSettings Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }

        public BloomPPE()
            : base(BLOOM_ID)
        {
        }

		/// <summary>
		/// Create the settings form.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="e"></param>
		public override void CreateSettingsDlg(object obj, EventArgs e)
        {
			Forms.BloomSettingsForm bloomSettingsForm = new Forms.BloomSettingsForm(_settings);
            bloomSettingsForm.OnSettingsAccept += (BloomSettings bs) =>
                {
                    _settings = bs;
                };
			bloomSettingsForm.ShowDialog();
        }

		/// <summary>
		/// Draw the effect to a seperate render target. 
		/// </summary>
		/// <param name="device">the graphics device.</param>
		/// <param name="sceneRenderTarget">The "Back buffer" render target</param>
		/// <param name="renderer">The renderer for the scene.</param>
		/// <returns></returns>
        public override RenderTarget2D DrawEffect(GraphicsDevice device, RenderTarget2D sceneRenderTarget, RenderingSystem.RendererImpl.Renderer renderer)
        {
            device.SamplerStates[1] = SamplerState.LinearClamp;

            // Pass 1: draw the scene into rendertarget 1, using a
            // shader that extracts only the brightest parts of the image.
            _bloomExtractEffect.Parameters["BloomThreshold"].SetValue(
                Settings.BloomThreshold);

            DrawQuad(sceneRenderTarget, _renderTarget1,
                               _bloomExtractEffect, device);

            // Pass 2: draw from rendertarget 1 into rendertarget 2,
            // using a shader to apply a horizontal gaussian blur filter.
            SetBlurEffectParameters(1.0f / (float)_renderTarget1.Width, 0);

            DrawQuad(_renderTarget1, _renderTarget2,
                               _gaussianBlurEffect, device);

            // Pass 3: draw from rendertarget 2 back into rendertarget 1,
            // using a shader to apply a vertical gaussian blur filter.
            SetBlurEffectParameters(0, 1.0f / (float)_renderTarget1.Height);

            DrawQuad(_renderTarget2, _renderTarget1,
                               _gaussianBlurEffect, device);

            // Pass 4: draw both rendertarget 1 and the original scene
            // image back into the main backbuffer, using a shader that
            // combines them to produce the final bloomed result.
            device.SetRenderTarget(_renderTarget2);

            EffectParameterCollection parameters = _bloomCombineEffect.Parameters;

            parameters["BloomIntensity"].SetValue(Settings.BloomIntensity);
            parameters["BaseIntensity"].SetValue(Settings.BaseIntensity);
            parameters["BloomSaturation"].SetValue(Settings.BloomSaturation);
            parameters["BaseSaturation"].SetValue(Settings.BaseSaturation);

            device.Textures[1] = sceneRenderTarget;

            Viewport viewport = device.Viewport;
            DrawQuad(_renderTarget1,
                               viewport.Width, viewport.Height,
                               _bloomCombineEffect);

            return _renderTarget2;
        }

		/// <summary>
		/// Get the settings data for saving.
		/// </summary>
		/// <returns></returns>
        public override object GetSettingsSaveData()
        {
            BloomSaveData bsd = new BloomSaveData();
            bsd.BaseIntensity = Settings.BaseIntensity;
            bsd.BaseSaturation = Settings.BaseSaturation;
            bsd.BloomIntensity = Settings.BloomIntensity;
            bsd.BloomSaturation = Settings.BloomSaturation;
            bsd.BloomThreshold = Settings.BloomThreshold;
            bsd.BlurAmount = Settings.BlurAmount;
            bsd.Name = Settings.SettingsName;
            bsd.Order = -1;

            return bsd;
        }

		/// <summary>
		/// Load all resource related content.
		/// </summary>
		/// <param name="device">Graphics device.</param>
		/// <param name="content">The content</param>
        public override void LoadContent(GraphicsDevice device, ContentManager content)
        {
            spriteBatch = new SpriteBatch(device);

            _bloomExtractEffect = content.Load<Effect>("shaders/BloomExtract");
            _bloomCombineEffect = content.Load<Effect>("shaders/BloomCombine");
            _gaussianBlurEffect = content.Load<Effect>("shaders/GaussianBlur");

            // Look up the resolution and format of our main backbuffer.
            PresentationParameters pp = device.PresentationParameters;

            int width = pp.BackBufferWidth;
            int height = pp.BackBufferHeight;

            SurfaceFormat format = pp.BackBufferFormat;

            // Create two rendertargets for the bloom processing. These are half the
            // size of the backbuffer, in order to minimize fillrate costs. Reducing
            // the resolution in this way doesn't hurt quality, because we are going
            // to be blurring the bloom images in any case.
            width /= 1;
            height /= 1;

            _renderTarget1 = new RenderTarget2D(device, width, height, false, format, DepthFormat.None);
            _renderTarget2 = new RenderTarget2D(device, width, height, false, format, DepthFormat.None);
        }

		/// <summary>
		/// Set the save settings data.
		/// </summary>
		/// <param name="saveData"></param>
        public override void SetSettingsSaveData(object saveData)
        {
            BloomSaveData bsd = saveData as BloomSaveData;
            Settings.BaseIntensity = bsd.BaseIntensity;
            Settings.BaseSaturation = bsd.BaseSaturation;
            Settings.BloomIntensity = bsd.BloomIntensity;
            Settings.BloomSaturation = bsd.BloomSaturation;
            Settings.BloomThreshold = bsd.BloomThreshold;
            Settings.BlurAmount = bsd.BlurAmount;
            Settings.SettingsName = bsd.Name;
        }

		/// <summary>
		/// Release all resource related content.
		/// </summary>
        public override void UnloadContent()
        {
            _renderTarget1.Dispose();
            _renderTarget2.Dispose();
        }

        /// <summary>
        /// Evaluates a single point on the gaussian falloff curve.
        /// Used for setting up the blur filter weightings.
        /// </summary>
        private float ComputeGaussian(float n)
        {
            float theta = Settings.BlurAmount;

            return (float)((1.0 / Math.Sqrt(2 * Math.PI * theta)) *
                           Math.Exp(-(n * n) / (2 * theta * theta)));
        }

		/// <summary>
		/// Set the vertical and horizontal blur settings of the blur.
		/// </summary>
		/// <param name="dx"></param>
		/// <param name="dy"></param>
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
    }
}