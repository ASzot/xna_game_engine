#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BaseLogic
{
    /// <summary>
    /// Manages core input functionality.
    /// </summary>
    public sealed class InputManager : GameComponent, IInput
    {
        /// <summary>
        /// The default controler sensitivity in looking around the scene.
        /// </summary>
        public const float DEF_CONTRL_SENS = 0.05f;

        /// <summary>
        /// The default mouse sensitivity in looking around the scene.
        /// </summary>
        private const float DEF_MOUSE_SENS = 0.005f;

        /// <summary>
        /// The mouse center position x
        /// </summary>
        private const int MouseCenterPositionX = 100;

        /// <summary>
        /// The mouse center position y
        /// </summary>
        private const int MouseCenterPositionY = 100;

        /// <summary>
        /// Whether the mouse will be captured and outside mouse movement blocked.
        /// </summary>
        private bool _captureMouse = true;

        /// <summary>
        /// The controller sensitivity with the sticks.
        /// </summary>
        private float _controllerSensitivity = DEF_CONTRL_SENS;

        /// <summary>
        /// The game pad state.
        /// </summary>
        private GamePadState _gamePadState, _lastGamePadState;

        /// <summary>
        /// The _keyboard state
        /// </summary>
        private KeyboardState _keyboardState, _lastKeyboardState;

        /// <summary>
        /// The _mouse sensitivity
        /// </summary>
        private float _mouseSensitivity = DEF_MOUSE_SENS;

        /// <summary>
        /// The _mouse state
        /// </summary>
        private MouseState _mouseState, _lastMouseState;

        /// <summary>
        /// The _pre capture mouse state
        /// </summary>
        private MouseState? _preCaptureMouseState;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputManager"/> class.
        /// </summary>
        /// <param name="game">Game that the game component should be attached to.</param>
        public InputManager(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IInput), this);
            game.Components.Add(this);
        }

        /// <summary>
        /// Gets the buttons pressed.
        /// </summary>
        /// <value>
        /// The buttons pressed.
        /// </value>
        public IEnumerable<Buttons> ButtonsPressed
        {
            get
            {
                if (WasPressed(Buttons.A))
                    yield return Buttons.A;
                if (WasPressed(Buttons.B))
                    yield return Buttons.B;
                if (WasPressed(Buttons.X))
                    yield return Buttons.X;
                if (WasPressed(Buttons.Y))
                    yield return Buttons.Y;
                if (WasPressed(Buttons.Start))
                    yield return Buttons.Start;
                if (WasPressed(Buttons.Back))
                    yield return Buttons.Back;
                if (WasPressed(Buttons.BigButton))
                    yield return Buttons.BigButton;
                if (WasPressed(Buttons.DPadDown))
                    yield return Buttons.DPadDown;
                if (WasPressed(Buttons.DPadLeft))
                    yield return Buttons.DPadLeft;
                if (WasPressed(Buttons.DPadRight))
                    yield return Buttons.DPadRight;
                if (WasPressed(Buttons.DPadUp))
                    yield return Buttons.DPadUp;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [capture mouse].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [capture mouse]; otherwise, <c>false</c>.
        /// </value>
        public bool CaptureMouse
        {
            get
            {
                return _captureMouse && this.Game.IsActive;
            }
            set
            {
                if (_captureMouse != value)
                {
                    if (_captureMouse = value && this.Game.IsActive)
                    {
                        _preCaptureMouseState = _mouseState;
                        Mouse.SetPosition(MouseCenterPositionX, MouseCenterPositionY);
                        this.Game.IsMouseVisible = false;
                    }
                    else
                    {
                        if (_preCaptureMouseState != null)
                        {
                            Mouse.SetPosition(_preCaptureMouseState.Value.X, _preCaptureMouseState.Value.Y);
                        }
                        this.Game.IsMouseVisible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the controller sensitivity.
        /// </summary>
        /// <value>
        /// The controller sensitivity.
        /// </value>
        public float ControllerSensitivity { get { return _controllerSensitivity; } set { _controllerSensitivity = value; } }

        /// <summary>
        /// Gets the state of the game pad.
        /// </summary>
        /// <value>
        /// The state of the game pad.
        /// </value>
        public GamePadState GamePadState { get { return _gamePadState; } }

        /// <summary>
        /// Gets the state of the keyboard.
        /// </summary>
        /// <value>
        /// The state of the keyboard.
        /// </value>
        public KeyboardState KeyboardState { get { return _keyboardState; } }

        /// <summary>
        /// Gets the keys pressed.
        /// </summary>
        /// <value>
        /// The keys pressed.
        /// </value>
        public IEnumerable<Keys> KeysPressed
        {
            get
            {
                foreach (Keys k in _keyboardState.GetPressedKeys())
                {
                    if (!_lastKeyboardState.IsKeyDown(k))
                        yield return k;
                }
            }
        }

        /// <summary>
        /// Gets the last state of the game pad.
        /// </summary>
        /// <value>
        /// The last state of the game pad.
        /// </value>
        public GamePadState LastGamePadState { get { return _lastGamePadState; } }

        /// <summary>
        /// Gets the last state of the mouse.
        /// </summary>
        /// <value>
        /// The last state of the mouse.
        /// </value>
        public MouseState LastMouseState { get { return LastMouseState; } }

        /// <summary>
        /// Gets the mouse delta.
        /// </summary>
        /// <value>
        /// The mouse delta.
        /// </value>
        public Vector2 MouseDelta
        {
            get
            {
                return new Vector2(
                    CaptureMouse ? _mouseState.X - MouseCenterPositionX : 0,
                    CaptureMouse ? _mouseState.Y - MouseCenterPositionY : 0);
            }
        }

        /// <summary>
        /// Gets or sets the mouse sensitivity.
        /// </summary>
        /// <value>
        /// The mouse sensitivity.
        /// </value>
        public float MouseSensitivity { get { return _mouseSensitivity; } set { _mouseSensitivity = value; } }

        /// <summary>
        /// Gets the state of the mouse.
        /// </summary>
        /// <value>
        /// The state of the mouse.
        /// </value>
        public MouseState MouseState { get { return _mouseState; } }

        /// <summary>
        /// Gets a value indicating whether [using game pad].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [using game pad]; otherwise, <c>false</c>.
        /// </value>
        public bool UsingGamePad { get { return _gamePadState.IsConnected; } }

        /// <summary>
        /// Called when the GameComponent needs to be updated. Override this method with component-specific update code.
        /// </summary>
        /// <param name="gameTime">Time elapsed since the last call to Update</param>
        public override void Update(GameTime gameTime)
        {
            _lastKeyboardState = _keyboardState;
            _lastGamePadState = _gamePadState;
            _lastMouseState = _mouseState;
            _gamePadState = GamePad.GetState(PlayerIndex.One);
            _mouseState = Mouse.GetState();
            _keyboardState = Keyboard.GetState();

            if (this.CaptureMouse)
            {
                Mouse.SetPosition(MouseCenterPositionX, MouseCenterPositionY);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Wases the pressed.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns></returns>
        public bool WasPressed(Buttons button)
        {
            return _gamePadState.IsButtonDown(button) && !_lastGamePadState.IsButtonDown(button);
        }

        /// <summary>
        /// Wases the pressed.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns></returns>
        public bool WasPressed(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.LeftButton:
                    return _mouseState.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton != ButtonState.Pressed;
                case MouseButton.MiddleButton:
                    return _mouseState.MiddleButton == ButtonState.Pressed && _lastMouseState.MiddleButton != ButtonState.Pressed;
                case MouseButton.RightButton:
                    return _mouseState.RightButton == ButtonState.Pressed && _lastMouseState.RightButton != ButtonState.Pressed;
                case MouseButton.XButton1:
                    return _mouseState.XButton1 == ButtonState.Pressed && _lastMouseState.XButton1 != ButtonState.Pressed;
                case MouseButton.XButton2:
                    return _mouseState.XButton2 == ButtonState.Pressed && _lastMouseState.XButton2 != ButtonState.Pressed;
                default:
                    return false;
            }
        }
    }
}