#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using RenderingSystem.Graphics;
using RenderingSystem.RendererImpl;

namespace RenderingSystem
{
    /// <summary>
    /// Representations of different fractions of the back buffer size.
    /// Corresponds to render targets of renderer.
    /// </summary>
    public enum RenderTargetSize { Full, Half, Quarter };

    /// <summary>
    /// The save data for the deferred renderer.
    /// </summary>
    [Serializable]
    public struct RendererSaveData
    {
        /// <summary>
        /// Ambient color used in the SSAO effect.
        /// </summary>
        public Vector4 AmbientColor;

        /// <summary>
        /// SSAO bias.
        /// </summary>
        public float Bias;

        /// <summary>
        /// The bloom save datas.
        /// (Multiple effects can be applied.)
        /// </summary>
        public BloomSaveData[] BloomSaveData;

        /// <summary>
        /// The blur count of the SSAO effect.
        /// </summary>
        public int BlurCount;

        /// <summary>
        /// The number of cascade shadow map divisions.
        /// </summary>
        public int CsmDiv;

        /// <summary>
        /// The resolution of the cascade shadow maps.
        /// </summary>
        public int CsmRes;

        /// <summary>
        /// The intensity of the SSAO effect.
        /// </summary>
        public float Intensity;

        /// <summary>
        /// The light shaft save datas.
        /// (Multiple effects can be applied).
        /// </summary>
        public LightShaftSaveData[] LightShaftSaveData;

        /// <summary>
        /// The max number of cascade shadow maps.
        /// </summary>
        public int MaxCsmMaps;

        /// <summary>
        /// The max radius of the SSAO radial blur.
        /// </summary>
        public float MaxRadius;

        /// <summary>
        /// The max number of spot light (PercentageCloserFilter) shadows in the scene.
        /// </summary>
        public int MaxSpotShadows;

        /// <summary>
        /// The radius of the SSAO radial blur.
        /// </summary>
        public float Radius;

        /// <summary>
        /// The random tile of the SSAO used in calculating the random vector sampling.
        /// </summary>
        public float RandomTile;

        /// <summary>
        /// Whether the skymap should be rendered.
        /// </summary>
        public bool RenderSkymap;

        /// <summary>
        /// The resolution of the spot shadow resolutions.
        /// </summary>
        public int SpotShadowRes;

        /// <summary>
        /// Whether the SSAO effect should be used.
        /// </summary>
        public bool UseSSAO;
    }

    /// <summary>
    /// Save data for the bloom effect.
    /// Serilized off to the level XML save file.
    /// </summary>
    public class BloomSaveData
    {
        public float BaseIntensity;
        public float BaseSaturation;
        public float BloomIntensity;
        public float BloomSaturation;
        public float BloomThreshold;
        public float BlurAmount;
        public string Name;
        public int Order;
    }

    /// <summary>
    /// Save data for the light shaft effect.
    /// Serilized off to the level XML save file.
    /// </summary>
    public class LightShaftSaveData
    {
        public float Blend;
        public Color ColorBalance;
        public float Contrast;
        public float Decay;
        public float Exposure;
        public int Order;

        public RenderTargetSize RTSize;
        public float Saturation;
        public float Scale;
        public Color ShaftTint;
        public float Spread;
    }

    /// <summary>
    /// The implmenetation of a deferred renderer.
    /// The renderer which should be accessed from external references.
    /// </summary>
    public class RendererAccess : GameRenderer
    {
        private TextureCube _ambientCube;
        private RenderingSystem.RendererImpl.BoundingBoxRenderer _boundingBoxRenderer;
        private RenderingSystem.RendererImpl.LineRenderer _lineRenderer;
        private PPEMgr _ppeMgr;
        private RenderingSystem.RendererImpl.QuadRenderer _quadRenderer;
        private RenderingSystem.RendererImpl.RenderController _renderContoller;
        private bool _renderCullBBs = false;
        private RenderingSystem.RendererImpl.Renderer _renderer;
        private bool _renderGBuffer = false;
        private bool _renderLines = false;
        private bool _renderPhysicsBBs = false;
        private bool _renderQuads = false;
        private bool _renderText = false;
        private RenderingSystem.RendererImpl.Mesh _skyboxMesh;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;
        private bool b_renderSkymap = true;
        private double d_drawMs = 0;
        private string s_defaultCubemapFilename = "BlueSkyAmbient";
        private Vector4 v_ambientColor = new Vector4(7f, 7f, 7f, 0f);

