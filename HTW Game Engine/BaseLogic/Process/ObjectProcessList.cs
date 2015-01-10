#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using RenderingSystem;

namespace BaseLogic.Process
{
    /// <summary>
    ///
    /// </summary>
    public interface LightModifier
    {
        /// <summary>
        /// Gets the modifying light.
        /// </summary>
        /// <returns></returns>
        GameLight GetModifyingLight();
    }

    /// <summary>
    ///
    /// </summary>
    public class ActorTrigger : RTEProcess
    {
        /// <summary>
        /// The o n_ acto r_ trigger
        /// </summary>
        public const string ON_ACTOR_TRIGGER = "actor in trigger";

        /// <summary>
        /// The p_actor
        /// </summary>
        private GameActor p_actor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorTrigger"/> class.
        /// </summary>
        /// <param name="pActor">The p actor.</param>
        public ActorTrigger(GameActor pActor)
        {
            p_actor = pActor;

            DeclareChainNames(ON_ACTOR_TRIGGER);
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            if (Paused)
                return;

            if (ActorInTrigger(p_actor))
                this.ExecuteProcessChain(ON_ACTOR_TRIGGER);
        }

        /// <summary>
        /// Actors the in trigger.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected virtual bool ActorInTrigger(GameActor actor)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class ActorTriggerCircle : ActorTrigger
    {
        /// <summary>
        /// The _trigger circle
        /// </summary>
        private CircleShape _triggerCircle;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorTriggerCircle"/> class.
        /// </summary>
        /// <param name="pActor">The p actor.</param>
        /// <param name="circleShape">The circle shape.</param>
        public ActorTriggerCircle(GameActor pActor, CircleShape circleShape)
            : base(pActor)
        {
            _triggerCircle = circleShape;
        }

        /// <summary>
        /// Actors the in trigger.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <returns></returns>
        protected override bool ActorInTrigger(GameActor actor)
        {
            return _triggerCircle.ContainsPoint(new Vector2(actor.Position.X, actor.Position.Z));
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class ActorTriggerRectangle : ActorTrigger
    {
        /// <summary>
        /// The _trigger rect
        /// </summary>
        private RectangleShape _triggerRect;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorTriggerRectangle"/> class.
        /// </summary>
        /// <param name="pActor">The p actor.</param>
        /// <param name="triggerArea">The trigger area.</param>
        public ActorTriggerRectangle(GameActor pActor, RectangleShape triggerArea)
            : base(pActor)
        {
            _triggerRect = triggerArea;
        }

        /// <summary>
        /// Actors the in trigger.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <returns></returns>
        protected override bool ActorInTrigger(GameActor actor)
        {
            // Check if the actor is in our little rectangle.
            return _triggerRect.ContainsPoint(new Vector2(actor.Position.X, actor.Position.Z));
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class CameraRotationProcess : GameProcess
    {
        /// <summary>
        /// The b_pitch going forwards
        /// </summary>
        private bool b_pitchGoingForwards;

        /// <summary>
        /// The b_yaw going forwards
        /// </summary>
        private bool b_yawGoingForwards;

        /// <summary>
        /// The f_angle end pitch
        /// </summary>
        private float f_angleEndPitch;

        /// <summary>
        /// The f_angle end yaw
        /// </summary>
        private float f_angleEndYaw;

        /// <summary>
        /// The f_speed pitch
        /// </summary>
        private float f_speedPitch;

        /// <summary>
        /// The f_speed yaw
        /// </summary>
        private float f_speedYaw;

        /// <summary>
        /// The p_cam
        /// </summary>
        private ICamera p_cam;

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraRotationProcess"/> class.
        /// </summary>
        /// <param name="cam">The cam.</param>
        /// <param name="yawRotSpeed">The yaw rot speed.</param>
        /// <param name="pitchRotSpeed">The pitch rot speed.</param>
        /// <param name="endAngleYaw">The end angle yaw.</param>
        /// <param name="endAnglePitch">The end angle pitch.</param>
        public CameraRotationProcess(ICamera cam, float yawRotSpeed, float pitchRotSpeed, float endAngleYaw,
            float endAnglePitch)
            : base(ProcessType.ProcType_Object)
        {
            b_pitchGoingForwards = cam.Pitch < endAnglePitch;
            b_yawGoingForwards = cam.Yaw < endAngleYaw;

            f_speedPitch = pitchRotSpeed;
            f_speedYaw = yawRotSpeed;
            f_angleEndPitch = endAnglePitch;
            f_angleEndYaw = endAngleYaw;
            p_cam = cam;
        }

        /// <summary>
        /// Called when [kill].
        /// </summary>
        public override void OnKill()
        {
            base.OnKill();

            p_cam.Yaw = f_angleEndYaw;
            p_cam.Pitch = f_angleEndPitch;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            bool updatingYaw = false;
            bool updatingPitch = false;

            if ((p_cam.Yaw < f_angleEndYaw && b_yawGoingForwards) ||
                (p_cam.Yaw > f_angleEndYaw && !b_yawGoingForwards))
            {
                updatingYaw = true;

                float speed = f_speedYaw * dt;
                if (!b_yawGoingForwards)
                    speed *= -1f;

                p_cam.Yaw += speed;
            }

            if ((p_cam.Pitch < f_angleEndPitch && b_pitchGoingForwards) ||
                (p_cam.Pitch > f_angleEndPitch && !b_pitchGoingForwards))
            {
                updatingPitch = true;

                float speed = f_speedPitch * dt;
                if (!b_pitchGoingForwards)
                    speed *= -1f;

                p_cam.Pitch += speed;
            }

            if (!updatingPitch && !updatingYaw)
                Kill = true;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class FlashingLightProcess : RTETimerProcess, LightModifier
    {
        /// <summary>
        /// The o n_ ligh t_ out
        /// </summary>
        private const string ON_LIGHT_OUT = "OnLightOut";

        /// <summary>
        /// The _timer
        /// </summary>
        private Core.ProcessTimer _timer;

        /// <summary>
        /// The p_light
        /// </summary>
        private GameLight p_light;

        /// <summary>
        /// The ui_flash out dur
        /// </summary>
        private uint ui_flashOutDur = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlashingLightProcess"/> class.
        /// </summary>
        /// <param name="flashOutFreq">The flash out freq.</param>
        /// <param name="flashOutDur">The flash out dur.</param>
        /// <param name="pLight">The p light.</param>
        public FlashingLightProcess(uint flashOutFreq, uint flashOutDur, GameLight pLight)
            : base(flashOutFreq)
        {
            fn_onTimeElapsed += OnLightOut;
            ui_flashOutDur = flashOutDur;
            p_light = pLight;

            DeclareChainNames(ON_LIGHT_OUT);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlashingLightProcess"/> class.
        /// </summary>
        /// <param name="flashOutFreq">The flash out freq.</param>
        /// <param name="flashOutDur">The flash out dur.</param>
        /// <param name="pLight">The p light.</param>
        /// <param name="startOffset">The start offset.</param>
        public FlashingLightProcess(uint flashOutFreq, uint flashOutDur, GameLight pLight, uint startOffset)
            : base(flashOutFreq, startOffset)
        {
            fn_onTimeElapsed += OnLightOut;
            ui_flashOutDur = flashOutDur;
            p_light = pLight;

            DeclareChainNames(ON_LIGHT_OUT);
        }

        /// <summary>
        /// Gets or sets the flash out dur.
        /// </summary>
        /// <value>
        /// The flash out dur.
        /// </value>
        public uint FlashOutDur
        {
            get { return ui_flashOutDur; }
            set
            {
                ui_flashOutDur = value;
            }
        }

        /// <summary>
        /// Gets or sets the flash out freq.
        /// </summary>
        /// <value>
        /// The flash out freq.
        /// </value>
        public uint FlashOutFreq
        {
            get { return ui_waitTime; }
            set { ui_waitTime = value; }
        }

        /// <summary>
        /// Gets the game light identifier.
        /// </summary>
        /// <value>
        /// The game light identifier.
        /// </value>
        public string GameLightID
        {
            get { return p_light.LightID; }
        }

        /// <summary>
        /// Gets the modifying light.
        /// </summary>
        /// <returns></returns>
        public GameLight GetModifyingLight()
        {
            return p_light;
        }

        /// <summary>
        /// Called when [light out].
        /// </summary>
        public void OnLightOut()
        {
            base.Reset();
            _timer = new Core.ProcessTimer(ui_flashOutDur);
            p_light.Enabled = false;

            _rteMgr.ExecuteProcessChain(ON_LIGHT_OUT);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Flashing Light";
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Paused)
                return;

            if (_timer == null)
            {
                base.Update(gameTime);
            }
            else
            {
                if (_timer.Tick(gameTime))
                {
                    _timer = null;
                    p_light.Enabled = true;
                }
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class LightOnProcess : GameProcess, LightModifier
    {
        /// <summary>
        /// The p_light
        /// </summary>
        private GameLight p_light;

        /// <summary>
        /// Initializes a new instance of the <see cref="LightOnProcess"/> class.
        /// </summary>
        /// <param name="pLight">The p light.</param>
        public LightOnProcess(GameLight pLight)
            : base(ProcessType.ProcType_Object)
        {
            p_light = pLight;
        }

        /// <summary>
        /// Gets or sets the light.
        /// </summary>
        /// <value>
        /// The light.
        /// </value>
        public GameLight Light
        {
            set { p_light = value; }
            get { return p_light; }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override GameProcess Clone()
        {
            return new LightOnProcess(p_light);
        }

        /// <summary>
        /// Gets the modifying light.
        /// </summary>
        /// <returns></returns>
        public GameLight GetModifyingLight()
        {
            return p_light;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            p_light.Enabled = true;
            Kill = true;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class LightOutProcess : GameProcess, LightModifier
    {
        /// <summary>
        /// The p_light
        /// </summary>
        private GameLight p_light;

        /// <summary>
        /// Initializes a new instance of the <see cref="LightOutProcess"/> class.
        /// </summary>
        /// <param name="pLight">The p light.</param>
        public LightOutProcess(GameLight pLight)
            : base(ProcessType.ProcType_Object)
        {
            p_light = pLight;
        }

        /// <summary>
        /// Gets or sets the light.
        /// </summary>
        /// <value>
        /// The light.
        /// </value>
        public GameLight Light
        {
            set { p_light = value; }
            get { return p_light; }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override GameProcess Clone()
        {
            return new LightOutProcess(p_light);
        }

        /// <summary>
        /// Gets the modifying light.
        /// </summary>
        /// <returns></returns>
        public GameLight GetModifyingLight()
        {
            return p_light;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            p_light.Enabled = false;
            Kill = true;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class LightToggleProcess : GameProcess, LightModifier
    {
        /// <summary>
        /// The p_light
        /// </summary>
        private GameLight p_light;

        /// <summary>
        /// Initializes a new instance of the <see cref="LightToggleProcess"/> class.
        /// </summary>
        /// <param name="pLight">The p light.</param>
        public LightToggleProcess(GameLight pLight)
            : base(ProcessType.ProcType_Object)
        {
            p_light = pLight;
        }

        /// <summary>
        /// Gets or sets the light.
        /// </summary>
        /// <value>
        /// The light.
        /// </value>
        public GameLight Light
        {
            set { p_light = value; }
            get { return p_light; }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override GameProcess Clone()
        {
            return new LightToggleProcess(p_light);
        }

        /// <summary>
        /// Gets the modifying light.
        /// </summary>
        /// <returns></returns>
        public GameLight GetModifyingLight()
        {
            return p_light;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            p_light.Enabled = !p_light.Enabled;
            Kill = true;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class ObjModifierProcess : GameProcess
    {
        /// <summary>
        /// The on modification finished
        /// </summary>
        public Action OnModificationFinished;

        /// <summary>
        /// The p_game object
        /// </summary>
        protected GameObj p_gameObj;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjModifierProcess"/> class.
        /// </summary>
        /// <param name="pGameObj">The p game object.</param>
        public ObjModifierProcess(GameObj pGameObj)
            : base(ProcessType.ProcType_Object)
        {
            p_gameObj = pGameObj;
        }

        /// <summary>
        /// Called when [kill].
        /// </summary>
        public override void OnKill()
        {
            if (OnModificationFinished != null)
                OnModificationFinished();

            base.OnKill();
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class ProjectileProccess : WaitProcess
    {
        /// <summary>
        /// The _projectile object
        /// </summary>
        private StaticObj _projectileObj;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileProccess"/> class.
        /// </summary>
        /// <param name="millisecondsAlive">The milliseconds alive.</param>
        /// <param name="projectile">The projectile.</param>
        /// <param name="vel">The vel.</param>
        /// <param name="originationPos">The origination position.</param>
        public ProjectileProccess(uint millisecondsAlive, StaticObj projectile, Vector3 vel,
            Vector3 originationPos)
            : this(millisecondsAlive, projectile, vel, originationPos, Quaternion.Identity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectileProccess"/> class.
        /// </summary>
        /// <param name="millisecondsAlive">The milliseconds alive.</param>
        /// <param name="projectile">The projectile.</param>
        /// <param name="vel">The vel.</param>
        /// <param name="originationPos">The origination position.</param>
        /// <param name="appliedRot">The applied rot.</param>
        public ProjectileProccess(uint millisecondsAlive, StaticObj projectile, Vector3 vel,
            Vector3 originationPos, Quaternion appliedRot)
            : base(millisecondsAlive)
        {
            _projectileObj = (StaticObj)projectile.Clone();

            GameSystem.GameSys_Instance.AddRenderObj(_projectileObj, true);

            _projectileObj.Position = originationPos;
            _projectileObj.Rotation = appliedRot;
            _projectileObj.SetLinearVelocity(vel);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Called when [kill].
        /// </summary>
        public override void OnKill()
        {
            GameSystem.GameSys_Instance.ObjMgr.RemoveDataElement(_projectileObj);
            base.OnKill();
        }

        /// <summary>
        /// Toggles the paused.
        /// </summary>
        public override void TogglePaused()
        {
            base.TogglePaused();
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            if (Paused)
                return;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class RotationModifierProcess : ObjModifierProcess
    {
        /// <summary>
        /// The b_going forwards
        /// </summary>
        private bool b_goingForwards;

        /// <summary>
        /// The f_current angle
        /// </summary>
        private float f_currentAngle;

        /// <summary>
        /// The f_end angle
        /// </summary>
        private float f_endAngle;

        /// <summary>
        /// The f_speed
        /// </summary>
        private float f_speed;

        /// <summary>
        /// The q_original rot
        /// </summary>
        private Quaternion q_originalRot;

        /// <summary>
        /// The v_original position
        /// </summary>
        private Vector3 v_originalPos;

        /// <summary>
        /// The v_rot axis
        /// </summary>
        private Vector3 v_rotAxis;

        /// <summary>
        /// The v_rot axis offset
        /// </summary>
        private Vector3 v_rotAxisOffset;

        /// <summary>
        /// Initializes a new instance of the <see cref="RotationModifierProcess"/> class.
        /// </summary>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="endAngle">The end angle.</param>
        /// <param name="doorObj">The door object.</param>
        public RotationModifierProcess(float startAngle, float endAngle, Object.SwingingDoorObj doorObj)
            : this(startAngle, endAngle, doorObj.RotSpeed, doorObj.RotAxis, doorObj.RotAxisOffset, doorObj)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RotationModifierProcess"/> class.
        /// </summary>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="endAngle">The end angle.</param>
        /// <param name="speed">The speed.</param>
        /// <param name="rotAxis">The rot axis.</param>
        /// <param name="rotAxisOffset">The rot axis offset.</param>
        /// <param name="pGameObj">The p game object.</param>
        public RotationModifierProcess(float startAngle, float endAngle, float speed, Vector3 rotAxis,
            Vector3 rotAxisOffset, GameObj pGameObj)
            : base(pGameObj)
        {
            b_goingForwards = startAngle < endAngle;
            f_endAngle = endAngle;
            f_currentAngle = startAngle;
            f_speed = speed;
            v_rotAxis = rotAxis;
            v_rotAxisOffset = rotAxisOffset;

            q_originalRot = pGameObj.Rotation;
            v_originalPos = pGameObj.Position;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Paused)
                return;

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if ((f_currentAngle >= f_endAngle && b_goingForwards) ||
                (f_currentAngle <= f_endAngle && !b_goingForwards))
            {
                Kill = true;
                return;
            }

            Matrix totalTransform = Matrix.CreateTranslation(v_rotAxisOffset) * Matrix.CreateFromAxisAngle(v_rotAxis,
                f_currentAngle);

            Vector3 scale, pos;
            Quaternion rot;
            totalTransform.Decompose(out scale, out rot, out pos);

            p_gameObj.Rotation = q_originalRot * rot;
            p_gameObj.Position = v_originalPos + pos;

            if (b_goingForwards)
                f_currentAngle += (f_speed * dt);
            else
                f_currentAngle -= (f_speed * dt);
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class RTEProcess : GameProcess, RealTimeEventTrigger
    {
        /// <summary>
        /// The _rte MGR
        /// </summary>
        private RealTimeEventMgr _rteMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="RTEProcess"/> class.
        /// </summary>
        public RTEProcess()
            : base(ProcessType.ProcType_Object)
        {
            _rteMgr = new RealTimeEventMgr();
        }

        /// <summary>
        /// Adds the real time event.
        /// </summary>
        /// <param name="gameProc">The game proc.</param>
        /// <param name="chainName">Name of the chain.</param>
        public void AddRealTimeEvent(GameProcess gameProc, string chainName)
        {
            _rteMgr.AddProccessToChain(gameProc, chainName);
        }

        /// <summary>
        /// Executes the process chain.
        /// </summary>
        /// <param name="chainName">Name of the chain.</param>
        public void ExecuteProcessChain(string chainName)
        {
            _rteMgr.ExecuteProcessChain(chainName);
        }

        /// <summary>
        /// Gets all game proccesses.
        /// </summary>
        /// <param name="chainName">Name of the chain.</param>
        /// <returns></returns>
        public List<GameProcess> GetAllGameProccesses(string chainName)
        {
            return _rteMgr.GetAllProcesses(chainName);
        }

        /// <summary>
        /// Gets all real time event chain names.
        /// </summary>
        /// <returns></returns>
        public string[] GetAllRealTimeEventChainNames()
        {
            return _rteMgr.GetAllChainNames();
        }

        /// <summary>
        /// Removes the real time event.
        /// </summary>
        /// <param name="chainName">Name of the chain.</param>
        /// <param name="gameProc">The game proc.</param>
        public void RemoveRealTimeEvent(string chainName, GameProcess gameProc)
        {
            _rteMgr.GetAllProcesses(chainName).Remove(gameProc);
        }

        /// <summary>
        /// Removes the real time event.
        /// </summary>
        /// <param name="chainName">Name of the chain.</param>
        /// <param name="index">The index.</param>
        public void RemoveRealTimeEvent(string chainName, int index)
        {
            _rteMgr.GetAllProcesses(chainName).RemoveAt(index);
        }

        /// <summary>
        /// Declares the chain names.
        /// </summary>
        /// <param name="names">The names.</param>
        protected void DeclareChainNames(params string[] names)
        {
            _rteMgr.DeclareChainNames(names);
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class RTETimerProcess : TimerProcess, RealTimeEventTrigger
    {
        /// <summary>
        /// The _rte MGR
        /// </summary>
        protected RealTimeEventMgr _rteMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="RTETimerProcess"/> class.
        /// </summary>
        /// <param name="activateFreq">The activate freq.</param>
        public RTETimerProcess(uint activateFreq)
            : base(activateFreq)
        {
            _rteMgr = new RealTimeEventMgr();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RTETimerProcess"/> class.
        /// </summary>
        /// <param name="activateFreq">The activate freq.</param>
        /// <param name="startOffset">The start offset.</param>
        public RTETimerProcess(uint activateFreq, uint startOffset)
            : base(activateFreq, startOffset)
        {
            _rteMgr = new RealTimeEventMgr();
        }

        /// <summary>
        /// Adds the real time event.
        /// </summary>
        /// <param name="gameProc">The game proc.</param>
        /// <param name="chainName">Name of the chain.</param>
        public void AddRealTimeEvent(GameProcess gameProc, string chainName)
        {
            _rteMgr.AddProccessToChain(gameProc, chainName);
        }

        /// <summary>
        /// Gets all game proccesses.
        /// </summary>
        /// <param name="chainName">Name of the chain.</param>
        /// <returns></returns>
        public List<GameProcess> GetAllGameProccesses(string chainName)
        {
            return _rteMgr.GetAllProcesses(chainName);
        }

        /// <summary>
        /// Gets all real time event chain names.
        /// </summary>
        /// <returns></returns>
        public string[] GetAllRealTimeEventChainNames()
        {
            return _rteMgr.GetAllChainNames();
        }

        /// <summary>
        /// Removes the real time event.
        /// </summary>
        /// <param name="chainName">Name of the chain.</param>
        /// <param name="gameProc">The game proc.</param>
        public void RemoveRealTimeEvent(string chainName, GameProcess gameProc)
        {
            _rteMgr.GetAllProcesses(chainName).Remove(gameProc);
        }

        /// <summary>
        /// Removes the real time event.
        /// </summary>
        /// <param name="chainName">Name of the chain.</param>
        /// <param name="index">The index.</param>
        public void RemoveRealTimeEvent(string chainName, int index)
        {
            _rteMgr.GetAllProcesses(chainName).RemoveAt(index);
        }

        /// <summary>
        /// Declares the chain names.
        /// </summary>
        /// <param name="names">The names.</param>
        protected void DeclareChainNames(params string[] names)
        {
            _rteMgr.DeclareChainNames(names);
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class TranslationModifierProcess : ObjModifierProcess
    {
        /// <summary>
        /// The f_current dist
        /// </summary>
        private float f_currentDist = 0f;

        /// <summary>
        /// The f_max dist
        /// </summary>
        private float f_maxDist;

        /// <summary>
        /// The f_speed
        /// </summary>
        private float f_speed;

        /// <summary>
        /// The v_direction
        /// </summary>
        private Vector3 v_direction;

        /// <summary>
        /// The v_end position
        /// </summary>
        private Vector3 v_endPos;

        /// <summary>
        /// The v_original position
        /// </summary>
        private Vector3 v_originalPos;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationModifierProcess"/> class.
        /// </summary>
        /// <param name="endPos">The end position.</param>
        /// <param name="door">The door.</param>
        public TranslationModifierProcess(Vector3 endPos, Object.TranslatingDoorObj door)
            : this(endPos, door.Speed, door)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationModifierProcess"/> class.
        /// </summary>
        /// <param name="endPos">The end position.</param>
        /// <param name="speed">The speed.</param>
        /// <param name="pGameObj">The p game object.</param>
        public TranslationModifierProcess(Vector3 endPos, float speed, GameObj pGameObj)
            : base(pGameObj)
        {
            v_originalPos = pGameObj.Position;

            v_direction = endPos - v_originalPos;
            f_maxDist = v_direction.Length();

            v_endPos = endPos;

            v_direction.Normalize();

            f_speed = speed;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Paused)
                return;

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (f_currentDist >= f_maxDist)
            {
                p_gameObj.Position = v_endPos;
                Kill = true;
                return;
            }

            p_gameObj.Position = (v_direction * f_currentDist) + v_originalPos;

            f_currentDist += (dt * f_speed);
        }
    }
}