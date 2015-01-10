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
using RenderingSystem;

namespace My_Xna_Game.UI
{
    /// <summary>
    ///
    /// </summary>
    internal class CreateProfileUIFrame : UIFrame
    {
        public const string FRAME_ID = "create frame ui frame";

        private static string s_nextFrameId;
        private static string s_previousFrameId;
        private List<UIButton> _buttons = new List<UIButton>();
        private BaseLogic.Core.FrameType<bool> _canMovePointer = new BaseLogic.Core.FrameType<bool>(true);
        private string _currentStr = "";
        private GameTexture _gradientBackgroundTex;
        private GameTexture _solidColorTex;
        private bool b_canEnter = false;
        private float f_txtSize = 0.7f;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProfileUIFrame"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="textRenderer">The text renderer.</param>
        public CreateProfileUIFrame(int width, int height, TextRenderer textRenderer)
            : base(FRAME_ID, true, true, true)
        {
            float fWidth = (float)width;
            float fHeight = (float)height;
            Vector2 scale = GetScalingFactor(fWidth, fHeight);

            textRenderer.SetRenderFont(TextType.UI);
            float btnHeight = textRenderer.GetHeightOfStr("Asdf");
            btnHeight *= f_txtSize * scale.Y;
            float heightMargin = 0f * f_txtSize * scale.Y;

            float btnWidth = textRenderer.GetLengthOfStr("W") * f_txtSize * scale.X;
            float widthMargin = 7.5f * f_txtSize * scale.X;

            float yOffset = 520f * scale.Y;
            float xOffset;

            Vector2 center = new Vector2(fWidth / 2f, fHeight / 2f);
            float halfKeyboardWidth = (btnWidth * 5f) + (widthMargin * 5f);
            xOffset = center.X - halfKeyboardWidth;

            Color backColor = Color.Black;
            Color foreColor = Color.White;

            Vector2 btnPos = new Vector2(xOffset, yOffset);

            _solidColorTex = new GameTexture("images/UI/SolidColor");
            _gradientBackgroundTex = new GameTexture("images/UI/GradientBackground");

            #region CreateKeyboard

            _buttons.Add(new UIButton(btnPos, "1", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "2", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "3", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "4", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "5", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "6", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "7", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "8", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "9", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "0", backColor, foreColor, f_txtSize));

            btnPos.Y += heightMargin + btnHeight;
            btnPos.X = xOffset;

            _buttons.Add(new UIButton(btnPos, "Q", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "W", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "E", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "R", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "T", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "Y", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "U", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "I", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "O", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "P", backColor, foreColor, f_txtSize));

            btnPos.Y += heightMargin + btnHeight;
            btnPos.X = xOffset;

            _buttons.Add(new UIButton(btnPos, "A", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "S", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "D", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "F", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "G", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "H", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "J", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "K", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "L", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, ":", backColor, foreColor, f_txtSize));

            btnPos.Y += heightMargin + btnHeight;
            btnPos.X = xOffset;

            _buttons.Add(new UIButton(btnPos, "Z", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "X", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "C", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "V", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "B", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "N", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "M", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, ",", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, ".", backColor, foreColor, f_txtSize));
            btnPos.X += btnWidth + widthMargin;
            _buttons.Add(new UIButton(btnPos, "?", backColor, foreColor, f_txtSize));

            btnPos.X = center.X;
            btnPos.Y += 85f * scale.Y;
            _buttons.Add(new UIButton(btnPos, "Back", backColor, foreColor, f_txtSize));

            #endregion CreateKeyboard
        }

        /// <summary>
        /// Sets the next frame identifier.
        /// </summary>
        /// <value>
        /// The next frame identifier.
        /// </value>
        public static string NextFrameId
        {
            set { s_nextFrameId = value; }
        }

        /// <summary>
        /// Sets the previous frame identifier.
        /// </summary>
        /// <value>
        /// The previous frame identifier.
        /// </value>
        public static string PreviousFrameId
        {
            set { s_previousFrameId = value; }
        }

        /// <summary>
        /// Decrements the index.
        /// </summary>
        public override void DecrementIndex()
        {
        }

