#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem.RendererImpl
{
    /// <summary>
    /// A 2D water plane which renders the scene to it using the appropriate clipping.
    /// The rendered scene is then displayed on the plane using a neat refraction effect
    /// with the sampling of a bump map. Using this has major performance implications as it
    /// requires another fullscreen scene render.
    /// </summary>
    public class WaterPlane : Mesh
    {
        private static WaterRenderEffect _effect;
        private static VertexBuffer _vertexBuffer;
        private RenderTarget2D _refractionRT;
        private Texture2D _refractionTex;
        private Texture2D _waterBumpTex;
        private bool b_enabled = true;
        private float f_scaleX;
        private float f_scaleZ;
        private float f_transSpeed;
        private float f_waterColorFactor;
        private float f_waveHeight;
        private float f_waveLength;
        private Matrix m_texTransform;
        private Quaternion q_rot;
        private Vector3 v_pos;
        private Vector3 v_transDir;

        private Vector4 v_waterColor;

        public WaterPlane()
        {
        }

        /// <summary>
        /// Whether the water plane should be rendered.
        /// </summary>
        public bool Enabled
        {
            get { return b_enabled; }
            set { b_enabled = value; }
        }

        /// <summary>
        /// The position of the plane.
        /// </summary>
        public Vector3 Position
        {
            set { v_pos = value; }
        }

        /// <summary>
        /// The rendered scene refraction image.
        /// </summary>
        public RenderTarget2D RefractionRT
        {
            set { _refractionRT = value; }
        }

        /// <summary>
        /// The rotation of the plane.
        /// </summary>
        public Quaternion Rotation
        {
            set { q_rot = value; }
        }

        /// <summary>
        /// Scale in X direction of the plane.
        /// </summary>
        public float ScaleX
        {
            set { f_scaleX = value; }
        }

        /// <summary>
        /// Scale in the Z direction of the plane.
        /// </summary>
        public float ScaleZ
        {
            set { f_scaleZ = value; }
        }

        /// <summary>
        /// The texture coordinate transform.
        /// </summary>
        public Matrix TextureTransform
        {
            set { m_texTransform = value; }
        }

        /// <summary>
        /// The translation direction of the bump map texture.
        /// </summary>
        public Vector3 TransDir
        {
            get { return v_transDir; }
            set { v_transDir = value; }
        }

        /// <summary>
        /// The translation speed of the bump map texture.
        /// </summary>
        public float TransSpeed
        {
            get { return f_transSpeed; }
            set { f_transSpeed = value; }
        }

        /// <summary>
        /// The water bump map texture used for creating the illusion of small waves.
        /// </summary>
        public Texture2D WaterBumpTexture
        {
            set { _waterBumpTex = value; }
        }

        /// <summary>
        /// Color of the waves.
        /// With the colors 1.0f is like 255.
        /// </summary>
        public Vector4 WaterColor
        {
            get { return v_waterColor; }
            set { v_waterColor = value; }
        }

        /// <summary>
        /// The weight the water color has with the refraction image.
        /// </summary>
        public float WaterColorFactor
        {
            get { return f_waterColorFactor; }
            set { f_waterColorFactor = value; }
        }

        /// <summary>
        /// The height of the waves.
        /// </summary>
        public float WaveHeight
        {
            get { return f_waveHeight; }
            set { f_waveHeight = value; }
        }

        /// <summary>
        /// The length of the waves.
        /// </summary>
        public float WaveLength
        {
            get { return f_waveLength; }
            set { f_waveLength = value; }
        }

        /// <summary>
        /// The world matrix of the water plane.
        /// </summary>
        public Matrix WorldTransform
        {
            get
            {
                Matrix world = Matrix.CreateScale(new Vector3(f_scaleX, 0f, f_scaleZ)) * Matrix.CreateFromQuaternion(q_rot) * Matrix.CreateTranslation(v_pos);

                return world;
            }
        }

        /// <summary>
        /// Draw the primitives on the vertex buffer.
        /// </summary>
        /// <param name="device"></param>
        public static void DrawVerticies(GraphicsDevice device)
        {
            device.DrawPrimitives(PrimitiveType.TriangleList, 0, _vertexBuffer.VertexCount / 3);
        }

        /// <summary>
        /// Load all the effect assets for the water plane.
        /// </summary>
        /// <param name="device"></param>
        public static void LoadContent(GraphicsDevice device)
        {
            // Load the vertex buffer.
            VertexPositionTexture[] waterVertices = new VertexPositionTexture[6];

            waterVertices[0] = new VertexPositionTexture(new Vector3(0, 0, 0), new Vector2(0, 1));
            waterVertices[2] = new VertexPositionTexture(new Vector3(1, 0, -1), new Vector2(1, 0));
            waterVertices[1] = new VertexPositionTexture(new Vector3(0, 0, -1), new Vector2(0, 0));

            waterVertices[3] = new VertexPositionTexture(new Vector3(0, 0, 0), new Vector2(0, 1));
            waterVertices[5] = new VertexPositionTexture(new Vector3(1, 0, 0), new Vector2(1, 1));
            waterVertices[4] = new VertexPositionTexture(new Vector3(1, 0, -1), new Vector2(1, 0));

            WaterPlane._vertexBuffer = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, waterVertices.Length, BufferUsage.WriteOnly);
            WaterPlane._vertexBuffer.SetData(waterVertices);

            _effect = new WaterRenderEffect();
        }

        /// <summary>
        /// Set the vertex buffer.
        /// </summary>
        /// <param name="device"></param>
        public static void SetVertexBuffer(GraphicsDevice device)
        {
            device.SetVertexBuffer(_vertexBuffer);
        }

        /// <summary>
        /// Render to the refraction render target.
        /// DOES A FULLSCREEN RENDER.
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="cam"></param>
        /// <param name="renderWorld"></param>
        public void BuildSceneRenderTextures(Renderer renderer, ICamera cam, RenderController renderWorld)
        {
            Plane waterPlane = CreatePlane(v_pos.Y + 20f, new Vector3(0f, -1f, 0f), false);
            _refractionTex = renderer.RenderSceneEffectTexture(cam, renderWorld, _refractionRT, waterPlane);
        }

        /// <summary>
        /// Set all of the variables for the effect this needs to be called before every render.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="cam"></param>
        /// <param name="gameTime"></param>
        public void SetEffectData(GraphicsDevice device, ICamera cam, GameTime gameTime)
        {
            WaterPlane._effect.SetCurrentTechnique("PlanarWaterTechnique");
            WaterPlane._effect.CameraPosParameter.SetValue(cam.Position);
            WaterPlane._effect.ProjParameter.SetValue(cam.Proj);
            if (_refractionTex == null)
            {
                throw new InvalidOperationException("The refraction texture was never rendered to.");
            }

            WaterPlane._effect.RefractionMapParameter.SetValue(_refractionTex);
            // Have a slightly scaled time parameter.
            const float timeAdjustmentFactor = 100f;
            float adjustedTime = (float)gameTime.TotalGameTime.TotalMilliseconds / timeAdjustmentFactor;
            WaterPlane._effect.TimeParameter.SetValue(adjustedTime);
            WaterPlane._effect.TransDirParameter.SetValue(v_transDir);
            WaterPlane._effect.TransSpeedParameter.SetValue(f_transSpeed);
            WaterPlane._effect.ViewParameter.SetValue(cam.View);
            WaterPlane._effect.WaterBumpMapParameter.SetValue(_waterBumpTex);
            WaterPlane._effect.WaterColorFactorParameter.SetValue(f_waterColorFactor);
            WaterPlane._effect.WaterColorParameter.SetValue(v_waterColor);
            WaterPlane._effect.WaveHeightParameter.SetValue(f_waveHeight);
            WaterPlane._effect.WaveLengthParameter.SetValue(f_waveLength);
            WaterPlane._effect.WorldParameter.SetValue(WorldTransform);
            WaterPlane._effect.TexTransform.SetValue(m_texTransform);

            WaterPlane._effect.Apply();
        }

        /// <summary>
        /// Set the water bump texture.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool SetWaterBumpTexture(string filename)
        {
            _waterBumpTex = ResourceMgr.LoadTexture(filename);

            if (_waterBumpTex == null)
                return false;
            return true;
        }

        /// <summary>
        /// Construct a plane from the given parameters.
        /// </summary>
        /// <param name="height">Height of the plane in world space.</param>
        /// <param name="planeNormalDirection">The direction the plane is facing.</param>
        /// <param name="clipSide">Whether the top or bottom of the plane is going to be clipped.</param>
        /// <returns></returns>
        private Plane CreatePlane(float height, Vector3 planeNormalDirection, bool clipSide)
        {
            planeNormalDirection.Normalize();
            Vector4 planeCoeffs = new Vector4(planeNormalDirection, height);
            if (clipSide)
                planeCoeffs *= -1;
            Plane finalPlane = new Plane(planeCoeffs);
            return finalPlane;
        }
    }

    /// <summary>
    /// Used to simplify accessing the fx variables of the water effect.
    /// </summary>
    internal class WaterRenderEffect : GeneralRenderEffect
    {
        private const string EFFECT_FILENAME = "water";

        private EffectParameter _camPosParameter;
        private EffectParameter _projParameter;
        private EffectParameter _refractionMapParamter;
        private EffectParameter _texTransform;
        private EffectParameter _timeParameter;
        private EffectParameter _transDirParameter;
        private EffectParameter _transSpeedParameter;
        private EffectParameter _viewParameter;
        private EffectParameter _waterBumpMapParameter;
        private EffectParameter _waterColorFactorParameter;
        private EffectParameter _waterColorParameter;
        private EffectParameter _waveHeightParameter;
        private EffectParameter _waveLengthParameter;
        private EffectParameter _worldParameter;

        public WaterRenderEffect()
            : base(EFFECT_FILENAME)
        {
            ExtractNames();
        }

        public EffectParameter CameraPosParameter
        {
            get { return _camPosParameter; }
        }

        public EffectParameter ProjParameter
        {
            get { return _projParameter; }
        }

        public EffectParameter RefractionMapParameter
        {
            get { return _refractionMapParamter; }
        }

        public EffectParameter TexTransform
        {
            get { return _texTransform; }
        }

        public EffectParameter TimeParameter
        {
            get { return _timeParameter; }
        }

        public EffectParameter TransDirParameter
        {
            get { return _transDirParameter; }
        }

        public EffectParameter TransSpeedParameter
        {
            get { return _transSpeedParameter; }
        }

        public EffectParameter ViewParameter
        {
            get { return _viewParameter; }
        }

        public EffectParameter WaterBumpMapParameter
        {
            get { return _waterBumpMapParameter; }
        }

        public EffectParameter WaterColorFactorParameter
        {
            get { return _waterColorFactorParameter; }
        }

        public EffectParameter WaterColorParameter
        {
            get { return _waterColorParameter; }
        }

        public EffectParameter WaveHeightParameter
        {
            get { return _waveHeightParameter; }
        }

        public EffectParameter WaveLengthParameter
        {
            get { return _waveLengthParameter; }
        }

        public EffectParameter WorldParameter
        {
            get { return _worldParameter; }
        }

        private void ExtractNames()
        {
            _viewParameter = GetEffectParameter("View");
            _projParameter = GetEffectParameter("Projection");
            _worldParameter = GetEffectParameter("World");
            _waterBumpMapParameter = GetEffectParameter("WaterBumpMap");
            _refractionMapParamter = GetEffectParameter("RefractionMap");
            _waveLengthParameter = GetEffectParameter("WaveLength");
            _waveHeightParameter = GetEffectParameter("WaveHeight");
            _waterColorParameter = GetEffectParameter("WaterColor");
            _waterColorFactorParameter = GetEffectParameter("WaterColorFactor");
            _camPosParameter = GetEffectParameter("CamPos");
            _timeParameter = GetEffectParameter("Time");
            _transDirParameter = GetEffectParameter("TransDir");
            _transSpeedParameter = GetEffectParameter("TransSpeed");
            _texTransform = GetEffectParameter("TexTransform");

            _waterColorParameter.SetValue(new Vector4(0.2f, 0.2f, 0.3f, 1.0f));
        }
    }
}