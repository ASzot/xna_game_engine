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

namespace RenderingSystem
{
	/// <summary>
	/// The type of light.
	/// </summary>
    public enum GameLightType { Point, Directional, Spot, Flashing, Rotating }

	/// <summary>
	/// A base for all lights.
	/// Gives base functionality for lights.
	/// </summary>
    public class GameLight
    {
		/// <summary>
		/// Unique ID of the light.
		/// </summary>
        public string LightID;
		/// <summary>
		/// The intensity of the specular light cast.
		/// </summary>
        public float SpecularIntensity;
		/// <summary>
		/// The intensity of the diffuse light cast.
		/// </summary>
        public float DiffuseIntensity;
		/// <summary>
		/// The specular and diffuse light color.
		/// </summary>
        public Color DiffuseColor;
		/// <summary>
		/// Whether this light should be rendered.
		/// </summary>
        public bool Enabled = true;
		
		/// <summary>
		/// Whether this light should be saved.
		/// </summary>
        [NonSerialized]
        public bool Serilize = true;

        public GameLight(Color diffuse, string lightID, float diffInten, float specInten)
        {
            this.DiffuseColor = diffuse;
            this.LightID = lightID;
            this.DiffuseIntensity = diffInten;
            this.SpecularIntensity = specInten;
        }

		/// <summary>
		/// Update the data of the light.
		/// </summary>
		/// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {

        }

		/// <summary>
		/// Make an exact data copy of the light.
		/// </summary>
		/// <returns></returns>
        public virtual GameLight Clone()
        {
            throw new NotImplementedException();
        }

