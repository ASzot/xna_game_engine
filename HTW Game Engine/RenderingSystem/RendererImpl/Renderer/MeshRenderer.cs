//-----------------------------------------------------------------------------
// Code adapted from...
//
// Jorge Adriano Luna
// http://jcoluna.wordpress.com
//-----------------------------------------------------------------------------

using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem.RendererImpl
{
    /// <summary>
    /// Originally an ambitious class now all it does is render spheres.
    /// An overall stupid class.
    /// </summary>
    public class ShapeRenderer
    {
        /// <summary>
        /// For easy access to the mesh part as we know this model will only have one mesh part.
        /// This is because the mesh part is where vertex and index information is.
        /// </summary>
        private ModelMeshPart _meshPart;

        /// <summary>
        /// Sphere model
        /// </summary>
        private Model _model;

        public ShapeRenderer(Model model)
        {
            _model = model;
            _meshPart = _model.Meshes[0].MeshParts[0];
        }

        /// <summary>
        /// Setup the rendering of the sphere. Bind vertex and index information.
        /// </summary>
        /// <param name="graphicsDevice"></param>
        public void BindMesh(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.SetVertexBuffer(_meshPart.VertexBuffer, _meshPart.VertexOffset);
            graphicsDevice.Indices = _meshPart.IndexBuffer;
        }

        /// <summary>
        /// Render a sphere.
        /// </summary>
        /// <param name="graphicsDevice"></param>
        public void RenderMesh(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0,
                                            _meshPart.NumVertices,
                                            _meshPart.StartIndex,
                                            _meshPart.PrimitiveCount);
        }
    }
}