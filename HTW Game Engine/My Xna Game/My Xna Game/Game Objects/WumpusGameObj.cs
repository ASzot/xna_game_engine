#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using BaseLogic;
using BaseLogic.Player;
using BaseLogic.Player.AI;
using Microsoft.Xna.Framework;
using Xna_Game_AI;
using BloomProc = BaseLogic.Process.ScreenFlareEffectProcess;
using BloomSettings = RenderingSystem.Graphics.BloomSettings;

namespace My_Xna_Game.Game_Objects
{
    /// <summary>
    ///
    /// </summary>
    public class WumpusAssultState : WaypointMovementState
    {
        private TimeSpan _attackingAnimDur;
        private TimeSpan _attackingAnimProg;
        private bool b_attacking;
        private bool b_hasDestinationSet = false;
        private float f_attackDamage;
        private Map.MapMgr p_mapMgr;
        private UserPlayer p_userPlayer;

        /// <summary>
        /// Initializes a new instance of the <see cref="WumpusAssultState"/> class.
        /// </summary>
        /// <param name="pMapMgr">The p map MGR.</param>
        /// <param name="pUserPlayer">The p user player.</param>
        /// <param name="attackDamage">The attack damage.</param>
        public WumpusAssultState(Map.MapMgr pMapMgr, UserPlayer pUserPlayer, float attackDamage)
            : base(pMapMgr.MapGraphIndex)
        {
            p_mapMgr = pMapMgr;
            p_userPlayer = pUserPlayer;
            f_attackDamage = attackDamage;
            f_speed = 2f;
        }