        /// <summary>
        /// Draws the frame.
        /// </summary>
        /// <param name="textRenderer">The text renderer.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="game">The game.</param>
        public override void DrawFrame(TextRenderer textRenderer, int width, int height, XnaGame game)
        {
            float fWidth = (float)width;
            float fHeight = (float)height;

            textRenderer.RenderTexture(_gradientBackgroundTex.Texture, width, height, 0, 0);

            Vector2 center = new Vector2(fWidth / 2f, fHeight / 2f);

            Vector2 scalingFactor = GetScalingFactor(fWidth, fHeight);

            textRenderer.SetRenderFont(TextType.UI);

            Vector2 res = new Vector2(width, height);

            textRenderer.SetRenderFont(TextType.UI);

            for (int i = 0; i < _buttons.Count; ++i)
            {
                UIButton uiBtn = _buttons[i];
                if (i == i_selectedIndex)
                    uiBtn.TextColor = UISelectionColor;
                else
                    uiBtn.TextColor = Color.White;
                DrawBtn(uiBtn.DrawPos, uiBtn.Text, uiBtn.Margin, uiBtn.TextSize, uiBtn.TextColor, uiBtn.BackgroundColor, textRenderer, res);
            }

            float dispBackWidth = 900f * scalingFactor.X;
            float dispBackHeight = 110f * scalingFactor.Y;

            float x = center.X - (dispBackWidth / 2f);
            float y = 50f * scalingFactor.Y;

            textRenderer.RenderTexture(_solidColorTex.Texture, (int)dispBackWidth, (int)dispBackHeight, (int)x, (int)y, Color.White);

            float txtHeight = textRenderer.GetHeightOfStr(_currentStr) * scalingFactor.X * f_txtSize;

            textRenderer.RenderText(_currentStr, new Vector2(x + (5f * scalingFactor.X), (y - 30f * scalingFactor.Y)), Color.LightBlue, scalingFactor * f_txtSize);

            y += dispBackHeight + 20f * scalingFactor.Y;

            bool usingGamePad = game.Input.InputMgr.UsingGamePad;

            string deleteStr = usingGamePad ? "'X'" : "'Backspace'";
            string spaceStr = usingGamePad ? "'Y'" : "Space";
            string enterStr = usingGamePad ? "Press 'Start'" : "Press 'Tab'";

            float gamePadTxtHeight = usingGamePad ? 150f : 0f;
            float startTxtHeight = b_canEnter ? 75f : 0f;
            dispBackHeight = (gamePadTxtHeight + startTxtHeight + 40f) * scalingFactor.Y;

            textRenderer.RenderTexture(_solidColorTex.Texture, (int)dispBackWidth, (int)dispBackHeight, (int)x, (int)y, UIBackingColor);

            string msgStr;
            const float hintScalingSize = 0.7f;
            if (usingGamePad)
            {
                msgStr = String.Format("{0} to delete", deleteStr);
                textRenderer.RenderText(msgStr, new Vector2(x, y), Color.White, scalingFactor * (f_txtSize * hintScalingSize));
                y += 75f * scalingFactor.Y;
                msgStr = string.Format("{0} to space", spaceStr);
                textRenderer.RenderText(msgStr, new Vector2(x, y), Color.White, scalingFactor * (f_txtSize * hintScalingSize));
                y += 75f * scalingFactor.Y;
            }

            if (b_canEnter)
            {
                msgStr = String.Format("{0} to enter", enterStr);
                textRenderer.RenderText(msgStr, new Vector2(x, y), Color.White, scalingFactor * (f_txtSize * hintScalingSize));
            }
        }

        /// <summary>
        /// Increments the index.
        /// </summary>
        public override void IncrementIndex()
        {
        }

        /// <summary>
        /// Called when [BTN selected].
        /// </summary>
        /// <param name="game">The game.</param>
        public override void OnBtnSelected(XnaGame game)
        {
            if (i_selectedIndex == 40)
            {
                GoBack();
                return;
            }

            if (_currentStr.Length > 8)
                return;

            UIButton btn = _buttons[i_selectedIndex];
            _currentStr += btn.Text;
            PlayClickSoundEffect();
        }

        /// <summary>
        /// Called when [game pad input].
        /// </summary>
        /// <param name="btn">The BTN.</param>
        /// <returns></returns>
        public override bool OnGamePadInput(Microsoft.Xna.Framework.Input.Buttons btn)
        {
            if (btn == Buttons.Start && _currentStr != "")
            {
                OnFinished();
                return true;
            }
            switch (btn)
            {
                case Buttons.DPadUp:
                    SelectorUp();
                    return true;
                case Buttons.DPadRight:
                    SelectorRight();
                    return true;
                case Buttons.DPadLeft:
                    SelectorLeft();
                    return true;
                case Buttons.DPadDown:
                    SelectorDown();
                    return true;
                case Buttons.X:
                    SelectorRemove();
                    break;

                case Buttons.B:
                    GoBack();
                    break;

                case Buttons.Y:
                    SelectorSpace();
                    break;
            }

            return false;
        }

