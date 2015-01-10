#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseLogic.Graphics
{
    /// <summary>
    /// Track which effect parameters need to be recomputed during the next OnApply.
    /// </summary>
    [Flags]
    public enum EffectDirtyFlags
    {
        /// <summary>
        /// The world view proj
        /// </summary>
        WorldViewProj = 1,

        /// <summary>
        /// The world
        /// </summary>
        World = 2,

        /// <summary>
        /// The eye position
        /// </summary>
        EyePosition = 4,

        /// <summary>
        /// The material color
        /// </summary>
        MaterialColor = 8,

        /// <summary>
        /// The fog
        /// </summary>
        Fog = 16,

        /// <summary>
        /// The fog enable
        /// </summary>
        FogEnable = 32,

        /// <summary>
        /// The alpha test
        /// </summary>
        AlphaTest = 64,

        /// <summary>
        /// The shader index
        /// </summary>
        ShaderIndex = 128,

        /// <summary>
        /// All
        /// </summary>
        All = -1
    }

    /// <summary>
    /// Custom effect for rendering skinned character models.
    /// </summary>
    public class CustomSkinnedEffect : Effect, IEffectMatrices, IEffectLights, IEffectFog
    {
        /// <summary>
        /// The maximum bones
        /// </summary>
        public const int MaxBones = 72;

        /// <summary>
        /// The bones parameter
        /// </summary>
        private EffectParameter bonesParam;

        /// <summary>
        /// The diffuse color parameter
        /// </summary>
        private EffectParameter diffuseColorParam;

        /// <summary>
        /// The emissive color parameter
        /// </summary>
        private EffectParameter emissiveColorParam;

        /// <summary>
        /// The eye position parameter
        /// </summary>
        private EffectParameter eyePositionParam;

        /// <summary>
        /// The fog color parameter
        /// </summary>
        private EffectParameter fogColorParam;

        /// <summary>
        /// The fog vector parameter
        /// </summary>
        private EffectParameter fogVectorParam;

        /// <summary>
        /// The shader index parameter
        /// </summary>
        private EffectParameter shaderIndexParam;

        /// <summary>
        /// The specular color parameter
        /// </summary>
        private EffectParameter specularColorParam;

        /// <summary>
        /// The specular power parameter
        /// </summary>
        private EffectParameter specularPowerParam;

        /// <summary>
        /// The texture parameter
        /// </summary>
        private EffectParameter textureParam;

        /// <summary>
        /// The world inverse transpose parameter
        /// </summary>
        private EffectParameter worldInverseTransposeParam;

        /// <summary>
        /// The world parameter
        /// </summary>
        private EffectParameter worldParam;

        /// <summary>
        /// The world view proj parameter
        /// </summary>
        private EffectParameter worldViewProjParam;

        /// <summary>
        /// The alpha
        /// </summary>
        private float alpha = 1;

        /// <summary>
        /// The ambient light color
        /// </summary>
        private Vector3 ambientLightColor = Vector3.Zero;

        /// <summary>
        /// The diffuse color
        /// </summary>
        private Vector3 diffuseColor = Vector3.One;

        /// <summary>
        /// The dirty flags
        /// </summary>
        private EffectDirtyFlags dirtyFlags = EffectDirtyFlags.All;

        /// <summary>
        /// The emissive color
        /// </summary>
        private Vector3 emissiveColor = Vector3.Zero;

        /// <summary>
        /// The fog enabled
        /// </summary>
        private bool fogEnabled;

        /// <summary>
        /// The fog end
        /// </summary>
        private float fogEnd = 1;

        /// <summary>
        /// The fog start
        /// </summary>
        private float fogStart = 0;

        /// <summary>
        /// The light0
        /// </summary>
        private DirectionalLight light0;

        /// <summary>
        /// The light1
        /// </summary>
        private DirectionalLight light1;

        /// <summary>
        /// The light2
        /// </summary>
        private DirectionalLight light2;

        /// <summary>
        /// The prefer per pixel lighting
        /// </summary>
        private bool preferPerPixelLighting;

        /// <summary>
        /// The projection
        /// </summary>
        private Matrix projection = Matrix.Identity;

        /// <summary>
        /// The view
        /// </summary>
        private Matrix view = Matrix.Identity;

        /// <summary>
        /// The weights per vertex
        /// </summary>
        private int weightsPerVertex = 4;

        /// <summary>
        /// The world
        /// </summary>
        private Matrix world = Matrix.Identity;

        /// <summary>
        /// The world view
        /// </summary>
        private Matrix worldView;

        /// <summary>
        /// Gets or sets the material alpha.
        /// </summary>
        /// <value>
        /// The alpha.
        /// </value>
        public float Alpha
        {
            get { return alpha; }

            set
            {
                alpha = value;
                dirtyFlags |= EffectDirtyFlags.MaterialColor;
            }
        }

        /// <summary>
        /// Gets or sets the ambient light color (range 0 to 1).
        /// </summary>
        public Vector3 AmbientLightColor
        {
            get { return ambientLightColor; }

            set
            {
                ambientLightColor = value;
                dirtyFlags |= EffectDirtyFlags.MaterialColor;
            }
        }

        /// <summary>
        /// Gets or sets the material diffuse color (range 0 to 1).
        /// </summary>
        /// <value>
        /// The color of the diffuse.
        /// </value>
        public Vector3 DiffuseColor
        {
            get { return diffuseColor; }

            set
            {
                diffuseColor = value;
                dirtyFlags |= EffectDirtyFlags.MaterialColor;
            }
        }

        /// <summary>
        /// Gets the first directional light.
        /// </summary>
        public DirectionalLight DirectionalLight0 { get { return light0; } }

        /// <summary>
        /// Gets the second directional light.
        /// </summary>
        public DirectionalLight DirectionalLight1 { get { return light1; } }

        /// <summary>
        /// Gets the third directional light.
        /// </summary>
        public DirectionalLight DirectionalLight2 { get { return light2; } }

        /// <summary>
        /// Gets or sets the material emissive color (range 0 to 1).
        /// </summary>
        /// <value>
        /// The color of the emissive.
        /// </value>
        public Vector3 EmissiveColor
        {
            get { return emissiveColor; }

            set
            {
                emissiveColor = value;
                dirtyFlags |= EffectDirtyFlags.MaterialColor;
            }
        }

        /// <summary>
        /// Gets or sets the fog color.
        /// </summary>
        public Vector3 FogColor
        {
            get { return fogColorParam.GetValueVector3(); }
            set { fogColorParam.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the fog enable flag.
        /// </summary>
        public bool FogEnabled
        {
            get { return fogEnabled; }

            set
            {
                if (fogEnabled != value)
                {
                    fogEnabled = value;
                    dirtyFlags |= EffectDirtyFlags.ShaderIndex | EffectDirtyFlags.FogEnable;
                }
            }
        }

        /// <summary>
        /// Gets or sets the fog end distance.
        /// </summary>
        public float FogEnd
        {
            get { return fogEnd; }

            set
            {
                fogEnd = value;
                dirtyFlags |= EffectDirtyFlags.Fog;
            }
        }

        /// <summary>
        /// Gets or sets the fog start distance.
        /// </summary>
        public float FogStart
        {
            get { return fogStart; }

            set
            {
                fogStart = value;
                dirtyFlags |= EffectDirtyFlags.Fog;
            }
        }

        /// <summary>
        /// This effect requires lighting, so we explicitly implement
        /// IEffectLights.LightingEnabled, and do not allow turning it off.
        /// </summary>
        /// <exception cref="System.NotSupportedException">CustomSkinnedEffect does not support setting LightingEnabled to false.</exception>
        bool IEffectLights.LightingEnabled
        {
            get { return true; }
            set { if (!value) throw new NotSupportedException("CustomSkinnedEffect does not support setting LightingEnabled to false."); }
        }

        /// <summary>
        /// Gets or sets the per-pixel lighting prefer flag.
        /// </summary>
        /// <value>
        /// <c>true</c> if [prefer per pixel lighting]; otherwise, <c>false</c>.
        /// </value>
        public bool PreferPerPixelLighting
        {
            get { return preferPerPixelLighting; }

            set
            {
                if (preferPerPixelLighting != value)
                {
                    preferPerPixelLighting = value;
                    dirtyFlags |= EffectDirtyFlags.ShaderIndex;
                }
            }
        }

        /// <summary>
        /// Gets or sets the projection matrix.
        /// </summary>
        public Matrix Projection
        {
            get { return projection; }

            set
            {
                projection = value;
                dirtyFlags |= EffectDirtyFlags.WorldViewProj;
            }
        }

        /// <summary>
        /// Gets or sets the material specular color (range 0 to 1).
        /// </summary>
        /// <value>
        /// The color of the specular.
        /// </value>
        public Vector3 SpecularColor
        {
            get { return specularColorParam.GetValueVector3(); }
            set { specularColorParam.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the material specular power.
        /// </summary>
        /// <value>
        /// The specular power.
        /// </value>
        public float SpecularPower
        {
            get { return specularPowerParam.GetValueSingle(); }
            set { specularPowerParam.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the current texture.
        /// </summary>
        /// <value>
        /// The texture.
        /// </value>
        public Texture2D Texture
        {
            get { return textureParam.GetValueTexture2D(); }
            set { textureParam.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the view matrix.
        /// </summary>
        public Matrix View
        {
            get { return view; }

            set
            {
                view = value;
                dirtyFlags |= EffectDirtyFlags.WorldViewProj | EffectDirtyFlags.EyePosition | EffectDirtyFlags.Fog;
            }
        }

        /// <summary>
        /// Gets or sets the number of skinning weights to evaluate for each vertex (1, 2, or 4).
        /// </summary>
        /// <value>
        /// The weights per vertex.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">value</exception>
        public int WeightsPerVertex
        {
            get { return weightsPerVertex; }

            set
            {
                if ((value != 1) &&
                    (value != 2) &&
                    (value != 4))
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                weightsPerVertex = value;
                dirtyFlags |= EffectDirtyFlags.ShaderIndex;
            }
        }

        /// <summary>
        /// Gets or sets the world matrix.
        /// </summary>
        public Matrix World
        {
            get { return world; }

            set
            {
                world = value;
                dirtyFlags |= EffectDirtyFlags.World | EffectDirtyFlags.WorldViewProj | EffectDirtyFlags.Fog;
            }
        }

        /// <summary>
        /// Gets a copy of the current skinning bone transform matrices.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">count</exception>
        public Matrix[] GetBoneTransforms(int count)
        {
            if (count <= 0 || count > MaxBones)
                throw new ArgumentOutOfRangeException("count");

            Matrix[] bones = bonesParam.GetValueMatrixArray(count);

            // Convert matrices from 43 to 44 format.
            for (int i = 0; i < bones.Length; i++)
            {
                bones[i].M44 = 1;
            }

            return bones;
        }

        /// <summary>
        /// Sets an array of skinning bone transform matrices.
        /// </summary>
        /// <param name="boneTransforms">The bone transforms.</param>
        /// <exception cref="System.ArgumentNullException">boneTransforms</exception>
        /// <exception cref="System.ArgumentException"></exception>
        public void SetBoneTransforms(Matrix[] boneTransforms)
        {
            if ((boneTransforms == null) || (boneTransforms.Length == 0))
                throw new ArgumentNullException("boneTransforms");

            if (boneTransforms.Length > MaxBones)
                throw new ArgumentException();

            bonesParam.SetValue(boneTransforms);
        }


        /// <summary>
        /// Creates a new CustomSkinnedEffect with default parameter settings.
        /// </summary>
        /// <param name="effect">The effect.</param>
        public CustomSkinnedEffect(Effect effect)
            : base(effect)
        {
            CacheEffectParameters(null);

            DirectionalLight0.Enabled = true;

            SpecularColor = Vector3.One;
            SpecularPower = 16;

            Matrix[] identityBones = new Matrix[MaxBones];

            for (int i = 0; i < MaxBones; i++)
            {
                identityBones[i] = Matrix.Identity;
            }

            SetBoneTransforms(identityBones);
        }

        /// <summary>
        /// Creates a new CustomSkinnedEffect by cloning parameter settings from an existing instance.
        /// </summary>
        /// <param name="cloneSource">The clone source.</param>
        protected CustomSkinnedEffect(CustomSkinnedEffect cloneSource)
            : base(cloneSource)
        {
            CacheEffectParameters(cloneSource);

            preferPerPixelLighting = cloneSource.preferPerPixelLighting;
            fogEnabled = cloneSource.fogEnabled;

            world = cloneSource.world;
            view = cloneSource.view;
            projection = cloneSource.projection;

            diffuseColor = cloneSource.diffuseColor;
            emissiveColor = cloneSource.emissiveColor;
            ambientLightColor = cloneSource.ambientLightColor;

            alpha = cloneSource.alpha;

            fogStart = cloneSource.fogStart;
            fogEnd = cloneSource.fogEnd;

            weightsPerVertex = cloneSource.weightsPerVertex;
        }

        /// <summary>
        /// Creates a clone of the current CustomSkinnedEffect instance.
        /// </summary>
        /// <returns></returns>
        public override Effect Clone()
        {
            return new CustomSkinnedEffect(this);
        }

        /// <summary>
        /// Copies parameters from an existing SkinnedEffect instance.
        /// </summary>
        /// <param name="cloneSource">The clone source.</param>
        public void CopyFromSkinnedEffect(SkinnedEffect cloneSource)
        {
            CacheEffectParametersFromSkinnedEffect(cloneSource);

            preferPerPixelLighting = cloneSource.PreferPerPixelLighting;
            fogEnabled = cloneSource.FogEnabled;

            world = cloneSource.World;
            view = cloneSource.View;
            projection = cloneSource.Projection;

            diffuseColor = cloneSource.DiffuseColor;
            emissiveColor = cloneSource.EmissiveColor;
            ambientLightColor = cloneSource.AmbientLightColor;

            alpha = cloneSource.Alpha;

            fogStart = cloneSource.FogStart;
            fogEnd = cloneSource.FogEnd;

            weightsPerVertex = cloneSource.WeightsPerVertex;

            Texture = cloneSource.Texture;
            SpecularColor = cloneSource.SpecularColor;
            SpecularPower = cloneSource.SpecularPower;
            FogColor = cloneSource.FogColor;

            eyePositionParam.SetValue(cloneSource.Parameters["EyePosition"].GetValueVector3());
            fogVectorParam.SetValue(cloneSource.Parameters["FogVector"].GetValueVector4());
            worldInverseTransposeParam.SetValue(cloneSource.Parameters["WorldInverseTranspose"].GetValueMatrix());
            bonesParam.SetValue(cloneSource.Parameters["Bones"].GetValueMatrixArray(MaxBones));
        }

        /// <summary>
        /// Sets up the standard key/fill/back lighting rig.
        /// </summary>
        public void EnableDefaultLighting()
        {
            AmbientLightColor = EnableDefaultLighting(light0, light1, light2);
        }

        /// <summary>
        /// Lazily computes derived parameter values immediately before applying the effect.
        /// </summary>
        protected override void OnApply()
        {
            // Recompute the world+view+projection matrix or fog vector?
            dirtyFlags = SetWorldViewProjAndFog(dirtyFlags, ref world, ref view, ref projection, ref worldView, fogEnabled, fogStart, fogEnd, worldViewProjParam, fogVectorParam);

            // Recompute the world inverse transpose and eye position?
            dirtyFlags = SetLightingMatrices(dirtyFlags, ref world, ref view, worldParam, worldInverseTransposeParam, eyePositionParam);

            // Recompute the diffuse/emissive/alpha material color parameters?
            if ((dirtyFlags & EffectDirtyFlags.MaterialColor) != 0)
            {
                SetMaterialColor(true, alpha, ref diffuseColor, ref emissiveColor, ref ambientLightColor, diffuseColorParam, emissiveColorParam);

                dirtyFlags &= ~EffectDirtyFlags.MaterialColor;
            }
        }

        /// <summary>
        /// Looks up shortcut references to our effect parameters.
        /// </summary>
        /// <param name="cloneSource">The clone source.</param>
        private void CacheEffectParameters(CustomSkinnedEffect cloneSource)
        {
            textureParam = Parameters["Texture"];
            diffuseColorParam = Parameters["DiffuseColor"];
            emissiveColorParam = Parameters["EmissiveColor"];
            specularColorParam = Parameters["SpecularColor"];
            specularPowerParam = Parameters["SpecularPower"];
            eyePositionParam = Parameters["EyePosition"];
            fogColorParam = Parameters["FogColor"];
            fogVectorParam = Parameters["FogVector"];
            worldParam = Parameters["World"];
            worldInverseTransposeParam = Parameters["WorldInverseTranspose"];
            worldViewProjParam = Parameters["WorldViewProj"];
            bonesParam = Parameters["Bones"];
            shaderIndexParam = Parameters["ShaderIndex"];

            light0 = new DirectionalLight(Parameters["DirLight0Direction"],
                                          Parameters["DirLight0DiffuseColor"],
                                          Parameters["DirLight0SpecularColor"],
                                          (cloneSource != null) ? cloneSource.light0 : null);

            light1 = new DirectionalLight(Parameters["DirLight1Direction"],
                                          Parameters["DirLight1DiffuseColor"],
                                          Parameters["DirLight1SpecularColor"],
                                          (cloneSource != null) ? cloneSource.light1 : null);

            light2 = new DirectionalLight(Parameters["DirLight2Direction"],
                                          Parameters["DirLight2DiffuseColor"],
                                          Parameters["DirLight2SpecularColor"],
                                          (cloneSource != null) ? cloneSource.light2 : null);
        }

        /// <summary>
        /// Looks up shortcut references to our effect parameters.
        /// </summary>
        /// <param name="cloneSource">The clone source.</param>
        private void CacheEffectParametersFromSkinnedEffect(SkinnedEffect cloneSource)
        {
            textureParam = Parameters["Texture"];
            diffuseColorParam = Parameters["DiffuseColor"];
            emissiveColorParam = Parameters["EmissiveColor"];
            specularColorParam = Parameters["SpecularColor"];
            specularPowerParam = Parameters["SpecularPower"];
            eyePositionParam = Parameters["EyePosition"];
            fogColorParam = Parameters["FogColor"];
            fogVectorParam = Parameters["FogVector"];
            worldParam = Parameters["World"];
            worldInverseTransposeParam = Parameters["WorldInverseTranspose"];
            worldViewProjParam = Parameters["WorldViewProj"];
            bonesParam = Parameters["Bones"];
            shaderIndexParam = Parameters["ShaderIndex"];

            light0 = new DirectionalLight(Parameters["DirLight0Direction"],
                                          Parameters["DirLight0DiffuseColor"],
                                          Parameters["DirLight0SpecularColor"],
                                          (cloneSource != null) ? cloneSource.DirectionalLight0 : null);

            light1 = new DirectionalLight(Parameters["DirLight1Direction"],
                                          Parameters["DirLight1DiffuseColor"],
                                          Parameters["DirLight1SpecularColor"],
                                          (cloneSource != null) ? cloneSource.DirectionalLight1 : null);

            light2 = new DirectionalLight(Parameters["DirLight2Direction"],
                                          Parameters["DirLight2DiffuseColor"],
                                          Parameters["DirLight2SpecularColor"],
                                          (cloneSource != null) ? cloneSource.DirectionalLight2 : null);
        }


        /// <summary>
        /// Sets up the standard key/fill/back lighting rig.
        /// </summary>
        /// <param name="light0">The light0.</param>
        /// <param name="light1">The light1.</param>
        /// <param name="light2">The light2.</param>
        /// <returns></returns>
        public virtual Vector3 EnableDefaultLighting(DirectionalLight light0, DirectionalLight light1, DirectionalLight light2)
        {
            // Key light.
            light0.Direction = new Vector3(-0.5265408f, -0.5735765f, -0.6275069f);
            light0.DiffuseColor = new Vector3(1, 0.9607844f, 0.8078432f);
            light0.SpecularColor = new Vector3(1, 0.9607844f, 0.8078432f);
            light0.Enabled = true;

            // Fill light.
            light1.Direction = new Vector3(0.7198464f, 0.3420201f, 0.6040227f);
            light1.DiffuseColor = new Vector3(0.9647059f, 0.7607844f, 0.4078432f);
            light1.SpecularColor = Vector3.Zero;
            light1.Enabled = true;

            // Back light.
            light2.Direction = new Vector3(0.4545195f, -0.7660444f, 0.4545195f);
            light2.DiffuseColor = new Vector3(0.3231373f, 0.3607844f, 0.3937255f);
            light2.SpecularColor = new Vector3(0.3231373f, 0.3607844f, 0.3937255f);
            light2.Enabled = true;

            // Ambient light.
            return new Vector3(0.05333332f, 0.09882354f, 0.1819608f);
        }

        /// <summary>
        /// Sets a vector which can be dotted with the object space vertex position to compute fog amount.
        /// </summary>
        /// <param name="worldView">The world view.</param>
        /// <param name="fogStart">The fog start.</param>
        /// <param name="fogEnd">The fog end.</param>
        /// <param name="fogVectorParam">The fog vector parameter.</param>
        public virtual void SetFogVector(ref Matrix worldView, float fogStart, float fogEnd, EffectParameter fogVectorParam)
        {
            if (fogStart == fogEnd)
            {
                // Degenerate case: force everything to 100% fogged if start and end are the same.
                fogVectorParam.SetValue(new Vector4(0, 0, 0, 1));
            }
            else
            {
                // We want to transform vertex positions into view space, take the resulting
                // Z value, then scale and offset according to the fog start/end distances.
                // Because we only care about the Z component, the shader can do all this
                // with a single dot product, using only the Z row of the world+view matrix.

                float scale = 1f / (fogStart - fogEnd);

                Vector4 fogVector = new Vector4();

                fogVector.X = worldView.M13 * scale;
                fogVector.Y = worldView.M23 * scale;
                fogVector.Z = worldView.M33 * scale;
                fogVector.W = (worldView.M43 + fogStart) * scale;

                fogVectorParam.SetValue(fogVector);
            }
        }

        /// <summary>
        /// Lazily recomputes the world inverse transpose matrix and
        /// eye position based on the current effect parameter settings.
        /// </summary>
        /// <param name="dirtyFlags">The dirty flags.</param>
        /// <param name="world">The world.</param>
        /// <param name="view">The view.</param>
        /// <param name="worldParam">The world parameter.</param>
        /// <param name="worldInverseTransposeParam">The world inverse transpose parameter.</param>
        /// <param name="eyePositionParam">The eye position parameter.</param>
        /// <returns></returns>
        public virtual EffectDirtyFlags SetLightingMatrices(EffectDirtyFlags dirtyFlags, ref Matrix world, ref Matrix view,
                                                             EffectParameter worldParam, EffectParameter worldInverseTransposeParam, EffectParameter eyePositionParam)
        {
            // Set the world and world inverse transpose matrices.
            if ((dirtyFlags & EffectDirtyFlags.World) != 0)
            {
                Matrix worldTranspose;
                Matrix worldInverseTranspose;

                Matrix.Invert(ref world, out worldTranspose);
                Matrix.Transpose(ref worldTranspose, out worldInverseTranspose);

                worldParam.SetValue(world);
                worldInverseTransposeParam.SetValue(worldInverseTranspose);

                dirtyFlags &= ~EffectDirtyFlags.World;
            }

            // Set the eye position.
            if ((dirtyFlags & EffectDirtyFlags.EyePosition) != 0)
            {
                Matrix viewInverse;

                Matrix.Invert(ref view, out viewInverse);

                eyePositionParam.SetValue(viewInverse.Translation);

                dirtyFlags &= ~EffectDirtyFlags.EyePosition;
            }

            return dirtyFlags;
        }

        /// <summary>
        /// Sets the diffuse/emissive/alpha material color parameters.
        /// </summary>
        /// <param name="lightingEnabled">if set to <c>true</c> [lighting enabled].</param>
        /// <param name="alpha">The alpha.</param>
        /// <param name="diffuseColor">Color of the diffuse.</param>
        /// <param name="emissiveColor">Color of the emissive.</param>
        /// <param name="ambientLightColor">Color of the ambient light.</param>
        /// <param name="diffuseColorParam">The diffuse color parameter.</param>
        /// <param name="emissiveColorParam">The emissive color parameter.</param>
        public virtual void SetMaterialColor(bool lightingEnabled, float alpha,
                                              ref Vector3 diffuseColor, ref Vector3 emissiveColor, ref Vector3 ambientLightColor,
                                              EffectParameter diffuseColorParam, EffectParameter emissiveColorParam)
        {
            // Desired lighting model:
            //
            //     ((AmbientLightColor + sum(diffuse directional light)) * DiffuseColor) + EmissiveColor
            //
            // When lighting is disabled, ambient and directional lights are ignored, leaving:
            //
            //     DiffuseColor + EmissiveColor
            //
            // For the lighting disabled case, we can save one shader instruction by precomputing
            // diffuse+emissive on the CPU, after which the shader can use DiffuseColor directly,
            // ignoring its emissive parameter.
            //
            // When lighting is enabled, we can merge the ambient and emissive settings. If we
            // set our emissive parameter to emissive+(ambient*diffuse), the shader no longer
            // needs to bother adding the ambient contribution, simplifying its computation to:
            //
            //     (sum(diffuse directional light) * DiffuseColor) + EmissiveColor
            //
            // For futher optimization goodness, we merge material alpha with the diffuse
            // color parameter, and premultiply all color values by this alpha.

            if (lightingEnabled)
            {
                Vector4 diffuse = new Vector4();
                Vector3 emissive = new Vector3();

                diffuse.X = diffuseColor.X * alpha;
                diffuse.Y = diffuseColor.Y * alpha;
                diffuse.Z = diffuseColor.Z * alpha;
                diffuse.W = alpha;

                emissive.X = (emissiveColor.X + ambientLightColor.X * diffuseColor.X) * alpha;
                emissive.Y = (emissiveColor.Y + ambientLightColor.Y * diffuseColor.Y) * alpha;
                emissive.Z = (emissiveColor.Z + ambientLightColor.Z * diffuseColor.Z) * alpha;

                diffuseColorParam.SetValue(diffuse);
                emissiveColorParam.SetValue(emissive);
            }
            else
            {
                Vector4 diffuse = new Vector4();

                diffuse.X = (diffuseColor.X + emissiveColor.X) * alpha;
                diffuse.Y = (diffuseColor.Y + emissiveColor.Y) * alpha;
                diffuse.Z = (diffuseColor.Z + emissiveColor.Z) * alpha;
                diffuse.W = alpha;

                diffuseColorParam.SetValue(diffuse);
            }
        }

        /// <summary>
        /// Lazily recomputes the world+view+projection matrix and
        /// fog vector based on the current effect parameter settings.
        /// </summary>
        /// <param name="dirtyFlags">The dirty flags.</param>
        /// <param name="world">The world.</param>
        /// <param name="view">The view.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="worldView">The world view.</param>
        /// <param name="fogEnabled">if set to <c>true</c> [fog enabled].</param>
        /// <param name="fogStart">The fog start.</param>
        /// <param name="fogEnd">The fog end.</param>
        /// <param name="worldViewProjParam">The world view proj parameter.</param>
        /// <param name="fogVectorParam">The fog vector parameter.</param>
        /// <returns></returns>
        public virtual EffectDirtyFlags SetWorldViewProjAndFog(EffectDirtyFlags dirtyFlags,
                                                                ref Matrix world, ref Matrix view, ref Matrix projection, ref Matrix worldView,
                                                                bool fogEnabled, float fogStart, float fogEnd,
                                                                EffectParameter worldViewProjParam, EffectParameter fogVectorParam)
        {
            // Recompute the world+view+projection matrix?
            if ((dirtyFlags & EffectDirtyFlags.WorldViewProj) != 0)
            {
                Matrix worldViewProj;

                Matrix.Multiply(ref world, ref view, out worldView);
                Matrix.Multiply(ref worldView, ref projection, out worldViewProj);

                worldViewProjParam.SetValue(worldViewProj);

                dirtyFlags &= ~EffectDirtyFlags.WorldViewProj;
            }

            if (fogEnabled)
            {
                // Recompute the fog vector?
                if ((dirtyFlags & (EffectDirtyFlags.Fog | EffectDirtyFlags.FogEnable)) != 0)
                {
                    SetFogVector(ref worldView, fogStart, fogEnd, fogVectorParam);

                    dirtyFlags &= ~(EffectDirtyFlags.Fog | EffectDirtyFlags.FogEnable);
                }
            }
            else
            {
                // When fog is disabled, make sure the fog vector is reset to zero.
                if ((dirtyFlags & EffectDirtyFlags.FogEnable) != 0)
                {
                    fogVectorParam.SetValue(Vector4.Zero);

                    dirtyFlags &= ~EffectDirtyFlags.FogEnable;
                }
            }

            return dirtyFlags;
        }
    }
}