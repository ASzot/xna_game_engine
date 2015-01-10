#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using RenderingSystem;

namespace BaseLogic.Object
{
    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public struct AnimObjSaveData
    {
        /// <summary>
        /// The actor identifier
        /// </summary>
        public string ActorID;

        /// <summary>
        /// The bb maximum
        /// </summary>
        public Vector3 BBMax;

        /// <summary>
        /// The bb minimum
        /// </summary>
        public Vector3 BBMin;

        /// <summary>
        /// The clipname
        /// </summary>
        public string Clipname;

        /// <summary>
        /// The filename
        /// </summary>
        public string Filename;

        /// <summary>
        /// The position
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// The rotation
        /// </summary>
        public Quaternion Rotation;

        /// <summary>
        /// The scale
        /// </summary>
        public float Scale;
    }

    /// <summary>
    ///
    /// </summary>
    public struct ChainProcSaveData
    {
        /// <summary>
        /// The chain name
        /// </summary>
        public string ChainName;

        /// <summary>
        /// The game proc datas
        /// </summary>
        public string[] GameProcDatas;
    }

    /// <summary>
    ///
    /// </summary>
    public struct FlashingLightProcessSaveData
    {
        /// <summary>
        /// The chain procs
        /// </summary>
        public ChainProcSaveData[] ChainProcs;

        /// <summary>
        /// The flash out dur
        /// </summary>
        public uint FlashOutDur;

        /// <summary>
        /// The flash out freq
        /// </summary>
        public uint FlashOutFreq;

        /// <summary>
        /// The game light identifier
        /// </summary>
        public string GameLightID;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlashingLightProcessSaveData"/> struct.
        /// </summary>
        /// <param name="flp">The FLP.</param>
        public FlashingLightProcessSaveData(Process.FlashingLightProcess flp)
        {
            FlashOutDur = flp.FlashOutDur;
            FlashOutFreq = flp.FlashOutFreq;
            GameLightID = flp.GameLightID;

            string[] chainNames = flp.GetAllRealTimeEventChainNames();
            ChainProcs = new ChainProcSaveData[chainNames.Count()];
            for (int i = 0; i < chainNames.Count(); ++i)
            {
                string chainName = chainNames.ElementAt(i);
                List<Process.GameProcess> gameProcs = flp.GetAllGameProccesses(chainName);
                ChainProcs[i].GameProcDatas = new string[gameProcs.Count];
                ChainProcs[i].ChainName = chainName;
                for (int j = 0; j < gameProcs.Count; ++j)
                {
                    Process.GameProcess gameProc = gameProcs[j];
                    ChainProcs[i].GameProcDatas[j] = ChainProcHelper.SaveProc(gameProc);
                }
            }
        }

        /// <summary>
        /// To the flashing light proc.
        /// </summary>
        /// <param name="lightMgr">The light MGR.</param>
        /// <param name="particleMgr">The particle MGR.</param>
        /// <returns></returns>
        public Process.FlashingLightProcess ToFlashingLightProc(Manager.LightManager lightMgr, Manager.ParticleMgr particleMgr)
        {
            Process.FlashingLightProcess flp = new Process.FlashingLightProcess(FlashOutFreq, FlashOutDur,
                lightMgr.GetLight(GameLightID));

            foreach (ChainProcSaveData chainProcSaveData in ChainProcs)
            {
                string chainName = chainProcSaveData.ChainName;
                foreach (string chainProcStrData in chainProcSaveData.GameProcDatas)
                {
                    Process.GameProcess gameProc = ChainProcHelper.LoadProc(chainProcStrData, lightMgr, particleMgr);
                    flp.AddRealTimeEvent(gameProc, chainName);
                }
            }

            return flp;
        }
    }