        public RendererAccess(GraphicsDevice device)
            : base(device)
        {
            _renderContoller = new RendererImpl.RenderController();
            Graphics.Forms.WindowsFormHelper.Init();
        }

        /// <summary>
        /// The ambient color used in SSAO.
        /// </summary>
        public Vector4 AmbientColor
        {
            set { v_ambientColor = value; }
            get { return v_ambientColor; }
        }

        /// <summary>
        /// Whether the bounding boxes used in culling detection should be rendered.
        /// Debugging purposes only.
        /// </summary>
        public bool RenderCullingBoundingBoxes
        {
            set { _renderCullBBs = value; }
            get { return _renderCullBBs; }
        }

        /// <summary>
        /// The final scene render target.
        /// </summary>
        public RenderedTarget RenderedTarget
        {
            get { return _renderer.RenderedTarget; }
            set { _renderer.RenderedTarget = value; }
        }

        /// <summary>
        /// Whether the GBuffer itself be rendered.
        /// </summary>
        public bool RenderGBuffer
        {
            set { _renderGBuffer = value; }
        }

        /// <summary>
        /// Whether lines should be rendered.
        /// </summary>
        public bool RenderLines
        {
            set { _renderLines = value; }
            get { return _renderLines; }
        }

        /// <summary>
        /// Whether the bounding boxes for physics objects should be rendered.
        /// Supplied by an external reference.
        /// Debugging purposes only.
        /// </summary>
        public bool RenderPhysicsBoundingBoxes
        {
            set { _renderPhysicsBBs = value; }
            get { return _renderPhysicsBBs; }
        }

        /// <summary>
        /// Whether quads should be rendered.
        /// </summary>
        public bool RenderQuads
        {
            set { _renderQuads = value; }
            get { return _renderQuads; }
        }

        /// <summary>
        /// Whether the skymap should be rendered.
        /// </summary>
        public bool RenderSkymap
        {
            get { return b_renderSkymap; }
            set { b_renderSkymap = value; }
        }

        /// <summary>
        /// Whether the text should be rendered.
        /// </summary>
        public bool RenderText
        {
            set { _renderText = value; }
            get { return _renderText; }
        }

        /// <summary>
        /// Whether the SSAO effect should be used.
        /// </summary>
        public bool UseSSAO
        {
            set { _renderer.SSAO.Enabled = value; }
            get { return _renderer.SSAO.Enabled; }
        }

        /// <summary>
        /// Create the editor form for the render settings.
        /// </summary>
        /// <param name="onDlgQuit"></param>
        public override void CreateRendererSettingsDlg(Action onDlgQuit)
        {
            _ppeMgr.CreatePostProcessMgrDlg(onDlgQuit);
        }

        /// <summary>
        /// Draw the entire scene.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="cam">User camera.</param>
        /// <param name="debugTimings"></param>
        public override void Draw(GameTime gameTime, ICamera cam, DebugTimings debugTimings)
        {
            _skyboxMesh.Transform = Matrix.CreateTranslation(cam.Position);
            List<RendererImpl.GeneralLight> visibleLights = new List<RendererImpl.GeneralLight>();
            _renderContoller.GetVisibleLights(cam.Frustum, visibleLights);

            _ppeMgr.SetSceneInfo(visibleLights, cam);

            DrawScene(cam, debugTimings, gameTime);
        }

        /// <summary>
        /// This version of the render doesn't support this.
        /// </summary>
        /// <returns></returns>
        public override FrameRenderData GetFrameRenderData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a post processing effect based on the post processing effect identifier.
        /// </summary>
        /// <param name="name">The identifier of the post processing effect to find.</param>
        /// <returns>The found post processing effect.</returns>
        public PostProcessingEffect GetPostProcessingEffect(string name)
        {
            return _ppeMgr.GetPostProcessingEffect(name);
        }

