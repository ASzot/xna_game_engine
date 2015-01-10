#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem.Graphics
{
	/// <summary>
	/// An effect for a light shaft effect which applied to one directional light in the scene.
	/// The nature of the effect is adjustable via settings.
    /// 
    /// Currently there are some problems with this effect in the scene.
    /// Check http://http.developer.nvidia.com/GPUGems3/gpugems3_ch13.html for more information on the light shaft effect.
    /// 
    /// This technique was adapted from the effect described at http://jcoluna.wordpress.com
	/// </summary>
    public class LightShaftPPE : PostProcessingEffect
    {
		/// <summary>
		/// The effect resource for rendering the light 'shafts' themselves.
		/// </summary>
        protected Effect _effect;

		// The fx variables for the light shaft effect.

        protected EffectParameter _parameterColorBuffer;
        protected EffectParameter _parameterScreenRes;
        protected EffectParameter _parameterSaturation;
        protected EffectParameter _parameterLinearColorBalance;
        protected EffectParameter _parameterLinearExposure;
        protected EffectParameter _parameterContrast;

        protected EffectParameter _parameterPixelSize;
        protected EffectParameter _parameterHalfPixel;
        protected EffectParameter _parameterHalfDepthTexture;
        protected EffectParameter _parameterRGBShaftTexture;
        protected EffectParameter _parameterLightCenter;
        protected EffectParameter _parameterScale;
        protected EffectParameter _parameterIntensity;
        protected EffectParameter _parameterSpread;
        protected EffectParameter _parameterTint;
        protected EffectParameter _parameterDecay;
        protected EffectParameter _parameterBlend;
        protected EffectParameter _parameterTextureAspectRatio;

		/// <summary>
		/// A quad renderer for the light shafts to easily render fullscreen passes.
		/// </summary>
        protected static RenderingSystem.RendererImpl.ScreenQuadRenderer _quadRenderer;

		/// <summary>
		/// A temporary render target used in intermediate renders.
		/// </summary>
        private RenderTarget2D _tmpFullRT;
		
		// Settings of the light shaft effect.

		/// <summary>
		/// How much the light shafts are blended with the rendered scene.
		/// </summary>
        private float _blend = 0.5f;
		/// <summary>
		/// The scale or size of the light shafts.
		/// </summary>
        private float _scale = 2f;
		/// <summary>
		/// The intensity of the light shaft colors.
		/// The larger the value the more prominent the light shafts.
		/// </summary>
        private float _intensity = 1f;
		/// <summary>
		/// How far the light shafts spread through openings.
		/// </summary>
        private float _spread = 0.2f;
        private float _decay = 0.0f;
        private Color _shaftTint = Color.White;
        private Vector2 _lightCenter = Vector2.Zero;

        private float _saturation = 1.02f;
        private float _contrast = 1.17f;
        private float _exposure = -0.45f;

        private Color _colorBalance = Color.White;
        protected Vector4 _linearColorBalance = Vector4.One;

        private RenderTargetSize _rtSize = RenderTargetSize.Quarter;

		/// <summary>
		/// The intensity of the light shafts.
		/// Higher values will result in more prominent light shafts.
		/// The fx variables will be updated.
		/// </summary>
        public float Intensity
        {
            get { return _intensity; }
            set
            {
                _intensity = value;
                if (_parameterIntensity != null)
                    _parameterIntensity.SetValue(Intensity);
            }
        }

		/// <summary>
		/// The size of the render target to render these lights shafts too.
		/// It has been noticed a larger size doesn't necissarily imply higher quality.
		/// </summary>
        public RenderTargetSize RTSize
        {
            get { return _rtSize; }
            set { _rtSize = value; }
        }

        public Vector2 LightCenter
        {
            get { return _lightCenter; }
            set
            {
                _lightCenter = value;
                if (_parameterLightCenter != null)
                    _parameterLightCenter.SetValue(value);
            }
        }

        public float Scale
        {
            get { return _scale; }
            set
            {
                _scale = value;
                if (_parameterScale != null)
                    _parameterScale.SetValue(value);
            }
        }

        public float Spread
        {
            get { return _spread; }
            set
            {
                _spread = value;
                if (_parameterSpread != null)
                    _parameterSpread.SetValue(value);
            }
        }
        public float Decay
        {
            get { return _decay; }
            set
            {
                _decay = value;
                if (_parameterDecay != null)
                    _parameterDecay.SetValue(value);
            }
        }

        public Color ShaftTint
        {
            get { return _shaftTint; }
            set
            {
                _shaftTint = value;
                if (_parameterTint != null)
                    _parameterTint.SetValue(value.ToVector4());
            }
        }
        public float Blend
        {
            get { return _blend; }
            set
            {
                _blend = value;
                if (_parameterBlend != null)
                    _parameterBlend.SetValue(value);
            }
        }

        public float Saturation
        {
            get { return _saturation; }
            set
            {
                _saturation = value;
                if (_parameterSaturation != null)
                    _parameterSaturation.SetValue(value);
            }
        }

        public float Contrast
        {
            get { return _contrast; }
            set
            {
                _contrast = value;
                if (_parameterContrast != null)
                    _parameterContrast.SetValue(value);
            }
        }

        public float Exposure
        {
            get { return _exposure; }
            set
            {
                _exposure = value;
                if (_parameterLinearExposure != null)
                    _parameterLinearExposure.SetValue((float)Math.Pow(2, _exposure));
            }
        }

        public Color ColorBalance
        {
            get { return _colorBalance; }
            set
            {
                _colorBalance = value;
                _linearColorBalance.X = (float)Math.Pow(_colorBalance.R / 255.0f, 2.2f);
                _linearColorBalance.Y = (float)Math.Pow(_colorBalance.G / 255.0f, 2.2f);
                _linearColorBalance.Z = (float)Math.Pow(_colorBalance.B / 255.0f, 2.2f);
                if (_parameterLinearColorBalance != null)
                    _parameterLinearColorBalance.SetValue(_linearColorBalance);
            }
        }

        public LightShaftPPE()
            : base("light shaft")
        {

        }

		/// <summary>
		/// Get the save data for this post processing effect.
		/// Gives the appropriate save structure.
		/// </summary>
		/// <returns>Of type LightShaftSaveData</returns>
        public override object GetSettingsSaveData()
        {
            LightShaftSaveData lssd = new LightShaftSaveData();
            lssd.Blend = this.Blend;
            lssd.ColorBalance = this.ColorBalance;
            lssd.Contrast = this.Contrast;
            lssd.Decay = this.Decay;
            lssd.Exposure = this.Exposure;
            lssd.Order = -1;
            lssd.Saturation = this.Saturation;
            lssd.Scale = this.Scale;
            lssd.ShaftTint = this.ShaftTint;
            lssd.Spread = this.Spread;
            lssd.RTSize = this.RTSize;

            return lssd;
        }

		/// <summary>
		/// Appropriately sets the light shaft save data.
		/// </summary>
		/// <param name="saveData">Of type light shaft save data.</param>
        public override void SetSettingsSaveData(object saveData)
        {
            LightShaftSaveData lssd = saveData as LightShaftSaveData;
            Blend = lssd.Blend;
            ColorBalance = lssd.ColorBalance;
            Contrast = lssd.Contrast;
            Decay = lssd.Decay;
            Exposure = lssd.Exposure;
            Saturation = lssd.Saturation;
            Scale = lssd.Scale;
            ShaftTint = lssd.ShaftTint;
            Spread = lssd.Spread;
            RTSize = lssd.RTSize;
        }

		/// <summary>
		/// Load the resource content involving the graphics device and content manager.
		/// </summary>
		/// <param name="device">The graphics device.</param>
		/// <param name="content">The content manager.</param>
        public override void LoadContent(GraphicsDevice device, ContentManager content)
        {
            _quadRenderer = new RenderingSystem.RendererImpl.ScreenQuadRenderer();
            try
            {
                _effect = content.Load<Effect>("shaders/LightShaft");
                ExtractParameters();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading bloom depth effect: " + ex.ToString());
            }

            PresentationParameters pp = device.PresentationParameters;
            _tmpFullRT = new RenderTarget2D(device, pp.BackBufferWidth, pp.BackBufferHeight, false, pp.BackBufferFormat, DepthFormat.None, 0,
                RenderTargetUsage.DiscardContents);
        }

		/// <summary>
		/// Get the parameters from the fx file for easy and quick access.
		/// </summary>
        protected void ExtractParameters()
        {
            _parameterColorBuffer = _effect.Parameters["ColorBuffer"];
            _parameterScreenRes = _effect.Parameters["ScreenRes"];
            _parameterSaturation = _effect.Parameters["Saturation"];
            _parameterLinearColorBalance = _effect.Parameters["LinearColorBalance"];
            _parameterLinearExposure = _effect.Parameters["LinearExposure"];
            _parameterContrast = _effect.Parameters["Contrast"];

            _parameterLinearColorBalance.SetValue(_linearColorBalance);
            _parameterSaturation.SetValue(_saturation);
            _parameterContrast.SetValue(_contrast);
            _parameterLinearExposure.SetValue((float)Math.Pow(2, _exposure));

            _parameterPixelSize = _effect.Parameters["PixelSize"];
            _parameterHalfPixel = _effect.Parameters["HalfPixel"];
            _parameterHalfDepthTexture = _effect.Parameters["DepthBuffer"];
            _parameterRGBShaftTexture = _effect.Parameters["ShaftBuffer"];

            _parameterScale = _effect.Parameters["Scale"];
            _parameterIntensity = _effect.Parameters["Intensity"];
            _parameterSpread = _effect.Parameters["Spread"];
            _parameterTint = _effect.Parameters["ShaftTint"];
            _parameterDecay = _effect.Parameters["Decay"];
            _parameterLightCenter = _effect.Parameters["LightCenter"];
            _parameterBlend = _effect.Parameters["Blend"];

            _parameterScale.SetValue(Scale);
            _parameterIntensity.SetValue(Intensity);
            _parameterSpread.SetValue(Spread);
            _parameterTint.SetValue(_shaftTint.ToVector4());
            _parameterDecay.SetValue(_decay);

            _parameterTextureAspectRatio = _effect.Parameters["TextureAspectRatio"];

            if (_parameterBlend != null)
                _parameterBlend.SetValue(_blend);
        }

		/// <summary>
		/// Update the position of the light direction for the light shafts to be rendered relative to.
		/// This should be called on every update.
		/// </summary>
		/// <param name="lightDir">The direciton of the directional light.</param>
		/// <param name="cam">The camera of the scene.</param>
        private void UpdateLightDir(Vector3 lightDir, ICamera cam)
        {
            Vector3 pos = cam.Transform.Translation - lightDir * 10;
            Vector4 pos4 = new Vector4(pos, 1);
            pos4 = Vector4.Transform(pos4, cam.ViewProj);

            pos.X = pos4.X / pos4.W;
            // flip Y 
            pos.Y = -pos4.Y / pos4.W;
            pos.Z = pos4.Z / pos4.W;

            // do some hacks to make the intensity goes to zero when the light is outside the screen
            float intensity = 1 - (float)Math.Sqrt(pos.X * pos.X + pos.Y * pos.Y) * 0.15f;
            intensity = Math.Min(1, intensity);
            intensity = Math.Max(0, intensity);
            if (pos.Z < 0 || pos.Z > 1)
                Intensity = 0;
            else
            {
                // Make the intensity function more narrow ( intensity^3 )
                Intensity = 1f * (intensity * intensity * intensity);
            }
            LightCenter = new Vector2(pos.X, pos.Y);
        }

		/// <summary>
		/// Create the light shaft editor form.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="e"></param>
        public override void CreateSettingsDlg(object obj, EventArgs e)
        {
            Forms.LightShaftsSettingsForm lssf = new Forms.LightShaftsSettingsForm(this);
            lssf.ShowDialog();
        }

		/// <summary>
		/// Synchronize with the direcitonal light of the scene.
		/// The first directional light in the light list will be used.
		/// </summary>
		/// <param name="frameLights">The list of lights for this frame.</param>
		/// <param name="cam">The camera for this scene.</param>
        public override void SetFrameData(System.Collections.Generic.List<RendererImpl.GeneralLight> frameLights, ICamera cam)
        {
            var dirLights = from fl in frameLights
                            where fl.LightType == RendererImpl.GeneralLight.Type.Directional
                            select fl;

            if (dirLights.Count() > 0)
            {
                var mainLight = dirLights.ElementAt(0);
                Vector3 dir = mainLight.Transform.Forward;
                //dir = new Vector3(0.5383026f, -0.6266303f, 0.5635273f);
                UpdateLightDir(dir, cam);
            }
        }

		/// <summary>
		/// Draw the light shaft effect using the final scene render target.
		/// The input render target will not be redrawn.
		/// </summary>
		/// <param name="device">The graphics device.</param>
		/// <param name="renderTarget">The final scene render target.</param>
		/// <param name="renderer">The renderer for the scene.</param>
		/// <returns></returns>
        public override RenderTarget2D DrawEffect(GraphicsDevice device, RenderTarget2D renderTarget, RendererImpl.Renderer renderer)
        {
            RenderTarget2D rt0 = null;
            RenderTarget2D rt1 = null;
            switch (_rtSize)
            {
                case RenderTargetSize.Full:
                    rt0 = renderer.FullBuffer0;
                    rt1 = renderer.FullBuffer1;
                    break;
                case RenderTargetSize.Half:
                    rt0 = renderer.HalfBuffer0;
                    rt1 = renderer.HalfBuffer1;
                    break;
                case RenderTargetSize.Quarter:
                    rt0 = renderer.QuarterBuffer0;
                    rt1 = renderer.QuarterBuffer1;
                    break;
            }
            RenderTarget2D halfDepth = renderer.GetDownsampledDepth();

            _effect.CurrentTechnique = _effect.Techniques[0];
            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.None;
            device.RasterizerState = RasterizerState.CullNone;

            //render to a half-res buffer
            device.SetRenderTarget(rt0);
            _parameterColorBuffer.SetValue(renderTarget);
            _parameterHalfDepthTexture.SetValue(halfDepth);
            _parameterTextureAspectRatio.SetValue(renderTarget.Height / (float)renderTarget.Width);
            // Convert to rgb first, so we have linear filtering
            _effect.CurrentTechnique = _effect.Techniques[0];

            Vector2 pixelSize = new Vector2(1.0f / (float)renderTarget.Width, 1.0f / (float)renderTarget.Height);
            _parameterPixelSize.SetValue(pixelSize);
            _parameterHalfPixel.SetValue(pixelSize * 0.5f);

            _effect.CurrentTechnique.Passes[0].Apply();
            _quadRenderer.RenderQuad(device, -Vector2.One, Vector2.One);

            pixelSize = new Vector2(1.0f / (float)rt0.Width, 1.0f / (float)rt0.Height);
            _parameterPixelSize.SetValue(pixelSize);
            _parameterHalfPixel.SetValue(pixelSize * 0.5f);
            _effect.CurrentTechnique = _effect.Techniques[1];

            device.SetRenderTarget(rt1);
            _parameterRGBShaftTexture.SetValue(rt0);

            _effect.CurrentTechnique.Passes[0].Apply();
            _quadRenderer.RenderQuad(device, -Vector2.One, Vector2.One);


            device.SetRenderTarget(_tmpFullRT);

            pixelSize = new Vector2(1.0f / (float)renderTarget.Width, 1.0f / (float)renderTarget.Height);
            _parameterPixelSize.SetValue(pixelSize);
            _parameterHalfPixel.SetValue(pixelSize * 0.5f);

            device.RasterizerState = RasterizerState.CullNone;
            device.DepthStencilState = DepthStencilState.None;

            device.BlendState = BlendState.Opaque;

            _parameterRGBShaftTexture.SetValue(rt1);
            _effect.CurrentTechnique = _effect.Techniques[2];
            _effect.CurrentTechnique.Passes[0].Apply();
            _quadRenderer.RenderQuad(device, -Vector2.One, Vector2.One);

            return _tmpFullRT;
        }
    }
}
