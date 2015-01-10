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
    internal class SelectProfileUIFrame : UIFrame
    {
        public const string FRAME_ID = "profile selector frame";

        private GameTexture _backgroundTex;
        private List<UIButton> _buttons = new List<UIButton>();
        private float f_height;
        private float f_txtSize = 1.0f;
        private float f_width;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectProfileUIFrame"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public SelectProfileUIFrame(int width, int height)
            : base(FRAME_ID, true, true, true)
        {
            _backgroundTex = new GameTexture("images/UI/GradientBackground");

            f_width = (float)width;
            f_height = (float)height;
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
            f_width = (float)width;
            f_height = (float)height;

            textRenderer.RenderTexture(_backgroundTex.Texture, width, height, 0, 0);

            Vector2 scale = GetScalingFactor(f_width, f_height);

            textRenderer.SetRenderFont(TextType.UI);

            UpdateGameProfiles(game.ProfileMgr, textRenderer);

            int selectedProfileIndex = game.ProfileMgr.SelectedProfileIndex;
            if (selectedProfileIndex != -1)
            {
                UIButton selectedUIBtn = _buttons[selectedProfileIndex + 1];
                float txtWidth = textRenderer.GetLengthOfStr(selectedUIBtn.Text) * scale.X * f_txtSize;
                float txtHeight = textRenderer.GetHeightOfStr(selectedUIBtn.Text) * scale.Y * f_txtSize;
                Vector2 drawPos = selectedUIBtn.DrawPos;
                drawPos.X -= (txtWidth / 2f) + 50f * scale.X;
                drawPos.Y -= (txtHeight / 2f) + 20f * scale.Y;
                textRenderer.RenderText("-", drawPos, Color.White, scale * f_txtSize);
            }

            for (int i = 0; i < _buttons.Count; ++i)
            {
                UIButton uiBtn = _buttons[i];
                if (i == i_selectedIndex)
                {
                    uiBtn.TextColor = UISelectionColor;
                }
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

            if (i_selectedIndex == 0 || i_selectedIndex == GetBtnCount() - 1)
                return;

            game.ProfileMgr.SelectedProfileIndex = i_selectedIndex - 1;
        }

        /// <summary>
        /// Called when [game pad input].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public override bool OnGamePadInput(Buttons key)
        {
            if (key == Buttons.X)
            {
                DeleteSelectedProfile();
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
            if (key == Keys.Delete || key == Keys.Back)
            {
                DeleteSelectedProfile();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            _backgroundTex.Unload();
        }

        /// <summary>
        /// Updates the game profiles.
        /// </summary>
        /// <param name="profileMgr">The profile MGR.</param>
        /// <param name="textRenderer">The text renderer.</param>
        public void UpdateGameProfiles(Core.ProfileMgr profileMgr, TextRenderer textRenderer)
        {
            Vector2 scale = GetScalingFactor(f_width, f_height);

            float offsetY = 100f * scale.Y;

            float buttonHeight = textRenderer.GetHeightOfStr("Asdf");
            buttonHeight *= f_txtSize * scale.Y;

            Vector2 btnPos = new Vector2(f_width / 2f, offsetY);

            _buttons.Clear();

            _buttons.Add(new UIButton(btnPos, "Add New", Color.Black, Color.White, f_txtSize, (XnaGame game) =>
            {
                CreateProfileUIFrame.NextFrameId = FRAME_ID;
                CreateProfileUIFrame.PreviousFrameId = FRAME_ID;
                s_moveToFrame = CreateProfileUIFrame.FRAME_ID;
            }));

            btnPos.Y += buttonHeight;

            foreach (var profile in profileMgr.GetDataElements())
            {
                string txtStr = profile.ToString();
                _buttons.Add(new UIButton(btnPos, txtStr, Color.Black, Color.White, f_txtSize));
                btnPos.Y += buttonHeight;
            }

            _buttons.Add(new UIButton(btnPos, "Back", Color.Black, Color.White, f_txtSize, (XnaGame game) =>
            {
                s_moveToFrame = MenuUIFrame.FRAME_ID;
            }));
        }

        /// <summary>
        /// Gets the BTN count.
        /// </summary>
        /// <returns></returns>
        protected override int GetBtnCount()
        {
            return _buttons.Count;
        }

        /// <summary>
        /// Deletes the selected profile.
        /// </summary>
        private void DeleteSelectedProfile()
        {
            XnaGame game = XnaGame.Game_Instance;
            // Do we have a valid index.
            if (i_selectedIndex > 0 && i_selectedIndex != GetBtnCount() - 1 && GetBtnCount() > 2)
            {
                game.ProfileMgr.RemoveDataElement(i_selectedIndex - 1);
                if (game.ProfileMgr.GetNumberOfDataElements() > 0)
                {
                    if (game.ProfileMgr.SelectedProfileIndex > 0)
                        game.ProfileMgr.SelectedProfileIndex--;
                    else
                        game.ProfileMgr.SelectedProfileIndex = 0;
                }
                else
                    game.ProfileMgr.SelectedProfileIndex = -1;
            }
        }
    }
}