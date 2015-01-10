#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using BaseLogic.Core;
using Henge3D;
using Henge3D.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RenderingSystem;
using RenderingSystem.RendererImpl;
using SkinnedModel;

namespace BaseLogic
{
    /// <summary>
    ///
    /// </summary>
    public struct AnimationSpan
    {
        /// <summary>
        /// The animation end
        /// </summary>
        public TimeSpan AnimationEnd;

        /// <summary>
        /// The animation start
        /// </summary>
        public TimeSpan AnimationStart;

        /// <summary>
        /// The anim offset
        /// </summary>
        public Vector3 AnimOffset;

        /// <summary>
        /// The name
        /// </summary>
        public string Name;

        /// <summary>
        /// The repeat
        /// </summary>
        public bool Repeat;

        /// <summary>
        /// The s_targeted model
        /// </summary>
        private string s_targetedModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationSpan"/> struct.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="name">The name.</param>
        /// <param name="targetModel">The target model.</param>
        /// <param name="repeat">if set to <c>true</c> [repeat].</param>
        public AnimationSpan(TimeSpan start, TimeSpan end, string name, string targetModel, bool repeat)
            : this(start, end, name, targetModel, repeat, Vector3.Zero)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationSpan"/> struct.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="name">The name.</param>
        /// <param name="targetModel">The target model.</param>
        /// <param name="repeat">if set to <c>true</c> [repeat].</param>
        /// <param name="animOffset">The anim offset.</param>
        public AnimationSpan(TimeSpan start, TimeSpan end, string name, string targetModel, bool repeat, Vector3 animOffset)
        {
            this.AnimationStart = start;
            this.AnimationEnd = end;
            this.Repeat = repeat;
            this.Name = name;
            this.s_targetedModel = targetModel;
            this.AnimOffset = animOffset;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationSpan"/> struct.
        /// </summary>
        /// <param name="frameStart">The frame start.</param>
        /// <param name="frameEnd">The frame end.</param>
        /// <param name="framerate">The framerate.</param>
        /// <param name="name">The name.</param>
        /// <param name="targetModel">The target model.</param>
        /// <param name="repeat">if set to <c>true</c> [repeat].</param>
        /// <param name="animOffset">The anim offset.</param>
        public AnimationSpan(int frameStart, int frameEnd, int framerate, string name, string targetModel, bool repeat, Vector3 animOffset)
        {
            //TODO:
            // Fix this it doesn't work.

            // Convert the frames to a timespan.
            double secondsStart = (double)frameStart / (double)framerate;
            int seconds = secondsStart.GetWholePart();
            int milliseconds = secondsStart.GetDecimalPart();

            this.AnimationStart = new TimeSpan(0, 0, 0, seconds, milliseconds);

            double secondsEnd = (double)frameEnd / (double)framerate;
            seconds = secondsEnd.GetWholePart();
            milliseconds = secondsEnd.GetDecimalPart() / 100000;

            this.AnimationEnd = new TimeSpan(0, 0, 0, seconds, milliseconds);

            this.Name = name;
            this.s_targetedModel = targetModel;
            this.Repeat = repeat;
            this.AnimOffset = animOffset;
        }

        /// <summary>
        /// Gets or sets the targeted model.
        /// </summary>
        /// <value>
        /// The targeted model.
        /// </value>
        public string TargetedModel
        {
            get { return s_targetedModel; }
            set { s_targetedModel = value.Replace("//", "/"); }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public struct BBRayTraceInfo
    {
        /// <summary>
        /// The point depth
        /// </summary>
        public float PointDepth;

        /// <summary>
        /// The points
        /// </summary>
        public Vector2[] Points;

        /// <summary>
        /// Transforms the points.
        /// </summary>
        /// <param name="pointPairs">The point pairs.</param>
        /// <param name="quat">The quat.</param>
        /// <returns></returns>
        public static TypePair<Vector3>[] TransformPoints(TypePair<Vector3>[] pointPairs, Quaternion quat)
        {
            var transformedPoints = from p in pointPairs
                                    select new TypePair<Vector3>(
                                        Vector3.Transform(p.Data1, quat),
                                        Vector3.Transform(p.Data2, quat));

            return transformedPoints.ToArray();
        }

        /// <summary>
        /// Transforms the points.
        /// </summary>
        /// <param name="pointPairs">The point pairs.</param>
        /// <param name="mat">The mat.</param>
        /// <returns></returns>
        public static TypePair<Vector3>[] TransformPoints(TypePair<Vector3>[] pointPairs, Matrix mat)
        {
            var transformedPoints = from p in pointPairs
                                    select new TypePair<Vector3>(
                                        Vector3.Transform(p.Data1, mat),
                                        Vector3.Transform(p.Data2, mat));

            return transformedPoints.ToArray();
        }

        /// <summary>
        /// Projects the points to raycasting.
        /// </summary>
        /// <param name="depthFactor">The depth factor.</param>
        /// <returns></returns>
        public TypePair<Vector3>[] ProjectPointsToRaycasting(float depthFactor)
        {
            float pointDepth = PointDepth;
            var projectedPoints = from p in Points
                                  select new TypePair<Vector3>(
                                      new Vector3(p.X, p.Y, pointDepth),
                                      new Vector3(p.X, p.Y, depthFactor + pointDepth));
            return projectedPoints.ToArray();
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class AnimatedObj : PhysObj
    {
        /// <summary>
        /// The applied rot
        /// </summary>
        public Quaternion AppliedRot = Quaternion.Identity;

        /// <summary>
        /// The bb maximum
        /// </summary>
        public Vector3 BBMax = Vector3.Zero;

        /// <summary>
        /// The bb minimum
        /// </summary>
        public Vector3 BBMin = Vector3.Zero;

        /// <summary>
        /// The _animating
        /// </summary>
        private bool _animating = true;

        /// <summary>
        /// The _animation player
        /// </summary>
        private AnimationPlayer _animationPlayer;

        /// <summary>
        /// The _animation play index
        /// </summary>
        private int _animationPlayIndex = 0;

        /// <summary>
        /// The _anim spans
        /// </summary>
        private List<AnimationSpan> _animSpans = new List<AnimationSpan>();

        /// <summary>
        /// The _bbrti
        /// </summary>
        private BBRayTraceInfo _bbrti;

        /// <summary>
        /// The _clipname
        /// </summary>
        private string _clipname;

        /// <summary>
        /// The _current play time
        /// </summary>
        private TimeSpan _currentPlayTime = TimeSpan.Zero;

        /// <summary>
        /// The _filename
        /// </summary>
        private string _filename;

        /// <summary>
        /// The _skinned mesh
        /// </summary>
        private SkinnedMesh _skinnedMesh;

        /// <summary>
        /// The _subset materials
        /// </summary>
        private List<SubsetMaterial> _subsetMaterials;

        /// <summary>
        /// The b_serilize object
        /// </summary>
        private bool b_serilizeObj = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimatedObj"/> class.
        /// </summary>
        /// <param name="actorID">The actor identifier.</param>
        public AnimatedObj(string actorID)
            : base(actorID)
        {
            _bbrti.Points = new Vector2[4];

            _animationPlayIndex = -1;
            _animating = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AnimatedObj"/> is animating.
        /// </summary>
        /// <value>
        ///   <c>true</c> if animating; otherwise, <c>false</c>.
        /// </value>
        public bool Animating
        {
            get { return _animating; }
            set { _animating = value; }
        }

        /// <summary>
        /// Gets the bb ray trace information.
        /// </summary>
        /// <value>
        /// The bb ray trace information.
        /// </value>
        public BBRayTraceInfo BBRayTraceInfo
        {
            get { return _bbrti; }
        }

        /// <summary>
        /// Gets the clipname.
        /// </summary>
        /// <value>
        /// The clipname.
        /// </value>
        public string Clipname
        {
            get { return _clipname; }
        }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        /// <value>
        /// The filename.
        /// </value>
        public string Filename
        {
            get { return _filename; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [serilize object].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [serilize object]; otherwise, <c>false</c>.
        /// </value>
        public bool SerilizeObj
        {
            get { return b_serilizeObj; }
            set { b_serilizeObj = value; }
        }

        /// <summary>
        /// Gets or sets the subset materials.
        /// </summary>
        /// <value>
        /// The subset materials.
        /// </value>
        public List<SubsetMaterial> SubsetMaterials
        {
            get { return _subsetMaterials; }
            set { _subsetMaterials = value; }
        }

        /// <summary>
        /// Gets the subset world matricies.
        /// </summary>
        /// <value>
        /// The subset world matricies.
        /// </value>
        public Matrix[] SubsetWorldMatricies
        {
            get
            {
                var worldMatricies = from sm in _skinnedMesh.SubMeshes
                                     select sm.World;
                return worldMatricies.ToArray();
            }
        }

        /// <summary>
        /// Adds the animation span.
        /// </summary>
        /// <param name="animSpan">The anim span.</param>
        public void AddAnimationSpan(AnimationSpan animSpan)
        {
            _animSpans.Add(animSpan);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override GameObj Clone()
        {
            AnimatedObj animObj = new AnimatedObj(Guid.NewGuid().ToString());
            animObj.LoadContent(GameSystem.GameSys_Instance.Content, _filename, _clipname);
            animObj.SetPhysicsData(BBMin, BBMax);
            animObj.Position = Position;
            animObj.Rotation = Rotation;
            animObj.Scale = Scale;
            animObj.SerilizeObj = SerilizeObj;
            animObj.Animating = Animating;
            animObj.SubsetMaterials = _subsetMaterials;

            return animObj;
        }

        /// <summary>
        /// Gets the name of the animation.
        /// </summary>
        /// <returns></returns>
        public string GetAnimationName()
        {
            if (_animationPlayIndex == -1)
                return "";

            return _animSpans.ElementAt(_animationPlayIndex).Name;
        }

        /// <summary>
        /// Gets the animation span.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Animation span of name  + name.ToString() +  does not exist.</exception>
        public AnimationSpan GetAnimationSpan(string name)
        {
            foreach (AnimationSpan animSpan in _animSpans)
            {
                if (animSpan.Name == name)
                    return animSpan;
            }

            throw new ArgumentException("Animation span of name " + name.ToString() + " does not exist.");
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="animClipName">Name of the anim clip.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid model processor used.</exception>
        public bool LoadContent(ContentManager content, string filename, string animClipName)
        {
            _clipname = animClipName;
            _skinnedMesh = new SkinnedMesh();
            _filename = filename;

            _model = ResourceMgr.LoadModel(filename);
            if (_model == null)
                return false;
            _skinnedMesh.SetModel(_model);
            if (!(_model.Tag is MeshData))
                throw new ArgumentException("Invalid model processor used.");

            _skinnedMesh.SetMetadata(_model.Tag as MeshData);
            _animationPlayer = new AnimationPlayer(_skinnedMesh.SkinningData);

            _animationPlayer.StartClip(_skinnedMesh.SkinningData.AnimationClips[animClipName]);

            _subsetMaterials = new List<SubsetMaterial>(_skinnedMesh.SubMeshes.Count);
            for (int i = 0; i < _skinnedMesh.SubMeshes.Count; ++i)
            {
                _subsetMaterials.Add(new SubsetMaterial());
            }

            return true;
        }

        /// <summary>
        /// Sets the physics data.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        public void SetPhysicsData(Vector3 min, Vector3 max)
        {
            BBMin = min;
            BBMax = max;

            BoundingBox bb = new BoundingBox(min, max);
            Vector3[] corners = bb.GetCorners();

            ConvexHull3D hull = new ConvexHull3D(corners);
            CompiledPolyhedron compiled = hull.ToPolyhedron();

            PolyhedronPart polyPart = new PolyhedronPart(compiled);
            _rigidBody = new GameRigidBody(ActorID);
            _rigidBody.Skin.Add(polyPart, new Henge3D.Physics.Material(0f, 0.5f));

            const float density = 50f;
            Vector3 dimensions = max - min;

            _rigidBody.MassProperties = MassProperties.FromCuboid(density * 10, dimensions);

            const float bufferFactor = 1.0f;
            max *= (bufferFactor);
            min *= (bufferFactor);
            _bbrti.PointDepth = max.Z;
            _bbrti.Points[0] = new Vector2(min.X, min.Y);
            _bbrti.Points[1] = new Vector2(max.X, min.Y);
            _bbrti.Points[2] = new Vector2(min.X, max.Y);
            _bbrti.Points[3] = new Vector2(max.X, max.Y);
        }

        /// <summary>
        /// Switches the animations.
        /// </summary>
        /// <param name="animName">Name of the anim.</param>
        /// <exception cref="System.ArgumentException">Invalid animation clip span name!</exception>
        public void SwitchAnimations(string animName)
        {
            for (int i = 0; i < _animSpans.Count; ++i)
            {
                AnimationSpan animSpan = _animSpans.ElementAt(i);
                if (animSpan.Name == animName)
                {
                    SwitchAnimations(i);
                    return;
                }
            }

            throw new ArgumentException("Invalid animation clip span name!");
        }

        /// <summary>
        /// Switches the animations.
        /// </summary>
        /// <param name="newAnimatingIndex">New index of the animating.</param>
        public void SwitchAnimations(int newAnimatingIndex)
        {
            if (newAnimatingIndex == -1)
            {
                _animationPlayIndex = -1;
                _animating = false;
                return;
            }

            _animationPlayIndex = newAnimatingIndex;
            AnimationSpan animSpan = _animSpans.ElementAt(_animationPlayIndex);
            _currentPlayTime = animSpan.AnimationStart;
            _animating = true;
        }

        /// <summary>
        /// To the renderer mesh.
        /// </summary>
        /// <returns></returns>
        public override Mesh ToRendererMesh()
        {
            Quaternion rot = Rotation * AppliedRot;
            Matrix trans = Matrix.CreateTranslation(Position);
            Matrix scale = Matrix.CreateScale(Scale);

            Matrix transform = scale * Matrix.CreateFromQuaternion(rot) * trans;

            _skinnedMesh.Transform = transform;

            Vector3 transformedMax = Vector3.Transform(BBMax, transform);
            Vector3 transformedMin = Vector3.Transform(BBMin, transform);

            transformedMax = BBMax;
            transformedMin = BBMin;

            _skinnedMesh.PhysicsBoundingBox = new BoundingBox(transformedMin, transformedMax);

            _skinnedMesh.SubsetMaterials = _subsetMaterials.ToArray();

            return _skinnedMesh;
        }

        /// <summary>
        /// Update the object.
        /// Called every frame by the manager.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Matrix final = _rigidBody.Transform.Combined;

            TimeSpan restingStateTimeFrame = TimeSpan.Zero;

            if (!_animating)
            {
                _animationPlayer.Update(_currentPlayTime, false, Matrix.Identity);
                _skinnedMesh.BoneMatrixes = _animationPlayer.GetSkinTransforms();
            }
            else
            {
                AnimationSpan animSpan = _animSpans.ElementAt(_animationPlayIndex);
                _currentPlayTime += gameTime.ElapsedGameTime;
                if (_currentPlayTime >= animSpan.AnimationEnd)
                {
                    _currentPlayTime = animSpan.AnimationEnd - new TimeSpan(0, 0, 0, 0, 1);
                    SwitchAnimations(animSpan.Repeat ? _animationPlayIndex : -1);
                    return;
                }

                _animationPlayer.Update(_currentPlayTime, false, Matrix.Identity);
                _skinnedMesh.BoneMatrixes = _animationPlayer.GetSkinTransforms();
            }
        }
    }
}