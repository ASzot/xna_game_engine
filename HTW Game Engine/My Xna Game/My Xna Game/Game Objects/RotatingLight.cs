#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using Microsoft.Xna.Framework;
using RenderingSystem;

namespace My_Xna_Game.Game_Objects
{
    internal class RotatingLightPL : PointLight
    {
        private float f_radius;
        private float f_speed;
        private float f_startOffset;
        private Vector3 v_offset;

        /// <summary>
        /// Initializes a new instance of the <see cref="RotatingLightPL"/> class.
        /// </summary>
        /// <param name="startOffset">The start offset.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="speed">The speed.</param>
        /// <param name="color">The color.</param>
        /// <param name="range">The range.</param>
        /// <param name="intensity">The intensity.</param>
        /// <param name="specInten">The spec inten.</param>
        public RotatingLightPL(float startOffset, float radius, Vector3 offset, float speed, Color color,
            float range, float intensity, float specInten)
            : base(Vector3.Zero, range, intensity, specInten, color)
        {
            f_speed = speed;
            f_radius = radius;
            v_offset = offset;
            f_startOffset = startOffset;
        }

        /// <summary>
        /// Update the data of the light.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float time = (float)gameTime.TotalGameTime.TotalSeconds * f_speed;

            float x = (float)Math.Sin(time + f_startOffset) * f_radius;
            float z = (float)Math.Cos(time + f_startOffset) * f_radius;

            Position = new Vector3(x, 0f, z);
            Position += v_offset;
        }
    }
}