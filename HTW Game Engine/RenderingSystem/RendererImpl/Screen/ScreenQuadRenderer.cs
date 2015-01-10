#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//TODO:
// Remove this class and replace with QuadRenderer.
// They do pretty much the same thing.

namespace RenderingSystem.RendererImpl
{
    /// <summary>
    /// Used to render a quad on the screen... wait this looks familiar...
    /// I don't know why there are two of these. Use the QuadRenderer.
    /// Does the exact same thing as the quad renderer.
    /// </summary>
    public class ScreenQuadRenderer
    {
        private short[] _indexBuffer = null;

        /// <summary>
        /// The vertices of the quad which will be rendered.
        /// </summary>
        private VertexPositionTexture[] _vertexBuffer = null;

        /// <summary>
        /// Construct the screen quad renderer, making the vertex and index buffer populating it with data.
        /// The data will be altered when the quad is rendered.
        /// </summary>
        public ScreenQuadRenderer()
        {
            _vertexBuffer = new VertexPositionTexture[4];
            _vertexBuffer[0] = new VertexPositionTexture(new Vector3(-1, 1, 1), new Vector2(0, 0));
            _vertexBuffer[1] = new VertexPositionTexture(new Vector3(1, 1, 1), new Vector2(1, 0));
            _vertexBuffer[2] = new VertexPositionTexture(new Vector3(-1, -1, 1), new Vector2(0, 1));
            _vertexBuffer[3] = new VertexPositionTexture(new Vector3(1, -1, 1), new Vector2(1, 1));

            _indexBuffer = new short[] { 0, 3, 2, 0, 1, 3 };
        }

        /// <summary>
        /// Render the quad with the two vertices on the screen.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="v1">The first vertex, (0,0) is top left, (width, height) is bottom right.</param>
        /// <param name="v2">The second vertex, (0,0) is top left, (width, height) is bottom right.</param>
        public void RenderQuad(GraphicsDevice graphicsDevice, Vector2 v1, Vector2 v2)
        {
            _vertexBuffer[0].Position.X = v1.X;
            _vertexBuffer[0].Position.Y = v2.Y;

            _vertexBuffer[1].Position.X = v2.X;
            _vertexBuffer[1].Position.Y = v2.Y;

            _vertexBuffer[2].Position.X = v1.X;
            _vertexBuffer[2].Position.Y = v1.Y;

            _vertexBuffer[3].Position.X = v2.X;
            _vertexBuffer[3].Position.Y = v1.Y;

            graphicsDevice.DrawUserIndexedPrimitives<VertexPositionTexture>
                (PrimitiveType.TriangleList, _vertexBuffer, 0, 4, _indexBuffer, 0, 2);
        }
    }
}