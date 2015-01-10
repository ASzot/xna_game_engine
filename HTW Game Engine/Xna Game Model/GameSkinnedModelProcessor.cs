//-----------------------------------------------------------------------------
//Based on
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
// http://create.msdn.com/en-US/education/catalog/sample/skinned_model
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Adapted from the code by...
// Jorge Adriano Luna
// http://jcoluna.wordpress.com
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Graphics;
using RenderingSystem.RendererImpl;
using SkinnedModel;

namespace Xna_Game_Model
{
    /// <summary>
    /// Custom processor extends the builtin framework ModelProcessor class,
    /// adding animation support.
    /// </summary>
    [ContentProcessor(DisplayName = "Xna Game Skinned Model")]
    public class GameSkinnedModelProcessor : GameProcessor
    {
        /// <summary>
        /// Gets or sets a value indicating whether [process physics].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [process physics]; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(false)]
        [DisplayName("Process Physics")]
        [Description("This needs to be set false for animated objects.")]
        public override bool ProcessPhysics
        {
            get
            {
                return base.ProcessPhysics;
            }
            set
            {
                base.ProcessPhysics = value;
            }
        }

        /// <summary>
        /// The main Process method converts an intermediate format content pipeline
        /// NodeContent tree to a ModelContent object with embedded animation data.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        /// <exception cref="Microsoft.Xna.Framework.Content.Pipeline.InvalidContentException">
        /// Input skeleton not found.
        /// or
        /// </exception>
        public override ModelContent Process(NodeContent input,
                                             ContentProcessorContext context)
        {
            _processPhysics = false;

            ValidateMesh(input, context, null);

            // Find the skeleton.
            BoneContent skeleton = MeshHelper.FindSkeleton(input);

            if (skeleton == null)
                throw new InvalidContentException("Input skeleton not found.");

            _isSkinned = true;
            // We don't want to have to worry about different parts of the model being
            // in different local coordinate systems, so let's just bake everything.
            FlattenTransforms(input, skeleton);

            // Read the bind pose and skeleton hierarchy data.
            IList<BoneContent> bones = MeshHelper.FlattenSkeleton(skeleton);

            if (bones.Count > SkinnedEffect.MaxBones)
            {
                throw new InvalidContentException(string.Format(
                    "Skeleton has {0} bones, but the maximum supported is {1}.",
                    bones.Count, SkinnedEffect.MaxBones));
            }

            List<Matrix> bindPose = new List<Matrix>();
            List<Matrix> inverseBindPose = new List<Matrix>();
            List<int> skeletonHierarchy = new List<int>();

            foreach (BoneContent bone in bones)
            {
                Matrix m = bone.Transform;
                //scale all translations
                m.Translation = m.Translation * Scale;
                bone.Transform = m;

                bindPose.Add(bone.Transform);
                inverseBindPose.Add(Matrix.Invert(bone.AbsoluteTransform));
                skeletonHierarchy.Add(bones.IndexOf(bone.Parent as BoneContent));
            }

            // Convert animation data to our runtime format.
            Dictionary<string, AnimationClip> animationClips;
            animationClips = ProcessAnimations(skeleton.Animations, bones);

            // Chain to the base ModelProcessor class so it can convert the model data.
            ModelContent model = base.Process(input, context);

            // Store our custom animation data in the Tag property of the model.
            MeshData metadata = model.Tag as MeshData;
            metadata.SkinningData = new SkinningData(animationClips, bindPose,
                                         inverseBindPose, skeletonHierarchy);

            return model;
        }

        /// <summary>
        /// Comparison function for sorting keyframes into ascending time order.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        private static int CompareKeyframeTimes(Keyframe a, Keyframe b)
        {
            return a.Time.CompareTo(b.Time);
        }

        /// <summary>
        /// Bakes unwanted transforms into the model geometry,
        /// so everything ends up in the same coordinate system.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="skeleton">The skeleton.</param>
        private static void FlattenTransforms(NodeContent node, BoneContent skeleton)
        {
            foreach (NodeContent child in node.Children)
            {
                // Don't process the skeleton, because that is special.
                if (child == skeleton)
                    continue;

                // Bake the local transform into the actual geometry.
                MeshHelper.TransformScene(child, child.Transform);

                // Having baked it, we can now set the local
                // coordinate system back to identity.
                child.Transform = Matrix.Identity;

                // Recurse.
                FlattenTransforms(child, skeleton);
            }
        }

