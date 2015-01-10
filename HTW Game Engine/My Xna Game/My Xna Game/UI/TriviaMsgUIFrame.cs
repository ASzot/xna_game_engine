#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using BaseLogic;
using Microsoft.Xna.Framework;

namespace My_Xna_Game.UI
{
    /// <summary>
    ///
    /// </summary>
    internal class TriviaMsgUIFrame : UIFrame
    {
        public const string FRAME_ID = "trivia msg";
        public const uint MILLISECONDS_DISPLAYED = 500;

        private Color _msgDisplayColor;
        private bool b_onCloseParam;
        private Action<bool> fn_onClose;
        private string s_msgToDisplay;
        private string s_navToNext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TriviaMsgUIFrame"/> class.
        /// </summary>
        /// <param name="fnOnClose">The function on close.</param>
        /// <param name="onCloseParam">if set to <c>true</c> [on close parameter].</param>
        /// <param name="navToNext">The nav to next.</param>
        /// <param name="triviaQuestion">The trivia question.</param>
        public TriviaMsgUIFrame(Action<bool> fnOnClose, bool onCloseParam, string navToNext, Trivia.TriviaQuestion triviaQuestion)
            : base(FRAME_ID, false, true, false)
        {
            fn_onClose = fnOnClose;
            b_onCloseParam = onCloseParam;
            s_navToNext = navToNext;

            s_msgToDisplay = b_onCloseParam ? "Correct" : "Incorrect";
            _msgDisplayColor = b_onCloseParam ? Color.Green : Color.Red;

            GameSystem.GameSys_Instance.ProcessMgr.AddProcess(new BaseLogic.Process.WaitEventProcess(MILLISECONDS_DISPLAYED, () =>
            {
                s_moveToFrame = s_navToNext;
                fn_onClose(b_onCloseParam);
            }));
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
            Vector2 scalingFact = GetScalingFactor(fWidth, fHeight);

            const float txtSize = 1f;

            scalingFact *= txtSize;

            textRenderer.SetRenderFont(TextType.UI);
            float txtWidth = textRenderer.GetLengthOfStr(s_msgToDisplay) * scalingFact.X;
            float txtHeight = textRenderer.GetHeightOfStr(s_msgToDisplay) * scalingFact.Y;

            Vector2 pos = new Vector2((fWidth / 2f) - (txtWidth / 2f), (fHeight / 2f) - (txtHeight / 2f));

            textRenderer.RenderText(s_msgToDisplay, pos, _msgDisplayColor, scalingFact);
        }
    }
}