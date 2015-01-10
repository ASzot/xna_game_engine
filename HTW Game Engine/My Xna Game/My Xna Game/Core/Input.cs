#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System.Collections.Generic;
using BaseLogic;
using BaseLogic.Camera;
using BaseLogic.Object;
using BaseLogic.Process;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RenderingSystem;

namespace My_Xna_Game
{
    /// <summary>
    /// Handless all input from the user.
    /// </summary>
    public class Input
    {
        public static char KeyboardInteractionKey = 'X';
        public static char XboxInteractionBtn = 'X';
        public static float CameraUpdateSpeed = 7.5f;

        private const float CTRL_MAX_SEN = 0.1f;
        private const float CTRL_MIN_SEN = 0.01f;
        private const float MAX_SENSITIVITY = 10.0f;
        private const float MIN_SENSITIVITY = 1.0f;
        private InputManager _inputMgr;
        private MouseState _lastMouseState;
        private List<float> _prevY = new List<float>();
        private bool b_lockMovement = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Input"/> class.
        /// </summary>
        /// <param name="game">The game.</param>
        public Input(Microsoft.Xna.Framework.Game game)
        {
            _inputMgr = new InputManager(game);
            _inputMgr.ControllerSensitivity = 0.05f;
        }

        /// <summary>
        /// Gets the input MGR.
        /// </summary>
        /// <value>
        /// The input MGR.
        /// </value>
        public InputManager InputMgr
        {
            get { return _inputMgr; }
        }

        /// <summary>
        /// Gets or sets the input sensitivity.
        /// </summary>
        /// <value>
        /// The input sensitivity.
        /// </value>
        public float InputSensitivity
        {
            get
            {
                float senMapped = BaseLogic.Core.GameMath.Map(_inputMgr.ControllerSensitivity, CTRL_MIN_SEN, CTRL_MAX_SEN, MIN_SENSITIVITY, MAX_SENSITIVITY);
                return senMapped;
            }
            set
            {
                float senMapped = BaseLogic.Core.GameMath.Map(value, MIN_SENSITIVITY, MAX_SENSITIVITY, CTRL_MIN_SEN, CTRL_MAX_SEN);
                _inputMgr.ControllerSensitivity = senMapped;
            }
        }

        /// <summary>
        /// Gets the interaction key.
        /// </summary>
        /// <value>
        /// The interaction key.
        /// </value>
        public char InteractionKey
        {
            get { return KeyboardInteractionKey; }
        }

        /// <summary>
        /// Gets or sets whether the user mouse or game pad should be locked on input for the game window alone.
        /// If not the user can mouse away from the program.
        /// Normally LockMovement=false in debug mode and true in non debug mode.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [lock movement]; otherwise, <c>false</c>.
        /// </value>
        public bool LockMovement
        {
            get { return b_lockMovement; }
            set { b_lockMovement = value; }
        }

