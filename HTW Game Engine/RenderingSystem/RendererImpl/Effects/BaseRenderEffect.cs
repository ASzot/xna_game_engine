//-----------------------------------------------------------------------------
// Code from...
//
// Jorge Adriano Luna
// http://jcoluna.wordpress.com
//-----------------------------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem.RendererImpl
{
    /// <summary>
    /// A wrapper for many of the shaders.
    /// Contains the data a large majority of the shaders will need.
    /// </summary>
    public class BaseRenderEffect
    {
        public static float TotalTime = 0;

        private EffectParameter _alphaReferenceParameter;
        private EffectParameter _ambientCubemapParameter;
        private EffectParameter _ambientParameter;
        private EffectParameter _bonesParameter;
        private EffectParameter _clippingParameter;
        private EffectParameter _clipPlaneParameter;
        private EffectParameter _colorBufferParameter;
        private EffectParameter _depthBufferParameter;
        private EffectParameter _diffuseMat;
        private EffectParameter _diffuseParameter;
        private Effect _effect;

        private EffectParameter _emissiveMapParameter;
        private EffectParameter _farClipParameter;
        private EffectParameter _lightBufferParameter;
        private EffectParameter _lightBufferPixelSizeParameter;
        private EffectParameter _lightSpecularBufferParameter;
        private EffectParameter _lightViewProjParameter;
        private EffectParameter _normalBufferParameter;
        private EffectParameter _normalMapParameter;
        private EffectParameter _projectionParameter;
        private EffectParameter _specularMapParameter;
        private EffectParameter _specularMat;
        private EffectParameter _texTransformParameter;
        private EffectParameter _textureMatrixParameter;
        private EffectParameter _totalTimeParameter;
        private EffectParameter _useDiffuseMat;
        private EffectParameter _useSpecularMat;
        private EffectParameter _viewParameter;
        private EffectParameter _worldParameter;
        private EffectParameter _worldViewParameter;
        private EffectParameter _wvpParameter;

        /// <summary>
        /// Construct the base render effect on the loaded effect resource.
        /// </summary>
        /// <param name="effect"></param>
        public BaseRenderEffect(Effect effect)
        {
            SetEffect(effect);
        }

        /// <summary>
        /// Alpha reference value, determines which values should be clipped.
        /// </summary>
        public EffectParameter AlphaReferenceParameter
        {
            get { return _alphaReferenceParameter; }
        }

        /// <summary>
        /// The cubemap which must be sampled for the ambient term.
        /// </summary>
        public EffectParameter AmbientCubemapParameter
        {
            get { return _ambientCubemapParameter; }
        }

        /// <summary>
        /// The ambient light parameter of the scene.
        /// </summary>
        public EffectParameter AmbientParameter
        {
            get { return _ambientParameter; }
        }

        /// <summary>
        /// Clipping threshold value if the effect uses it.
        /// </summary>
        public EffectParameter ClippingParameter
        {
            get { return _clippingParameter; }
        }

        /// <summary>
        /// Clipping plane value if the effect uses it.
        /// </summary>
        public EffectParameter ClipPlaneParameter
        {
            get { return _clipPlaneParameter; }
        }

        /// <summary>
        /// The diffuse map.
        /// </summary>
        public EffectParameter DiffuseMapParameter
        {
            get { return _diffuseParameter; }
        }

        /// <summary>
        /// The diffuse material.
        /// </summary>
        public EffectParameter DiffuseMatParameter
        {
            get { return _diffuseMat; }
        }

        /// <summary>
        /// The emissive map.
        /// </summary>
        public EffectParameter EmissiveMapParameter
        {
            get { return _emissiveMapParameter; }
        }

        /// <summary>
        /// The normal map.
        /// </summary>
        public EffectParameter NormalMapParameter
        {
            get { return _normalMapParameter; }
        }

        /// <summary>
        /// The specular map.
        /// </summary>
        public EffectParameter SpecularMapParameter
        {
            get { return _specularMapParameter; }
        }

        /// <summary>
        /// The specular material.
        /// </summary>
        public EffectParameter SpecularMatParameter
        {
            get { return _specularMat; }
        }

        /// <summary>
        /// The texure coordinates transform.
        /// </summary>
        public EffectParameter TexTransformParameter
        {
            get { return _texTransformParameter; }
        }

        /// <summary>
        /// Use diffuse material.
        /// </summary>
        public EffectParameter UseDiffuseMaterial
        {
            get { return _useDiffuseMat; }
        }

        /// <summary>
        /// Use specular material.
        /// </summary>
        public EffectParameter UseSpecularMaterial
        {
            get { return _useSpecularMat; }
        }

        /// <summary>
        /// Apply the fx variable assignments to when the effect is actually used.
        /// </summary>
        public void Apply()
        {
            if (_totalTimeParameter != null)
                _totalTimeParameter.SetValue(TotalTime);
            _effect.CurrentTechnique.Passes[0].Apply();
        }

        /// <summary>
        /// Get a parameter not already found in this class.
        /// </summary>
        /// <param name="parameter">The name of the parameter.</param>
        /// <returns>The found parameter.</returns>
        public EffectParameter GetParameter(string parameter)
        {
            return _effect.Parameters[parameter];
        }

        /// <summary>
        /// Get the number of tecniques the loaded effect uses.
        /// </summary>
        /// <returns></returns>
        public int GetTechCount()
        {
            if (_effect != null)
                return _effect.Techniques.Count;
            return 0;
        }

        /// <summary>
        /// Set the bone transform matrices in the fx file.
        /// </summary>
        /// <param name="bones"></param>
        public void SetBones(Matrix[] bones)
        {
            _bonesParameter.SetValue(bones);
        }

        /// <summary>
        /// Set the color render target from calculating the GBuffer in the fx file.
        /// </summary>
        /// <param name="colorRT"></param>
        public void SetColorRT(Texture2D colorRT)
        {
            if (_colorBufferParameter != null)
                _colorBufferParameter.SetValue(colorRT);
        }

        /// <summary>
        /// Set the current technique  to use in the fx file.
        /// </summary>
        /// <param name="techOrder">Zero indexed order of the technique.</param>
        public void SetCurrentTech(int techOrder)
        {
            _effect.CurrentTechnique = _effect.Techniques[techOrder];
        }

        /// <summary>
        /// Set the current technique to use in the fx file.
        /// </summary>
        /// <param name="str">Name of the technique./param>
        public void SetCurrentTech(string techName)
        {
            _effect.CurrentTechnique = _effect.Techniques[techName];
        }

        /// <summary>
        /// Set the depth render target from calculating the GBuffer in the fx file.
        /// </summary>
        /// <param name="depthRT"></param>
        public void SetDepthRT(Texture2D depthRT)
        {
            if (_depthBufferParameter != null)
                _depthBufferParameter.SetValue(depthRT);
        }

        /// <summary>
        /// Set the effect and load the parameters.
        /// </summary>
        /// <param name="effect"></param>
        public void SetEffect(Effect effect)
        {
            _effect = effect;
            LoadParameters();
        }

        /// <summary>
        /// Set the far clip in the fx file.
        /// </summary>
        /// <param name="farClip"></param>
        public void SetFarClip(float farClip)
        {
            if (_farClipParameter != null)
                _farClipParameter.SetValue(farClip);
        }

        /// <summary>
        /// Set the pixel size in the fx file.
        /// </summary>
        /// <param name="pixelSize"></param>
        public void SetLightBufferPixelSize(Vector2 pixelSize)
        {
            if (_lightBufferPixelSizeParameter != null)
                _lightBufferPixelSizeParameter.SetValue(pixelSize);
        }

        /// <summary>
        /// Set the light render targets from the GBuffer creation in the fx file.
        /// </summary>
        /// <param name="lightRT"></param>
        /// <param name="lightSpecRT"></param>
        public void SetLightRT(Texture lightRT, Texture lightSpecRT)
        {
            if (_lightBufferParameter != null)
                _lightBufferParameter.SetValue(lightRT);
            if (_lightSpecularBufferParameter != null)
                _lightSpecularBufferParameter.SetValue(lightSpecRT);
        }

        /// <summary>
        /// Set the view * proj matrix of the light in the fx file.
        /// </summary>
        /// <param name="matrix"></param>
        public void SetLightViewProj(Matrix matrix)
        {
            _lightViewProjParameter.SetValue(matrix);
        }

        /// <summary>
        /// Set the matrix data for the render.
        /// </summary>
        /// <param name="world">Rendering object world matrix.</param>
        /// <param name="view">Camera view matrix.</param>
        /// <param name="projection">Camera projectin matrix.</param>
        public void SetMatrixData(Matrix world, Matrix view, Matrix projection)
        {
            Matrix worldView, worldViewProj;
            Matrix.Multiply(ref world, ref view, out worldView);
            Matrix.Multiply(ref worldView, ref projection, out worldViewProj);

            if (_worldParameter != null)
                _worldParameter.SetValue(world);
            if (_viewParameter != null)
                _viewParameter.SetValue(view);
            if (_projectionParameter != null)
                _projectionParameter.SetValue(projection);
            if (_worldViewParameter != null)
                _worldViewParameter.SetValue(worldView);
            if (_wvpParameter != null)
                _wvpParameter.SetValue(worldViewProj);
        }

        /// <summary>
        /// Set the normal render target from the GBufffer creation in the fx file.
        /// </summary>
        /// <param name="normalRT"></param>
        public void SetNormalRT(Texture normalRT)
        {
            if (_normalBufferParameter != null)
                _normalBufferParameter.SetValue(normalRT);
        }

        /// <summary>
        /// Set the texture cooridinate transform matrix in the fx file.
        /// </summary>
        /// <param name="textureTransform"></param>
        public void SetTextureMatrix(Matrix textureTransform)
        {
            _textureMatrixParameter.SetValue(textureTransform);
        }

        /// <summary>
        /// Set the world matrix of the object being rendered in the fx file.
        /// </summary>
        /// <param name="globalTransform"></param>
        public void SetWorld(Matrix globalTransform)
        {
            _worldParameter.SetValue(globalTransform);
        }

        /// <summary>
        /// Set the object world * camera view * camera projection matrix in the fx file.
        /// Needed to transform vertices to homogoenous coordinates in world view proj space.
        /// </summary>
        /// <param name="worldViewProj"></param>
        public void SetWVP(Matrix worldViewProj)
        {
            _wvpParameter.SetValue(worldViewProj);
        }

        /// <summary>
        /// Load the parameters from the effect resource.
        /// </summary>
        private void LoadParameters()
        {
            _worldParameter = _effect.Parameters["World"];
            _viewParameter = _effect.Parameters["View"];
            _projectionParameter = _effect.Parameters["Projection"];
            _worldViewParameter = _effect.Parameters["WorldView"];
            _wvpParameter = _effect.Parameters["WorldViewProjection"];

            _textureMatrixParameter = _effect.Parameters["TextureMatrix"];
            _lightViewProjParameter = _effect.Parameters["LightViewProj"];
            _farClipParameter = _effect.Parameters["FarClip"];
            _lightBufferPixelSizeParameter = _effect.Parameters["LightBufferPixelSize"];

            _bonesParameter = _effect.Parameters["Bones"];
            _normalBufferParameter = _effect.Parameters["NormalBuffer"];
            _lightBufferParameter = _effect.Parameters["LightBuffer"];
            _lightSpecularBufferParameter = _effect.Parameters["LightSpecularBuffer"];

            _depthBufferParameter = _effect.Parameters["DepthBuffer"];
            _colorBufferParameter = _effect.Parameters["ColorBuffer"];

            _ambientParameter = _effect.Parameters["AmbientColor"];
            _ambientCubemapParameter = _effect.Parameters["AmbientCubeMap"];

            _totalTimeParameter = _effect.Parameters["TotalTime"];

            _texTransformParameter = _effect.Parameters["TexTransform"];
            if (_texTransformParameter != null)
            {
                _texTransformParameter.SetValue(Matrix.Identity);
            }

            _diffuseParameter = _effect.Parameters["DiffuseMap"];
            if (_diffuseParameter == null)
                _diffuseParameter = _effect.Parameters["Diffuse"];

            _normalMapParameter = _effect.Parameters["NormalMap"];
            _specularMapParameter = _effect.Parameters["SpecularMap"];
            _emissiveMapParameter = _effect.Parameters["EmissiveMap"];

            _useSpecularMat = _effect.Parameters["UseSpecularMat"];
            _useDiffuseMat = _effect.Parameters["UseDiffuseMat"];

            _specularMat = _effect.Parameters["SpecularMat"];
            _diffuseMat = _effect.Parameters["DiffuseMat"];

            _alphaReferenceParameter = _effect.Parameters["AlphaReference"];

            _clippingParameter = _effect.Parameters["Clipping"];
            _clipPlaneParameter = _effect.Parameters["ClipPlane"];

            if (_textureMatrixParameter != null)
                _textureMatrixParameter.SetValue(Matrix.Identity);
        }
    }
}