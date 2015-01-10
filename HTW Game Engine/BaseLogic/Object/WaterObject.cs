#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using Microsoft.Xna.Framework;

using RenderingSystem;

namespace BaseLogic.Object
{
    /// <summary>
    /// 
    /// </summary>
    public class WaterObject : GameObj
    {
        /// <summary>
        /// The defaul t_ wate r_ bum p_ loc
        /// </summary>
        private const string DEFAULT_WATER_BUMP_LOC = "waterbump";

        /// <summary>
        /// The _refraction rt
        /// </summary>
        private GameRenderTarget _refractionRT;

        /// <summary>
        /// The _refraction rt height
        /// </summary>
        private int _refractionRTHeight;

        /// <summary>
        /// The _refraction rt width
        /// </summary>
        private int _refractionRTWidth;

        /// <summary>
        /// The _water bump map
        /// </summary>
        private GameTexture _waterBumpMap;

        /// <summary>
        /// The b_enabled
        /// </summary>
        private bool b_enabled = true;

        /// <summary>
        /// The b_kill
        /// </summary>
        private bool b_kill = false;

        /// <summary>
        /// The f_scale x
        /// </summary>
        private float f_scaleX = 1f;

        /// <summary>
        /// The f_scale z
        /// </summary>
        private float f_scaleZ = 1f;

        /// <summary>
        /// The f_tex rot x
        /// </summary>
        private float f_texRotX = 0.0f;

        /// <summary>
        /// The f_tex rot y
        /// </summary>
        private float f_texRotY = 0.0f;

        /// <summary>
        /// The f_tex rot z
        /// </summary>
        private float f_texRotZ = 0.0f;

        /// <summary>
        /// The f_tex scale x
        /// </summary>
        private float f_texScaleX = 1.0f;

        /// <summary>
        /// The f_tex scale y
        /// </summary>
        private float f_texScaleY = 1.0f;

        /// <summary>
        /// The f_trans speed
        /// </summary>
        private float f_transSpeed = 0.002f;

        /// <summary>
        /// The f_water color factor
        /// </summary>
        private float f_waterColorFactor = 0.2f;

        /// <summary>
        /// The f_wave height
        /// </summary>
        private float f_waveHeight = 0.2f;

        /// <summary>
        /// The f_wave length
        /// </summary>
        private float f_waveLength = 0.4f;

        /// <summary>
        /// The q_rot
        /// </summary>
        private Quaternion q_rot = Quaternion.Identity;

        /// <summary>
        /// The s_actor identifier
        /// </summary>
        private string s_actorId;

        /// <summary>
        /// The v_pos
        /// </summary>
        private Vector3 v_pos = Vector3.Zero;

        /// <summary>
        /// The v_trans dir
        /// </summary>
        private Vector3 v_transDir = new Vector3(-1f, 0f, 0f);

        /// <summary>
        /// The v_water color
        /// </summary>
        private Vector4 v_waterColor = new Vector4(0.2f, 0.2f, 0.3f, 1.0f);