        /// <summary>
        /// Get the save data for serilization of the renderer.
        /// </summary>
        /// <returns></returns>
        public RendererSaveData GetSaveData()
        {
            RendererSaveData rsd;

            GetSSAORenderInfo(out rsd.RandomTile, out rsd.Radius, out rsd.MaxRadius, out rsd.Bias, out rsd.Intensity, out rsd.BlurCount);
            GetShadowInfo(out rsd.CsmRes, out rsd.CsmDiv, out rsd.MaxSpotShadows, out rsd.MaxCsmMaps, out rsd.SpotShadowRes);

            rsd.UseSSAO = UseSSAO;

            List<BloomSaveData> bloomSaveData = new List<BloomSaveData>();
            List<LightShaftSaveData> lightShaftSaveData = new List<LightShaftSaveData>();
            for (int i = 0; i < _ppeMgr.GetNumberOfDataElements(); ++i)
            {
                var ppe = _ppeMgr.GetDataElementAt(i);
                object saveData = ppe.GetSettingsSaveData();
                if (saveData is BloomSaveData)
                {
                    BloomSaveData bsd = saveData as BloomSaveData;
                    bsd.Order = i;
                    bloomSaveData.Add(bsd);
                }
                else if (saveData is LightShaftSaveData)
                {
                    LightShaftSaveData lssd = saveData as LightShaftSaveData;
                    lssd.Order = i;
                    lightShaftSaveData.Add(lssd);
                }
                else if (saveData == null)
                {
                    // There is no save data for this post processing effect.
                }
                else
                {
                    throw new ArgumentException("Unexpected save data of type " + saveData.GetType().ToString() + "!");
                }
            }

            rsd.BloomSaveData = bloomSaveData.ToArray();
            rsd.LightShaftSaveData = lightShaftSaveData.ToArray();

            rsd.AmbientColor = v_ambientColor;
            rsd.RenderSkymap = b_renderSkymap;

            return rsd;
        }

        /// <summary>
        /// Get the shadow information for saving.
        /// </summary>
        /// <param name="csmRes"></param>
        /// <param name="csmDiv"></param>
        /// <param name="numSpotShadows"></param>
        /// <param name="numCsmShadows"></param>
        /// <param name="spotShadowRes"></param>
        public void GetShadowInfo(out int csmRes, out int csmDiv, out int numSpotShadows, out int numCsmShadows, out int spotShadowRes)
        {
            _renderer.GetShadowInfo(out csmRes, out csmDiv, out numSpotShadows, out numCsmShadows, out spotShadowRes);
        }

        /// <summary>
        /// Get the SSAO information for saving.
        /// </summary>
        /// <param name="randomTile"></param>
        /// <param name="radius"></param>
        /// <param name="maxRadius"></param>
        /// <param name="bias"></param>
        /// <param name="intensity"></param>
        /// <param name="blurCount"></param>
        public void GetSSAORenderInfo(out float randomTile, out float radius, out float maxRadius, out float bias, out float intensity, out int blurCount)
        {
            randomTile = _renderer.SSAO.RandomTile;
            radius = _renderer.SSAO.Radius;
            maxRadius = _renderer.SSAO.MaxRadius;
            bias = _renderer.SSAO.Bias;
            intensity = _renderer.SSAO.Intensity;
            blurCount = _renderer.SSAO.BlurCount;
        }

        /// <summary>
        /// Load all of the resource related content.
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            PresentationParameters pp = _device.PresentationParameters;
            _renderer = new RendererImpl.Renderer(_device, content, pp.BackBufferWidth, pp.BackBufferHeight);

            _spriteBatch = new SpriteBatch(_device);

            _skyboxMesh = new RendererImpl.Mesh();
            Model skyboxModel = ResourceMgr.LoadModel("Models/skybox");
            _skyboxMesh.SetModel(skyboxModel);
            _skyboxMesh.SetMetadata(skyboxModel.Tag as MeshData);

            _boundingBoxRenderer = new RendererImpl.BoundingBoxRenderer();
            _lineRenderer = new RendererImpl.LineRenderer();
            _quadRenderer = new QuadRenderer();
            _quadRenderer.LoadContent(_device);

            _spriteFont = content.Load<SpriteFont>("defaultFont");

            _ambientCube = ResourceMgr.LoadTextureCube(s_defaultCubemapFilename);

            _ppeMgr = new PPEMgr(_spriteBatch, _device, content);
        }

