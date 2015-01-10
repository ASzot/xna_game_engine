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
using RenderingSystem;

namespace My_Xna_Game.UI
{
    /// <summary>
    ///
    /// </summary>
    internal class GameMenuUIFrame : UIFrame
    {
        public const string FRAME_ID = "game menu ui";

        private List<UIButton> _buttons = new List<UIButton>();
        private Rectangle _uiBackingRenderRect;
        private GameTexture _uiBackingTex;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameMenuUIFrame"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="txtRenderer">The text renderer.</param>
        public GameMenuUIFrame(int width, int height, TextRenderer txtRenderer)
            : base(FRAME_ID, false, true, true)
        {
            float fWidth = (float)width;
            float fHeight = (float)height;
            Vector2 center = new Vector2(fWidth / 2f, fHeight / 2f);

            Vector2 scale = GetScalingFactor(fWidth, fHeight);

            const float txtScale = 0.5f;

            const int numberOfButtons = 3;
            float buttonHeight = txtRenderer.GetHeightOfStr("Asdf");
            buttonHeight *= txtScale * scale.Y;

            float btnMargin = 10f * scale.Y;

            float totalHeight = (btnMargin * (float)(numberOfButtons - 1)) + (buttonHeight * (float)numberOfButtons);
            float startPosY = center.Y - (totalHeight / 2f);

            Vector2 pos = new Vector2(center.X, startPosY);
            _buttons.Add(new UIButton(pos, "Resume", Color.Black, Color.White, txtScale, (XnaGame game) =>
            {
                s_moveToFrame = PlayerUIFrame.FRAME_ID;
            }));
            pos.Y += btnMargin + buttonHeight;
            _buttons.Add(new UIButton(pos, "Settings", Color.Black, Color.White, txtScale, (XnaGame game) =>
            {
                s_moveToFrame = SettingsUIFrame.FRAME_ID;
            }));
            pos.Y += btnMargin + buttonHeight;
            _buttons.Add(new UIButton(pos, "Help", Color.Black, Color.White, txtScale, (XnaGame game) =>
            {
                s_moveToFrame = HelpUIFrame.FRAME_ID;
                HelpUIFrame.PreviousFrameID = FRAME_ID;
            }));
            pos.Y += btnMargin + buttonHeight;
            _buttons.Add(new UIButton(pos, "Exit To Menu", Color.Black, Color.White, txtScale, (XnaGame game) =>
            {
                s_moveToFrame = MenuUIFrame.FRAME_ID;
                game.OnLevelQuit();
            }));
            pos.Y += btnMargin + buttonHeight;
            _buttons.Add(new UIButton(pos, "Exit Game", Color.Black, Color.White, txtScale, (XnaGame game) =>
            {
                game.Kill = true;
            }));

            float widestTxtWidth = 0f;
            foreach (UIButton btn in _buttons)
            {
                string txt = btn.Text;
                float txtWidth = txtRenderer.GetLengthOfStr(txt) * scale.X * txtScale;
                if (txtWidth > widestTxtWidth)
                    widestTxtWidth = txtWidth;
            }
            float marginX = 20f * scale.X;

            float uiBackingX = center.X - (widestTxtWidth / 2f) - marginX;
            float uiBackingY = startPosY - buttonHeight;

            float uiBackingWidth = (2 * marginX) + widestTxtWidth;
            float uiBackingHeight = (pos.Y - uiBackingY) + buttonHeight + btnMargin;

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
            base.UpdateFrame();
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