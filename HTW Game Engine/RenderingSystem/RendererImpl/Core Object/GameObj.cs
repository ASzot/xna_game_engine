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
    /// A wraper for Texture2D.
    /// </summary>
    [Serializable]
    public struct GameTexture
    {
        private const string s_defaultDiffuseFilename = "images/default_diffuse";
        private const string s_defaultNormalFilename = "images/default_normal";
        private const string s_defaultSpecularFilename = "images/default_specular";


        public static GameTexture DefaultDiffuse
        {
            get { return new GameTexture(s_defaultDiffuseFilename); }
        }

        public static GameTexture DefaultNormal
        {
            get { return new GameTexture(s_defaultNormalFilename); }
        }

        public static GameTexture DefaultSpecular
        {
            get { return new GameTexture(s_defaultSpecularFilename); }
        }

        public static GameTexture Null
        {
            get { return new GameTexture(null, null); }
        }

        /// <summary>
        /// Loads a preexisting texture.
        /// </summary>
        /// <param name="texture2D">The preexisting texture.</param>
        /// <param name="filenameStr">The absolute filename of the texture referenced.</param>
        public GameTexture(Texture2D texture2D, string filenameStr)
        {
            Texture = texture2D;
            Filename = filenameStr;
        }

        /// <summary>
        /// Create a new texture.
        /// </summary>
        /// <param name="filename">The absolute filename of the texture.</param>
        public GameTexture(string filename)
        {
            Texture = ResourceMgr.LoadTextureAbsoluteFilename(filename);
            Filename = filename;
        }

        /// <summary>
        /// Release all unmanaged resources for this texture.
        /// </summary>
        public void Unload()
        {
            if (Texture != null && !Texture.IsDisposed)
                Texture.Dispose();
        }

        /// <summary>
        /// Returns whether the texture is null.
        /// </summary>
        /// <returns>true: texture not null; false: texture null</returns>
        public bool IsValidTexture()
        {
            return Texture != null;
        }

        [NonSerialized] 
        public Texture2D Texture;

        /// <summary>
        /// The absolute filename of the texture.
        /// </summary>
        public string Filename;
    }

    /// <summary>
    /// A wraper class for a RenderTarget2D.
    /// </summary>
    public class GameRenderTarget
    {
        private const SurfaceFormat DEF_SURFACE_FORMAT = SurfaceFormat.Color;
        private const DepthFormat DEF_DEPTH_FORMAT = DepthFormat.Depth24Stencil8;
        private const RenderTargetUsage DEF_RT_USAGE = RenderTargetUsage.DiscardContents;

        private RenderTarget2D _renderTarget;

        public RenderTarget2D RT
        {
            get { return _renderTarget; }
        }

        public GameRenderTarget(int width, int height)
        {
            _renderTarget = new RenderTarget2D(ResourceMgr.Device, width, height, false, DEF_SURFACE_FORMAT, DEF_DEPTH_FORMAT, 0, DEF_RT_USAGE);
        }

        public GameRenderTarget(int width, int height, SurfaceFormat sf, DepthFormat df, RenderTargetUsage rtu, bool mipmap, int multiSampleCount)
        {
            _renderTarget = new RenderTarget2D(ResourceMgr.Device, width, height, mipmap, sf, df, multiSampleCount, rtu);
        }
    }

	/// <summary>
	/// The subset material for a rendering game object.
	/// </summary>
    [Serializable]
    public class SubsetMaterial
    {
		/// <summary>
		/// Diffuse map.
		/// </summary>
        public GameTexture DiffuseMap = GameTexture.Null;
		/// <summary>
		/// Specular map.
		/// </summary>
        public GameTexture SpecularMap = GameTexture.Null;
		/// <summary>
		/// Normal map.
		/// </summary>
        public GameTexture NormalMap = GameTexture.Null;
		/// <summary>
		/// Emissive map. Not really used.
		/// </summary>
        public GameTexture EmmisiveMap = GameTexture.Null;

		/// <summary>
		/// Unload all umanaged resources for this material.
		/// </summary>
        public void UnloadContent()
        {
            DiffuseMap.Unload();
            SpecularMap.Unload();
            NormalMap.Unload();
            EmmisiveMap.Unload();
        }

		/// <summary>
		/// The diffuse color if it is used.
		/// 1.0f is a 255 value.
		/// </summary>
        public Vector4 DiffuseMat = Vector4.Zero;
		/// <summary>
		/// The specular color if it is used.
		/// 1.0f is a 255 value.
		/// </summary>
        public Vector3 SpecularMat = Vector3.Zero;

		/// <summary>
		/// Whether a diffuse material should be used.
		/// If used diffuse map won't be used.
		/// </summary>
        public bool UseDiffuseMat = false;
		/// <summary>
		/// Whether a specular material should be used.
		/// If used specular map won't be used.
		/// </summary>
        public bool UseSpecularMat = false;

		/// <summary>
		/// Whether alpha masking will be used.
		/// Significant performance impace be careful.
		/// </summary>
        public bool UseAlphaMask = false;
		/// <summary>
		/// The value of alpha to clip from.
		/// </summary>
        public float AlphaReference = 0.5f;

		/// <summary>
		/// The transformation of the texture coordinates.
		/// </summary>
        public Matrix TexTransform = Matrix.Identity;
    }

	/// <summary>
	/// A game object which is represented on screen and updates every frame.
	/// </summary>
    public interface GameObj : GameActor
    {
		/// <summary>
		/// Rotation of the object.
		/// </summary>
		Quaternion Rotation { get; set; }
		/// <summary>
		/// Uniform scaling of the object.
		/// </summary>
		float Scale { get; set; }

		/// <summary>
		/// Update the object.
		/// Called every frame by the manager.
		/// </summary>
		/// <param name="gameTime"></param>
        void Update(GameTime gameTime);
        GameObj Clone();

        RendererImpl.Mesh ToRendererMesh();
    }
}