        /// <summary>
        /// This render doesn't support the screen resizing.
        /// </summary>
        /// <param name="vp"></param>
        public override void OnResize(Viewport vp)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Set the loading data of the render data probaly loaded from a file.
        /// </summary>
        /// <param name="rsd"></param>
        /// <param name="content"></param>
        public void SetLoadData(RendererSaveData rsd, ContentManager content)
        {
            _ppeMgr.ClearDataElements();

            v_ambientColor = rsd.AmbientColor;
            b_renderSkymap = rsd.RenderSkymap;

            SetSSAORenderInfo(rsd.RandomTile, rsd.Radius, rsd.MaxRadius, rsd.Bias, rsd.Intensity, rsd.BlurCount);
            SetShadowInfo(rsd.CsmRes, rsd.CsmDiv, rsd.MaxSpotShadows, rsd.MaxCsmMaps, rsd.SpotShadowRes);

            UseSSAO = rsd.UseSSAO;

            BloomSaveData[] bloomSaveData = rsd.BloomSaveData;
            LightShaftSaveData[] lightShaftSaveData = rsd.LightShaftSaveData;

            List<object> saveDatas = new List<object>();
            saveDatas.AddRange(bloomSaveData);
            saveDatas.AddRange(lightShaftSaveData);

            Func<object, int> orderbyFunc = (object obj) =>
                {
                    if (obj is BloomSaveData)
                    {
                        return ((BloomSaveData)obj).Order;
                    }
                    else if (obj is LightShaftSaveData)
                    {
                        return ((LightShaftSaveData)obj).Order;
                    }
                    else
                        throw new ArgumentException("Unexpected save data type!");
                };

            // Convert to the exact order the effects were in originally.
            var orderedSaveDatas = from sd in saveDatas
                                   orderby orderbyFunc(sd)
                                   select sd;

            foreach (var saveData in orderedSaveDatas)
            {
                if (saveData is BloomSaveData)
                {
                    BloomPPE bloomPPE = new BloomPPE();
                    bloomPPE.LoadContent(_device, ResourceMgr.Content);
                    bloomPPE.SetSettingsSaveData(saveData as BloomSaveData);

                    _ppeMgr.AddToList(bloomPPE);
                }
                else if (saveData is LightShaftSaveData)
                {
                    LightShaftPPE lightShaftPPE = new LightShaftPPE();
                    lightShaftPPE.LoadContent(_device, ResourceMgr.Content);
                    lightShaftPPE.SetSettingsSaveData(saveData as LightShaftSaveData);

                    _ppeMgr.AddToList(lightShaftPPE);
                }
                else
                    throw new ArgumentException("Unexpected save data type!");
            }

            Graphics.LightingEffectMgr lightEffectPpe = new LightingEffectMgr();
            lightEffectPpe.LoadContent(Device, content);

            _ppeMgr.AddToList(lightEffectPpe);
        }

        /// <summary>
        /// Set the shadow load information loaded from the level file.
        /// </summary>
        /// <param name="csmRes"></param>
        /// <param name="csmDiv"></param>
        /// <param name="numSpotShadows"></param>
        /// <param name="numCsmShadows"></param>
        /// <param name="spotShadowRes"></param>
        public void SetShadowInfo(int csmRes, int csmDiv, int numSpotShadows, int numCsmShadows, int spotShadowRes)
        {
            _renderer.SetShadowInfo(csmRes, csmDiv, numSpotShadows, numCsmShadows, spotShadowRes);
        }

        /// <summary>
        /// Set the SSAO information loaded from the level file.
        /// </summary>
        /// <param name="randomTile"></param>
        /// <param name="radius"></param>
        /// <param name="maxRadius"></param>
        /// <param name="bias"></param>
        /// <param name="intensity"></param>
        /// <param name="blurCount"></param>
        public void SetSSAORenderInfo(float randomTile, float radius, float maxRadius, float bias, float intensity, int blurCount)
        {
            _renderer.SSAO.RandomTile = randomTile;
            _renderer.SSAO.Radius = radius;
            _renderer.SSAO.MaxRadius = maxRadius;
            _renderer.SSAO.Bias = bias;
            _renderer.SSAO.Intensity = intensity;
            _renderer.SSAO.BlurCount = blurCount;
        }

