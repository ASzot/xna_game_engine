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
using System.IO;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RenderingSystem
{
    /// <summary>
    /// In charge of loading game resources.
    /// Stores the content and device.
    /// </summary>
    public static class ResourceMgr
    {
        private const string SOUND_LOC = "sound/";
        private const string TEXTURE_LOC = "images/";
        private const string SKYMAP_LOC = "images/skymaps/";
        private const string SHADER_LOC = "shaders/";

        private static ContentManager _contentMgr = null;
        private static GraphicsDevice _device = null;


        /// <summary>
        /// Reference to the game's content manager.
        /// </summary>
        public static ContentManager Content
        {
            get { return _contentMgr; }
        }

        /// <summary>
        /// Reference to the game's graphics device.
        /// </summary>
        public static GraphicsDevice Device
        {
            get { return _device; }
        }

        /// <summary>
        /// Assign the content manager.
        /// </summary>
        /// <param name="contentMgr"></param>
		public static void SetContentMgr(ContentManager contentMgr)
		{
			_contentMgr = contentMgr;
		}

        /// <summary>
        /// Assign the graphics device.
        /// </summary>
        /// <param name="device"></param>
        public static void SetGraphicsDevice(GraphicsDevice device)
        {
            _device = device;
        }

        /// <summary>
        /// Loads a sound effect from the default sound folder.
        /// </summary>
        /// <param name="filename">The relative sound asset filename.</param>
        /// <returns>Loaded sound effect.</returns>
        public static SoundEffect LoadSoundEffect(string filename)
        {
            SoundEffect effect;
            try
            {
                effect = _contentMgr.Load<SoundEffect>(SOUND_LOC + filename);
            }
            catch (Exception)
            {
                return null;
            }

            return effect;
        }

        /// <summary>
        /// Loads a model from the absolute filename.
        /// </summary>
        /// <param name="filename">Absolute filename.</param>
        /// <returns>Loaded model.</returns>
		public static Model LoadModel(string filename)
		{
			Model model;
			try
			{
				model = _contentMgr.Load<Model>(filename);
			}
			catch (Exception)
			{
				return null;
			}
			return model;
		}

        /// <summary>
        /// Loads a song from the default sound folder.
        /// </summary>
        /// <param name="filename">The relative filename of the song.</param>
        /// <returns>Loaded song.</returns>
        public static Microsoft.Xna.Framework.Media.Song LoadSong(string filename)
        {
            var song = _contentMgr.Load<Microsoft.Xna.Framework.Media.Song>(SOUND_LOC + filename);

            return song;
        }

        /// <summary>
        /// Loads a game texture from the absolute filename location.
        /// </summary>
        /// <param name="absoluteFilename">The absolute asset filename.</param>
        /// <returns>The loaded game texture.</returns>
        public static GameTexture LoadGameTextureAbsoluteFilename(string absoluteFilename)
        {
            GameTexture gameTexture = new GameTexture(LoadTextureAbsoluteFilename(absoluteFilename), absoluteFilename);

            return gameTexture;
        }

        /// <summary>
        /// Loads a game texture from the default texture location.
        /// </summary>
        /// <param name="filename">The relative asset filename.</param>
        /// <returns>The loaded game texture.</returns>
        public static GameTexture LoadGameTexture(string filename)
        {
            GameTexture gameTexture = new GameTexture(LoadTexture(filename), TEXTURE_LOC + filename);

            return gameTexture;
        }

        /// <summary>
        /// Loads a texture cube from the default skymap location.
        /// </summary>
        /// <param name="filename">The relative asset filename.</param>
        /// <returns>The loaded texture cube.</returns>
        public static TextureCube LoadTextureCube(string filename)
        {
            TextureCube texture = _contentMgr.Load<TextureCube>(SKYMAP_LOC + filename);

            return texture;
        }

        /// <summary>
        /// Loads a effect from the default shader location.
        /// </summary>
        /// <param name="relativeFilename">The relative asset filename.</param>
        /// <returns>The loaded effect.</returns>
        public static Effect LoadEffect(string relativeFilename)
        {
            Effect effect = _contentMgr.Load<Effect>(SHADER_LOC + relativeFilename);

            return effect;
        }

        /// <summary>
        /// Loads a texture from the absolute filaname location.
        /// </summary>
        /// <param name="absoluteFilename">Absolute asset filename.</param>
        /// <returns>The loaded texture.</returns>
        public static Texture2D LoadTextureAbsoluteFilename(string absoluteFilename)
        {
            Texture2D texture;
            try
            {
                texture = _contentMgr.Load<Texture2D>(absoluteFilename);
            }
            catch (Exception)
            {
                return null;
            }

            return texture;
        }

        /// <summary>
        /// Loads a texture from the default texture location.
        /// </summary>
        /// <param name="relativeFilename">Relative asset filename.</param>
        /// <returns>Loaded texture.</returns>
        public static Texture2D LoadTexture(string relativeFilename)
        {
            Texture2D texture;
            try
            {
                texture = _contentMgr.Load<Texture2D>(TEXTURE_LOC + relativeFilename);
            }
            catch (Exception)
            {
                return null;
            }

            return texture;
        }
    }
}
