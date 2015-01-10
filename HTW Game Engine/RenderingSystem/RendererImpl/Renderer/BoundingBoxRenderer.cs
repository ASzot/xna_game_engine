//-----------------------------------------------------------------------------
// Code based on...
//
// Jorge Adriano Luna
// http://jcoluna.wordpress.com
//-----------------------------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem.RendererImpl
{
    /// <summary>
    /// Used to render bounding boxes.
    /// Not optimized so not actually practical to use.
    /// Use for debugging purposes only.
    /// </summary>
    public class BoundingBoxRenderer
    {
        private static short[] _indicies = new short[24];
        private BasicEffect _effect;
        private VertexDeclaration _vertexDecl;
        private VertexPositionColor[] _vertices = new VertexPositionColor[8];
        /// <summary>
        /// Setup the vertices and indices of the bounding box.
        /// </summary>
        public BoundingBoxRenderer()
        {
            _indicies[0] = 0;
            _indicies[1] = 1;
            _indicies[2] = 1;
            _indicies[3] = 2;
            _indicies[4] = 2;
            _indicies[5] = 3;
            _indicies[6] = 3;
            _indicies[7] = 0;

            _indicies[8] = 4;
            _indicies[9] = 5;
            _indicies[10] = 5;
            _indicies[11] = 6;
            _indicies[12] = 6;
            _indicies[13] = 7;
            _indicies[14] = 7;
            _indicies[15] = 4;

            _indicies[16] = 0;
            _indicies[17] = 4;
            _indicies[18] = 1;
            _indicies[19] = 5;
            _indicies[20] = 2;
            _indicies[21] = 6;
            _indicies[22] = 3;
            _indicies[23] = 7;

            InitBoundingBox();
        }

        /// <summary>
        /// Draw a bounding box.
        /// </summary>
        /// <param name="device">The graphics device.</param>
        /// <param name="boundingBox">The bounding box to draw.</param>
        /// <param name="camera">The scene's camera.</param>
        /// <param name="color">The color of the bounding box to be drawn.</param>
        public void Draw(GraphicsDevice device, BoundingBox boundingBox, ICamera camera, Color color)
        {
            if (_vertexDecl == null)
            {
                _vertexDecl = VertexPositionColor.VertexDeclaration;
            }

            if (_effect == null)
            {
                _effect = new BasicEffect(device);
            }
            _effect.DiffuseColor = color.ToVector3();

            _effect.View = camera.View;
            _effect.Projection = camera.Proj;
            Vector3 size = boundingBox.Max - boundingBox.Min;
            _effect.World = Matrix.CreateScale(size) * Matrix.CreateTranslation(boundingBox.Max - size * 0.5f);

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawUserIndexedPrimitives<VertexPositionColor>(
                                    PrimitiveType.LineList, _vertices,
                                    0,
                                    8,
                                    _indicies,
                                    0,
                                    12);
            }
        }

        /// <summary>
        /// Set up the vertices of the bounding box with some default parameters.
        /// The values are changed on the bounding box actually being rendered.
        /// </summary>
        private void InitBoundingBox()
        {
            BoundingBox boundingBox = new BoundingBox(-Vector3.One, Vector3.One);

            _vertices[0].Position = new Vector3(boundingBox.Min.X, boundingBox.Min.Y, boundingBox.Min.Z);            
            _vertices[1].Position = new Vector3(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
            _vertices[2].Position = new Vector3(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Max.Z);
            _vertices[3].Position = new Vector3(boundingBox.Min.X, boundingBox.Min.Y, boundingBox.Max.Z);

            _vertices[4].Position = new Vector3(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
            _vertices[5].Position = new Vector3(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
            _vertices[6].Position = new Vector3(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Max.Z);
            _vertices[7].Position = new Vector3(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Max.Z);

            for (int i = 0; i < 8; i++)
                _vertices[i].Color = new Color(255, 255, 255, 255);
        }
    }
}
