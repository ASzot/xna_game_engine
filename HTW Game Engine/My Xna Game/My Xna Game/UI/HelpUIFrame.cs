#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System.Collections.Generic;
using BaseLogic;
using Microsoft.Xna.Framework.Input;
using RenderingSystem;

namespace My_Xna_Game.UI
{
    internal class HelpUIFrame : UIFrame
    {
        public const string FRAME_ID = "help ui frame";

        private static string s_previousFrameID;
        private int _currentFrame = 0;
        private List<GameTexture> _subFrames = new List<GameTexture>();

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpUIFrame"/> class.
        /// </summary>
        public HelpUIFrame()
            : base(FRAME_ID, true, true, true)
        {
            _subFrames.Add(new GameTexture("images/UI/HelpScreen1"));
            _subFrames.Add(new GameTexture("images/UI/HelpScreen2"));
            _subFrames.Add(new GameTexture("images/UI/HelpScreen3"));
        }

        /// <summary>
        /// Sets the previous frame identifier.
        /// </summary>
        /// <value>
        /// The previous frame identifier.
        /// </value>
        public static string PreviousFrameID
        {
            set { s_previousFrameID = value; }
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
            GameTexture subFrameToDraw = _subFrames[_currentFrame];
            textRenderer.RenderTexture(subFrameToDraw.Texture, width, height, 0, 0);
        }

        /// <summary>
        /// Called when [game pad input].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public override bool OnGamePadInput(Buttons key)
        {
            if (key == Buttons.A)
            {
                NavigateForward();
                return true;
            }
            else if (key == Buttons.B)
            {
                NavigateBack();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Navigates the back.
        /// </summary>
        private void NavigateBack()
        {
            PlayClickSoundEffect();
            if (_currentFrame == 0)
            {
                s_moveToFrame = s_previousFrameID;
            }
            else
            {
                _currentFrame--;
            }
        }

        /// <summary>
        /// Navigates the forward.
        /// </summary>
        private void NavigateForward()
        {
            PlayClickSoundEffect();
            if (_currentFrame == _subFrames.Count - 1)
            {
                s_moveToFrame = s_previousFrameID;
            }
            else
            {
                _currentFrame++;
            }
        }
    }
}