        /// <summary>
        /// Synchronize the physics and renderable objects together.
        /// Get renderable versions of the object representations.
        /// Also get all the lines, billboards, and texts to render.
        /// </summary>
        /// <param name="lights"></param>
        /// <param name="gameObjects"></param>
        /// <param name="particleSystems"></param>
        /// <param name="lines"></param>
        /// <param name="billboards"></param>
        /// <param name="texts"></param>
        public void Sync(IEnumerable<RenderingSystem.GameLight> lights, IEnumerable<RenderingSystem.GameObj> gameObjects,
            IEnumerable<RenderingSystem.GameParticleSystem> particleSystems, IEnumerable<Vector3[]> lines,
            IEnumerable<DrawableBillboard> billboards, IEnumerable<string> texts)
        {
            _renderContoller.ClearData();
            if (lights != null)
            {
                IEnumerable<RendererImpl.GeneralLight> renderLights = from l in lights
                                                                      select l.ToRendererLight();
                foreach (RendererImpl.GeneralLight light in renderLights)
                    _renderContoller.AddLight(light);
            }
            if (gameObjects != null)
            {
                IEnumerable<RendererImpl.Mesh> renderMeshes = from go in gameObjects
                                                              select go.ToRendererMesh();
                foreach (RendererImpl.Mesh mesh in renderMeshes)
                    _renderContoller.AddMesh(mesh);
            }
            if (particleSystems != null)
            {
                IEnumerable<RendererImpl.ParticleSystem> renderPSystems = from ps in particleSystems
                                                                          select ps.ParticleSystem;

                foreach (RendererImpl.ParticleSystem psys in renderPSystems)
                    _renderContoller.AddParticleSystem(psys);
            }
            if (lines != null)
            {
                _renderContoller.LinesToRender = lines;
            }
            if (billboards != null)
            {
                _renderContoller.BillboardsToRender = billboards;
            }
            if (texts != null)
            {
                _renderContoller.TextToRender = texts;
            }

            if (b_renderSkymap)
                _renderContoller.AddMesh(_skyboxMesh);

            _renderContoller.Visit(delegate(RenderingSystem.RendererImpl.Mesh.SubMesh subMesh)
            {
                _renderer.SetupSubMesh(subMesh);
                //add some ambient value
                if (subMesh.RenderEffect.AmbientParameter != null)
                    subMesh.RenderEffect.AmbientParameter.SetValue(v_ambientColor);
                //set the same cubemap to all meshes. we could set different cubemaps according to
                //the mesh position. The cubemap color is modulated by the AmbientParameter (above) and diffuse
                //texture, and added to the lighting result
                if (subMesh.RenderEffect.AmbientCubemapParameter != null && b_renderSkymap)
                    subMesh.RenderEffect.AmbientCubemapParameter.SetValue(_ambientCube);
            });
        }

        /// <summary>
        /// Release all unmanaged memory in reference to the loaded resources.
        /// </summary>
        public override void UnloadContent()
        {
            if (_renderer != null)
                _renderer.UnloadContent();
            if (_spriteBatch != null && _spriteBatch.IsDisposed)
                _spriteBatch.Dispose();
            if (_ambientCube != null && _ambientCube.IsDisposed)
                _ambientCube.Dispose();
        }

        public override void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Stops the renderer early displaying the desired render target.
        /// Debugging purposes only.
        /// </summary>
        /// <param name="rt"></param>
        /// <returns></returns>
        private string CheckForSpecialOutputs(RenderTarget2D rt)
        {
            if (rt == _renderer.DepthBuffer)
                return "[Depth Buffer]";
            else if (rt == _renderer.LightBuffer)
                return "[Light Buffer]";
            else if (rt == _renderer.LightSpecularBuffer)
                return "[Light Specular Buffer]";
            else if (rt == _renderer.NormalBuffer)
                return "[Normal Buffer]";
            else if (rt == _renderer.SSAO.GetRenderingTargets(_renderer)[RenderingSystem.RendererImpl.SSAO.RT_0_INDEX])
                return "[SSAO Buffer]";

            return null;
        }

        /// <summary>
        /// Draw the entire scene composed of meshes, lights and effects.
        /// Draw only post processing effects along with the anti-alaising.
        /// </summary>
        /// <param name="cam"></param>
        /// <param name="debugTime"></param>
        /// <param name="gameTime"></param>
        private void DrawScene(ICamera cam, DebugTimings debugTime, GameTime gameTime)
        {
            debugTime.StartTotalRenderSw();

            Rectangle fullscreenRect = new Rectangle(0, 0, _device.Viewport.Width, _device.Viewport.Height);

            _device.BlendState = BlendState.Opaque;
            _device.DepthStencilState = DepthStencilState.Default;

            RenderTarget2D output = _renderer.RenderScene(cam, _renderContoller, gameTime, _ppeMgr, debugTime);

            string rtName = CheckForSpecialOutputs(output);

            if (rtName != null)
            {
                _device.SetRenderTarget(null);
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp, null, null);
                _spriteBatch.Draw(output, fullscreenRect, Color.White);
                _spriteBatch.DrawString(_spriteFont, rtName, Vector2.Zero, Color.Red);
                _spriteBatch.End();

                return;
            }

