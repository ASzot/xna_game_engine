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
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_Physics_Editor
{
    public enum MouseButton
    {
        LeftButton,
        MiddleButton,
        RightButton,
        XButton1,
        XButton2
    }

    public sealed class Input : GameComponent
    {
        const int MouseCenterPositionX = 100;
        const int MouseCenterPositionY = 100;
        const float DEF_MOUSE_SENS = 0.005f;
        const float DEF_CONTRL_SENS = 0.05f;

        private GamePadState _gamePadState, _lastGamePadState;
        private MouseState? _preCaptureMouseState;
        private MouseState _mouseState, _lastMouseState;

        private KeyboardState _keyboardState, _lastKeyboardState;
        private float _mouseSensitivity = DEF_MOUSE_SENS;
        private float _controllerSensitivity = DEF_CONTRL_SENS;
        private bool _captureMouse = true;

        public Input(Game game)
            :   base(game)
        {
            game.Services.AddService(typeof(Input), this);
            game.Components.Add(this);
        }

        public GamePadState LastGamePadState { get { return _lastGamePadState; } }
        public GamePadState GamePadState { get { return _gamePadState; } }
        public MouseState LastMouseState { get { return LastMouseState; } }
        public MouseState MouseState { get { return _mouseState; } }
        public KeyboardState KeyboardState { get { return _keyboardState; } }
        public float MouseSensitivity { get { return _mouseSensitivity; } set { _mouseSensitivity = value; } }
        public float ControllerSensitivity { get { return _controllerSensitivity; } set { _controllerSensitivity = value; } }
        public bool UsingGamePad { get { return _gamePadState.IsConnected; } }

        public Vector2 MouseDelta
        {
            get
            {
                return new Vector2(
                    CaptureMouse ? _mouseState.X - MouseCenterPositionX : 0,
                    CaptureMouse ? _mouseState.Y - MouseCenterPositionY : 0);
            }
        }

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

        public bool WasPressed(Buttons button)
        {
            return _gamePadState.IsButtonDown(button) && !_lastGamePadState.IsButtonDown(button);
        }

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
    }
}