        /// <summary>
        /// Called when [key input].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public override bool OnKeyInput(Microsoft.Xna.Framework.Input.Keys key)
        {
            if (key == Keys.Tab && _currentStr != "")
            {
                OnFinished();
                return true;
            }
            if (key == Keys.Up)
            {
                SelectorUp();
                return true;
            }
            if (key == Keys.Down)
            {
                SelectorDown();
                return true;
            }
            if (key == Keys.Left)
            {
                SelectorLeft();
                return true;
            }
            if (key == Keys.Right)
            {
                SelectorRight();
                return true;
            }
            if (key == Keys.Back)
            {
                SelectorRemove();
                return true;
            }
            if (key == Keys.Back)
            {
                GoBack();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            _gradientBackgroundTex.Unload();
            _solidColorTex.Unload();
        }

        /// <summary>
        /// Updates the frame.
        /// </summary>
        public override void UpdateFrame()
        {
            _canMovePointer.UpdateFrameCount();
            XnaGame game = XnaGame.Game_Instance;
            Input input = game.Input;
            GamePadState gamePadState = input.InputMgr.GamePadState;

            const int keypadPointerSensitivity = 10;

            if (!_canMovePointer.Data)
                return;

            if (gamePadState.ThumbSticks.Left.X > 0.95f)
            {
                SelectorRight();
                _canMovePointer.Lock(false, keypadPointerSensitivity);
            }
            if (gamePadState.ThumbSticks.Left.X < -0.95f)
            {
                SelectorLeft();
                _canMovePointer.Lock(false, keypadPointerSensitivity);
            }
            if (gamePadState.ThumbSticks.Left.Y > 0.95f)
            {
                SelectorUp();
                _canMovePointer.Lock(false, keypadPointerSensitivity);
            }
            if (gamePadState.ThumbSticks.Left.Y < -0.95f)
            {
                SelectorDown();
                _canMovePointer.Lock(false, keypadPointerSensitivity);
            }

            b_canEnter = !game.ProfileMgr.DoesProfileExist(_currentStr);
        }

        /// <summary>
        /// Goes the back.
        /// </summary>
        private void GoBack()
        {
            PlayClickSoundEffect();
            if (s_previousFrameId != null)
                s_moveToFrame = s_previousFrameId;
            else
                s_moveToFrame = MenuUIFrame.FRAME_ID;
        }

        /// <summary>
        /// Called when [finished].
        /// </summary>
        private void OnFinished()
        {
            if (!b_canEnter)
                return;

            XnaGame game = XnaGame.Game_Instance;

            game.ProfileMgr.AddToList(new Core.GameProfile(_currentStr));

            s_moveToFrame = s_nextFrameId;
        }

        /// <summary>
        /// Selectors down.
        /// </summary>
        private void SelectorDown()
        {
            if (i_selectedIndex < 30)
                i_selectedIndex += 10;
            else
                i_selectedIndex -= 30;
        }

        /// <summary>
        /// Selectors the left.
        /// </summary>
        private void SelectorLeft()
        {
            if (i_selectedIndex > 0)
                i_selectedIndex--;
            else
                i_selectedIndex = 40;
        }

        /// <summary>
        /// Selectors the remove.
        /// </summary>
        private void SelectorRemove()
        {
            if (_currentStr.Length > 0)
                _currentStr = _currentStr.Remove(_currentStr.Length - 1, 1);
        }

        /// <summary>
        /// Selectors the right.
        /// </summary>
        private void SelectorRight()
        {
            if (i_selectedIndex < 40)
                i_selectedIndex++;
            else
                i_selectedIndex = 0;
        }

        /// <summary>
        /// Selectors the space.
        /// </summary>
        private void SelectorSpace()
        {
            _currentStr += " ";
        }

        /// <summary>
        /// Selectors up.
        /// </summary>
        private void SelectorUp()
        {
            if (i_selectedIndex > 9)
                i_selectedIndex -= 10;
            else
                i_selectedIndex += 30;
        }
    }
}