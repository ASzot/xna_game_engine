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
    /// Functionality for a button in a UI frame.
    /// </summary>
    internal class UIButton
    {
        public const float DEFAULT_MARGIN_X = 20f;
        public const float DEFAULT_MARGIN_Y = 10f;

        public Action<XnaGame> OnClicked;
        private Color _boxColor = Color.Black;
        private Color _txtColor = Color.White;
        private float f_txtSize = 1f;
        private string s_text = "Empty";
        private Vector2 v_drawPos = Vector2.Zero;
        private Vector2 v_margin = new Vector2(10f, 10f);
        private Vector2 vcach_btnDim;

        /// <summary>
        /// Initializes a new instance of the <see cref="UIButton"/> class.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <param name="txt">The text.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="textColor">Color of the text.</param>
        /// <param name="size">The size.</param>
        public UIButton(Vector2 pos, string txt, Color backgroundColor, Color textColor, float size)
            : this(pos, txt, backgroundColor, textColor, new Vector2(20f, 10f), size)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UIButton"/> class.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <param name="txt">The text.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="textColor">Color of the text.</param>
        /// <param name="size">The size.</param>
        /// <param name="action">The action.</param>
        public UIButton(Vector2 pos, string txt, Color backgroundColor, Color textColor, float size, Action<XnaGame> action)
            : this(pos, txt, backgroundColor, textColor, new Vector2(20f, 10f), size)
        {
            OnClicked = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UIButton"/> class.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <param name="txt">The text.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="textColor">Color of the text.</param>
        /// <param name="margin">The margin.</param>
        /// <param name="size">The size.</param>
        public UIButton(Vector2 pos, string txt, Color backgroundColor, Color textColor, Vector2 margin, float size)
        {
            v_drawPos = pos;
            s_text = txt;
            _txtColor = textColor;
            _boxColor = backgroundColor;
            f_txtSize = size;
            v_margin = margin;
        }

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>
        /// The color of the background.
        /// </value>
        public Color BackgroundColor
        {
            get { return _boxColor; }
            set { _boxColor = value; }
        }

        /// <summary>
        /// Gets or sets the draw position.
        /// </summary>
        /// <value>
        /// The draw position.
        /// </value>
        public Vector2 DrawPos
        {
            get { return v_drawPos; }
            set { v_drawPos = value; }
        }

        /// <summary>
        /// Gets or sets the margin.
        /// </summary>
        /// <value>
        /// The margin.
        /// </value>
        public Vector2 Margin
        {
            get { return v_margin; }
            set { v_margin = value; }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get { return s_text; }
            set { s_text = value; }
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>
        /// The color of the text.
        /// </value>
        public Color TextColor
        {
            get { return _txtColor; }
            set { _txtColor = value; }
        }

        /// <summary>
        /// Gets or sets the size of the text.
        /// </summary>
        /// <value>
        /// The size of the text.
        /// </value>
        public float TextSize
        {
            get { return f_txtSize; }
            set { f_txtSize = value; }
        }

        /// <summary>
        /// Gets the BTN dim.
        /// </summary>
        /// <param name="txtRenderer">The text renderer.</param>
        /// <returns></returns>
        public Vector2 GetBtnDim(TextRenderer txtRenderer)
        {
            txtRenderer.SetRenderFont(TextType.UI);

            float height = txtRenderer.GetHeightOfStr(s_text);
            float width = txtRenderer.GetLengthOfStr(s_text);

            return new Vector2(width, height);
        }

        /// <summary>
        /// Mouses the in BTN zone.
        /// </summary>
        /// <param name="mouseX">The mouse x.</param>
        /// <param name="mouseY">The mouse y.</param>
        /// <param name="window">The window.</param>
        /// <returns></returns>
        public bool MouseInBtnZone(int mouseX, int mouseY, GameWindow window)
        {
            Vector2 dim = vcach_btnDim;

            //OPTIMIZE:
            Vector2 tl = v_drawPos;
            Vector2 bl = v_drawPos - new Vector2(0f, dim.Y);
            Vector2 tr = v_drawPos - new Vector2(dim.X, 0);
            Vector2 br = v_drawPos - new Vector2(dim.X, dim.Y);

            Vector2 mouseClick = new Vector2(mouseX, mouseY);

            bool inZone = false;
            if (tl.X < mouseX && tl.Y > mouseY)
            {
                if (bl.X < mouseX && bl.Y < mouseY)
                {
                    if (br.X > mouseX && br.Y < mouseY)
                    {
                        if (tr.X > mouseX && tr.Y > mouseY)
                        {
                            inZone = true;
                        }
                    }
                }
            }

            return inZone;
        }

        /// <summary>
        /// Updates the BTN dim.
        /// </summary>
        /// <param name="txtRenderer">The text renderer.</param>
        /// <param name="scale">The scale.</param>
        public void UpdateBtnDim(TextRenderer txtRenderer, float scale)
        {
            vcach_btnDim = GetBtnDim(txtRenderer) * scale;
        }
    }
}