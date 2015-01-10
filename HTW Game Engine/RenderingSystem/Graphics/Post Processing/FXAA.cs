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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

//////////////////////////////////////////////////////////////////////////////////
// Based on the shader code at http://mtnphil.wordpress.com/2012/10/15/fxaa-in-xna/

namespace RenderingSystem.Graphics
{
	/// <summary>
	/// A Fast Approximate Anti-Alaising algorithm based on the code supplied by NVIDIA.
	/// </summary>
    public class FxaaPPE : PostProcessingEffect
    {
		/// <summary>
		/// The fx resource for the fast approximate anti-alaising effect.
		/// </summary>
        private Effect _effect;

		// These variable descriptions are based on the information supplied by the demo by NVIDIA.

        // This effects sub-pixel AA quality and inversely sharpness.
        //   Where N ranges between,
        //     N = 0.50 (default)
        //     N = 0.33 (sharper)
        private float _count = 0.40f;

        // Choose the amount of sub-pixel aliasing removal.
        // This can effect sharpness.
        //   1.00 - upper limit (softer)
        //   0.75 - default amount of filtering
        //   0.50 - lower limit (sharper, less sub-pixel aliasing removal)
        //   0.25 - almost off
        //   0.00 - completely off
        private float _subPixelAliasingRemoval = 0.75f;

        // The minimum amount of local contrast required to apply algorithm.
        //   0.333 - too little (faster)
        //   0.250 - low quality
        //   0.166 - default
        //   0.125 - high quality 
        //   0.063 - overkill (slower)
        private float _edgeThreshold = 0.166f;
        
        // Trims the algorithm from processing darks.
        //   0.0833 - upper limit (default, the start of visible unfiltered edges)
        //   0.0625 - high quality (faster)
        //   0.0312 - visible limit (slower)
        // Special notes when using FXAA_GREEN_AS_LUMA,
        //   Likely want to set this to zero.
        //   As colors that are mostly not-green
        //   will appear very dark in the green channel!
        //   Tune by looking at mostly non-green content,
        //   then start at zero and increase until aliasing is a problem.
        private float _edgeThesholdMin = 0f; 

        // This does not effect PS3, as this needs to be compiled in.
        //   Use FXAA_CONSOLE__PS3_EDGE_SHARPNESS for PS3.
        //   Due to the PS3 being ALU bound,
        //   there are only three safe values here: 2 and 4 and 8.
        //   These options use the shaders ability to a free *|/ by 2|4|8.
        // For all other platforms can be a non-power of two.
        //   8.0 is sharper (default!!!)
        //   4.0 is softer
        //   2.0 is really soft (good only for vector graphics inputs)
        private float _consoleEdgeSharpness = 8.0f;

        // This does not effect PS3, as this needs to be compiled in.
        //   Use FXAA_CONSOLE__PS3_EDGE_THRESHOLD for PS3.
        //   Due to the PS3 being ALU bound,
        //   there are only two safe values here: 1/4 and 1/8.
        //   These options use the shaders ability to a free *|/ by 2|4|8.
        // The console setting has a different mapping than the quality setting.
        // Other platforms can use other values.
        //   0.125 leaves less aliasing, but is softer (default!!!)
        //   0.25 leaves more aliasing, and is sharper
        private float _consoleEdgeThreshold = 0.125f;

        // Trims the algorithm from processing darks.
        // The console setting has a different mapping than the quality setting.
        // This only applies when FXAA_EARLY_EXIT is 1.
        // This does not apply to PS3, 
        // PS3 was simplified to avoid more shader instructions.
        //   0.06 - faster but more aliasing in darks
        //   0.05 - default
        //   0.04 - slower and less aliasing in darks
        // Special notes when using FXAA_GREEN_AS_LUMA,
        //   Likely want to set this to zero.
        //   As colors that are mostly not-green
        //   will appear very dark in the green channel!
        //   Tune by looking at mostly non-green content,
        //   then start at zero and increase until aliasing is a problem.
        private float _consoleEdgeThresholdMin = 0f;

		
        public float Count
        {
            get { return _count; }
            set { _count = value; }
        }

        public float SubPixelAliasingRemoval
        {
            get { return _subPixelAliasingRemoval; }
            set { _subPixelAliasingRemoval = value; }
        }

