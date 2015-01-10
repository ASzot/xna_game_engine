#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Linq;
using BaseLogic;
using BaseLogic.Camera;
using BaseLogic.Object;
using BaseLogic.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using My_Xna_Game.Core;
using My_Xna_Game.Game_Objects;
using My_Xna_Game.Map;
using My_Xna_Game.Trivia;
using RenderingSystem;

namespace My_Xna_Game
{
    /// <summary>
    /// The core game component. 
    /// In charge of managing all the different components of the game system.
    /// </summary>
    public abstract class XnaGame : Microsoft.Xna.Framework.Game
    {
        public bool UpdateGame = true;
        public UserPlayer UserPlayer;
        protected Xna_Game_AI.AIMgrImpl _aiMgr = new Xna_Game_AI.AIMgrImpl();
        protected ICamera _camera;
        protected GameSystem _gameSystem;
        protected Input _input;
        protected SoundHelper _soundHelper;
        protected GraphicsDeviceManager graphics;
        protected bool _shouldLoadHTWGameComps = true;

        private static XnaGame s_game;
        private GameUI _gameUI;
        private bool b_kill = false;
        private int i_answersCorrect;
        private string s_loadedFilename;
        private string s_mapFilename;
        private string s_triviaFilename;

        // Hunt the Wumpus specific items.
        protected Core.SecretGenerator _secretGenerator;
        protected TriviaMgr _triviaMgr;
        protected UI.DialogMgr _dialogMgr;
        protected MapMgr _mapMgr;
        protected OrbletMgr _orbletMgr;
        protected HighscoreMgr _highscoreMgr;
        private ProfileMgr _profileMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaGame"/> class.
        /// </summary>
        public XnaGame()
        {
            s_game = this;

            LoadGameSystem();

            // Set the title to whatever here.
            Window.Title = "Hunt the Wumpus";
            Window.AllowUserResizing = false;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Gets the game_ instance.
        /// </summary>
        /// <value>
        /// The game_ instance.
        /// </value>
        public static XnaGame Game_Instance
        {
            get { return s_game; }
        }

        /// <summary>
        /// Gets or sets the camera for the scene.
        /// </summary>
        /// <value>
        /// The camera.
        /// </value>
        public ICamera Camera
        {
            get { return _camera; }
            set { _camera = value; }
        }

        /// <summary>
        /// Whether Hunt the wumpus game specific components should be loaded.
        /// Examples include the map manager and trivia manager.
        /// </summary>
        public bool ShouldLoadHTWGameComps
        {
            set { _shouldLoadHTWGameComps = value; }
            get { return _shouldLoadHTWGameComps; }
        }

        /// <summary>
        /// Gets whether the user is in debug mode.
        /// Debug mode is where the scene can be manipulated and the user
        /// is using the free look camera.
        /// Debug mode is determined by which camera is being used.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [debug mode]; otherwise, <c>false</c>.
        /// </value>
        public bool DebugMode
        {
            get { return !(_camera is FirstPersonCamera); }
        }

        /// <summary>
        /// Gets the game system.
        /// </summary>
        /// <value>
        /// The game system.
        /// </value>
        public GameSystem GameSystem
        {
            get { return _gameSystem; }
        }

        /// <summary>
        /// Gets the game UI.
        /// </summary>
        /// <value>
        /// The game UI.
        /// </value>
        public GameUI GameUI
        {
            get { return _gameUI; }
        }

        /// <summary>
        /// Gets the input.
        /// </summary>
        /// <value>
        /// The input.
        /// </value>
        public Input Input
        {
            get { return _input; }
        }

        /// <summary>
        /// Gets if this is the first time the user has played and has not created a profile.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is first play; otherwise, <c>false</c>.
        /// </value>
        public bool IsFirstPlay
        {
            get { return !_profileMgr.ProfilesExist; }
        }

        /// <summary>
        /// Indicates whether the game should be quit immediately.
        /// </summary>
        /// <value>
        ///   <c>true</c> if kill; otherwise, <c>false</c>.
        /// </value>
        public bool Kill
        {
            set { b_kill = value; }
        }

        /// <summary>
        /// Gets the map MGR.
        /// </summary>
        /// <value>
        /// The map MGR.
        /// </value>
        public MapMgr MapMgr
        {
            get { return _mapMgr; }
        }

        /// <summary>
        /// Gets the orblet MGR.
        /// </summary>
        /// <value>
        /// The orblet MGR.
        /// </value>
        public OrbletMgr OrbletMgr
        {
            get { return _orbletMgr; }
        }

        /// <summary>
        /// Gets the profile MGR.
        /// </summary>
        /// <value>
        /// The profile MGR.
        /// </value>
        public ProfileMgr ProfileMgr
        {
            get { return _profileMgr; }
        }

        /// <summary>
        /// Gets the sound helper.
        /// </summary>
        /// <value>
        /// The sound helper.
        /// </value>
        public SoundHelper SoundHelper
        {
            get { return _soundHelper; }
        }

        /// <summary>
        /// Gets the trivia MGR.
        /// </summary>
        /// <value>
        /// The trivia MGR.
        /// </value>
        public TriviaMgr TriviaMgr
        {
            get { return _triviaMgr; }
        }

        /// <summary>
        /// Gets the wumpus player.
        /// </summary>
        /// <value>
        /// The wumpus player.
        /// </value>
        public WumpusGameObj WumpusPlayer
        {
            get
            {
                GamePlayer gamePlayer = _gameSystem.PlayerMgr.GetPlayerOfId(WumpusGameObj.WUMPUS_ID);
                return gamePlayer as WumpusGameObj;
            }
        }

        /// <summary>
        /// Creates the test scene.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public abstract void CreateTestScene(GameTime gameTime);

        /// <summary>
        /// Displays the dialog.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="chainName">Name of the chain.</param>
        /// <param name="fnOnDlgEventFire">The function on dialog event fire.</param>
        /// <param name="allowDlgQuit">if set to <c>true</c> [allow dialog quit].</param>
        public void DisplayDialog(string filename, string chainName, Action<string> fnOnDlgEventFire = null, bool allowDlgQuit = true)
        {
            if (_dialogMgr == null)
                return;

            var pp = GraphicsDevice.PresentationParameters;
            int width = pp.BackBufferWidth;
            int height = pp.BackBufferHeight;

            _dialogMgr.DisplayDialogChain(_gameUI, filename, chainName, width, height, _gameSystem.TextRenderer, fnOnDlgEventFire, allowDlgQuit);
        }

        /// <summary>
        /// Displays a dialog to the user.
        /// </summary>
        /// <param name="dialogTxtStr">The dialog text string.</param>
        /// <param name="speakingID">The speaking identifier.</param>
        public void DisplayDialogMsg(string dialogTxtStr, string speakingID)
        {
            if (_dialogMgr == null)
                return;

            var pp = GraphicsDevice.PresentationParameters;
            int width = pp.BackBufferWidth;
            int height = pp.BackBufferHeight;

            _dialogMgr.DisplayDialogChain(_gameUI, dialogTxtStr, speakingID, width, height, _gameSystem.TextRenderer);
        }

        /// <summary>
        /// Displays the trivia question.
        /// </summary>
        /// <param name="numberOfQuestions">The number of questions.</param>
        /// <param name="resultFn">The result function.</param>
        /// <param name="allowDlgQuit">if set to <c>true</c> [allow dialog quit].</param>
        public void DisplayTriviaQuestion(int numberOfQuestions, Action<int> resultFn, bool allowDlgQuit = true)
        {
            i_answersCorrect = 0;
            DisplayNestedTriviaQuestion(0, numberOfQuestions, resultFn, allowDlgQuit);
        }

        /// <summary>
        /// Displays the trivia question.
        /// </summary>
        /// <param name="resultFn">The result function.</param>
        /// <param name="allowDlgQuit">if set to <c>true</c> [allow dialog quit].</param>
        public void DisplayTriviaQuestion(Action<bool> resultFn, bool allowDlgQuit = true)
        {
            Trivia.TriviaQuestion triviaData = GetRndTriviaQuestion();

            var pp = GraphicsDevice.PresentationParameters;
            int width = pp.BackBufferWidth;
            int height = pp.BackBufferHeight;

            _gameUI.ImmediateNavigateToFrame(UI.TriviaUIFrame.FRAME_ID);
            _gameUI.LoadTriviaData(triviaData, width, height, _gameSystem.TextRenderer, resultFn, allowDlgQuit);
        }

        /// <summary>
        /// Gets a generated game secret (hint)
        /// </summary>
        /// <returns></returns>
        public string GetGameSecretStr()
        {
            if (_secretGenerator == null)
                return "Secret generator not set.";
            return _secretGenerator.GetSecretMsgStr(this);
        }

        /// <summary>
        /// Gets the game user player.
        /// </summary>
        /// <returns></returns>
        public GameUserPlayer GetGameUserPlayer()
        {
            return _gameSystem.GetUserPlayer() as GameUserPlayer;
        }

        /// <summary>
        /// Gets the high scores.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public abstract string[] GetHighScores();

        /// <summary>
        /// Loads the map.
        /// </summary>
        /// <param name="mapFilename">The map filename.</param>
        /// <param name="displayName">The display name.</param>
        public void LoadMap(string mapFilename, string displayName)
        {
            s_mapFilename = mapFilename;
            s_loadedFilename = displayName;
        }

        /// <summary>
        /// Loads the trivia.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void LoadTrivia(string filename)
        {
            s_triviaFilename = filename;
        }

        public void OnLevelQuit()
        {
            for (int i = 0; i < _gameSystem.ProcessMgr.GameProcesses.Count; ++i)
            {
                var gameProc = _gameSystem.ProcessMgr.GameProcesses[i];
                gameProc.Kill = true;
                gameProc.OnKill();
                _gameSystem.ProcessMgr.GameProcesses.RemoveAt(i--);
            }

            _gameSystem.ProcessMgr.UpdateProccesses(null);

            //_soundHelper.UnloadContent();
            //_gameSystem.UnloadContent();
            _gameSystem.ClearAllData();

            _gameSystem = new GameSystem();
            _gameSystem.GraphicsSettings = GetGraphicsSettings();

            _gameSystem.SetGraphicsSettings(graphics);
            _gameSystem.SetAIMgr(_aiMgr);

            _aiMgr = new Xna_Game_AI.AIMgrImpl();
            _aiMgr.SelectedEdge = 1;
            LoadContent();

            _mapMgr = null;
        }

        /// <summary>
        /// Draws the frame.
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Draw.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Doesn't matter what color.
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // This code section draws the AI navigation paths as lines. However, this is an expesive operation
            // and increases the frame rate a lot.

            //if (_aiMgr.PathGraphs.Count > 0)
            //{
            //    var graph = _aiMgr.PathGraphs.ElementAt(0);

            //    graph.AddEdgesForRendering(_gameSystem.PrimitivesRenderer, Color.Red, Color.Blue, new List<int>());
            //}

            //List<Vector3[]> constraintLines = _gameSystem.PhysicsMgr.GetConstraintDebugLines();
            //foreach (Vector3[] line in constraintLines)
            //{
            //    _gameSystem.PrimitivesRenderer.AddRenderLine(line[0], line[1], Color.Green);
            //}

            (_gameSystem.Renderer as RendererAccess).RenderLines = true;
            if (_mapMgr != null)
                _mapMgr.DisplayGuideLines(_gameSystem.PrimitivesRenderer, GetGameUserPlayer(), WumpusPlayer);

            _gameSystem.Draw(gameTime, _gameUI.ExclusiveRendering);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Gets the camera.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected abstract FreeLookCamera GetCamera();

        /// <summary>
        /// Gets the graphics settings.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected abstract GraphicsSettings GetGraphicsSettings();

        /// <summary>
        /// Gets the player weapon object.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected abstract WeaponObj GetPlayerWeaponObj();

        /// <summary>
        /// Gets the random trivia question.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected abstract Trivia.TriviaQuestion GetRndTriviaQuestion();

        /// <summary>
        /// Called after the Game and GraphicsDevice are created, but before LoadContent.  Reference page contains code sample.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            _input = new Input(this);
        }

