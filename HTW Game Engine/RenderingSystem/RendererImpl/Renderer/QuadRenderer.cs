#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem.RendererImpl
{
    /// <summary>
    /// A 2D billboard which can be rendered.
    /// </summary>
    public class DrawableBillboard
    {
        /// <summary>
        /// Texture to draw.
        /// </summary>
        private Texture2D _texture = null;

        /// <summary>
        /// Scale of the billboard.
        /// </summary>
        private float f_scale = 1f;

        /// <summary>
        /// The position of the billboard in global space.
        /// </summary>
        private Vector3 v_pos = Vector3.Zero;

        public DrawableBillboard(Texture2D texture)
        {
            _texture = texture;
        }

        public DrawableBillboard(Texture2D texture, Vector3 position)
        {
            _texture = texture;
            v_pos = position;
        }

        /// <summary>
        /// World position of the billboard.
        /// </summary>
        public Vector3 Position
        {
            get { return v_pos; }
            set { v_pos = value; }
        }

        /// <summary>
        /// The uniform scale of the billboard.
        /// </summary>
        public float Scale
        {
            get { return f_scale; }
            set { f_scale = value; }
        }

        /// <summary>
        /// The texture to billboard.
        /// </summary>
        public Texture2D Texture
        {
            get { return _texture; }
        }
    }

    /// <summary>
    /// Renders a 2D quads into 3D space.
    /// Used to render the billboards.
    /// </summary>
    public class QuadRenderer
    {
        private BasicEffect _effect;
        private VertexBuffer _vertexBuffer;

        public QuadRenderer()
        {
        }

        /// <summary>
        /// Load all resource related content.
        /// Load vertices and effect.
        /// </summary>
        /// <param name="device"></param>
        public void LoadContent(GraphicsDevice device)
        {
            VertexPositionTexture[] billboardVertices = new VertexPositionTexture[6];
            billboardVertices[0] = new VertexPositionTexture(new Vector3(0, 0, 0), new Vector2(0, 1));
            billboardVertices[2] = new VertexPositionTexture(new Vector3(1, 0, -1), new Vector2(1, 0));
            billboardVertices[1] = new VertexPositionTexture(new Vector3(0, 0, -1), new Vector2(0, 0));

            billboardVertices[3] = new VertexPositionTexture(new Vector3(0, 0, 0), new Vector2(0, 1));
            billboardVertices[5] = new VertexPositionTexture(new Vector3(1, 0, 0), new Vector2(1, 1));
            billboardVertices[4] = new VertexPositionTexture(new Vector3(1, 0, -1), new Vector2(1, 0));

            _vertexBuffer = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, billboardVertices.Length,
                BufferUsage.WriteOnly);
            _vertexBuffer.SetData(billboardVertices);

            _effect = new BasicEffect(device);
        }

        /// <summary>
        /// Render a list of quads.
        /// </summary>
        /// <param name="device">Graphics device.</param>
        /// <param name="cam">The scene's camera.</param>
        /// <param name="bbs">The list of billboards to draw.</param>
        public void RenderQuads(GraphicsDevice device, ICamera cam, IEnumerable<DrawableBillboard> bbs)
        {
            foreach (DrawableBillboard bb in bbs)
            {
                RenderQuad(device, cam, bb);
            }
        }

        /// <summary>
        /// Render a singular quad.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="cam"></param>
        /// <param name="bb"></param>
        private void RenderQuad(GraphicsDevice device, ICamera cam, DrawableBillboard bb)
        {
            _effect.CurrentTechnique = _effect.Techniques[0];

            Vector3 lookAt = Vector3.TransformNormal(
                    cam.ForwardAxis,
                    Matrix.CreateFromAxisAngle(cam.SideAxis, cam.Pitch) * Matrix.CreateFromAxisAngle(cam.UpAxis, cam.Yaw));

            float oppositeYaw = cam.Yaw;
            float oppositePitch = cam.Pitch + MathHelper.PiOver2;

            Matrix rot = Matrix.CreateFromAxisAngle(cam.SideAxis, oppositePitch) * Matrix.CreateFromAxisAngle(cam.UpAxis,
                oppositeYaw);

            Matrix world = rot * Matrix.CreateTranslation(bb.Position);
            _effect.World = world;
            _effect.Projection = cam.Proj;
            _effect.View = cam.View;
            _effect.Texture = bb.Texture;
            _effect.TextureEnabled = true;
            _effect.AmbientLightColor = new Vector3(1f);
            _effect.LightingEnabled = false;
            _effect.CurrentTechnique.Passes[0].Apply();

            device.DepthStencilState = DepthStencilState.None;
            device.BlendState = BlendState.AlphaBlend;
            SetVertexBuffer(device);
            device.DrawPrimitives(PrimitiveType.TriangleList, 0, _vertexBuffer.VertexCount / 3);
            device.BlendState = BlendState.Opaque;
        }

        /// <summary>
        /// Bind the vertex buffer.
        /// </summary>
        /// <param name="device"></param>
        private void SetVertexBuffer(GraphicsDevice device)
        {
            device.SetVertexBuffer(_vertexBuffer);
        }
    }
}