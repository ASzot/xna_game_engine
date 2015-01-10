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

using RenderingSystem.RendererImpl;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RenderingSystem
{
	/// <summary>
	/// A game particle system abstracting the renderable particle system.
	/// Allows transformations of the partice system.
	/// </summary>
    public class GameParticleSystem : GameActor
    {
		/// <summary>
		/// The particle system being rendered.
		/// </summary>
        protected ParticleSystem _particleSystem;

        private string s_actorID;
        private bool b_kill = false;
        private Vector3 v_pos = Vector3.Zero;
        private Quaternion q_rot = Quaternion.Identity;

        //TODO:
        // Make a base particle settings file.
        protected string _settingsFilename = "particles/FireSettings";

		/// <summary>
		/// The compounded transform of the particle system.
		/// </summary>
        public Matrix Transform
        {
            get 
            {
                // Someone can allow for scaling here as well if they are feeling like it.
                Matrix final = Matrix.CreateFromQuaternion(q_rot) * Matrix.CreateTranslation(v_pos);

                return final;
            }
        }

        public string ActorID
        {
            get { return s_actorID; }
            set { s_actorID = value; }
        }

        public bool Kill
        {
            get { return b_kill; }
            set { b_kill = value; }
        }

        public Vector3 Position
        {
            get { return v_pos; }
            set { v_pos = value; }
        }

        public Quaternion Rotation
        {
            get { return q_rot; }
            set { q_rot = value; }
        }

		public bool Enabled
		{
			get { return _particleSystem.Enabled; }
			set { _particleSystem.Enabled = value; }
		}

		/// <summary>
		/// The renderable particle system.
		/// </summary>
        public ParticleSystem ParticleSystem
        {
            get
            {
                _particleSystem.GlobalTransform = Transform;

                return _particleSystem;
            }
        }

        public GameParticleSystem(string id)
        {
            s_actorID = id;
        }

        public GameParticleSystem()
            : this(Guid.NewGuid().ToString())
        {

        }

		/// <summary>
		/// Whether the renderalbe particle system has been loaded into memory.
		/// </summary>
		/// <returns></returns>
        public bool IsParticleSystemSet()
        {
            return _particleSystem != null;
        }

		/// <summary>
		/// Set the particle settings for the renderable particle system.
		/// </summary>
		/// <param name="content"></param>
		/// <param name="device"></param>
		/// <param name="particleSettings"></param>
        public void SetInfo(ContentManager content, GraphicsDevice device, ParticleSettings particleSettings)
        {
            _particleSystem = new ParticleSystem(device, content, particleSettings);
        }

		/// <summary>
		/// Gets the information for the renderable particle system.
		/// </summary>
		/// <returns></returns>
        public ParticleSettings GetInfo()
        {
            return _particleSystem.Settings;
        }

		/// <summary>
		/// Loads the renderable particle system.
		/// Uses the default particle settings file.
		/// </summary>
		/// <param name="content"></param>
		/// <param name="device"></param>
        public virtual void LoadContent(ContentManager content, GraphicsDevice device)
        {
            _particleSystem = new ParticleSystem(device, content, _settingsFilename);
        }

		/// <summary>
		/// Loads the renderable particle system.
		/// Uses the specified settings file.
		/// </summary>
		/// <param name="content"></param>
		/// <param name="device"></param>
		/// <param name="filename"></param>
        public virtual void LoadContent(ContentManager content, GraphicsDevice device, string filename)
        {
            _particleSystem = new ParticleSystem(device, content, filename);
        }

		/// <summary>
		/// Updates the renderable particle system.
		/// </summary>
		/// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _particleSystem.Update(dt);
        }
    }

}
