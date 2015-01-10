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
    /// Manages the rendering of the SSAO effect.
    /// This is NOT managed as a post processing effect.
    /// Although it is implmented in a similar fashion with the image being overlayed to the existing render.
    /// </summary>
    public class SSAO
    {
        // The indices of the render target information to use.
        public const int DEPTH_RT_INDEX = 0;

        public const int NORMAL_RT_INDEX = 1;
        public const int RT_0_INDEX = 2;
        public const int RT_1_INDEX = 3;

        protected ScreenQuadRenderer _quadRenderer;
        private int _blurCount = 1;
        private Effect _effect;
        private BlendState _finalMixBlendState;
        private float _intensity = 1.75f;
        private EffectParameter _parameteGBufferHalfPixel;
        private EffectParameter _parameterBias;
        private EffectParameter _parameterBlurDirection;
        private EffectParameter _parameterDepthBuffer;
        private EffectParameter _parameterFarClip;
        private EffectParameter _parameterHalfBufferHalfPixel;
        private EffectParameter _parameterNormalBuffer;
        private EffectParameter _parameterRadius;
        private EffectParameter _parameterRandomMap;
        private EffectParameter _parameterRandomTile;
        private EffectParameter _parameterSSAOBuffer;
        private EffectParameter _parameterSSAOIntensity;
        private EffectParameter _parameterSSAORes;
        private EffectParameter _parameterTempBufferRes;
        private Texture2D _randomTex;
        private bool b_enabled = true;
        private float f_bias = 0.00001f;
        private float f_maxRadius = 0.5f;
        private float f_radius = 0.05f;
        private float f_randomTile = 100;

        public SSAO()
        {
            _quadRenderer = new ScreenQuadRenderer();
        }

        /// <summary>
        /// The bias to use when calculating the SSAO image.
        /// </summary>
        public float Bias
        {
            get { return f_bias; }
            set { f_bias = value; }
        }

        /// <summary>
        /// The number of blurs to apply over the SSAO image.
        /// </summary>
        public int BlurCount
        {
            get { return _blurCount; }
            set { _blurCount = value; if (_blurCount < 0) _blurCount = 0; }
        }

        /// <summary>
        /// Whether the effect is drawn or not.
        /// </summary>
        public bool Enabled
        {
            get { return b_enabled; }
            set { b_enabled = value; }
        }

        /// <summary>
        /// The intensity of the SSAO shading.
        /// Has to be a value between 0.1f and 2.0f.
        /// More intense the more the shading is noticable.
        /// </summary>
        public float Intensity
        {
            get { return _intensity; }
            set
            {
                _intensity = value;
                _intensity = MathHelper.Clamp(_intensity, 0.1f, 2.0f);
            }
        }

        /// <summary>
        /// The max radius of the radial blur applied.
        /// </summary>
        public float MaxRadius
        {
            get { return f_maxRadius; }
            set { f_maxRadius = value; }
        }

        /// <summary>
        /// The radius of the radial blur applied.
        /// </summary>
        public float Radius
        {
            get { return f_radius; }
            set { f_radius = value; }
        }

        /// <summary>
        /// The random tile used in calculating the random vector sampling in the fx effect.
        /// </summary>
        public float RandomTile
        {
            get { return f_randomTile; }
            set { f_randomTile = value; }
        }

        /// <summary>
        /// Compute the SSAO image itself.
        /// </summary>
        /// <param name="renderer">The scene renderer.</param>
        /// <param name="device">The graphics device.</param>
        public void ComputeSSAO(Renderer renderer, GraphicsDevice device)
        {
            RenderTarget2D[] rts = GetRenderingTargets(renderer);

            RenderTarget2D depthBuffer = rts[DEPTH_RT_INDEX];
            RenderTarget2D normalBuffer = rts[NORMAL_RT_INDEX];

            RenderTarget2D rt0 = rts[RT_0_INDEX];
            RenderTarget2D rt1 = rts[RT_1_INDEX];

            device.SetRenderTarget(rt0);
            device.Clear(Color.Black);

            _effect.CurrentTechnique = _effect.Techniques[0];
            _parameterDepthBuffer.SetValue(depthBuffer);
            _parameterNormalBuffer.SetValue(normalBuffer);
            _parameterRandomMap.SetValue(_randomTex);
            _parameterRandomTile.SetValue(f_randomTile);
            _parameterRadius.SetValue(new Vector2(f_radius, f_maxRadius));
            _parameterBias.SetValue(f_bias);

            _parameterHalfBufferHalfPixel.SetValue(new Vector2(0.5f / depthBuffer.Width, 0.5f / depthBuffer.Height));
            _parameteGBufferHalfPixel.SetValue(new Vector2(0.5f / normalBuffer.Width, 0.5f / normalBuffer.Height));

            _parameterFarClip.SetValue(renderer.CurrentCamera.FarClip);

            _effect.CurrentTechnique.Passes[0].Apply();

            device.RasterizerState = RasterizerState.CullNone;
            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.None;

            _quadRenderer.RenderQuad(device, -Vector2.One, Vector2.One);

            if (_blurCount > 0)
            {
                Vector2 tempBufferRes = new Vector2(rt1.Width, rt1.Height);
                _parameterTempBufferRes.SetValue(tempBufferRes);
                _effect.CurrentTechnique = _effect.Techniques[1];
                device.BlendState = BlendState.Opaque;
                device.DepthStencilState = DepthStencilState.None;
                for (int i = 0; i < _blurCount; i++)
                {
                    device.SetRenderTarget(rt1);
                    device.Clear(Color.Black);
                    _parameterSSAOBuffer.SetValue(rt0);
                    _parameterBlurDirection.SetValue(Vector2.UnitX / rt1.Width);
                    _effect.Techniques[1].Passes[0].Apply();
                    _quadRenderer.RenderQuad(device, -Vector2.One, Vector2.One);

                    device.SetRenderTarget(rt0);
                    device.Clear(Color.Black);
                    _parameterSSAOBuffer.SetValue(rt1);
                    _parameterBlurDirection.SetValue(Vector2.UnitY / rt1.Height);
                    _effect.Techniques[1].Passes[0].Apply();
                    _quadRenderer.RenderQuad(device, -Vector2.One, Vector2.One);
                }
            }

            device.DepthStencilState = DepthStencilState.None;

            device.BlendState = BlendState.Opaque;
        }

        /// <summary>
        /// Mix the computed SSAO image with the final scene render.
        /// </summary>
        /// <param name="renderer">The scene renderer.</param>
        /// <param name="device">The graphics device</param>
        public void FinalMix(Renderer renderer, GraphicsDevice device)
        {
            RenderTarget2D half0 = renderer.FullBuffer0;

            device.RasterizerState = RasterizerState.CullNone;
            device.DepthStencilState = DepthStencilState.None;

            device.BlendState = _finalMixBlendState;

            _effect.CurrentTechnique = _effect.Techniques[2];
            _parameterSSAOBuffer.SetValue(half0);
            Vector2 tempBufferRes = new Vector2(half0.Width, half0.Height);
            _parameterSSAORes.SetValue(tempBufferRes);
            _parameterSSAOIntensity.SetValue(Intensity);
            _effect.CurrentTechnique.Passes[0].Apply();
            _quadRenderer.RenderQuad(device, -Vector2.One, Vector2.One);
        }

        /// <summary>
        /// Get all render targets possible for the SSAO pass.
        /// </summary>
        /// <param name="renderer"></param>
        /// <returns></returns>
        public RenderTarget2D[] GetRenderingTargets(Renderer renderer)
        {
            // There will always be 4 render targets returned.
            RenderTarget2D[] rts = { renderer.FullDepth, renderer.NormalBuffer, renderer.FullBuffer0, renderer.FullBuffer1 };
            return rts;
        }

        /// <summary>
        /// Initialize the resources associated with this effect.
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            _finalMixBlendState = new BlendState()
            {
                ColorDestinationBlend = Blend.InverseSourceAlpha,
                AlphaDestinationBlend = Blend.InverseSourceAlpha,
                ColorSourceBlend = Blend.Zero,
                AlphaSourceBlend = Blend.Zero
            };

            _effect = content.Load<Effect>("shaders/SSAO");
            LoadParameters(content);
        }

        /// <summary>
        /// Unload all of the ummanaged data with the effect file.
        /// </summary>
        public void Unload()
        {
            _effect.Unload();
        }

        /// <summary>
        /// Load all of the effect parameters from the fx file.
        /// </summary>
        /// <param name="content"></param>
        private void LoadParameters(ContentManager content)
        {
            _randomTex = content.Load<Texture2D>("images/random");
            _parameterDepthBuffer = _effect.Parameters["DepthBuffer"];
            _parameterNormalBuffer = _effect.Parameters["NormalBuffer"];
            _parameterRandomMap = _effect.Parameters["RandomMap"];
            _parameterRandomTile = _effect.Parameters["RandomTile"];
            _parameterRadius = _effect.Parameters["Radius"];
            _parameterBias = _effect.Parameters["Bias"];
            _parameterTempBufferRes = _effect.Parameters["TempBufferRes"];
            _parameterSSAOBuffer = _effect.Parameters["SSAOBuffer"];
            _parameterBlurDirection = _effect.Parameters["BlurDirection"];
            _parameterSSAORes = _effect.Parameters["SSAORes"];
            _parameterSSAOIntensity = _effect.Parameters["SSAOIntensity"];
            _parameterFarClip = _effect.Parameters["FarClip"];
            _parameterHalfBufferHalfPixel = _effect.Parameters["HalfBufferHalfPixel"];
            _parameteGBufferHalfPixel = _effect.Parameters["GBufferPixelSize"];
        }
    }
}