        /// <summary>
        /// Loads all of the game content as in resources.
        /// </summary>
        protected override void LoadContent()
        {
            _gameSystem.LoadContent(this);

            _camera = (FreeLookCamera)_gameSystem.CreateCamera(new FreeLookCamera());
            _camera.Position = new Vector3(0f, 20f, 6f);
            _camera.Pitch = MathHelper.ToRadians(-30f);

            _gameSystem.OnSceneLoad += OnSceneLoad;

            _profileMgr = new ProfileMgr();
            _profileMgr.LoadProfiles();

            UI.StartMsgUIFrame.HasRan = _profileMgr.GetNumberOfDataElements() != 0;

            var pp = GraphicsDevice.PresentationParameters;
            int width = pp.BackBufferWidth;
            int height = pp.BackBufferHeight;

            _triviaMgr = new Trivia.TriviaMgr();

            _gameUI = new GameUI();

            _gameSystem.DrawUIFunc += DrawUI;
            _gameUI.LoadContent(width, height, _gameSystem.TextRenderer);

            _highscoreMgr = new HighscoreMgr();
            _highscoreMgr.LoadContent();

            _soundHelper = new SoundHelper();
            _soundHelper.LoadContent();
        }

        /// <summary>
        /// Called when the scene objects and data is to load.
        /// </summary>
        /// <param name="loadingFilename">The load filename.</param>
        protected virtual void OnSceneLoad(string loadingFilename)
        {
            LoadAnimationData();

            // Create the defaults for the scene.
            const float playerScale = 1.5f;
            UserPlayer = _gameSystem.CreateUserPlayer(playerScale, new GameUserPlayer(_gameSystem), "cube");

            // Get the camera specified.
            _camera = GetCamera();

            UserPlayer.SetWeapon(GetPlayerWeaponObj());

            PhasedAnimObj hands = _gameSystem.CreatePhasedAnimObj("Models/crossbowhands", "player hands", "ArmatureAction_001");
            UserPlayer.SetHandsModel(hands);
            hands.Scale = 1f;

            // For the hands.
            hands.SubsetMaterials[0].DiffuseMap = ResourceMgr.LoadGameTexture("DiffuseMaps/CrossbowDiffuse1");
            hands.SubsetMaterials[1].DiffuseMap = ResourceMgr.LoadGameTexture("DiffuseMaps/CrossbowDiffuse2");
            hands.SubsetMaterials[2].DiffuseMap = ResourceMgr.LoadGameTexture("HandsDiffuse");
            hands.Position = new Vector3(0f, 0f, 0f);

            UserPlayer.SetCamera(_camera as FreeLookCamera);

            // The offset of the ray fire can be modified here.
            const float defaultYRayFireOffset = 3f;
            if (_camera is FirstPersonCamera)
            {
                UserPlayer.YRayFireOffset = (_camera as FirstPersonCamera).EyeOffset.Y;
            }
            else
            {
                UserPlayer.YRayFireOffset = defaultYRayFireOffset;
            }

            UserPlayer.ZRayDistance = -1000f;
            UserPlayer.ZRayStart = -2f;
            UserPlayer.LocalTrans = new Vector3(0.6f, -0.65f, -1.35f);
            UserPlayer.LocalRot = Quaternion.CreateFromYawPitchRoll(0f, -MathHelper.PiOver2, MathHelper.Pi);

            if (_shouldLoadHTWGameComps)
            {
                _triviaMgr.LoadTrivia(s_triviaFilename);

                _orbletMgr = new OrbletMgr();

                _mapMgr = new MapMgr();

                _mapMgr.LoadContent(this, s_mapFilename);

                _dialogMgr = new UI.DialogMgr();
                // Custom dialogs can be set here.
                _dialogMgr.LoadContent
                    (
                    "OrbletDialog.xml"
                    );

                _secretGenerator = new Core.SecretGenerator();
            }
            else
            {
                _triviaMgr = null;
                _orbletMgr = null;
                _mapMgr = null;
                _dialogMgr = null;
                _secretGenerator = null;
            }

            CreateHoldableObjs();
        }

