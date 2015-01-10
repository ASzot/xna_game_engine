#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using BaseLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RenderingSystem;

namespace My_Xna_Game.UI
{
    /// <summary>
    ///
    /// </summary>
    public abstract class UIFrame
    {
        public const string INVALID_UI_FRAME = "invalid";

        protected const int TARGET_X = 1920;
        protected const int TARGET_Y = 1080;
        protected int i_selectedIndex;
        protected string s_moveToFrame = INVALID_UI_FRAME;
        private const float BTN_CLICK_VOLUME = 0.5f;
        private Texture2D _buttonTexture;
        private bool b_allowQuit = true;
        private bool b_exclusiveInput = false;
        private bool b_exclusiveRendering = false;
        private bool b_exclusiveUpdate = false;
        private string s_id;

        public UIFrame(string id, bool exclusive, bool exclusiveInput, bool exclusiveUpdate)
        {
            s_id = id;
            b_exclusiveRendering = exclusive;
            b_exclusiveInput = exclusiveInput;
            b_exclusiveUpdate = exclusiveUpdate;
            _buttonTexture = ResourceMgr.LoadTexture("Default/DefBtnBackground");
        }

        /// <summary>
        /// Gets the color of the UI backing.
        /// </summary>
        /// <value>
        /// The color of the UI backing.
        /// </value>
        public static Color UIBackingColor
        {
            get
            {
                Color color = new Color(20, 20, 20, 200);
                return color;
            }
        }

