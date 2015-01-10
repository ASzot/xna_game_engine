#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem.RendererImpl
{
    /// <summary>
    /// A simple wrapper around XNA's Effect.
    /// Can be applied to any effect resource.
    /// If BaseRenderEffect applied use that instead.
    /// </summary>
    internal class GeneralRenderEffect
    {
        /// <summary>
        /// The loaded effect resource.
        /// </summary>
        protected Effect _effect;

        /// <summary>
        /// Load the effect resource from a file.
        /// </summary>
        /// <param name="effectName">The effect name relative to the default shaders folder.</param>
        public GeneralRenderEffect(string effectName)
        {
            _effect = ResourceMgr.LoadEffect(effectName);
        }

        /// <summary>
        /// Apply the fx variable changes made.
        /// </summary>
        public void Apply()
        {
            _effect.CurrentTechnique.Passes[0].Apply();
        }

        /// <summary>
        /// Get an effect technique
        /// </summary>
        /// <param name="techniqueName"></param>
        /// <returns></returns>
        public EffectTechnique GetEffectTechnique(string techniqueName)
        {
            return _effect.Techniques[techniqueName];
        }

        /// <summary>
        /// Get an effect technique
        /// </summary>
        /// <param name="techniqueIndex"></param>
        /// <returns></returns>
        public EffectTechnique GetEffectTechnique(int techniqueIndex)
        {
            return _effect.Techniques[techniqueIndex];
        }

        /// <summary>
        /// Set the current technique to render with.
        /// </summary>
        /// <param name="techName"></param>
        public void SetCurrentTechnique(string techName)
        {
            _effect.CurrentTechnique = _effect.Techniques[techName];
        }

        /// <summary>
        /// Get an fx effect parameter for setting.
        /// Don't forget to call Apply().
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        protected EffectParameter GetEffectParameter(string parameterName)
        {
            return _effect.Parameters[parameterName];
        }
    }
}