#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BaseLogic
{
    /// <summary>
    /// The two different types of font which can be used.
    /// </summary>
    public enum TextType { UI, Default };

    /// <summary>
    /// Renders all text to the screen.
    /// </summary>
    public class TextRenderer
    {
        /// <summary>
        /// The target height. This will change the resolution of the text.
        /// </summary>
        private const float TARGET_HEIGHT = 600f;

        /// <summary>
        /// The target width. This will change the resolution of the text.
        /// </summary>
        private const float TARGET_WIDTH = 800f;

        /// <summary>
        /// The _current font
        /// </summary>
        private SpriteFont _currentFont;

        /// <summary>
        /// The _def font
        /// </summary>
        private SpriteFont _defFont;

        /// <summary>
        /// The _sprite batch
        /// </summary>
        private SpriteBatch _spriteBatch;

        /// <summary>
        /// The _ui font
        /// </summary>
        private SpriteFont _uiFont;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextRenderer"/> class.
        /// </summary>
        public TextRenderer()
        {
        }

        /// <summary>
        /// Begins the render text.
        /// </summary>
        public void BeginRenderText()
        {
            _spriteBatch.Begin();
        }

        /// <summary>
        /// Ends the render text.
        /// </summary>
        /// <param name="device">The device.</param>
        public void EndRenderText(GraphicsDevice device)
        {
            _spriteBatch.End();

            device.BlendState = BlendState.Opaque;
        }

        /// <summary>
        /// Gets the height of string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public float GetHeightOfStr(string str)
        {
            return _currentFont.MeasureString(str).Y;
        }

        /// <summary>
        /// Gets the length of string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public float GetLengthOfStr(string str)
        {
            return _currentFont.MeasureString(str).X;
        }

        /// <summary>
        /// Gets the scaling factor.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public Vector2 GetScalingFactor(float width, float height)
        {
            return new Vector2(width / TARGET_WIDTH, height / TARGET_HEIGHT);
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="content">The content.</param>
        public void LoadContent(GraphicsDevice device, ContentManager content)
        {
            _spriteBatch = new SpriteBatch(device);
            _defFont = content.Load<SpriteFont>("defaultFont");
            _uiFont = content.Load<SpriteFont>("GameUIFont");
            _currentFont = _defFont;
        }

        /// <summary>
        /// Renders the text.
        /// </summary>
        /// <param name="renderTxtStr">The render text string.</param>
        /// <param name="pos">The position.</param>
        /// <param name="color">The color.</param>
        /// <param name="scale">The scale.</param>
        public void RenderText(string renderTxtStr, Vector2 pos, Color color, Vector2 scale)
        {
            _spriteBatch.DrawString(_currentFont, renderTxtStr, pos, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Renders the text.
        /// </summary>
        /// <param name="renderTxtStr">The render text string.</param>
        /// <param name="pos">The position.</param>
        /// <param name="color">The color.</param>
        public void RenderText(string renderTxtStr, Vector2 pos, Color color)
        {
            RenderText(renderTxtStr, pos, color, Vector2.One);
        }

        /// <summary>
        /// Renders the texture.
        /// </summary>
        /// <param name="tex">The tex.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void RenderTexture(Texture2D tex, int width, int height, int x, int y)
        {
            RenderTexture(tex, width, height, x, y, Color.White);
        }

        /// <summary>
        /// Renders the texture.
        /// </summary>
        /// <param name="tex">The tex.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="color">The color.</param>
        public void RenderTexture(Texture2D tex, int width, int height, int x, int y, Color color)
        {
            _spriteBatch.Draw(tex, new Rectangle(x, y, width, height), color);
        }

        /// <summary>
        /// Renders the texture.
        /// </summary>
        /// <param name="tex">The tex.</param>
        /// <param name="sourceRectangle">The source rectangle.</param>
        /// <param name="destRectangle">The dest rectangle.</param>
        /// <param name="color">The color.</param>
        public void RenderTexture(Texture2D tex, Rectangle sourceRectangle, Rectangle destRectangle, Color color)
        {
            _spriteBatch.Draw(tex, destRectangle, sourceRectangle, color);
        }

        /// <summary>
        /// Renders the texture.
        /// </summary>
        /// <param name="tex">The tex.</param>
        /// <param name="scaling">The scaling.</param>
        /// <param name="pos">The position.</param>
        /// <param name="color">The color.</param>
        /// <param name="rotAngle">The rot angle.</param>
        /// <param name="spriteEffect">The sprite effect.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="layerDepth">The layer depth.</param>
        public void RenderTexture(Texture2D tex, Vector2 scaling, Vector2 pos, Color color, float rotAngle, SpriteEffects spriteEffect, Vector2 origin, float layerDepth)
        {
            Rectangle destRectangle = new Rectangle(0, 0, tex.Width, tex.Height);
            _spriteBatch.Draw(tex, pos, destRectangle, color, rotAngle, origin, scaling, spriteEffect, layerDepth);
        }

        /// <summary>
        /// Sets the render font.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void SetRenderFont(TextType type)
        {
            if (type == TextType.Default)
                _currentFont = _defFont;
            else if (type == TextType.UI)
                _currentFont = _uiFont;
            else
                throw new ArgumentException();
        }
    }
}