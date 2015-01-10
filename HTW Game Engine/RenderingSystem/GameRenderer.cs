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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

// When Andrew set off on this project he had great visions of a forward and deferred renderer which would be implemented via
// this one simple interface.

namespace RenderingSystem
{
    /// <summary>
    /// Some basic info about the rendering of the scene.
    /// Not really used right now. But it was intended as a debugging analyzing feature.
    /// </summary>
    public struct FrameRenderData
    {
        /// <summary>
        /// The number of meshes drawn.
        /// </summary>
        public int NumObjectsDrawn;
        /// <summary>
        /// The number of lights drawn.
        /// </summary>
        public int NumLightsDrawn;
    }

    /// <summary>
    /// A general renderer enabling rendering flexiability.
    /// </summary>
    public abstract class GameRenderer
    {
        public GameRenderer(GraphicsDevice device)
        {
            _device = device;
        }

        protected GraphicsDevice _device;
        public GraphicsDevice Device { get { return _device; } }
        public abstract void OnResize(Viewport vp);
        public abstract void Draw(GameTime gameTime, ICamera cam, DebugTimings debugTimings);
        public abstract void Update(GameTime gameTime);
        public abstract FrameRenderData GetFrameRenderData();
        public abstract void LoadContent(ContentManager contentMgr);
        public abstract void UnloadContent();
        public abstract void CreateRendererSettingsDlg(Action onDlgQuit);
    }
}
