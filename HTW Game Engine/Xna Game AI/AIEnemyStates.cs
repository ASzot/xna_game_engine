#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using BaseLogic;
using BaseLogic.Player;
using BaseLogic.Player.AI;
using BaseLogic.Player.AI.Graph;
using BaseLogic.Process;
using Microsoft.Xna.Framework;
using GameMath = BaseLogic.Core.GameMath;

namespace Xna_Game_AI
{
    /// <summary>
    /// The fire state of the enemy where they will just shoot at a player.
    /// </summary>
    public class EnemyFireState : AIState
    {
        /// <summary>
        /// The b_raise weapon done
        /// </summary>
        private bool b_raiseWeaponDone = false;

        /// <summary>
        /// The b_weapon load done
        /// </summary>
        private bool b_weaponLoadDone = true;

        /// <summary>
        /// The p_next state
        /// </summary>
        private AIState p_nextState = null;

        /// <summary>
        /// The p_victim
        /// </summary>
        private GameAIPlayer p_victim;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyFireState"/> class.
        /// </summary>
        /// <param name="victim">The victim.</param>
        /// <param name="pNextState">State of the p next.</param>
        public EnemyFireState(GameAIPlayer victim, AIState pNextState)
        {
            p_victim = victim;
            p_nextState = pNextState;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override AIState Clone()
        {
            return new EnemyFireState(p_victim, p_nextState.Clone());
        }

        /// <summary>
        /// Enters the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Enter(GameAIPlayer entity)
        {
            Vector3 toVictim = p_victim.Position - entity.Position;
            toVictim.Y = 0f;
            entity.SetDirection(toVictim);
            if (entity.AnimationClipName == "rest->raise weapon")
                return;

            entity.AnimationClipName = "rest->raise weapon";
            AnimationSpan animSpan = entity.GetAnimationSpan("rest->raise weapon");

            double millisecondsDouble = (animSpan.AnimationEnd - animSpan.AnimationStart).TotalMilliseconds;
            uint milliseconds = (uint)Math.Round(millisecondsDouble);

            WaitEventProcess waitEventProc = new WaitEventProcess(milliseconds, () =>
                {
                    b_raiseWeaponDone = true;
                });

            GameSystem.GameSys_Instance.ProcessMgr.AddProcess(waitEventProc);
        }

        /// <summary>
        /// Exits the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Exit(GameAIPlayer entity)
        {
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameAIPlayer entity, GameTime gameTime)
        {
            Vector3 toVictim = p_victim.Position - entity.Position;
            toVictim.Y = 0f;
            entity.SetDirection(toVictim);

            if (b_raiseWeaponDone && entity.WeaponObj.NeedsReload())
            {
                entity.WeaponObj.Reload();
                b_weaponLoadDone = false;
                AnimationSpan animSpan = entity.GetAnimationSpan("raise weapon->reload");

                uint milliseconds = (uint)Math.Round((animSpan.AnimationEnd - animSpan.AnimationStart).TotalMilliseconds);

                WaitEventProcess waitEventProc = new WaitEventProcess(milliseconds, () =>
                    {
                        b_weaponLoadDone = true;
                    });

                GameSystem.GameSys_Instance.ProcessMgr.AddProcess(waitEventProc);

                entity.AnimationClipName = "raise weapon->reload";
            }
            else if (!entity.CanNoLongerShoot(p_victim))
            {
                // Alright we can't see our vicitim anymore.
                entity.ChangeState(p_nextState);
                return;
            }
            else if (b_raiseWeaponDone && entity.WeaponObj.CanFire(gameTime) && b_weaponLoadDone)
            {
                // Fire away.
                entity.AnimationClipName = "raise weapon->fire";

                // Shoot our weapon at the player.
                entity.ShootWeaponAt(gameTime, p_victim.Position);
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class EnemyRestState : AIState
    {
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override AIState Clone()
        {
            return new EnemyRestState();
        }

        /// <summary>
        /// Enters the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Enter(GameAIPlayer entity)
        {
            entity.SetVelocity(Vector3.Zero);
            entity.AnimationClipName = "raise weapon";
        }

        /// <summary>
        /// Exits the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Exit(GameAIPlayer entity)
        {
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameAIPlayer entity, GameTime gameTime)
        {
            EnemyAIPlayer player = entity as EnemyAIPlayer;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class EnemyWanderState : AIState
    {
        /// <summary>
        /// The _theta
        /// </summary>
        private float _theta;

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override AIState Clone()
        {
            return new EnemyWanderState();
        }

        /// <summary>
        /// Enters the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Enter(GameAIPlayer entity)
        {
            Random rand = GameMath.GetRandom();
            _theta = (float)rand.NextDouble() * MathHelper.TwoPi;
        }

        /// <summary>
        /// Exits the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Exit(GameAIPlayer entity)
        {
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="gameTime">The game time.</param>
        /// <exception cref="System.ArgumentException">User player doesn't exist!</exception>
        public override void Update(GameAIPlayer entity, GameTime gameTime)
        {
            Matrix entityRot = entity.GetLookDirRot();
            Vector3 pos = entity.Position;

            var intersection = AIBehaviors.GetCollisionCourse(entity);

            _theta += (float)GameMath.NextDoubleInRange(-1, 1f) * 0.01f;
            Vector3 desiredVel = Vector3.Zero;
            if (intersection != null)
            {
                Vector3 avoidVel;

                avoidVel = AIBehaviors.AvoidObstacle(entity, intersection.Position);
                avoidVel.Normalize();

                float dot = Vector3.Dot(avoidVel, Vector3.UnitX);
                float angle = (float)Math.Acos(dot);
                if (avoidVel.Z < 0.0)
                    angle = -angle;

                _theta = angle;
            }

            Vector3 wanderVel;

            Vector3 seekPos = new Vector3((float)Math.Cos(_theta), 0f, (float)Math.Sin(_theta));
            seekPos += entity.Position;
            wanderVel = AIBehaviors.Seek(seekPos, entity);
            wanderVel *= 3f;
            wanderVel.Y = 0f;

            desiredVel += wanderVel;

            desiredVel *= 1f;
            desiredVel.Y = 0f;

            entity.SetVelocity(desiredVel);

            entity.AnimationClipName = "moving";

            GamePlayer userPlayer = GamePlayer.p_PlayerMgr.GetPlayerOfId("user player");
            if (userPlayer == null)
                throw new ArgumentException("User player doesn't exist!");

            if (entity.IsAwareOf(userPlayer))
            {
                // Since we immediately exit the state. ( this could cause problems ).
                return;
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class FindTargetState : WaypointMovementState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindTargetState"/> class.
        /// </summary>
        /// <param name="movementGraphIndex">Index of the movement graph.</param>
        public FindTargetState(int movementGraphIndex)
            : base(movementGraphIndex)
        {
        }

        /// <summary>
        /// Enters the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Enter(GameAIPlayer entity)
        {
            base.Enter(entity);

            Vector3 randVec = GameMath.GetRandomPointInsideBounds(new Vector3(-10f, 0f, -10f), new Vector3(10f, 0f, 10f));
            randVec = new Vector3(10f, 0f, 0f);

            base.SetDestination(randVec);
        }

        /// <summary>
        /// Called when [destination reached].
        /// </summary>
        public override void OnDestinationReached()
        {
            Vector3 randVec = GameMath.GetRandomPointInsideBounds(new Vector3(-10f, 0f, -10f), new Vector3(10f, 0f, 10f));
            randVec.Y = 0f;

            base.SetDestination(randVec);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameAIPlayer entity, GameTime gameTime)
        {
            base.Update(entity, gameTime);
            entity.AnimationClipName = "moving";
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class WaypointMovementState : AIState
    {
        /// <summary>
        /// The _waypoints
        /// </summary>
        protected List<Vector3> _waypoints = null;

        /// <summary>
        /// The f_speed
        /// </summary>
        protected float f_speed = 2f;

        /// <summary>
        /// The i_graph index
        /// </summary>
        protected int i_graphIndex = -1;

        /// <summary>
        /// The _path planner
        /// </summary>
        private PathPlanner _pathPlanner;

        /// <summary>
        /// Initializes a new instance of the <see cref="WaypointMovementState"/> class.
        /// </summary>
        /// <param name="movementGraphIndex">Index of the movement graph.</param>
        public WaypointMovementState(int movementGraphIndex)
        {
            i_graphIndex = movementGraphIndex;
        }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        public float Speed
        {
            get { return f_speed; }
            set { f_speed = value; }
        }

        /// <summary>
        /// Gets the waypoints.
        /// </summary>
        /// <value>
        /// The waypoints.
        /// </value>
        public List<Vector3> Waypoints
        {
            get { return _waypoints; }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override AIState Clone()
        {
            return new WaypointMovementState(i_graphIndex);
        }

        /// <summary>
        /// Enters the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Enter(GameAIPlayer entity)
        {
            SparseGraph graph = (p_aiMgr as AIMgrImpl).PathGraphs[i_graphIndex];
            _pathPlanner = new PathPlanner(entity, graph);
        }

        /// <summary>
        /// Exits the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Exit(GameAIPlayer entity)
        {
        }

        /// <summary>
        /// Called when [destination reached].
        /// </summary>
        public virtual void OnDestinationReached()
        {
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameAIPlayer entity, GameTime gameTime)
        {
            const float error = 0.1f;

            Vector3 waypointSeek = Vector3.Zero;

            if (_waypoints != null && _waypoints.Count > 0)
            {
                // We have active waypoints.
                Vector3 currentWaypoint = _waypoints[0];
                Vector3 entityPos = entity.Position;
                entityPos.Y = 0f;
                Vector3 wayPointPos = currentWaypoint;
                wayPointPos.Y = 0f;
                if (AIBehaviors.InDistanceSquared(entityPos, wayPointPos, error))
                {
                    // We have arrived at this way point continue on to our next waypoint.
                    _waypoints.RemoveAt(0);
                    if (_waypoints.Count == 0)
                    {
                        OnDestinationReached();
                        return;
                    }
                    currentWaypoint = _waypoints[0];
                }
                // Move to that waypoint.
                waypointSeek = AIBehaviors.Seek(currentWaypoint, entity);
            }
            else
                OnDestinationReached();

            Vector3 vel = (waypointSeek);
            if (vel != Vector3.Zero)
                vel.Normalize();
            vel *= f_speed;
            vel.Y = 0f;
            //Vector3 testVel = new Vector3(-1f, 0f, 0f);
            entity.SetVelocity(vel);
        }

        /// <summary>
        /// Sets the destination.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <returns></returns>
        protected bool SetDestination(Vector3 pos)
        {
            return _pathPlanner.CreatePathToPosition<GraphSearchDijkstra>(pos, out _waypoints);
        }

        /// <summary>
        /// Sets the destination.
        /// </summary>
        /// <param name="targetIndex">Index of the target.</param>
        /// <returns></returns>
        protected bool SetDestination(int targetIndex)
        {
            return _pathPlanner.CreatePathToPosition<GraphSearchDijkstra>(targetIndex, out _waypoints);
        }
    }
}