#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using BaseLogic;
using Microsoft.Xna.Framework;
using RenderingSystem;

namespace My_Xna_Game.UI
{
    /// <summary>
    ///
    /// </summary>
    internal class TriviaUIFrame : UIFrame
    {
        public const string FRAME_ID = "trivia ui";
        private GameTexture _backingTex;
        private List<UIButton> _optionBtns = new List<UIButton>();
        private Trivia.TriviaQuestion _triviaData;
        private float f_backingHeight = 0f;
        private float f_backingWidth = 0f;
        private float f_txtScaling = 1f;
        private Action<bool> fn_onTriviaAnswered;
        private Vector2 v_backingPos;

        /// <summary>
        /// Initializes a new instance of the <see cref="TriviaUIFrame"/> class.
        /// </summary>
        public TriviaUIFrame()
            : base(FRAME_ID, false, true, true)
        {
            i_selectedIndex = 1;
            _backingTex = new GameTexture("images/UI/SolidColor");
        }

        /// <summary>
        /// Decrements the index.
        /// </summary>
        public override void DecrementIndex()
        {
            if (i_selectedIndex > 1)
                --i_selectedIndex;
            else
                i_selectedIndex = GetBtnCount() - 1;
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

            Vector2 res = new Vector2(width, height);

            textRenderer.SetRenderFont(TextType.UI);
            float btnHeight = textRenderer.GetHeightOfStr("Asdf") * res.Y;
            float finalDrawPos = _optionBtns[_optionBtns.Count - 1].DrawPos.Y + btnHeight;

            textRenderer.RenderTexture(_backingTex.Texture, (int)f_backingWidth, (int)f_backingHeight,
                (int)v_backingPos.X, (int)v_backingPos.Y, UIBackingColor);

            int answersCount = _triviaData.PossibleAnswers.Count();

            for (int i = 0; i < _optionBtns.Count; ++i)
            {
                UIButton uiBtn = _optionBtns[i];
                if (i == i_selectedIndex)
                    uiBtn.TextColor = UISelectionColor;
                else
                    uiBtn.TextColor = Color.White;
                DrawBtn(uiBtn.DrawPos, uiBtn.Text, uiBtn.Margin, uiBtn.TextSize * f_txtScaling, uiBtn.TextColor, uiBtn.BackgroundColor, textRenderer, res);
            }
        }

        /// <summary>
        /// Increments the index.
        /// </summary>
        public override void IncrementIndex()
        {
            if (i_selectedIndex < GetBtnCount() - 1)
                ++i_selectedIndex;
            else
                i_selectedIndex = 1;
        }

        /// <summary>
        /// Loads the trivia data.
        /// </summary>
        /// <param name="triviaData">The trivia data.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="textRenderer">The text renderer.</param>
        /// <param name="onTriviaAnswered">The on trivia answered.</param>
        public void LoadTriviaData(Trivia.TriviaQuestion triviaData, int width, int height, TextRenderer textRenderer, Action<bool> onTriviaAnswered)
        {
            _triviaData = triviaData;
            i_selectedIndex = 1;

            float fHeight = (float)height;
            float fWidth = (float)width;
            Vector2 scale = GetScalingFactor(fWidth, fHeight);

            float y = fHeight / 6f;
            float x = fWidth / 2f;

            textRenderer.SetRenderFont(TextType.UI);

            float btnHeight = textRenderer.GetHeightOfStr("Asdf");
            const float txtScale = 0.5f;
            btnHeight *= txtScale * scale.Y;

            float marginY = 50f * scale.Y;

            Vector2 btnPos = new Vector2(x, y);

            _optionBtns.Clear();

            string questionStr = "Q: " + _triviaData.Question;
            _optionBtns.Add(new UIButton(btnPos, questionStr, Color.Black, Color.White, txtScale));

            int answersCount = triviaData.PossibleAnswers.Count();

            string[] optionStrs = new string[answersCount];
            for (int i = 0; i < answersCount; ++i)
            {
                string str = _triviaData.PossibleAnswers.ElementAt(i);
                optionStrs[i] = (i + 1).ToString() + ") " + str;
            }

            float longestOptionWidth = 0f;
            for (int i = 0; i < optionStrs.Count(); ++i)
            {
                string optionStr = optionStrs.ElementAt(i);
                btnPos.Y += marginY + btnHeight;

                _optionBtns.Add(new UIButton(btnPos, optionStr, Color.Black, Color.White, txtScale));
                float optionWidth = textRenderer.GetLengthOfStr(optionStr) * scale.X * txtScale;
                if (optionWidth > longestOptionWidth)
                    longestOptionWidth = optionWidth;
            }

            float questionWidth = textRenderer.GetLengthOfStr(questionStr) * scale.X * txtScale;
            if (questionWidth > longestOptionWidth)
                longestOptionWidth = questionWidth;

            btnPos.Y += marginY + btnHeight;

            if (AllowQuit)
                _optionBtns.Add(new UIButton(btnPos, "Close", Color.Black, Color.White, txtScale, OnClose));

            btnPos.Y += marginY + btnHeight;

            f_backingHeight = btnPos.Y - y;

            marginY = 30f * scale.Y;
            float marginX = 20f * scale.X;
            f_backingWidth = longestOptionWidth + marginX;

            v_backingPos.X = x - (f_backingWidth / 2f);
            v_backingPos.Y = y - marginY;

            fn_onTriviaAnswered = onTriviaAnswered;

            if (_triviaData.Question.Length > 80)
                f_txtScaling = 0.6f;
            else if (_triviaData.Question.Length > 60)
                f_txtScaling = 0.75f;
        }

        /// <summary>
        /// Called when [BTN selected].
        /// </summary>
        /// <param name="game">The game.</param>
        public override void OnBtnSelected(XnaGame game)
        {
            PlayClickSoundEffect();
            if (i_selectedIndex == GetBtnCount() - 1 && _optionBtns[i_selectedIndex].Text == "Close")
            {
                // The user choose the close button.
                fn_onTriviaAnswered(false);
                s_moveToFrame = PlayerUIFrame.FRAME_ID;
                if (_optionBtns[i_selectedIndex].OnClicked != null)
                    _optionBtns[i_selectedIndex].OnClicked(game);
                else
                {
                    OnClose(game);
                }
                return;
            }

            TriviaMsgUIFrame triviaMsgUIFrame = new TriviaMsgUIFrame(fn_onTriviaAnswered,
                i_selectedIndex - 1 == _triviaData.CorrectAnswerIndex, PlayerUIFrame.FRAME_ID, _triviaData);

            GameUI.GameUI_Instance.ImmediateNavigateToFrame(triviaMsgUIFrame);
        }

        /// <summary>
        /// Called when [close].
        /// </summary>
        /// <param name="game">The game.</param>
        public override void OnClose(XnaGame game)
        {
            game.GetGameUserPlayer().GoldCount--;
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
            _backingTex.Unload();
        }

        /// <summary>
        /// Gets the BTN count.
        /// </summary>
        /// <returns></returns>
        protected override int GetBtnCount()
        {
            return _optionBtns.Count;
        }
    }
}