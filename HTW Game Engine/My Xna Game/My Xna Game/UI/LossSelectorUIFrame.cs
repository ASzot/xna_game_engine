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
    internal class LossMsgUIFrame : UIFrame
    {
        public const string FRAME_ID = "loss msg ui frame";
        private static string s_causeTxtStr;
        private GameTexture _backingTex;

        private List<UIButton> _buttons = new List<UIButton>();

        private float f_txtSize = 1.0f;

        /// <summary>
        /// Initializes a new instance of the <see cref="LossMsgUIFrame"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public LossMsgUIFrame(int width, int height)
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

        public static string CauseTxtStr
        {
            set { s_causeTxtStr = value; }
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
            textRenderer.RenderTexture(_backingTex.Texture, width, height, 0, 0, UIBackingColor);

            float fWidth = (float)width;
            float fHeight = (float)height;

            Vector2 res = new Vector2(width, height);

            Vector2 scale = GetScalingFactor(fWidth, fHeight);
            Vector2 txtPos = new Vector2(fWidth / 2f, 200f * scale.Y);

            string lossTxt = "Game Over";

            textRenderer.SetRenderFont(TextType.UI);
            const float lossTxtSize = 2.0f;
            float txtWidth = textRenderer.GetLengthOfStr(lossTxt) * scale.X * lossTxtSize;

            txtPos.X -= (txtWidth / 2f);

            textRenderer.RenderText(lossTxt, txtPos, Color.Red, scale * lossTxtSize);

            if (s_causeTxtStr != null)
            {
                const float causeTxtSize = 1.0f;
                txtWidth = textRenderer.GetLengthOfStr(s_causeTxtStr) * scale.X * causeTxtSize;

                txtPos.X = (fWidth / 2f) - (txtWidth / 2f);
                txtPos.Y = 600f * scale.Y;

                textRenderer.RenderText(s_causeTxtStr, txtPos, Color.Red, scale * f_txtSize);
            }

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