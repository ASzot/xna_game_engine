#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem.Graphics
{
	/// <summary>
	/// A helper for simplifying render state calls.
	/// </summary>
    static class RenderSettings
    {
        private static GraphicsDevice s_device = null;

        public static void Initialize(GraphicsDevice device)
        {
            s_device = device;
        }

        public static void SetDS_Def()
        {
            s_device.DepthStencilState = DepthStencilState.Default;
        }

        public static void SetDS_None()
        {
            s_device.DepthStencilState = DepthStencilState.None;   
        }

        public static void SetRSS_CCW()
        {
            s_device.RasterizerState = RasterizerState.CullCounterClockwise;
        }

        public static void SetRSS_CW()
        {
            s_device.RasterizerState = RasterizerState.CullClockwise;
        }

        public static void SetRSS_N()
        {
            s_device.RasterizerState = RasterizerState.CullNone;
        }

        public static void SetBlendState_Opaque()
        {
            s_device.BlendState = BlendState.Opaque;
        }
    }
}
