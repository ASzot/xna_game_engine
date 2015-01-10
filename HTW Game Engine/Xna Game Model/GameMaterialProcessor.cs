//-----------------------------------------------------------------------------
// Code adapted from...
//
// Jorge Adriano Luna
// http://jcoluna.wordpress.com
//-----------------------------------------------------------------------------

using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace Xna_Game_Model
{
    /// <summary>
    ///  We need this class to input our #defines into the HLSL shaders
    /// </summary>
    [ContentProcessor(DisplayName = "Xna Game Material Processor")]
    class GameMaterialProcessor : MaterialProcessor
    {
        public override MaterialContent Process(MaterialContent input, ContentProcessorContext context)
        {
            if (context.Parameters.ContainsKey("Defines"))
                context.Parameters.Remove("Defines");
            if (input.OpaqueData.ContainsKey("Defines"))
            {
                context.Parameters.Add("Defines", input.OpaqueData["Defines"]);
            }
            return base.Process(input, context);
        }
     
        protected override ExternalReference<CompiledEffectContent> BuildEffect(ExternalReference<EffectContent> effect, ContentProcessorContext context)
        {
            OpaqueDataDictionary processorParameters = new OpaqueDataDictionary();
            if (context.Parameters.ContainsKey("Defines"))
            {
                processorParameters.Add("Defines", context.Parameters["Defines"]);
            }
            return context.BuildAsset<EffectContent, CompiledEffectContent>(effect, "GameFXProcessor", processorParameters, "EffectImporter", effect.Name);
        }

    }
}