    // Monolithic light structure.
    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public struct LightSaveData
    {
        /// <summary>
        /// The cast shadows
        /// </summary>
        public bool CastShadows;

        /// <summary>
        /// The depth bias
        /// </summary>
        public float DepthBias;

        // Light.
        /// <summary>
        /// The diffuse
        /// </summary>
        public Color Diffuse;

        /// <summary>
        /// The flare glow size
        /// </summary>
        public float FlareGlowSize;

        /// <summary>
        /// The flare query size
        /// </summary>
        public float FlareQuerySize;

        /// <summary>
        /// The intensity
        /// </summary>
        public float Intensity;

        /// <summary>
        /// The light identifier
        /// </summary>
        public string LightID;

        /// <summary>
        /// The light type
        /// </summary>
        public GameLightType LightType;

        // Point Light.
        /// <summary>
        /// The position
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// The range
        /// </summary>
        public float Range;

        // Spot Light.
        /// <summary>
        /// The rotation
        /// </summary>
        public Vector3 Rotation;

        // Dir Light
        /// <summary>
        /// The shadow distance
        /// </summary>
        public float ShadowDistance;

        /// <summary>
        /// The spec intensity
        /// </summary>
        public float SpecIntensity;

        /// <summary>
        /// The spot angle
        /// </summary>
        public float SpotAngle;

        /// <summary>
        /// The spot exponent
        /// </summary>
        public float SpotExponent;

        /// <summary>
        /// The use lens flare
        /// </summary>
        public bool UseLensFlare;
    }

    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public struct ParticleSystemSaveData
    {
        /// <summary>
        /// The actor identifier
        /// </summary>
        public string ActorID;

        /// <summary>
        /// The duration seconds
        /// </summary>
        public double DurationSeconds;

        /// <summary>
        /// The particle settings
        /// </summary>
        public RenderingSystem.RendererImpl.ParticleSettings ParticleSettings;

        /// <summary>
        /// The position
        /// </summary>
        public Vector3 Position;
    }

    /// <summary>
    ///
    /// </summary>
    public struct ProcessSaveData
    {
        /// <summary>
        /// The flashing light save data
        /// </summary>
        public FlashingLightProcessSaveData[] FlashingLightSaveData;
    }

    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public struct SaveGameData
    {
        /// <summary>
        /// The ai save data
        /// </summary>
        public Player.AISaveData AISaveData;

        /// <summary>
        /// The anim object save datas
        /// </summary>
        public AnimObjSaveData[] AnimObjSaveDatas;

        /// <summary>
        /// The light save datas
        /// </summary>
        public LightSaveData[] LightSaveDatas;

        /// <summary>
        /// The particle save datas
        /// </summary>
        public ParticleSystemSaveData[] ParticleSaveDatas;

        /// <summary>
        /// The physics save data
        /// </summary>
        public Manager.PhysicsSaveData PhysicsSaveData;

        /// <summary>
        /// The proc save data
        /// </summary>
        public ProcessSaveData ProcSaveData;

        /// <summary>
        /// The render save data
        /// </summary>
        public RendererSaveData RenderSaveData;

        /// <summary>
        /// The static object save datas
        /// </summary>
        public StaticObjSaveData[] StaticObjSaveDatas;

        /// <summary>
        /// The swinging door object save data
        /// </summary>
        public SwingingDoorObjSaveData[] SwingingDoorObjSaveData;

        /// <summary>
        /// The translating door object save data
        /// </summary>
        public TranslatingDoorObjSaveData[] TranslatingDoorObjSaveData;

        /// <summary>
        /// The water plane save datas
        /// </summary>
        public WaterPlaneSaveData[] WaterPlaneSaveDatas;
    }

    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public struct StaticObjSaveData
    {
        /// <summary>
        /// The invali d_ mass
        /// </summary>
        public const float INVALID_MASS = -1f;

        /// <summary>
        /// The actor identifier
        /// </summary>
        public string ActorID;

        /// <summary>
        /// The filename
        /// </summary>
        public string Filename;

        /// <summary>
        /// The inertia
        /// </summary>
        public Matrix Inertia;

        /// <summary>
        /// The object mass
        /// </summary>
        public float ObjMass;

        /// <summary>
        /// The physical data filename
        /// </summary>
        public string PhysDataFilename;

        /// <summary>
        /// The position
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// The rotation
        /// </summary>
        public Quaternion Rotation;

        /// <summary>
        /// The scale
        /// </summary>
        public float Scale;

        /// <summary>
        /// The subset materials
        /// </summary>
        public SubsetMaterial[] SubsetMaterials;
    }

    /// <summary>
    ///
    /// </summary>
    public struct SwingingDoorObjSaveData
    {
        /// <summary>
        /// The closed angle
        /// </summary>
        public float ClosedAngle;

        /// <summary>
        /// The open angle
        /// </summary>
        public float OpenAngle;

        /// <summary>
        /// The rot axis
        /// </summary>
        public Vector3 RotAxis;

        /// <summary>
        /// The rot axis offset
        /// </summary>
        public Vector3 RotAxisOffset;

        /// <summary>
        /// The static object save data
        /// </summary>
        public StaticObjSaveData StaticObjSaveData;

        /// <summary>
        /// The swing speed
        /// </summary>
        public float SwingSpeed;
    }