        /// <summary>
        /// Called when graphics resources need to be unloaded. Override this method to unload any game-specific graphics resources.
        /// </summary>
        protected override void UnloadContent()
        {
            // Unload in the opposite order of loading.
            _soundHelper.UnloadContent();
            _gameUI.UnloadContent();
            _gameSystem.UnloadContent();
        }

        /// <summary>
        /// Reference page contains links to related conceptual articles.
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Update.</param>
        protected override void Update(GameTime gameTime)
        {
            // Exit the application?
            if (b_kill)
                this.Exit();

            // Check if the player or the wumpus has died.

            if (_gameUI.ExclusiveInput)
                _gameUI.ProcessInput(_input, this);
            else
                _input.ProccessInput(this, gameTime);

            _gameUI.Update();

            if (!_gameUI.ExclusiveUpdate && UpdateGame)
            {
                var userPlayer = GetGameUserPlayer();
                var wumpusPlayer = WumpusPlayer;

                // If the hunt the wumpus game components aren't loaded then there is no winning or lossing.
                if (((userPlayer == null && wumpusPlayer != null) ||
                    (userPlayer != null && (userPlayer.WeaponObj.TotalAmmo + userPlayer.WeaponObj.CurrentAmmo == 0)) ||
                    (userPlayer != null && userPlayer.GoldCount < 0)) && _shouldLoadHTWGameComps)
                {
                    // The player lost.
                    OnLoss();
                }

                if (wumpusPlayer == null && userPlayer != null && _shouldLoadHTWGameComps)
                {
                    // The player won.
                    OnWin();
                }

                // Update everything else.
                _gameSystem.Update(gameTime);

                if (_mapMgr != null)
                    _mapMgr.Update(gameTime);

                if (_orbletMgr != null)
                    _orbletMgr.Update(gameTime, GetGameUserPlayer());
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Creates the holdable objs.
        /// </summary>
        private void CreateHoldableObjs()
        {
            BaseLogic.Manager.ObjectMgr objMgr = _gameSystem.ObjMgr;

            var flareStaticObjs = from gameObj in objMgr.GetDataElements()
                                  where gameObj is StaticObj
                                  where gameObj.ActorID.Contains("flare")
                                  select gameObj as StaticObj;

            for (int i = 0; i < flareStaticObjs.Count(); ++i)
            {
                StaticObj flareStaticObj = flareStaticObjs.ElementAt(i);
                FlareHodableObj holdableFlare = new FlareHodableObj(flareStaticObj, _gameSystem);
                holdableFlare.HoldingOffset = new Vector3(0f, 0f, -5f);
                _gameSystem.AddRenderObj(holdableFlare, true);
            }

            var throwableObjs = from gameObj in objMgr.GetDataElements()
                                where gameObj is StaticObj
                                where gameObj.ActorID.Contains("throw")
                                select gameObj as StaticObj;

            for (int i = 0; i < throwableObjs.Count(); ++i)
            {
                StaticObj throwableStaticObj = throwableObjs.ElementAt(i);
                ThrowableObj throwableGameObj = new ThrowableObj(throwableStaticObj, objMgr);
                throwableGameObj.HoldingOffset = new Vector3(0f, 0f, -5f);
                _gameSystem.AddRenderObj(throwableGameObj, true);
            }

            var danceBtns = from gameObj in objMgr.GetDataElements()
                            where gameObj is StaticObj
                            where gameObj.ActorID.Contains("dance")
                            select gameObj as StaticObj;

            for (int i = 0; i < danceBtns.Count(); ++i)
            {
                StaticObj danceBtnStaticObj = danceBtns.ElementAt(i);
                DancePartyBtn danceBtnGameObj = new DancePartyBtn(danceBtnStaticObj, this);
                danceBtnGameObj.HoldingOffset = new Vector3(0f, 0f, -5f);
                _gameSystem.AddRenderObj(danceBtnGameObj, true);
            }
        }

        /// <summary>
        /// Displays the nested trivia question.
        /// </summary>
        /// <param name="currentLayer">The current layer.</param>
        /// <param name="numberOfQuestions">The number of questions.</param>
        /// <param name="resultFn">The result function.</param>
        /// <param name="allowQuit">if set to <c>true</c> [allow quit].</param>
        private void DisplayNestedTriviaQuestion(int currentLayer, int numberOfQuestions, Action<int> resultFn, bool allowQuit = true)
        {
            DisplayTriviaQuestion((bool result) =>
            {
                if (result)
                    ++i_answersCorrect;
                if (currentLayer < numberOfQuestions - 1)
                    DisplayNestedTriviaQuestion(++currentLayer, numberOfQuestions, resultFn, allowQuit);
                else
                    resultFn(i_answersCorrect);
            }, allowQuit);
        }

        /// <summary>
        /// Draws the UI.
        /// </summary>
        /// <param name="textRenderer">The text renderer.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        private void DrawUI(TextRenderer textRenderer, int width, int height)
        {
            _gameUI.DrawUI(textRenderer, width, height, this);
        }

        /// <summary>
        /// Loads the animation data.
        /// </summary>
        private void LoadAnimationData()
        {
            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(TimeSpan.Zero, new TimeSpan(0, 0, 0, 1, 666), "disaprove",
                "Models/MarineDude/man", false));
            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(TimeSpan.Zero, new TimeSpan(0, 0, 0, 1, 267), "moving", "Models/dude", true));

            // Engineer.
            const string engineerFilename = "Models//engineer";
            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(TimeSpan.FromSeconds(0.04166), TimeSpan.FromSeconds(0.875),
                "moving", engineerFilename, true));
            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(TimeSpan.FromSeconds(0.875), TimeSpan.FromSeconds(1.4166),
                "rest->raise weapon", engineerFilename, false));
            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(TimeSpan.FromSeconds(1.4166), TimeSpan.FromSeconds(2.6666),
                "raise weapon->reload", engineerFilename, false));
            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(TimeSpan.FromSeconds(2.6666), TimeSpan.FromSeconds(3.08333),
                "raise weapon->fire", engineerFilename, false));

            // Player hands.
            const string handsFilename = "Models/crossbowhands";
            Vector3 restingOffset = new Vector3(0f, 0f, 0f);
            Vector3 holdingOffset = new Vector3(0.6f, -0.55f, -1.50f);

            Vector3 offsetVec = new Vector3(-0.0f, 0.0f, 0.0f);
            restingOffset += offsetVec;
            holdingOffset += offsetVec;

            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 416),
                "rest->raise weapon", handsFilename, false, holdingOffset));
            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(new TimeSpan(0, 0, 0, 0, 416), new TimeSpan(0, 0, 0, 0, 625),
                "raise weapon->fire", handsFilename, false, holdingOffset));
            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(new TimeSpan(0, 0, 0, 0, 625), new TimeSpan(0, 0, 0, 2, 708),
                "raise weapon->reload", handsFilename, false, holdingOffset));
            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(new TimeSpan(0, 0, 0, 2, 708), new TimeSpan(0, 0, 0, 3, 125),
                "raise weapon->rest", handsFilename, false, restingOffset));
            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(new TimeSpan(0, 0, 0, 3, 166), new TimeSpan(0, 0, 0, 4, 0),
                "raise weapon->walk", handsFilename, true, holdingOffset));
            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(new TimeSpan(0, 0, 0, 4, 41), new TimeSpan(0, 0, 0, 4, 458),
                "unloaded rest->raise weapon", handsFilename, false, holdingOffset));
            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(new TimeSpan(0, 0, 0, 4, 458), new TimeSpan(0, 0, 0, 5, 291),
                "unloaded raise weapon->walk", handsFilename, true, holdingOffset));

            // Wumpus
            const string wumpusFilename = "Models/Wumpus/wumpus";
            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0, 1, 417), "walking", wumpusFilename, true));
            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(new TimeSpan(0, 0, 0, 1, 417), new TimeSpan(0, 0, 0, 2, 83), "attacking", wumpusFilename, false));
            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(new TimeSpan(0, 0, 0, 2, 83), new TimeSpan(0, 0, 0, 2, 291), "go to sleep", wumpusFilename, false));
            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(new TimeSpan(0, 0, 0, 2, 291), new TimeSpan(0, 0, 0, 2, 500), "wake up", wumpusFilename, false));

            // UH-60
            const string uh60Filename = "Models/uh60";
            _gameSystem.ObjMgr.AddAnimationSpan(new AnimationSpan(new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0, 625), "choppin", uh60Filename, true));
        }

        /// <summary>
        /// Loads the game system.
        /// </summary>
        private void LoadGameSystem()
        {
            graphics = new GraphicsDeviceManager(this);

            _gameSystem = new GameSystem();
            _gameSystem.GraphicsSettings = GetGraphicsSettings();

            _gameSystem.SetGraphicsSettings(graphics);
            _gameSystem.SetAIMgr(_aiMgr);

            _aiMgr.SelectedEdge = 1;
        }

        /// <summary>
        /// Called when [loss].
        /// </summary>
        private void OnLoss()
        {
            GameUserPlayer userPlayer = GetGameUserPlayer();
            string causeStr = null;
            if (userPlayer == null)
            {
                causeStr = "Player died.";
            }
            else if (userPlayer.WeaponObj.CurrentAmmo + userPlayer.WeaponObj.TotalAmmo == 0)
            {
                causeStr = "Ran out of arrows.";
            }

            UI.LossMsgUIFrame.CauseTxtStr = causeStr;
            _gameUI.NavigateToFrame(UI.LossMsgUIFrame.FRAME_ID);

            Input.VibrateController(0f, 0f);
        }

        /// <summary>
        /// Called when [win].
        /// </summary>
        private void OnWin()
        {
            int arrowCount = UserPlayer.WeaponObj.CurrentAmmo + UserPlayer.WeaponObj.TotalAmmo;
            int turnCount = _mapMgr.DoorOpenCount;
            int goldCount = GetGameUserPlayer().GoldCount;

            int computedScore = HighscoreMgr.ComputeScore(turnCount, goldCount, arrowCount);

            HighScoreData highscoreData = new HighScoreData(_profileMgr.SelectedProfile.Name, computedScore, s_loadedFilename, arrowCount, goldCount, turnCount);
            bool isHighscore = _highscoreMgr.AddScore(highscoreData);

            _gameUI.LoadWinUIFrame(highscoreData, isHighscore);

            _gameUI.NavigateToFrame(UI.WinMsgUIFrame.FRAME_ID);

            Input.VibrateController(0f, 0f);
        }
    }
}