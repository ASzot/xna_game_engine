//-----------------------------------------------------------------------------
// Code from...
//
// Jorge Adriano Luna
// http://jcoluna.wordpress.com
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem.RendererImpl
{
    /// <summary>
    /// In charge of rendering  the shadows for lights and storing the shadow entries.
    /// This includes shadow maps and light transforms.
    /// Uses PCF for spot lights and Cascading Shadow maps for directional lights.
    /// </summary>
    internal class ShadowRenderer
    {
        private static int _cascadeMapResolution = 1024;

        private static int _numCascadeSplits = 3;

        private static int i_maxNumCsmShadows = 1;

        private static int i_maxNumSpotShadows = 4;

        private static int i_spotShadowResolution = 512;

        private List<CascadeShadowMapEntry> _cascadeShadowMaps;

        private int _currentFreeCascadeShadowMap;

        private int _currentFreeSpotShadowMap;

        private Plane[] _directionalClippingPlanes;

        private Vector3[] _frustumCornersVS;

        private Vector3[] _frustumCornersWS;

        // Variables to help in the construction of cascading shadow maps.
        private float[] _splitDepthsTmp;

        private Vector3[] _splitFrustumCornersVS;

        private List<RegShadowMapEntry> _spotShadowMaps;

        private BoundingFrustum _tmpFrustum;

        private List<Mesh.SubMesh> _visibleMeshes;

        /// <summary>
        /// Setup the shadow map entries
        /// </summary>
        /// <param name="device">The graphics device.</param>
        public ShadowRenderer(GraphicsDevice device)
        {
            _splitDepthsTmp = new float[_numCascadeSplits + 1];
            _frustumCornersWS = new Vector3[8];
            _frustumCornersVS = new Vector3[8];
            _splitFrustumCornersVS = new Vector3[8];

            _directionalClippingPlanes = new Plane[6];
            _tmpFrustum = new BoundingFrustum(Matrix.Identity);

            _spotShadowMaps = new List<RegShadowMapEntry>();
            _cascadeShadowMaps = new List<CascadeShadowMapEntry>();
            _visibleMeshes = new List<Mesh.SubMesh>(100);

            //create the render targets
            for (int i = 0; i < i_maxNumSpotShadows; i++)
            {
                RegShadowMapEntry entry = new RegShadowMapEntry();
                //we store the linear depth, in a float render target. We need also the HW zbuffer
                entry.Texture = new RenderTarget2D(device, i_spotShadowResolution,
                                                   i_spotShadowResolution, false, SurfaceFormat.Single,
                                                   DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents);
                entry.LightViewProjection = Matrix.Identity;
                _spotShadowMaps.Add(entry);
            }
            for (int i = 0; i < i_maxNumCsmShadows; i++)
            {
                CascadeShadowMapEntry entry = new CascadeShadowMapEntry();
                entry.Texture = new RenderTarget2D(device, _cascadeMapResolution * _numCascadeSplits,
                                                   _cascadeMapResolution, false, SurfaceFormat.Single,
                                                   DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents);
                _cascadeShadowMaps.Add(entry);
            }
        }

        /// <summary>
        /// Create the shadow maps setting the specified values for shadow settings.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="csmRes"></param>
        /// <param name="csmDiv"></param>
        /// <param name="numSpotShadows"></param>
        /// <param name="numCsmShadows"></param>
        /// <param name="spotShadowRes"></param>
        public ShadowRenderer(GraphicsDevice device, int csmRes, int csmDiv, int numSpotShadows, int numCsmShadows, int spotShadowRes)
        {
            _cascadeMapResolution = csmRes;
            _numCascadeSplits = csmDiv;
            i_maxNumSpotShadows = numSpotShadows;
            i_maxNumCsmShadows = numCsmShadows;
            i_spotShadowResolution = spotShadowRes;

            _splitDepthsTmp = new float[_numCascadeSplits + 1];
            _frustumCornersWS = new Vector3[8];
            _frustumCornersVS = new Vector3[8];
            _splitFrustumCornersVS = new Vector3[8];

            _directionalClippingPlanes = new Plane[6];
            _tmpFrustum = new BoundingFrustum(Matrix.Identity);

            _spotShadowMaps = new List<RegShadowMapEntry>();
            _cascadeShadowMaps = new List<CascadeShadowMapEntry>();
            _visibleMeshes = new List<Mesh.SubMesh>(100);

            //create the render targets
            for (int i = 0; i < i_maxNumSpotShadows; i++)
            {
                RegShadowMapEntry entry = new RegShadowMapEntry();
                //we store the linear depth, in a float render target. We need also the HW zbuffer
                entry.Texture = new RenderTarget2D(device, i_spotShadowResolution,
                                                   i_spotShadowResolution, false, SurfaceFormat.Single,
                                                   DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents);
                entry.LightViewProjection = Matrix.Identity;
                _spotShadowMaps.Add(entry);
            }
            for (int i = 0; i < i_maxNumCsmShadows; i++)
            {
                CascadeShadowMapEntry entry = new CascadeShadowMapEntry();
                entry.Texture = new RenderTarget2D(device, _cascadeMapResolution * _numCascadeSplits,
                                                   _cascadeMapResolution, false, SurfaceFormat.Single,
                                                   DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents);
                _cascadeShadowMaps.Add(entry);
            }
        }

        /// <summary>
        /// The resolution of the cascade shadow map.
        /// </summary>
        public static int CascadeMapResolution
        {
            get { return _cascadeMapResolution; }
        }

        /// <summary>
        /// The max number of cascade shadow maps supported.
        /// </summary>
        public static int MaxNumCsmShadows
        {
            get { return i_maxNumCsmShadows; }
        }

        /// <summary>
        /// The max number of spot shadows supported.
        /// </summary>
        public static int MaxNumSpotShadows
        {
            get { return i_maxNumSpotShadows; }
        }

        /// <summary>
        /// The number of levels or resolution partitions the shadow map has.
        /// </summary>
        public static int NumCascadeSplits
        {
            get { return _numCascadeSplits; }
        }

        /// <summary>
        /// The resolution of the spot shadow maps.
        /// </summary>
        public static int SpotShadowResolution
        {
            get { return i_spotShadowResolution; }
        }

        /// <summary>
        /// Generate the cascade shadow map for a given directional light
        /// </summary>
        public void GenerateShadowTextureDirectionalLight(Renderer renderer, RenderController renderWorld, GeneralLight light, CascadeShadowMapEntry cascadeShadowMap, ICamera camera)
        {
            //bind the render target
            renderer.GraphicsDevice.SetRenderTarget(cascadeShadowMap.Texture);
            //clear it to white, ie, far far away
            renderer.GraphicsDevice.Clear(Color.White);
            renderer.GraphicsDevice.BlendState = BlendState.Opaque;
            renderer.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            // Get the corners of the frustum
            camera.Frustum.GetCorners(_frustumCornersWS);
            Matrix eyeTransform = camera.View;
            Vector3.Transform(_frustumCornersWS, ref eyeTransform, _frustumCornersVS);

            float near = camera.NearClip, far = MathHelper.Min(camera.FarClip, light.ShadowDistance);

            _splitDepthsTmp[0] = near;
            _splitDepthsTmp[_numCascadeSplits] = far;

            //compute each distance the way you like...
            for (int i = 1; i < _splitDepthsTmp.Length - 1; i++)
                _splitDepthsTmp[i] = near + (far - near) * (float)Math.Pow((i / (float)_numCascadeSplits), 1.6f);

            Viewport splitViewport = new Viewport();
            Vector3 lightDir = -Vector3.Normalize(light.Transform.Forward);

            for (int i = 0; i < _numCascadeSplits; i++)
            {
                cascadeShadowMap.LightClipPlanes[i].X = -_splitDepthsTmp[i];
                cascadeShadowMap.LightClipPlanes[i].Y = -_splitDepthsTmp[i + 1];

                cascadeShadowMap.LightViewProjectionMatrices[i] = CreateLightViewProjectionMatrix(lightDir, camera, _splitDepthsTmp[i], _splitDepthsTmp[i + 1]);
                Matrix viewProj = cascadeShadowMap.LightViewProjectionMatrices[i];
                _tmpFrustum.Matrix = viewProj;
                //we tweak the near plane, so keep all other planes
                _directionalClippingPlanes[0] = _tmpFrustum.Left;
                _directionalClippingPlanes[1] = _tmpFrustum.Right;
                _directionalClippingPlanes[2] = _tmpFrustum.Bottom;
                _directionalClippingPlanes[3] = _tmpFrustum.Top;
                _directionalClippingPlanes[4] = _tmpFrustum.Far;
                //the near clipping plane is set inside the CreateLightViewProjectionMatrix method, keep it

                // Set the viewport for the current split
                splitViewport.MinDepth = 0;
                splitViewport.MaxDepth = 1;
                splitViewport.Width = _cascadeMapResolution;
                splitViewport.Height = _cascadeMapResolution;
                splitViewport.X = i * _cascadeMapResolution;
                splitViewport.Y = 0;
                renderer.GraphicsDevice.Viewport = splitViewport;

                _tmpFrustum.Matrix = viewProj;

                _visibleMeshes.Clear();

                renderWorld.GetShadowCasters(_tmpFrustum, _directionalClippingPlanes, _visibleMeshes);
                for (int index = 0; index < _visibleMeshes.Count; index++)
                {
                    Mesh.SubMesh subMesh = _visibleMeshes[index];
                    //render it
                    subMesh.RenderShadowMap(ref viewProj, renderer.GraphicsDevice);
                }
            }
        }

        /// <summary>
        /// Create the shadow map for a spot light.
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="renderWorld"></param>
        /// <param name="light"></param>
        /// <param name="shadowMap"></param>
        public void GenerateShadowTextureSpotLight(Renderer renderer, RenderController renderWorld, GeneralLight light, RegShadowMapEntry shadowMap)
        {
            //bind the render target
            renderer.GraphicsDevice.SetRenderTarget(shadowMap.Texture);
            //clear it to white, ie, far far away
            renderer.GraphicsDevice.Clear(Color.White);
            renderer.GraphicsDevice.BlendState = BlendState.Opaque;
            renderer.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            Matrix viewProj = light.ViewProjection;
            shadowMap.LightViewProjection = viewProj;

            BoundingFrustum frustum = light.Frustum;

            _visibleMeshes.Clear();
            //cull meshes outside the light volume
            renderWorld.GetShadowCasters(frustum, _visibleMeshes);

            for (int index = 0; index < _visibleMeshes.Count; index++)
            {
                Mesh.SubMesh subMesh = _visibleMeshes[index];
                //render it
                subMesh.RenderShadowMap(ref viewProj, renderer.GraphicsDevice);
            }
        }

        /// <summary>
        /// Resets free shadow maps.
        /// Called at the begining of every frame.
        /// </summary>
        public void InitFrame()
        {
            _currentFreeSpotShadowMap = 0;
            _currentFreeCascadeShadowMap = 0;
        }

        /// <summary>
        /// Get an avaliable cascading shadow map avaliable for use.
        /// </summary>
        /// <returns></returns>
        internal CascadeShadowMapEntry GetFreeCascadeShadowMap()
        {
            if (_currentFreeCascadeShadowMap < _cascadeShadowMaps.Count)
            {
                return _cascadeShadowMaps[_currentFreeCascadeShadowMap++];
            }
            return null;
        }

        /// <summary>
        /// Get an avaliable spot shadow map for use.
        /// </summary>
        /// <returns></returns>
        internal RegShadowMapEntry GetFreeSpotShadowMap()
        {
            if (_currentFreeSpotShadowMap < _spotShadowMaps.Count)
            {
                return _spotShadowMaps[_currentFreeSpotShadowMap++];
            }
            return null;
        }

        /// <summary>
        /// Get the projection matrix for a direction light.
        /// </summary>
        /// <param name="lightDir">The direction of the direction light.</param>
        /// <param name="camera">The scene's camera.</param>
        /// <param name="minZ">The minimum clip plane.</param>
        /// <param name="maxZ">The maximum clip plane. This determines how far the directional shadows will go.</param>
        /// <returns>The generated projection matrix.</returns>
        private Matrix CreateLightViewProjectionMatrix(Vector3 lightDir, ICamera camera, float minZ, float maxZ)
        {
            for (int i = 0; i < 4; i++)
                _splitFrustumCornersVS[i] = _frustumCornersVS[i + 4] * (minZ / camera.FarClip);

            for (int i = 4; i < 8; i++)
                _splitFrustumCornersVS[i] = _frustumCornersVS[i] * (maxZ / camera.FarClip);

            Matrix cameraMat = camera.Transform;
            Vector3.Transform(_splitFrustumCornersVS, ref cameraMat, _frustumCornersWS);

            // Matrix with that will rotate in points the direction of the light
            Vector3 cameraUpVector = Vector3.Up;
            if (Math.Abs(Vector3.Dot(cameraUpVector, lightDir)) > 0.9f)
                cameraUpVector = Vector3.Forward;

            Matrix lightRotation = Matrix.CreateLookAt(Vector3.Zero,
                                                       -lightDir,
                                                       cameraUpVector);

            // Transform the positions of the corners into the direction of the light
            for (int i = 0; i < _frustumCornersWS.Length; i++)
            {
                _frustumCornersWS[i] = Vector3.Transform(_frustumCornersWS[i], lightRotation);
            }

            // Find the smallest box around the points
            Vector3 mins = _frustumCornersWS[0], maxes = _frustumCornersWS[0];
            for (int i = 1; i < _frustumCornersWS.Length; i++)
            {
                Vector3 p = _frustumCornersWS[i];
                if (p.X < mins.X) mins.X = p.X;
                if (p.Y < mins.Y) mins.Y = p.Y;
                if (p.Z < mins.Z) mins.Z = p.Z;
                if (p.X > maxes.X) maxes.X = p.X;
                if (p.Y > maxes.Y) maxes.Y = p.Y;
                if (p.Z > maxes.Z) maxes.Z = p.Z;
            }

            // Find the smallest box around the points in view space
            Vector3 minsVS = _splitFrustumCornersVS[0], maxesVS = _splitFrustumCornersVS[0];
            for (int i = 1; i < _splitFrustumCornersVS.Length; i++)
            {
                Vector3 p = _splitFrustumCornersVS[i];
                if (p.X < minsVS.X) minsVS.X = p.X;
                if (p.Y < minsVS.Y) minsVS.Y = p.Y;
                if (p.Z < minsVS.Z) minsVS.Z = p.Z;
                if (p.X > maxesVS.X) maxesVS.X = p.X;
                if (p.Y > maxesVS.Y) maxesVS.Y = p.Y;
                if (p.Z > maxesVS.Z) maxesVS.Z = p.Z;
            }
            BoundingBox _lightBox = new BoundingBox(mins, maxes);

            bool fixShadowJittering = true;
            if (fixShadowJittering)
            {
                // I borrowed this code from some forum that I don't remember anymore =/
                // We snap the camera to 1 pixel increments so that moving the camera does not cause the shadows to jitter.
                // This is a matter of integer dividing by the world space size of a texel
                float diagonalLength = (_frustumCornersWS[0] - _frustumCornersWS[6]).Length();
                diagonalLength += 2; //Without this, the shadow map isn't big enough in the world.
                float worldsUnitsPerTexel = diagonalLength / (float)_cascadeMapResolution;

                Vector3 vBorderOffset = (new Vector3(diagonalLength, diagonalLength, diagonalLength) -
                                         (_lightBox.Max - _lightBox.Min)) * 0.5f;
                _lightBox.Max += vBorderOffset;
                _lightBox.Min -= vBorderOffset;

                _lightBox.Min /= worldsUnitsPerTexel;
                _lightBox.Min.X = (float)Math.Floor(_lightBox.Min.X);
                _lightBox.Min.Y = (float)Math.Floor(_lightBox.Min.Y);
                _lightBox.Min.Z = (float)Math.Floor(_lightBox.Min.Z);
                _lightBox.Min *= worldsUnitsPerTexel;

                _lightBox.Max /= worldsUnitsPerTexel;
                _lightBox.Max.X = (float)Math.Floor(_lightBox.Max.X);
                _lightBox.Max.Y = (float)Math.Floor(_lightBox.Max.Y);
                _lightBox.Max.Z = (float)Math.Floor(_lightBox.Max.Z);
                _lightBox.Max *= worldsUnitsPerTexel;
            }

            Vector3 boxSize = _lightBox.Max - _lightBox.Min;
            if (boxSize.X == 0 || boxSize.Y == 0 || boxSize.Z == 0)
                boxSize = Vector3.One;
            Vector3 halfBoxSize = boxSize * 0.5f;

            // The position of the light should be in the center of the back
            // pannel of the box.
            Vector3 lightPosition = _lightBox.Min + halfBoxSize;
            lightPosition.Z = _lightBox.Min.Z;

            // We need the position back in world coordinates so we transform
            // the light position by the inverse of the lights rotation
            lightPosition = Vector3.Transform(lightPosition,
                                              Matrix.Invert(lightRotation));

            // Create the view matrix for the light
            Matrix lightView = Matrix.CreateLookAt(lightPosition,
                                                   lightPosition - lightDir,
                                                   cameraUpVector);

            // Create the projection matrix for the light
            // The projection is orthographic since we are using a directional light
            Matrix lightProjection = Matrix.CreateOrthographic(boxSize.X, boxSize.Y,
                                                               -boxSize.Z, 0);

            Vector3 lightDirVS = Vector3.TransformNormal(-lightDir, camera.View);
            //check if the light is in the same direction as the camera
            if (lightDirVS.Z > 0)
            {
                //use the far point as clipping plane
                Plane p = new Plane(-Vector3.Forward, maxesVS.Z);
                Plane.Transform(ref p, ref cameraMat, out _directionalClippingPlanes[5]);
            }
            else//lightDirVS.Z < 0
            {
                //use the closest point as clipping plane
                Plane p = new Plane(Vector3.Forward, minsVS.Z);
                Plane.Transform(ref p, ref cameraMat, out _directionalClippingPlanes[5]);
            }

            return lightView * lightProjection;
        }

        /// <summary>
        /// The cascading shadow entry used by directional lights.
        /// </summary>
        internal class CascadeShadowMapEntry
        {
            /// <summary>
            /// The various clip planes for the different levels of resolution.
            /// </summary>
            public Vector2[] LightClipPlanes;

            /// <summary>
            /// Transforms of relative 'partitions'
            /// </summary>
            public Matrix[] LightViewProjectionMatrices;

            /// <summary>
            /// Shadow map.
            /// </summary>
            public RenderTarget2D Texture;

            public CascadeShadowMapEntry()
            {
                LightViewProjectionMatrices = new Matrix[_numCascadeSplits];
                LightClipPlanes = new Vector2[_numCascadeSplits];
            }
        }

        /// <summary>
        /// The PCF shadow entry used by spot lights.
        /// </summary>
        internal class RegShadowMapEntry
        {
            /// <summary>
            /// Light transform.
            /// </summary>
            public Matrix LightViewProjection;

            /// <summary>
            /// Shadow map.
            /// </summary>
            public RenderTarget2D Texture;
        }
    }
}