		/// <summary>
		/// Convert to the general renderable light.
		/// </summary>
		/// <returns></returns>
        public virtual RendererImpl.GeneralLight ToRendererLight()
        {
            throw new NotImplementedException();
        }
        
    }

	/// <summary>
	/// A point light radiating in all directions for a given distance.
	/// Has a set position in world space.
	/// </summary>
    public class PointLight : GameLight
    {
		/// <summary>
		/// World space position.
		/// </summary>
        public Vector3 Position;
		/// <summary>
		/// The range of this point light.
		/// Beyond this range the light won't cast.
		/// </summary>
        public float Range;

        public PointLight(Vector3 pos, float range, float intensity, float specInten, Color diffuse, string lightID)
            : base(diffuse, lightID, intensity, specInten)
        {
            this.Position = pos;
            this.Range = range;
        }

        public PointLight(Vector3 pos, float range, float intensity, float specInten, Color diffuse)
            : this(pos, range, intensity, specInten, diffuse, Guid.NewGuid().ToString())
        {

        }
		

        public override GameLight Clone()
        {
            PointLight pl = new PointLight(Position, Range, DiffuseIntensity, SpecularIntensity, DiffuseColor);
            return pl;
        }

        public override RendererImpl.GeneralLight ToRendererLight()
        {
            RendererImpl.GeneralLight light = new RendererImpl.GeneralLight();

            light.Color = DiffuseColor;
            light.DiffuseIntensity = DiffuseIntensity;
            light.LightType = RendererImpl.GeneralLight.Type.Point;
            light.Radius = Range;
            light.Transform = Matrix.CreateTranslation(Position);
            light.CastShadows = false;
            light.SpecularIntensity = SpecularIntensity;
            light.Enabled = this.Enabled;

            return light;
        }
    }

	/// <summary>
	/// A directional light having a direction in world space.
	/// Doesn't have a world position, on the infinite back plane.
	/// Can cast shadows and have a flare glare.
	/// </summary>
    public class DirLight : GameLight
    {
		/// <summary>
		/// The rotation of the directional light.
		/// Used to calculate the direction vector.
		/// </summary>
        public Vector3 Rotation;
		/// <summary>
		/// Whether shadow map should be calculated for this light.
		/// </summary>
        public bool CastShadows;
		/// <summary>
		/// The bias used when calculating the shadow map for this light.
		/// </summary>
        public float ShadowBias = 0.0005f;
		/// <summary>
		/// The max distance of the cascading shadow maps for this light.
		/// </summary>
        public float ShadowDistance = 20f;

		/// <summary>
		/// The flare occlusion query (sampling) size for the flare effect.
		/// </summary>
        public float FlareQuerySize = 100;
		/// <summary>
		/// The flare circle glow thing.
		/// </summary>
        public float FlareGlowSize = 400;
		/// <summary>
		/// Whether the lens flare effect should be used. 
		/// </summary>
        public bool UseLensFlare = true;

        public DirLight(Vector3 rot, Color diffuse, float inten, float specInten, bool castShadows, string lightID)
            : base(diffuse, lightID, inten, specInten)
        {
            this.Rotation = rot;
            this.CastShadows = castShadows;
        }

        public DirLight(Vector3 rot, Color diffuse, float inten, float specInten, bool castShadows)
            : this(rot, diffuse, inten, specInten, castShadows, Guid.NewGuid().ToString())
        {
            
        }

        public override RendererImpl.GeneralLight ToRendererLight()
        {
            Matrix lightWorld = Matrix.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z);

            RendererImpl.GeneralLight light = new RendererImpl.GeneralLight();
            light.LightType = RendererImpl.GeneralLight.Type.Directional;
            light.ShadowDepthBias = ShadowBias;
            light.Transform = lightWorld;
            light.DiffuseIntensity = DiffuseIntensity;
            light.SpecularIntensity = SpecularIntensity;
            light.ShadowDistance = ShadowDistance;
            light.CastShadows = CastShadows;
            light.Color = DiffuseColor;
            light.FlareEnabled = UseLensFlare;
            light.GlowSize = FlareGlowSize;
            light.QuerySize = FlareQuerySize;
            light.Enabled = this.Enabled;

            return light;
        }
    }

	/// <summary>
	/// A spot light having a position, direction, and range.
	/// Can cast shadows and have a flare effect.
	/// </summary>
    public class SpotLight : GameLight
    {
		/// <summary>
		/// The world transform of this light.
		/// Encodes direction and position of the spot light.
		/// </summary>
        public Matrix World
        {
            get
            {
                Matrix transform = Matrix.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z);
                transform.Translation = Position;
                return transform;
            }
        }

		/// <summary>
		/// Whether the light should cast shadows.
		/// </summary>
        public bool CastShadows;
		/// <summary>
		/// The range of the spot light.
		/// </summary>
        public float Range;
		/// <summary>
		/// The angle of the spot light cone.
		/// </summary>
        public float SpotConeAngle = 45f;
		/// <summary>
		/// The drop off of the spot light.
		/// The higher the value the more gradual the drop off.
		/// </summary>
        public float SpotExponent = 10f;
		/// <summary>
		/// The bias used in calculating the shadow map.
		/// </summary>
        public float DepthBias = 0.0005f;
		/// <summary>
		/// The world position of the spot light.
		/// </summary>
        public Vector3 Position = Vector3.Zero;
		/// <summary>
		/// The rotation of the spot light.
		/// Used for direction.
		/// </summary>
        public Vector3 Rotation = Vector3.Zero;
		/// <summary>
		/// The flare occlusion query (sampling ) size.
		/// </summary>
        public float FlareQuerySize = 100;
		/// <summary>
		/// The size of the flare glow size or the circle thing.
		/// </summary>
        public float FlareGlowSize = 400;
		/// <summary>
		/// Whether the lens flare should be used.
		/// </summary>
        public bool UseLensFlare = true;


        public SpotLight(Vector3 pos, Vector3 rot, Color diffuse, float range, float intensity, float specIntensity, bool castShadows, string lightID)
            : base(diffuse, lightID, intensity, specIntensity)
        {
            this.Position = pos;
            this.Rotation = rot;
            this.CastShadows = castShadows;
			this.CastShadows = castShadows;
            this.Range = range;
        }

        public SpotLight(Vector3 pos, Vector3 rot, Color diffuse, float range, float intensity, float specIntensity, bool castShadows)
            : this(pos, rot, diffuse, range, intensity, specIntensity, castShadows, Guid.NewGuid().ToString())
        {

        }

        public override RendererImpl.GeneralLight ToRendererLight()
        {
			RendererImpl.GeneralLight light = new RendererImpl.GeneralLight();
			light.LightType = RendererImpl.GeneralLight.Type.Spot;
			light.Radius = Range;
			light.CastShadows = CastShadows;
			light.ShadowDepthBias = DepthBias;
			light.SpotConeAngle = SpotConeAngle;
			light.SpotExponent = SpotExponent;
			light.ShadowDepthBias = DepthBias;
			light.DiffuseIntensity = DiffuseIntensity;
			light.Transform = World;
			light.Color = DiffuseColor;
            light.SpecularIntensity = SpecularIntensity;
            light.FlareEnabled = UseLensFlare;
            light.GlowSize = FlareGlowSize;
            light.QuerySize = FlareQuerySize;
            light.Enabled = this.Enabled;

            return light;
        }
    }
}
