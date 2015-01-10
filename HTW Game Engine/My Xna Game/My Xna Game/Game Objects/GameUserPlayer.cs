#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using BaseLogic.Object;
using Microsoft.Xna.Framework;
using RenderingSystem;
using BloomProc = BaseLogic.Process.ScreenFlareEffectProcess;
using BloomSettings = RenderingSystem.Graphics.BloomSettings;

namespace My_Xna_Game.Game_Objects
{
    public class GameUserPlayer : BaseLogic.Player.UserPlayer
    {
        public const int NUM_FOOTSTEP_SOUNDS = 4;
        public const string PLAYER_LIGHT_ID = "player flash light";
        private const float TELE_CHRG_TIME_SEC = 5f;
        private TimeSpan _currentTeleChargeTime = TimeSpan.Zero;
        private double _currentWalkTimeSec = 0.0;
        private BloomSettings _originalBloomSettings;
        private SpotLight _spotLight;
        private double _stepIntervalSec;
        private TimeSpan _teleChargeTime;
        private Vector3 _teleQueryPosition = Vector3.Zero;
        private int i_currentFootstep = 1;
        private int i_goldCount = 3;
        private HoldableObj p_holdableObj;
        private TeleportationOrblet p_teleOrblet = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameUserPlayer"/> class.
        /// </summary>
        /// <param name="gameSys">The game system.</param>
        public GameUserPlayer(BaseLogic.GameSystem gameSys)
        {
            _spotLight = new SpotLight(Vector3.Zero, Vector3.Zero, Color.White, 40f, 1f, 1f, true, "player flash light");
            _spotLight.DepthBias = 0.001f;
            _spotLight.UseLensFlare = false;
            _spotLight.SpotConeAngle = 30f;
            _spotLight.SpotExponent = 1f;
            _spotLight.Serilize = false;

            _stepIntervalSec = 1.0;

            gameSys.LightMgr.AddToList(_spotLight);

            UsingFlashLight = false;

            _teleChargeTime = TimeSpan.FromSeconds(TELE_CHRG_TIME_SEC);
        }

        /// <summary>
        /// Gets or sets the gold count.
        /// </summary>
        /// <value>
        /// The gold count.
        /// </value>
        public int GoldCount
        {
            get { return i_goldCount; }
            set { i_goldCount = value; }
        }