    /// <summary>
    ///
    /// </summary>
    public struct TranslatingDoorObjSaveData
    {
        /// <summary>
        /// The closed height
        /// </summary>
        public float ClosedHeight;

        /// <summary>
        /// The open height
        /// </summary>
        public float OpenHeight;

        /// <summary>
        /// The speed
        /// </summary>
        public float Speed;

        /// <summary>
        /// The static object save data
        /// </summary>
        public StaticObjSaveData StaticObjSaveData;
    }

    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public struct WaterPlaneSaveData
    {
        /// <summary>
        /// The actor identifier
        /// </summary>
        public string ActorID;

        /// <summary>
        /// The position
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// The rotation
        /// </summary>
        public Quaternion Rotation;

        /// <summary>
        /// The scale
        /// </summary>
        public Vector2 Scale;

        /// <summary>
        /// The tex rot
        /// </summary>
        public Vector3 TexRot;

        /// <summary>
        /// The tex scale
        /// </summary>
        public Vector2 TexScale;

        /// <summary>
        /// The trans dir
        /// </summary>
        public Vector3 TransDir;

        /// <summary>
        /// The trans speed
        /// </summary>
        public float TransSpeed;

        /// <summary>
        /// The water color
        /// </summary>
        public Vector4 WaterColor;

        /// <summary>
        /// The water color factor
        /// </summary>
        public float WaterColorFactor;

        /// <summary>
        /// The wave height
        /// </summary>
        public float WaveHeight;

        /// <summary>
        /// The wave length
        /// </summary>
        public float WaveLength;
    }

    /// <summary>
    ///
    /// </summary>
    public static class ChainProcHelper
    {
        /// <summary>
        /// Loads the proc.
        /// </summary>
        /// <param name="strData">The string data.</param>
        /// <param name="lightMgr">The light MGR.</param>
        /// <param name="particleMgr">The particle MGR.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Invalid process name!</exception>
        public static Process.GameProcess LoadProc(string strData, Manager.LightManager lightMgr, Manager.ParticleMgr particleMgr)
        {
            // Some dangerous stuff is going down here exceptions can be thrown if we don't output the data right.

            string[] feilds = strData.Split(',');

            string particleProcName = typeof(Process.ParticleEffectProcess).ToString();
            string soundProcName = typeof(Process.SoundEffectProcess).ToString();

            string procName = feilds[0];
            if (procName == particleProcName)
            {
                string particleId = feilds[1];
                string waitTimeStr = feilds[2];
                PointLight pointLight = null;
                if (feilds.Count() > 3)
                {
                    string lightId = feilds[3];
                    pointLight = lightMgr.GetLight(lightId) as PointLight;
                }

                uint waitTime = uint.Parse(waitTimeStr);

                GameParticleSystem particleSystem = particleMgr.GetParticleSystem(particleId);

                Process.ParticleEffectProcess particleProc = new Process.ParticleEffectProcess(waitTime, particleSystem, pointLight);
                return particleProc;
            }
            else if (procName == soundProcName)
            {
                string filename = feilds[1];
                string loopingStr = feilds[2];
                string panStr = feilds[3];
                string pitchStr = feilds[4];
                string volumeStr = feilds[5];

                bool looping = bool.Parse(loopingStr);
                float pan = float.Parse(panStr);
                float pitch = float.Parse(pitchStr);
                float volume = float.Parse(volumeStr);

                Process.SoundEffectProcess soundProc = new Process.SoundEffectProcess(filename);
                soundProc.Looping = looping;
                soundProc.Pan = pan;
                soundProc.Pitch = pitch;
                soundProc.Volume = volume;
                return soundProc;
            }
            else
            {
                throw new ArgumentException("Invalid process name!");
            }
        }

