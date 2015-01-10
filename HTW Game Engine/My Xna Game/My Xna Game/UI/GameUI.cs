#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using BaseLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using My_Xna_Game.UI;

namespace My_Xna_Game
{
    public class GameUI
    {
        private static GameUI _gameUI;
        private BaseLogic.Core.FrameType<bool> _canMovePointer = new BaseLogic.Core.FrameType<bool>(true);
        private UIFrame _currentUIFrame;

        private Dictionary<string, UIFrame> _uiFrames = new Dictionary<string, UIFrame>();

        public static GameUI GameUI_Instance
        {
            get { return _gameUI; }
        }

        public bool ExclusiveInput
        {
            get
            {
                if (_currentUIFrame != null)
                    return _currentUIFrame.ExclusiveInput;
                else
                    throw new ArgumentException("No current ui frame!");
            }
        }

        public bool ExclusiveRendering
        {
            get
            {
                if (_currentUIFrame != null)
                    return _currentUIFrame.ExclusiveRendering;
                else
                    throw new ArgumentException("No current ui frame!");
            }
        }

        public bool ExclusiveUpdate
        {
            get
            {
                if (_currentUIFrame != null)
                    return _currentUIFrame.ExclusiveUpdate;
                else
                    throw new ArgumentException("No current ui frame");
            }
        }

        public GameUI()
        {
            _gameUI = this;
        }

        public void DrawUI(TextRenderer textRenderer, int width, int height, XnaGame game)
        {
            if (_currentUIFrame.MoveToFrame != UIFrame.INVALID_UI_FRAME && _uiFrames.ContainsKey(_currentUIFrame.MoveToFrame))
            {
                string moveToFrame = _currentUIFrame.MoveToFrame;
                _currentUIFrame.InvalidateMoveFrame();
                _currentUIFrame = _uiFrames[moveToFrame];
                _currentUIFrame.UpdateFrame();
            }

            _currentUIFrame.DrawFrame(textRenderer, width, height, game);
        }

        public void ImmediateNavigateToFrame(string frameID)
        {
            if (_uiFrames.ContainsKey(frameID))
            {
                _currentUIFrame.InvalidateMoveFrame();
                _currentUIFrame = _uiFrames[frameID];
                _currentUIFrame.UpdateFrame();
            }
        }

        public void ImmediateNavigateToFrame(UIFrame uiFrame)
        {
            _currentUIFrame.InvalidateMoveFrame();
            _currentUIFrame = uiFrame;
            _currentUIFrame.UpdateFrame();
        }

        public void LoadContent(int width, int height, TextRenderer textRenderer)
        {
            List<UIFrame> frames = new List<UIFrame>();
            frames.Add(new MenuUIFrame(width, height, textRenderer));
            frames.Add(new PlayerUIFrame());
            frames.Add(new HighScoreUIFrame(width, height, textRenderer));
            frames.Add(new TriviaUIFrame());
            frames.Add(new GameMenuUIFrame(width, height, textRenderer));
            frames.Add(new DialogUIFrame());
            frames.Add(new LevelSelectorUIFrame(width, height, textRenderer));
            frames.Add(new CreateProfileUIFrame(width, height, textRenderer));
            frames.Add(new WinMsgUIFrame(width, height));
            frames.Add(new LossMsgUIFrame(width, height));
            frames.Add(new SelectProfileUIFrame(width, height));
            frames.Add(new SettingsUIFrame(width, height, textRenderer));
            frames.Add(new HelpUIFrame());

            foreach (UIFrame frame in frames)
            {
                _uiFrames.Add(frame.ID, frame);
            }

            // Uncomment this for the intro screen to run.
            //if (!StartMsgUIFrame.HasRan)
            //    _currentUIFrame = new StartMsgUIFrame();
            //else
                _currentUIFrame = frames[0];
        }

        public bool LoadDialogData(Dialog dialogData, int width, int height, TextRenderer textRenderer, Action<PlayerResponse> onResponse, bool allowQuit)
        {
            if (_currentUIFrame is DialogUIFrame)
            {
                DialogUIFrame currentDialogFrame = _currentUIFrame as DialogUIFrame;

                currentDialogFrame.AllowQuit = allowQuit;
                currentDialogFrame.LoadDialogData(dialogData, width, height, textRenderer, onResponse);
                return true;
            }

            return false;
        }

        public void LoadWinUIFrame(Core.HighScoreData highscoreData, bool isHighscore)
        {
            foreach (UIFrame uiFrame in _uiFrames.Values)
            {
                if (uiFrame is WinMsgUIFrame)
                {
                    (uiFrame as WinMsgUIFrame).LoadWinData(highscoreData, isHighscore);
                }
            }
        }

