#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace BaseLogic.Graphics
{
    /// <summary>
    /// Renders lines.
    /// </summary>
    public class PrimitivesRenderer
    {
        // The first component of the vector array is going to be the start position, the second is the end position, and the third is the color of the line as a vector3.
        /// <summary>
        /// The _lines to draw
        /// </summary>
        private List<Vector3[]> _linesToDraw = new List<Vector3[]>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PrimitivesRenderer"/> class.
        /// </summary>
        public PrimitivesRenderer()
        {
        }

        /// <summary>
        /// Gets the lines to draw.
        /// </summary>
        /// <value>
        /// The lines to draw.
        /// </value>
        public List<Vector3[]> LinesToDraw
        {
            get { return _linesToDraw; }
        }

        /// <summary>
        /// Adds the render line.
        /// </summary>
        /// <param name="startPos">The start position.</param>
        /// <param name="endPos">The end position.</param>
        /// <param name="color">The color.</param>
        public void AddRenderLine(Vector3 startPos, Vector3 endPos, Color color)
        {
            Vector3[] line = { startPos, endPos, color.ToVector3() };
            _linesToDraw.Add(line);
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        public void LoadContent()
        {
        }

        /// <summary>
        /// Called when [finished rendering].
        /// </summary>
        public void OnFinishedRendering()
        {
            // All of the render primitives clear out every frame.
            _linesToDraw.Clear();
        }
    }
}