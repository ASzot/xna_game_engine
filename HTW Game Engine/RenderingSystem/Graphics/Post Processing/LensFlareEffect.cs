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

namespace RenderingSystem.Graphics
{
	/// <summary>
	/// A singular lens flare effect.
	/// </summary>
    class LensFlareEffect
    {
		/// <summary>
		/// Representing the flare data.
		/// </summary>
        public class Flare
        {
            public Flare(float position, float scale, Color color, string textureName)
            {
                Position = position;
                Scale = scale;
                Color = color;
                TextureName = textureName;
            }

            public float Position;
            public float Scale;
            public Color Color;
            public string TextureName;
            public Texture2D Texture;
        }



        /// <summary>
        /// Size of the flare circle thing.
        /// </summary>
        float _glowSize = 400;

        /// <summary>
        /// The query (sampling) size 
        /// </summary>
        float _querySize = 100;

		/// <summary>
		/// The query size for sampling the occlusion data.
		/// </summary>
        public float QuerySize
        {
            get { return _querySize; }
            set
            {
                _querySize = value;
                _queryVerticies[0].Position = new Vector3(-_querySize / 2, -_querySize / 2, -1);
                _queryVerticies[1].Position = new Vector3(_querySize / 2, -_querySize / 2, -1);
                _queryVerticies[2].Position = new Vector3(-_querySize / 2, _querySize / 2, -1);
                _queryVerticies[3].Position = new Vector3(_querySize / 2, _querySize / 2, -1);
            }
        }

		/// <summary>
		/// The size of the glow circle thing.
		/// </summary>
        public float GlowSize
        {
            get { return _glowSize; }
            set { _glowSize = value; }
        }

		/// <summary>
		/// Whether the flare is projected onto the infinite plan. Used for directional light.
		/// </summary>
        private bool b_infiniteViewPlane = true;

		/// <summary>
		/// Whether the flare is projected onto the infinite plan. Used for directional light.
		/// </summary>
        public bool InfiniteViewPlane
        {
            get { return b_infiniteViewPlane; }
            set { b_infiniteViewPlane = value; }
        }

		/// <summary>
		/// The position of the light in world space.
		/// </summary>
        public Vector3 LightPos;

        /// <summary>
        /// The position gained from the occlusion test.
        /// </summary>
        Vector2 _lightPosition;
		/// <summary>
		/// Whether the light should be ignored as it is behind the player.
		/// </summary>
        bool b_lightBehindCamera;


        /// <summary>
        /// The texture for the glow circle thing.
        /// </summary>
        Texture2D _glowSprite;
		/// <summary>
		/// The effect for just rendering the circle things on the screen.
		/// </summary>
        BasicEffect _basicEffect;
		/// <summary>
		/// The vertices of the query position in the occlusion.
		/// </summary>
        VertexPositionColor[] _queryVerticies;

		/// <summary>
		/// The blend state for rendering the cirlce flare.
		/// </summary>
        static readonly BlendState ColorWriteDisable = new BlendState
        {
            ColorSourceBlend = Blend.One,
            AlphaSourceBlend = Blend.One,
            ColorDestinationBlend = Blend.Zero,
            AlphaDestinationBlend = Blend.Zero,
            ColorWriteChannels = ColorWriteChannels.None  
        };

		/// <summary>
		/// The occlusion tester supplied by the XNA.
		/// </summary>
        OcclusionQuery _occlusionQuery;
		/// <summary>
		/// Whether the occlusion queries are active.
		/// </summary>
        bool b_occlusionQueryActive;
		/// <summary>
		/// The alpha value supplied by the occlusion test.
		/// </summary>
        float f_occlusionAlpha;

		/// <summary>
		/// The settings of the flares displayed along the vector from the sun.
		/// </summary>
        public Flare[] flares =
        {
            new Flare(-0.5f, 0.7f, new Color( 50,  25,  50), "flare1"),
            new Flare( 0.3f, 0.4f, new Color(100, 255, 200), "flare1"),
            new Flare( 1.2f, 1.0f, new Color(100,  50,  50), "flare1"),
            new Flare( 1.5f, 1.5f, new Color( 50, 100,  50), "flare1"),

            new Flare(-0.3f, 0.7f, new Color(200,  50,  50), "flare2"),
            new Flare( 0.6f, 0.9f, new Color( 50, 100,  50), "flare2"),
            new Flare( 0.7f, 0.4f, new Color( 50, 200, 200), "flare2"),

            new Flare(-0.7f, 0.7f, new Color( 50, 100,  25), "flare3"),
            new Flare( 0.0f, 0.6f, new Color( 25,  25,  25), "flare3"),
            new Flare( 2.0f, 1.4f, new Color( 25,  50, 100), "flare3"),
        };

		/// <summary>
		/// Construct a lens flare with the default settings.
		/// </summary>
        public LensFlareEffect()
        {

        }

