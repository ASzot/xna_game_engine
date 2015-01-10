#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

using RenderingSystem;

namespace BaseLogic.Process
{
    /// <summary>
    ///
    /// </summary>
    public class ParticleEffectProcess : GameProcess
    {
        /// <summary>
        /// The _particle system
        /// </summary>
        private GameParticleSystem _particleSys;

        /// <summary>
        /// The _point light
        /// </summary>
        private RenderingSystem.PointLight _pointLight = null;

        /// <summary>
        /// The ui_current wait
        /// </summary>
        private uint ui_currentWait = 0;

        /// <summary>
        /// The ui_wait time
        /// </summary>
        private uint ui_waitTime = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleEffectProcess"/> class.
        /// </summary>
        /// <param name="millisecondsDuration">Duration of the milliseconds.</param>
        /// <param name="gps">The GPS.</param>
        /// <param name="pointLight">The point light.</param>
        public ParticleEffectProcess(uint millisecondsDuration, GameParticleSystem gps, RenderingSystem.PointLight pointLight)
            : this(millisecondsDuration, gps)
        {
            _pointLight = pointLight;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleEffectProcess"/> class.
        /// </summary>
        /// <param name="millisecondsDuration">Duration of the milliseconds.</param>
        /// <param name="gps">The GPS.</param>
        public ParticleEffectProcess(uint millisecondsDuration, GameParticleSystem gps)
            : base(ProcessType.ProcType_Graphics)
        {
            ui_waitTime = millisecondsDuration;

            _particleSys = gps;
        }

        /// <summary>
        /// Gets or sets the light.
        /// </summary>
        /// <value>
        /// The light.
        /// </value>
        public RenderingSystem.PointLight Light
        {
            get { return _pointLight; }
            set { _pointLight = value; }
        }

        /// <summary>
        /// Gets the particle system.
        /// </summary>
        /// <value>
        /// The particle system.
        /// </value>
        public GameParticleSystem ParticleSys
        {
            get { return _particleSys; }
        }

        /// <summary>
        /// Gets or sets the wait time.
        /// </summary>
        /// <value>
        /// The wait time.
        /// </value>
        public uint WaitTime
        {
            get { return ui_waitTime; }
            set { ui_waitTime = value; }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override GameProcess Clone()
        {
            RenderingSystem.GameParticleSystem gps = new GameParticleSystem();
            gps.SetInfo(ResourceMgr.Content, ResourceMgr.Device, _particleSys.GetInfo());
            gps.Position = _particleSys.Position;
            gps.Rotation = _particleSys.Rotation;
            RenderingSystem.PointLight pointLight = null;
            if (_pointLight != null)
                pointLight = (PointLight)_pointLight.Clone();
            ParticleEffectProcess pep = new ParticleEffectProcess(ui_waitTime, gps, pointLight);
            return pep;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            GameSystem.GameSys_Instance.ParticleMgr.AddToList(_particleSys);
            if (_pointLight != null)
                GameSystem.GameSys_Instance.LightMgr.AddToList(_pointLight);
        }

        /// <summary>
        /// Called when [kill].
        /// </summary>
        public override void OnKill()
        {
            base.OnKill();

            GameSystem.GameSys_Instance.ParticleMgr.RemoveDataElement(_particleSys);
            if (_pointLight != null)
                GameSystem.GameSys_Instance.LightMgr.RemoveDataElement(_pointLight);
        }

        /// <summary>
        /// Resets the particle system.
        /// </summary>
        /// <param name="gameParticleSystem">The game particle system.</param>
        public void ResetParticleSystem(GameParticleSystem gameParticleSystem)
        {
            // Remove the previous particle system.
            if (_particleSys != null)
                GameSystem.GameSys_Instance.ParticleMgr.RemoveDataElement(_particleSys);

            _particleSys = gameParticleSystem;
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

            ui_currentWait += (uint)gameTime.ElapsedGameTime.Milliseconds;

            if (ui_waitTime <= ui_currentWait)
                Kill = true;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class ScreenFlareEffectProcess : TimerProcess
    {
        /// <summary>
        /// The _delta settings per time
        /// </summary>
        private RenderingSystem.Graphics.BloomSettings _deltaSettingsPerTime;

        /// <summary>
        /// The _final bloom settings
        /// </summary>
        private RenderingSystem.Graphics.BloomSettings _finalBloomSettings;

        /// <summary>
        /// The p_bloom settings
        /// </summary>
        private RenderingSystem.Graphics.BloomSettings p_bloomSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenFlareEffectProcess"/> class.
        /// </summary>
        /// <param name="timespanMilliseconds">The timespan milliseconds.</param>
        /// <param name="fromBloomSettings">From bloom settings.</param>
        /// <param name="toBloomSettings">To bloom settings.</param>
        public ScreenFlareEffectProcess(uint timespanMilliseconds, RenderingSystem.Graphics.BloomSettings fromBloomSettings,
            RenderingSystem.Graphics.BloomSettings toBloomSettings)
            : base(timespanMilliseconds)
        {
            fn_onTimeElapsed += OnTimespanElapsed;
            SetBloomSettings(fromBloomSettings);
            p_bloomSettings = GetBloomSettings();

            var deltaBloomSettings = SubBloomSettings(fromBloomSettings, toBloomSettings);
            float fSeconds = 0.001f * (float)timespanMilliseconds;
            _deltaSettingsPerTime = DivBloomSettings(deltaBloomSettings, fSeconds);

            _finalBloomSettings = toBloomSettings;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenFlareEffectProcess"/> class.
        /// </summary>
        /// <param name="timespanMilliseconds">The timespan milliseconds.</param>
        /// <param name="bloomThreshold">The bloom threshold.</param>
        /// <param name="blurAmount">The blur amount.</param>
        /// <param name="bloomIntensity">The bloom intensity.</param>
        /// <param name="baseIntensity">The base intensity.</param>
        /// <param name="bloomSaturation">The bloom saturation.</param>
        /// <param name="baseSaturation">The base saturation.</param>
        public ScreenFlareEffectProcess(uint timespanMilliseconds, float bloomThreshold, float blurAmount,
            float bloomIntensity, float baseIntensity, float bloomSaturation, float baseSaturation)
            : this(timespanMilliseconds, new RenderingSystem.Graphics.BloomSettings("to bloom", bloomThreshold,
                blurAmount, bloomIntensity, baseIntensity, bloomSaturation, baseSaturation))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenFlareEffectProcess"/> class.
        /// </summary>
        /// <param name="timespanMilliseconds">The timespan milliseconds.</param>
        /// <param name="toBloomSettings">To bloom settings.</param>
        public ScreenFlareEffectProcess(uint timespanMilliseconds, RenderingSystem.Graphics.BloomSettings toBloomSettings)
            : base(timespanMilliseconds)
        {
            fn_onTimeElapsed += OnTimespanElapsed;
            p_bloomSettings = GetBloomSettings();

            var deltaBloomSettings = SubBloomSettings(p_bloomSettings, toBloomSettings);
            float fSeconds = 0.001f * (float)timespanMilliseconds;
            _deltaSettingsPerTime = DivBloomSettings(deltaBloomSettings, fSeconds);
            _finalBloomSettings = toBloomSettings;
        }

        /// <summary>
        /// Adds the bloom settings.
        /// </summary>
        /// <param name="settings1">The settings1.</param>
        /// <param name="settings2">The settings2.</param>
        /// <returns></returns>
        public static RenderingSystem.Graphics.BloomSettings AddBloomSettings(RenderingSystem.Graphics.BloomSettings settings1,
            RenderingSystem.Graphics.BloomSettings settings2)
        {
            return new RenderingSystem.Graphics.BloomSettings("add settings",
                settings1.BloomThreshold + settings2.BloomThreshold,
                settings1.BlurAmount + settings2.BlurAmount,
                settings1.BloomIntensity + settings2.BloomIntensity,
                settings1.BaseIntensity + settings2.BaseIntensity,
                settings1.BloomSaturation + settings2.BloomSaturation,
                settings1.BaseSaturation + settings2.BaseSaturation);
        }

        /// <summary>
        /// Divs the bloom settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="f">The f.</param>
        /// <returns></returns>
        public static RenderingSystem.Graphics.BloomSettings DivBloomSettings(RenderingSystem.Graphics.BloomSettings settings, float f)
        {
            return new RenderingSystem.Graphics.BloomSettings("div settings",
                settings.BloomThreshold / f,
                settings.BlurAmount / f,
                settings.BloomIntensity / f,
                settings.BaseIntensity / f,
                settings.BloomSaturation / f,
                settings.BaseSaturation / f);
        }

        /// <summary>
        /// Gets the bloom settings.
        /// </summary>
        /// <returns></returns>
        public static RenderingSystem.Graphics.BloomSettings GetBloomSettings()
        {
            // We are just getting the bloom settings from the RenderAccess.
            return ((GameSystem.GameSys_Instance.Renderer as RenderingSystem.RendererAccess).GetPostProcessingEffect(RenderingSystem.Graphics.BloomPPE.BLOOM_ID)
                as RenderingSystem.Graphics.BloomPPE).Settings;
        }

        /// <summary>
        /// Memberwises the assign.
        /// </summary>
        /// <param name="assignTo">The assign to.</param>
        /// <param name="assigner">The assigner.</param>
        public static void MemberwiseAssign(RenderingSystem.Graphics.BloomSettings assignTo, RenderingSystem.Graphics.BloomSettings assigner)
        {
            assignTo.BaseIntensity = assigner.BaseIntensity;
            assignTo.BaseSaturation = assigner.BaseSaturation;
            assignTo.BloomIntensity = assigner.BloomIntensity;
            assignTo.BloomSaturation = assigner.BloomSaturation;
            assignTo.BloomThreshold = assigner.BloomThreshold;
            assignTo.BlurAmount = assigner.BlurAmount;
        }

        /// <summary>
        /// Muls the bloom settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="f">The f.</param>
        /// <returns></returns>
        public static RenderingSystem.Graphics.BloomSettings MulBloomSettings(RenderingSystem.Graphics.BloomSettings settings, float f)
        {
            return new RenderingSystem.Graphics.BloomSettings("mul settings",
                settings.BloomThreshold * f,
                settings.BlurAmount * f,
                settings.BloomIntensity * f,
                settings.BaseIntensity * f,
                settings.BloomSaturation * f,
                settings.BaseSaturation * f);
        }

        /// <summary>
        /// Sets the bloom settings.
        /// </summary>
        /// <param name="bloomSettings">The bloom settings.</param>
        public static void SetBloomSettings(RenderingSystem.Graphics.BloomSettings bloomSettings)
        {
            ((GameSystem.GameSys_Instance.Renderer as RenderingSystem.RendererAccess).GetPostProcessingEffect(RenderingSystem.Graphics.BloomPPE.BLOOM_ID)
                as RenderingSystem.Graphics.BloomPPE).Settings = bloomSettings;
        }

        /// <summary>
        /// Subs the bloom settings.
        /// </summary>
        /// <param name="settings1">The settings1.</param>
        /// <param name="settings2">The settings2.</param>
        /// <returns></returns>
        public static RenderingSystem.Graphics.BloomSettings SubBloomSettings(RenderingSystem.Graphics.BloomSettings settings1,
            RenderingSystem.Graphics.BloomSettings settings2)
        {
            RenderingSystem.Graphics.BloomSettings deltaSettings = new RenderingSystem.Graphics.BloomSettings("delta settings",
                settings2.BloomThreshold - settings1.BloomThreshold,
                settings2.BlurAmount - settings1.BlurAmount,
                settings2.BloomIntensity - settings1.BloomIntensity,
                settings2.BaseIntensity - settings1.BaseIntensity,
                settings2.BloomSaturation - settings1.BloomSaturation,
                settings2.BaseSaturation - settings1.BaseSaturation);

            return deltaSettings;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
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

            var dSettings = MulBloomSettings(_deltaSettingsPerTime, dt);

            var accumulatedSettings = AddBloomSettings(dSettings, p_bloomSettings);

            MemberwiseAssign(p_bloomSettings, accumulatedSettings);
        }

        /// <summary>
        /// Called when [timespan elapsed].
        /// </summary>
        private void OnTimespanElapsed()
        {
            Kill = true;
            MemberwiseAssign(p_bloomSettings, _finalBloomSettings);
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SongProcess : GameProcess
    {
        /// <summary>
        /// The _song
        /// </summary>
        private Song _song;

        /// <summary>
        /// The s_filename
        /// </summary>
        private string s_filename;

        /// <summary>
        /// Initializes a new instance of the <see cref="SongProcess"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public SongProcess(string filename)
            : base(ProcessType.ProcType_Sound)
        {
            s_filename = filename;
            _song = ResourceMgr.LoadSong(s_filename);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SongProcess"/> class.
        /// </summary>
        /// <param name="song">The song.</param>
        public SongProcess(Song song)
            : base(ProcessType.ProcType_Sound)
        {
            _song = song;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override GameProcess Clone()
        {
            SongProcess sp = new SongProcess(s_filename);
            return sp;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            MediaPlayer.Play(_song);
        }

        /// <summary>
        /// Called when [kill].
        /// </summary>
        public override void OnKill()
        {
            base.OnKill();

            MediaPlayer.Stop();
        }

        /// <summary>
        /// Toggles the paused.
        /// </summary>
        public override void TogglePaused()
        {
            base.TogglePaused();

            if (Paused)
                MediaPlayer.Pause();
            else
                MediaPlayer.Resume();
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (MediaPlayer.PlayPosition == _song.Duration)
                Kill = true;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class Sound3D_DistanceEffectProcess : Sound3DEffectProcess
    {
        /// <summary>
        /// The f_on dist sq
        /// </summary>
        private float f_onDistSq;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sound3D_DistanceEffectProcess"/> class.
        /// </summary>
        /// <param name="soundEffect">The sound effect.</param>
        /// <param name="userPlayer">The user player.</param>
        /// <param name="emitterObj">The emitter object.</param>
        /// <param name="onDistSq">The on dist sq.</param>
        public Sound3D_DistanceEffectProcess(SoundEffectInstance soundEffect, Player.UserPlayer userPlayer, GameActor emitterObj, float onDistSq)
            : base(soundEffect, userPlayer, emitterObj)
        {
            f_onDistSq = onDistSq;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sound3D_DistanceEffectProcess"/> class.
        /// </summary>
        /// <param name="soundEffect">The sound effect.</param>
        /// <param name="userPlayer">The user player.</param>
        /// <param name="emitterPos">The emitter position.</param>
        /// <param name="onDistSq">The on dist sq.</param>
        public Sound3D_DistanceEffectProcess(SoundEffectInstance soundEffect, Player.UserPlayer userPlayer, Vector3 emitterPos, float onDistSq)
            : base(soundEffect, userPlayer, emitterPos)
        {
            f_onDistSq = onDistSq;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float distanceSq = p_userPlayer.DistanceSqTo(_emitter.Position);

            if (distanceSq < f_onDistSq && _soundEffect.State == SoundState.Paused)
            {
                _soundEffect.Resume();
            }
            else if (distanceSq > f_onDistSq && _soundEffect.State == SoundState.Playing)
            {
                _soundEffect.Pause();
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class Sound3DEffectProcess : SoundEffectProcess
    {
        /// <summary>
        /// The _emitter
        /// </summary>
        protected AudioEmitter _emitter;

        /// <summary>
        /// The p_user player
        /// </summary>
        protected Player.UserPlayer p_userPlayer;

        /// <summary>
        /// The p_emitter object
        /// </summary>
        private GameActor p_emitterObj;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sound3DEffectProcess"/> class.
        /// </summary>
        /// <param name="soundEffect">The sound effect.</param>
        /// <param name="userPlayer">The user player.</param>
        /// <param name="emitterObj">The emitter object.</param>
        public Sound3DEffectProcess(SoundEffectInstance soundEffect, Player.UserPlayer userPlayer, GameActor emitterObj)
            : base(soundEffect)
        {
            p_userPlayer = userPlayer;

            _emitter = new AudioEmitter();

            p_emitterObj = emitterObj;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sound3DEffectProcess"/> class.
        /// </summary>
        /// <param name="soundEffect">The sound effect.</param>
        /// <param name="userPlayer">The user player.</param>
        /// <param name="emitterPos">The emitter position.</param>
        public Sound3DEffectProcess(SoundEffectInstance soundEffect, Player.UserPlayer userPlayer, Vector3 emitterPos)
            : base(soundEffect)
        {
            p_userPlayer = userPlayer;

            _emitter = new AudioEmitter();
            _emitter.Position = emitterPos;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            _soundEffect.Volume = Volume;
            _soundEffect.Pitch = Pitch;
            _soundEffect.Pan = Pan;
            _soundEffect.IsLooped = Looping;
            _soundEffect.Apply3D(p_userPlayer.AudioListener, _emitter);

            _soundEffect.Play();
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (p_emitterObj != null)
            {
                _emitter.Position = p_emitterObj.Position;
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SoundEffectProcess : GameProcess
    {
        /// <summary>
        /// The looping
        /// </summary>
        public bool Looping = false;

        /// <summary>
        /// The pan
        /// </summary>
        public float Pan = 0.0f;

        /// <summary>
        /// The pitch
        /// </summary>
        public float Pitch = 0.0f;

        /// <summary>
        /// The volume
        /// </summary>
        public float Volume = 1.0f;

        /// <summary>
        /// The _sound effect
        /// </summary>
        protected SoundEffectInstance _soundEffect;

        /// <summary>
        /// The s_filename
        /// </summary>
        private string s_filename;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundEffectProcess"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public SoundEffectProcess(string filename)
            : this(ResourceMgr.LoadSoundEffect(filename).CreateInstance())
        {
            s_filename = filename;
            SoundEffect effect = ResourceMgr.LoadSoundEffect(filename);
            _soundEffect = effect.CreateInstance();

            _soundEffect.Volume = Volume;
            _soundEffect.Pitch = Pitch;
            _soundEffect.Pan = Pan;
            _soundEffect.IsLooped = Looping;

            _soundEffect.Play();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundEffectProcess"/> class.
        /// </summary>
        /// <param name="soundEffect">The sound effect.</param>
        public SoundEffectProcess(SoundEffectInstance soundEffect)
            : base(ProcessType.ProcType_Sound)
        {
            _soundEffect = soundEffect;
        }

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        /// <value>
        /// The filename.
        /// </value>
        public string Filename
        {
            get { return s_filename; }
            set { s_filename = value; }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override GameProcess Clone()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            _soundEffect.Volume = Volume;
            _soundEffect.Pitch = Pitch;
            _soundEffect.Pan = Pan;
            _soundEffect.IsLooped = Looping;

            _soundEffect.Play();
        }

        /// <summary>
        /// Called when [kill].
        /// </summary>
        public override void OnKill()
        {
            base.OnKill();

            _soundEffect.Stop();
        }

        /// <summary>
        /// Toggles the paused.
        /// </summary>
        public override void TogglePaused()
        {
            base.TogglePaused();

            if (Paused)
            {
                _soundEffect.Pause();
            }
            else
            {
                if (b_initUpdate)
                {
                    Initialize();
                    b_initUpdate = false;
                }
                else
                    _soundEffect.Resume();
            }
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_soundEffect.State == SoundState.Stopped)
                Kill = true;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class TimerProcess : GameProcess
    {
        /// <summary>
        /// The fn_on time elapsed
        /// </summary>
        protected Action fn_onTimeElapsed;

        /// <summary>
        /// The ui_wait time
        /// </summary>
        protected uint ui_waitTime = 0;

        /// <summary>
        /// The ui_current wait time
        /// </summary>
        private uint ui_currentWaitTime = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerProcess"/> class.
        /// </summary>
        /// <param name="frequencyMilliseconds">The frequency milliseconds.</param>
        public TimerProcess(uint frequencyMilliseconds)
            : base(ProcessType.ProcType_Wait)
        {
            ui_waitTime = frequencyMilliseconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerProcess"/> class.
        /// </summary>
        /// <param name="frequencyMilliseconds">The frequency milliseconds.</param>
        /// <param name="startOffsetMilliseconds">The start offset milliseconds.</param>
        public TimerProcess(uint frequencyMilliseconds, uint startOffsetMilliseconds)
            : this(frequencyMilliseconds)
        {
            ui_currentWaitTime = startOffsetMilliseconds;
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public override void Reset()
        {
            ui_currentWaitTime = 0;
            base.Reset();
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

            ui_currentWaitTime += (uint)gameTime.ElapsedGameTime.Milliseconds;
            if (ui_waitTime <= ui_currentWaitTime && fn_onTimeElapsed != null)
            {
                fn_onTimeElapsed();
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class WaitEventProcess : WaitProcess
    {
        /// <summary>
        /// The fn_finished
        /// </summary>
        private Action fn_finished;

        /// <summary>
        /// Initializes a new instance of the <see cref="WaitEventProcess"/> class.
        /// </summary>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <param name="onFinished">The on finished.</param>
        public WaitEventProcess(uint milliseconds, Action onFinished)
            : base(milliseconds)
        {
            fn_finished += onFinished;
        }

        /// <summary>
        /// Called when [kill].
        /// </summary>
        public override void OnKill()
        {
            fn_finished();

            base.OnKill();
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class WaitProcess : GameProcess
    {
        /// <summary>
        /// The ui_current wait
        /// </summary>
        private uint ui_currentWait = 0;

        /// <summary>
        /// The ui_wait time
        /// </summary>
        private uint ui_waitTime = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="WaitProcess"/> class.
        /// </summary>
        /// <param name="milliseconds">The milliseconds.</param>
        public WaitProcess(uint milliseconds)
            : base(ProcessType.ProcType_Wait)
        {
            ui_waitTime = milliseconds;
        }

        /// <summary>
        /// Gets the current wait.
        /// </summary>
        /// <value>
        /// The current wait.
        /// </value>
        public uint CurrentWait
        {
            get { return ui_currentWait; }
        }

        /// <summary>
        /// Gets the wait time.
        /// </summary>
        /// <value>
        /// The wait time.
        /// </value>
        public uint WaitTime
        {
            get { return ui_waitTime; }
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
            base.OnKill();
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

            ui_currentWait += (uint)gameTime.ElapsedGameTime.Milliseconds;

            if (ui_waitTime <= ui_currentWait)
                Kill = true;
        }
    }
}