        public float EdgeThreshold
        {
            get { return _edgeThreshold; }
            set { _edgeThreshold = value; }
        }

        public float EdgeThresholdMin
        {
            get { return _edgeThesholdMin; }
            set { _edgeThesholdMin = value; }
        }

        public float ConsoleEdgeSharpness
        {
            get { return _consoleEdgeSharpness; }
            set { _consoleEdgeSharpness = value; }
        }

        public float ConsoleEdgeThreshold
        {
            get { return _consoleEdgeThreshold; }
            set { _consoleEdgeThreshold = value; }
        }

        public float ConsoleEdgeThresholdMin
        {
            get { return _consoleEdgeThresholdMin; }
            set { _consoleEdgeThresholdMin = value; }
        }

        public FxaaPPE()
            : base("fxaa")
        {

        }

        public override void CreateSettingsDlg(object obj, EventArgs e)
        {
            base.CreateSettingsDlg(obj, e);
        }

		/// <summary>
		/// Draw the blur on the final scene render target.
		/// </summary>
		/// <param name="device">The graphics device.</param>
		/// <param name="renderTarget">The scene render target.</param>
		/// <param name="renderer">The scene renderer.</param>
		/// <returns></returns>
        public override RenderTarget2D DrawEffect(GraphicsDevice device, RenderTarget2D renderTarget, RendererImpl.Renderer renderer)
        {
            device.SetRenderTarget(renderTarget);

            Viewport viewport = device.Viewport;
            Matrix projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, 0, 1);
            Matrix halfPixelOffset = Matrix.CreateTranslation(-0.5f, -0.5f, 0);
            _effect.Parameters["World"].SetValue(Matrix.Identity);
            _effect.Parameters["View"].SetValue(Matrix.Identity);
            _effect.Parameters["Projection"].SetValue(halfPixelOffset * projection);
            _effect.Parameters["InverseViewportSize"].SetValue(new Vector2(1f / viewport.Width, 1f / viewport.Height));
            _effect.Parameters["ConsoleSharpness"].SetValue(new Vector4(
                -_count / viewport.Width,
                -_count / viewport.Height,
                _count / viewport.Width,
                _count / viewport.Height
                ));
            _effect.Parameters["ConsoleOpt1"].SetValue(new Vector4(
                -2.0f / viewport.Width,
                -2.0f / viewport.Height,
                2.0f / viewport.Width,
                2.0f / viewport.Height
                ));
            _effect.Parameters["ConsoleOpt2"].SetValue(new Vector4(
                8.0f / viewport.Width,
                8.0f / viewport.Height,
                -4.0f / viewport.Width,
                -4.0f / viewport.Height
                ));
            _effect.Parameters["SubPixelAliasingRemoval"].SetValue(_subPixelAliasingRemoval);
            _effect.Parameters["EdgeThreshold"].SetValue(_edgeThreshold);
            _effect.Parameters["EdgeThresholdMin"].SetValue(_edgeThesholdMin);
            _effect.Parameters["ConsoleEdgeSharpness"].SetValue(_consoleEdgeSharpness);
            _effect.Parameters["ConsoleEdgeThreshold"].SetValue(_consoleEdgeThreshold);
            _effect.Parameters["ConsoleEdgeThresholdMin"].SetValue(_consoleEdgeThresholdMin);

            _effect.CurrentTechnique = _effect.Techniques["FXAA"];

            device.SetRenderTarget(null);
            p_spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, SamplerState.LinearClamp, null, null, _effect);
            p_spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
            p_spriteBatch.End();

            return null;
        }

		/// <summary>
		/// Load the resource related data.
		/// </summary>
		/// <param name="device"></param>
		/// <param name="content"></param>
        public override void LoadContent(Microsoft.Xna.Framework.Graphics.GraphicsDevice device, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            _effect = content.Load<Effect>("shaders/FXAA");
        }

		/// <summary>
		/// Get the save data.
		/// </summary>
		/// <returns>The save data.</returns>
        public override object GetSettingsSaveData()
        {
            return base.GetSettingsSaveData();
        }

		/// <summary>
		/// Set the save data.
		/// </summary>
		/// <param name="saveData"></param>
        public override void SetSettingsSaveData(object saveData)
        {
            base.SetSettingsSaveData(saveData);
        }
    }
}
