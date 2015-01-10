#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System.Collections.Generic;

using BaseLogic;
using BaseLogic.Process;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace My_Xna_Game.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class SoundHelper
    {
        private Dictionary<string, SoundEffect> _soundEffects = new Dictionary<string, SoundEffect>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundHelper"/> class.
        /// </summary>
        public SoundHelper()
        {
        }

        /// <summary>
        /// Immediates the play sound effect.
        /// </summary>
        /// <param name="soundEffectFilename">The sound effect filename.</param>
        /// <param name="repeat">if set to <c>true</c> [repeat].</param>
        /// <param name="volume">The volume.</param>
        public void ImmediatePlaySoundEffect(string soundEffectFilename, bool repeat, float volume)
        {
            SoundEffectInstance soundEffectInstance = CreateSoundEffectInstance(soundEffectFilename);
            soundEffectInstance.IsLooped = repeat;
            soundEffectInstance.Volume = volume;
            soundEffectInstance.Play();
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        public void LoadContent()
        {
            LoadSoundEffect("DoorSlidingShort");
            LoadSoundEffect("TorchFire");
            LoadSoundEffect("TeleportFlare");
            LoadSoundEffect("BowFireArrow");
            LoadSoundEffect("RunningWater");
            LoadSoundEffect("WumpusBreathing");
            LoadSoundEffect("EquipmentRustle");

            for (int i = 1; i <= Game_Objects.GameUserPlayer.NUM_FOOTSTEP_SOUNDS; ++i)
            {
                LoadSoundEffect("Footstep" + i.ToString());
            }

            LoadSoundEffect("Roar");
            LoadSoundEffect("ButtonClick");
            LoadSoundEffect("CrossbowReload");
        }

        /// <summary>
        /// Plays the given sound effect.
        /// </summary>
        /// <param name="soundEffectFilename">The sound effect filename.</param>
        /// <param name="repeat">if set to <c>true</c> [repeat].</param>
        /// <param name="emitterPos">The emitter position.</param>
        /// <param name="activateDistSq">The activate dist sq.</param>
        /// <param name="volume">The volume.</param>
        /// <returns></returns>
        public GameProcess Play3DSoundEffect(string soundEffectFilename, bool repeat, Vector3 emitterPos, float activateDistSq, float volume)
        {
            SoundEffectInstance soundEffectInstance = CreateSoundEffectInstance(soundEffectFilename);

            GameSystem gameSystem = GameSystem.GameSys_Instance;

            Sound3D_DistanceEffectProcess soundEffectProc = new Sound3D_DistanceEffectProcess(soundEffectInstance, gameSystem.GetUserPlayer(), emitterPos, activateDistSq)
            {
                Volume = volume,
                Looping = repeat,
            };

            gameSystem.AddGameProcess(soundEffectProc);

            return soundEffectProc;
        }

        /// <summary>
        /// Plays the sound effect.
        /// </summary>
        /// <param name="soundEffectFilename">The sound effect filename.</param>
        /// <param name="repeat">if set to <c>true</c> [repeat].</param>
        /// <param name="emitterObj">The emitter object.</param>
        /// <param name="activateDistSq">The activate dist sq.</param>
        /// <param name="volume">The volume.</param>
        /// <returns></returns>
        public GameProcess Play3DSoundEffect(string soundEffectFilename, bool repeat, RenderingSystem.GameActor emitterObj, float activateDistSq, float volume)
        {
            SoundEffectInstance soundEffectInstance = CreateSoundEffectInstance(soundEffectFilename);

            GameSystem gameSystem = GameSystem.GameSys_Instance;
            Sound3D_DistanceEffectProcess soundEffectProc = new Sound3D_DistanceEffectProcess(soundEffectInstance, gameSystem.GetUserPlayer(), emitterObj, activateDistSq)
            {
                Looping = repeat,
                Volume = volume,
            };

            gameSystem.AddGameProcess(soundEffectProc);

            return soundEffectProc;
        }

        /// <summary>
        /// Play3s the d sound effect.
        /// </summary>
        /// <param name="soundEffectFilename">The sound effect filename.</param>
        /// <param name="repeat">if set to <c>true</c> [repeat].</param>
        /// <param name="emitterPos">The emitter position.</param>
        /// <param name="volume">The volume.</param>
        /// <returns></returns>
        public GameProcess Play3DSoundEffect(string soundEffectFilename, bool repeat, Vector3 emitterPos, float volume)
        {
            SoundEffectInstance soundEffectInstance = CreateSoundEffectInstance(soundEffectFilename);

            GameSystem gameSystem = GameSystem.GameSys_Instance;
            Sound3DEffectProcess soundEffectProc = new Sound3DEffectProcess(soundEffectInstance, gameSystem.GetUserPlayer(), emitterPos)
           {
               Volume = volume,
               Looping = repeat,
           };

            gameSystem.AddGameProcess(soundEffectProc);

            return soundEffectProc;
        }

        /// <summary>
        /// Play3s the d sound effect.
        /// </summary>
        /// <param name="soundEffectFilename">The sound effect filename.</param>
        /// <param name="repeat">if set to <c>true</c> [repeat].</param>
        /// <param name="emitterObj">The emitter object.</param>
        /// <param name="volume">The volume.</param>
        /// <returns></returns>
        public GameProcess Play3DSoundEffect(string soundEffectFilename, bool repeat, RenderingSystem.GameActor emitterObj, float volume)
        {
            SoundEffectInstance soundEffectInstance = CreateSoundEffectInstance(soundEffectFilename);

            GameSystem gameSystem = GameSystem.GameSys_Instance;
            Sound3DEffectProcess soundEffectProc = new Sound3DEffectProcess(soundEffectInstance, gameSystem.GetUserPlayer(), emitterObj)
            {
                Looping = repeat,
                Volume = volume,
            };

            gameSystem.AddGameProcess(soundEffectProc);

            return soundEffectProc;
        }

        /// <summary>
        /// Plays the sound effect.
        /// </summary>
        /// <param name="soundEffectFilename">The sound effect filename.</param>
        /// <param name="repeat">if set to <c>true</c> [repeat].</param>
        /// <param name="volume">The volume.</param>
        /// <returns></returns>
        public GameProcess PlaySoundEffect(string soundEffectFilename, bool repeat, float volume)
        {
            SoundEffectInstance soundEffectInstance = CreateSoundEffectInstance(soundEffectFilename);

            GameSystem gameSystem = GameSystem.GameSys_Instance;
            SoundEffectProcess soundEffectProc = new SoundEffectProcess(soundEffectInstance)
            {
                Looping = repeat,
                Volume = volume,
            };
            gameSystem.AddGameProcess(soundEffectProc);

            return soundEffectProc;
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public void UnloadContent()
        {
            foreach (SoundEffect soundEffect in _soundEffects.Values)
            {
                soundEffect.Dispose();
            }
        }

        /// <summary>
        /// Creates the sound effect instance.
        /// </summary>
        /// <param name="soundEffectFilename">The sound effect filename.</param>
        /// <returns></returns>
        private SoundEffectInstance CreateSoundEffectInstance(string soundEffectFilename)
        {
            SoundEffect soundEffect = _soundEffects[soundEffectFilename];
            return soundEffect.CreateInstance();
        }

        /// <summary>
        /// Loads the sound effect.
        /// </summary>
        /// <param name="filename">The filename.</param>
        private void LoadSoundEffect(string filename)
        {
            SoundEffect soundEffect = RenderingSystem.ResourceMgr.LoadSoundEffect(filename);
            _soundEffects.Add(filename, soundEffect);
        }
    }
}