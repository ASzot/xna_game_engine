//-----------------------------------------------------------------------------
// Adapted from the code by...
// Jorge Adriano Luna
// http://jcoluna.wordpress.com
//-----------------------------------------------------------------------------


using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Graphics;

namespace Xna_Game_Model
{
    /// <summary>
    /// Processes a texture for the game. Be aware it has to be set under the files preferences.
    /// </summary>
    [ContentProcessor(DisplayName = "Xna Game Texture")]
    public class GameTextureProcessor : TextureProcessor
    {
        /// <summary>
        /// The b_is cubemap
        /// </summary>
        private bool b_isCubemap = false;

        /// <summary>
        /// Specifies whether color keying of a texture is enabled.
        /// </summary>
        public override bool ColorKeyEnabled
        {
            get { return false; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is cubemap.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is cubemap; otherwise, <c>false</c>.
        /// </value>
        public bool IsCubemap
        {
            get { return b_isCubemap; }
            set { b_isCubemap = value; }
        }

        /// <summary>
        /// Specifies whether alpha premultiply of textures is enabled.
        /// </summary>
        public override bool PremultiplyAlpha
        {
            get { return false; }
        }

        /// <summary>
        /// Processes a texture.
        /// </summary>
        /// <param name="input">The texture content to process.</param>
        /// <param name="context">Context for the specified processor.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">input</exception>
        public override TextureContent Process(TextureContent input, ContentProcessorContext context)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            Console.WriteLine("Xna game texture processor: " + context.OutputFilename);

            // Do we have a normal map?
            if (context.OutputFilename.Contains("normal"))
            {
                TextureFormat = TextureProcessorOutputFormat.Color;
            }
            else
            {
                TextureFormat = TextureProcessorOutputFormat.DxtCompressed;
            }

            // Always use mipmaps.
            GenerateMipmaps = true;

            // Get the information associated with the texture.
            FileInfo fileInfo = new FileInfo(Path.GetDirectoryName(input.Identity.SourceFilename) + "\\" + Path.GetFileNameWithoutExtension(input.Identity.SourceFilename) + ".metadata");
            if (fileInfo.Exists)
            {
                using (FileStream fileStream = fileInfo.OpenRead())
                {
                    StreamReader streamReader = new StreamReader(fileStream);
                    while (!streamReader.EndOfStream)
                    {
                        string line = streamReader.ReadLine();
                        ParseMetaData(line);
                    }
                }
            }

            // Do we need to continue to get the cubemap data?
            if (b_isCubemap)
            {
                return GenerateCubemap(input, context);
            }
            return base.Process(input, context);
        }

        /// <summary>
        /// Creates the face.
        /// </summary>
        /// <param name="bitmapContent">Content of the bitmap.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        /// <param name="xOffset">The x offset.</param>
        /// <returns></returns>
        private MipmapChain CreateFace(BitmapContent bitmapContent, int w, int h, int xOffset)
        {
            PixelBitmapContent<Color> result;

            result = new PixelBitmapContent<Color>(w, h);

            Rectangle sourceRegion = new Rectangle(xOffset, 0, w, h);

            Rectangle destinationRegion = new Rectangle(0, 0, w, h);

            BitmapContent.Copy(bitmapContent, sourceRegion, result, destinationRegion);

            return result;
        }

        /// <summary>
        /// Generates the cubemap.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        private TextureContent GenerateCubemap(TextureContent input, ContentProcessorContext context)
        {
            if (input.Faces[1].Count != 0)
            {
                // We already have all the sides of our cubemap processed. 
                return base.Process(input, context);
            }


            TextureCubeContent cubeContent = new TextureCubeContent();
            // Convert the input data to Color format, for ease of processing.
            input.ConvertBitmapType(typeof(PixelBitmapContent<Color>));

            int height = input.Faces[0][0].Height;
            int width = input.Faces[0][0].Width / 6;

            // The image is composed of the 6 sides: X+,X-, Y+,Y-, Z+, Z-
            cubeContent.Faces[(int)CubeMapFace.PositiveX] = CreateFace(input.Faces[0][0], width, height, 0);
            cubeContent.Faces[(int)CubeMapFace.NegativeX] = CreateFace(input.Faces[0][0], width, height, width * 1);
            cubeContent.Faces[(int)CubeMapFace.PositiveY] = CreateFace(input.Faces[0][0], width, height, width * 2);
            cubeContent.Faces[(int)CubeMapFace.NegativeY] = CreateFace(input.Faces[0][0], width, height, width * 3);
            cubeContent.Faces[(int)CubeMapFace.PositiveZ] = CreateFace(input.Faces[0][0], width, height, width * 4);
            cubeContent.Faces[(int)CubeMapFace.NegativeZ] = CreateFace(input.Faces[0][0], width, height, width * 5);

            // Calculate mipmap data.
            cubeContent.GenerateMipmaps(true);

            // Compress the cubemap into DXT1 format.
            cubeContent.ConvertBitmapType(typeof(Dxt1BitmapContent));
            return cubeContent;
        }

        /// <summary>
        /// Parses the meta data.
        /// </summary>
        /// <param name="line">The line.</param>
        private void ParseMetaData(string line)
        {
            if (line.Contains("TextureFormat"))
            {
                string format = line.Replace("TextureFormat=", "");
                if (format == "Color")
                    TextureFormat = TextureProcessorOutputFormat.Color;
                else if (format == "DxtCompressed")
                    TextureFormat = TextureProcessorOutputFormat.DxtCompressed;
            }
            else if (line.Contains("TextureType"))
            {
                string textureType = line.Replace("TextureType=", "");
                if (textureType.Contains("Cubemap"))
                {
                    b_isCubemap = true;
                }
            }
            else if (line.Contains("NoMipMaps"))
            {
                GenerateMipmaps = false;
            }
        }
    }
}