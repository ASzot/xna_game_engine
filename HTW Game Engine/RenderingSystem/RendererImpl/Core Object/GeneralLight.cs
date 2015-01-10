#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using Microsoft.Xna.Framework;

namespace RenderingSystem.RendererImpl
{
    /// <summary>
    /// A all encapsulating light class for any type of light.
    /// </summary>
    public class GeneralLight
    {
        /// <summary>
        /// The bounding sphere for point lights.
        /// </summary>
        private BoundingSphere _boundingSphere;

        /// <summary>
        /// Whether the directional or spot light will cast shadows.
        /// </summary>
        private bool b_castShadows;

        /// <summary>
        /// The diffuse light color.
        /// </summary>
        private Color _diffuseColor = Color.White;
        
        /// <summary>
        /// Whether the light is rendered.
        /// </summary>
        private bool b_enabled = true;

        /// <summary>
        /// Whether the spot or directional light has a flare.
        /// </summary>
        private bool b_flareEnabled = false;

        /// <summary>
        /// The frustum for the spot light.
        /// </summary>
        private BoundingFrustum _frustum;

        /// <summary>
        /// The glow size of the flare if enabled.
        /// </summary>
        private float f_glowSize;

        /// <summary>
        /// The intensity of the diffuse light.
        /// </summary>
        private float f_diffuseIntensity = 1;

        /// <summary>
        /// The inverse transform of this light's world transform.
        /// </summary>
        private Matrix _invTransform = Matrix.Identity;

        /// <summary>
        /// The type of light.
        /// </summary>
        private Type _lightType = Type.Point;

        /// <summary>
        /// The render projection matrix of the light.
        /// </summary>
        private Matrix m_projection = Matrix.Identity;

        /// <summary>
        /// The query (sampling) size of the flare effect if enabled.
        /// </summary>
        private float f_querySize;

        /// <summary>
        /// The radius of the point light, or the distance of the spot light.
        /// </summary>
        private float f_radius = 1;

        /// <summary>
        /// The depth bias for calculating the shadow maps for spot and directional lights.
        /// </summary>
        private float f_shadowDepthBias = 0.0005f;

        /// <summary>
        /// The max distance of the cascade shadow maps for directional lights.
        /// </summary>
        private float f_shadowDistance = 50;

        /// <summary>
        /// Specular intensity of the light.
        /// </summary>
        private float f_specularIntensity = 1;

        /// <summary>
        /// The angle of the spot light cone.
        /// </summary>
        private float f_spotConeAngle = 45;

        /// <summary>
        /// Like the drop of off the spot light.
        /// </summary>
        private float f_spotExponent = 10;

        /// <summary>
        /// The world matrix of the light.
        /// </summary>
        private Matrix m_transform = Matrix.Identity;

        /// <summary>
        /// The view multiplied by the projection matrix of the light. Used for rendering.
        /// </summary>
        private Matrix _viewProjection = Matrix.Identity;

        /// <summary>
        /// Construct with default parameters.
        /// </summary>
        public GeneralLight()
        {
            _boundingSphere = new BoundingSphere(m_transform.Translation, f_radius);
            _frustum = new BoundingFrustum(Matrix.Identity);
            UpdateSpotLight();
        }

        /// <summary>
        /// Representing the type of light.
        /// </summary>
        public enum Type
        {
            Point,
            Directional,
            Spot
        } ;

        /// <summary>
        /// The bounding sphere which fits the current point light.
        /// </summary>
        public BoundingSphere BoundingSphere
        {
            get { return _boundingSphere; }
        }

        /// <summary>
        /// Whether the light casts shadows.
        /// </summary>
        public bool CastShadows
        {
            get { return b_castShadows; }
            set { b_castShadows = value; }
        }

        /// <summary>
        /// The color of the light.
        /// </summary>
        public Color Color
        {
            get { return _diffuseColor; }
            set { _diffuseColor = value; }
        }

