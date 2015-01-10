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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using RenderingSystem;

namespace My_Xna_Game.UI
{
    /// <summary>
    ///
    /// </summary>
    internal class LevelSelectorUIFrame : UIFrame
    {
        public const string FRAME_ID = "level selector frame";
        private Video _backgroundVideo;
        private List<UIButton> _buttons = new List<UIButton>();
        private VideoPlayer _player;
        private float f_txtSize = 0.7f;

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelSelectorUIFrame"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="textRenderer">The text renderer.</param>
        public LevelSelectorUIFrame(int width, int height, TextRenderer textRenderer)
            : base(FRAME_ID, true, true, true)
        {
            float fWidth = (float)width;
            float fHeight = (float)height;
            Vector2 scale = GetScalingFactor(fWidth, fHeight);

            Vector2 center = new Vector2(fWidth / 2f, fHeight / 2f);
            float offsetY = 100f * scale.Y;

            textRenderer.SetRenderFont(TextType.UI);
            // The string doesn't matter only trying to get the height of the font.
            float btnHeight = textRenderer.GetHeightOfStr("A");
            btnHeight *= f_txtSize * scale.Y;

            Vector2 btnPos = new Vector2(center.X, offsetY);

            // These are the names that will be shown to the user.
            string[] displayNames =
            {
                "Level 1",
                "Level 2",
                "Level 3",
                "Tester Level",
            };

            // Custom levels could be inserted here.
            string[] loadNames =
            {
                "Level1",
                "Level1",
                "Level1",
                "TesterLevel",
            };

            // The corresponding trivia to load is here.
            string[] triviaNames =
            {
                "NormalTrivia",
                "NormalTrivia",
                "NormalTrivia",
                null
            };

            // These are the door connection datas.
            string[] mapNames =
            {
                "Level1Map",
                "Level2Map",
                "Level3Map",
                null
            };

            for (int i = 1; i < displayNames.Length + 1; ++i)
            {
                _buttons.Add(new UIButton(btnPos, displayNames[i - 1], Color.Black, Color.White, f_txtSize));

                btnPos.Y += btnHeight;
            }

            _buttons[0].OnClicked += (XnaGame game) =>
            {
                game.GameSystem.LoadScene(loadNames[0] + ".sav");
                if (mapNames[0] == null || triviaNames[0] == null)
                    game.ShouldLoadHTWGameComps = false;
                else
                {
                    game.LoadTrivia(triviaNames[0] + ".xml");
                    game.LoadMap(mapNames[0] + ".xml", displayNames[0]);
                }
                s_moveToFrame = "player ui";
            };
            _buttons[1].OnClicked += (XnaGame game) =>
            {
                game.GameSystem.LoadScene(loadNames[1] + ".sav");
                if (mapNames[1] == null || triviaNames[1] == null)
                    game.ShouldLoadHTWGameComps = false;
                else
                {
                    game.LoadTrivia(triviaNames[1] + ".xml");
                    game.LoadMap(mapNames[1] + ".xml", displayNames[1]);
                }
                s_moveToFrame = "player ui";
            };
            _buttons[2].OnClicked += (XnaGame game) =>
            {
                game.GameSystem.LoadScene(loadNames[2] + ".sav");
                if (mapNames[2] == null || triviaNames[2] == null)
                    game.ShouldLoadHTWGameComps = false;
                else
                {
                    game.LoadTrivia(triviaNames[2] + ".xml");
                    game.LoadMap(mapNames[2] + ".xml", displayNames[2]);
                }
                s_moveToFrame = "player ui";
            };
            _buttons[3].OnClicked += (XnaGame game) =>
            {
                game.GameSystem.LoadScene(loadNames[3] + ".sav");
                if (mapNames[3] == null || triviaNames[3] == null)
                    game.ShouldLoadHTWGameComps = false;
                else
                {
                    game.LoadTrivia(triviaNames[3] + ".xml");
                    game.LoadMap(mapNames[3] + ".xml", displayNames[3]);
                }
                s_moveToFrame = "player ui";
            };

            _buttons.Add(new UIButton(btnPos, "Back", Color.Black, Color.White, f_txtSize, (XnaGame game) =>
            {
                s_moveToFrame = MenuUIFrame.FRAME_ID;
            }));

            // Choose the background video to play here.
            // I personally like the blurred lights background video because it looks cool.
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
            Vector2 res = new Vector2(width, height);

            textRenderer.SetRenderFont(TextType.UI);

            Texture2D videoTex = null;
            if (_player.State != MediaState.Stopped)
                videoTex = _player.GetTexture();

            textRenderer.RenderTexture(videoTex, width, height, 0, 0);

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
        /// Gets the BTN count.
        /// </summary>
        /// <returns></returns>
        protected override int GetBtnCount()
        {
            return _buttons.Count;
        }
    }
}