        /// <summary>
        /// Proccesses the input.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="gameTime">The game time.</param>
        public void ProccessInput(XnaGame game, GameTime gameTime)
        {
            GameSystem gameSystem = game.GameSystem;
            ICamera cam = game.Camera;
            float speed = CameraUpdateSpeed;
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector3 moveVec = Vector3.Zero;

            if (_inputMgr.KeyboardState.IsKeyDown(Keys.LeftControl) && gameSystem.InDebugMode)
            {
                PollFormCreationInput(game);
            }
            else if (_inputMgr.KeyboardState.IsKeyDown(Keys.LeftAlt))
            {
                PollMiscInput(game, gameTime);
            }
            else if (!gameSystem.HasActiveWindows)
            {
                if (!b_lockMovement)
                {
                    if (_inputMgr.UsingGamePad)
                        moveVec = PollMovementMethodGamePad();
                    else
                        moveVec = PollMovementMethodKeyboard();
                }

                if (_inputMgr.KeyboardState.IsKeyDown(Keys.Escape) || _inputMgr.WasPressed(Buttons.Start))
                    game.GameUI.NavigateToFrame(UI.GameMenuUIFrame.FRAME_ID);

                if (!gameSystem.InDebugMode)
                {
                    PollGameSpecificInput(game, gameTime);
                    if (game.UserPlayer.HasWeaponAimed)
                        speed /= 2;
                    Vector3 vel = game.UserPlayer.GetVelocity();
                    const float yMovementThresh = 0.001f;
                    bool prevOver = false;
                    foreach (float f in _prevY)
                    {
                        if (f > yMovementThresh)
                        {
                            prevOver = true;
                            break;
                        }
                    }

                    const float jumpSpeedModifier = 4f;
                    if (vel.Y > yMovementThresh || prevOver)
                        speed /= jumpSpeedModifier;

                    _prevY.Add(vel.Y);
                    if (_prevY.Count > 30)
                        _prevY.RemoveAt(0);
                }
                else
                {
                    if (_inputMgr.MouseState.MiddleButton == ButtonState.Pressed
                        && _lastMouseState.MiddleButton == ButtonState.Released)
                    {
                        // Allows the selection of objects.
                        game.GameSystem.CastEditRay();
                    }
                    if (_inputMgr.KeyboardState.IsKeyDown(Keys.Q) ||
                        _inputMgr.GamePadState.Buttons.LeftShoulder == ButtonState.Pressed)
                        speed *= 5f;
                    if (_inputMgr.KeyboardState.IsKeyDown(Keys.E) ||
                        _inputMgr.GamePadState.Buttons.RightShoulder == ButtonState.Pressed)
                        speed /= 5f;
                }
            }

            moveVec.Y = 0f;
            cam.Move(moveVec, speed, dt);

            if (_inputMgr.UsingGamePad)
                PollLookMethodGamePad(cam);
            else
                PollLookMethodMouse(cam, game, gameSystem);
        }

        /// <summary>
        /// Vibrates the controller.
        /// </summary>
        /// <param name="leftMotor">The left motor.</param>
        /// <param name="rightMotor">The right motor.</param>
        public void VibrateController(float leftMotor, float rightMotor)
        {
            if (_inputMgr.UsingGamePad)
            {
                GamePad.SetVibration(PlayerIndex.One, leftMotor, rightMotor);
            }
        }

        /// <summary>
        /// Keeps the vibrations running until a number of milliseconds has elapsed.
        /// </summary>
        /// <param name="leftMotor">The vibration of the left trigger.</param>
        /// <param name="rightMotor">The vibration of the right trigger.</param>
        /// <param name="milliseconds">The number of milliseconds the vibrations will run for.</param>
        public void VibrateController(float leftMotor, float rightMotor, uint milliseconds)
        {
            VibrateController(leftMotor, rightMotor);

            var waitProc = new BaseLogic.Process.WaitEventProcess(milliseconds, () =>
                {
                    VibrateController(0f, 0f);
                });
            XnaGame.Game_Instance.GameSystem.AddGameProcess(waitProc);
        }

        /// <summary>
        /// Polls the form creation input.
        /// (Detects whether a user input to open an editor window).
        /// </summary>
        /// <param name="game">The game.</param>
        private void PollFormCreationInput(XnaGame game)
        {
            GameSystem gameSystem = game.GameSystem;

            // The user cannot open editor windows in play mode.
            if (!game.DebugMode)
                return;

            var keysPressed = _inputMgr.KeysPressed;
            bool shiftDown = _inputMgr.KeyboardState.IsKeyDown(Keys.LeftShift);

            foreach (var key in keysPressed)
            {
                switch (key)
                {
                    case Keys.S:
                        if (shiftDown || gameSystem.LastSaveFilename == null)
                        {
                            gameSystem.SaveSceneDialog();
                        }
                        else if (!shiftDown)
                            gameSystem.SaveScene(gameSystem.LastSaveFilename);
                        break;

                    case Keys.L:
                        gameSystem.LoadSceneDialog();
                        break;

                    case Keys.W:
                        gameSystem.CreateForm(BaseLogic.Manager.EditorFormType.GameLightEditorForm);
                        break;

                    case Keys.O:
                        gameSystem.CreateForm(BaseLogic.Manager.EditorFormType.GameObjEditorForm);
                        break;

                    case Keys.A:
                        gameSystem.CreateForm(BaseLogic.Manager.EditorFormType.SSAOSettingsForm);
                        break;

                    case Keys.T:
                        gameSystem.CreateForm(BaseLogic.Manager.EditorFormType.ShadowSettingsForm);
                        break;

                    case Keys.P:
                        gameSystem.CreateForm(BaseLogic.Manager.EditorFormType.ParticleSettingsForm);
                        break;

                    case Keys.G:
                        gameSystem.CreateForm(BaseLogic.Manager.EditorFormType.RendererSettingsForm);
                        break;

                    case Keys.N:
                        gameSystem.CreateForm(BaseLogic.Manager.EditorFormType.AIGraphEditorForm);
                        break;

                    case Keys.E:
                        gameSystem.CreateForm(BaseLogic.Manager.EditorFormType.SceneSettingsForm);
                        break;

                    case Keys.U:
                        gameSystem.CreateForm(BaseLogic.Manager.EditorFormType.RealTimeEventEditorForm);
                        break;
                }
            }
        }