		/// <summary>
		/// Load all resource related content.
		/// </summary>
		/// <param name="device"></param>
        public void LoadContent(GraphicsDevice device)
        {

            // Load the glow and flare textures.
            _glowSprite = ResourceMgr.LoadTexture("glow");

            foreach (Flare flare in flares)
            {
                flare.Texture = ResourceMgr.LoadTexture(flare.TextureName);
            }

            // Effect for drawing occlusion query polygons.
            _basicEffect = new BasicEffect(device);
            
            _basicEffect.View = Matrix.Identity;
            _basicEffect.VertexColorEnabled = true;

            // Create vertex data for the occlusion query polygons.
            _queryVerticies = new VertexPositionColor[4];

            _queryVerticies[0].Position = new Vector3(-_querySize / 2, -_querySize / 2, -1);
            _queryVerticies[1].Position = new Vector3( _querySize / 2, -_querySize / 2, -1);
            _queryVerticies[2].Position = new Vector3(-_querySize / 2,  _querySize / 2, -1);
            _queryVerticies[3].Position = new Vector3( _querySize / 2,  _querySize / 2, -1);

            // Create the occlusion query object.
            _occlusionQuery = new OcclusionQuery(device);
        }

		/// <summary>
		/// Draw the lens flare effect over whatever is currently set as the render target.
		/// </summary>
		/// <param name="device">The graphics device.</param>
		/// <param name="spriteBatch"></param>
        public void Draw(GraphicsDevice device, SpriteBatch spriteBatch)
        {
            DrawGlow(spriteBatch);
            DrawFlares(device, spriteBatch);

            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.Default;
            device.SamplerStates[0] = SamplerState.LinearWrap;
        }

		/// <summary>
		/// Update the occlusion data which determines the oppacity of the flare effect to render.
		/// This should be called during the geometry rendering stage of GBuffer construction.
		/// </summary>
		/// <param name="view">Camera view matrix.</param>
		/// <param name="proj">Camera projection matrix.</param>
		/// <param name="device">Graphics device.</param>
        public void UpdateOcclusion(Matrix view, Matrix proj, GraphicsDevice device)
        {
            Viewport viewport = device.Viewport;

            Matrix viewMat = view;
            if (b_infiniteViewPlane)
            {
                viewMat.Translation = Vector3.Zero;
            }

            Vector3 projectedPosition = viewport.Project(LightPos, proj,
                                                         viewMat, Matrix.Identity);

            // Don't draw any flares if the light is behind the camera.
            if ((projectedPosition.Z < 0) || (projectedPosition.Z > 1))
            {
                b_lightBehindCamera = true;
                return;
            }

            _lightPosition = new Vector2(projectedPosition.X, projectedPosition.Y);
            b_lightBehindCamera = false;

            if (b_occlusionQueryActive)
            {
                // If the previous query has not yet completed, wait until it does.
                if (!_occlusionQuery.IsComplete)
                    return;

                // Use the occlusion query pixel count to work
                // out what percentage of the sun is visible.
                float queryArea = _querySize * _querySize;

                f_occlusionAlpha = Math.Min(_occlusionQuery.PixelCount / queryArea, 1);
            }

            // Set renderstates for drawing the occlusion query geometry. We want depth
            // tests enabled, but depth writes disabled, and we disable color writes
            // to prevent this query polygon actually showing up on the screen.
            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.DepthRead;

            // Set up our BasicEffect to center on the current 2D light position.
            _basicEffect.World = Matrix.CreateTranslation(_lightPosition.X,
                                                         _lightPosition.Y, 0);

            _basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0,
                                                                        viewport.Width,
                                                                        viewport.Height,
                                                                        0, 0, 1);

            _basicEffect.CurrentTechnique.Passes[0].Apply();

            // Issue the occlusion query.
            _occlusionQuery.Begin();

            device.DrawUserPrimitives(PrimitiveType.TriangleStrip, _queryVerticies, 0, 2);

            _occlusionQuery.End();

            b_occlusionQueryActive = true;
        }

		/// <summary>
		/// Draw the glow or the circle thing.
		/// </summary>
		/// <param name="spriteBatch"></param>
        private void DrawGlow(SpriteBatch spriteBatch)
        {
            if (b_lightBehindCamera || f_occlusionAlpha <= 0)
                return;

            Color color = Color.White * f_occlusionAlpha;
            Vector2 origin = new Vector2(_glowSprite.Width, _glowSprite.Height) / 2;
            float scale = _glowSize * 2 / _glowSprite.Width;

            spriteBatch.Begin();

            spriteBatch.Draw(_glowSprite, _lightPosition, null, color, 0,
                             origin, scale, SpriteEffects.None, 0);

            spriteBatch.End();
        }

		/// <summary>
		/// Draw the flares themselves which are along the vector.
		/// </summary>
		/// <param name="device"></param>
		/// <param name="spriteBatch"></param>
        private void DrawFlares(GraphicsDevice device, SpriteBatch spriteBatch)
        {
			// If the light is below the camera or the light is completely occluded by something don't draw.
            if (b_lightBehindCamera || f_occlusionAlpha <= 0)
                return;

            Viewport viewport = device.Viewport;

            Vector2 screenCenter = new Vector2(viewport.Width, viewport.Height) / 2;
            
            Vector2 flareVector = screenCenter - _lightPosition;

            spriteBatch.Begin(0, BlendState.Additive);

            foreach (Flare flare in flares)
            {
                Vector2 flarePosition = _lightPosition + flareVector * flare.Position;

                Vector4 flareColor = flare.Color.ToVector4();

                flareColor.W *= f_occlusionAlpha;

                Vector2 flareOrigin = new Vector2(flare.Texture.Width,
                                                  flare.Texture.Height) / 2;

                spriteBatch.Draw(flare.Texture, flarePosition, null,
                                 new Color(flareColor), 1, flareOrigin,
                                 flare.Scale, SpriteEffects.None, 0);
            }

            spriteBatch.End();
        }
    }
}