        /// <summary>
        /// Gets the holding object identifier.
        /// </summary>
        /// <value>
        /// The holding object identifier.
        /// </value>
        public string HoldingObjId
        {
            get
            {
                if (!IsHoldingObj)
                    return null;
                return p_holdableObj.ActorID;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is holding object.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is holding object; otherwise, <c>false</c>.
        /// </value>
        public bool IsHoldingObj
        {
            get { return p_holdableObj != null; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is teleporting.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is teleporting; otherwise, <c>false</c>.
        /// </value>
        public bool IsTeleporting
        {
            get { return _currentTeleChargeTime != TimeSpan.Zero; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [using flash light].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [using flash light]; otherwise, <c>false</c>.
        /// </value>
        public bool UsingFlashLight
        {
            get { return _spotLight.Enabled; }
            set { _spotLight.Enabled = value; }
        }

        /// <summary>
        /// Gets the rotation.
        /// </summary>
        /// <returns></returns>
        public override Quaternion GetRotation()
        {
            if (!XnaGame.Game_Instance.DebugMode)
            {
                ICamera cam = XnaGame.Game_Instance.Camera;
                return Quaternion.CreateFromYawPitchRoll(cam.Yaw, cam.Pitch, 0f);
            }
            else
                return base.GetRotation();
        }

        /// <summary>
        /// Gets the world matrix.
        /// </summary>
        /// <returns></returns>
        public override Matrix GetWorldMatrix()
        {
            if (!XnaGame.Game_Instance.DebugMode)
            {
                ICamera cam = XnaGame.Game_Instance.Camera;
                Matrix world = Matrix.CreateFromYawPitchRoll(cam.Yaw, cam.Pitch, 0f) * Matrix.CreateTranslation(cam.Position);
                return world;
            }
            else
                return base.GetWorldMatrix();
        }

        /// <summary>
        /// Called when [drop BTN].
        /// </summary>
        public void OnDropBtn()
        {
            if (IsHoldingObj)
            {
                p_holdableObj.OnDrop();
                p_holdableObj = null;
            }
        }

        /// <summary>
        /// Called when [interaction BTN].
        /// </summary>
        public void OnInteractionBtn()
        {
            if (IsHoldingObj)
                p_holdableObj.OnInteraction();
        }

        /// <summary>
        /// Called when [throw BTN].
        /// </summary>
        public void OnThrowBtn()
        {
            if (IsHoldingObj)
            {
                float throwSpeed = p_holdableObj.OnThrow();

                ICamera cam = XnaGame.Game_Instance.Camera;
                Vector3 directionVec = cam.Direction;
                p_holdableObj.SetLinearVelocity(directionVec * throwSpeed);

                p_holdableObj = null;
            }
        }

        /// <summary>
        /// Recieves the MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="gameTime">The game time.</param>
        public override void RecieveMsg(BaseLogic.Player.PlayerMessage msg, GameTime gameTime)
        {
            base.RecieveMsg(msg, gameTime);
            if (msg.Type == BaseLogic.Player.PlayerMessageType.InflictDamage)
            {
                XnaGame.Game_Instance.Input.VibrateController(0.6f, 0.6f, 100);
            }
        }

        /// <summary>
        /// Reloads this instance.
        /// </summary>
        public override void Reload()
        {
            if (IsHoldingObj)
                return;
            if (_weapon.CurrentAmmo != _weapon.ClipSize && _weapon.TotalAmmo != 0)
                XnaGame.Game_Instance.SoundHelper.PlaySoundEffect("CrossbowReload", false, 1.0f);
            base.Reload();
        }

        /// <summary>
        /// Sets the holding object.
        /// </summary>
        /// <param name="pHoldableObj">The p holdable object.</param>
        public void SetHoldingObj(HoldableObj pHoldableObj)
        {
            if (IsHoldingObj)
                p_holdableObj.OnDrop();
            p_holdableObj = pHoldableObj;
            p_holdableObj.SetGamePlayer(this);
        }

        /// <summary>
        /// Starts the teleportation.
        /// </summary>
        /// <param name="telePos">The tele position.</param>
        /// <param name="teleOrblet">The tele orblet.</param>
        public void StartTeleportation(Vector3 telePos, TeleportationOrblet teleOrblet)
        {
            p_teleOrblet = teleOrblet;
            _currentTeleChargeTime = TimeSpan.FromMilliseconds(1.0);

            _teleQueryPosition = telePos;

            b_invicible = true;
            XnaGame game = XnaGame.Game_Instance;
            game.Input.LockMovement = true;

            uint timespanMilli = (uint)(_teleChargeTime.TotalMilliseconds);

            var settings = BloomProc.GetBloomSettings();
            _originalBloomSettings = new BloomSettings(settings.SettingsName, settings.BloomThreshold,
                settings.BlurAmount, settings.BloomIntensity, settings.BaseIntensity,
                settings.BloomSaturation, settings.BloomSaturation);

            BloomProc screenFlareProc = new BloomProc(timespanMilli, 0f, 5f, 5f, 1f, 1f, 1f);
            game.GameSystem.AddGameProcess(screenFlareProc);

            game.Input.VibrateController(1.0f, 1.0f);
        }

        /// <summary>
        /// Toggles the aim weapon.
        /// </summary>
        public override void ToggleAimWeapon()
        {
            if (IsHoldingObj)
                return;
            base.ToggleAimWeapon();
        }

        /// <summary>
        /// Toggles the flash light.
        /// </summary>
        public void ToggleFlashLight()
        {
            UsingFlashLight = !UsingFlashLight;
        }

        /// <summary>
        /// Toggles the weapon raised.
        /// </summary>
        public override void ToggleWeaponRaised()
        {
            if (IsHoldingObj)
                return;
            base.ToggleWeaponRaised();
            XnaGame.Game_Instance.SoundHelper.PlaySoundEffect("EquipmentRustle", false, 1.0f);
        }

        /// <summary>
        /// Turns to player.
        /// </summary>
        /// <param name="turnToPosition">The turn to position.</param>
        public void TurnToPlayer(Vector3 turnToPosition)
        {
            var gameSystem = BaseLogic.GameSystem.GameSys_Instance;
            var cam = gameSystem.GameCamera as BaseLogic.Camera.FreeLookCamera;

            Vector3 toPosition = turnToPosition - cam.Position;

            toPosition.Normalize();

            float desiredPitch = (float)Math.Asin(Vector3.Dot(cam.UpAxis, toPosition));
            float desiredYaw = (float)Math.Atan2(Vector3.Dot(cam.ForwardAxis, toPosition), Vector3.Dot(-cam.SideAxis, toPosition));

            const float yawTurnSpeed = 2f;
            const float pitchTurnSpeed = 2f;

            gameSystem.AddGameProcess(new BaseLogic.Process.CameraRotationProcess(cam, yawTurnSpeed, pitchTurnSpeed, MathHelper.PiOver2 - desiredYaw, desiredPitch));
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (BaseLogic.GameSystem.GameSys_Instance.InDebugMode)
                return;

            if (this.IsTeleporting)
            {
                _currentTeleChargeTime += gameTime.ElapsedGameTime;
                if (_currentTeleChargeTime >= _teleChargeTime)
                {
                    _currentTeleChargeTime = TimeSpan.Zero;
                    EndTeleportation();
                }
            }

            float speed = Speed;

            double speedModifier = speed * 0.2f;
            _currentWalkTimeSec += (speedModifier * gameTime.ElapsedGameTime.TotalSeconds);

            if (_currentWalkTimeSec >= _stepIntervalSec)
            {
                string footstepSoundName = String.Format("Footstep{0}", i_currentFootstep);
                XnaGame.Game_Instance.SoundHelper.PlaySoundEffect(footstepSoundName, false, 1.0f);
                i_currentFootstep++;
                if (i_currentFootstep > NUM_FOOTSTEP_SOUNDS)
                    i_currentFootstep = 1;

                _currentWalkTimeSec = 0;
            }

            const float speedWeaponBobThreshold = 4.0f;
            if (speed >= speedWeaponBobThreshold && !p_hands.Animating && HasWeaponRaised)
            {
                if (_weapon.CurrentAmmo > 0)
                    DoHandAnimation("raise weapon->walk");
                else
                    DoHandAnimation("unloaded raise weapon->walk");
            }
            else if (speed < speedWeaponBobThreshold && p_hands.Animating)
            {
                string animSpanName = p_hands.GetCurrentAnimationSpanName();
                if (animSpanName == "raise weapon->walk" || animSpanName == "unloaded raise weapon->walk")
                {
                    if (HasWeaponRaised)
                    {
                        if (_weapon.CurrentAmmo > 0)
                        {
                            p_hands.SwitchAnimations("rest->raise weapon");
                            p_hands.AnimationPlayTime = p_hands.GetAnimationSpan("rest->raise weapon").AnimationEnd;
                            p_hands.Animating = false;
                        }
                        else
                        {
                            p_hands.SwitchAnimations("unloaded rest->raise weapon");
                            p_hands.AnimationPlayTime = p_hands.GetAnimationSpan("unloaded rest->raise weapon").AnimationEnd;
                            p_hands.Animating = false;
                        }
                    }
                    else
                    {
                        p_hands.SwitchAnimations("raise weapon->rest");
                        p_hands.AnimationPlayTime = p_hands.GetAnimationSpan("raise weapon->rest").AnimationEnd;
                        p_hands.Animating = false;
                    }
                }
            }

            ICamera cam = BaseLogic.GameSystem.GameSys_Instance.GameCamera;

            Matrix transform = cam.Transform;

            Vector3 offsetPos = new Vector3(1f, 0.4f, 2f);
            Vector3 spotLightPos = Vector3.Transform(offsetPos, transform);

            _spotLight.Position = spotLightPos;

            float yaw = cam.Yaw;
            float pitch = cam.Pitch;

            _spotLight.Rotation = new Vector3(pitch, yaw, 0f);
        }

        /// <summary>
        /// Fires the weapon.
        /// </summary>
        protected override void FireWeapon()
        {
            if (IsHoldingObj)
                return;

            XnaGame game = XnaGame.Game_Instance;

            game.SoundHelper.PlaySoundEffect("BowFireArrow", false, 1.0f);
            game.Input.VibrateController(0.0f, 1.0f, 100);
            base.FireWeapon();
        }

        /// <summary>
        /// Ends the teleportation.
        /// </summary>
        private void EndTeleportation()
        {
            this.Position = _teleQueryPosition;

            b_invicible = false;
            XnaGame game = XnaGame.Game_Instance;
            game.Input.LockMovement = false;

            uint timespanMilli = (uint)(_teleChargeTime.TotalMilliseconds);

            BloomProc screenFlareProc = new BloomProc(timespanMilli, _originalBloomSettings);
            game.GameSystem.AddGameProcess(screenFlareProc);

            game.Input.VibrateController(0.0f, 0.0f);

            p_teleOrblet.RepositionInRandomRoom(game.MapMgr);
        }
    }
}