        /// <summary>
        /// Polls the game specific input.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="gameTime">The game time.</param>
        private void PollGameSpecificInput(XnaGame game, GameTime gameTime)
        {
            GameSystem gameSystem = game.GameSystem;

            Game_Objects.GameUserPlayer gameUserPlayer = game.GetGameUserPlayer();

            float jumpPower = BaseLogic.Player.UserPlayer.JUMP_POWER;

            if (_inputMgr.UsingGamePad)
            {
                if (_inputMgr.WasPressed(Buttons.A))
                {
                    game.UserPlayer.Jump(jumpPower);
                }
                else if (_inputMgr.WasPressed(Buttons.Y))
                {
                    game.UserPlayer.ToggleWeaponRaised();
                }
                else if (_inputMgr.WasPressed(Buttons.X))
                {
                    game.UserPlayer.Reload();
                }
                else if (_inputMgr.WasPressed(Buttons.B) && game.MapMgr != null)
                {
                    game.MapMgr.OnInteractionButton(game);
                }
                else if (_inputMgr.WasPressed(Buttons.Back) && game.OrbletMgr != null)
                {
                    game.OrbletMgr.HelperOrblet.ToggleSummon(game);
                }
                else if (_inputMgr.WasPressed(Buttons.DPadLeft))
                {
                    gameUserPlayer.ToggleFlashLight();
                }
                else if (_inputMgr.WasPressed(Buttons.DPadUp))
                {
                    gameUserPlayer.OnThrowBtn();
                }
                else if (_inputMgr.WasPressed(Buttons.RightShoulder))
                {
                    gameUserPlayer.OnInteractionBtn();
                }
            }
            else
            {
                var keysPressed = _inputMgr.KeysPressed;
                foreach (var key in keysPressed)
                {
                    switch (key)
                    {
                        case Keys.Space:
                            if (game.UserPlayer != null)
                                game.UserPlayer.Jump(jumpPower);
                            break;

                        case Keys.E:
                            if (!gameUserPlayer.IsHoldingObj)
                                game.UserPlayer.ToggleWeaponRaised();
                            break;

                        case Keys.X:
                            if (game.MapMgr != null)
                                game.MapMgr.OnInteractionButton(game);
                            break;

                        case Keys.R:
                            if (!gameUserPlayer.IsHoldingObj && game.UserPlayer.HasWeaponRaised)
                                game.UserPlayer.Reload();
                            break;

                        case Keys.B:
                            gameUserPlayer.OnDropBtn();
                            break;

                        case Keys.N:
                            if (gameUserPlayer != null)
                                gameUserPlayer.OnInteractionBtn();
                            break;

                        case Keys.H:
                            if (game.OrbletMgr != null)
                                game.OrbletMgr.HelperOrblet.ToggleSummon(game);
                            break;

                        case Keys.F:
                            if (gameUserPlayer != null)
                                gameUserPlayer.ToggleFlashLight();
                            break;
                    }
                }
            }

            if (_inputMgr.MouseState.MiddleButton == ButtonState.Pressed
                && _lastMouseState.MiddleButton == ButtonState.Released
                && game.UserPlayer.HasWeaponRaised)
            {
                game.UserPlayer.ToggleAimWeapon();
            }
            else if (_inputMgr.MouseState.MiddleButton == ButtonState.Released      // TODO: Should this be deleted?
                && _lastMouseState.MiddleButton == ButtonState.Pressed
                && game.UserPlayer.HasWeaponAimed)
            {
                //game.UserPlayer.ToggleAimWeapon();
            }

            // Check if the player even has a weapon.
            if (game.UserPlayer != null && !gameSystem.HasActiveWindows)
            {
                WeaponObj weapon = game.UserPlayer.WeaponObj;
                if (weapon != null)
                {
                    bool fire;

                    if (_inputMgr.UsingGamePad)
                    {
                        fire = (!weapon.Automatic) ? (_inputMgr.LastGamePadState.Triggers.Right <= 0.95f)
                            && (_inputMgr.GamePadState.Triggers.Right > 0.95f)
                            : _inputMgr.GamePadState.Triggers.Right > 0.95f;
                    }
                    else
                    {
                        fire = (!weapon.Automatic) ? (_inputMgr.MouseState.LeftButton == ButtonState.Pressed)
                            && (_lastMouseState.LeftButton == ButtonState.Released) :
                            (_inputMgr.MouseState.LeftButton == ButtonState.Pressed);
                    }

                    if (fire && game.UserPlayer.HasWeaponRaised && weapon.CanFire(gameTime))
                    {
                        game.UserPlayer.ShootWeapon(gameTime);
                    }
                }
            }
        }

