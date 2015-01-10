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
using RenderingSystem;

namespace My_Xna_Game.UI
{
    /// <summary>
    ///
    /// </summary>
    internal class DialogUIFrame : UIFrame
    {
        public const string FRAME_ID = "dialog ui frame";

        private List<UIButton> _buttons = new List<UIButton>();
        private Dialog _dialog;
        private Action<PlayerResponse> _onResponse;
        private GameTexture _solidColorTex;
        private float f_uiBackingHeight;
        private int i_progressDialogIndex = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogUIFrame"/> class.
        /// </summary>
        public DialogUIFrame()
            : base(FRAME_ID, false, true, false)
        {
            _solidColorTex = new GameTexture("images/UI/SolidColor");
        }

        /// <summary>
        /// Draws the frame.
        /// </summary>
        /// <param name="textRenderer">The text renderer.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="game">The game.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public override void DrawFrame(TextRenderer textRenderer, int width, int height, XnaGame game)
        {
            float fWidth = (float)width;
            float fHeight = (float)height;

            Vector2 scaling = GetScalingFactor(fWidth, fHeight);
            Vector2 res = new Vector2(width, height);

            if (_dialog == null)
                throw new ArgumentException();

            float txtScale = 0.5f;

            float uiBackingY = fHeight - f_uiBackingHeight;

            textRenderer.RenderTexture(_solidColorTex.Texture, (int)fWidth, (int)f_uiBackingHeight, 0, (int)uiBackingY, UIBackingColor);

            float marginX = 10f * scaling.X;
            float marginY = 2f * scaling.Y;
            float x, y;

            if (i_progressDialogIndex != -1)
            {
                AISpeech speech = _dialog.AISpeech;
                string currentTxt = speech.Speeches[i_progressDialogIndex];

                textRenderer.RenderText(currentTxt, new Vector2(marginX, uiBackingY + marginY), Color.White, scaling * txtScale);

                string promptTxt = "Press A";
                float promptTxtWidth = textRenderer.GetLengthOfStr(promptTxt) * scaling.X * txtScale;
                float promptTxtHeight = textRenderer.GetHeightOfStr(promptTxt) * scaling.Y * txtScale;

                marginX = 50f * scaling.X;
                marginY = 20f * scaling.Y;

                x = fWidth - promptTxtWidth - marginX;
                y = fHeight - promptTxtHeight - marginY;

                textRenderer.RenderText(promptTxt, new Vector2(x, y), Color.White, scaling * txtScale);
            }
            else
            {
                // Display the responses.

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
        }

        /// <summary>
        /// Loads the dialog data.
        /// </summary>
        /// <param name="dialog">The dialog.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="textRenderer">The text renderer.</param>
        /// <param name="onResponse">The on response.</param>
        public void LoadDialogData(Dialog dialog, int width, int height, TextRenderer textRenderer, Action<PlayerResponse> onResponse)
        {
            _onResponse = null;
            _onResponse += onResponse;
            _dialog = dialog;
            i_selectedIndex = 0;
            i_progressDialogIndex = 0;

            float fWidth = (float)width;
            float fHeight = (float)height;

            List<PlayerResponse> responses = _dialog.PlayerResponses;

            textRenderer.SetRenderFont(TextType.UI);
            Vector2 scaling = GetScalingFactor(fWidth, fHeight);

            f_uiBackingHeight = 300f * scaling.Y;

            float marginX = 40f * scaling.X;
            float marginY = 5f * scaling.Y;

            const float txtSize = 0.5f;

            float btnHeight = textRenderer.GetHeightOfStr("Asdf") * scaling.Y * txtSize;

            Vector2 pos = new Vector2(marginX, (fHeight - f_uiBackingHeight) + marginY + (btnHeight / 2f));

            float btnMargin = 5f * scaling.Y;

            _buttons.Clear();
            foreach (PlayerResponse response in responses)
            {
                float txtWidth = textRenderer.GetLengthOfStr(response.OptionTxt) * scaling.X * txtSize;
                pos.X = marginX + (txtWidth / 2f);
                _buttons.Add(new UIButton(pos, response.OptionTxt, Color.Black, Color.White, txtSize));

                pos.Y += btnHeight + btnMargin;
            }
        }

        /// <summary>
        /// Called when [BTN selected].
        /// </summary>
        /// <param name="game">The game.</param>
        public override void OnBtnSelected(XnaGame game)
        {
            PlayClickSoundEffect();
            if (i_progressDialogIndex == -1)
            {
                // We are now done with this dialog frame.
                _onResponse(_dialog.PlayerResponses[i_selectedIndex]);
                return;
            }

            if (i_progressDialogIndex < _dialog.AISpeech.Speeches.Count - 1)
            {
                i_progressDialogIndex++;
            }
            else
            {
                if (_dialog.PlayerResponses.Count == 0)
                {
                    _onResponse(null);
                    return;
                }
                i_progressDialogIndex = -1;
            }
        }

        /// <summary>
        /// Called when [close].
        /// </summary>
        /// <param name="game">The game.</param>
        public override void OnClose(XnaGame game)
        {
            if (game.OrbletMgr.HelperOrblet.Summoned)
            {
                game.OrbletMgr.HelperOrblet.Dismiss(game);
            }
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            _solidColorTex.Unload();
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