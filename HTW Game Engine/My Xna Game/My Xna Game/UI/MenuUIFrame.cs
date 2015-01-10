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
    internal class MenuUIFrame : UIFrame
    {
        public const string FRAME_ID = "base menu";
        private const float f_txtSize = 0.7f;

        private List<UIButton> _buttons = new List<UIButton>();
        private VideoPlayer _player;
        private Texture2D _titleTex;

        private Video _video;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuUIFrame"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="textRenderer">The text renderer.</param>
        public MenuUIFrame(int width, int height, TextRenderer textRenderer)
            : base(FRAME_ID, true, true, true)
        {
            // We want the entire list of UI controls to be centered.
            float fHeight = (float)height;
            float fWidth = (float)width;
            Vector2 scale = GetScalingFactor(fWidth, fHeight);

            float marginY = 5f * scale.Y;

            float quarterDownY = fHeight / 4f;
            float halfwayX = fWidth - (fWidth / 4f);

            Vector2 btnPos = new Vector2(halfwayX, quarterDownY);

            textRenderer.SetRenderFont(TextType.UI);
            float btnHeight = textRenderer.GetHeightOfStr("Asdf");
            btnHeight *= f_txtSize * scale.Y;

            _buttons.Add(new UIButton(btnPos, "Play", Color.Black, Color.White, f_txtSize, (XnaGame game) =>
            {
                _player.Volume = 0.0f;
                if (game.IsFirstPlay)
                {
                    CreateProfileUIFrame.NextFrameId = LevelSelectorUIFrame.FRAME_ID;
                    CreateProfileUIFrame.PreviousFrameId = FRAME_ID;
                    s_moveToFrame = CreateProfileUIFrame.FRAME_ID;
                }
                else
                {
                    s_moveToFrame = LevelSelectorUIFrame.FRAME_ID;
                }
            }));
            btnPos.Y += marginY + btnHeight;
            _buttons.Add(new UIButton(btnPos, "High Scores", Color.Black, Color.White, f_txtSize, (XnaGame game) =>
            {
                s_moveToFrame = HighScoreUIFrame.FRAME_ID;
                _player.Volume = 0.0f;
            }));
            btnPos.Y += marginY + btnHeight;
            _buttons.Add(new UIButton(btnPos, "Profiles", Color.Black, Color.White, f_txtSize, (XnaGame game) =>
            {
                s_moveToFrame = SelectProfileUIFrame.FRAME_ID;
                _player.Volume = 0.0f;
            }));
            btnPos.Y += marginY + btnHeight;
            _buttons.Add(new UIButton(btnPos, "Help", Color.Black, Color.White, f_txtSize, (XnaGame game) =>
            {
                s_moveToFrame = HelpUIFrame.FRAME_ID;
                _player.Volume = 0.0f;
                HelpUIFrame.PreviousFrameID = FRAME_ID;
            }));
            btnPos.Y += marginY + btnHeight;
            _buttons.Add(new UIButton(btnPos, "Exit", Color.Black, Color.White, f_txtSize, (XnaGame game) =>
            {
                game.Kill = true;
            }));

            foreach (UIButton uiBtn in _buttons)
                uiBtn.UpdateBtnDim(textRenderer, f_txtSize);

            _video = ResourceMgr.Content.Load<Video>("videos/BlurredLights");
            _player = new VideoPlayer();

            _player.Volume = 0.8f;

            _titleTex = ResourceMgr.LoadTexture("UI/TitleLogo");
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
            Texture2D videoTex = null;
            if (_player.State != MediaState.Stopped)
                videoTex = _player.GetTexture();

            textRenderer.RenderTexture(videoTex, width, height, 0, 0);

            textRenderer.SetRenderFont(TextType.UI);

            Vector2 uiCenter = GetCenterPos((float)width, (float)height);
            Vector2 res = new Vector2(width, height);

            Vector2 scalingFact = GetScalingFactor((float)width, (float)height);

            const float orignalWidth = 800;
            const float orignalHeight = 100;

            float actualWidth = orignalWidth * scalingFact.X;
            float actualHeight = orignalHeight * scalingFact.Y;

            const float originalX = 100;
            float actualX = originalX * scalingFact.X;

            textRenderer.RenderTexture(_titleTex, (int)actualWidth, (int)actualHeight, (int)actualX, (int)uiCenter.Y);

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
            if (!_player.IsDisposed)
                _player.Dispose();
        }

        /// <summary>
        /// Updates the frame.
        /// </summary>
        public override void UpdateFrame()
        {
            if (_player.State == MediaState.Stopped)
            {
                _player.IsLooped = true;
                _player.Play(_video);
            }
        }

        /// <summary>
        /// Updates the UI mouse.
        /// </summary>
        /// <param name="mouseState">State of the mouse.</param>
        /// <param name="window">The window.</param>
        public override void UpdateUIMouse(Microsoft.Xna.Framework.Input.MouseState mouseState, GameWindow window)
        {
            //foreach (UIButton uiBtn in _buttons)
            //{
            //    if (uiBtn.MouseInBtnZone(mouseState.X, mouseState.Y, window))
            //    {
            //        if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            //        {
            //            if (uiBtn.OnClicked != null)
            //                uiBtn.OnClicked();
            //        }
            //        else
            //        {
            //            uiBtn.TextColor = UISelectionColor;
            //        }
            //    }
            //}
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