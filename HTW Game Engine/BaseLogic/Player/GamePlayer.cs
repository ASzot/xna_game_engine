#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using Microsoft.Xna.Framework;
using RenderingSystem;

namespace BaseLogic.Player
{
    /// <summary>
    ///
    /// </summary>
    public enum PlayerMessageType
    {
        /// <summary>
        /// The inflict damage
        /// </summary>
        InflictDamage,
    };

    /// <summary>
    ///
    /// </summary>
    public struct PlayerMessage
    {
        /// <summary>
        /// The sender
        /// </summary>
        public GamePlayer Sender;

        /// <summary>
        /// The type
        /// </summary>
        public PlayerMessageType Type;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerMessage"/> struct.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="type">The type.</param>
        public PlayerMessage(GamePlayer sender, PlayerMessageType type)
        {
            this.Sender = sender;
            this.Type = type;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class GamePlayer : GameActor
    {
        /// <summary>
        /// The p_ player MGR
        /// </summary>
        public static Manager.PlayerMgr p_PlayerMgr;

        /// <summary>
        /// The raytrace function
        /// </summary>
        public static Func<Manager.RaytraceFireInfo, Manager.RaytraceIntersectionInfo> RaytraceFunc;

        /// <summary>
        /// The _weapon
        /// </summary>
        protected Object.WeaponObj _weapon;

        /// <summary>
        /// The b_invicible
        /// </summary>
        protected bool b_invicible = false;

        /// <summary>
        /// The b_kill
        /// </summary>
        protected bool b_kill = false;

        /// <summary>
        /// The f_health
        /// </summary>
        protected float f_health = 100f;

        /// <summary>
        /// The f_max health
        /// </summary>
        protected float f_maxHealth = 100f;

        /// <summary>
        /// The f_max speed
        /// </summary>
        protected float f_maxSpeed = 2f;

        /// <summary>
        /// The f_y ray fire offset
        /// </summary>
        protected float f_yRayFireOffset = 4f;

        /// <summary>
        /// The F_Z ray distance
        /// </summary>
        protected float f_zRayDistance = 1000f;

        /// <summary>
        /// The F_Z ray start
        /// </summary>
        protected float f_zRayStart = 5f;

        /// <summary>
        /// The i_gun subset index
        /// </summary>
        protected int i_gunSubsetIndex;

        /// <summary>
        /// The p_game object
        /// </summary>
        protected PhysObj p_gameObj;

        /// <summary>
        /// The s_actor identifier
        /// </summary>
        protected string s_actorID;

        /// <summary>
        /// The s_last aggressor identifier
        /// </summary>
        protected string s_lastAggressorId = null;

        /// <summary>
        /// The v_gun shot offset
        /// </summary>
        protected Vector3 v_gunShotOffset;

        /// <summary>
        /// The _current time
        /// </summary>
        private TimeSpan _currentTime = TimeSpan.Zero;

        /// <summary>
        /// The _max time
        /// </summary>
        private TimeSpan _maxTime = TimeSpan.FromSeconds(3.0);

        /// <summary>
        /// Initializes a new instance of the <see cref="GamePlayer"/> class.
        /// </summary>
        /// <param name="actorID">The actor identifier.</param>
        public GamePlayer(string actorID)
        {
            s_actorID = actorID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GamePlayer"/> class.
        /// </summary>
        public GamePlayer()
            : this(Guid.NewGuid().ToString())
        {
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
        /// Gets or sets the name of the animation clip.
        /// </summary>
        /// <value>
        /// The name of the animation clip.
        /// </value>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        public string AnimationClipName
        {
            get
            {
                if (p_gameObj == null)
                    throw new ArgumentException();
                if (!(p_gameObj is AnimatedObj))
                    throw new ArgumentException();
                return (p_gameObj as AnimatedObj).GetAnimationName();
            }
            set
            {
                if (p_gameObj != null && p_gameObj is AnimatedObj)
                {
                    if (value == "")
                        (p_gameObj as AnimatedObj).SwitchAnimations(-1);
                    else if (value != AnimationClipName)
                        (p_gameObj as AnimatedObj).SwitchAnimations(value);
                }
            }
        }

        /// <summary>
        /// Gets the game object.
        /// </summary>
        /// <value>
        /// The game object.
        /// </value>
        public PhysObj GameObj
        {
            get { return p_gameObj; }
        }

        /// <summary>
        /// Gets the game object identifier.
        /// </summary>
        /// <value>
        /// The game object identifier.
        /// </value>
        public string GameObjID
        {
            get { return p_gameObj.ActorID; }
        }

        /// <summary>
        /// Gets or sets the gun shot offset.
        /// </summary>
        /// <value>
        /// The gun shot offset.
        /// </value>
        public Vector3 GunShotOffset
        {
            get { return v_gunShotOffset; }
            set { v_gunShotOffset = value; }
        }

        /// <summary>
        /// Gets or sets the gun subset.
        /// </summary>
        /// <value>
        /// The gun subset.
        /// </value>
        public int GunSubset
        {
            get { return i_gunSubsetIndex; }
            set { i_gunSubsetIndex = value; }
        }

        /// <summary>
        /// Gets or sets the health.
        /// </summary>
        /// <value>
        /// The health.
        /// </value>
        public float Health
        {
            get { return f_health; }
            set
            {
                if (!b_invicible)
                    f_health = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GamePlayer"/> is invicible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if invicible; otherwise, <c>false</c>.
        /// </value>
        public bool Invicible
        {
            get { return b_invicible; }
            set { b_invicible = value; }
        }

        /// <summary>
        /// Setting to true will remove the object from the manager in charge of it.
        /// </summary>
        public bool Kill
        {
            get { return b_kill; }
            set
            {
                b_kill = value;
                if (p_gameObj != null)
                    p_gameObj.Kill = true;
            }
        }

        /// <summary>
        /// Gets or sets the maximum health.
        /// </summary>
        /// <value>
        /// The maximum health.
        /// </value>
        public float MaxHealth
        {
            get { return f_maxHealth; }
            set { f_maxHealth = value; }
        }

        /// <summary>
        /// Gets or sets the maximum speed.
        /// </summary>
        /// <value>
        /// The maximum speed.
        /// </value>
        public float MaxSpeed
        {
            get { return f_maxSpeed; }
            set { f_maxSpeed = value; }
        }

        /// <summary>
        /// Gets the object bb ray trace information.
        /// </summary>
        /// <value>
        /// The object bb ray trace information.
        /// </value>
        /// <exception cref="System.InvalidOperationException">
        /// </exception>
        public BBRayTraceInfo ObjBBRayTraceInfo
        {
            get
            {
                if (p_gameObj == null)
                    throw new InvalidOperationException();
                if (!(p_gameObj is AnimatedObj))
                    throw new InvalidOperationException();
                return (p_gameObj as AnimatedObj).BBRayTraceInfo;
            }
        }

        /// <summary>
        /// The position of the object in the 3D scene.
        /// </summary>
        public Vector3 Position
        {
            get { return p_gameObj.Position; }
            set { p_gameObj.Position = value; }
        }

        /// <summary>
        /// Gets the speed.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        public float Speed
        {
            get
            {
                // Don't include the Y velocity ( which is only going to be gravity ).
                Vector3 vel = GetVelocity();
                Vector2 vel2D = new Vector2(vel.X, vel.Z);
                return vel2D.Length();
            }
        }

        /// <summary>
        /// Gets or sets the weapon object.
        /// </summary>
        /// <value>
        /// The weapon object.
        /// </value>
        public Object.WeaponObj WeaponObj
        {
            get { return _weapon; }
            set { _weapon = value; }
        }

        /// <summary>
        /// Gets or sets the y ray fire offset.
        /// </summary>
        /// <value>
        /// The y ray fire offset.
        /// </value>
        public float YRayFireOffset
        {
            get { return f_yRayFireOffset; }
            set { f_yRayFireOffset = value; }
        }

        /// <summary>
        /// Gets or sets the z ray distance.
        /// </summary>
        /// <value>
        /// The z ray distance.
        /// </value>
        public float ZRayDistance
        {
            get { return f_zRayDistance; }
            set { f_zRayDistance = value; }
        }

        /// <summary>
        /// Gets or sets the z ray start.
        /// </summary>
        /// <value>
        /// The z ray start.
        /// </value>
        public float ZRayStart
        {
            get { return f_zRayStart; }
            set { f_zRayStart = value; }
        }

        /// <summary>
        /// Determines whether this instance [can no longer shoot] the specified victim.
        /// </summary>
        /// <param name="victim">The victim.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Raytrace didn't work</exception>
        public bool CanNoLongerShoot(GamePlayer victim)
        {
            // Is the player in front of us?
            Vector3 victimRelativeTo = victim.Position - Position;
            Vector3 headed = GetDirection();
            headed.Z *= -1f;
            float dot = Vector3.Dot(headed, victimRelativeTo);

            // The dot product will be positive if in front, negative if behind, and zero if on the side.
            if (dot < 0f)
                return false;

            Matrix rot = Matrix.CreateFromQuaternion(GetRotation()) * Matrix.CreateRotationY(MathHelper.Pi);

            Matrix transformMat = rot * Matrix.CreateTranslation(Position);
            Vector3 start = new Vector3(0f, f_yRayFireOffset, f_zRayStart);

            Vector3 victimFirePos = victim.Position;
            victimFirePos.Y += 0f;
            Vector3 end = victimFirePos;

            start = Vector3.Transform(start, transformMat);

            Manager.RaytraceIntersectionInfo raytraceIntersection = RaytraceFunc(new Manager.RaytraceFireInfo(start, end));
            if (raytraceIntersection == null)
            {
                throw new ArgumentException("Raytrace didn't work");
            }
            else
            {
                GameRigidBody gameRigidBody = raytraceIntersection.IntersectionBody as GameRigidBody;
                if (gameRigidBody.Name != victim.p_gameObj.ActorID)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether this instance can see the specified victim.
        /// </summary>
        /// <param name="victim">The victim.</param>
        /// <returns></returns>
        public bool CanSee(GamePlayer victim)
        {
            // Is the player in front of us?
            Vector3 victimRelativeTo = victim.Position - Position;
            Vector3 headed = GetDirection();
            headed.Z *= -1f;
            float dot = Vector3.Dot(headed, victimRelativeTo);

            // The dot product will be positive if in front, negative if behind, and zero if on the side.
            if (dot < 0f)
                return false;

            Matrix rot = Matrix.CreateFromQuaternion(GetRotation()) * Matrix.CreateRotationY(MathHelper.Pi);

            Matrix transformMat = rot * Matrix.CreateTranslation(Position);
            Vector3 start = new Vector3(0f, f_yRayFireOffset, f_zRayStart);

            Vector3 victimFirePos = victim.Position;
            victimFirePos.Y += 0f;
            Vector3 end = victimFirePos;

            start = Vector3.Transform(start, transformMat);

            Manager.RaytraceIntersectionInfo raytraceIntersection = RaytraceFunc(new Manager.RaytraceFireInfo(start, end));
            if (raytraceIntersection == null)
            {
                return false;
            }
            else
            {
                GameRigidBody gameRigidBody = raytraceIntersection.IntersectionBody as GameRigidBody;
                if (gameRigidBody.Name != victim.p_gameObj.ActorID)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether this instance can shoot the specified victim.
        /// </summary>
        /// <param name="victim">The victim.</param>
        /// <returns></returns>
        public bool CanShoot(GamePlayer victim)
        {
            if (!CanSee(victim))
                return false;

            return true;
        }

        /// <summary>
        /// Distances the sq to.
        /// </summary>
        /// <param name="gameActor">The game actor.</param>
        /// <returns></returns>
        public float DistanceSqTo(GameActor gameActor)
        {
            return DistanceSqTo(gameActor.Position);
        }

        /// <summary>
        /// Distances the sq to.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <returns></returns>
        public float DistanceSqTo(Vector3 pos)
        {
            return Vector3.DistanceSquared(this.Position, pos);
        }

        /// <summary>
        /// Distances to.
        /// </summary>
        /// <param name="gameActor">The game actor.</param>
        /// <returns></returns>
        public float DistanceTo(GameActor gameActor)
        {
            return DistanceTo(gameActor.Position);
        }

        /// <summary>
        /// Distances to.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <returns></returns>
        public float DistanceTo(Vector3 pos)
        {
            return Vector3.Distance(this.Position, pos);
        }

        /// <summary>
        /// Gets the aggressor.
        /// </summary>
        /// <returns></returns>
        public GamePlayer GetAggressor()
        {
            if (s_lastAggressorId == null)
                return null;

            return p_PlayerMgr.GetPlayerOfId(s_lastAggressorId);
        }

        /// <summary>
        /// Gets the animation span.
        /// </summary>
        /// <param name="animSpanName">Name of the anim span.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public AnimationSpan GetAnimationSpan(string animSpanName)
        {
            if (!(p_gameObj is AnimatedObj))
                throw new ArgumentException();

            AnimatedObj animObj = p_gameObj as AnimatedObj;
            AnimationSpan animSpan = animObj.GetAnimationSpan(animSpanName);

            return animSpan;
        }

        /// <summary>
        /// Gets the direction.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public Vector3 GetDirection()
        {
            if (p_gameObj == null)
                throw new ArgumentException();

            Vector3 dir = Vector3.UnitZ;
            Matrix rotMat = Matrix.CreateFromQuaternion(p_gameObj.Rotation);
            dir = Vector3.TransformNormal(dir, rotMat);

            //dir.Z = -dir.Z;
            dir.X = -dir.X;

            return dir;
        }

        /// <summary>
        /// Gets the hand transform.
        /// </summary>
        /// <returns></returns>
        public virtual Matrix GetHandTransform()
        {
            return this.p_gameObj.World;
        }

        /// <summary>
        /// Gets the look dir rot.
        /// </summary>
        /// <returns></returns>
        public Matrix GetLookDirRot()
        {
            Vector3 lookDir = GetDirection();

            return Matrix.CreateLookAt(Vector3.Zero, lookDir, Vector3.Up);
        }

        /// <summary>
        /// Gets the rotation.
        /// </summary>
        /// <returns></returns>
        public virtual Quaternion GetRotation()
        {
            return p_gameObj.Rotation;
        }

        /// <summary>
        /// Gets the velocity.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetVelocity()
        {
            return p_gameObj.GetLinearVelocity();
        }

        /// <summary>
        /// Gets the world matrix.
        /// </summary>
        /// <returns></returns>
        public virtual Matrix GetWorldMatrix()
        {
            return p_gameObj.World;
        }

        /// <summary>
        /// Determines whether [is aware of] [the specified target].
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public bool IsAwareOf(GamePlayer target)
        {
            if (CanSee(target))
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether this instance is falling.
        /// </summary>
        /// <returns></returns>
        public bool IsFalling()
        {
            const float threshold = 0.01f;
            Vector3 vel = GetVelocity();
            if (vel.Y < threshold && vel.Y > -threshold)
                return false;
            return true;
        }

        /// <summary>
        /// Jumps the specified jump power.
        /// </summary>
        /// <param name="jumpPower">The jump power.</param>
        public void Jump(float jumpPower)
        {
            if (IsFalling())
                return;

            Vector3 curVel = GetVelocity();
            curVel.Y = jumpPower;

            SetVelocity(curVel);
        }

        /// <summary>
        /// Positions the unobstructed.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <returns></returns>
        public bool PositionUnobstructed(Vector3 pos)
        {
            Matrix rot = Matrix.CreateFromQuaternion(GetRotation()) * Matrix.CreateRotationY(MathHelper.Pi);

            Matrix transformMat = rot * Matrix.CreateTranslation(Position);
            Vector3 start = new Vector3(0f, f_yRayFireOffset, f_zRayStart);

            Vector3 end = pos;

            start = Vector3.Transform(start, transformMat);

            Manager.RaytraceIntersectionInfo raytraceIntersection = RaytraceFunc(new Manager.RaytraceFireInfo(start, end));
            if (raytraceIntersection == null)
                return true;
            else
            {
                float distance = raytraceIntersection.IntersectionPoint.LengthSquared();
                float posDistance = pos.LengthSquared();
                if (posDistance < distance)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Recieves the MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="gameTime">The game time.</param>
        public virtual void RecieveMsg(PlayerMessage msg, GameTime gameTime)
        {
            switch (msg.Type)
            {
                case PlayerMessageType.InflictDamage:
                    s_lastAggressorId = msg.Sender.ActorID;
                    Object.WeaponObj weapon = msg.Sender.WeaponObj;
                    if (weapon.CanFire(gameTime))
                        weapon.InflictDamage(this);

                    break;
            }
        }

        /// <summary>
        /// Sets the direction.
        /// </summary>
        /// <param name="dir">The dir.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void SetDirection(Vector3 dir)
        {
            if (dir == Vector3.Zero)
                return;
            dir.Normalize();
            if (dir == Vector3.Zero)
                return;
            if (p_gameObj == null)
                throw new ArgumentException();

            dir.X = -dir.X;

            p_gameObj.SetRotationDir(dir);
        }

        /// <summary>
        /// Sets the game object PTR.
        /// </summary>
        /// <param name="animObj">The anim object.</param>
        public void SetGameObjPtr(PhysObj animObj)
        {
            p_gameObj = animObj;
            AnimationClipName = "";
            //s_actorID = p_gameObj.ActorID.ToString() + " player";
        }

        /// <summary>
        /// Sets the velocity.
        /// </summary>
        /// <param name="vel">The vel.</param>
        public void SetVelocity(Vector3 vel)
        {
            if (vel != Vector3.Zero)
                SetDirection(vel);
            Vector3 currentVel = p_gameObj.GetLinearVelocity();
            p_gameObj.SetLinearVelocity(new Vector3(vel.X, vel.Y + currentVel.Y, vel.Z));
        }

        /// <summary>
        /// Sets the weapon.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void SetWeapon(Object.WeaponObj obj)
        {
            _weapon = obj;
        }

        /// <summary>
        /// Shoots the weapon.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void ShootWeapon(GameTime gameTime)
        {
            // Do we even have a weapon.
            if (_weapon == null)
                return;

            // Implement an accuracy factor here.

            Matrix transformMat = Matrix.CreateFromQuaternion(GetRotation()) * Matrix.CreateTranslation(Position);
            Vector3 start = new Vector3(0f, f_yRayFireOffset, f_zRayStart);
            Vector3 end = new Vector3(0f, f_yRayFireOffset, f_zRayDistance);
            start = Vector3.Transform(start, transformMat);
            end = Vector3.Transform(end, transformMat);

            Manager.RaytraceIntersectionInfo raytraceIntersection = RaytraceFunc(new Manager.RaytraceFireInfo(start, end));
            if (raytraceIntersection != null)
            {
                // We actually hit something.
                Henge3D.Physics.RigidBody rigidBody = raytraceIntersection.IntersectionBody;
                if (rigidBody is GameRigidBody)
                {
                    GameRigidBody gameRigidBody = rigidBody as GameRigidBody;
                    // Check if there is a controller for this object.
                    GamePlayer hitPlayer = p_PlayerMgr.GetGamePlayerForGameRB(gameRigidBody);
                    if (hitPlayer != null)
                    {
                        s_lastAggressorId = hitPlayer.ActorID;
                        hitPlayer.RecieveMsg(new PlayerMessage(this, PlayerMessageType.InflictDamage), gameTime);
                    }
                }
            }

            _weapon.Shoot(gameTime);
        }

        /// <summary>
        /// Shoots the weapon at.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="end">The end.</param>
        public virtual void ShootWeaponAt(GameTime gameTime, Vector3 end)
        {
            if (_weapon == null)
                return;

            Matrix rot = Matrix.CreateFromQuaternion(GetRotation()) * Matrix.CreateRotationY(MathHelper.Pi);

            Matrix transformMat = rot * Matrix.CreateTranslation(Position);
            Vector3 start = new Vector3(0f, f_yRayFireOffset, f_zRayStart);

            start = Vector3.Transform(start, transformMat);

            Manager.RaytraceIntersectionInfo raytraceIntersection = RaytraceFunc(new Manager.RaytraceFireInfo(start, end));
            if (raytraceIntersection != null)
            {
                // We actually hit something.
                Henge3D.Physics.RigidBody rigidBody = raytraceIntersection.IntersectionBody;
                if (rigidBody is GameRigidBody)
                {
                    GameRigidBody gameRigidBody = rigidBody as GameRigidBody;
                    // Check if there is a controller for this object.
                    GamePlayer hitPlayer = p_PlayerMgr.GetGamePlayerForGameRB(gameRigidBody);
                    if (hitPlayer != null)
                    {
                        s_lastAggressorId = hitPlayer.ActorID;
                        hitPlayer.RecieveMsg(new PlayerMessage(this, PlayerMessageType.InflictDamage), gameTime);
                    }
                }
            }

            _weapon.Shoot(gameTime);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ActorID;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void Update(GameTime gameTime)
        {
            if (f_health <= 0.0)
            {
                p_gameObj.Kill = true;
                this.Kill = true;
            }

            if (s_lastAggressorId != null)
            {
                _currentTime += gameTime.ElapsedGameTime;
            }

            if (_currentTime > _maxTime)
            {
                s_lastAggressorId = null;
                _currentTime = TimeSpan.Zero;
            }
        }
    }
}