            debugTime.StartPostProccessRender();
            output = _ppeMgr.DrawEffects(_device, output, _renderer);
            debugTime.EndPostProccessRender();

            _device.BlendState = BlendState.Opaque;
            _device.DepthStencilState = DepthStencilState.None;

            if (!_ppeMgr.AntiAliasingEnabled)
                _device.SetRenderTarget(null);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            if (!_ppeMgr.AntiAliasingEnabled)
                _spriteBatch.Draw(output, fullscreenRect, Color.White);

            if (_renderGBuffer)
            {
                int smallWidth = _device.Viewport.Width / 3;
                int smallHeigth = _device.Viewport.Height / 3;

                //draw the intermediate steps into screen
                _spriteBatch.Draw(_renderer.NormalBuffer, new Rectangle(0, 0, smallWidth, smallHeigth),
                                    Color.White);
                _spriteBatch.Draw(_renderer.DepthBuffer, new Rectangle(smallWidth, 0, smallWidth, smallHeigth),
                                    Color.White);
                _spriteBatch.Draw(_renderer.HalfBuffer0, new Rectangle(smallWidth * 2, 0, smallWidth, smallHeigth),
                                  Color.White);
            }
            _spriteBatch.End();

            if (_renderText)
            {
                _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp,
                                   DepthStencilState.Default, RasterizerState.CullCounterClockwise);

                // Draw all of the render text.

                int yPos = 0;
                int xPos = 0;
                _spriteBatch.DrawString(_spriteFont, String.Format("{0:00.0},{1:00.0},{2:00.0}", cam.Position.X, cam.Position.Y, cam.Position.Z),
                    new Vector2(xPos, yPos += 20), Color.White);
                _spriteBatch.DrawString(_spriteFont, String.Format("Draw:{0:00.0}", d_drawMs), new Vector2(xPos, yPos += 20), Color.White);
                xPos += 10;
                _spriteBatch.DrawString(_spriteFont, String.Format("Shadow Maps:{0:00.0}", debugTime.ShadowRenderMs),
                    new Vector2(xPos, yPos += 20), Color.White);
                _spriteBatch.DrawString(_spriteFont, String.Format("GBuffer:{0:00.0}", debugTime.GBufferRenderMs),
                    new Vector2(xPos, yPos += 20), Color.White);
                _spriteBatch.DrawString(_spriteFont, String.Format("Lighting:{0:00.0}", debugTime.LightRenderMs),
                    new Vector2(xPos, yPos += 20), Color.White);
                _spriteBatch.DrawString(_spriteFont, String.Format("Reconstruct Shading:{0:00.0}", debugTime.ReconstructShadingRenderMs),
                    new Vector2(xPos, yPos += 20), Color.White);
                _spriteBatch.DrawString(_spriteFont, String.Format("SSAO:{0:00.0}", debugTime.SSAORenderMs),
                    new Vector2(xPos, yPos += 20), Color.White);
                _spriteBatch.DrawString(_spriteFont, String.Format("Post Proccess:{0:00.0}", debugTime.PostProcessRenderMs),
                    new Vector2(xPos, yPos += 20), Color.White);

                xPos = 0;
                _spriteBatch.DrawString(_spriteFont, String.Format("Update:{0:00.0}", debugTime.TotalUpdateMs),
                    new Vector2(xPos, yPos += 20), Color.White);

                xPos += 10;
                _spriteBatch.DrawString(_spriteFont, String.Format("Camera:{0:00.0}", debugTime.CameraUpdateMs),
                    new Vector2(xPos, yPos += 20), Color.White);
                _spriteBatch.DrawString(_spriteFont, String.Format("Renderer:{0:00.0}", debugTime.RendererUpdateMs),
                    new Vector2(xPos, yPos += 20), Color.White);
                _spriteBatch.DrawString(_spriteFont, String.Format("Lights:{0:00.0}", debugTime.LightUpdateMs),
                    new Vector2(xPos, yPos += 20), Color.White);
                _spriteBatch.DrawString(_spriteFont, String.Format("Objects:{0:00.0}", debugTime.ObjUpdateMs),
                    new Vector2(xPos, yPos += 20), Color.White);
                _spriteBatch.DrawString(_spriteFont, String.Format("Players:{0:00.0}", debugTime.PlayerUpdateMs),
                    new Vector2(xPos, yPos += 20), Color.White);