        /// <summary>
        /// Saves the proc.
        /// </summary>
        /// <param name="gameProc">The game proc.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Cannot save  + gameProc.GetType().ToString()</exception>
        public static string SaveProc(Process.GameProcess gameProc)
        {
            StringBuilder sb = new StringBuilder();

            string particleProcName = typeof(Process.ParticleEffectProcess).ToString();
            string soundProcName = typeof(Process.SoundEffectProcess).ToString();

            if (gameProc is Process.ParticleEffectProcess)
            {
                Process.ParticleEffectProcess particleProc = gameProc as Process.ParticleEffectProcess;
                string addStr;
                if (particleProc.Light != null)
                {
                    addStr = string.Format("{0}:{1}:{2}:{3}", particleProcName, particleProc.ParticleSys.ActorID,
                        particleProc.WaitTime, particleProc.Light.LightID);
                }
                else
                {
                    addStr = string.Format("{0}:{1}:{2}", particleProcName, particleProc.ParticleSys.ActorID,
                        particleProc.WaitTime);
                }

                sb.Append(addStr);
            }
            else if (gameProc is Process.SoundEffectProcess)
            {
                Process.SoundEffectProcess soundEffectProc = gameProc as Process.SoundEffectProcess;
                string addStr = string.Format("{0}:{1}:{2}:{3}:{4}:{5}", soundProcName, soundEffectProc.Filename,
                    soundEffectProc.Looping, soundEffectProc.Pan, soundEffectProc.Pitch, soundEffectProc.Volume);
                sb.Append(addStr);
            }
            else
            {
                throw new ArgumentException("Cannot save " + gameProc.GetType().ToString());
            }

            return sb.ToString();
        }
    }

    /// <summary>
    ///
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// The conten t_ path
        /// </summary>
        private const string CONTENT_PATH = "\\My Xna GameContent\\";

        /// <summary>
        /// The leve l_ sav e_ path
        /// </summary>
        private const string LEVEL_SAVE_PATH = "\\My Xna GameContent\\levels\\";

        /// <summary>
        /// Gets the content save location.
        /// </summary>
        /// <returns></returns>
        public static string GetContentSaveLocation()
        {
            string currentDir = Environment.CurrentDirectory;

            string completeFilename = NavigateUpLevel(currentDir, 4);
            completeFilename += CONTENT_PATH;

            return completeFilename;
        }

        /// <summary>
        /// Gets the save level location complete filename.
        /// </summary>
        /// <returns></returns>
        public static string GetSaveLevelLocationCompleteFilename()
        {
            string currentDir = Environment.CurrentDirectory;
            string completeFilename = NavigateUpLevel(currentDir, 4);
            completeFilename += LEVEL_SAVE_PATH;

            return completeFilename;
        }

        /// <summary>
        /// Navigates down level.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="numLevelsUp">The number levels up.</param>
        /// <returns></returns>
        public static string NavigateDownLevel(string path, int numLevelsUp)
        {
            string desiredDirStr = path;
            char[] seperators = { '/', '\\' };
            string[] subFolders = path.Split(seperators);

            // Get rid of the first N levels.
            string final = "";
            for (int i = numLevelsUp; i < subFolders.Count(); ++i)
            {
                final += subFolders[i];
                if (i < subFolders.Count() - 1)
                    final += "\\";
            }

            return final;
        }

        /// <summary>
        /// Navigates up level.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="numLevelsUp">The number levels up.</param>
        /// <returns></returns>
        public static string NavigateUpLevel(string path, int numLevelsUp)
        {
            string desiredDirStr = path;
            for (int i = 0; i < numLevelsUp; ++i)
            {
                desiredDirStr = Directory.GetParent(desiredDirStr).FullName;
            }
            return desiredDirStr;
        }

        /// <summary>
        /// Removes the type of the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string RemoveFileType(string path)
        {
            char[] seperators = { '.' };
            string[] parts = path.Split(seperators);

            return parts[0];
        }

