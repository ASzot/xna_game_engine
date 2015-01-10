#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

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
    internal class SettingsUIFrame : UIFrame
    {
        public const string FRAME_ID = "settings ui";

        private List<UIButton> _buttons = new List<UIButton>();
        private Rectangle _uiBackingRenderRect;
        private GameTexture _uiBackingTex;
        private float f_txtSize = 0.5f;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsUIFrame"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="txtRenderer">The text renderer.</param>
        public SettingsUIFrame(int width, int height, TextRenderer txtRenderer)
            : base(FRAME_ID, false, true, true)
        {
            float fWidth = (float)width;
            float fHeight = (float)height;

            Vector2 scale = GetScalingFactor(fWidth, fHeight);

            Vector2 btnPos = new Vector2(fWidth / 2f, fHeight / 2f);

			// Just a random string to get the height of the current font.
            float buttonHeight = txtRenderer.GetHeightOfStr("H");
            buttonHeight *= f_txtSize * scale.Y;

            btnPos.Y += buttonHeight;
            _buttons.Add(new UIButton(btnPos, "", Color.Black, Color.White, f_txtSize, (XnaGame game) =>
            {
                string btnTxt = _buttons[0].Text;
                string[] btnTxts = btnTxt.Split(' ');
                string senStr = btnTxts[0];
                float currentSetting = float.Parse(senStr);
                if (++currentSetting > 10f)
                {
                    currentSetting = 1;
                }

                _buttons[0].Text = "Sensitivity: " + currentSetting.ToString();
                game.Input.InputSensitivity = currentSetting;
            }));

            // This was an old part of the hunt the wumpus game where there was integration with a math solving system.
            // This does make the UI look off.

            //TODO:
            // Fix the spacing created from the button which is no longer displayed.
            float marginX = 10f * scale.X;
            float txtWidth = txtRenderer.GetLengthOfStr("Display work: Yes") * scale.X * f_txtSize;
            float txtHeight = buttonHeight;

            Vector2 center = new Vector2(fWidth / 2f, fHeight / 2f);
            float uiBackingX = center.X - (txtWidth / 2f) - marginX;
            float uiBackingY = center.Y - txtHeight;

            float uiBackingWidth = (2 * marginX) + txtWidth;
            float uiBackingHeight = (btnPos.Y - uiBackingY) + txtHeight;

            _uiBackingRenderRect = new Rectangle((int)uiBackingX, (int)uiBackingY, (int)uiBackingWidth, (int)uiBackingHeight);

            _uiBackingTex = new GameTexture("images/UI/SolidColor");
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
            Vector2 res = new Vector2(width, height);

            textRenderer.RenderTexture(_uiBackingTex.Texture, _uiBackingRenderRect.Width, _uiBackingRenderRect.Height,
                _uiBackingRenderRect.X, _uiBackingRenderRect.Y, UIBackingColor);

            for (int i = 0; i < _buttons.Count; ++i)
            {
                UIButton uiBtn = _buttons[i];
                if (i == i_selectedIndex)
                    uiBtn.TextColor = UISelectionColor;
                else
                    uiBtn.TextColor = Color.White;
                DrawBtn(uiBtn.DrawPos, uiBtn.Text, uiBtn.Margin, uiBtn.TextSize, uiBtn.TextColor, uiBtn.BackgroundColor, textRenderer, res);
            }
        }

        /// <summary>
        /// Called when [BTN selected].
        /// </summary>
        /// <param name="game">The game.</param>
        public override void OnBtnSelected(XnaGame game)
        {
            UIButton uiBtn = _buttons[i_selectedIndex];
            if (uiBtn.OnClicked != null)
            {
                PlayClickSoundEffect();
                uiBtn.OnClicked(game);
            }
        }

        /// <summary>
        /// Called when [game pad input].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public override bool OnGamePadInput(Buttons key)
        {
            if (key == Buttons.B)
            {
                s_moveToFrame = GameMenuUIFrame.FRAME_ID;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Called when [key input].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public override bool OnKeyInput(Keys key)
        {
            if (key == Keys.Back)
            {
                s_moveToFrame = GameMenuUIFrame.FRAME_ID;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            _uiBackingTex.Unload();
        }

        /// <summary>
        /// Updates the frame.
        /// </summary>
        public override void UpdateFrame()
        {
            if (_buttons[0].Text == "")
            {
                float ctrlSen = XnaGame.Game_Instance.Input.InputSensitivity;
                string ctrlSenStr = "Sensitivity: " + ctrlSen.ToString();
                _buttons[0].Text = ctrlSenStr;
            }
        }

        /// <summary>
        /// Gets the BTN count.
        /// </summary>
        /// <returns></returns>
        protected override int GetBtnCount()
        {
            return _buttons.Count;
        }
    }
}