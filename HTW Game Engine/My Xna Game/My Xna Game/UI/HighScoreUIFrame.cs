#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System.Collections.Generic;
using System.Linq;
using BaseLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using RenderingSystem;

namespace My_Xna_Game.UI
{
    /// <summary>
    ///
    /// </summary>
    internal class HighScoreUIFrame : UIFrame
    {
        public const string FRAME_ID = "high score menu";

        private Video _backgroundVideo;
        private List<UIButton> _buttons = new List<UIButton>();
        private VideoPlayer _player;

        /// <summary>
        /// Initializes a new instance of the <see cref="HighScoreUIFrame"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="textRenderer">The text renderer.</param>
        public HighScoreUIFrame(int width, int height, TextRenderer textRenderer)
            : base(FRAME_ID, true, true, true)
        {
            // We want the entire list of UI controls to be centered.
            float fHeight = (float)height;
            float fWidth = (float)width;
            Vector2 scale = GetScalingFactor(fWidth, fHeight);

            float marginY = 50f * scale.Y;

            float btnY = fHeight / 8f;
            float btnX = (fWidth / 2f);

            Vector2 btnPos = new Vector2(btnX, btnY);

            textRenderer.SetRenderFont(TextType.UI);
            float btnHeight = textRenderer.GetHeightOfStr("Asdf");
            const float txtScale = 0.5f;
            btnHeight *= txtScale * scale.Y;

            _buttons.Add(new UIButton(btnPos, "Back", Color.Black, Color.White, txtScale, (XnaGame game) =>
            {
                s_moveToFrame = "base menu";
            }));

            foreach (UIButton uiBtn in _buttons)
                uiBtn.UpdateBtnDim(textRenderer, txtScale);

            _backgroundVideo = ResourceMgr.Content.Load<Video>("videos/BlurredLights");
            _player = new VideoPlayer();

            _player.Volume = 0.8f;
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

            textRenderer.SetRenderFont(TextType.UI);

            Texture2D videoTex = null;
            if (_player.State != MediaState.Stopped)
                videoTex = _player.GetTexture();

            textRenderer.RenderTexture(videoTex, width, height, 0, 0);

            Vector2 res = new Vector2(width, height);
            Vector2 scaling = GetScalingFactor(fWidth, fHeight);

            for (int i = 0; i < _buttons.Count; ++i)
            {
                UIButton uiBtn = _buttons[i];
                if (i == i_selectedIndex)
                    uiBtn.TextColor = UISelectionColor;
                else
                    uiBtn.TextColor = Color.White;
                DrawBtn(uiBtn.DrawPos, uiBtn.Text, uiBtn.Margin, uiBtn.TextSize, uiBtn.TextColor, uiBtn.BackgroundColor, textRenderer, res);
            }

            UIButton backBtn = _buttons[0];
            Vector2 btnDim = backBtn.GetBtnDim(textRenderer);

            string[] highscore = game.GetHighScores();
            Vector2 txtPos = backBtn.DrawPos;
            float marginY = 30f * scaling.Y;
            txtPos.Y += marginY;

            float centerX = (fWidth / 2f);

            const float txtSize = 0.5f;
            float highscoreYMargin = -25f * scaling.Y;
            float txtHeight = textRenderer.GetHeightOfStr("Asdf") * scaling.Y * txtSize;
            for (int i = 0; i < highscore.Count(); ++i)
            {
                string crntHighscore = highscore[i];

                float strWidth = textRenderer.GetLengthOfStr(crntHighscore) * txtSize * scaling.X;
                float localCenter = strWidth / 2f;

                txtPos.X = centerX - localCenter;

                textRenderer.RenderText(crntHighscore, txtPos, Color.White, scaling * txtSize);

                txtPos.Y += highscoreYMargin + txtHeight;
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
        }

        /// <summary>
        /// Updates the frame.
        /// </summary>
        public override void UpdateFrame()
        {
            if (_player.State == MediaState.Stopped)
            {
                _player.IsLooped = true;
                _player.Play(_backgroundVideo);
            }
        }

        /// <summary>
        /// Updates the UI mouse.
        /// </summary>
        /// <param name="mouseState">State of the mouse.</param>
        /// <param name="window">The window.</param>
        public override void UpdateUIMouse(Microsoft.Xna.Framework.Input.MouseState mouseState, GameWindow window)
        {
            base.UpdateUIMouse(mouseState, window);
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