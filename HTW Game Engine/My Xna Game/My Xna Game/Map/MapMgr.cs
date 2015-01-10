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
using System.Text.RegularExpressions;
using System.Xml.Linq;
using BaseLogic;
using BaseLogic.Core;
using BaseLogic.Player.AI.Graph;
using Microsoft.Xna.Framework;
using My_Xna_Game.Game_Objects;
using RenderingSystem;

namespace My_Xna_Game.Map
{
    /// <summary>
    /// Loads the map data from a file.
    /// </summary>
    public class MapContentSerilizer
    {
        private const string MAP_FOLDERNAME = "Maps\\";

        /// <summary>
        /// Load all the information associate with the map into mapMgr.
        /// </summary>
        /// <param name="filename">The relative filename to load from.</param>
        /// <param name="mapMgr">The mapMgr to load the data into.</param>
        public static void LoadMap(string filename, MapMgr mapMgr)
        {
            // Load the stream.
            string loadLoc = BaseLogic.Object.FileHelper.GetContentSaveLocation() + MAP_FOLDERNAME + filename;
            Stream loadStream = File.OpenRead(loadLoc);

            // Parse the XML.
            XDocument doc = XDocument.Load(loadStream);

            XElement baseEle = doc.Element("MapSaveData");

            // Parse the door connections.
            XElement doorConnectionsEle = baseEle.Element("DoorConnections");

            IEnumerable<XElement> doorConnectionEles = doorConnectionsEle.Elements("DoorConnection");
            for (int i = 0; i < doorConnectionEles.Count(); ++i)
            {
                XElement doorConnection = doorConnectionEles.ElementAt(i);
                string enabledStr = doorConnection.Value;
                bool enabled = bool.Parse(enabledStr);
                mapMgr.Doors[i].Enabled = enabled;
            }

            // Parse the map graph.
            XElement mapGraphEle = baseEle.Element("MapGraph");

            XElement nodesEle = mapGraphEle.Element("GraphNodes");
            IEnumerable<XElement> nodeEles = nodesEle.Elements("Node");
            foreach (XElement nodeEle in nodeEles)
            {
                Vector3 pos = new Vector3();
                XElement posEle = nodeEle.Element("Pos");
                XElement xPosEle = posEle.Element("X");
                XElement yPosEle = posEle.Element("Y");
                XElement zPosEle = posEle.Element("Z");

                pos.X = float.Parse(xPosEle.Value);
                pos.Y = float.Parse(yPosEle.Value);
                pos.Z = float.Parse(zPosEle.Value);

                mapMgr.MapGraph.AddNode(pos);
            }

            // Parse the room information like if there is a hazard or not.
            XElement roomInfosEle = baseEle.Element("RoomInfos");
            IEnumerable<XElement> roomInfoEles = roomInfosEle.Elements("RoomInfo");
            for (int i = 0; i < roomInfoEles.Count(); ++i)
            {
                XElement roomInfoEle = roomInfoEles.ElementAt(i);
                XElement roomTypeEle = roomInfoEle.Element("Type");
                RoomInfo.RoomType type = (RoomInfo.RoomType)Enum.Parse(typeof(RoomInfo.RoomType), roomTypeEle.Value);
                mapMgr.RoomInformation[i] = new RoomInfo(type);
            }

            // Parse the locations of the power ups.
            XElement powerUpLocations = baseEle.Element("PowerUps");
            IEnumerable<XElement> healthPowerUps = powerUpLocations.Elements("HealthPowerUp");
            for (int i = 0; i < healthPowerUps.Count(); ++i)
            {
                XElement healthPowerUpEle = healthPowerUps.ElementAt(i);
                XElement roomNumEle = healthPowerUpEle.Element("RoomNum");

                int roomIndex = int.Parse(roomNumEle.Value) - 1;

                mapMgr.PowerUpMgr.PlacePowerUp(roomIndex, GameSystem.GameSys_Instance, mapMgr, new HealthPowerUp());
            }

            // Parse the locations of the teleporter orblets.
            XElement teleporterOrbletsEle = baseEle.Element("TeleporterOrblets");
            IEnumerable<XElement> teleporterOrbletEles = teleporterOrbletsEle.Elements("TeleporterOrblet");
            foreach (XElement teleporterOrbletEle in teleporterOrbletEles)
            {
                XAttribute roomNumEle = teleporterOrbletEle.Attribute("RoomNum");
                int roomIndex = int.Parse(roomNumEle.Value) - 1;
                Vector3 roomPos = mapMgr.GetRoomPos(roomIndex);
                XnaGame.Game_Instance.OrbletMgr.SpawnOrblet(roomPos, new TeleportationOrblet());
            }

            // Parse coordinations of the lights effects. ( only the flashing lights )
            // I forgot why I did it here but I must have had a good reason so I will trust my previous self.
            XElement effectSaveDataEle = baseEle.Element("EffectSaveData");
            IEnumerable<XElement> flashingLightEles = effectSaveDataEle.Elements("FlashingLights");

            foreach (XElement flashingLightEle in flashingLightEles)
            {
                IEnumerable<XElement> lightEles = flashingLightEles.Elements("light");
                string[] lightIds = new string[lightEles.Count()];
                for (int i = 0; i < lightEles.Count(); ++i)
                {
                    XElement lightEle = lightEles.ElementAt(i);
                    lightIds[i] = lightEle.Value;
                }

                XAttribute roomAttribute = flashingLightEle.Attribute("room");
                int roomIndex = int.Parse(roomAttribute.Value) - 1;

                XElement freqEle = flashingLightEle.Element("freq");
                XElement durEle = flashingLightEle.Element("dur");

                uint freq = uint.Parse(freqEle.Value);
                uint dur = uint.Parse(durEle.Value);

                var lights = from id in lightIds
                             select GameSystem.GameSys_Instance.LightMgr.GetLight(id);

                mapMgr.RoomInformation[roomIndex].MakeFlashingLights(lights.ToArray(), freq, dur, GameSystem.GameSys_Instance);
            }

            XnaGame game = XnaGame.Game_Instance;

            // Parse the player position.
            XElement playerEle = baseEle.Element("Player");
            XAttribute playerRoomNumAttribute = playerEle.Attribute("RoomNum");
            int playerRoomIndex = int.Parse(playerRoomNumAttribute.Value) - 1;
            mapMgr.SpawnPlayerRoomIndex = playerRoomIndex;

            // Parse the wumpus position.
            XElement wumpusEle = baseEle.Element("Wumpus");
            XAttribute wumpusRoomNumAttribute = wumpusEle.Attribute("RoomNum");
            int wumpusRoomIndex = int.Parse(wumpusRoomNumAttribute.Value) - 1;
            mapMgr.SpawnWumpusRoomIndex = wumpusRoomIndex;
        }

