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

namespace RenderingSystem
{
    /// <summary>
    /// Aids in the unloading of resources.
    /// </summary>
    public static class ResourceHelper
    {
        /// <summary>
        /// Unloads a render target if it is null and not disposed.
        /// </summary>
        /// <param name="renderTarget"></param>
        public static void Unload(this RenderTarget2D renderTarget)
        {
            if (renderTarget != null && !renderTarget.IsDisposed)
                renderTarget.Dispose();
        }

        /// <summary>
        /// Unloads a texture2D if it is null and not disposed.
        /// </summary>
        /// <param name="tex2D"></param>
        public static void Unload(this Texture2D tex2D)
        {
            if (tex2D != null && !tex2D.IsDisposed)
                tex2D.Dispose();
        }

        /// <summary>
        /// Unloads an effect if it is null and not disposed.
        /// </summary>
        /// <param name="effect"></param>
        public static void Unload(this Effect effect)
        {
            if (effect != null && !effect.IsDisposed)
                effect.Dispose();
        }
    }
}
