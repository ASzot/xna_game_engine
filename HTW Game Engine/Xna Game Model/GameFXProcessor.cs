#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace Xna_Game_Model
{
    /// <summary>
    /// This class takes the array of #defines from the material, and inserts it into the fx file
    /// </summary>
    [ContentProcessor(DisplayName = "Xna Game FX Processor")]
    public class GameFXProcessor : EffectProcessor
    {
        public override CompiledEffectContent Process(Microsoft.Xna.Framework.Content.Pipeline.Graphics.EffectContent input, ContentProcessorContext context)
        {
            this.DebugMode = EffectProcessorDebugMode.Optimize;
            if (context.Parameters.ContainsKey("Defines"))
                this.Defines = context.Parameters["Defines"].ToString();
            return base.Process(input, context);
        }
    }
}