        /// <summary>
        /// Gets the color of the UI selection.
        /// </summary>
        /// <value>
        /// The color of the UI selection.
        /// </value>
        public static Color UISelectionColor
        {
            get { return Color.Red; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [allow quit].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow quit]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowQuit
        {
            get { return b_allowQuit; }
            set { b_allowQuit = value; }
        }

        /// <summary>
        /// Gets a value indicating whether [exclusive input].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [exclusive input]; otherwise, <c>false</c>.
        /// </value>
        public bool ExclusiveInput
        {
            get { return b_exclusiveInput; }
        }

        /// <summary>
        /// Gets a value indicating whether [exclusive rendering].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [exclusive rendering]; otherwise, <c>false</c>.
        /// </value>
        public bool ExclusiveRendering
        {
            get { return b_exclusiveRendering; }
        }

        /// <summary>
        /// Gets a value indicating whether [exclusive update].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [exclusive update]; otherwise, <c>false</c>.
        /// </value>
        public bool ExclusiveUpdate
        {
            get { return b_exclusiveUpdate; }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string ID
        {
            get { return s_id; }
        }

        /// <summary>
        /// Gets the move to frame.
        /// </summary>
        /// <value>
        /// The move to frame.
        /// </value>
        public string MoveToFrame
        {
            get { return s_moveToFrame; }
        }

        /// <summary>
        /// Decrements the index.
        /// </summary>
        public virtual void DecrementIndex()
        {
            if (i_selectedIndex > 0)
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
        public virtual void DrawFrame(TextRenderer textRenderer, int width, int height, XnaGame game)
        {
        }

        /// <summary>
        /// Increments the index.
        /// </summary>
        public virtual void IncrementIndex()
        {
            if (i_selectedIndex < GetBtnCount() - 1)
                ++i_selectedIndex;
            else
                i_selectedIndex = 0;
        }

        /// <summary>
        /// Sets the move to frame data to the invalid frame string constant.
        /// </summary>
        public void InvalidateMoveFrame()
        {
            s_moveToFrame = INVALID_UI_FRAME;
        }

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <param name="frameID">The frame identifier.</param>
        public void NavigateTo(string frameID)
        {
            s_moveToFrame = frameID;
        }

        /// <summary>
        /// Called when [BTN selected].
        /// </summary>
        /// <param name="game">The game.</param>
        public virtual void OnBtnSelected(XnaGame game)
        {
        }

        /// <summary>
        /// Called when [close].
        /// </summary>
        /// <param name="game">The game.</param>
        public virtual void OnClose(XnaGame game)
        {
        }

        /// <summary>
        /// Called when [game pad input].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public virtual bool OnGamePadInput(Microsoft.Xna.Framework.Input.Buttons key)
        {
            return false;
        }

        /// <summary>
        /// Called when [key input].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public virtual bool OnKeyInput(Microsoft.Xna.Framework.Input.Keys key)
        {
            return false;
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public virtual void UnloadContent()
        {
        }

        /// <summary>
        /// Updates the frame.
        /// </summary>
        public virtual void UpdateFrame()
        {
        }

        /// <summary>
        /// Updates the UI mouse.
        /// </summary>
        /// <param name="mouseState">State of the mouse.</param>
        /// <param name="window">The window.</param>
        public virtual void UpdateUIMouse(Microsoft.Xna.Framework.Input.MouseState mouseState, GameWindow window)
        {
        }

        /// <summary>
        /// Draws the BTN.
        /// </summary>
        /// <param name="centerX">The center x.</param>
        /// <param name="centerY">The center y.</param>
        /// <param name="text">The text.</param>
        /// <param name="marginX">The margin x.</param>
        /// <param name="marginY">The margin y.</param>
        /// <param name="txtSize">Size of the text.</param>
        /// <param name="txtColor">Color of the text.</param>
        /// <param name="boxColor">Color of the box.</param>
        /// <param name="textRenderer">The text renderer.</param>
        /// <param name="resX">The resource x.</param>
        /// <param name="resY">The resource y.</param>
        protected void DrawBtn(float centerX, float centerY, string text, float marginX, float marginY, float txtSize,
            Color txtColor, Color boxColor, TextRenderer textRenderer, float resX, float resY)
        {
            Vector2 scalingFactor = GetScalingFactor(resX, resY);

            marginX *= scalingFactor.X;
            marginY *= scalingFactor.Y;

            float textLength = textRenderer.GetLengthOfStr(text) * scalingFactor.X * txtSize;
            float textHeight = textRenderer.GetHeightOfStr(text) * scalingFactor.Y * txtSize;

            Vector2 textRenderPos = new Vector2(-textLength, -textHeight);
            textRenderPos = textRenderPos / 2;
            textRenderPos.X += centerX;
            textRenderPos.Y += centerY;

            Vector2 boxRenderPos = new Vector2(centerX, centerY);
            boxRenderPos.X -= (textLength / 2);
            boxRenderPos.Y -= (textHeight / 2);
            boxRenderPos.X -= marginX;
            boxRenderPos.Y -= marginY;

            float boxWidth = (centerX - boxRenderPos.X) * 2;
            float boxHeight = (centerY - boxRenderPos.Y) * 2;

            boxWidth *= scalingFactor.X;
            boxHeight *= scalingFactor.Y;

            textRenderer.RenderTexture(_buttonTexture, (int)boxWidth, (int)boxHeight, (int)boxRenderPos.X, (int)boxRenderPos.Y, boxColor);
            textRenderer.RenderText(text, textRenderPos, txtColor, scalingFactor * txtSize);
        }

        /// <summary>
        /// Draws the BTN.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="text">The text.</param>
        /// <param name="margin">The margin.</param>
        /// <param name="txtSize">Size of the text.</param>
        /// <param name="txtColor">Color of the text.</param>
        /// <param name="boxColor">Color of the box.</param>
        /// <param name="txtRenderer">The text renderer.</param>
        /// <param name="res">The resource.</param>
        protected void DrawBtn(Vector2 point, string text, Vector2 margin, float txtSize, Color txtColor, Color boxColor, TextRenderer txtRenderer, Vector2 res)
        {
            DrawBtn(point.X, point.Y, text, margin.X, margin.Y, txtSize, txtColor, boxColor, txtRenderer, res.X, res.Y);
        }

        /// <summary>
        /// Gets the BTN count.
        /// </summary>
        /// <returns></returns>
        protected virtual int GetBtnCount()
        {
            return 0;
        }

        /// <summary>
        /// Gets the center position.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        protected Vector2 GetCenterPos(float width, float height)
        {
            return new Vector2(width / 2f, height / 2f);
        }

        /// <summary>
        /// Gets the scaling factor.
        /// </summary>
        /// <param name="resX">The resource x.</param>
        /// <param name="resY">The resource y.</param>
        /// <returns></returns>
        protected Vector2 GetScalingFactor(float resX, float resY)
        {
            return new Vector2(resX / (float)TARGET_X, resY / (float)TARGET_Y);
        }

        /// <summary>
        /// Plays the click sound effect.
        /// </summary>
        protected void PlayClickSoundEffect()
        {
            if (b_exclusiveUpdate)
                XnaGame.Game_Instance.SoundHelper.ImmediatePlaySoundEffect("ButtonClick", false, BTN_CLICK_VOLUME);
            else
                XnaGame.Game_Instance.SoundHelper.PlaySoundEffect("ButtonClick", false, BTN_CLICK_VOLUME);
        }
    }
}