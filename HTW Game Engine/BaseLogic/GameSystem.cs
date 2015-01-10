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

using RenderingSystem;

namespace BaseLogic
{
    /// <summary>
    ///
    /// </summary>
    public struct GraphicsSettings
    {
        /// <summary>
        /// The fullscreen
        /// </summary>
        public bool Fullscreen;

        /// <summary>
        /// The multi sample
        /// </summary>
        public bool MultiSample;

        /// <summary>
        /// The v synchronize
        /// </summary>
        public bool VSync;

        /// <summary>
        /// The window height
        /// </summary>
        public int WindowHeight;

        /// <summary>
        /// The window width
        /// </summary>
        public int WindowWidth;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsSettings"/> struct.
        /// </summary>
        /// <param name="multiSample">if set to <c>true</c> [multi sample].</param>
        /// <param name="vsync">if set to <c>true</c> [vsync].</param>
        public GraphicsSettings(bool multiSample, bool vsync)
        {
            Fullscreen = true;
            MultiSample = multiSample;
            VSync = vsync;
            WindowWidth = 0;
            WindowHeight = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsSettings"/> struct.
        /// </summary>
        /// <param name="windowWidth">Width of the window.</param>
        /// <param name="windowHeight">Height of the window.</param>
        /// <param name="multiSample">if set to <c>true</c> [multi sample].</param>
        /// <param name="vsync">if set to <c>true</c> [vsync].</param>
        public GraphicsSettings(int windowWidth, int windowHeight, bool multiSample, bool vsync)
        {
            Fullscreen = false;
            MultiSample = multiSample;
            VSync = vsync;
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
        }

        /// <summary>
        /// Creates the definition.
        /// </summary>
        /// <returns></returns>
        public static GraphicsSettings CreateDef()
        {
            GraphicsSettings graphicsSettings;
            graphicsSettings.Fullscreen = false;
            graphicsSettings.MultiSample = false;
            graphicsSettings.VSync = true;
            graphicsSettings.WindowHeight = 400;
            graphicsSettings.WindowWidth = 800;

            return graphicsSettings;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GameSystem
    {
        /// <summary>
        /// The defaul t_ savegam e_ folder
        /// </summary>
        public const string DEFAULT_SAVEGAME_FOLDER = "Saved Games";

        /// <summary>
        /// The mode l_ filenam e_ loc
        /// </summary>
        public const string MODEL_FILENAME_LOC = "Models//";

        /// <summary>
        /// The particl e_ filenam e_ loc
        /// </summary>
        public const string PARTICLE_FILENAME_LOC = "particles//";

        /// <summary>
        /// The draw UI function
        /// </summary>
        public Action<TextRenderer, int, int> DrawUIFunc;

        /// <summary>
        /// The sp_game system
        /// </summary>
        private static GameSystem sp_gameSystem = null;

        /// <summary>
        /// The _ai MGR
        /// </summary>
        private Player.AIMgr _aiMgr = null;

        /// <summary>
        /// The _cam
        /// </summary>
        private ICamera _cam = null;

        /// <summary>
        /// The _content
        /// </summary>
        private ContentManager _content = null;

        /// <summary>
        /// The _debug timings
        /// </summary>
        private DebugTimings _debugTimings = new DebugTimings();

        /// <summary>
        /// The _form MGR
        /// </summary>
        private Manager.EditorFormMgr _formMgr;

        /// <summary>
        /// The _graphics settings
        /// </summary>
        private GraphicsSettings _graphicsSettings = GraphicsSettings.CreateDef();

        /// <summary>
        /// The _level saver
        /// </summary>
        private Object.LevelSerilizer _levelSaver = null;

        /// <summary>
        /// The _light MGR
        /// </summary>
        private Manager.LightManager _lightMgr;

        /// <summary>
        /// The _obj MGR
        /// </summary>
        private Manager.ObjectMgr _objMgr;

        /// <summary>
        /// The _particle MGR
        /// </summary>
        private Manager.ParticleMgr _particleMgr;

        /// <summary>
        /// The _physics MGR
        /// </summary>
        private Manager.GamePhysicsMgr _physicsMgr = null;

        /// <summary>
        /// The _player MGR
        /// </summary>
        private Manager.PlayerMgr _playerMgr;

        /// <summary>
        /// The _primitive renderer
        /// </summary>
        private Graphics.PrimitivesRenderer _primitiveRenderer = null;

        /// <summary>
        /// The _process MGR
        /// </summary>
        private Manager.ProcessMgr _processMgr;

        /// <summary>
        /// The _renderer
        /// </summary>
        private GameRenderer _renderer = null;

        /// <summary>
        /// The _TXT renderer
        /// </summary>
        private TextRenderer _txtRenderer = null;

        /// <summary>
        /// The b_debug mode
        /// </summary>
        private bool b_debugMode = true;

        /// <summary>
        /// The s_latest save file
        /// </summary>
        private string s_latestSaveFile = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameSystem"/> class.
        /// </summary>
        public GameSystem()
        {
            sp_gameSystem = this;
        }

        /// <summary>
        /// Gets the game sys_ instance.
        /// </summary>
        /// <value>
        /// The game sys_ instance.
        /// </value>
        public static GameSystem GameSys_Instance
        {
            get { return sp_gameSystem; }
        }

        /// <summary>
        /// Gets the ai MGR.
        /// </summary>
        /// <value>
        /// The ai MGR.
        /// </value>
        public Player.AIMgr AIMgr
        {
            get { return _aiMgr; }
        }

        /// <summary>
        /// Gets or sets the color of the ambient.
        /// </summary>
        /// <value>
        /// The color of the ambient.
        /// </value>
        public Vector4 AmbientColor
        {
            get { return (_renderer as RendererAccess).AmbientColor; }
            set { (_renderer as RendererAccess).AmbientColor = value; }
        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public ContentManager Content
        {
            get { return _content; }
        }

        /// <summary>
        /// Gets the debug timings.
        /// </summary>
        /// <value>
        /// The debug timings.
        /// </value>
        public DebugTimings DebugTimings
        {
            get { return _debugTimings; }
        }

        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <value>
        /// The device.
        /// </value>
        public GraphicsDevice Device
        {
            get { return _renderer.Device; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw debug text].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [draw debug text]; otherwise, <c>false</c>.
        /// </value>
        public bool DrawDebugText
        {
            get { return _debugTimings.Update; }
            set { _debugTimings.Update = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw skymap].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [draw skymap]; otherwise, <c>false</c>.
        /// </value>
        public bool DrawSkymap
        {
            get { return (_renderer as RendererAccess).RenderSkymap; }
            set { (_renderer as RendererAccess).RenderSkymap = value; }
        }

        /// <summary>
        /// Gets the game camera.
        /// </summary>
        /// <value>
        /// The game camera.
        /// </value>
        public ICamera GameCamera
        {
            get { return _cam; }
        }

        /// <summary>
        /// Gets or sets the graphics settings.
        /// </summary>
        /// <value>
        /// The graphics settings.
        /// </value>
        public GraphicsSettings GraphicsSettings
        {
            get { return _graphicsSettings; }
            set { _graphicsSettings = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has active windows.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has active windows; otherwise, <c>false</c>.
        /// </value>
        public bool HasActiveWindows
        {
            get { return _formMgr.HasActiveWindows(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [in debug mode].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [in debug mode]; otherwise, <c>false</c>.
        /// </value>
        public bool InDebugMode
        {
            set
            {
                (_renderer as RendererAccess).RenderText = value;
                (_renderer as RendererAccess).RenderLines = value;
                (_renderer as RendererAccess).RenderQuads = value;
                b_debugMode = value;
            }
            get
            {
                return b_debugMode;
            }
        }

        /// <summary>
        /// Gets the last save filename.
        /// </summary>
        /// <value>
        /// The last save filename.
        /// </value>
        public string LastSaveFilename
        {
            get { return s_latestSaveFile; }
        }

        /// <summary>
        /// Gets the light MGR.
        /// </summary>
        /// <value>
        /// The light MGR.
        /// </value>
        public Manager.LightManager LightMgr
        {
            get { return _lightMgr; }
        }

        /// <summary>
        /// Gets the object MGR.
        /// </summary>
        /// <value>
        /// The object MGR.
        /// </value>
        public Manager.ObjectMgr ObjMgr
        {
            get { return _objMgr; }
        }

        /// <summary>
        /// Gets or sets the on scene load.
        /// </summary>
        /// <value>
        /// The on scene load.
        /// </value>
        public Action<string> OnSceneLoad
        {
            get { return _levelSaver.OnSceneLoad; }
            set { _levelSaver.OnSceneLoad = value; }
        }

        /// <summary>
        /// Gets the particle MGR.
        /// </summary>
        /// <value>
        /// The particle MGR.
        /// </value>
        public Manager.ParticleMgr ParticleMgr
        {
            get { return _particleMgr; }
        }

        /// <summary>
        /// Gets the physics MGR.
        /// </summary>
        /// <value>
        /// The physics MGR.
        /// </value>
        public Manager.GamePhysicsMgr PhysicsMgr
        {
            get { return _physicsMgr; }
        }

        /// <summary>
        /// Gets the player MGR.
        /// </summary>
        /// <value>
        /// The player MGR.
        /// </value>
        public Manager.PlayerMgr PlayerMgr
        {
            get { return _playerMgr; }
        }

        /// <summary>
        /// Gets the primitives renderer.
        /// </summary>
        /// <value>
        /// The primitives renderer.
        /// </value>
        public Graphics.PrimitivesRenderer PrimitivesRenderer
        {
            get { return _primitiveRenderer; }
        }

        /// <summary>
        /// Gets the process MGR.
        /// </summary>
        /// <value>
        /// The process MGR.
        /// </value>
        public Manager.ProcessMgr ProcessMgr
        {
            get { return _processMgr; }
        }

        /// <summary>
        /// Gets the renderer.
        /// </summary>
        /// <value>
        /// The renderer.
        /// </value>
        public GameRenderer Renderer
        {
            get { return _renderer; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [render physics bounding boxes].
        /// </summary>
        /// <value>
        /// <c>true</c> if [render physics bounding boxes]; otherwise, <c>false</c>.
        /// </value>
        public bool RenderPhysicsBoundingBoxes
        {
            get { return (_renderer as RendererAccess).RenderPhysicsBoundingBoxes; }
            set { (_renderer as RendererAccess).RenderPhysicsBoundingBoxes = value; }
        }

        /// <summary>
        /// Gets the text renderer.
        /// </summary>
        /// <value>
        /// The text renderer.
        /// </value>
        public TextRenderer TextRenderer
        {
            get { return _txtRenderer; }
        }

        /// <summary>
        /// Adds the form for updating.
        /// </summary>
        /// <param name="updatingForm">The updating form.</param>
        public void AddFormForUpdating(UpdatingForm updatingForm)
        {
            _formMgr.AddFormForUpdating(updatingForm);
        }

        /// <summary>
        /// Adds the game process.
        /// </summary>
        /// <param name="process">The process.</param>
        public void AddGameProcess(Process.GameProcess process)
        {
            _processMgr.AddProcess(process);
        }

        /// <summary>
        /// Adds a render object to the object manager and registers it with the physics manager
        /// if it is a physics object.
        /// </summary>
        /// <param name="gameObj">The game object to add.</param>
        /// <param name="forcePhysReg">If true the physics object will be added no matter.
        /// This could result in a physics body being added twice.
        /// Used for when the rigid body has been created although not with a collision skin.</param>
        public void AddRenderObj(GameObj gameObj, bool forcePhysReg = false)
        {
            if (gameObj is PhysObj)
            {
                PhysObj physObj = gameObj as PhysObj;
                if (forcePhysReg || !physObj.HasRegisteredRB())
                    physObj.RegisterRB(_physicsMgr.HengePhysicsMgr);
            }
            _objMgr.AddToList(gameObj);
        }

        /// <summary>
        /// Casts the edit ray.
        /// </summary>
        public void CastEditRay()
        {
            Vector3 end = new Vector3(0f, 0f, -1000f);
            Vector3 start = _cam.Position;

            end = Vector3.Transform(end, _cam.Transform);

            Manager.RaytraceIntersectionInfo intersectionResult = _physicsMgr.Raytrace(new Manager.RaytraceFireInfo(start, end));
            if (intersectionResult != null)
            {
                Henge3D.Physics.RigidBody rigidBody = intersectionResult.IntersectionBody;
                if (rigidBody is GameRigidBody)
                {
                    string id = (rigidBody as GameRigidBody).Name;
                    GameObj gameObj = _objMgr.GetDataElement(id);
                    if (!(gameObj is StaticObj))
                        return;
                    StaticObj staticObj = gameObj as StaticObj;
                    int indexOf = _objMgr.GetIndexOfObj(staticObj);
                    System.Windows.Forms.Form form = _formMgr.CreateEditorForm(typeof(BaseLogic.Editor.Forms.GameObjEditorForm),
                        this, false);
                    (form as BaseLogic.Editor.Forms.GameObjEditorForm).SelectedObjIndex = indexOf;
                }
            }
        }

        /// <summary>
        /// Clears all data.
        /// </summary>
        public void ClearAllData()
        {
            _lightMgr.ClearDataElements();
            _playerMgr.ClearDataElements();
            _objMgr.ClearDataElements();
            _particleMgr.ClearDataElements();
        }

        /// <summary>
        /// Creates the animated object.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="actorID">The actor identifier.</param>
        /// <param name="clipName">Name of the clip.</param>
        /// <param name="bbMin">The bb minimum.</param>
        /// <param name="bbMax">The bb maximum.</param>
        /// <returns></returns>
        public AnimatedObj CreateAnimatedObj(string filename, string actorID, string clipName, Vector3 bbMin, Vector3 bbMax)
        {
            return CreateAnimatedObjAbsoluteFilename(MODEL_FILENAME_LOC + filename, actorID, clipName, bbMin, bbMax);
        }

        /// <summary>
        /// Creates the animated object absolute filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="actorID">The actor identifier.</param>
        /// <param name="clipName">Name of the clip.</param>
        /// <param name="bbMin">The bb minimum.</param>
        /// <param name="bbMax">The bb maximum.</param>
        /// <returns></returns>
        public AnimatedObj CreateAnimatedObjAbsoluteFilename(string filename, string actorID, string clipName, Vector3 bbMin, Vector3 bbMax)
        {
            if (_objMgr.ActorIDExists(actorID))
                return CreateAnimatedObjAbsoluteFilename(filename, actorID + "c", clipName, bbMin, bbMax);

            AnimatedObj animatedObj = new AnimatedObj(actorID);
            if (!animatedObj.LoadContent(_content, filename, clipName))
                return null;
            animatedObj.SetPhysicsData(bbMin, bbMax);
            animatedObj.RegisterRB(_physicsMgr.HengePhysicsMgr);
            AddRenderObj(animatedObj);

            return animatedObj;
        }

        /// <summary>
        /// Creates the camera.
        /// </summary>
        /// <param name="cam">The cam.</param>
        /// <returns></returns>
        public ICamera CreateCamera(ICamera cam)
        {
            cam.SetDefault(_renderer.Device.Viewport);

            _cam = cam;
            if (_cam is Camera.FirstPersonCamera)
                b_debugMode = false;
            else
                b_debugMode = true;

            return cam;
        }

        /// <summary>
        /// Creates the form.
        /// </summary>
        /// <param name="formType">Type of the form.</param>
        /// <param name="modalWindow">if set to <c>true</c> [modal window].</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void CreateForm(Manager.EditorFormType formType, bool modalWindow = false)
        {
            Type type;
            switch (formType)
            {
                case Manager.EditorFormType.GameObjEditorForm:
                    type = typeof(Editor.Forms.GameObjEditorForm);
                    break;

                case Manager.EditorFormType.GameLightEditorForm:
                    type = typeof(Editor.Forms.GameLightEditorForm);
                    break;

                case Manager.EditorFormType.SceneSettingsForm:
                    type = typeof(Editor.Forms.SceneSettingsForm);
                    break;

                case Manager.EditorFormType.SSAOSettingsForm:
                    _formMgr.CreateSSAOSettingsForm(_renderer as RendererAccess);
                    return;

                case Manager.EditorFormType.ShadowSettingsForm:
                    _formMgr.CreateShadowSettingsForm(_renderer as RendererAccess);
                    return;

                case Manager.EditorFormType.ParticleSettingsForm:
                    _formMgr.CreateParticleMgrForm(_particleMgr, _renderer.Device, _content);
                    return;

                case Manager.EditorFormType.RendererSettingsForm:
                    _formMgr.CreateRendererSettingsForm(_renderer);
                    return;

                case Manager.EditorFormType.AIGraphEditorForm:
                    _aiMgr.CreateGraphEditor(this);
                    return;

                case Manager.EditorFormType.RealTimeEventEditorForm:
                    type = typeof(Editor.Forms.Object_Editors.RealTimeEventEditorForm);
                    break;

                default:
                    throw new ArgumentException();
            }

            _formMgr.CreateEditorForm(type, this, modalWindow);
        }

        /// <summary>
        /// Creates the particle system.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public GameParticleSystem CreateParticleSystem(string filename, string id)
        {
            if (_particleMgr.ActorIDExists(id))
                return CreateParticleSystem(filename, id + "c");

            GameParticleSystem psys = new GameParticleSystem(id);
            psys.LoadContent(_content, _renderer.Device, PARTICLE_FILENAME_LOC + filename);

            _particleMgr.AddToList(psys);

            return psys;
        }

        /// <summary>
        /// Creates the particle system.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public GameParticleSystem CreateParticleSystem(RenderingSystem.RendererImpl.ParticleSettings settings, string id)
        {
            if (_particleMgr.ActorIDExists(id))
                return CreateParticleSystem(settings, id + "c");

            GameParticleSystem psys = new GameParticleSystem(id);
            psys.SetInfo(_content, _renderer.Device, settings);

            _particleMgr.AddToList(psys);

            return psys;
        }

        /// <summary>
        /// Creates the phased anim object.
        /// </summary>
        /// <param name="absoluteFilename">The absolute filename.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="clipname">The clipname.</param>
        /// <returns></returns>
        public Object.PhasedAnimObj CreatePhasedAnimObj(string absoluteFilename, string id, string clipname)
        {
            if (_objMgr.ActorIDExists(id))
                return CreatePhasedAnimObj(absoluteFilename, id, clipname);

            Object.PhasedAnimObj phasedAnimObj = new Object.PhasedAnimObj(id);
            if (!phasedAnimObj.LoadContent(absoluteFilename, clipname))
                return null;
            AddRenderObj(phasedAnimObj);

            return phasedAnimObj;
        }

        /// <summary>
        /// Creates the phased object.
        /// </summary>
        /// <param name="absoluteFilename">The absolute filename.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Object.PhasedObj CreatePhasedObj(string absoluteFilename, string id)
        {
            if (_objMgr.ActorIDExists(id))
                return CreatePhasedObj(absoluteFilename, id + "c");

            Object.PhasedObj phasedObj = new Object.PhasedObj(id);
            phasedObj.LoadContent(absoluteFilename);

            AddRenderObj(phasedObj);

            return phasedObj;
        }

        /// <summary>
        /// Creates the static object.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="actorID">The actor identifier.</param>
        /// <returns></returns>
        public StaticObj CreateStaticObj(string filename, string actorID)
        {
            return CreateStaticObjAbsoluteFilename(MODEL_FILENAME_LOC + filename, actorID);
        }

        /// <summary>
        /// Creates the static object.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public StaticObj CreateStaticObj(string filename)
        {
            string actorID = filename + " static object";

            return CreateStaticObj(filename, actorID);
        }

        /// <summary>
        /// Creates the static object.
        /// </summary>
        /// <param name="staticObj">The static object.</param>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public StaticObj CreateStaticObj(StaticObj staticObj, string filename)
        {
            staticObj.LoadContent(_content, MODEL_FILENAME_LOC + filename);
            //staticObj.RegisterRB(_physicsMgr.HengePhysicsMgr);
            AddRenderObj(staticObj);

            return staticObj;
        }

        /// <summary>
        /// Creates the static object absolute filename.
        /// </summary>
        /// <param name="absoluteFilename">The absolute filename.</param>
        /// <param name="actorID">The actor identifier.</param>
        /// <returns></returns>
        public StaticObj CreateStaticObjAbsoluteFilename(string absoluteFilename, string actorID)
        {
            if (_objMgr.ActorIDExists(actorID))
                return CreateStaticObjAbsoluteFilename(absoluteFilename, actorID + "c");

            StaticObj staticObj = new StaticObj(actorID);
            if (!staticObj.LoadContent(_content, absoluteFilename))
                return null;
            staticObj.RegisterRB(_physicsMgr.HengePhysicsMgr);
            AddRenderObj(staticObj);

            return staticObj;
        }

        /// <summary>
        /// Creates the user player.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <param name="userPlayer">The user player.</param>
        /// <param name="bbModelFilename">The bb model filename.</param>
        /// <returns></returns>
        public Player.UserPlayer CreateUserPlayer(float scale, Player.UserPlayer userPlayer, string bbModelFilename)
        {
            string bbModelLocation = MODEL_FILENAME_LOC + bbModelFilename;

            StaticObj staticObj = new StaticObj("user bb");
            staticObj.LoadContent(_content, bbModelLocation);
            staticObj.RegisterRB(_physicsMgr.HengePhysicsMgr);
            staticObj.Scale = scale;

            userPlayer.SetGameObjPtr(staticObj);

            _playerMgr.AddToList(userPlayer);

            return userPlayer;
        }

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="skipSceneRender">if set to <c>true</c> [skip scene render].</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void Draw(GameTime gameTime, bool skipSceneRender = false)
        {
            // Check if the user forgot to set something.
            if (_renderer == null || _cam == null)
                throw new ArgumentException();

            if (!skipSceneRender)
                _renderer.Draw(gameTime, _cam, _debugTimings);

            Viewport vp = _renderer.Device.Viewport;

            _txtRenderer.BeginRenderText();
            // Draw the game UI.
            DrawUIFunc(_txtRenderer, vp.Width, vp.Height);
            _txtRenderer.EndRenderText(_renderer.Device);

            _primitiveRenderer.OnFinishedRendering();
        }

        /// <summary>
        /// Gets the real time event triggers.
        /// </summary>
        /// <returns></returns>
        public List<Process.RealTimeEventTrigger> GetRealTimeEventTriggers()
        {
            var processRTEs = _processMgr.GetRealTimeTriggerEvents();

            return processRTEs.ToList();
        }

        /// <summary>
        /// Gets the user player.
        /// </summary>
        /// <returns></returns>
        public Player.UserPlayer GetUserPlayer()
        {
            Player.GamePlayer player = _playerMgr.GetPlayerOfId("user player");
            return player as Player.UserPlayer;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="game">The game.</param>
        public void LoadContent(Game game)
        {
            GraphicsDevice device = game.GraphicsDevice;
            ContentManager content = game.Content;

            ResourceMgr.SetContentMgr(content);
            ResourceMgr.SetGraphicsDevice(device);

            _renderer = new RenderingSystem.RendererAccess(device);
            _renderer.LoadContent(content);

            _txtRenderer = new TextRenderer();
            _txtRenderer.LoadContent(device, content);

            _formMgr = new Manager.EditorFormMgr();

            _physicsMgr = new Manager.GamePhysicsMgr(game);
            _physicsMgr.LoadContent();

            _lightMgr = new Manager.LightManager();

            _objMgr = new Manager.ObjectMgr();
            _objMgr.LoadContent();

            _playerMgr = new Manager.PlayerMgr();

            _processMgr = new Manager.ProcessMgr();

            _objMgr.SetPhysicsMgr(_physicsMgr);

            _particleMgr = new Manager.ParticleMgr();

            _levelSaver = new Object.LevelSerilizer();

            _primitiveRenderer = new Graphics.PrimitivesRenderer();
            _primitiveRenderer.LoadContent();

            _content = content;
        }

        /// <summary>
        /// Loads the scene.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void LoadScene(string filename)
        {
            LoadScene(filename, DEFAULT_SAVEGAME_FOLDER);
        }

        /// <summary>
        /// Loads the scene.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="foldername">The foldername.</param>
        public void LoadScene(string filename, string foldername)
        {
            _levelSaver.AttemptLoadGame(this, filename, foldername);
        }

        /// <summary>
        /// Loads the scene dialog.
        /// </summary>
        public void LoadSceneDialog()
        {
            if (_formMgr.LoadFormOpen)
                return;

            Action<string> onFormSubmit = (string s) =>
                {
                    _formMgr.LoadFormOpen = false;
                    if (s == null)
                        return;
                    else
                        LoadScene(s);
                };

            _formMgr.CreateLoadLevelForm(onFormSubmit);
        }

        /// <summary>
        /// Called when [resize].
        /// </summary>
        public void OnResize()
        {
            _renderer.OnResize(_renderer.Device.Viewport);
        }

        /// <summary>
        /// Saves the scene.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void SaveScene(string filename)
        {
            s_latestSaveFile = filename;
            SaveScene(filename, DEFAULT_SAVEGAME_FOLDER);
        }

        /// <summary>
        /// Saves the scene.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="folder">The folder.</param>
        public void SaveScene(string filename, string folder)
        {
            _levelSaver.AttemptSaveGame(this, filename, folder);
        }

        /// <summary>
        /// Saves the scene dialog.
        /// </summary>
        public void SaveSceneDialog()
        {
            if (_formMgr.SaveFormOpen)
                return;

            Action<string> onFormSubmit = (string s) =>
                {
                    _formMgr.SaveFormOpen = false;
                    if (s == null)
                        return;
                    else
                        SaveScene(s);
                };

            _formMgr.CreateSaveLevelForm(onFormSubmit);
        }

        /// <summary>
        /// Sets the ai MGR.
        /// </summary>
        /// <param name="aiMgr">The ai MGR.</param>
        public void SetAIMgr(Player.AIMgr aiMgr)
        {
            _aiMgr = aiMgr;
        }

        /// <summary>
        /// Sets the graphics settings.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        public void SetGraphicsSettings(GraphicsDeviceManager graphics)
        {
            graphics.SynchronizeWithVerticalRetrace = _graphicsSettings.VSync;
            graphics.IsFullScreen = _graphicsSettings.Fullscreen;
            graphics.PreferMultiSampling = _graphicsSettings.MultiSample;
            if (_graphicsSettings.WindowWidth != 0 && _graphicsSettings.WindowHeight != 0)
            {
                graphics.PreferredBackBufferWidth = _graphicsSettings.WindowWidth;
                graphics.PreferredBackBufferHeight = _graphicsSettings.WindowHeight;
            }
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public void UnloadContent()
        {
            _renderer.UnloadContent();
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            _debugTimings.StartTotalUpdateSw();

            _debugTimings.StartCameraUpdateSw();
            _cam.Update(gameTime);
            _debugTimings.EndCameraUpdateSw();

            _debugTimings.StartRendererUpdateSw();
            _renderer.Update(gameTime);
            _debugTimings.EndRendererUpdateSw();

            _debugTimings.StartLightUpdateSw();
            _lightMgr.UpdateLights(gameTime);
            _debugTimings.EndLightUpdateSw();

            _debugTimings.StartObjUpdateSw();
            _objMgr.UpdateObjs(gameTime);
            _debugTimings.EndObjUpdateSw();

            _debugTimings.StartPlayerUpdateSw();
            _playerMgr.Update(gameTime);
            _debugTimings.EndPlayerUpdateSw();

            _debugTimings.StartProcUpdateSw();
            _processMgr.UpdateProccesses(gameTime);
            _debugTimings.EndProcUpdateSw();

            _debugTimings.StartLevelUpdateSw();
            _levelSaver.Update(this);
            _debugTimings.EndLevelUpdateSw();

            _debugTimings.StartParticleUpdateSw();
            _particleMgr.UpdateSystems(gameTime);
            _debugTimings.EndParticleUpdateSw();

            _debugTimings.StartFormUpdateSw();
            _formMgr.UpdateForms();
            _debugTimings.EndFormUpdateSw();

            bool renderText = (_renderer as RendererAccess).RenderText;
            bool renderQuads = (_renderer as RendererAccess).RenderQuads;

            var quadsToRender = new List<RenderingSystem.RendererImpl.DrawableBillboard>();
            if (renderQuads)
                quadsToRender = _lightMgr.GetDrawableBillboards();

            var textToRender = new List<string>();
            if (renderText)
                textToRender = GetGameSysRenderText();

            _debugTimings.StartSyncUpdateSw();
            (_renderer as RendererAccess).Sync(_lightMgr.GetDataElements(), _objMgr.GetDataElements(), _particleMgr.GetDataElements(),
                _primitiveRenderer.LinesToDraw, quadsToRender, textToRender);
            _debugTimings.EndSyncUpdateSw();

            _debugTimings.EndTotalUpdateSw();
        }

        /// <summary>
        /// Gets the game system render text.
        /// </summary>
        /// <returns></returns>
        private List<string> GetGameSysRenderText()
        {
            List<string> textToRender = new List<string>();
            if (s_latestSaveFile != null)
                textToRender.Add(String.Format("[{0}]", s_latestSaveFile));

            return textToRender;
        }
    }
}