        /// <summary>
        /// Initializes a new instance of the <see cref="WaterObject"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public WaterObject(string id)
        {
            s_actorId = id;
            SetWaterBumpTexture(DEFAULT_WATER_BUMP_LOC);
            var pp = ResourceMgr.Device.PresentationParameters;
            SetRefractionMapResolution(pp.BackBufferWidth, pp.BackBufferHeight);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WaterObject"/> class.
        /// </summary>
        public WaterObject()
            : this(Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// The unique identification of the actor.
        /// </summary>
        public string ActorID
        {
            get { return s_actorId; }
            set { s_actorId = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WaterObject"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled
        {
            get { return b_enabled; }
            set { b_enabled = value; }
        }

        /// <summary>
        /// Setting to true will remove the object from the manager in charge of it.
        /// </summary>
        public bool Kill
        {
            get { return b_kill; }
            set { b_kill = value; }
        }

        /// <summary>
        /// The position of the object in the 3D scene.
        /// </summary>
        public Vector3 Position
        {
            get { return v_pos; }
            set { v_pos = value; }
        }

        /// <summary>
        /// Rotation of the object.
        /// </summary>
        public Quaternion Rotation
        {
            get { return q_rot; }
            set { q_rot = value; }
        }

        /// <summary>
        /// Uniform scaling of the object.
        /// </summary>
        public float Scale
        {
            get
            {
                // Hopefully the user will never access this.
                float scaleAvg = (f_scaleX + f_scaleZ) / 2f;
                return scaleAvg;
            }
            set { f_scaleZ = f_scaleX = value; }
        }

        /// <summary>
        /// Gets or sets the scale x.
        /// </summary>
        /// <value>
        /// The scale x.
        /// </value>
        public float ScaleX
        {
            get { return f_scaleX; }
            set { f_scaleX = value; }
        }

        /// <summary>
        /// Gets or sets the scale z.
        /// </summary>
        /// <value>
        /// The scale z.
        /// </value>
        public float ScaleZ
        {
            set { f_scaleZ = value; }
            get { return f_scaleZ; }
        }

        /// <summary>
        /// Gets or sets the tex rot x.
        /// </summary>
        /// <value>
        /// The tex rot x.
        /// </value>
        public float TexRotX
        {
            get { return f_texRotX; }
            set { f_texRotX = value; }
        }

        /// <summary>
        /// Gets or sets the tex rot y.
        /// </summary>
        /// <value>
        /// The tex rot y.
        /// </value>
        public float TexRotY
        {
            get { return f_texRotY; }
            set { f_texRotY = value; }
        }

        /// <summary>
        /// Gets or sets the tex rot z.
        /// </summary>
        /// <value>
        /// The tex rot z.
        /// </value>
        public float TexRotZ
        {
            get { return f_texRotZ; }
            set { f_texRotZ = value; }
        }

        /// <summary>
        /// Gets or sets the tex scale x.
        /// </summary>
        /// <value>
        /// The tex scale x.
        /// </value>
        public float TexScaleX
        {
            get { return f_texScaleX; }
            set { f_texScaleX = value; }
        }

        /// <summary>
        /// Gets or sets the tex scale y.
        /// </summary>
        /// <value>
        /// The tex scale y.
        /// </value>
        public float TexScaleY
        {
            get { return f_texScaleY; }
            set { f_texScaleY = value; }
        }

        /// <summary>
        /// Gets or sets the trans dir.
        /// </summary>
        /// <value>
        /// The trans dir.
        /// </value>
        public Vector3 TransDir
        {
            get { return v_transDir; }
            set { v_transDir = value; }
        }

        /// <summary>
        /// Gets or sets the trans speed.
        /// </summary>
        /// <value>
        /// The trans speed.
        /// </value>
        public float TransSpeed
        {
            get { return f_transSpeed; }
            set { f_transSpeed = value; }
        }

        /// <summary>
        /// Gets or sets the color of the water.
        /// </summary>
        /// <value>
        /// The color of the water.
        /// </value>
        public Vector4 WaterColor
        {
            get { return v_waterColor; }
            set { v_waterColor = value; }
        }

        /// <summary>
        /// Gets or sets the water color factor.
        /// </summary>
        /// <value>
        /// The water color factor.
        /// </value>
        public float WaterColorFactor
        {
            get { return f_waterColorFactor; }
            set { f_waterColorFactor = value; }
        }

        /// <summary>
        /// Gets or sets the height of the wave.
        /// </summary>
        /// <value>
        /// The height of the wave.
        /// </value>
        public float WaveHeight
        {
            get { return f_waveHeight; }
            set { f_waveHeight = value; }
        }

        /// <summary>
        /// Gets or sets the length of the wave.
        /// </summary>
        /// <value>
        /// The length of the wave.
        /// </value>
        public float WaveLength
        {
            get { return f_waveLength; }
            set { f_waveLength = value; }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public GameObj Clone()
        {
            WaterObject waterObj = new WaterObject();
            waterObj._waterBumpMap = ResourceMgr.LoadGameTextureAbsoluteFilename(_waterBumpMap.Filename);
            waterObj.f_scaleX = f_scaleX;
            waterObj.f_scaleZ = f_scaleZ;
            waterObj.f_transSpeed = f_transSpeed;
            waterObj.f_waterColorFactor = f_waterColorFactor;
            waterObj.f_waveHeight = f_waveHeight;
            waterObj.f_waveLength = f_waveLength;
            waterObj.q_rot = q_rot;
            waterObj.v_pos = v_pos;
            waterObj.v_transDir = v_transDir;
            waterObj.v_waterColor = v_waterColor;
            waterObj.SetRefractionMapResolution(_refractionRTWidth, _refractionRTHeight);

            return waterObj;
        }

        /// <summary>
        /// Sets the refraction map resolution.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void SetRefractionMapResolution(int width, int height)
        {
            _refractionRTWidth = width;
            _refractionRTHeight = height;
            _refractionRT = new GameRenderTarget(width, height);
        }

        /// <summary>
        /// Sets the water bump texture.
        /// </summary>
        /// <param name="relFilename">The relative filename.</param>
        public void SetWaterBumpTexture(string relFilename)
        {
            _waterBumpMap = ResourceMgr.LoadGameTexture(relFilename);
        }

        /// <summary>
        /// To the renderer mesh.
        /// </summary>
        /// <returns></returns>
        public RenderingSystem.RendererImpl.Mesh ToRendererMesh()
        {
            RenderingSystem.RendererImpl.WaterPlane waterPlane = new RenderingSystem.RendererImpl.WaterPlane();
            waterPlane.Enabled = b_enabled;
            if (!b_enabled)
                return waterPlane;
            waterPlane.Position = v_pos;
            waterPlane.Rotation = q_rot;
            waterPlane.ScaleX = f_scaleX;
            waterPlane.ScaleZ = f_scaleZ;
            waterPlane.TransDir = v_transDir;
            waterPlane.TransSpeed = f_transSpeed;
            waterPlane.WaterBumpTexture = _waterBumpMap.Texture;
            waterPlane.WaterColor = v_waterColor;
            waterPlane.WaterColorFactor = f_waterColorFactor;
            waterPlane.WaveHeight = f_waveHeight;
            waterPlane.WaveLength = f_waveLength;
            waterPlane.RefractionRT = _refractionRT.RT;

            Matrix textureTransform = Matrix.CreateScale(f_texScaleX, f_texScaleY, 1.0f) * Matrix.CreateRotationX(f_texRotX) *
                Matrix.CreateRotationY(f_texRotY) * Matrix.CreateRotationZ(f_texRotZ);

            waterPlane.TextureTransform = textureTransform;

            return waterPlane;
        }

        /// <summary>
        /// Update the object.
        /// Called every frame by the manager.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
        }
    }
}