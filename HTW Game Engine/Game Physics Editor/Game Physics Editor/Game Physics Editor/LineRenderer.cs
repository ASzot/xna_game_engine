#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_Physics_Editor
{
    class LineRenderer
    {
        private VertexPositionColor[] _points = new VertexPositionColor[2];
        private VertexDeclaration _declaration;
        private BasicEffect _effect;

        public LineRenderer()
        {
        }

        public void Draw(GraphicsDevice device, Vector3 start, Vector3 end, Camera camera, Vector3 vColor)
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