        /// <summary>
        /// Polls the look method game pad.
        /// </summary>
        /// <param name="cam">The cam.</param>
        private void PollLookMethodGamePad(ICamera cam)
        {
            Vector2 rightThumbStick = _inputMgr.GamePadState.ThumbSticks.Right;
            Vector2 yawPitch = new Vector2(rightThumbStick.X, rightThumbStick.Y);
            yawPitch *= _inputMgr.ControllerSensitivity;
            if (rightThumbStick != Vector2.Zero)
            {
                cam.Yaw -= yawPitch.X;
                cam.Pitch += yawPitch.Y;
            }
        }

        /// <summary>
        /// Polls the look method mouse.
        /// </summary>
        /// <param name="cam">The cam.</param>
        /// <param name="game">The game.</param>
        /// <param name="gameSystem">The game system.</param>
        private void PollLookMethodMouse(ICamera cam, XnaGame game, GameSystem gameSystem)
        {
            Vector2 yawPitch = new Vector2(_inputMgr.MouseDelta.X, _inputMgr.MouseDelta.Y);
            yawPitch *= _inputMgr.MouseSensitivity;
            if (yawPitch != Vector2.Zero)
            {
                cam.Yaw -= yawPitch.X;
                cam.Pitch -= yawPitch.Y;
            }

            if (_inputMgr.MouseState.RightButton == ButtonState.Pressed && !gameSystem.HasActiveWindows)
                _inputMgr.CaptureMouse = true;
            else
                _inputMgr.CaptureMouse = false;

            _lastMouseState = new MouseState(_inputMgr.MouseState.X, _inputMgr.MouseState.Y, _inputMgr.MouseState.ScrollWheelValue,
                _inputMgr.MouseState.LeftButton, _inputMgr.MouseState.MiddleButton, _inputMgr.MouseState.RightButton,
                _inputMgr.MouseState.XButton1, _inputMgr.MouseState.XButton2);

            game.GameUI.UpdateUIMousePos(_inputMgr.MouseState, game.Window);
        }

