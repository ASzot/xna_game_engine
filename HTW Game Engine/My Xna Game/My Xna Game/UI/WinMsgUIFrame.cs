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
    internal class WinMsgUIFrame : UIFrame
    {
        public const string FRAME_ID = "win msg ui frame";

        private GameTexture _backingTex;
        private List<UIButton> _buttons = new List<UIButton>();
        private Core.HighScoreData _highscoreData;
        private bool b_isHighscore;
        private float f_txtSize = 1.0f;

        /// <summary>
        /// Initializes a new instance of the <see cref="WinMsgUIFrame"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public WinMsgUIFrame(int width, int height)
            : base(FRAME_ID, false, true, true)
        {
            float fWidth = (float)width;
            float fHeight = (float)height;

            Vector2 scale = GetScalingFactor(fWidth, fHeight);

            Vector2 btnPos = new Vector2(fWidth / 2f, 100f * scale.Y);
            _buttons.Add(new UIButton(btnPos, "To Menu", Color.Black, Color.White, f_txtSize, (XnaGame game) =>
            {
                s_moveToFrame = MenuUIFrame.FRAME_ID;
                game.OnLevelQuit();
            }));

            _backingTex = new GameTexture("images/UI/SolidColor");
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

            textRenderer.RenderTexture(_backingTex.Texture, width, height, 0, 0, UIBackingColor);

            Vector2 res = new Vector2(width, height);

            Vector2 scale = GetScalingFactor(fWidth, fHeight);
            Vector2 txtPos = new Vector2(fWidth / 2f, 200f * scale.Y);

            textRenderer.SetRenderFont(TextType.UI);
            float txtHeight = textRenderer.GetHeightOfStr("Asdf") * scale.Y * f_txtSize;
            float txtWidth = textRenderer.GetLengthOfStr(_highscoreData.Name) * scale.X * f_txtSize;

            txtPos.X = (fWidth / 2f) - (txtWidth / 2f);
            textRenderer.RenderText(_highscoreData.Name, txtPos, Color.Green, scale * f_txtSize);
            txtPos.Y += (txtHeight);

            string scoreTxt = "Score: " + _highscoreData.Score.ToString();
            txtWidth = textRenderer.GetLengthOfStr(scoreTxt) * scale.X * f_txtSize;
            txtPos.X = (fWidth / 2f) - (txtWidth / 2f);
            textRenderer.RenderText(scoreTxt, txtPos, Color.Green, scale * f_txtSize);
            txtPos.Y += (txtHeight);

            string turnsTxt = "Turns : " + _highscoreData.TurnCount.ToString();
            txtWidth = textRenderer.GetLengthOfStr(turnsTxt) * scale.X * f_txtSize;
            txtPos.X = (fWidth / 2f) - (txtWidth / 2f);
            textRenderer.RenderText(turnsTxt, txtPos, Color.Green, scale * f_txtSize);
            txtPos.Y += (txtHeight);

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
        /// Loads the win data.
        /// </summary>
        /// <param name="highscoreData">The highscore data.</param>
        /// <param name="isHighscore">if set to <c>true</c> [is highscore].</param>
        public void LoadWinData(Core.HighScoreData highscoreData, bool isHighscore)
        {
            _highscoreData = highscoreData;
            b_isHighscore = isHighscore;
        }

        /// <summary>
        /// Called when [BTN selected].
        /// </summary>
        /// <param name="game">The game.</param>
        public override void OnBtnSelected(XnaGame game)
        {
            PlayClickSoundEffect();
            _buttons[0].OnClicked(game);
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