        /// <summary>
        /// Whether the light is enabled.
        /// </summary>
        public bool Enabled
        {
            get { return b_enabled; }
            set { b_enabled = value; }
        }

        /// <summary>
        /// Whether the directional or spot light uses a flare effect.
        /// </summary>
        public bool FlareEnabled
        {
            get { return b_flareEnabled; }
            set { b_flareEnabled = value; }
        }

        /// <summary>
        /// The frustum of the directional light.
        /// </summary>
        public BoundingFrustum Frustum
        {
            get { return _frustum; }
        }

        /// <summary>
        /// The glow size of the flare effect if enabled.
        /// </summary>
        public float GlowSize
        {
            get { return f_glowSize; }
            set { f_glowSize = value; }
        }

        /// <summary>
        /// Controls the intensity of the diffuse light.
        /// </summary>
        public float DiffuseIntensity
        {
            get { return f_diffuseIntensity; }
            set { f_diffuseIntensity = value; }
        }

        /// <summary>
        /// The type of the light: spot, directional, or point.
        /// </summary>
        public Type LightType
        {
            get { return _lightType; }
            set
            {
                _lightType = value;
                if (_lightType == Type.Spot)
                    UpdateSpotLight();
            }
        }

        /// <summary>
        /// The projection matrix for this light. Used in rendering.
        /// </summary>
        public Matrix Projection
        {
            get { return m_projection; }
        }

        /// <summary>
        /// The query (sampling) size of the flare.
        /// </summary>
        public float QuerySize
        {
            get { return f_querySize; }
            set { f_querySize = value; }
        }

        /// <summary>
        /// The radius of the point light or the distance of the spot light.
        /// </summary>
        public float Radius
        {
            get { return f_radius; }
            set
            {
                f_radius = value;
                _boundingSphere.Radius = f_radius;
                if (_lightType == Type.Spot)
                    UpdateSpotLight();
            }
        }

        /// <summary>
        /// The depth bias used in calculating the shadow map.
        /// </summary>
        public float ShadowDepthBias
        {
            get { return f_shadowDepthBias; }
            set { f_shadowDepthBias = value; }
        }

        /// <summary>
        /// The max distance of the cascade shadow map.
        /// </summary>
        public float ShadowDistance
        {
            get { return f_shadowDistance; }
            set { f_shadowDistance = value; }
        }

        /// <summary>
        /// The intensity of the specular light.
        /// </summary>
        public float SpecularIntensity
        {
            get { return f_specularIntensity; }
            set { f_specularIntensity = value; }
        }

        /// <summary>
        /// The angle of the spot light cone.
        /// </summary>
        public float SpotConeAngle
        {
            get { return f_spotConeAngle; }
            set
            {
                f_spotConeAngle = value;
                if (_lightType == Type.Spot)
                    UpdateSpotLight();
            }
        }

        /// <summary>
        /// Kinda like the drop off of the spot light borders. 
        /// The higher the value the more gradual the fade will be.
        /// </summary>
        public float SpotExponent
        {
            get { return f_spotExponent; }
            set { f_spotExponent = value; }
        }

        /// <summary>
        /// Stores the transform (world space) of the light.
        /// The direction and position is in here.
        /// </summary>
        public Matrix Transform
        {
            get { return m_transform; }
            set
            {
                m_transform = value;
                Matrix.Invert(ref m_transform, out _invTransform);
                _boundingSphere.Center = m_transform.Translation;
                if (_lightType == Type.Spot)
                    UpdateSpotLight();
            }
        }

        /// <summary>
        /// The view multiplied by the projection.
        /// </summary>
        public Matrix ViewProjection
        {
            get { return _viewProjection; }
        }

        /// <summary>
        /// Updates the projection matrix and frustum of associated with the spot light.
        /// </summary>
        protected void UpdateSpotLight()
        {
            m_projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(f_spotConeAngle * 2), 1, 0.01f * f_radius, f_radius);

            Matrix.Multiply(ref _invTransform, ref m_projection, out _viewProjection);
            Frustum.Matrix = _viewProjection;
        }
    }
}