        /// <summary>
        /// Strips the name.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string StripName(string path)
        {
            char[] seperators = { '/', '\\' };
            string[] subFolders = path.Split(seperators);

            return subFolders[subFolders.Count() - 1];
        }
    }

    /// <summary>
    ///
    /// </summary>
    internal class LevelSerilizer
    {
        /// <summary>
        /// The on scene load
        /// </summary>
        public Action<string> OnSceneLoad;

        /// <summary>
        /// The _requested save data
        /// </summary>
        private SaveGameData? _requestedSaveData = null;

        /// <summary>
        /// The b_load game requested
        /// </summary>
        private bool b_loadGameRequested = false;

        /// <summary>
        /// The b_save game requested
        /// </summary>
        private bool b_saveGameRequested = false;

        /// <summary>
        /// The s_attempted filename
        /// </summary>
        private string s_attemptedFilename = null;

        /// <summary>
        /// The s_attempted folder
        /// </summary>
        private string s_attemptedFolder = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelSerilizer"/> class.
        /// </summary>
        public LevelSerilizer()
        {
        }

        /// <summary>
        /// Attempts the load game.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="foldername">The foldername.</param>
        public void AttemptLoadGame(GameSystem gameSystem, string filename, string foldername)
        {
            if (!b_loadGameRequested)
            {
                b_loadGameRequested = true;
                s_attemptedFilename = filename;
                s_attemptedFolder = foldername;
            }
        }

        /// <summary>
        /// Attempts the save game.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="foldername">The foldername.</param>
        public void AttemptSaveGame(GameSystem gameSystem, string filename, string foldername)
        {
            if (!b_saveGameRequested)
            {
                b_saveGameRequested = true;
                _requestedSaveData = CreateSaveGameData(gameSystem);
                s_attemptedFilename = filename;
                s_attemptedFolder = foldername;
            }
        }

        /// <summary>
        /// Updates the specified game system.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        public void Update(GameSystem gameSystem)
        {
            if (b_saveGameRequested)
            {
                DoSaveGame();

                b_saveGameRequested = false;
            }
            else if (b_loadGameRequested)
            {
                DoLoadGame(gameSystem);

                b_loadGameRequested = false;
            }
        }

        /// <summary>
        /// Creates the save game data.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        private SaveGameData CreateSaveGameData(GameSystem gameSystem)
        {
            IEnumerable<GameObj> gameObjs = gameSystem.ObjMgr.GetDataElements();

            IEnumerable<StaticObj> staticObjs = from go in gameObjs
                                                where go is StaticObj
                                                where !(go is DoorObj)
                                                where (go as StaticObj).SerilizeObj
                                                select go as StaticObj;
            IEnumerable<SwingingDoorObj> swingingDoorObjs = from go in gameObjs
                                                            where go is SwingingDoorObj
                                                            where (go as SwingingDoorObj).SerilizeObj
                                                            select go as SwingingDoorObj;
            IEnumerable<TranslatingDoorObj> translatingDoorObjs = from go in gameObjs
                                                                  where go is TranslatingDoorObj
                                                                  where (go as TranslatingDoorObj).SerilizeObj
                                                                  select go as TranslatingDoorObj;
            IEnumerable<AnimatedObj> animatedObjs = from go in gameObjs
                                                    where go is AnimatedObj
                                                    where (go as AnimatedObj).SerilizeObj
                                                    select go as AnimatedObj;
            IEnumerable<WaterObject> waterObjs = from go in gameObjs
                                                 where go is WaterObject
                                                 select go as WaterObject;

            IEnumerable<GameLight> lights = from light in gameSystem.LightMgr.GetDataElements()
                                            where light.Serilize
                                            select light;

            SaveGameData sgd;

            sgd.StaticObjSaveDatas = new StaticObjSaveData[staticObjs.Count()];
            for (int i = 0; i < staticObjs.Count(); ++i)
            {
                StaticObj staticObj = staticObjs.ElementAt(i);

                StaticObjSaveData sosd = staticObj.GetStaticObjSaveData();

                sgd.StaticObjSaveDatas[i] = sosd;
            }

            sgd.SwingingDoorObjSaveData = new SwingingDoorObjSaveData[swingingDoorObjs.Count()];
            for (int i = 0; i < swingingDoorObjs.Count(); ++i)
            {
                SwingingDoorObj swingingDoor = swingingDoorObjs.ElementAt(i);

                SwingingDoorObjSaveData dosd = swingingDoor.GetDoorObjSaveData();

                sgd.SwingingDoorObjSaveData[i] = dosd;
            }

            sgd.TranslatingDoorObjSaveData = new TranslatingDoorObjSaveData[translatingDoorObjs.Count()];
            for (int i = 0; i < translatingDoorObjs.Count(); ++i)
            {
                TranslatingDoorObj translatingDoor = translatingDoorObjs.ElementAt(i);

                TranslatingDoorObjSaveData dosd = translatingDoor.GetDoorObjSaveData();

                sgd.TranslatingDoorObjSaveData[i] = dosd;
            }

            sgd.WaterPlaneSaveDatas = new WaterPlaneSaveData[waterObjs.Count()];
            for (int i = 0; i < waterObjs.Count(); ++i)
            {
                WaterObject waterObj = waterObjs.ElementAt(i);

                WaterPlaneSaveData wpsd;
                wpsd.Position = waterObj.Position;
                wpsd.Rotation = waterObj.Rotation;
                wpsd.Scale = new Vector2(waterObj.ScaleX, waterObj.ScaleZ);
                wpsd.TexRot = new Vector3(waterObj.TexRotX, waterObj.TexRotY, waterObj.TexRotZ);
                wpsd.TexScale = new Vector2(waterObj.TexScaleX, waterObj.TexScaleY);
                wpsd.TransDir = waterObj.TransDir;
                wpsd.TransSpeed = waterObj.TransSpeed;
                wpsd.WaterColor = waterObj.WaterColor;
                wpsd.WaterColorFactor = waterObj.WaterColorFactor;
                wpsd.WaveHeight = waterObj.WaveHeight;
                wpsd.WaveLength = waterObj.WaveLength;
                wpsd.ActorID = waterObj.ActorID;

                sgd.WaterPlaneSaveDatas[i] = wpsd;
            }

            sgd.AnimObjSaveDatas = new AnimObjSaveData[animatedObjs.Count()];
            for (int i = 0; i < animatedObjs.Count(); ++i)
            {
                AnimatedObj animObj = animatedObjs.ElementAt(i);

                AnimObjSaveData aosd;
                aosd.Position = animObj.Position;
                aosd.Rotation = animObj.Rotation;
                aosd.Scale = animObj.Scale;
                aosd.Filename = animObj.Filename;
                aosd.BBMax = animObj.BBMax;
                aosd.BBMin = animObj.BBMin;
                aosd.ActorID = animObj.ActorID;
                aosd.Clipname = animObj.Clipname;

                sgd.AnimObjSaveDatas[i] = aosd;
            }

            sgd.LightSaveDatas = new LightSaveData[lights.Count()];
            for (int i = 0; i < lights.Count(); ++i)
            {
                GameLight light = lights.ElementAt(i);

                LightSaveData lsd = new LightSaveData();
                if (light is PointLight)
                {
                    PointLight pointLight = light as PointLight;
                    lsd.Diffuse = pointLight.DiffuseColor;
                    lsd.LightID = pointLight.LightID;
                    lsd.Intensity = pointLight.DiffuseIntensity;
                    lsd.Position = pointLight.Position;
                    lsd.Range = pointLight.Range;
                    lsd.UseLensFlare = false;
                    lsd.SpecIntensity = pointLight.SpecularIntensity;
                    lsd.LightType = GameLightType.Point;
                    sgd.LightSaveDatas[i] = lsd;
                }
                else if (light is SpotLight)
                {
                    SpotLight spotLight = light as SpotLight;
                    lsd.Diffuse = spotLight.DiffuseColor;
                    lsd.LightID = spotLight.LightID;
                    lsd.Intensity = spotLight.DiffuseIntensity;
                    lsd.Range = spotLight.Range;
                    lsd.SpotAngle = spotLight.SpotConeAngle;
                    lsd.SpotExponent = spotLight.SpotExponent;
                    lsd.Position = spotLight.Position;
                    lsd.Rotation = spotLight.Rotation;
                    lsd.DepthBias = spotLight.DepthBias;
                    lsd.CastShadows = spotLight.CastShadows;
                    lsd.UseLensFlare = spotLight.UseLensFlare;
                    lsd.FlareGlowSize = spotLight.FlareGlowSize;
                    lsd.FlareQuerySize = spotLight.FlareQuerySize;
                    lsd.SpecIntensity = spotLight.SpecularIntensity;
                    lsd.LightType = GameLightType.Spot;
                    sgd.LightSaveDatas[i] = lsd;
                }
                else if (light is DirLight)
                {
                    DirLight dirLight = light as DirLight;
                    lsd.Diffuse = dirLight.DiffuseColor;
                    lsd.DepthBias = dirLight.ShadowBias;
                    lsd.CastShadows = dirLight.CastShadows;
                    lsd.Intensity = dirLight.DiffuseIntensity;
                    lsd.SpecIntensity = dirLight.SpecularIntensity;
                    lsd.ShadowDistance = dirLight.ShadowDistance;
                    lsd.LightID = dirLight.LightID;
                    lsd.Rotation = dirLight.Rotation;
                    lsd.LightType = GameLightType.Directional;
                    sgd.LightSaveDatas[i] = lsd;
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            IEnumerable<GameParticleSystem> particleSystems = gameSystem.ParticleMgr.GetDataElements();
            sgd.ParticleSaveDatas = new ParticleSystemSaveData[particleSystems.Count()];
            for (int i = 0; i < particleSystems.Count(); ++i)
            {
                GameParticleSystem particleSystem = particleSystems.ElementAt(i);

                ParticleSystemSaveData ppsd;
                ppsd.ActorID = particleSystem.ActorID;
                ppsd.ParticleSettings = particleSystem.ParticleSystem.Settings;
                ppsd.ParticleSettings.BlendState = Microsoft.Xna.Framework.Graphics.BlendState.Additive;
                ppsd.Position = particleSystem.Position;
                ppsd.DurationSeconds = particleSystem.ParticleSystem.Settings.Duration.TotalSeconds;

                sgd.ParticleSaveDatas[i] = ppsd;
            }

            sgd.AISaveData = gameSystem.AIMgr.GetAISaveData();

            sgd.RenderSaveData = (gameSystem.Renderer as RendererAccess).GetSaveData();
            sgd.PhysicsSaveData = gameSystem.PhysicsMgr.GetSaveData();

            var flashingLightProcs = from flp in gameSystem.ProcessMgr.GameProcesses
                                     where flp is Process.FlashingLightProcess
                                     select flp as Process.FlashingLightProcess;

            ProcessSaveData procSaveData;
            IEnumerable<FlashingLightProcessSaveData> flashingLightProcSaveData = from flp in flashingLightProcs
                                                                                  where flp.Serilize
                                                                                  select new FlashingLightProcessSaveData(flp);

            procSaveData.FlashingLightSaveData = flashingLightProcSaveData.ToArray();

            sgd.ProcSaveData = procSaveData;

            return sgd;
        }

        /// <summary>
        /// Does the load game.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        /// <exception cref="System.InvalidOperationException">
        /// </exception>
        private void DoLoadGame(GameSystem gameSystem)
        {
            if (s_attemptedFolder == null)
                throw new InvalidOperationException();
            if (s_attemptedFilename == null)
                throw new InvalidOperationException();

            string completeFilename = FileHelper.GetSaveLevelLocationCompleteFilename() + s_attemptedFilename;

            if (!File.Exists(completeFilename))
            {
                return;
            }

            Stream stream = File.Open(completeFilename, FileMode.Open);

            // A File not found exception is being thrown here.
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));

            SaveGameData data = (SaveGameData)serializer.Deserialize(stream);

            LoadSaveGameData(gameSystem, data);

            stream.Close();
        }

        /// <summary>
        /// Does the save game.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// </exception>
        private void DoSaveGame()
        {
            if (s_attemptedFolder == null)
                throw new InvalidOperationException();
            if (s_attemptedFilename == null)
                throw new InvalidOperationException();

            string completeFilename = FileHelper.GetSaveLevelLocationCompleteFilename() + s_attemptedFilename;

            if (File.Exists(completeFilename))
            {
                File.Delete(completeFilename);
            }

            Stream stream = File.Create(completeFilename);

            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
            if (_requestedSaveData != null)
                serializer.Serialize(stream, _requestedSaveData.Value);
            else
                throw new InvalidOperationException();

            stream.Close();
        }

        /// <summary>
        /// Loads the save game data.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        /// <param name="sgd">The SGD.</param>
        /// <exception cref="System.ArgumentException"></exception>
        private void LoadSaveGameData(GameSystem gameSystem, SaveGameData sgd)
        {
            gameSystem.ClearAllData();

            for (int i = 0; i < sgd.StaticObjSaveDatas.Count(); ++i)
            {
                StaticObjSaveData sosd = sgd.StaticObjSaveDatas[i];
                StaticObj staticObj = new StaticObj(sosd);
                bool forceAdd = staticObj.RigidBody.Manager == null;
                gameSystem.AddRenderObj(staticObj, forceAdd);
            }

            for (int i = 0; i < sgd.SwingingDoorObjSaveData.Count(); ++i)
            {
                SwingingDoorObjSaveData dosd = sgd.SwingingDoorObjSaveData[i];
                SwingingDoorObj doorObj = new SwingingDoorObj(dosd);
                gameSystem.AddRenderObj(doorObj, true);
            }

            for (int i = 0; i < sgd.TranslatingDoorObjSaveData.Count(); ++i)
            {
                TranslatingDoorObjSaveData dosd = sgd.TranslatingDoorObjSaveData[i];
                TranslatingDoorObj doorObj = new TranslatingDoorObj(dosd);
                gameSystem.AddRenderObj(doorObj, true);
            }

            for (int i = 0; i < sgd.WaterPlaneSaveDatas.Count(); ++i)
            {
                WaterPlaneSaveData wpsd = sgd.WaterPlaneSaveDatas[i];
                WaterObject waterObj = new WaterObject(wpsd.ActorID);
                waterObj.Position = wpsd.Position;
                waterObj.Rotation = wpsd.Rotation;
                waterObj.ScaleX = wpsd.Scale.X;
                waterObj.ScaleZ = wpsd.Scale.Y;
                waterObj.TexRotX = wpsd.TexRot.X;
                waterObj.TexRotY = wpsd.TexRot.Y;
                waterObj.TexRotZ = wpsd.TexRot.Z;
                waterObj.TransDir = wpsd.TransDir;
                waterObj.TransSpeed = wpsd.TransSpeed;
                waterObj.WaterColor = wpsd.WaterColor;
                waterObj.WaterColorFactor = wpsd.WaterColorFactor;
                waterObj.WaveHeight = wpsd.WaveHeight;
                waterObj.WaveLength = wpsd.WaveLength;
            }

            for (int i = 0; i < sgd.AnimObjSaveDatas.Count(); ++i)
            {
                //AnimObjSaveData aosd = sgd.AnimObjSaveDatas[i];
                //AnimatedObj animObj = gameSystem.CreateAnimatedObjAbsoluteFilename(aosd.Filename, aosd.ActorID, aosd.Clipname, aosd.BBMin, aosd.BBMax);
                //animObj.Position = aosd.Position;
                //animObj.Rotation = aosd.Rotation;
                //animObj.Scale = aosd.Scale;
            }

            GameLight[] lights = new GameLight[sgd.LightSaveDatas.Count()];
            for (int i = 0; i < sgd.LightSaveDatas.Count(); ++i)
            {
                LightSaveData lsd = sgd.LightSaveDatas[i];
                switch (lsd.LightType)
                {
                    case GameLightType.Point:
                        PointLight pointLight = new PointLight(lsd.Position, lsd.Range, lsd.Intensity, lsd.SpecIntensity, lsd.Diffuse, lsd.LightID);
                        lights[i] = pointLight;
                        break;

                    case GameLightType.Spot:
                        SpotLight spotLight = new SpotLight(lsd.Position, lsd.Rotation, lsd.Diffuse, lsd.Range, lsd.Intensity, lsd.SpecIntensity, lsd.CastShadows, lsd.LightID);
                        spotLight.DepthBias = lsd.DepthBias;
                        spotLight.SpotConeAngle = lsd.SpotAngle;
                        spotLight.SpotExponent = lsd.SpotExponent;
                        spotLight.FlareQuerySize = lsd.FlareQuerySize;
                        spotLight.FlareGlowSize = lsd.FlareGlowSize;
                        spotLight.UseLensFlare = lsd.UseLensFlare;
                        lights[i] = spotLight;
                        break;

                    case GameLightType.Directional:
                        DirLight dirLight = new DirLight(lsd.Rotation, lsd.Diffuse, lsd.Intensity, lsd.SpecIntensity, lsd.CastShadows, lsd.LightID);
                        dirLight.ShadowDistance = lsd.ShadowDistance;
                        dirLight.ShadowBias = lsd.DepthBias;
                        lights[i] = dirLight;
                        break;

                    default:
                        throw new ArgumentException();
                }
            }

            gameSystem.LightMgr.AddRange(lights);
            for (int i = 0; i < sgd.ParticleSaveDatas.Count(); ++i)
            {
                ParticleSystemSaveData ppsd = sgd.ParticleSaveDatas[i];
                GameParticleSystem particleSystem = gameSystem.CreateParticleSystem(ppsd.ParticleSettings, ppsd.ActorID);
                particleSystem.ParticleSystem.Settings.BlendState = Microsoft.Xna.Framework.Graphics.BlendState.Additive;
                particleSystem.Position = ppsd.Position;
                particleSystem.ParticleSystem.Settings.Duration = TimeSpan.FromSeconds(ppsd.DurationSeconds);

                particleSystem.SetInfo(gameSystem.Content, gameSystem.Device, particleSystem.ParticleSystem.Settings);
            }

            gameSystem.AIMgr.SetAILoadData(sgd.AISaveData);

            gameSystem.PhysicsMgr.LoadSaveData(sgd.PhysicsSaveData);

            (gameSystem.Renderer as RendererAccess).SetLoadData(sgd.RenderSaveData, gameSystem.Content);

            var flashingLightProcSaveData = sgd.ProcSaveData.FlashingLightSaveData;
            if (flashingLightProcSaveData != null)
            {
                var flashingLightProcs = from flpsd in flashingLightProcSaveData
                                         select flpsd.ToFlashingLightProc(gameSystem.LightMgr, gameSystem.ParticleMgr);

                foreach (Process.FlashingLightProcess flp in flashingLightProcs)
                {
                    gameSystem.AddGameProcess(flp);
                }
            }

            OnSceneLoad(s_attemptedFilename);
        }
    }
}