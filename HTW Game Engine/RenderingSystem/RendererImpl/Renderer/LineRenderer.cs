#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem.RendererImpl
{
    /// <summary>
    /// Can render lines.
    /// Not optimized, lines are not batch drawn.
    /// Should probaly only be used for debugging purposes.
    /// </summary>
    public class LineRenderer
    {
        private VertexDeclaration _declaration;
        private BasicEffect _effect;
        private VertexPositionColor[] _points = new VertexPositionColor[2];

        public LineRenderer()
        {
        }

        /// <summary>
        /// Draw a line.
        /// </summary>
        /// <param name="device">The graphics device.</param>
        /// <param name="start">The world space start point.</param>
        /// <param name="end">The world space end point.</param>
        /// <param name="camera">The scene's camera.</param>
        /// <param name="vColor">The color of the line to draw.</param>
        public void Draw(GraphicsDevice device, Vector3 start, Vector3 end, ICamera camera, Vector3 vColor)
        {
            if (_declaration == null)
            {
                _declaration = VertexPositionColor.VertexDeclaration;
            }
            if (_effect == null)
            {
                _effect = new BasicEffect(device);
            }

            _points[0].Position = start;
            _points[1].Position = end;

            _effect.DiffuseColor = vColor;
            _effect.View = camera.View;
            _effect.Projection = camera.Proj;
            _effect.World = Matrix.Identity;

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, _points, 0, 1, _declaration);
            }
        }
    }
}