        /// <summary>
        /// Polls the misc input.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="gameTime">The game time.</param>
        private void PollMiscInput(XnaGame game, GameTime gameTime)
        {
            GameSystem gameSystem = game.GameSystem;
            ICamera cam = game.Camera;

            if (_inputMgr.KeyboardState.IsKeyDown(Keys.RightAlt))
                game.UpdateGame = false;
            else
                game.UpdateGame = true;

            var keysPressed = _inputMgr.KeysPressed;
            foreach (var key in keysPressed)
            {
                switch (key)
                {
                    case Keys.J:
                        StaticObj ballStaticObj = gameSystem.CreateStaticObj("cube");
                        ballStaticObj.Scale = 1.0f;
                        ballStaticObj.Position = cam.Position + cam.Direction * 2.0f;
                        Matrix transform = cam.Transform;
                        Quaternion rot;
                        Vector3 pos, scale;

                        transform.Decompose(out scale, out rot, out pos);
                        ballStaticObj.Rotation = rot;

                        const float spawnSpeed = 0f;
                        ballStaticObj.SetLinearVelocity(cam.Direction * spawnSpeed);
                        break;

                    case Keys.D1:
                        (gameSystem.Renderer as RendererAccess).RenderedTarget = RenderingSystem.RendererImpl.RenderedTarget.FinalRT;
                        break;

                    case Keys.D2:
                        (gameSystem.Renderer as RendererAccess).RenderedTarget = RenderingSystem.RendererImpl.RenderedTarget.DepthRT;
                        break;

                    case Keys.D3:
                        (gameSystem.Renderer as RendererAccess).RenderedTarget = RenderingSystem.RendererImpl.RenderedTarget.NormalRT;
                        break;

                    case Keys.D4:
                        (gameSystem.Renderer as RendererAccess).RenderedTarget = RenderingSystem.RendererImpl.RenderedTarget.LightRT;
                        break;

                    case Keys.D5:
                        (gameSystem.Renderer as RendererAccess).RenderedTarget = RenderingSystem.RendererImpl.RenderedTarget.LightSpecRT;
                        break;

                    case Keys.D6:
                        (gameSystem.Renderer as RendererAccess).RenderedTarget = RenderingSystem.RendererImpl.RenderedTarget.SsaoRT;
                        break;

                    case Keys.F:
                        game.Camera = (FreeLookCamera)gameSystem.CreateCamera(new FreeLookCamera());
                        game.Camera.Position = new Vector3(0f, 10f, 0f);
                        break;

                    case Keys.G:
                        game.Camera = (FirstPersonCamera)gameSystem.CreateCamera(new FirstPersonCamera(game.UserPlayer));
                        game.UserPlayer.SetCamera(game.Camera as FirstPersonCamera);
                        game.Camera.Yaw = MathHelper.PiOver2;
                        break;

                    case Keys.R:
                        gameSystem.PlayerMgr.KillAll();
                        break;

                    case Keys.P:
                        // A song could be set up to play here
                        //var songProc = new SongProcess("Some Song");
                        //gameSystem.AddGameProcess(songProc);
                        break;

                    case Keys.T:
                        // This is the test scene which is defined programatically.
                        // Just for debugging purposes.
                        game.CreateTestScene(gameTime);
                        break;
                }
            }
        }

        /// <summary>
        /// Polls the movement method game pad.
        /// </summary>
        /// <returns></returns>
        private Vector3 PollMovementMethodGamePad()
        {
            Vector2 thumbPadLeft = _inputMgr.GamePadState.ThumbSticks.Left;

            return new Vector3(-thumbPadLeft.X, 0f, thumbPadLeft.Y);
        }

        /// <summary>
        /// Polls the movement method keyboard.
        /// </summary>
        /// <returns></returns>
        private Vector3 PollMovementMethodKeyboard()
        {
            Vector3 moveVec = Vector3.Zero;

            if (_inputMgr.KeyboardState.IsKeyDown(Keys.W))
                moveVec += Vector3.UnitZ;
            if (_inputMgr.KeyboardState.IsKeyDown(Keys.A))
                moveVec += Vector3.UnitX;
            if (_inputMgr.KeyboardState.IsKeyDown(Keys.D))
                moveVec += -Vector3.UnitX;
            if (_inputMgr.KeyboardState.IsKeyDown(Keys.S))
                moveVec += -Vector3.UnitZ;

            return moveVec;
        }
    }
}
