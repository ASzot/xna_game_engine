#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

////////////////////////////////////////////////////////////////////////////////////////////////
// The list of all very solid sources used to help create the rendering system as a whole.
// Some were used more extensively than others.
// http://jcoluna.wordpress.com (Check out for a very solid explanation of how this all works).
// http://diaryofagraphicsprogrammer.blogspot.com/2008/03/light-pre-pass-renderer.html
// http://mynameismjp.wordpress.com/2009/03/10/reconstructing-position-from-depth/
// For the ComputeFrustumCorners(...) http://mynameismjp.wordpress.com/2009/03/10/reconstructing-position-from-depth/

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem.RendererImpl
{
    /// <summary>
    /// The different types of rendering targets used throughout the deferred rendering processes.
    /// Used for debugging purposes in stopping the rendering ealry.
    /// </summary>
    public enum RenderedTarget { NormalRT, DepthRT, LightRT, LightSpecRT, SsaoRT, FinalRT };

    /// <summary>
    /// Renders lights and models onto a render target.
    /// This is the core of the rendering code.
    /// </summary>
    public class Renderer
    {
        private Graphics.BlurPPE _blur;

        private DepthStencilState _ccwDepthState;

        private Effect _clearGBuffer;

        private ContentManager _contentManager;

        private Vector3[] _cornersViewSpace = new Vector3[8];

        private Vector3[] _cornersWorldSpace = new Vector3[8];

        private Vector3[] _currentFrustumCorners = new Vector3[4];

        private DepthStencilState _cwDepthState;

        private RenderTarget2D _depthBuffer;

        private bool _depthDownsampledThisFrame = false;

        private DepthStencilState _depthStateDrawLights;

        private DepthStencilState _depthStateReconstructZ;

        private GraphicsDevice _device;

        private DepthStencilState _directionalDepthState;

        private DownsampleDepthEffect _downsampleDepth;

        private RenderTarget2D _fullBuffer0;

        private RenderTarget2D _fullBuffer1;

        private RenderTargetBinding[] _gBufferBinding = new RenderTargetBinding[2];

        private RenderTarget2D _halfBuffer0;

        private RenderTarget2D _halfBuffer1;

        private RenderTarget2D _halfDepth;

        private RenderTargetBinding[] _lightAccumBinding = new RenderTargetBinding[2];

        private BlendState _lightAddBlendState;

        private RenderTarget2D _lightBuffer;

        private List<LightEntry> _lightEntries = new List<LightEntry>();

        private Effect _lighting;

        private List<LightEntry> _lightShadowCasters = new List<LightEntry>();

        private RenderTarget2D _lightSpecularBuffer;

        private Vector3[] _localFrustumCorners = new Vector3[4];

        private RenderTarget2D _normalBuffer;

        private RenderTarget2D _outputTexture;

        private ScreenQuadRenderer _quadRenderer;

        private RenderTarget2D _quarterBuffer0;

        private RenderTarget2D _quarterBuffer1;

        private Effect _reconstructZBuffer;

        private RenderedTarget _renderedTarget = RenderedTarget.FinalRT;

        private ShadowRenderer _shadowRenderer;

        private ShapeRenderer _sphereRenderer;

        private ShapeRenderer _spotRenderer;

        private SpriteBatch _spriteBatch;

        private SSAO _ssao;

        private List<GeneralLight> _visibleLights = new List<GeneralLight>(10);

        private List<Mesh.SubMesh>[] _visibleMeshes = new List<Mesh.SubMesh>[(int)(MeshData.RenderQueueType.Count)];

        private List<ParticleSystemEntry> _visibleParticleSystemEntries = new List<ParticleSystemEntry>();

        private List<ParticleSystem> _visibleParticleSystems = new List<ParticleSystem>();

        private bool b_useQuads = true;

        private int i_height;

        private int i_width;

        private ICamera p_camera;

        /// <summary>
        /// Construct the render states create, GBuffer resources, load shaders into memory, load render effect content.
        /// </summary>
        /// <param name="device">The game's graphics device.</param>
        /// <param name="content">The game's content manager.</param>
        /// <param name="width">The width of the scene render. Doesn't have to be the same as the window width.</param>
        /// <param name="height">The height of the scene render. Doesn't have to be the same as the window height.</param>
        public Renderer(GraphicsDevice device, ContentManager content, int width, int height)
        {
            i_width = width;
            i_height = height;
            _contentManager = content;
            _device = device;
            _quadRenderer = new ScreenQuadRenderer();
            _sphereRenderer = new ShapeRenderer(content.Load<Model>("Models/light_volumes/sphere"));
            _spotRenderer = new ShapeRenderer(content.Load<Model>("Models/light_volumes/cone"));

            _cwDepthState = new DepthStencilState();
            _cwDepthState.DepthBufferWriteEnable = false;
            _cwDepthState.DepthBufferFunction = CompareFunction.LessEqual;

            _ccwDepthState = new DepthStencilState();
            _ccwDepthState.DepthBufferWriteEnable = false;
            _ccwDepthState.DepthBufferFunction = CompareFunction.GreaterEqual;

            _directionalDepthState = new DepthStencilState(); ;
            _directionalDepthState.DepthBufferWriteEnable = false;
            _directionalDepthState.DepthBufferFunction = CompareFunction.Greater;

            _depthStateDrawLights = new DepthStencilState();

            // Light volumes need to be drawn with front facing culling so GreaterEqual is necissary here.
            _depthStateDrawLights.DepthBufferFunction = CompareFunction.GreaterEqual;
            // We only read from the z-buffer (depth buffer).
            _depthStateDrawLights.DepthBufferWriteEnable = false;

            _shadowRenderer = new ShadowRenderer(GraphicsDevice);

            _depthStateReconstructZ = new DepthStencilState
            {
                DepthBufferEnable = true,
                DepthBufferWriteEnable = true,
                DepthBufferFunction = CompareFunction.Always
            };

            _lightAddBlendState = new BlendState()
                {
                    AlphaSourceBlend = Blend.One,
                    ColorSourceBlend = Blend.One,
                    AlphaDestinationBlend = Blend.One,
                    ColorDestinationBlend = Blend.One,
                };

            // Create the various render queues for specialized or general rendering.
            for (int index = 0; index < _visibleMeshes.Length; index++)
            {
                _visibleMeshes[index] = new List<Mesh.SubMesh>();
            }

            CreateGBuffer();
            LoadShaders();

            _downsampleDepth = new DownsampleDepthEffect();
            _downsampleDepth.LoadContent(content, this);

            _ssao = new SSAO();
            _ssao.LoadContent(content);

            _blur = new Graphics.BlurPPE();
            _blur.LoadContent(_device, _contentManager);
            _spriteBatch = new SpriteBatch(_device);
            _blur.SetSpriteBatch(_spriteBatch);

            WaterPlane.LoadContent(_device);
        }

        /// <summary>
        /// A pointer to the camera being used.
        /// </summary>
        public ICamera CurrentCamera
        {
            get { return p_camera; }
        }

        /// <summary>
        /// The rendered depth buffer containing the depth values of the scene.
        /// </summary>
        public RenderTarget2D DepthBuffer
        {
            get { return _depthBuffer; }
        }

        /// <summary>
        /// A render target the same resolution of the back buffer.
        /// Various render effect use this.
        /// A primary render target.
        /// </summary>
        public RenderTarget2D FullBuffer0
        {
            get { return _fullBuffer0; }
        }

        /// <summary>
        /// A render target the same resolution of the back buffer.
        /// Various render effects use this.
        /// A secondary temporary render target.
        /// </summary>
        public RenderTarget2D FullBuffer1
        {
            get { return _fullBuffer1; }
        }

        /// <summary>
        /// A temporary depth buffer the same resolution of the primary depth buffer.
        /// </summary>
        public RenderTarget2D FullDepth
        {
            get { return _depthBuffer; }
        }

        public GraphicsDevice GraphicsDevice
        {
            get { return _device; }
        }

        /// <summary>
        /// A render target half the resolution of the back buffer.
        /// Various render effect use this.
        /// A primary render target.
        /// </summary>
        public RenderTarget2D HalfBuffer0
        {
            get { return _halfBuffer0; }
        }

        /// <summary>
        /// A render target half the resolution of the back buffer.
        /// Various render effects use this.
        /// A secondary temporary render target.
        /// </summary>
        public RenderTarget2D HalfBuffer1
        {
            get { return _halfBuffer1; }
        }

        /// <summary>
        /// A temporary depth buffer half the resolution of the primary depth buffer.
        /// Various render effects use this.
        /// </summary>
        public RenderTarget2D HalfDepth
        {
            get { return _halfDepth; }
        }

        /// <summary>
        /// The rendered light buffer containing the diffuse light of the scene.
        /// </summary>
        public RenderTarget2D LightBuffer
        {
            get { return _lightBuffer; }
        }

        /// <summary>
        /// The rendered light buffer containing the specular light and power of the scene.
        /// </summary>
        public RenderTarget2D LightSpecularBuffer
        {
            get { return _lightSpecularBuffer; }
        }

        /// <summary>
        /// The render normal buffer containing the normals of the scene.
        /// </summary>
        public RenderTarget2D NormalBuffer
        {
            get { return _normalBuffer; }
        }

        /// <summary>
        /// A render target a quarter the resolution of the back buffer.
        /// Various render effect use this.
        /// A primary render target.
        /// </summary>
        public RenderTarget2D QuarterBuffer0
        {
            get { return _quarterBuffer0; }
        }

        /// <summary>
        /// A render target a quarter the resolution of the back buffer.
        /// Various render effects use this.
        /// A secondary temporary render target.
        /// </summary>
        public RenderTarget2D QuarterBuffer1
        {
            get { return _quarterBuffer1; }
        }

        /// <summary>
        /// The final rendered scene.
        /// </summary>
        public RenderedTarget RenderedTarget
        {
            get { return _renderedTarget; }
            set { _renderedTarget = value; }
        }

        /// <summary>
        /// In charge of drawing the SSAO effect over the scene.
        /// </summary>
        public SSAO SSAO
        {
            get { return _ssao; }
        }

        /// <summary>
        /// Whether the quads should be rendered.
        /// Primarily used for debugging purposes.
        /// </summary>
        public bool UseQuads
        {
            get { return b_useQuads; }
            set { b_useQuads = value; }
        }

        /// <summary>
        /// This method computes the frustum corners applied to a quad that can be smaller than
        /// our screen. This is useful because instead of drawing a full-screen quad for each
        /// point light, we can draw smaller quads that fit the light's bounding sphere in screen-space,
        /// avoiding unecessary pixel shader operations
        /// </summary>
        /// <param name="effect">The effect we want to apply those corners</param>
        /// <param name="topLeftVertex"> The top left vertex, in screen space [-1..1]</param>
        /// <param name="bottomRightVertex">The bottom right vertex, in screen space [-1..1]</param>
        public void ApplyFrustumCorners(Effect effect, Vector2 topLeftVertex, Vector2 bottomRightVertex)
        {
            ApplyFrustumCorners(effect.Parameters["FrustumCorners"], topLeftVertex, bottomRightVertex);
        }

        public void ApplyFrustumCorners(EffectParameter frustumCorners, Vector2 topLeftVertex, Vector2 bottomRightVertex)
        {
            float dx = _currentFrustumCorners[1].X - _currentFrustumCorners[0].X;
            float dy = _currentFrustumCorners[0].Y - _currentFrustumCorners[2].Y;

            _localFrustumCorners[0] = _currentFrustumCorners[2];
            _localFrustumCorners[0].X += dx * (topLeftVertex.X * 0.5f + 0.5f);
            _localFrustumCorners[0].Y += dy * (bottomRightVertex.Y * 0.5f + 0.5f);

            _localFrustumCorners[1] = _currentFrustumCorners[2];
            _localFrustumCorners[1].X += dx * (bottomRightVertex.X * 0.5f + 0.5f);
            _localFrustumCorners[1].Y += dy * (bottomRightVertex.Y * 0.5f + 0.5f);

            _localFrustumCorners[2] = _currentFrustumCorners[2];
            _localFrustumCorners[2].X += dx * (topLeftVertex.X * 0.5f + 0.5f);
            _localFrustumCorners[2].Y += dy * (topLeftVertex.Y * 0.5f + 0.5f);

            _localFrustumCorners[3] = _currentFrustumCorners[2];
            _localFrustumCorners[3].X += dx * (bottomRightVertex.X * 0.5f + 0.5f);
            _localFrustumCorners[3].Y += dy * (topLeftVertex.Y * 0.5f + 0.5f);

            frustumCorners.SetValue(_localFrustumCorners);
        }

        /// <summary>
        /// Get the downsampled depth if it already exists if not create it then return it.
        /// </summary>
        /// <returns>The downsampled depth render target.</returns>
        public RenderTarget2D GetDownsampledDepth()
        {
            if (!_depthDownsampledThisFrame)
                GenerateDownsampleDepth();
            return _halfDepth;
        }

        /// <summary>
        /// Get the shadow save information for serilization.
        /// </summary>
        /// <param name="csmRes">The resolution of the cascade shadow map.</param>
        /// <param name="csmDiv">The number of cascade shadow map divisions.</param>
        /// <param name="numSpotShadows">The max number of spot light shadows.</param>
        /// <param name="numCsmShadows">The max number of cascade light shadows.</param>
        /// <param name="spotShadowRes">The resolution of the spot light shadows.</param>
        public void GetShadowInfo(out int csmRes, out int csmDiv, out int numSpotShadows, out int numCsmShadows, out int spotShadowRes)
        {
            csmRes = ShadowRenderer.CascadeMapResolution;
            csmDiv = ShadowRenderer.NumCascadeSplits;
            numSpotShadows = ShadowRenderer.MaxNumSpotShadows;
            numCsmShadows = ShadowRenderer.MaxNumCsmShadows;
            spotShadowRes = ShadowRenderer.SpotShadowResolution;
        }

        /// <summary>
        /// Gets a shadow map at the specified index.
        /// </summary>
        /// <param name="shadowMapIndex">The index in _lightShadowCasters.</param>
        /// <returns></returns>
        public Texture2D GetShadowMap(int shadowMapIndex)
        {
            if (shadowMapIndex < _lightShadowCasters.Count)
                return _lightShadowCasters[shadowMapIndex].SpotShadowMap.Texture;
            return null;
        }

        /// <summary>
        /// Render the meshes and lights in the scene and the SSAO over the scene.
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="renderController"></param>
        /// <param name="gameTime"></param>
        /// <param name="ppeMgr"></param>
        /// <param name="debugTimings"></param>
        /// <returns></returns>
        public RenderTarget2D RenderScene(ICamera camera, RenderController renderController, GameTime gameTime, Graphics.PPEMgr ppeMgr,
            DebugTimings debugTimings)
        {
            _depthDownsampledThisFrame = false;
            p_camera = camera;

            DrawWaterPreRender(camera, renderController);

            BaseRenderEffect.TotalTime = (float)gameTime.TotalGameTime.TotalSeconds;
            //compute the frustum corners for this camera
            ComputeFrustumCorners(camera);

            ///////////////////////////////////////////////////////////////
            // START Shadow Mapping.
            debugTimings.StartShadowRenderSw();

            //this resets the free shadow maps
            _shadowRenderer.InitFrame();
            _visibleLights.Clear();

            renderController.GetVisibleLights(camera.Frustum, _visibleLights);
            //sort lights, choose the shadow casters
            SortLights(camera);
            SelectShadowCasters();
            //generate all shadow maps
            GenerateShadows(camera, renderController);

            debugTimings.EndShadowRenderSw();
            // END Shadow Mapping
            //////////////////////////////////////////////////////////////

            //////////////////////////////////////////////////////////////
            // START GBuffer Render.
            debugTimings.StartGBufferRenderSw();

            //first of all, we must bind our GBuffer and reset all states
            _device.SetRenderTargets(_gBufferBinding);

            _device.Clear(ClearOptions.DepthBuffer | ClearOptions.Stencil, Color.Black, 1.0f, 0);
            _device.BlendState = BlendState.Opaque;
            _device.DepthStencilState = DepthStencilState.None;
            _device.RasterizerState = RasterizerState.CullNone;

            //bind the effect that outputs the default GBuffer values
            _clearGBuffer.CurrentTechnique.Passes[0].Apply();
            //draw a full screen quad for clearing our GBuffer
            _quadRenderer.RenderQuad(_device, -Vector2.One, Vector2.One);

            _device.DepthStencilState = DepthStencilState.Default;
            _device.RasterizerState = RasterizerState.CullCounterClockwise;

            //select the visible meshes
            CullVisibleMeshes(camera, renderController);

            //select the visible particle systems
            CullVisibleParticleSystems(camera, renderController);

            //now, render them to the G-Buffer
            RenderToGbuffer(camera);

            ppeMgr.DrawGeometryPhase(_device, camera.View, camera.Proj);

            debugTimings.EndGBufferRenderSw();
            // END GBuffer Render.
            /////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////
            // START Light Render.
            debugTimings.StartLightRenderSw();
            // Render GBuffer and lights.

            // Clear the light buffer with black
            _device.SetRenderTargets(_lightAccumBinding);
            // We don't want Color.Black as the alpha component is 255. (Someone learned this the hard way).
            _device.Clear(new Color(0, 0, 0, 0));

            if (_renderedTarget == RenderedTarget.DepthRT)
                return _depthBuffer;
            else if (_renderedTarget == RenderedTarget.NormalRT)
                return _normalBuffer;

            // Don't use depth/stencil test... there's no depth buffer in use right now.
            _device.DepthStencilState = DepthStencilState.None;
            _device.RasterizerState = RasterizerState.CullCounterClockwise;

            // Draw using additive blending I honestly don't know why its what works.

            _device.BlendState = _lightAddBlendState;

            RenderLights(camera);

            debugTimings.EndLightRenderSw();
            // END light render.
            /////////////////////////////////////////////////////////

            if (_ssao.Enabled)
            {
                /////////////////////////////////////////////////////
                // START ssao render.
                debugTimings.StartSsaoRenderSw();
                _ssao.ComputeSSAO(this, _device);
                debugTimings.EndSsaoRenderSw();
                // END ssao render.
                /////////////////////////////////////////////////////
            }

            if (_ssao.Enabled && _renderedTarget == RendererImpl.RenderedTarget.SsaoRT)
            {
                // We always render the final SSAO to the 0th render target.
                return _ssao.GetRenderingTargets(this)[RenderingSystem.RendererImpl.SSAO.RT_0_INDEX];
            }

            ///////////////////////////////////////////////////
            // BEGIN Shading reconstruction.
            debugTimings.StartReconstructShadingSw();
            // Reconstruct each object shading, using the light texture as input (and another specific parameters too).
            _device.SetRenderTarget(_outputTexture);
            _device.Clear(ClearOptions.DepthBuffer | ClearOptions.Stencil, Color.Black, 1.0f, 0);
            _device.DepthStencilState = DepthStencilState.Default;
            _device.BlendState = BlendState.Opaque;
            _device.RasterizerState = RasterizerState.CullCounterClockwise;

            if (_renderedTarget == RendererImpl.RenderedTarget.LightRT)
                return _lightBuffer;
            else if (_renderedTarget == RendererImpl.RenderedTarget.LightSpecRT)
                return _lightSpecularBuffer;

            // Reconstruct the shading, using the already culled list.
            ReconstructShading(camera);
            // Render objects that doesn't need the lightbuffer information, such as skyboxes, pure reflective meshes, etc
            DrawOpaqueObjects(camera);
            // Draw the water planes.
            DrawWater(camera, gameTime, renderController);
            // The objects with transparency.
            DrawBlendObjects(camera);

            // Draw SSAO texture. It's not correct to do it here, because ideally the SSAO should affect only
            // the ambient light, but it looks good this way.
            if (_ssao.Enabled)
                _ssao.FinalMix(this, _device);

            // Unbind our final buffer and return it.
            _device.SetRenderTarget(null);

            debugTimings.EndReconstructShadingSw();
            // END Reconstruct shadings.
            /////////////////////////////////////////////////////

            return _outputTexture;
        }

        /// <summary>
        /// Practically the same thing as RenderScene(...).
        /// Although this should be used in the case of a render to a texture which will be displayed with an effect.
        /// Basically not an ordianary scene render.
        /// Final effect stuff is excluded.
        /// Used with water and stuff like that.
        /// </summary>
        /// <param name="camera">Camera to render from the perspective of. (Typically not scene camera)</param>
        /// <param name="renderController">Render data.</param>
        /// <param name="renderTarget">The render target to render to.</param>
        /// <param name="clipPlane">The clip plane to use.</param>
        /// <returns>The final rendered scene.</returns>
        public RenderTarget2D RenderSceneEffectTexture(ICamera camera, RenderController renderController, RenderTarget2D renderTarget, Plane? clipPlane = null)
        {
            _depthDownsampledThisFrame = false;
            p_camera = camera;

            //compute the frustum corners for this camera
            ComputeFrustumCorners(camera);

            //this resets the free shadow maps
            _shadowRenderer.InitFrame();

            _visibleLights.Clear();
            renderController.GetVisibleLights(camera.Frustum, _visibleLights);
            //sort lights, choose the shadow casters
            SortLights(camera);
            SelectShadowCasters();

            //generate all shadow maps
            GenerateShadows(camera, renderController);

            //first of all, we must bind our GBuffer and reset all states
            _device.SetRenderTargets(_gBufferBinding);

            _device.Clear(ClearOptions.DepthBuffer | ClearOptions.Stencil, Color.Black, 1.0f, 0);
            _device.BlendState = BlendState.Opaque;
            _device.DepthStencilState = DepthStencilState.None;
            _device.RasterizerState = RasterizerState.CullNone;

            //bind the effect that outputs the default GBuffer values
            _clearGBuffer.CurrentTechnique.Passes[0].Apply();
            //draw a full screen quad for clearing our GBuffer
            _quadRenderer.RenderQuad(_device, -Vector2.One, Vector2.One);

            _device.DepthStencilState = DepthStencilState.Default;
            _device.RasterizerState = RasterizerState.CullCounterClockwise;

            //select the visible meshes
            CullVisibleMeshes(camera, renderController);

            //select the visible particle systems
            CullVisibleParticleSystems(camera, renderController);

            //now, render them to the G-Buffer
            if (clipPlane == null)
                RenderToGbuffer(camera);
            else
                RenderToGbuffer(camera, clipPlane.Value);

            //resolve our GBuffer and render the lights
            //clear the light buffer with black
            _device.SetRenderTargets(_lightAccumBinding);
            //dont be fooled by Color.Black, as its alpha is 255 (or 1.0f)
            _device.Clear(new Color(0, 0, 0, 0));

            //dont use depth/stencil test...we dont have a depth buffer, anyway
            _device.DepthStencilState = DepthStencilState.None;
            _device.RasterizerState = RasterizerState.CullCounterClockwise;

            //draw using additive blending.
            //At first I was using BlendState.additive, but it seems to use alpha channel for modulation,
            //and as we use alpha channel as the specular intensity, we have to create our own blend state here

            _device.BlendState = _lightAddBlendState;

            RenderLights(camera);

            //reconstruct each object shading, using the light texture as input (and another specific parameters too)
            _device.SetRenderTarget(renderTarget);
            _device.Clear(ClearOptions.DepthBuffer | ClearOptions.Stencil, Color.Black, 1.0f, 0);
            _device.DepthStencilState = DepthStencilState.Default;
            _device.BlendState = BlendState.Opaque;
            _device.RasterizerState = RasterizerState.CullCounterClockwise;

            //reconstruct the shading, using the already culled list
            if (clipPlane == null)
                ReconstructShading(camera);
            else
                ReconstructShading(camera, clipPlane.Value);
            //render objects that doesn't need the lightbuffer information, such as skyboxes, pure reflective meshes, etc
            DrawOpaqueObjects(camera);
            //draw objects with transparency
            DrawBlendObjects(camera);

            //unbind our final buffer and return it
            _device.SetRenderTarget(null);

            return renderTarget;
        }

        /// <summary>
        /// Set the shadow save information.
        /// </summary>
        /// <param name="csmRes">The resolution of the cascade shadow map.</param>
        /// <param name="csmDiv">The number of cascade shadow map divisions.</param>
        /// <param name="numSpotShadows">The max number of spot light shadows.</param>
        /// <param name="numCsmShadows">The max number of cascade light shadows.</param>
        /// <param name="spotShadowRes">The resolution of the spot light shadows.</param>
        public void SetShadowInfo(int csmRes, int csmDiv, int numSpotShadows, int numCsmShadows, int spotShadowRes)
        {
            _shadowRenderer = new ShadowRenderer(GraphicsDevice, csmRes, csmDiv, numSpotShadows, numCsmShadows, spotShadowRes);
        }

        /// <summary>
        /// Called for every mesh created. Sets the GBuffer textures for the mesh rendering.
        /// </summary>
        /// <param name="subMesh">The sub mesh to set the GBuffer textures for the render effect.</param>
        public void SetupSubMesh(Mesh.SubMesh subMesh)
        {
            subMesh.RenderEffect.SetLightRT(_lightBuffer, _lightSpecularBuffer);

            subMesh.RenderEffect.SetLightBufferPixelSize(new Vector2(0.5f / _lightBuffer.Width, 0.5f / _lightBuffer.Height));
            subMesh.RenderEffect.SetDepthRT(_depthBuffer);
            subMesh.RenderEffect.SetNormalRT(_normalBuffer);
        }

        /// <summary>
        /// Unload all ummanaged data in reference to loaded resources.
        /// </summary>
        public void UnloadContent()
        {
            _ssao.Unload();
            _depthBuffer.Unload();
            _halfDepth.Unload();
            _normalBuffer.Unload();
            _lightBuffer.Unload();
            _lightSpecularBuffer.Unload();
            _outputTexture.Unload();
            _halfBuffer0.Unload();
            _halfBuffer1.Unload();
            _quarterBuffer0.Unload();
            _quarterBuffer1.Unload();
            _fullBuffer0.Unload();
            _fullBuffer1.Unload();
        }

        /// <summary>
        /// Get the frustum corners for the camera.
        /// Used later to get the pixel position using the depth value.
        /// </summary>
        private void ComputeFrustumCorners(ICamera camera)
        {
            camera.Frustum.GetCorners(_cornersWorldSpace);
            Matrix matView = camera.View; //this is the inverse of our camera transform
            Vector3.Transform(_cornersWorldSpace, ref matView, _cornersViewSpace); //put the frustum into view space
            for (int i = 0; i < 4; i++) //take only the 4 farthest points
            {
                _currentFrustumCorners[i] = _cornersViewSpace[i + 4];
            }
            Vector3 temp = _currentFrustumCorners[3];
            _currentFrustumCorners[3] = _currentFrustumCorners[2];
            _currentFrustumCorners[2] = temp;
        }

        /// <summary>
        /// Create the render targets associated with the GBuffer.
        /// </summary>
        private void CreateGBuffer()
        {
            //One of our premises is to do not use the PRESERVE CONTENTS flags,
            //that is supposed to be more expensive than DISCARD CONTENT.
            //We use a floating point (32bit) buffer for Z values, although our HW use only 24bits.
            //We could use some packing and use a 24bit buffer too, but lets start simpler
            _depthBuffer = new RenderTarget2D(_device, i_width, i_height, false, SurfaceFormat.Single,
                                              DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            //the downsampled depth buffer must have the same format as the main one
            _halfDepth = new RenderTarget2D(_device, i_width / 2, i_height / 2, false,
                                              SurfaceFormat.Single, DepthFormat.None, 0,
                                              RenderTargetUsage.DiscardContents);

            //Our normal buffer stores encoded view-space normal into RG (10bit each) and the specular power in B.
            //Some engines encode the specular power with some log or ln functions. We will output
            //only the normal texture's alpha channel multiplied by a const value (100),
            //so we have specular power in the range [1..100].
            //Currently, A is not used (2bit).
            _normalBuffer = new RenderTarget2D(_device, i_width, i_height, false, SurfaceFormat.Rgba1010102,
                                               DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents);

            //This buffer stores all the "pure" lighting on the scene, no albedo applied to it. We use an floating
            //point format to allow us "overbright" some areas. Read the blog for more information. We use a depth buffer
            //to optimize light rendering.
            _lightBuffer = new RenderTarget2D(_device, i_width, i_height, false, SurfaceFormat.HdrBlendable,
                                              DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents);

            //we need a separate texture for the specular, since the xbox doesnt allow a RGBA64 buffer
            _lightSpecularBuffer = new RenderTarget2D(_device, i_width, i_height, false,
                                             SurfaceFormat.HdrBlendable, DepthFormat.None, 0,
                                             RenderTargetUsage.DiscardContents);

            //We need another depth here because we need to render all objects again, to reconstruct their shading
            //using our light texture.
            _outputTexture = new RenderTarget2D(_device, i_width, i_height, false, SurfaceFormat.Color,
                                                DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents);

            _fullBuffer0 = new RenderTarget2D(_device, i_width, i_height, false,
                                              SurfaceFormat.Color, DepthFormat.None, 0,
                                              RenderTargetUsage.DiscardContents);
            _fullBuffer1 = new RenderTarget2D(_device, i_width, i_height, false,
                                              SurfaceFormat.Color, DepthFormat.None, 0,
                                              RenderTargetUsage.DiscardContents);

            const int halfRes = 2;
            _halfBuffer0 = new RenderTarget2D(_device, i_width / halfRes, i_height / halfRes, false,
                                              SurfaceFormat.Color, DepthFormat.None, 0,
                                              RenderTargetUsage.DiscardContents);
            _halfBuffer1 = new RenderTarget2D(_device, i_width / halfRes, i_height / halfRes, false,
                                              SurfaceFormat.Color, DepthFormat.None, 0,
                                              RenderTargetUsage.DiscardContents);

            const int quarterRes = 4;
            _quarterBuffer0 = new RenderTarget2D(_device, i_width / quarterRes, i_height / quarterRes, false,
                                                SurfaceFormat.Color, DepthFormat.None, 0,
                                                RenderTargetUsage.DiscardContents);
            _quarterBuffer1 = new RenderTarget2D(_device, i_width / quarterRes, i_height / quarterRes, false,
                                                SurfaceFormat.Color, DepthFormat.None, 0,
                                                RenderTargetUsage.DiscardContents);

            _gBufferBinding[0] = new RenderTargetBinding(_normalBuffer);
            _gBufferBinding[1] = new RenderTargetBinding(_depthBuffer);

            _lightAccumBinding[0] = new RenderTargetBinding(_lightBuffer);
            _lightAccumBinding[1] = new RenderTargetBinding(_lightSpecularBuffer);
        }

        /// <summary>
        /// Cull the not visible meshes.
        /// Visible meshes placed in _visibleMeshes.
        /// </summary>
        /// <param name="cam">The rendering camera.</param>
        /// <param name="renderController">The render data.</param>
        private void CullVisibleMeshes(ICamera cam, RenderController renderController)
        {
            for (int index = 0; index < _visibleMeshes.Length; index++)
            {
                List<Mesh.SubMesh> visibleMesh = _visibleMeshes[index];
                visibleMesh.Clear();
            }

            renderController.GetVisibleMeshes(cam.Frustum, _visibleMeshes);
        }

        /// <summary>
        /// Culls the particle systems which aren't visible.
        /// Puts visible particle systems in the _visibleParticleSystemEntries list.
        /// Sorts the particle system according to distance to camera position.
        /// </summary>
        /// <param name="cam">Rendering camera.</param>
        /// <param name="renderController">The render data.</param>
        private void CullVisibleParticleSystems(ICamera cam, RenderController renderController)
        {
            _visibleParticleSystemEntries.Clear();
            _visibleParticleSystems.Clear();
            Vector3 translation = cam.Transform.Translation;
            renderController.GetVisibleParticleSystems(cam.Frustum, _visibleParticleSystems);

            // Find visible particle systems).
            for (int index = 0; index < _visibleParticleSystems.Count; index++)
            {
                ParticleSystem particleSystem = _visibleParticleSystems[index];
                if (cam.Frustum.Intersects(particleSystem.GlobalBoundingBox))
                {
                    ParticleSystemEntry entry = new ParticleSystemEntry();
                    entry.particleSystem = particleSystem;
                    entry.sqrDistanceToCam = Vector3.DistanceSquared(translation,
                                                                     particleSystem.GlobalTransform.Translation);
                    _visibleParticleSystemEntries.Add(entry);
                }
            }

            // Sort by distance, from furthest to closest.
            _visibleParticleSystemEntries.Sort(delegate(ParticleSystemEntry p1, ParticleSystemEntry p2)
            {
                return (int)(p2.sqrDistanceToCam - p1.sqrDistanceToCam);
            });
        }

        /// <summary>
        /// Draw the objects which use the blending effect.
        /// </summary>
        /// <param name="cam">The rendering camera.</param>
        private void DrawBlendObjects(ICamera cam)
        {
            _device.DepthStencilState = DepthStencilState.DepthRead;
            _device.BlendState = BlendState.Additive;

            List<Mesh.SubMesh> meshes = _visibleMeshes[(int)MeshData.RenderQueueType.Blend];
            for (int index = 0; index < meshes.Count; index++)
            {
                Mesh.SubMesh visibleMesh = meshes[index];
                visibleMesh.SimpleRender(cam, _device);
            }

            // Reset states, since the custom shaders could have overriden them.
            _device.DepthStencilState = DepthStencilState.DepthRead;
            _device.BlendState = BlendState.Additive;
            _device.BlendFactor = Color.White;
            _device.RasterizerState = RasterizerState.CullCounterClockwise;

            Matrix proj = cam.Proj;
            Matrix view = cam.View;
            for (int index = 0; index < _visibleParticleSystemEntries.Count; index++)
            {
                ParticleSystem particleSystem = _visibleParticleSystemEntries[index].particleSystem;

                particleSystem.SetCamera(view, proj);
                particleSystem.Draw();
            }
        }

        /// <summary>
        /// Draw all the straight up opaque objects.
        /// </summary>
        /// <param name="cam">Rendering camera.</param>
        private void DrawOpaqueObjects(ICamera cam)
        {
            List<Mesh.SubMesh> meshes = _visibleMeshes[(int)MeshData.RenderQueueType.SkipGbuffer];
            for (int index = 0; index < meshes.Count; index++)
            {
                Mesh.SubMesh visibleMesh = meshes[index];
                visibleMesh.SimpleRender(cam, _device);
            }
        }

        /// <summary>
        /// Draw any water effects which are active.
        /// Needs to be done seperately of regular render.
        /// </summary>
        /// <param name="cam">The rendering camera.</param>
        /// <param name="gameTime"></param>
        /// <param name="renderController">Render data.</param>
        private void DrawWater(ICamera cam, GameTime gameTime, RenderController renderController)
        {
            IEnumerable<WaterPlane> waterPlanes = from wp in renderController.Meshes
                                                  where wp is WaterPlane
                                                  select wp as WaterPlane;

            foreach (WaterPlane waterPlane in waterPlanes)
            {
                waterPlane.SetEffectData(_device, cam, gameTime);
                WaterPlane.SetVertexBuffer(_device);
                WaterPlane.DrawVerticies(_device);
            }
        }

        /// <summary>
        /// Call before scene render.
        /// Constructs the water's refraction scene render water texture.
        /// Doesn't actually draw the water to the scene.
        /// EVERY WATER PLANE DOES A FULL RENDER PASS.
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="renderWorld"></param>
        private void DrawWaterPreRender(ICamera camera, RenderController renderWorld)
        {
            IEnumerable<WaterPlane> waterPlanes = from wp in renderWorld.Meshes
                                                  where wp is WaterPlane
                                                  select wp as WaterPlane;

            foreach (WaterPlane waterPlane in waterPlanes)
            {
                waterPlane.BuildSceneRenderTextures(this, camera, renderWorld);
            }
        }

        /// <summary>
        /// Create the downsampled depth buffer notifying the renderer the downsampled depth has been created.
        /// </summary>
        private void GenerateDownsampleDepth()
        {
            _downsampleDepth.RenderEffect(this, _device);
            _depthDownsampledThisFrame = true;
        }

        /// <summary>
        /// Create the shadow maps.
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="renderWorld"></param>
        private void GenerateShadows(ICamera camera, RenderController renderWorld)
        {
            // We almost blur the shadows afterwards, there is no time to implement.
            for (int index = 0; index < _lightShadowCasters.Count; index++)
            {
                LightEntry light = _lightShadowCasters[index];
                // Only spot and directional
                if (light.Light.LightType == GeneralLight.Type.Spot)
                {
                    _shadowRenderer.GenerateShadowTextureSpotLight(this, renderWorld, light.Light, light.SpotShadowMap);
                    // Didn't have time to implement blured shadow maps.
                    //light.spotShadowMap.Texture = _blur.DrawEffect(_device, light.spotShadowMap.Texture);
                }
                else if (light.Light.LightType == GeneralLight.Type.Directional)
                {
                    _shadowRenderer.GenerateShadowTextureDirectionalLight(this, renderWorld, light.Light, light.CascadeShadowMap, camera);
                    // I tried, I really did.
                    //light.cascadeShadowMap.Texture = _blur.DrawEffect(_device, light.cascadeShadowMap.Texture);
                }
            }
        }

        /// <summary>
        /// Load all of the shaders used.
        /// </summary>
        private void LoadShaders()
        {
            _clearGBuffer = _contentManager.Load<Effect>("shaders/ClearGBuffer");
            _lighting = _contentManager.Load<Effect>("shaders/LightingLpp");
            _reconstructZBuffer = _contentManager.Load<Effect>("shaders/ReconstructDepth");
        }

        /// <summary>
        /// Reconstruct the shading.
        /// (I like to think of it as giving everything color)
        /// </summary>
        /// <param name="cam">The rendering camera.</param>
        private void ReconstructShading(ICamera cam)
        {
            List<Mesh.SubMesh> meshes = _visibleMeshes[(int)MeshData.RenderQueueType.Default];
            for (int index = 0; index < meshes.Count; index++)
            {
                Mesh.SubMesh visibleMesh = meshes[index];
                visibleMesh.ReconstructShading(cam, _device);
            }
        }

        /// <summary>
        /// Reconstruct the shading.
        /// (I like to think of it as giving everything color)
        /// Also use a clip plane in the rendering.
        /// </summary>
        /// <param name="cam">The rendering camera.</param>
        private void ReconstructShading(ICamera cam, Plane clipPlane)
        {
            List<Mesh.SubMesh> meshes = _visibleMeshes[(int)MeshData.RenderQueueType.Default];
            for (int index = 0; index < meshes.Count; index++)
            {
                Mesh.SubMesh visibleMesh = meshes[index];
                visibleMesh.ReconstructShading(cam, _device, clipPlane);
            }
        }

        /// <summary>
        /// Render to the Z buffer.
        /// </summary>
        /// <param name="cam"></param>
        private void ReconstructZBuffer(ICamera cam)
        {
            //bind effect
            _reconstructZBuffer.Parameters["GBufferPixelSize"].SetValue(new Vector2(0.5f / i_width, 0.5f / i_height));
            _reconstructZBuffer.Parameters["DepthBuffer"].SetValue(_depthBuffer);
            _reconstructZBuffer.Parameters["FarClip"].SetValue(cam.FarClip);
            //our projection matrix is almost all 0s, we just need these 2 values to restoure our Z-buffer from our linear depth buffer
            _reconstructZBuffer.Parameters["ProjectionValues"].SetValue(new Vector2(cam.Proj.M33, cam.Proj.M43));
            _reconstructZBuffer.CurrentTechnique.Passes[0].Apply();

            //we need to always write to z-buffer

            //store previous state
            BlendState oldBlendState = _device.BlendState;

            _device.DepthStencilState = _depthStateReconstructZ;

            _quadRenderer.RenderQuad(_device, -Vector2.One, Vector2.One);

            _device.DepthStencilState = _depthStateDrawLights;
            _device.BlendState = oldBlendState;
        }

        /// <summary>
        /// Render the lights to the light buffers.
        /// </summary>
        /// <param name="cam">The rendering camera.</param>
        private void RenderLights(ICamera cam)
        {
            _lighting.Parameters["GBufferPixelSize"].SetValue(new Vector2(0.5f / i_width, 0.5f / i_height));
            _lighting.Parameters["DepthBuffer"].SetValue(_depthBuffer);
            _lighting.Parameters["NormalBuffer"].SetValue(_normalBuffer);

            //just comment this line if you dont want to reconstruct the zbuffer
            ReconstructZBuffer(cam);

            _lighting.Parameters["TanAspect"].SetValue(new Vector2(cam.TanFovy * cam.Aspect, -cam.TanFovy));

            for (int i = 0; i < _lightEntries.Count; i++)
            {
                LightEntry lightEntry = _lightEntries[i];
                GeneralLight light = lightEntry.Light;

                //convert light position into viewspace
                Vector3 viewSpaceLPos = Vector3.Transform(light.Transform.Translation,
                                                            cam.View);
                Vector3 viewSpaceLDir = Vector3.TransformNormal(Vector3.Normalize(light.Transform.Backward),
                                                            cam.View);
                _lighting.Parameters["LightPosition"].SetValue(viewSpaceLPos);
                _lighting.Parameters["LightDir"].SetValue(viewSpaceLDir);
                Vector4 lightColor = light.Color.ToVector4() * light.DiffuseIntensity;
                lightColor.W = light.SpecularIntensity;
                _lighting.Parameters["LightColor"].SetValue(lightColor);
                float invRadiusSqr = 1.0f / (light.Radius * light.Radius);
                _lighting.Parameters["InvLightRadiusSqr"].SetValue(invRadiusSqr);
                _lighting.Parameters["FarClip"].SetValue(cam.FarClip);

                switch (light.LightType)
                {
                    case GeneralLight.Type.Point:
                    case GeneralLight.Type.Spot:
                        if (light.LightType == GeneralLight.Type.Point)
                        {
                            //check if the light touches the near plane
                            BoundingSphere boundingSphereExpanded = light.BoundingSphere;
                            boundingSphereExpanded.Radius *= 1.375f; //expand it a little, because our mesh is not a perfect sphere
                            PlaneIntersectionType planeIntersectionType;
                            cam.Frustum.Near.Intersects(ref boundingSphereExpanded, out planeIntersectionType);
                            if (planeIntersectionType != PlaneIntersectionType.Back)
                            {
                                _device.RasterizerState = RasterizerState.CullCounterClockwise;
                                _device.DepthStencilState = _ccwDepthState;
                            }
                            else
                            {
                                _device.RasterizerState = RasterizerState.CullClockwise;
                                _device.DepthStencilState = _cwDepthState;
                            }

                            Matrix lightMatrix = Matrix.CreateScale(light.Radius);
                            lightMatrix.Translation = light.BoundingSphere.Center;

                            _lighting.Parameters["WorldViewProjection"].SetValue(lightMatrix *
                                                                                 cam.ViewProj);

                            _lighting.CurrentTechnique = _lighting.Techniques[1];
                            _lighting.CurrentTechnique.Passes[0].Apply();

                            _sphereRenderer.BindMesh(_device);
                            _sphereRenderer.RenderMesh(_device);
                        }
                        else
                        {
                            //check if the light touches the far plane

                            Plane near = cam.Frustum.Near;
                            near.D += 3; //give some room because we dont use a perfect-fit mesh for the spot light
                            PlaneIntersectionType planeIntersectionType = near.Intersects(light.Frustum);
                            if (planeIntersectionType != PlaneIntersectionType.Back)
                            {
                                _device.RasterizerState = RasterizerState.CullCounterClockwise;
                                _device.DepthStencilState = _ccwDepthState;
                            }
                            else
                            {
                                _device.RasterizerState = RasterizerState.CullClockwise;
                                _device.DepthStencilState = _cwDepthState;
                            }

                            float tan = (float)Math.Tan(MathHelper.ToRadians(light.SpotConeAngle));
                            Matrix lightMatrix = Matrix.CreateScale(light.Radius * tan, light.Radius * tan, light.Radius);

                            lightMatrix = lightMatrix * light.Transform;

                            _lighting.Parameters["WorldViewProjection"].SetValue(lightMatrix *
                                                                                 cam.ViewProj);
                            float cosSpotAngle = (float)Math.Cos(MathHelper.ToRadians(light.SpotConeAngle));
                            _lighting.Parameters["SpotAngle"].SetValue(cosSpotAngle);
                            _lighting.Parameters["SpotExponent"].SetValue(light.SpotExponent / (1 - cosSpotAngle));
                            if (lightEntry.CastShadows)
                            {
                                _lighting.CurrentTechnique = _lighting.Techniques[4];
                                _lighting.Parameters["MatLightViewProjSpot"].SetValue(lightEntry.SpotShadowMap.LightViewProjection);
                                _lighting.Parameters["DepthBias"].SetValue(light.ShadowDepthBias);
                                Vector2 shadowMapPixelSize = new Vector2(0.5f / lightEntry.SpotShadowMap.Texture.Width, 0.5f / lightEntry.SpotShadowMap.Texture.Height);
                                _lighting.Parameters["ShadowMapPixelSize"].SetValue(shadowMapPixelSize);
                                _lighting.Parameters["ShadowMapSize"].SetValue(new Vector2(lightEntry.SpotShadowMap.Texture.Width, lightEntry.SpotShadowMap.Texture.Height));
                                _lighting.Parameters["ShadowMap"].SetValue(lightEntry.SpotShadowMap.Texture);
                                _lighting.Parameters["CameraTransform"].SetValue(cam.Transform);
                            }
                            else
                            {
                                _lighting.CurrentTechnique = _lighting.Techniques[3];
                            }

                            _lighting.CurrentTechnique.Passes[0].Apply();

                            _spotRenderer.BindMesh(_device);
                            _spotRenderer.RenderMesh(_device);
                        }

                        break;

                    case GeneralLight.Type.Directional:

                        _device.DepthStencilState = _directionalDepthState;
                        _device.RasterizerState = RasterizerState.CullNone;
                        ApplyFrustumCorners(_lighting, -Vector2.One, Vector2.One);
                        if (lightEntry.CastShadows)
                        {
                            _lighting.CurrentTechnique = _lighting.Techniques[5];

                            _lighting.Parameters["DepthBias"].SetValue(light.ShadowDepthBias);
                            Vector2 shadowMapPixelSize = new Vector2(0.5f / lightEntry.CascadeShadowMap.Texture.Width, 0.5f / lightEntry.CascadeShadowMap.Texture.Height);
                            _lighting.Parameters["ShadowMapPixelSize"].SetValue(shadowMapPixelSize);
                            _lighting.Parameters["ShadowMapSize"].SetValue(new Vector2(lightEntry.CascadeShadowMap.Texture.Width, lightEntry.CascadeShadowMap.Texture.Height));
                            _lighting.Parameters["ShadowMap"].SetValue(lightEntry.CascadeShadowMap.Texture);
                            _lighting.Parameters["CameraTransform"].SetValue(cam.Transform);

                            _lighting.Parameters["ClipPlanes"].SetValue(lightEntry.CascadeShadowMap.LightClipPlanes);
                            _lighting.Parameters["MatLightViewProj"].SetValue(lightEntry.CascadeShadowMap.LightViewProjectionMatrices);

                            Vector3 cascadeDistances = Vector3.Zero;
                            cascadeDistances.X = lightEntry.CascadeShadowMap.LightClipPlanes[0].X;
                            cascadeDistances.Y = lightEntry.CascadeShadowMap.LightClipPlanes[1].X;
                            cascadeDistances.Z = lightEntry.CascadeShadowMap.LightClipPlanes[2].X;
                            _lighting.Parameters["CascadeDistances"].SetValue(cascadeDistances);
                        }
                        else
                        {
                            _lighting.CurrentTechnique = _lighting.Techniques[2];
                        }
                        _lighting.CurrentTechnique.Passes[0].Apply();
                        _quadRenderer.RenderQuad(_device, -Vector2.One, Vector2.One);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            _device.RasterizerState = RasterizerState.CullCounterClockwise;
        }

        /// <summary>
        /// Render all of the geometry data to the GBuffer using a clip plane.
        /// </summary>
        /// <param name="cam">Rendering camera.</param>
        /// <param name="clipPlane"></param>
        private void RenderToGbuffer(ICamera cam, Plane clipPlane)
        {
            List<Mesh.SubMesh> meshes = _visibleMeshes[(int)MeshData.RenderQueueType.Default];
            foreach (Mesh.SubMesh mesh in meshes)
            {
                mesh.RenderToGBuffer(cam, _device, clipPlane);
            }
        }

        /// <summary>
        /// Render all of the geometry data to the GBuffer.
        /// </summary>
        /// <param name="cam">Rendering camera.</param>
        private void RenderToGbuffer(ICamera cam)
        {
            List<Mesh.SubMesh> meshes = _visibleMeshes[(int)MeshData.RenderQueueType.Default];
            foreach (Mesh.SubMesh mesh in meshes)
            {
                mesh.RenderToGBuffer(cam, _device);
            }
        }

        /// <summary>
        /// Just get all of the lights which cast shadows and give them the appropriate entry.
        /// Added to _lightShadowCasters.
        /// </summary>
        private void SelectShadowCasters()
        {
            _lightShadowCasters.Clear();

            for (int i = 0; i < _lightEntries.Count; i++)
            {
                LightEntry entry = _lightEntries[i];
                if (_lightEntries[i].Light.CastShadows)
                {
                    if (entry.Light.LightType == GeneralLight.Type.Spot)
                    {
                        entry.SpotShadowMap = _shadowRenderer.GetFreeSpotShadowMap();
                        entry.CastShadows = entry.SpotShadowMap != null;
                        if (entry.CastShadows)
                        {
                            _lightShadowCasters.Add(entry);
                        }
                    }
                    else if (entry.Light.LightType == GeneralLight.Type.Directional)
                    {
                        entry.CascadeShadowMap = _shadowRenderer.GetFreeCascadeShadowMap();
                        entry.CastShadows = entry.CascadeShadowMap != null;
                        if (entry.CastShadows)
                        {
                            _lightShadowCasters.Add(entry);
                        }
                    }
                }
                //assign it back, since it's a struct
                _lightEntries[i] = entry;
            }
        }

        /// <summary>
        /// Sort the lights according to the an direct relation to the light radius and an inverse relation to the distance of the camera.
        /// Also creates the light entries.
        /// </summary>
        /// <param name="cam">The rendering camera.</param>
        private void SortLights(ICamera cam)
        {
            _lightEntries.Clear();

            Vector3 camPos = cam.Transform.Translation;
            for (int index = 0; index < _visibleLights.Count; index++)
            {
                LightEntry lightEntry = new LightEntry();
                lightEntry.Light = _visibleLights[index];
                lightEntry.SqrDistanceToCam = Math.Max(1, Vector3.Distance(lightEntry.Light.Transform.Translation,
                                                            camPos));
                //compute a value to determine light order
                lightEntry.Priority = 1000 * lightEntry.Light.Radius / Math.Max(1, lightEntry.SqrDistanceToCam);
                _lightEntries.Add(lightEntry);
            }

            _lightEntries.Sort(delegate(LightEntry p1, LightEntry p2)
            {
                return (int)(p2.Priority - p1.Priority);
            });
        }

        /// <summary>
        /// Contains whatever information a light in the scene would need.
        /// Has the possibility for both PCF and Cascade shadow maps.
        /// </summary>
        private struct LightEntry
        {
            /// <summary>
            /// Contains the information for Cascade shadow mapping.
            /// </summary>
            public ShadowRenderer.CascadeShadowMapEntry CascadeShadowMap;

            public bool CastShadows;
            public GeneralLight Light;

            /// <summary>
            /// The priority in deciding the order of the light render.
            /// Based on the distance to the camera.
            /// </summary>
            public float Priority;

            /// <summary>
            /// Contains the information for PCF shadow mapping.
            /// Can be not used.
            /// </summary>
            public ShadowRenderer.RegShadowMapEntry SpotShadowMap;

            public float SqrDistanceToCam;
        }

        /// <summary>
        /// Gives information for a particle system and the order in which it should be rendered.
        /// </summary>
        private struct ParticleSystemEntry
        {
            public ParticleSystem particleSystem;
            public float sqrDistanceToCam;
        }
    }
}