        /// <summary>
        /// Checks whether a mesh contains skininng information.
        /// </summary>
        /// <param name="mesh">The mesh.</param>
        /// <returns></returns>
        private static bool MeshHasSkinning(MeshContent mesh)
        {
            foreach (GeometryContent geometry in mesh.Geometry)
            {
                if (!geometry.Vertices.Channels.Contains(VertexChannelNames.Weights()))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Makes sure this mesh contains the kind of data we know how to animate.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="context">The context.</param>
        /// <param name="parentBoneName">Name of the parent bone.</param>
        private static void ValidateMesh(NodeContent node, ContentProcessorContext context,
                                 string parentBoneName)
        {
            MeshContent mesh = node as MeshContent;

            if (mesh != null)
            {
                // Validate the mesh.
                if (parentBoneName != null)
                {
                    context.Logger.LogWarning(null, null,
                        "Mesh {0} is a child of bone {1}. SkinnedModelProcessor " +
                        "does not correctly handle meshes that are children of bones.",
                        mesh.Name, parentBoneName);
                }

                if (!MeshHasSkinning(mesh))
                {
                    context.Logger.LogWarning(null, null,
                        "Mesh {0} has no skinning information, so it has been deleted.",
                        mesh.Name);

                    mesh.Parent.Children.Remove(mesh);
                    return;
                }
            }
            else if (node is BoneContent)
            {
                // If this is a bone, remember that we are now looking inside it.
                parentBoneName = node.Name;
            }

            // Recurse (iterating over a copy of the child collection,
            // because validating children may delete some of them).
            foreach (NodeContent child in new List<NodeContent>(node.Children))
                ValidateMesh(child, context, parentBoneName);
        }

        /// <summary>
        /// Converts an intermediate format content pipeline AnimationContent
        /// object to our runtime AnimationClip format.
        /// </summary>
        /// <param name="animation">The animation.</param>
        /// <param name="boneMap">The bone map.</param>
        /// <returns></returns>
        /// <exception cref="Microsoft.Xna.Framework.Content.Pipeline.InvalidContentException">
        /// Animation has no keyframes.
        /// or
        /// Animation has a zero duration.
        /// </exception>
        private AnimationClip ProcessAnimation(AnimationContent animation,
                                              Dictionary<string, int> boneMap)
        {
            List<Keyframe> keyframes = new List<Keyframe>();

            // For each input animation channel.
            foreach (KeyValuePair<string, AnimationChannel> channel in
                animation.Channels)
            {
                // Look up what bone this channel is controlling.
                int boneIndex;

                if (!boneMap.TryGetValue(channel.Key, out boneIndex))
                {
                    throw new InvalidContentException(string.Format(
                        "Found animation for bone '{0}', " +
                        "which is not part of the skeleton.", channel.Key));
                }

                // Convert the keyframe data.
                foreach (AnimationKeyframe keyframe in channel.Value)
                {
                    Matrix m = keyframe.Transform;
                    m.Translation = m.Translation * Scale;
                    keyframe.Transform = m;

                    keyframes.Add(new Keyframe(boneIndex, keyframe.Time,
                                               keyframe.Transform));
                }
            }

            // Sort the merged keyframes by time.
            keyframes.Sort(CompareKeyframeTimes);

            if (keyframes.Count == 0)
                throw new InvalidContentException("Animation has no keyframes.");

            if (animation.Duration <= TimeSpan.Zero)
                throw new InvalidContentException("Animation has a zero duration.");

            return new AnimationClip(animation.Duration, keyframes);
        }

        /// <summary>
        /// Converts an intermediate format content pipeline AnimationContentDictionary
        /// object to our runtime AnimationClip format.
        /// </summary>
        /// <param name="animations">The animations.</param>
        /// <param name="bones">The bones.</param>
        /// <returns></returns>
        /// <exception cref="Microsoft.Xna.Framework.Content.Pipeline.InvalidContentException">Input file does not contain any animations.</exception>
        private Dictionary<string, AnimationClip> ProcessAnimations(
            AnimationContentDictionary animations, IList<BoneContent> bones)
        {
            // Build up a table mapping bone names to indices.
            Dictionary<string, int> boneMap = new Dictionary<string, int>();

            for (int i = 0; i < bones.Count; i++)
            {
                string boneName = bones[i].Name;

                if (!string.IsNullOrEmpty(boneName))
                    boneMap.Add(boneName, i);
            }

            // Convert each animation in turn.
            Dictionary<string, AnimationClip> animationClips;
            animationClips = new Dictionary<string, AnimationClip>();

            foreach (KeyValuePair<string, AnimationContent> animation in animations)
            {
                AnimationClip processed = ProcessAnimation(animation.Value, boneMap);

                animationClips.Add(animation.Key, processed);
            }

            if (animationClips.Count == 0)
            {
                throw new InvalidContentException(
                            "Input file does not contain any animations.");
            }

            return animationClips;
        }
    }
}