        public bool LoadTriviaData(Trivia.TriviaQuestion triviaData, int width, int height,
            TextRenderer textRender, Action<bool> onTriviaAnswered, bool allowQuit)
        {
            if (_currentUIFrame is TriviaUIFrame)
            {
                TriviaUIFrame currentTriviaFrame = _currentUIFrame as TriviaUIFrame;

                currentTriviaFrame.AllowQuit = allowQuit;
                currentTriviaFrame.LoadTriviaData(triviaData, width, height, textRender, onTriviaAnswered);

                return true;
            }

            return false;
        }

        public void NavigateToFrame(string frameID)
        {
            _currentUIFrame.NavigateTo(frameID);
        }

        public void ProcessInput(Input input, XnaGame game)
        {
            // Will only process input for exclusive UI's.

            _canMovePointer.UpdateFrameCount();

            if (input.InputMgr.UsingGamePad)
            {
                InputManager inputMgr = input.InputMgr;
                var btnsPressed = inputMgr.ButtonsPressed;
                foreach (Buttons btn in btnsPressed)
                {
                    if (_currentUIFrame.OnGamePadInput(btn))
                        return;

                    switch (btn)
                    {
                        case Buttons.A:
                            OnPressedEnterForm(game);
                            break;

                        case Buttons.Start:
                            OnPressedEscapeFrame(game);
                            break;

                        case Buttons.B:
                            OnPressedBack(game);
                            break;
                    }
                }

                const int pointerMovementSensitivity = 10;

                Vector2 leftThumbPad = inputMgr.GamePadState.ThumbSticks.Left;
                if ((leftThumbPad.Y > 0.9f || inputMgr.WasPressed(Buttons.DPadUp)) &&
                    _canMovePointer.Data)
                {
                    _currentUIFrame.DecrementIndex();
                    _canMovePointer.Lock(false, pointerMovementSensitivity);
                }
                else if ((leftThumbPad.Y < -0.9f || inputMgr.WasPressed(Buttons.DPadDown)) &&
                    _canMovePointer.Data)
                {
                    _currentUIFrame.IncrementIndex();
                    _canMovePointer.Lock(false, pointerMovementSensitivity);
                }
            }
            else
            {
                var keysPressed = input.InputMgr.KeysPressed;
                foreach (var key in keysPressed)
                {
                    if (_currentUIFrame.OnKeyInput(key))
                        continue;

                    switch (key)
                    {
                        case Keys.Enter:
                            OnPressedEnterForm(game);
                            break;

                        case Keys.Up:
                            _currentUIFrame.DecrementIndex();
                            break;

                        case Keys.Down:
                            _currentUIFrame.IncrementIndex();
                            break;

                        case Keys.Escape:
                            OnPressedEscapeFrame(game);
                            break;

                        case Keys.Back:
                        case Keys.Delete:
                            OnPressedBack(game);
                            break;
                    }
                }
            }
        }

        public void UnloadContent()
        {
            foreach (UIFrame uiFrame in _uiFrames.Values)
            {
                uiFrame.UnloadContent();
            }
        }

        public void Update()
        {
            _currentUIFrame.UpdateFrame();
        }

        public void UpdateUIMousePos(Microsoft.Xna.Framework.Input.MouseState mouseState, GameWindow window)
        {
            _currentUIFrame.UpdateUIMouse(mouseState, window);
        }

        private void OnPressedBack(XnaGame game)
        {
            if (!_currentUIFrame.AllowQuit)
                return;

            if (_currentUIFrame.ID == TriviaUIFrame.FRAME_ID ||
                _currentUIFrame.ID == GameMenuUIFrame.FRAME_ID ||
                _currentUIFrame.ID == DialogUIFrame.FRAME_ID)
            {
                _currentUIFrame.OnClose(game);
                _currentUIFrame.NavigateTo(PlayerUIFrame.FRAME_ID);
            }
        }

        private void OnPressedEnterForm(XnaGame game)
        {
            _currentUIFrame.OnBtnSelected(game);
        }

        private void OnPressedEscapeFrame(XnaGame game)
        {
            if (!_currentUIFrame.AllowQuit)
                return;

            if (_currentUIFrame.ID == TriviaMsgUIFrame.FRAME_ID ||
                _currentUIFrame.ID == TriviaUIFrame.FRAME_ID ||
                _currentUIFrame.ID == DialogUIFrame.FRAME_ID)
            {
                _currentUIFrame.OnClose(game);
                _currentUIFrame.NavigateTo(GameMenuUIFrame.FRAME_ID);
            }
        }
    }
}