        /// <summary>
        /// Saves the map.
        /// </summary>
        /// <param name="filename">The relative filename to the map load location.</param>
        /// <param name="mapMgr">The map manager.</param>
        public static void SaveMap(string filename, MapMgr mapMgr)
        {
            // Load the stream.
            string saveLoc = BaseLogic.Object.FileHelper.GetContentSaveLocation() + MAP_FOLDERNAME + filename;

            Stream saveStream;

            // Overwrite the existing file if there was one.
            if (File.Exists(saveLoc))
            {
                saveStream = File.OpenWrite(saveLoc);
            }
            else
            {
                saveStream = File.Create(saveLoc);
            }

            XDocument doc = new XDocument();

            XElement baseEle = new XElement("MapSaveData");

            // Save the door connections.
            XElement doorConnectionsEle = new XElement("DoorConnections");
            foreach (DoorGameObj door in mapMgr.Doors)
            {
                XElement doorConnectionEle = new XElement("DoorConnection", door.Enabled);
                doorConnectionEle.Add(new XAttribute("To", door.To));
                doorConnectionEle.Add(new XAttribute("From", door.From));
                doorConnectionsEle.Add(doorConnectionEle);
            }

            baseEle.Add(doorConnectionsEle);

            // Save the map graph.
            XElement mapGraphEle = new XElement("MapGraph");
            XElement nodesEle = new XElement("GraphNodes");
            foreach (GraphNode node in mapMgr.MapGraph.Nodes)
            {
                XElement nodeEle = new XElement("Node");

                XElement posEle = new XElement("Pos");
                posEle.Add(new XElement("X", node.Position.X.ToString()));
                posEle.Add(new XElement("Y", node.Position.Y.ToString()));
                posEle.Add(new XElement("Z", node.Position.Z.ToString()));
                XElement indexEle = new XElement("Index", node.Index.ToString());

                nodeEle.Add(posEle);
                nodeEle.Add(indexEle);

                nodesEle.Add(nodeEle);
            }

            mapGraphEle.Add(nodesEle);

            baseEle.Add(mapGraphEle);

            // Save the room informations. ( if the room has a pit hazard in it ).
            XElement roomInfosEle = new XElement("RoomInfos");
            for (int i = 0; i < mapMgr.RoomInformation.Count(); ++i)
            {
                RoomInfo roomInfo = mapMgr.RoomInformation[i];
                XElement roomInfoEle = new XElement("RoomInfo");
                roomInfoEle.Add(new XAttribute("Room", (i + 1).ToString()));
                roomInfoEle.Add(new XElement("Type", roomInfo.Type.ToString()));

                roomInfosEle.Add(roomInfoEle);
            }

            baseEle.Add(roomInfosEle);

            doc.Add(baseEle);
            doc.Save(saveStream);
            saveStream.Close();

            // Sadly we don't save everything so really this method doesn't work.
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MapMgr
    {
        // Some defaults.
        public const float DOOR_PROJ_DIST = 6f;
        /// <summary>
        /// The apex height of one of the hexagons.
        /// </summary>
        public const float HEX_APEX = 13.85f;
        public const float PLACEMENT_HEIGHT = 4f;
        private const float DOOR_OPEN_RADIUS = 5f;
        private const int NUM_ROOMS = 30;
        private const float PERIMETER_MIN_DIST_SQ = 900f;
        private const int PIT_HAZARD_COUNT = 2;
        private const int PIT_HAZARD_TRIVIA_INCORRECT_DAMAGE = 25;
        private const int PIT_HAZARD_DAMAGE = 1;

        /// <summary>
        /// The _door objs
        /// </summary>
        private List<DoorGameObj> _doorObjs;
        /// <summary>
        /// The _map graph
        /// </summary>
        private SparseGraph _mapGraph;
        /// <summary>
        /// The _map graph index
        /// </summary>
        private int _mapGraphIndex;
        /// <summary>
        /// The _power up MGR
        /// </summary>
        private PowerUpMgr _powerUpMgr;
        /// <summary>
        /// The _room infos
        /// </summary>
        private RoomInfo[] _roomInfos;
        /// <summary>
        /// The _room position
        /// </summary>
        private Vector3[] _roomPos;
        /// <summary>
        /// The b_always cull
        /// </summary>
        private bool b_alwaysCull = false;
        /// <summary>
        /// The i_door open count
        /// </summary>
        private int i_doorOpenCount = 0;
        /// <summary>
        /// The b_display guidlines
        /// </summary>
        private bool b_displayGuidlines = false;
        /// <summary>
        /// The p_disable guidelines proc
        /// </summary>
        private BaseLogic.Process.WaitEventProcess p_disableGuidelinesProc = null;

        /// <summary>
        /// The i_spawn wumpus room index
        /// </summary>
        private int i_spawnWumpusRoomIndex;
        /// <summary>
        /// The i_spawn player room index
        /// </summary>
        private int i_spawnPlayerRoomIndex;

        /// <summary>
        /// Gets or sets the disable guidelines proc.
        /// </summary>
        /// <value>
        /// The disable guidelines proc.
        /// </value>
        public BaseLogic.Process.WaitEventProcess DisableGuidelinesProc
        {
            get { return p_disableGuidelinesProc; }
            set { p_disableGuidelinesProc = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is displaying guidelines.
        /// The guidelines are the path leading the wumpus.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is displaying guidelines; otherwise, <c>false</c>.
        /// The guidelines are the path leading the wumpus.
        /// </value>
        public bool IsDisplayingGuidelines
        {
            get { return b_displayGuidlines; }
            set { b_displayGuidlines = value; }
        }

        /// <summary>
        /// Sets the index of the spawn player room.
        /// </summary>
        /// <value>
        /// The index of the spawn player room.
        /// </value>
        public int SpawnPlayerRoomIndex
        {
            set { i_spawnPlayerRoomIndex = value; }
        }

        /// <summary>
        /// Sets the index of the spawn wumpus room.
        /// </summary>
        /// <value>
        /// The index of the spawn wumpus room.
        /// </value>
        public int SpawnWumpusRoomIndex
        {
            set { i_spawnWumpusRoomIndex = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [always cull].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [always cull]; otherwise, <c>false</c>.
        /// </value>
        public bool AlwaysCull
        {
            get { return b_alwaysCull; }
            set { b_alwaysCull = value; }
        }

        /// <summary>
        /// Gets the door open count.
        /// </summary>
        /// <value>
        /// The door open count.
        /// </value>
        public int DoorOpenCount
        {
            get { return i_doorOpenCount; }
        }

        /// <summary>
        /// Gets the doors.
        /// </summary>
        /// <value>
        /// The doors.
        /// </value>
        public List<DoorGameObj> Doors
        {
            get { return _doorObjs; }
        }

        /// <summary>
        /// Gets the map graph.
        /// </summary>
        /// <value>
        /// The map graph.
        /// </value>
        public SparseGraph MapGraph
        {
            get { return _mapGraph; }
        }

        /// <summary>
        /// Gets the index of the map graph.
        /// </summary>
        /// <value>
        /// The index of the map graph.
        /// </value>
        public int MapGraphIndex
        {
            get { return _mapGraphIndex; }
        }

        /// <summary>
        /// Gets the power up manager.
        /// </summary>
        /// <value>
        /// The power up manager.
        /// </value>
        public PowerUpMgr PowerUpMgr
        {
            get { return _powerUpMgr; }
        }

        /// <summary>
        /// Gets the room information.
        /// </summary>
        /// <value>
        /// The room information.
        /// </value>
        public RoomInfo[] RoomInformation
        {
            get { return _roomInfos; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MapMgr"/> class.
        /// </summary>
        public MapMgr()
        {
        }

        /// <summary>
        /// Gets a 1 in possibility chance.
        /// </summary>
        /// <param name="possiblility">The possiblility of the event happening.</param>
        /// <returns>Whether the event happened.</returns>
        public static bool GetChance(int possiblility)
        {
            int random = ThreadSafeRandom.ThisThreadsRandom.Next(0, possiblility);
            if (random == 0)
                return true;
            return false;
        }

        /// <summary>
        /// Gets the door from number int.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        public static int GetDoorFromNumberInt(string id)
        {
            string[] splitStr = id.Split(':');
            if (splitStr[0] != "door")
                throw new ArgumentException();

            string numsStr = splitStr[1];
            string fromStr = numsStr.Split(',')[0];
            int from;
            if (!int.TryParse(fromStr, out from))
                throw new ArgumentException();
            return from;
        }

        /// <summary>
        /// Gets the door from string.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static string GetDoorFromStr(string id)
        {
            string[] splitStr = id.Split(':');
            if (splitStr[0] != "door")
                throw new ArgumentException();

            string numsStr = splitStr[1];
            string fromStr = numsStr.Split(',')[0];
            return fromStr;
        }

        /// <summary>
        /// Gets the door to number int.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        public static int GetDoorToNumberInt(string id)
        {
            string[] splitStr = id.Split(':');
            if (splitStr[0] != "door")
                throw new ArgumentException();

            string numsStr = splitStr[1];
            string toStr = numsStr.Split(',')[1];
            int to;
            if (!int.TryParse(toStr, out to))
                throw new ArgumentException();
            return to;
        }

        /// <summary>
        /// Gets the door to string.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static string GetDoorToStr(string id)
        {
            string[] splitStr = id.Split(':');
            if (splitStr[0] != "door")
                throw new ArgumentException();

            string numsStr = splitStr[1];
            string toStr = numsStr.Split(',')[1];
            return toStr;
        }

        /// <summary>
        /// Gets the random room number.
        /// </summary>
        /// <returns></returns>
        public static int GetRandomRoomNum()
        {
            return ThreadSafeRandom.ThisThreadsRandom.Next(1, NUM_ROOMS + 1);
        }

        /// <summary>
        /// Gets the room number int.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static int GetRoomNumberInt(string id)
        {
            string[] splitStr = id.Split(':');
            if (splitStr[0] != "room")
                throw new ArgumentException();

            string numStr = splitStr[1];

            int num;
            if (!int.TryParse(numStr, out num))
                return -1;
            return num;
        }

        /// <summary>
        /// Gets the room number string.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static string GetRoomNumberStr(string id)
        {
            string[] splitStr = id.Split(':');
            if (splitStr[0] != "room")
                throw new ArgumentException();

            return splitStr[1];
        }

        /// <summary>
        /// Determines whether [is door identifier] [the specified identifier].
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static bool IsDoorId(string id)
        {
            return Regex.IsMatch(id, @"room:+[\d],+[\d]");
        }

        /// <summary>
        /// Determines whether [is room identifier] [the specified identifier].
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static bool IsRoomId(string id)
        {
            return Regex.IsMatch(id, @"room:+[\d]");
        }

        /// <summary>
        /// Determines whether [is wall identifier] [the specified identifier].
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static bool IsWallId(string id)
        {
            return Regex.IsMatch(id, @"wall:+[\d]");
        }

        /// <summary>
        /// Switches the door identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static string SwitchDoorId(string id)
        {
            string toStr = GetDoorToStr(id);
            string fromStr = GetDoorFromStr(id);

            return string.Format("door:{0},{1}", fromStr, toStr);
        }

        /// <summary>
        /// Creates the doors.
        /// Needed for static map generation as well.
        /// </summary>
        /// <param name="gameSys">The game system.</param>
        public void CreateDoors(BaseLogic.GameSystem gameSys)
        {
            BaseLogic.Manager.ObjectMgr objMgr = gameSys.ObjMgr;

            _doorObjs = new List<DoorGameObj>();

            var doorStaticObjs = from go in objMgr.GetDataElements()
                                 where go is BaseLogic.Object.DoorObj
                                 select go as BaseLogic.Object.DoorObj;

            foreach (var doorStaticObj in doorStaticObjs)
            {
                DoorGameObj doorObj = new DoorGameObj();
                doorObj.LoadContent(doorStaticObj, DOOR_OPEN_RADIUS, gameSys);

                _doorObjs.Add(doorObj);
            }
        }

        /// <summary>
        /// Spawns the player.
        /// </summary>
        /// <param name="game">The game.</param>
        public void SpawnPlayer(XnaGame game)
        {
            MoveToRoom(i_spawnPlayerRoomIndex, game.GetGameUserPlayer());
        }

        /// <summary>
        /// Spawns the wumpus.
        /// </summary>
        /// <param name="game">The game.</param>
        public void SpawnWumpus(XnaGame game)
        {
            MoveToRoom(i_spawnWumpusRoomIndex, game.WumpusPlayer);
        }

        /// <summary>
        /// Gets the button hints for the player.
        /// This is stuff like press 'A' to play and stuff like that.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        public string GetButtonHint(Input input, XnaGame game)
        {
            GameUserPlayer userPlayer = game.GetGameUserPlayer();
            if (userPlayer == null)
                return null;
            // Get the correct interaction display key.
            string interactionKeyStr = input.InputMgr.UsingGamePad ? "B" : "X";

            // First get the interaction prompts for the doors.
            foreach (DoorGameObj doorObj in _doorObjs)
            {
                if (doorObj.IsInRegion() && !doorObj.InAnimation && !doorObj.InOpenState)
                {
                    string stateStr = doorObj.InOpenState ? "close" : "open";
                    string msgStr = "Press " + interactionKeyStr + " to {0} door";
                    msgStr = String.Format(msgStr, stateStr);
                    return msgStr;
                }
            }

            // Next get the interaction prompts for the holdable objects.
            var holdableObjs = from gameObj in game.GameSystem.ObjMgr.GetDataElements()
                               where gameObj is BaseLogic.Object.HoldableObj
                               select gameObj as BaseLogic.Object.HoldableObj;

            foreach (var holdableObj in holdableObjs)
            {
                if (userPlayer.IsHoldingObj && holdableObj.ActorID == userPlayer.HoldingObjId)
                {
                    continue;
                }
                if (userPlayer.DistanceTo(holdableObj) < DOOR_OPEN_RADIUS)
                {
                    string msgStr = "Press " + interactionKeyStr + " to hold " + holdableObj.GetIdentifier();
                    return msgStr;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the current room of the user player.
        /// </summary>
        /// <returns></returns>
        public int GetCurrentRoom()
        {
            var userPlayer = BaseLogic.GameSystem.GameSys_Instance.GetUserPlayer();
            return GetRoomNumOf(userPlayer);
        }

        /// <summary>
        /// Displays the guide lines leading the player to the wumpus.
        /// </summary>
        /// <param name="primitivesRenderer">The primitives renderer.</param>
        /// <param name="user">The user.</param>
        /// <param name="wumpus">The wumpus.</param>
        public void DisplayGuideLines(BaseLogic.Graphics.PrimitivesRenderer primitivesRenderer, GameUserPlayer user, WumpusGameObj wumpus)
        {
            if (!b_displayGuidlines)
                return;

            // Both the wumpus and player must be alive for this to work.
            if (wumpus == null || user == null)
            {
                return;
            }

            // Do a graph search.
            GraphSearchDijkstra search = new GraphSearchDijkstra();
            // Find the graph node index of the player and the wumpus.
            int userIndex = GetRoomNumOf(user) - 1;
            int wumpusIndex = GetRoomNumOf(wumpus) - 1;
            // Construct the path.
            search.ConstructPath(_mapGraph, userIndex, wumpusIndex);

            List<int> nodePath = search.GetPathToTarget();

            // Ensure we have a valid path.
            if (nodePath == null || nodePath.Count < 2)
                return;

            // All we need to display is the next move for the player.
            List<DoorGameObj> doors = GetGameDoorsForNodeIndex(userIndex);
            int nextToRoomNum = nodePath[1] + 1;
            Vector3 nextToPos = Vector3.Zero;
            foreach (DoorGameObj door in doors)
            {
                if (door.To == nextToRoomNum || door.From == nextToRoomNum)
                {
                    nextToPos = door.Position;
                }
            }

            Vector3 lineStart = user.Position;
            Vector3 lineEnd = nextToPos;
            const float offsetY = 2.0f;

            lineStart.Y = lineEnd.Y = offsetY;

            // Add the render line for rendering.
            primitivesRenderer.AddRenderLine(lineStart, lineEnd, Color.Red);
        }

        /// <summary>
        /// Gets the index of the disabled doors for room.
        /// </summary>
        /// <param name="roomIndex">Index of the room.</param>
        /// <returns></returns>
        public List<DoorGameObj> GetDisabledDoorsForRoomIndex(int roomIndex)
        {
            List<DoorGameObj> allDoors = GetGameDoorsForRoomIndex(roomIndex);
            List<DoorGameObj> enabledDoors = GetGameDoorsForNodeIndex(roomIndex);

            for (int i = 0; i < allDoors.Count; ++i)
            {
                foreach (DoorGameObj enabledDoor in enabledDoors)
                {
                    if (enabledDoor == allDoors[i])
                    {
                        allDoors.RemoveAt(i--);
                        break;
                    }
                }
            }

            return allDoors;
        }

        /// <summary>
        /// Gets the index of the game doors for node.
        /// </summary>
        /// <param name="nodeIndex">Index of the node.</param>
        /// <returns></returns>
        public List<DoorGameObj> GetGameDoorsForNodeIndex(int nodeIndex)
        {
            // Get all rooms this room is connected to.
            IEnumerable<GraphEdge> indexEdges = _mapGraph.GetNodeEdges(nodeIndex);

            List<DoorGameObj> doors = new List<DoorGameObj>();
            foreach (GraphEdge edge in indexEdges)
            {
                // With the doors we only store a door for one direction.
                // i.e. door:11,17 exists but door:17,11 doesn't as there is
                // only one door between those rooms.
                DoorGameObj possibleDoor1 = GetDoorGameObj(edge.To, edge.From);
                DoorGameObj possibleDoor2 = GetDoorGameObj(edge.From, edge.To);

                if (possibleDoor1 != null && possibleDoor2 != null)
                {
                    // We have an edge door.
                    // [from] [to]
                    doors.Add(possibleDoor1);
                    continue;
                }

                // Okay, just found about the null coalescing operator after almost a year
                // of C# programming...
                DoorGameObj door = possibleDoor1 ?? possibleDoor2;
                doors.Add(door);
            }

            return doors;
        }

        /// <summary>
        /// Returns a list of doors disregarding the graph information.
        /// </summary>
        /// <param name="roomIndex">Index of the room.</param>
        /// <returns></returns>
        public List<DoorGameObj> GetGameDoorsForRoomIndex(int roomIndex)
        {
            // Rely on if there is a potential door there not if there is an edge.
            List<DoorGameObj> doors = new List<DoorGameObj>();

            var objMgr = GameSystem.GameSys_Instance.ObjMgr;

            foreach (DoorGameObj door in _doorObjs)
            {
                if (door.To - 1 == roomIndex || door.From - 1 == roomIndex)
                    doors.Add(door);
            }

            return doors;
        }

        /// <summary>
        /// Gets the pit hazard room indices.
        /// </summary>
        /// <returns></returns>
        public List<int> GetPitHazardRoomIndices()
        {
            List<int> pitHazardIndices = new List<int>();

            for (int i = 0; i < NUM_ROOMS; ++i)
            {
                if (_roomInfos[i].Type == RoomInfo.RoomType.PitHazard)
                    pitHazardIndices.Add(i);
            }

            return pitHazardIndices;
        }

        /// <summary>
        /// Gets the random index of the safe empty.
        /// </summary>
        /// <param name="orbletMgr">The orblet MGR.</param>
        /// <returns></returns>
        public int GetRandomSafeEmptyIndex(OrbletMgr orbletMgr)
        {
            List<int> safeIndices = new List<int>();

            var orbletPositions = from orblet in orbletMgr.GetDataElements()
                                  where orblet is TeleportationOrblet
                                  select orblet.Position;

            List<int> notSafeIndices = new List<int>();
            foreach (Vector3 pos in orbletPositions)
            {
                int orbletRoom = GetRoomNumOf(pos);
                notSafeIndices.Add(orbletRoom - 1);
            }

            for (int i = 0; i < _roomInfos.Count(); ++i)
            {
                RoomInfo roomInfo = _roomInfos[i];

                if (roomInfo.IsSafe() && !notSafeIndices.Contains(i) && i != 6)
                    safeIndices.Add(i);
            }

            int randomIndex = ThreadSafeRandom.ThisThreadsRandom.Next(0, safeIndices.Count);

            return safeIndices[randomIndex];
        }

        /// <summary>
        /// Gets the random index of the safe.
        /// </summary>
        /// <returns></returns>
        public int GetRandomSafeIndex()
        {
            List<int> safeIndices = new List<int>();

            for (int i = 0; i < _roomInfos.Count(); ++i)
            {
                RoomInfo roomInfo = _roomInfos[i];

                if (roomInfo.IsSafe())
                    safeIndices.Add(i);
            }

            int randomIndex = ThreadSafeRandom.ThisThreadsRandom.Next(0, safeIndices.Count);

            return safeIndices[randomIndex];
        }

        /// <summary>
        /// Gets the room.
        /// </summary>
        /// <param name="roomNum">The room number.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public GameObj GetRoom(int roomNum)
        {
            var objMgr = BaseLogic.GameSystem.GameSys_Instance.ObjMgr;

            var rooms = from obj in objMgr.GetDataElements()
                        where IsRoomId(obj.ActorID)
                        where GetRoomNumberInt(obj.ActorID) == roomNum
                        select obj;

            if (rooms.Count() > 1)
                throw new ArgumentException();

            return rooms.ElementAt(0);
        }

        /// <summary>
        /// Gets the room number of.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <returns></returns>
        public int GetRoomNumOf(GameActor actor)
        {
            return GetRoomNumOf(actor.Position);
        }

        /// <summary>
        /// Gets the room number of.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public int GetRoomNumOf(Vector3 point)
        {
            int closestIndex = -1;
            float closestDist = float.MaxValue;

            for (int i = 0; i < _roomPos.Count(); ++i)
            {
                Vector3 roomPos = _roomPos[i];
                float dist = Vector3.DistanceSquared(point, roomPos);

                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestIndex = i;
                }
            }

            return closestIndex + 1;
        }

        /// <summary>
        /// Gets the room position.
        /// </summary>
        /// <param name="roomIndex">Index of the room.</param>
        /// <returns></returns>
        public Vector3 GetRoomPos(int roomIndex)
        {
            return _roomPos[roomIndex];
        }

        /// <summary>
        /// Ins the range of door.
        /// </summary>
        /// <param name="distanceSq">The distance sq.</param>
        /// <param name="actor">The actor.</param>
        /// <returns></returns>
        public DoorGameObj InRangeOfDoor(float distanceSq, GameActor actor)
        {
            foreach (DoorGameObj doorObj in _doorObjs)
            {
                if (doorObj.IsInRegion(actor.Position))
                    return doorObj;
            }
            return null;
        }

        /// <summary>
        /// Determines whether [is perimeter room] [the specified room index].
        /// </summary>
        /// <param name="roomIndex">Index of the room.</param>
        /// <returns></returns>
        public bool IsPerimeterRoom(int roomIndex)
        {
            // Andrew taking the lazy way out of things...

            // DON'T TAKE THIS AS AN EXAMPLE.

            // The following is a break in the room generation being dynamic.

            // None of the other room generation code use any preassumed door
            // positions or connections. The following is an exception.

            // Why you may ask? It's 3 a.m. and Andrew has had enough...

            // These were found by just referencing the minimap.
            int[] perimeterRooms =
            {
                6, 1, 11, 16, 21, 26, 27, 28, 30, 29, 24, 19, 14, 4, 10, 9, 7, 8
            };

            return perimeterRooms.Contains(roomIndex + 1);
        }

        /// <summary>
        /// Loads the content and all map information.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="loadFilename">The load filename.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void LoadContent(XnaGame game, string loadFilename)
        {
            _powerUpMgr = new PowerUpMgr();

            BaseLogic.GameSystem gameSys = game.GameSystem;

            CreateDoors(gameSys);

            BaseLogic.Manager.ObjectMgr objMgr = gameSys.ObjMgr;

            var gameObjs = objMgr.GetDataElements();

            var roomGameObjPositions = from go in gameObjs
                                       where IsRoomId(go.ActorID)
                                       select go.Position;

            List<Vector3> roomPositions = roomGameObjPositions.ToList();

            if (roomPositions.Count() != NUM_ROOMS)
                throw new ArgumentException();

            _roomPos = roomGameObjPositions.ToArray();
            _roomInfos = new RoomInfo[NUM_ROOMS];
            _mapGraph = new SparseGraph(true);

            MapContentSerilizer.LoadMap(loadFilename, this);

            CreateGraphEdges();
            AssociateCullings(game.GameSystem);

            _mapGraph.Serilize = false;
            (gameSys.AIMgr as Xna_Game_AI.AIMgrImpl).PathGraphs.Add(_mapGraph);
            _mapGraphIndex = (gameSys.AIMgr as Xna_Game_AI.AIMgrImpl).PathGraphs.Count - 1;

            List<int> pitHazardsIndices = GetPitHazardRoomIndices();
            foreach (int pitHazardIndex in pitHazardsIndices)
            {
                Vector3 hazardPos = GetRoomPos(pitHazardIndex);
                hazardPos.Y = 0f;

                var soundEffectProc = XnaGame.Game_Instance.SoundHelper.Play3DSoundEffect("RunningWater", true, hazardPos, 1225f, 1.0f);
                soundEffectProc.Serilize = false;
            }
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void LoadContent(XnaGame game)
        {
            _powerUpMgr = new PowerUpMgr();

            BaseLogic.GameSystem gameSys = game.GameSystem;

            CreateDoors(gameSys);

            BaseLogic.Manager.ObjectMgr objMgr = gameSys.ObjMgr;

            var gameObjs = objMgr.GetDataElements();

            var roomGameObjPositions = from go in gameObjs
                                       where IsRoomId(go.ActorID)
                                       select go.Position;

            List<Vector3> roomPositions = roomGameObjPositions.ToList();

            if (roomPositions.Count() != NUM_ROOMS)
                throw new ArgumentException();

            _roomPos = roomGameObjPositions.ToArray();

            _roomInfos = new RoomInfo[NUM_ROOMS];
            for (int i = 0; i < NUM_ROOMS; ++i)
                _roomInfos[i] = RoomInfo.Empty;

            CreateDoorConnections();

            GenerateRoomContent(game);

            (gameSys.AIMgr as Xna_Game_AI.AIMgrImpl).PathGraphs.Add(_mapGraph);
            _mapGraphIndex = (gameSys.AIMgr as Xna_Game_AI.AIMgrImpl).PathGraphs.Count - 1;
        }

        /// <summary>
        /// Moves to random room.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="orbletMgr">The orblet MGR.</param>
        public void MoveToRandomRoom(GameActor actor, OrbletMgr orbletMgr)
        {
            int index = GetRandomSafeEmptyIndex(orbletMgr);

            MoveToRoom(index, actor);
        }

        /// <summary>
        /// Moves to random room avoid.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="orbletMgr">The orblet MGR.</param>
        /// <param name="avoidObj">The avoid object.</param>
        /// <param name="minDist">The minimum dist.</param>
        public void MoveToRandomRoomAvoid(GameActor actor, OrbletMgr orbletMgr, GameActor avoidObj, float minDist)
        {
            int index = GetRandomSafeEmptyIndex(orbletMgr);

            int avoidRoomIndex = GetRoomNumOf(avoidObj) - 1;

            Vector3 avoidRoomPos = GetRoomPos(avoidRoomIndex);
            Vector3 thisRoomPos = GetRoomPos(index);
            while (Vector3.Distance(avoidRoomPos, thisRoomPos) > minDist)
            {
                index = GetRandomSafeEmptyIndex(orbletMgr);
                thisRoomPos = GetRoomPos(index);
            }

            MoveToRoom(index, actor);
        }

        /// <summary>
        /// Moves to room.
        /// </summary>
        /// <param name="roomIndex">Index of the room.</param>
        /// <param name="gameObj">The game object.</param>
        public void MoveToRoom(int roomIndex, GameActor gameObj)
        {
            Vector3 pos = _roomPos[roomIndex];
            pos.Y += PLACEMENT_HEIGHT;

            gameObj.Position = pos;
        }

        /// <summary>
        /// Called when [interaction button].
        /// </summary>
        /// <param name="game">The game.</param>
        public void OnInteractionButton(XnaGame game)
        {
            foreach (DoorGameObj doorObj in _doorObjs)
            {
                if (doorObj.IsInRegion())
                {
                    if (doorObj.InOpenState)
                        continue;

                    // Ask trivia question first.
                    if (!doorObj.InOpenState)
                    {
                        if (GetChance(2))
                        {
                            game.DisplayTriviaQuestion((bool correct) =>
                            {
                                if (correct)
                                    UseDoor(doorObj);
                                else
                                    game.GetGameUserPlayer().GoldCount--;
                            });
                        }
                        else
                        {
                            UseDoor(doorObj);
                            game.GetGameUserPlayer().GoldCount++;
                        }
                    }
                    break;
                }
            }

            GameUserPlayer userPlayer = game.GetGameUserPlayer();
            var holdableObjs = from gameObj in game.GameSystem.ObjMgr.GetDataElements()
                               where gameObj is BaseLogic.Object.HoldableObj
                               select gameObj as BaseLogic.Object.HoldableObj;

            foreach (var holdableObj in holdableObjs)
            {
                if (userPlayer.IsHoldingObj && holdableObj.ActorID == userPlayer.HoldingObjId)
                {
                    continue;
                }
                if (userPlayer.DistanceTo(holdableObj) < DOOR_OPEN_RADIUS)
                {
                    userPlayer.SetHoldingObj(holdableObj);
                }
            }
        }

        /// <summary>
        /// Opens all doors.
        /// </summary>
        public void OpenAllDoors()
        {
            foreach (DoorGameObj doorObj in _doorObjs)
            {
                if (!doorObj.InOpenState && doorObj.Enabled)
                    doorObj.ToggleDoorState();
            }
        }

        /// <summary>
        /// Rooms the contains hazards.
        /// </summary>
        /// <param name="roomIndex">Index of the room.</param>
        /// <param name="orbletMgr">The orblet MGR.</param>
        /// <returns></returns>
        public bool RoomContainsHazards(int roomIndex, OrbletMgr orbletMgr)
        {
            var orbletPositions = from orblet in orbletMgr.GetDataElements()
                                  where orblet is TeleportationOrblet
                                  select orblet.Position;

            foreach (Vector3 pos in orbletPositions)
            {
                int orbletRoom = GetRoomNumOf(pos);
                if (orbletRoom == roomIndex + 1)
                    return true;
            }

            if (_roomInfos[roomIndex].Type == RoomInfo.RoomType.PitHazard)
                return true;

            return false;
        }

        /// <summary>
        /// Saves the content.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void SaveContent(string filename)
        {
            MapContentSerilizer.SaveMap(filename, this);
        }

        /// <summary>
        /// Shuts all doors in the map.
        /// </summary>
        public void ShutAllDoors()
        {
            foreach (DoorGameObj doorObj in _doorObjs)
            {
                if (doorObj.InOpenState && doorObj.Enabled)
                    doorObj.ToggleDoorState();
            }
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            const float doorShutDistThresh = 100f;

            XnaGame game = XnaGame.Game_Instance;

            if (p_disableGuidelinesProc != null && p_disableGuidelinesProc.Kill)
            {
                p_disableGuidelinesProc = null;
            }

            GameUserPlayer userPlayer = game.GetGameUserPlayer();
            if (userPlayer == null)
                return;

            _powerUpMgr.Update(gameTime, userPlayer);

            int userPlayerRoomIndex = GetRoomNumOf(userPlayer) - 1;
            List<int> hazardRoomIndices = GetPitHazardRoomIndices();

            if (hazardRoomIndices.Contains(userPlayerRoomIndex))
                userPlayer.Health -= PIT_HAZARD_DAMAGE;

            foreach (DoorGameObj doorObj in _doorObjs)
            {
                doorObj.Update(gameTime);

                if (!doorObj.InOpenState)
                    continue;

                Vector2 projPointPlayer = new Vector2(userPlayer.Position.X, userPlayer.Position.Z);
                Vector2 projPointDoor = new Vector2(doorObj.Position.X, doorObj.Position.Z);

                if (Vector2.DistanceSquared(projPointPlayer, projPointDoor) > doorShutDistThresh)
                {
                    doorObj.OnCloseDoor();
                }
            }

            Cull(game, userPlayer);
        }

        /// <summary>
        /// Associates the cullings.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        private void AssociateCullings(GameSystem gameSystem)
        {
            // Associate the culling data of the game objects based on what room they are closest to.
            var allObjects = gameSystem.ObjMgr.GetDataElements();
            var placementObjs = from gameObj in allObjects
                                where !IsDoorId(gameObj.ActorID)
                                where !IsRoomId(gameObj.ActorID)
                                where !IsWallId(gameObj.ActorID)
                                where !(gameObj is BaseLogic.Object.WaterObject)
                                select gameObj;
            foreach (var placementObj in placementObjs)
            {
                int roomIndex = GetRoomNumOf(placementObj) - 1;
                _roomInfos[roomIndex].AddCullingObj(placementObj);
            }

            // Assocate the culling data of the particle system based on what room they are closest to.
            var allParticleSystems = gameSystem.ParticleMgr.GetDataElements();
            foreach (var particleSystem in allParticleSystems)
            {
                int roomIndex = GetRoomNumOf(particleSystem) - 1;
                _roomInfos[roomIndex].AddCullingObj(particleSystem);
            }

            var allGameLights = from light in gameSystem.LightMgr.GetDataElements()
                                where light.LightID != GameUserPlayer.PLAYER_LIGHT_ID
                                select light;
            var spotLights = from gameLight in allGameLights
                             where gameLight is SpotLight
                             select (gameLight as SpotLight);
            var pointLights = from gameLight in allGameLights
                              where gameLight is PointLight
                              select (gameLight as PointLight);

            foreach (var spotLight in spotLights)
            {
                int roomIndex = GetRoomNumOf(spotLight.Position) - 1;
                _roomInfos[roomIndex].AddCullingObj(spotLight);
            }

            foreach (var pointLight in pointLights)
            {
                int roomIndex = GetRoomNumOf(pointLight.Position) - 1;
                _roomInfos[roomIndex].AddCullingObj(pointLight);
            }

            for (int i = 0; i < NUM_ROOMS; ++i)
            {
                _roomInfos[i].AddCullingObj(GetRoom(i + 1));
            }
        }

        /// <summary>
        /// Creates the door connections.
        /// </summary>
        private void CreateDoorConnections()
        {
            // Get all of the doors.
            List<int[]> connectionsReg = new List<int[]>();
            foreach (var door in _doorObjs)
            {
                int[] connection = { door.To, door.From };
                connectionsReg.Add(connection);
            }

            // Switch all the connections to account for all edges.
            List<int[]> connectionsRev = new List<int[]>();
            foreach (int[] connection in connectionsReg)
            {
                int[] connectionRev = { connection[1], connection[0] };
                connectionsRev.Add(connectionRev);
            }

            List<int[]> finalConnections = new List<int[]>();
            foreach (int[] con in connectionsReg)
                finalConnections.Add(con);
            foreach (int[] con in connectionsRev)
                finalConnections.Add(con);

            _mapGraph = new SparseGraph(true);

            // The nodes of the graphs are the center of the rooms.
            foreach (Vector3 pos in _roomPos)
                _mapGraph.AddNode(pos);

            // The edges are the doors which are between the rooms.
            // Right now every room is connected to every other room.
            foreach (int[] con in finalConnections)
                _mapGraph.AddEdge(con[0] - 1, con[1] - 1);

            // Randomly scramble the nodes order in our calculations to create a more random feel.
            IEnumerable<int> nodeIndicesEn = from node in _mapGraph.Nodes
                                             select node.Index;
            List<int> nodeIndices = nodeIndicesEn.ToList();
            nodeIndices.Shuffle();

            for (int i = 0; i < PIT_HAZARD_COUNT; ++i)
            {
                // As we are creating our pit hazards make sure we don't use a perimeter room.
                int randomRoom;
                do
                {
                    randomRoom = GetRandomRoomNum();
                } while (!_roomInfos[randomRoom - 1].IsEmpty() || IsPerimeterRoom(randomRoom - 1));

                IEnumerable<GraphEdge> associatedEdges = _mapGraph.GetNodeEdges(randomRoom - 1);

                // Pit hazards are different we have to keep the doors so the player can wander into the
                // room, although the edge connection must be removed for navigational purposes.
                foreach (GraphEdge edge in associatedEdges)
                {
                    _mapGraph.RemoveEdge(edge.From, edge.To);
                    _mapGraph.RemoveEdge(edge.To, edge.From);
                }

                _roomInfos[randomRoom - 1] = new RoomInfo(RoomInfo.RoomType.PitHazard);
            }

            for (int i = 0; i < nodeIndices.Count; ++i)
            {
                int index = nodeIndices[i];

                List<DoorGameObj> doors = GetGameDoorsForNodeIndex(index);

                // How many doors are we going to have to disable.
                IEnumerable<DoorGameObj> enabledDoorsEn = from door in doors
                                                          where door.Enabled
                                                          select door;
                List<DoorGameObj> enabledDoors = enabledDoorsEn.ToList();
                // Keep in mind we cannot have less than three doors enabled.
                // (So we will never get a negative)
                const int doorsToDisableMax = 3;
                int disableDoorCount = enabledDoors.Count - doorsToDisableMax;
                for (int j = 0; j < disableDoorCount; ++j)
                {
                    // Get a random door as to randomize the doors which are disabled.
                    int randomIndex = ThreadSafeRandom.ThisThreadsRandom.Next(0, enabledDoors.Count);
                    DoorGameObj door = enabledDoors[randomIndex];
                    int doorTo = door.To - 1;
                    int doorFrom = door.From - 1;

                    // Check if this door can be disabled with the map still working.
                    int toIndex = doorTo != index ? door.To : door.From;

                    int toDisabledCount = GetNumberOfDisabledDoors(toIndex);
                    if (toDisabledCount < doorsToDisableMax)
                    {
                        if (!_mapGraph.RemoveEdge(doorTo, doorFrom) || !_mapGraph.RemoveEdge(doorFrom, doorTo))
                        {
                            continue;
                        }

                        // Can the map still function without this door connection?
                        GraphSearchDijkstra search = new GraphSearchDijkstra();
                        // Get the rooms which have already had the door diables.
                        List<int> testedNodeIndices = nodeIndices.GetRange(0, i);
                        bool pathFound = testedNodeIndices.Count == 0;
                        for (int k = 0; k < testedNodeIndices.Count; ++k)
                        {
                            int testedNodeIndex = testedNodeIndices[k];
                            search.ConstructPath(_mapGraph, index, testedNodeIndex);
                            pathFound = search.GetPathToTarget() != null;
                            // We just have to ensure we can connect to the rest of the map.
                            // We can asume the rest of the map can connect amoungst each other already.
                            if (pathFound)
                                break;
                        }

                        if (!pathFound)
                        {
                            // Can't connect to the rest of the map with
                            // removing this connection.
                            // Restore the connection.
                            _mapGraph.AddEdge(doorTo, doorFrom);
                            _mapGraph.AddEdge(doorFrom, doorTo);
                        }
                        else
                        {
                            // We found a door which can be disabled.
                            door.Enabled = false;
                        }
                    }

                    enabledDoors.RemoveAt(randomIndex);
                }
            }
        }

        /// <summary>
        /// Creates the graph edges based on the game door information.
        /// </summary>
        private void CreateGraphEdges()
        {
            foreach (DoorGameObj door in _doorObjs)
            {
                if (!door.Enabled)
                    continue;

                int toIndex = door.To - 1;
                int fromIndex = door.From - 1;

                _mapGraph.AddEdge(toIndex, fromIndex);
                _mapGraph.AddEdge(fromIndex, toIndex);
            }

            // Remove the edges to the pit hazard rooms.
            List<int> pitHazardRoomIndices = GetPitHazardRoomIndices();

            foreach (int pitHazardRoomIndex in pitHazardRoomIndices)
            {
                IEnumerable<GraphEdge> connectingEdges = _mapGraph.GetNodeEdges(pitHazardRoomIndex);
                foreach (GraphEdge removeEdge in connectingEdges)
                {
                    _mapGraph.RemoveEdge(removeEdge.From, removeEdge.To);
                    _mapGraph.RemoveEdge(removeEdge.To, removeEdge.From);
                }
            }
        }

        /// <summary>
        /// Gets the associated wall.
        /// </summary>
        /// <param name="doorGameObj">The door game object.</param>
        /// <param name="objMgr">The object MGR.</param>
        private void GetAssociatedWall(DoorGameObj doorGameObj, BaseLogic.Manager.ObjectMgr objMgr)
        {
        }

        /// <summary>
        /// Culls the specified game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="userPlayer">The user player.</param>
        private void Cull(XnaGame game, GameUserPlayer userPlayer)
        {
            if (b_alwaysCull || !game.DebugMode)
            {
                // Culling is active.
                // Only render the rooms adjacent to the player.
                int roomNum = GetRoomNumOf(userPlayer);

                List<DoorGameObj> doors = GetGameDoorsForNodeIndex(roomNum - 1);

                List<int> roomsToKeep = new List<int>();
                List<int> roomsNextTo = new List<int>();
                foreach (DoorGameObj door in doors)
                {
                    roomsToKeep.Add(door.To);
                    roomsToKeep.Add(door.From);
                }

                doors = GetGameDoorsForRoomIndex(roomNum - 1);
                foreach (DoorGameObj door in doors)
                {
                    roomsNextTo.Add(door.To);
                    roomsNextTo.Add(door.From);
                }

                roomsToKeep = roomsToKeep.Distinct().ToList();
                roomsNextTo = roomsNextTo.Distinct().ToList();

                bool pitHazardInSight = false;

                // Sucks to suck everyone else gets culled.
                for (int i = 0; i < NUM_ROOMS; ++i)
                {
                    bool cull = !roomsToKeep.Contains(i + 1);
                    // We always have to render pit hazards as there is no connection
                    // between them and the map therefore they are never a possible travel
                    // point for the player.
                    if (_roomInfos[i].Type == RoomInfo.RoomType.PitHazard)
                    {
                        if (roomsNextTo.Contains(i + 1))
                        {
                            pitHazardInSight = true;
                            roomsToKeep.Add(i + 1);
                            cull = false;
                        }
                        else
                        {
                            cull = true;
                        }
                    }

                    _roomInfos[i].SetCulling(cull);
                    _roomInfos[i].SetSoundCulling((roomNum - 1) != i);
                }

                // The water is VERY expensive to water so cull it at all costs.
                var objMgr = game.GameSystem.ObjMgr;
                var waterObj = objMgr.GetDataElement("wa wa") as BaseLogic.Object.WaterObject;
                waterObj.Enabled = pitHazardInSight;

                // See if we can cull any of the doors.
                for (int i = 0; i < _doorObjs.Count; ++i)
                {
                    DoorGameObj door = _doorObjs[i];
                    int to = door.To;
                    int from = door.From;

                    door.RenderEnabled = roomsToKeep.Contains(to) || roomsToKeep.Contains(from);

                    // Get the associated wall with this door.
                }
            }
            else
            {
                // Everyone gets to render!
                foreach (RoomInfo roomInfo in _roomInfos)
                {
                    roomInfo.SetCulling(false);
                    // Strict culling normally has to do with sound.
                    roomInfo.SetSoundCulling(true);
                }

                foreach (DoorGameObj door in _doorObjs)
                {
                    door.RenderEnabled = true;
                }
            }
        }

        /// <summary>
        /// Generates the content of the room.
        /// </summary>
        /// <param name="game">The game.</param>
        private void GenerateRoomContent(XnaGame game)
        {
            GameSystem gameSys = game.GameSystem;

            // Iteration: Create specialized rooms.
            for (int i = 0; i < NUM_ROOMS; ++i)
            {
                if (_roomInfos[i].IsEmpty())
                {
                    _roomInfos[i] = new RoomInfo(RoomInfo.RoomType.SceneOccupied);
                }
            }

            // Iteration: Create roofs for all.
            for (int i = 0; i < NUM_ROOMS; ++i)
            {
                _roomInfos[i].GenerateContent(i);
            }

            // Select two random rooms to spawn the teleporting orblets in.
            const int teleportingOrbletsCount = 2;
            for (int i = 0; i < teleportingOrbletsCount; ++i)
            {
                int safeRoomIndex = GetRandomSafeIndex();
                Vector3 spawnPos = GetRoomPos(safeRoomIndex);
                spawnPos.Y += 8f;
                game.OrbletMgr.SpawnOrblet(spawnPos, new TeleportationOrblet());
            }
        }

        /// <summary>
        /// Gets the door game object.
        /// </summary>
        /// <param name="toIndex">To index.</param>
        /// <param name="fromIndex">From index.</param>
        /// <returns></returns>
        private DoorGameObj GetDoorGameObj(int toIndex, int fromIndex)
        {
            foreach (DoorGameObj door in _doorObjs)
            {
                if ((door.To - 1) == toIndex && (door.From - 1) == fromIndex)
                    return door;
            }
            return null;
        }

        /// <summary>
        /// Gets the number of disabled doors.
        /// </summary>
        /// <param name="roomIndex">Index of the room.</param>
        /// <returns></returns>
        private int GetNumberOfDisabledDoors(int roomIndex)
        {
            int doorCount = _mapGraph.GetNodeEdges(roomIndex - 1).Count();

            return (6 - doorCount);
        }

        /// <summary>
        /// Called when [door open].
        /// </summary>
        private void OnDoorOpen()
        {
            i_doorOpenCount++;
        }

        /// <summary>
        /// Uses the door.
        /// </summary>
        /// <param name="doorGameObj">The door game object.</param>
        private void UseDoor(DoorGameObj doorGameObj)
        {
            // Is this a teleporting door?
            Vector3 fromPos = _roomPos[doorGameObj.From - 1];
            Vector3 toPos = _roomPos[doorGameObj.To - 1];
            float distSq = Vector3.DistanceSquared(fromPos, toPos);

            XnaGame game = XnaGame.Game_Instance;
            GameUserPlayer userPlayer = game.GetGameUserPlayer();

            // Is this a trap?
            GraphEdge edge1 = _mapGraph.GetEdge(doorGameObj.From - 1, doorGameObj.To - 1);
            GraphEdge edge2 = _mapGraph.GetEdge(doorGameObj.To - 1, doorGameObj.From - 1);

            bool isNavToTrap = edge1 == null && edge2 == null;

            if (isNavToTrap)
            {
                // Regular door.
                doorGameObj.ToggleDoorState();
                if (doorGameObj.InOpenState)
                    OnDoorOpen();

                // It's a trap!
                Vector3 room1Pos = GetRoomPos(doorGameObj.From - 1);
                Vector3 room2Pos = GetRoomPos(doorGameObj.To - 1);

                float room1Dist = Vector3.DistanceSquared(room1Pos, userPlayer.Position);
                float room2Dist = Vector3.DistanceSquared(room2Pos, userPlayer.Position);

                Vector3 travelingTo = room1Dist < room2Dist ? room2Pos : room1Pos;

                // This is pretty messy but whatever.
                // I would recommend changing sometime soon.
                game.OrbletMgr.HelperOrblet.SpeakWithPlayer(userPlayer, HelperOrblet.DLG_CHAIN_PIT_TRIVIA, travelingTo, (string eventFiredStr) =>
                {
                    game.DisplayTriviaQuestion(3, (int correctCount) =>
                    {
                        if (correctCount >= 2)
                        {
                            game.OrbletMgr.HelperOrblet.SpeakWithPlayer(userPlayer, HelperOrblet.DLG_CHAIN_PIT_CORRECT, travelingTo);
                        }
                        else
                        {
                            // The player will be seriously hurt here... or will lose i haven't decided which i want yet...
                            game.OrbletMgr.HelperOrblet.SpeakWithPlayer(userPlayer, HelperOrblet.DLG_CHAIN_PIT_INCORRECT, travelingTo);
                            userPlayer.Health -= PIT_HAZARD_TRIVIA_INCORRECT_DAMAGE;
                        }

                        game.OrbletMgr.HelperOrblet.Dismiss(game);
                    }, false);
                });
            }
            else if (distSq > PERIMETER_MIN_DIST_SQ)
            {
                // Teleporting door.
                string objId = String.Format("door:{0},{1}", doorGameObj.To, doorGameObj.From);
                GameObj gameObj =
                    BaseLogic.GameSystem.GameSys_Instance.ObjMgr.GetDataElement(objId);
                // Project just in front of the door.

                Vector3 localPos = new Vector3(-DOOR_PROJ_DIST, userPlayer.Position.Y, 0f);
                Quaternion rot1 = gameObj.Rotation;
                Quaternion rot2 = gameObj.Rotation * Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathHelper.Pi);

                Vector3 proj1 = Vector3.Transform(localPos, rot1) + gameObj.Position;
                Vector3 proj2 = Vector3.Transform(localPos, rot2) + gameObj.Position;

                float dist1 = Vector3.DistanceSquared(proj1, toPos);
                float dist2 = Vector3.DistanceSquared(proj2, toPos);

                Vector3 pos = dist1 < dist2 ? proj1 : proj2;

                userPlayer.Position = pos;
            }
            else
            {
                // Regular door.
                doorGameObj.ToggleDoorState();
                if (doorGameObj.InOpenState)
                    OnDoorOpen();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RoomInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public enum RoomType { None, PitHazard, SceneOccupied };

        /// <summary>
        /// The _game lights
        /// </summary>
        private List<GameLight> _gameLights = new List<GameLight>();
        /// <summary>
        /// The _game procs
        /// </summary>
        private List<BaseLogic.Process.GameProcess> _gameProcs = new List<BaseLogic.Process.GameProcess>();
        /// <summary>
        /// The _particle systems
        /// </summary>
        private List<GameParticleSystem> _particleSystems = new List<GameParticleSystem>();
        /// <summary>
        /// The _room type
        /// </summary>
        private RoomType _roomType;
        /// <summary>
        /// The _static objs
        /// </summary>
        private List<StaticObj> _staticObjs = new List<StaticObj>();
        /// <summary>
        /// The b_generate roof
        /// </summary>
        private bool b_generateRoof = true;
        /// <summary>
        /// The v_room center
        /// </summary>
        private Vector3 v_roomCenter;

        /// <summary>
        /// Gets the empty.
        /// </summary>
        /// <value>
        /// The empty.
        /// </value>
        public static RoomInfo Empty
        {
            get { return new RoomInfo(RoomType.None); }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public RoomType Type
        {
            get { return _roomType; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomInfo"/> class.
        /// </summary>
        /// <param name="roomType">Type of the room.</param>
        public RoomInfo(RoomType roomType)
        {
            _roomType = roomType;
        }

        /// <summary>
        /// Adds the culling object.
        /// </summary>
        /// <param name="gameActor">The game actor.</param>
        public void AddCullingObj(GameActor gameActor)
        {
            if (gameActor is StaticObj)
                _staticObjs.Add(gameActor as StaticObj);
            else if (gameActor is GameParticleSystem)
            {
                GameParticleSystem particleSys = gameActor as GameParticleSystem;
                _particleSystems.Add(gameActor as GameParticleSystem);
                if (particleSys.ActorID.Contains("fire"))
                {
                    var soundEffectProc = XnaGame.Game_Instance.SoundHelper.Play3DSoundEffect("TorchFire", true, particleSys.Position, 0.6f);
                    soundEffectProc.Serilize = false;
                    _gameProcs.Add(soundEffectProc);
                }
            }
        }

        /// <summary>
        /// Adds the culling object.
        /// </summary>
        /// <param name="gameLight">The game light.</param>
        public void AddCullingObj(GameLight gameLight)
        {
            _gameLights.Add(gameLight);
        }

        /// <summary>
        /// Generates the content.
        /// </summary>
        /// <param name="roomIndex">Index of the room.</param>
        public void GenerateContent(int roomIndex)
        {
            MapMgr mapMgr = XnaGame.Game_Instance.MapMgr;
            if (_roomType != RoomType.None)
            {
                GameSystem gameSystem = GameSystem.GameSys_Instance;
                GenerateRoomContent(gameSystem, mapMgr, roomIndex + 1);
            }

            if (b_generateRoof)
                GenerateRoof();

            StaticObj roomObj = mapMgr.GetRoom(roomIndex + 1) as StaticObj;
            _staticObjs.Add(roomObj);
        }

        /// <summary>
        /// Generates the roof.
        /// </summary>
        public void GenerateRoof()
        {
            GameSystem gameSys = GameSystem.GameSys_Instance;

            StaticObj roofObj = gameSys.CreateStaticObj("hexagon");
            roofObj.SubsetMaterials[0].DiffuseMap = new GameTexture("images/granite");
            roofObj.SubsetMaterials[0].TexTransform = Matrix.CreateScale(3f);
            roofObj.SetMass(float.PositiveInfinity);
            Vector3 roofPos = v_roomCenter;
            roofPos.Y += 12f;
            roofObj.Position = roofPos;
            roofObj.Scale = 2f;

            _staticObjs.Add(roofObj);
        }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return (_roomType == RoomType.None);
        }

        /// <summary>
        /// Determines whether this instance is safe.
        /// </summary>
        /// <returns></returns>
        public bool IsSafe()
        {
            if (_roomType == RoomType.PitHazard)
                return false;
            return true;
        }

        /// <summary>
        /// Sets the sound culling.
        /// </summary>
        /// <param name="cull">if set to <c>true</c> [cull].</param>
        public void SetSoundCulling(bool cull)
        {
            foreach (var gameProc in _gameProcs)
            {
                if ((gameProc is BaseLogic.Process.SoundEffectProcess))
                {
                    if (cull && !gameProc.Paused)
                        gameProc.TogglePaused();
                    else if (!cull && gameProc.Paused)
                        gameProc.TogglePaused();
                }
            }
        }

        /// <summary>
        /// Sets the culling.
        /// </summary>
        /// <param name="cull">if set to <c>true</c> [cull].</param>
        public void SetCulling(bool cull)
        {
            //foreach (GameLight gameLight in _gameLights)
            //    gameLight.Enabled = !cull;
            foreach (StaticObj staticObj in _staticObjs)
                staticObj.Enabled = !cull;
            foreach (var gameProc in _gameProcs)
            {
                if (!(gameProc is BaseLogic.Process.SoundEffectProcess))
                    gameProc.Paused = cull;
            }
            foreach (var particleSys in _particleSystems)
                particleSys.Enabled = !cull;
        }

        /// <summary>
        /// Generates the content of the room.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        /// <param name="mapMgr">The map MGR.</param>
        /// <param name="roomNum">The room number.</param>
        private void GenerateRoomContent(GameSystem gameSystem, MapMgr mapMgr, int roomNum)
        {
            v_roomCenter = mapMgr.GetRoomPos(roomNum - 1);

            switch (_roomType)
            {
                case RoomType.PitHazard:
                    GenerateRoomPitHazard(gameSystem, mapMgr, roomNum);
                    break;

                case RoomType.SceneOccupied:
                    // Create one of the random scenery rooms we have at our disposal.
                    int randRoom = ThreadSafeRandom.ThisThreadsRandom.Next(0, 4);
                    switch (randRoom)
                    {
                        case 0:
                            GenerateRoomScenery1(gameSystem, mapMgr, roomNum);
                            break;

                        case 1:
                            GenerateRoomScenery2(gameSystem, mapMgr, roomNum);
                            break;

                        case 2:
                            GenerateRoomScenery3(gameSystem, mapMgr, roomNum);
                            break;

                        case 3:
                            GenerateRoomScenery4(gameSystem, mapMgr, roomNum);
                            break;
                    }

                    break;
            }

            if (MapMgr.GetChance(10))
                mapMgr.PowerUpMgr.PlacePowerUp(roomNum - 1, gameSystem, mapMgr, new HealthPowerUp());
        }

        /// <summary>
        /// Generates the room pit hazard.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        /// <param name="mapMgr">The map MGR.</param>
        /// <param name="roomNum">The room number.</param>
        private void GenerateRoomPitHazard(GameSystem gameSystem, MapMgr mapMgr, int roomNum)
        {
            GameObj roomObj = mapMgr.GetRoom(roomNum);
            Vector3 roomPos = roomObj.Position;
            roomPos.Y -= 16.8f;
            roomObj.Position = roomPos;

            StaticObj roomStaticObj = roomObj as StaticObj;
            roomStaticObj.SubsetMaterials[0].DiffuseMap = new GameTexture("images/DiffuseMaps/ground_diffuse");
            roomStaticObj.SubsetMaterials[0].NormalMap = new GameTexture("images/NormalMaps/ground_normal");
            roomStaticObj.SubsetMaterials[0].SpecularMap = new GameTexture("images/SpecularMaps/ground_specular");

            Vector3 lightPos = roomPos;
            lightPos.Y += 12f;
            PointLight pointLight = new PointLight(lightPos, 20f, 1f, 1f, Color.Coral, "Pool Light R: " + roomNum.ToString());
            gameSystem.LightMgr.AddToList(pointLight);

            List<DoorGameObj> doors = mapMgr.GetGameDoorsForRoomIndex(roomNum - 1);

            foreach (DoorGameObj door in doors)
            {
                var wallObj = gameSystem.CreateStaticObj("wall");
                wallObj.Position = door.Position - new Vector3(0f, 16f, 0f);
                wallObj.Rotation = door.Rotation;
                wallObj.SetMass(float.PositiveInfinity);
                wallObj.Scale = 2.0f;
                wallObj.SubsetMaterials[0].DiffuseMap = new GameTexture("images/fieldstone-c");
                wallObj.SubsetMaterials[0].NormalMap = new GameTexture("images/NormalMaps/fieldstone-n");
                wallObj.SubsetMaterials[0].TexTransform = Matrix.CreateScale(3f);

                _staticObjs.Add(wallObj);
            }
        }

        /// <summary>
        /// Generates the room scenery1.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        /// <param name="mapMgr">The map MGR.</param>
        /// <param name="roomNum">The room number.</param>
        private void GenerateRoomScenery1(GameSystem gameSystem, MapMgr mapMgr, int roomNum)
        {
            Vector3 tubeLightPos = v_roomCenter;
            tubeLightPos.Y += 10.8f;
            StaticObj tubeLight = gameSystem.CreateStaticObj("tubelight", "tube light R:" + roomNum.ToString());
            tubeLight.Position = tubeLightPos;
            tubeLight.SubsetMaterials[0].UseDiffuseMat = tubeLight.SubsetMaterials[1].UseDiffuseMat
                = tubeLight.SubsetMaterials[1].UseAlphaMask = true;
            tubeLight.SubsetMaterials[0].DiffuseMat = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
            tubeLight.SubsetMaterials[1].DiffuseMat = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            tubeLight.Scale = 2f;
            tubeLight.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitX, -MathHelper.PiOver2);

            Vector3 lightPos = tubeLightPos;
            lightPos.Y -= 1.0f;
            PointLight pointLight = new PointLight(lightPos, 15f, 0.7f, 0.7f, Color.White,
                "tube light light R:" + roomNum.ToString());
            SpotLight spotLight = new SpotLight(lightPos, new Vector3(-MathHelper.PiOver2, 0f, 0f),
                Color.White, 20f, 1f, 1f, true, "spot light R: " + roomNum.ToString());
            spotLight.SpotConeAngle = 70f;
            gameSystem.LightMgr.AddToList(pointLight);
            gameSystem.LightMgr.AddToList(spotLight);

            // There is a chance the light can be flashing.
            if (MapMgr.GetChance(4))
            {
                uint freq = 1000;
                uint dur = 100;
                uint randomStartOffset = (uint)ThreadSafeRandom.ThisThreadsRandom.Next(0, (int)freq);
                MakeFlashingLight(pointLight, freq, dur, randomStartOffset, gameSystem);
                MakeFlashingLight(spotLight, freq, dur, randomStartOffset, gameSystem);
            }

            _staticObjs.Add(tubeLight);
            _gameLights.Add(pointLight);
        }

        /// <summary>
        /// Generates the room scenery2.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        /// <param name="mapMgr">The map MGR.</param>
        /// <param name="roomNum">The room number.</param>
        private void GenerateRoomScenery2(GameSystem gameSystem, MapMgr mapMgr, int roomNum)
        {
        }

        /// <summary>
        /// Generates the room scenery3.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        /// <param name="mapMgr">The map MGR.</param>
        /// <param name="roomNum">The room number.</param>
        private void GenerateRoomScenery3(GameSystem gameSystem, MapMgr mapMgr, int roomNum)
        {
            b_generateRoof = false;

            int roomIndex = roomNum - 1;

            List<DoorGameObj> doors = mapMgr.GetDisabledDoorsForRoomIndex(roomIndex);

            if (doors.Count != 0)
            {
                int randomIndex = ThreadSafeRandom.NextInt(0, doors.Count);

                DoorGameObj placementDoor = doors[randomIndex];

                Vector3 roomPos = mapMgr.GetRoomPos(roomIndex);

                Quaternion halfRot = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathHelper.Pi);
                Quaternion quaterRot = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathHelper.PiOver2);
                Quaternion eigthRot = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathHelper.PiOver4);

                Vector3 localStatuePos = new Vector3(3f, 0f, 6f);
                Vector3 localHeadLightPos = new Vector3(4f, 9f, 6f);
                Vector3 benchLocalPos = new Vector3(3f, 0.8f, -6f);

                //Vector3 localGroundLightPos = new Vector3(-7f, 1f, 4f);
                //Quaternion groundLightLocalRot = Quaternion.CreateFromAxisAngle(Vector3.UnitY, -MathHelper.PiOver4);
                //Vector3 spotLightLocalPos = new Vector3(-7.2f, 1f, 4f);

                Quaternion doorRot = placementDoor.GetRotationForPoint(roomPos, localStatuePos);
                StaticObj statue = gameSystem.CreateStaticObj("statue");
                statue.Scale = 1.5f;
                statue.Position = Vector3.Transform(localStatuePos, doorRot);
                statue.Position += placementDoor.Position;
                statue.Rotation = doorRot * halfRot * eigthRot;

                Vector3 headLightPos = Vector3.Transform(localHeadLightPos, doorRot) + placementDoor.Position;
                PointLight headLight = new PointLight(headLightPos, 15f, 1f, 1f, BaseLogic.ColorHelper.GetRandomColor());
                gameSystem.LightMgr.AddToList(headLight);

                doorRot = placementDoor.GetRotationForPoint(roomPos, benchLocalPos);
                StaticObj bench = gameSystem.CreateStaticObj("bench");
                bench.Scale = 1.5f;
                bench.Position = Vector3.Transform(benchLocalPos, doorRot) + placementDoor.Position;
                bench.Rotation = doorRot * Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathHelper.ToRadians(150f));
                bench.SubsetMaterials[0].DiffuseMap = new GameTexture("images/DiffuseMaps/BenchDiffuse");

                //StaticObj groundLight = gameSystem.CreateStaticObj("groundlight");
                //groundLight.Scale = 0.4f;
                //groundLight.Position = Vector3.Transform(localGroundLightPos, placementDoor.Rotation);
                //groundLight.Position += placementDoor.Position;
                //groundLight.Rotation = placementDoor.Rotation * groundLightLocalRot;
                //groundLight.SubsetMaterials[0].DiffuseMat = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);
                //groundLight.SubsetMaterials[0].DiffuseMat = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
                //groundLight.SubsetMaterials[1].UseDiffuseMat = groundLight.SubsetMaterials[0].UseDiffuseMat = true;

                //Vector3 spotLightPos = Vector3.Transform(spotLightLocalPos, placementDoor.Rotation) + placementDoor.Position;
                //Vector3 spotLightRot = new Vector3(MathHelper.PiOver4, MathHelper.ToRadians(0f), 0f);
                //spotLightRot = Vector3.Transform(spotLightRot, placementDoor.Rotation);
                //SpotLight spotLight = new SpotLight(spotLightPos, spotLightRot, Color.Green, 10f, 1f, 1f, true);

                //gameSystem.LightMgr.AddToList(spotLight);

                _staticObjs.Add(statue);
                _staticObjs.Add(bench);
                _gameLights.Add(headLight);
                //_gameLights.Add(spotLight);
                //_staticObjs.Add(groundLight);

                if (doors.Count < 2)
                    return;

                int prevIndex = randomIndex;
                do
                {
                    randomIndex = ThreadSafeRandom.NextInt(0, doors.Count);
                } while (randomIndex == prevIndex);

                placementDoor = doors[randomIndex];

                roomPos = mapMgr.GetRoomPos(roomIndex);

                Vector3 barrelLocalPos = new Vector3(-2f, 1f, -5f);
                doorRot = placementDoor.GetRotationForPoint(roomPos, barrelLocalPos);
                StaticObj barrel = gameSystem.CreateStaticObjAbsoluteFilename("Models/Barrel/barrel", "barrel R: " + roomIndex.ToString());
                barrel.Position = Vector3.Transform(barrelLocalPos, doorRot) + placementDoor.Position;
                barrel.Rotation = doorRot;
                barrel.Scale = 1f;
                barrel.SubsetMaterials[0].DiffuseMap = new GameTexture("images/DiffuseMaps/PlanksNe");
                barrel.SubsetMaterials[1].DiffuseMap = new GameTexture("images/DiffuseMaps/rust");
                barrel.SubsetMaterials[2].DiffuseMap = new GameTexture("images/DiffuseMaps/PlanksNe");

                _staticObjs.Add(barrel);
            }
        }

        /// <summary>
        /// Generates the room scenery4.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        /// <param name="mapMgr">The map MGR.</param>
        /// <param name="roomNum">The room number.</param>
        private void GenerateRoomScenery4(GameSystem gameSystem, MapMgr mapMgr, int roomNum)
        {
            int roomIndex = roomNum - 1;
            List<DoorGameObj> doors = mapMgr.GetGameDoorsForNodeIndex(roomIndex);

            foreach (DoorGameObj door in doors)
            {
                StaticObj torchObj1 = gameSystem.CreateStaticObj("torchstand");
                torchObj1.Position = Vector3.Transform(new Vector3(0.3f, 6f, 4f), door.Rotation);
                torchObj1.Position += door.Position;
                torchObj1.Rotation = door.Rotation;
                torchObj1.Scale = 0.3f;
                torchObj1.SubsetMaterials[0].DiffuseMat = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
                torchObj1.SubsetMaterials[1].DiffuseMat = Color.Silver.ToVector4();
                torchObj1.SubsetMaterials[1].UseDiffuseMat = torchObj1.SubsetMaterials[0].UseDiffuseMat = true;
                torchObj1.SubsetMaterials[2].DiffuseMap = new GameTexture("images/granite");
                torchObj1.SubsetMaterials[2].TexTransform = Matrix.CreateScale(1f, 2f, 1f);
                Vector3 torchLightPos = Vector3.Transform(new Vector3(1.5f, 6f, 4f), door.Rotation);
                torchLightPos += door.Position;
                torchLightPos.Y += 1f;
                GameLight torchLight1 = new PointLight(torchLightPos, 10f, 0.5f, 0.5f, Color.Orange);
                gameSystem.LightMgr.AddToList(torchLight1);

                var fire1 = gameSystem.CreateParticleSystem("FireSettings", Guid.NewGuid().ToString());
                fire1.Position = torchLightPos;

                var smoke1 = gameSystem.CreateParticleSystem("SmokePlumeSettings", Guid.NewGuid().ToString());
                smoke1.Position = torchLightPos;

                StaticObj torchObj2 = (StaticObj)torchObj1.Clone();
                torchObj2.Position = Vector3.Transform(new Vector3(0.3f, 6f, -4f), door.Rotation);
                torchObj2.Position += door.Position;
                gameSystem.AddRenderObj(torchObj2);
                torchLightPos = Vector3.Transform(new Vector3(1.5f, 6f, -4f), door.Rotation);
                torchLightPos += door.Position;
                torchLightPos.Y += 1f;
                GameLight torchLight2 = new PointLight(torchLightPos, 10f, 0.5f, 0.5f, Color.Orange);
                gameSystem.LightMgr.AddToList(torchLight2);

                var fire2 = gameSystem.CreateParticleSystem("FireSettings", Guid.NewGuid().ToString());
                fire2.Position = torchLightPos;

                var smoke2 = gameSystem.CreateParticleSystem("SmokePlumeSettings", Guid.NewGuid().ToString());
                smoke2.Position = torchLightPos;

                _particleSystems.Add(fire1);
                _particleSystems.Add(fire2);
                _particleSystems.Add(smoke1);
                _particleSystems.Add(smoke2);

                _staticObjs.Add(torchObj2);
                _staticObjs.Add(torchObj1);
                _gameLights.Add(torchLight1);
                _gameLights.Add(torchLight2);
            }
        }

        /// <summary>
        /// Makes the flashing light.
        /// </summary>
        /// <param name="gameLight">The game light.</param>
        /// <param name="freq">The freq.</param>
        /// <param name="dur">The dur.</param>
        /// <param name="gameSystem">The game system.</param>
        public void MakeFlashingLight(GameLight gameLight, uint freq, uint dur, GameSystem gameSystem)
        {
            uint randomStartOffset = (uint)ThreadSafeRandom.ThisThreadsRandom.Next(0, (int)freq);
            BaseLogic.Process.FlashingLightProcess flashingLightProc =
                new BaseLogic.Process.FlashingLightProcess(freq, dur, gameLight, randomStartOffset);
            gameSystem.AddGameProcess(flashingLightProc);
            flashingLightProc.Serilize = false;
            _gameProcs.Add(flashingLightProc);
        }

        /// <summary>
        /// Makes the flashing lights.
        /// </summary>
        /// <param name="gameLights">The game lights.</param>
        /// <param name="freq">The freq.</param>
        /// <param name="dur">The dur.</param>
        /// <param name="gameSystem">The game system.</param>
        public void MakeFlashingLights(GameLight[] gameLights, uint freq, uint dur, GameSystem gameSystem)
        {
            uint randomStartOffset = (uint)ThreadSafeRandom.ThisThreadsRandom.Next(0, (int)freq);
            foreach (GameLight gameLight in gameLights)
            {
                MakeFlashingLight(gameLight, freq, dur, randomStartOffset, gameSystem);
            }
        }

        /// <summary>
        /// Makes the flashing light.
        /// </summary>
        /// <param name="gameLight">The game light.</param>
        /// <param name="freq">The freq.</param>
        /// <param name="dur">The dur.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="gameSystem">The game system.</param>
        public void MakeFlashingLight(GameLight gameLight, uint freq, uint dur, uint offset, GameSystem gameSystem)
        {
            BaseLogic.Process.FlashingLightProcess flashingLightProc =
                new BaseLogic.Process.FlashingLightProcess(freq, dur, gameLight, offset);
            gameSystem.AddGameProcess(flashingLightProc);
            flashingLightProc.Serilize = false;
            _gameProcs.Add(flashingLightProc);
        }
    }
}