        /// <summary>
        /// Attacks the player.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void AttackPlayer(GameAIPlayer entity)
        {
            entity.AnimationClipName = "attacking";
            b_attacking = true;
            _attackingAnimProg = TimeSpan.Zero;
            p_userPlayer.Health -= f_attackDamage;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override AIState Clone()
        {
            return new WumpusAssultState(p_mapMgr, p_userPlayer, f_attackDamage);
        }

        /// <summary>
        /// Enters the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Enter(GameAIPlayer entity)
        {
            base.Enter(entity);

            XnaGame.Game_Instance.SoundHelper.Play3DSoundEffect("Roar", false, entity, 1.0f);

            AnimationSpan animSpan = entity.GetAnimationSpan("attacking");
            _attackingAnimDur = animSpan.AnimationEnd - animSpan.AnimationStart;
            _attackingAnimProg = TimeSpan.Zero;
        }

        /// <summary>
        /// Exits the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Exit(GameAIPlayer entity)
        {
            base.Exit(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameAIPlayer entity, GameTime gameTime)
        {
            base.Update(entity, gameTime);

            WumpusGameObj wumpus = entity as WumpusGameObj;

            if (b_attacking)
            {
                _attackingAnimProg += gameTime.ElapsedGameTime;
                if (_attackingAnimProg >= _attackingAnimDur)
                {
                    b_attacking = false;
                    _attackingAnimProg = TimeSpan.Zero;
                }
                return;
            }

            // The number one priority for the wumpus is to attack the player when in assult state.
            const float distThresholdSq = 64f;
            float distSq = Vector3.DistanceSquared(entity.Position, p_userPlayer.Position);
            if (distSq < distThresholdSq)
            {
                // We are in attacking range.
                AttackPlayer(entity);
                return;
            }

            // Chase after the player in his current position.

            // Can we straight up see the user player?
            if (wumpus.CanDetect(p_userPlayer))
            {
                Vector3 vel = AIBehaviors.Seek(p_userPlayer.Position, entity);
                vel *= f_speed;
                entity.SetVelocity(vel);
                b_hasDestinationSet = false;
            }
            else if (!b_hasDestinationSet)
            {
                // Can't see the player let's go in pursuit of him.
                if (SetDestination(p_userPlayer.Position))
                    b_hasDestinationSet = true;
            }

            entity.AnimationClipName = "walking";
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class WumpusDeepSleepState : WumpusSleepState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WumpusDeepSleepState"/> class.
        /// </summary>
        public WumpusDeepSleepState()
        {
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override AIState Clone()
        {
            return new WumpusDeepSleepState();
        }

        /// <summary>
        /// Enters the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Enter(GameAIPlayer entity)
        {
            base.Enter(entity);
        }

        /// <summary>
        /// Exits the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Exit(GameAIPlayer entity)
        {
            base.Exit(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameAIPlayer entity, GameTime gameTime)
        {
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class WumpusGameObj : EnemyAIPlayer
    {
        public const string WUMPUS_ID = "wumpus";
        private const float FORGET_DIST_SQ = 2500f;
        private const double FORGET_PERIOD = 5f;
        private const double MAX_SLEEP_TIME = 15.0;
        private const double MAX_WANDER_TIME = 30.0;
        private const double MIN_SLEEP_TIME = 10.0;
        private const double MIN_WANDER_TIME = 20.0;
        private const double PLAYER_FORGET_PERIOD = 10.0;
        private const float TELEPORTATION_DAMAGE = 10f;
        private double _currentWalkTimeSec = 0.0;
        private TimeSpan _stateMaxTime = TimeSpan.Zero;
        private TimeSpan _stateTime = TimeSpan.Zero;
        private double _stepIntervalSec = 1.0;
        private float f_attackDamage = 10f;
        private int i_currentFootstep = 1;
        private BaseLogic.Process.GameProcess p_breathingSound;
        private Map.MapMgr p_mapMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="WumpusGameObj"/> class.
        /// </summary>
        /// <param name="physObj">The physical object.</param>
        /// <param name="pMapMgr">The p map MGR.</param>
        public WumpusGameObj(PhysObj physObj, Map.MapMgr pMapMgr)
            : base(WUMPUS_ID, new WumpusDeepSleepState(), physObj)
        {
            p_mapMgr = pMapMgr;
            double seconds = BaseLogic.Core.ThreadSafeRandom.NextDouble(MIN_WANDER_TIME, MAX_WANDER_TIME);
            _stateMaxTime = TimeSpan.FromSeconds(seconds);
            _memory.MaxTime = TimeSpan.FromSeconds(10.0);
            p_breathingSound = XnaGame.Game_Instance.SoundHelper.Play3DSoundEffect("WumpusBreathing", true, this, 1225f, 1.0f);
        }

        /// <summary>
        /// Gets or sets the attack damage.
        /// </summary>
        /// <value>
        /// The attack damage.
        /// </value>
        public float AttackDamage
        {
            get { return f_attackDamage; }
            set { f_attackDamage = value; }
        }

        //public void OnPlayerFire(UserPlayer userPlayer)
        //{
        //    float distanceToSoundSq = Vector3.DistanceSquared(userPlayer.Position, Position);
        //    const float distanceThresholdSq = 100f;

        //    if (!_memory.HasDamageBeenTaken())
        //    {
        //        // The player missed the wumpus may wake up.

        //    }
        //}

        /// <summary>
        /// Determines whether this instance can detect the specified player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        public bool CanDetect(GamePlayer player)
        {
            const float distanceThresholdSq = 100f;
            const float velThresholdSq = 100f;
            const float distanceImmediateThresholdSq = 10f;

            float distanceSq = Vector3.DistanceSquared(player.Position, this.Position);

            if (distanceSq < distanceImmediateThresholdSq)
                return true;

            if (this.CanSee(player))
                return true;

            bool canPlayerSee = player.CanSee(this);
            if (canPlayerSee)
            {
                if (distanceSq < distanceThresholdSq)
                {
                    Vector3 playerVel = player.GetVelocity();
                    float speedSq = playerVel.LengthSquared();
                    if (speedSq > velThresholdSq)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Recieves the MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="gameTime">The game time.</param>
        /// <exception cref="System.ArgumentException">Couldn't locate the user player.</exception>
        public override void RecieveMsg(PlayerMessage msg, GameTime gameTime)
        {
            base.RecieveMsg(msg, gameTime);

            switch (msg.Type)
            {
                case PlayerMessageType.InflictDamage:
                    if (_stateMachine.CurrentState is WumpusSleepState)
                    {
                        UserPlayer userPlayer = (UserPlayer)p_PlayerMgr.GetPlayerOfId(UserPlayer.USER_PLAYER_ID);
                        if (userPlayer == null)
                            throw new ArgumentException("Couldn't locate the user player.");
                        _stateMachine.ChangeState(new WumpusAssultState(p_mapMgr, userPlayer, f_attackDamage));
                    }
                    break;
            }
        }

        /// <summary>
        /// Teleports the specified tele position.
        /// </summary>
        /// <param name="telePos">The tele position.</param>
        public void Teleport(Vector3 telePos)
        {
            _stateMachine.ChangeState(new WumpusTeleportState(telePos));
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            UserPlayer userPlayer = GameSystem.GameSys_Instance.GetUserPlayer();

            XnaGame.Game_Instance.GameSystem.DebugTimings.StartAi1UpdateSw();
            if (userPlayer == null)
                return;

            float distSq = userPlayer.DistanceSqTo(this);

            base.Update(gameTime);

            if (_stateMachine.CurrentState is WumpusDeepSleepState)
                return;

            if (_stateMachine.CurrentState is WumpusTeleportState)
            {
                if ((_stateMachine.CurrentState as WumpusTeleportState).Done)
                    _stateMachine.RevertToPreviousState();
                return;
            }

            if (distSq > FORGET_DIST_SQ)
            {
                ChangeState(new WumpusWanderState(p_mapMgr));
                double seconds = BaseLogic.Core.ThreadSafeRandom.NextDouble(MIN_WANDER_TIME, MAX_WANDER_TIME);
                _stateMaxTime = TimeSpan.FromSeconds(seconds);
                return;
            }

            float speed = Speed;
            double speedModifier = speed * 0.2f;
            _currentWalkTimeSec += (speedModifier * gameTime.ElapsedGameTime.TotalSeconds);

            if (_currentWalkTimeSec >= _stepIntervalSec)
            {
                string footstepSoundName = String.Format("Footstep{0}", i_currentFootstep);
                XnaGame.Game_Instance.SoundHelper.Play3DSoundEffect(footstepSoundName, false, this, 1.0f);
                i_currentFootstep++;
                if (i_currentFootstep > GameUserPlayer.NUM_FOOTSTEP_SOUNDS)
                    i_currentFootstep = 1;

                _currentWalkTimeSec = 0;
            }

            if (distSq < FORGET_DIST_SQ && !(_stateMachine.CurrentState is WumpusAssultState) && CanDetect(userPlayer))
            {
                var assultState = new WumpusAssultState(p_mapMgr, userPlayer, f_attackDamage);
                assultState.Speed = 2f;
                ChangeState(assultState);
                _stateTime = TimeSpan.Zero;
                _stateMaxTime = TimeSpan.FromSeconds(FORGET_PERIOD);
            }

            XnaGame.Game_Instance.GameSystem.DebugTimings.EndAi1UpdateSw();

            XnaGame.Game_Instance.GameSystem.DebugTimings.StartAi2UpdateSw();
            if (_stateTime >= _stateMaxTime)
            {
                _stateTime = TimeSpan.Zero;
                if (_stateMachine.CurrentState is WumpusWanderState)
                {
                    ChangeState(new WumpusSleepState());
                    double seconds = BaseLogic.Core.ThreadSafeRandom.NextDouble(MIN_SLEEP_TIME, MAX_SLEEP_TIME);
                    _stateMaxTime = TimeSpan.FromSeconds(seconds);
                }
                else if (_stateMachine.CurrentState is WumpusSleepState || _stateMachine.CurrentState is WumpusAssultState)
                {
                    ChangeState(new WumpusWanderState(p_mapMgr));
                    double seconds = BaseLogic.Core.ThreadSafeRandom.NextDouble(MIN_WANDER_TIME, MAX_WANDER_TIME);
                    _stateMaxTime = TimeSpan.FromSeconds(seconds);
                }
            }
            if (_stateMachine.CurrentState is WumpusSleepState || _stateMachine.CurrentState is WumpusWanderState || _stateMachine.CurrentState is WumpusAssultState)
                _stateTime += gameTime.ElapsedGameTime;

            XnaGame.Game_Instance.GameSystem.DebugTimings.EndAi2UpdateSw();

            XnaGame.Game_Instance.GameSystem.DebugTimings.StartAi3UpdateSw();
            // Wumpus door stuff.
            const float distanceSq = 4f;
            DoorGameObj doorObj = p_mapMgr.InRangeOfDoor(distanceSq, this);
            if (doorObj != null)
            {
                // Teleport to the other side of the door.
                int to = doorObj.To;
                int from = doorObj.From;

                Vector3 prevPos = Position;

                int roomOf = p_mapMgr.GetRoomNumOf(this);

                int movingToRoomNum = roomOf == to ? from : to;
                int movingToRoomIndex = movingToRoomNum - 1;

                // Teleport to the point which is closest to the room we are moving to
                // not the room we are already in.
                Vector3 roomPos = p_mapMgr.GetRoomPos(movingToRoomIndex);

                Vector3 offset = new Vector3(Map.MapMgr.DOOR_PROJ_DIST, prevPos.Y, -0f);
                Vector3 teleportationPoint = doorObj.GetTeleportationPoint(roomPos, offset);

                Position = teleportationPoint;
            }

            XnaGame.Game_Instance.GameSystem.DebugTimings.EndAi3UpdateSw();

            if (_memory.SummateDamagesTaken() > TELEPORTATION_DAMAGE)
            {
                int randomSafeRoomIndex;
                int userPlayerIndex = p_mapMgr.GetRoomNumOf(userPlayer) - 1;
                int currentIndex = p_mapMgr.GetRoomNumOf(this);

                do
                {
                    randomSafeRoomIndex = p_mapMgr.GetRandomSafeEmptyIndex(XnaGame.Game_Instance.OrbletMgr);
                } while ((randomSafeRoomIndex == currentIndex) || (randomSafeRoomIndex == userPlayerIndex));

                Vector3 telePos = p_mapMgr.GetRoomPos(randomSafeRoomIndex);
                Teleport(telePos);

                _memory.ForgetTotalDamage();
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class WumpusSleepState : AIState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WumpusSleepState"/> class.
        /// </summary>
        public WumpusSleepState()
        {
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override AIState Clone()
        {
            return new WumpusSleepState();
        }

        /// <summary>
        /// Enters the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Enter(GameAIPlayer entity)
        {
            entity.AnimationClipName = "go to sleep";
        }

        /// <summary>
        /// Exits the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Exit(GameAIPlayer entity)
        {
            entity.AnimationClipName = "wake up";
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameAIPlayer entity, GameTime gameTime)
        {
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class WumpusTeleportState : AIState
    {
        private const float TELE_CHARGE_TIME_SECONDS = 3f;

        private TimeSpan _currentTeleChargeTime;
        private BloomSettings _originalBloomSettings;
        private TimeSpan _teleChargeTime;
        private Vector3 _teleQueryPosition;

        private bool b_done = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="WumpusTeleportState"/> class.
        /// </summary>
        /// <param name="telePos">The tele position.</param>
        public WumpusTeleportState(Vector3 telePos)
        {
            _teleQueryPosition = telePos;
            _teleChargeTime = TimeSpan.FromSeconds(TELE_CHARGE_TIME_SECONDS);
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="WumpusTeleportState"/> is done.
        /// </summary>
        /// <value>
        ///   <c>true</c> if done; otherwise, <c>false</c>.
        /// </value>
        public bool Done
        {
            get
            {
                return b_done;
            }
        }

        public override AIState Clone()
        {
            var clonedState = new WumpusTeleportState(_teleQueryPosition);
            clonedState._teleChargeTime = _teleChargeTime;
            clonedState._originalBloomSettings = _originalBloomSettings;
            clonedState._currentTeleChargeTime = _currentTeleChargeTime;
            clonedState.b_done = b_done;

            return clonedState;
        }

        /// <summary>
        /// Enters the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Enter(GameAIPlayer entity)
        {
            _currentTeleChargeTime = TimeSpan.FromMilliseconds(1.0);

            uint timespanMilli = (uint)(_teleChargeTime.TotalMilliseconds);

            var settings = BloomProc.GetBloomSettings();
            _originalBloomSettings = new BloomSettings(settings.SettingsName, settings.BloomThreshold,
                settings.BlurAmount, settings.BloomIntensity, settings.BaseIntensity,
                settings.BloomSaturation, settings.BloomSaturation);

            BloomProc screenFlareProc = new BloomProc(timespanMilli, 0f, 5f, 5f, 1f, 1f, 1f);
            GameSystem.GameSys_Instance.AddGameProcess(screenFlareProc);

            entity.AnimationClipName = "";
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
            _currentTeleChargeTime += gameTime.ElapsedGameTime;
            if (_currentTeleChargeTime >= _teleChargeTime)
            {
                entity.Position = _teleQueryPosition;

                uint timespanMilli = (uint)(_teleChargeTime.TotalMilliseconds);

                BloomProc screenFlareProc = new BloomProc(timespanMilli, _originalBloomSettings);
                GameSystem.GameSys_Instance.AddGameProcess(screenFlareProc);

                b_done = true;
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class WumpusWanderState : WaypointMovementState
    {
        private Map.MapMgr p_mapMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="WumpusWanderState"/> class.
        /// </summary>
        /// <param name="pMapMgr">The p map MGR.</param>
        public WumpusWanderState(Map.MapMgr pMapMgr)
            : base(pMapMgr.MapGraphIndex)
        {
            p_mapMgr = pMapMgr;
            f_speed = 2f;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override AIState Clone()
        {
            WumpusWanderState wanderState = new WumpusWanderState(p_mapMgr);
            wanderState.f_speed = f_speed;
            wanderState._waypoints = _waypoints;

            return wanderState;
        }

        /// <summary>
        /// Enters the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Enter(GameAIPlayer entity)
        {
            base.Enter(entity);

            if (_waypoints == null)
                PickNewRoomTarget();
        }

        /// <summary>
        /// Exits the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Exit(GameAIPlayer entity)
        {
            base.Exit(entity);
        }

        /// <summary>
        /// Called when [destination reached].
        /// </summary>
        public override void OnDestinationReached()
        {
            base.OnDestinationReached();

            PickNewRoomTarget();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameAIPlayer entity, GameTime gameTime)
        {
            base.Update(entity, gameTime);
            entity.AnimationClipName = "walking";
        }

        /// <summary>
        /// Picks the new room target.
        /// </summary>
        /// <param name="index">The index.</param>
        private void PickNewRoomTarget(int index)
        {
            if (!SetDestination(index))
                PickNewRoomTarget();
        }

        /// <summary>
        /// Picks the new room target.
        /// </summary>
        private void PickNewRoomTarget()
        {
            PickNewRoomTarget(p_mapMgr.GetRandomSafeIndex() - 1);
        }
    }
}