                xPos += 10;
                _spriteBatch.DrawString(_spriteFont, String.Format("AI 1:{0:00.0}", debugTime.Ai1UpdateMs),
                    new Vector2(xPos, yPos += 20), Color.White);
                _spriteBatch.DrawString(_spriteFont, String.Format("AI 2:{0:00.0}", debugTime.Ai2UpdateMs),
                    new Vector2(xPos, yPos += 20), Color.White);
                _spriteBatch.DrawString(_spriteFont, String.Format("AI 3:{0:00.0}", debugTime.Ai3UpdateMs),
                    new Vector2(xPos, yPos += 20), Color.White);

                _spriteBatch.DrawString(_spriteFont, String.Format("Processes:{0:00.0}", debugTime.ProcUpdateMs),
                    new Vector2(xPos, yPos += 20), Color.White);
                _spriteBatch.DrawString(_spriteFont, String.Format("Level:{0:00.0}", debugTime.LevelUpdateMs),
                    new Vector2(xPos, yPos += 20), Color.White);
                _spriteBatch.DrawString(_spriteFont, String.Format("Particles:{0:00.0}", debugTime.ParticleUpdateMs),
                    new Vector2(xPos, yPos += 20), Color.White);
                _spriteBatch.DrawString(_spriteFont, String.Format("Forms:{0:00.0}", debugTime.FormUpdateMs),
                    new Vector2(xPos, yPos += 20), Color.White);
                _spriteBatch.DrawString(_spriteFont, String.Format("Render Sync:{0:00.0}", debugTime.SyncUpdateMs),
                    new Vector2(xPos, yPos += 20), Color.White);

                foreach (string msgTxt in _renderContoller.TextToRender)
                {
                    _spriteBatch.DrawString(_spriteFont, msgTxt, new Vector2(0, yPos += 20), Color.White);
                }

                _spriteBatch.End();
            }

            if (_renderQuads)
            {
                _quadRenderer.RenderQuads(_device, cam, _renderContoller.BillboardsToRender);
            }

            if (_renderCullBBs)
            {
                List<RenderingSystem.RendererImpl.Mesh.SubMesh> visibleSubMeshes = new List<RenderingSystem.RendererImpl.Mesh.SubMesh>(50);
                List<RenderingSystem.RendererImpl.ParticleSystem> visibleParticleSystems = new List<RenderingSystem.RendererImpl.ParticleSystem>(25);
                _device.DepthStencilState = DepthStencilState.None;
                _renderContoller.GetVisibleMeshes(cam.Frustum, visibleSubMeshes);
                _renderContoller.GetVisibleParticleSystems(cam.Frustum, visibleParticleSystems);
                foreach (RenderingSystem.RendererImpl.Mesh.SubMesh subMesh in visibleSubMeshes)
                {
                    _boundingBoxRenderer.Draw(_device, subMesh.GlobalBoundingBox, cam, Color.Red);
                }
                foreach (RenderingSystem.RendererImpl.ParticleSystem particleSystem in visibleParticleSystems)
                {
                    _boundingBoxRenderer.Draw(_device, particleSystem.GlobalBoundingBox, cam, Color.Orange);
                }
            }

            if (_renderPhysicsBBs)
            {
                _device.DepthStencilState = DepthStencilState.None;

                IEnumerable<BoundingBox> physicsBoundingBoxes = _renderContoller.GetPhysicsBoundingBoxes();
                foreach (BoundingBox bb in physicsBoundingBoxes)
                {
                    _boundingBoxRenderer.Draw(_device, bb, cam, Color.Purple);
                }
            }

            if (_renderLines)
            {
                _device.DepthStencilState = DepthStencilState.None;

                IEnumerable<Vector3[]> lineBoundingBoxes = _renderContoller.LinesToRender;
                foreach (Vector3[] line in lineBoundingBoxes)
                {
                    if (line.Count() != 3)
                        throw new ArgumentException("There can only be three ( start, end, color ) elements in the array for a line to be rendered!");

                    Vector3 start = line[0];
                    Vector3 end = line[1];
                    Vector3 color = line[2];

                    _lineRenderer.Draw(_device, start, end, cam, color);
                }
            }
            d_drawMs = debugTime.EndTotalRenderSw();
        }
    }
}