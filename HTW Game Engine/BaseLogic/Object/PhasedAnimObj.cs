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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RenderingSystem;
using RenderingSystem.RendererImpl;
using SkinnedModel;

namespace BaseLogic.Object
{
    /// <summary>
    ///
    /// </summary>
    public class PhasedAnimObj : GameObj
    {
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
        /// The _model
        /// </summary>
        private Model _model;

        /// <summary>
        /// The _skinned mesh
        /// </summary>
        private SkinnedMesh _skinnedMesh;

        /// <summary>
        /// The _subset materials
        /// </summary>
        private List<SubsetMaterial> _subsetMaterials;

        /// <summary>
        /// The b_kill
        /// </summary>
        private bool b_kill = false;

        /// <summary>
        /// The b_serilize object
        /// </summary>
        private bool b_serilizeObj = true;

        /// <summary>
        /// The d_animation speed
        /// </summary>
        private double d_animationSpeed = 1.0;

        /// <summary>
        /// The f_scale
        /// </summary>
        private float f_scale = 1f;

        /// <summary>
        /// The q_rot
        /// </summary>
        private Quaternion q_rot = Quaternion.Identity;

        /// <summary>
        /// The s_actor identifier
        /// </summary>
        private string s_actorID;

        /// <summary>
        /// The v_pos
        /// </summary>
        private Vector3 v_pos = Vector3.Zero;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhasedAnimObj"/> class.
        /// </summary>
        /// <param name="actorID">The actor identifier.</param>
        public PhasedAnimObj(string actorID)
        {
            s_actorID = actorID;

            _bbrti.Points = new Vector2[4];

            _animationPlayIndex = -1;
            _animating = false;
        }

        /// <summary>
        /// The unique identification of the actor.
        /// </summary>
        public string ActorID
        {
            get { return s_actorID; }
            set { s_actorID = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PhasedAnimObj"/> is animating.
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
        /// Gets or sets the animation play time.
        /// </summary>
        /// <value>
        /// The animation play time.
        /// </value>
        public TimeSpan AnimationPlayTime
        {
            get { return _currentPlayTime; }
            set { _currentPlayTime = value; }
        }

        /// <summary>
        /// Gets or sets the animation speed.
        /// </summary>
        /// <value>
        /// The animation speed.
        /// </value>
        public double AnimationSpeed
        {
            get { return d_animationSpeed; }
            set { d_animationSpeed = value; }
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
            get { return f_scale; }
            set { f_scale = value; }
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
        /// Gets the transform.
        /// </summary>
        /// <value>
        /// The transform.
        /// </value>
        public Matrix Transform
        {
            get
            {
                Matrix translation = Matrix.CreateTranslation(v_pos);
                Matrix rot = Matrix.CreateFromQuaternion(q_rot);
                Matrix scale = Matrix.CreateScale(f_scale);

                return scale * rot * translation;
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
        public GameObj Clone()
        {
            PhasedAnimObj phasedAnimObj = new PhasedAnimObj(Guid.NewGuid().ToString());
            phasedAnimObj.LoadContent(Filename, _clipname);
            phasedAnimObj.Scale = f_scale;
            phasedAnimObj.Position = v_pos;
            phasedAnimObj.Rotation = q_rot;
            phasedAnimObj.SerilizeObj = b_serilizeObj;
            phasedAnimObj.Animating = this.Animating;
            phasedAnimObj.SubsetMaterials = _subsetMaterials;

            return phasedAnimObj;
        }

        /// <summary>
        /// Gets the animation span.
        /// </summary>
        /// <param name="animName">Name of the anim.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid animation span!</exception>
        public AnimationSpan GetAnimationSpan(string animName)
        {
            foreach (AnimationSpan animSpan in _animSpans)
            {
                if (animSpan.Name == animName)
                    return animSpan;
            }

            throw new ArgumentException("Invalid animation span!");
        }

        /// <summary>
        /// Gets the bone transform.
        /// </summary>
        /// <param name="boneName">Name of the bone.</param>
        /// <returns></returns>
        public Matrix GetBoneTransform(string boneName)
        {
            return _skinnedMesh.GetCompleteBoneTransform(boneName);
        }

        /// <summary>
        /// Gets the name of the current animation span.
        /// </summary>
        /// <returns></returns>
        public string GetCurrentAnimationSpanName()
        {
            if (_animationPlayIndex == -1)
                return null;

            AnimationSpan animSpan = _animSpans.ElementAt(_animationPlayIndex);
            return animSpan.Name;
        }

        /// <summary>
        /// Gets the current anim span.
        /// </summary>
        /// <returns></returns>
        public AnimationSpan GetCurrentAnimSpan()
        {
            return _animSpans.ElementAt(_animationPlayIndex);
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="animClipName">Name of the anim clip.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid model processor used.</exception>
        public bool LoadContent(string filename, string animClipName)
        {
            _clipname = animClipName;
            _filename = filename;
            _skinnedMesh = new SkinnedMesh();

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
        public virtual Mesh ToRendererMesh()
        {
            _skinnedMesh.Transform = this.Transform;
            _skinnedMesh.SubsetMaterials = _subsetMaterials.ToArray();

            return _skinnedMesh;
        }

        /// <summary>
        /// Update the object.
        /// Called every frame by the manager.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            Matrix final = this.Transform;

            TimeSpan restingStateTimeFrame = TimeSpan.Zero;

            if (!_animating)
            {
                _animationPlayer.Update(_currentPlayTime, false, Matrix.Identity);
                _skinnedMesh.BoneMatrixes = _animationPlayer.GetSkinTransforms();
            }
            else
            {
                AnimationSpan animSpan = _animSpans.ElementAt(_animationPlayIndex);

                double elapsedSeconds = gameTime.ElapsedGameTime.TotalSeconds;
                elapsedSeconds *= d_animationSpeed;
                TimeSpan animationUpdateTime = TimeSpan.FromSeconds(elapsedSeconds);

                _currentPlayTime += animationUpdateTime;
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