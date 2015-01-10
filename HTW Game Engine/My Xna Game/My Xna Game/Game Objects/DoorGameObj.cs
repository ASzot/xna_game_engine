#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Linq;
using BaseLogic;
using BaseLogic.Process;
using Microsoft.Xna.Framework;

namespace My_Xna_Game.Game_Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class DoorGameObj
    {
        private ObjModifierProcess _objModifierProc;
        private CircleShape _trigger;
        private bool b_enabled = true;
        private bool b_openState = false;
        private int i_from;
        private int i_to;
        private StaticObj p_associatedWall;
        private BaseLogic.Object.DoorObj p_doorObj;
        private BaseLogic.Player.UserPlayer p_userPlayer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoorGameObj"/> class.
        /// </summary>
        public DoorGameObj()
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DoorGameObj"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled
        {
            get
            {
                return b_enabled;
            }
            set
            {
                b_enabled = value;
                if (!b_enabled)
                {
                    // Become one of the walls.
                    p_doorObj.SubsetMaterials[0].DiffuseMap = new RenderingSystem.GameTexture("images/fieldstone-c");
                    p_doorObj.SubsetMaterials[0].NormalMap = new RenderingSystem.GameTexture("images/NormalMaps/fieldstone-n");
                    p_doorObj.SubsetMaterials[0].TexTransform = Matrix.CreateScale(3f);
                    p_doorObj.Scale = 1.98f;
                }
                else
                {
                    // Look like a door.
                    p_doorObj.SubsetMaterials[0].DiffuseMap = new RenderingSystem.GameTexture("images/granite");
                    p_doorObj.SubsetMaterials[0].NormalMap = new RenderingSystem.GameTexture("images/default_normal");
                    p_doorObj.SubsetMaterials[0].TexTransform = Matrix.CreateScale(2f);
                    p_doorObj.Scale = 1f;
                }
            }
        }

        /// <summary>
        /// Gets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public int From
        {
            get { return i_from; }
        }

        /// <summary>
        /// Gets a value indicating whether [in animation].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [in animation]; otherwise, <c>false</c>.
        /// </value>
        public bool InAnimation
        {
            get
            {
                return _objModifierProc != null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [in open state].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [in open state]; otherwise, <c>false</c>.
        /// </value>
        public bool InOpenState
        {
            get
            {
                return b_openState;
            }
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Vector3 Position
        {
            get { return p_doorObj.Position; }
        }

        /// <summary>
        /// Sets a value indicating whether [render enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [render enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool RenderEnabled
        {
            set
            {
                p_doorObj.Enabled = value;
                p_associatedWall.Enabled = value;
            }
        }

        /// <summary>
        /// Gets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        public Quaternion Rotation
        {
            get { return p_doorObj.Rotation; }
        }

        /// <summary>
        /// Gets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        public int To
        {
            get { return i_to; }
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="doorGameObj1">The door game obj1.</param>
        /// <param name="doorGameObj2">The door game obj2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(DoorGameObj doorGameObj1, DoorGameObj doorGameObj2)
        {
            return !(doorGameObj1 == doorGameObj2);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="doorGameObj1">The door game obj1.</param>
        /// <param name="doorGameObj2">The door game obj2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(DoorGameObj doorGameObj1, DoorGameObj doorGameObj2)
        {
            if ((object)doorGameObj1 == null && (object)doorGameObj2 == null)
                return true;

            if ((object)doorGameObj1 == null || (object)doorGameObj2 == null)
                return false;

            return (doorGameObj1.To == doorGameObj2.To) && (doorGameObj1.From == doorGameObj2.From);
        }

        /// <summary>
        /// Gets the rotation for point.
        /// </summary>
        /// <param name="referencePoint">The reference point.</param>
        /// <param name="testPoint">The test point.</param>
        /// <returns></returns>
        public Quaternion GetRotationForPoint(Vector3 referencePoint, Vector3 testPoint)
        {
            testPoint.Y = 0f;
            testPoint.Z = 0f;
            // Will get the rotation which will project closest to the reference point.
            Quaternion rot1 = this.Rotation;
            Quaternion rot2 = rot1 * Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathHelper.Pi);

            Vector3 proj1 = Vector3.Transform(testPoint, rot1);
            Vector3 proj2 = Vector3.Transform(testPoint, rot2);

            float dist1 = Vector3.DistanceSquared(referencePoint, proj1);
            float dist2 = Vector3.DistanceSquared(referencePoint, proj2);

            Quaternion actualRot = dist1 < dist2 ? rot1 : rot2;

            return actualRot;
        }

        /// <summary>
        /// Gets the teleportation point.
        /// </summary>
        /// <param name="closestPoint">The closest point.</param>
        /// <param name="transformOffset">The transform offset.</param>
        /// <returns></returns>
        public Vector3 GetTeleportationPoint(Vector3 closestPoint, Vector3 transformOffset)
        {
            Vector3 doorPos = Position;
            // Use the closed state.
            doorPos.Y = p_doorObj.GetClosedValue();

            Vector3 transformedPoint1 = Vector3.Transform(transformOffset, Rotation);
            transformedPoint1 += doorPos;

            // Just a rotation representing a rotation of 180 degrees.
            Quaternion halfRot = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathHelper.Pi);
            Quaternion totalRot = Rotation * halfRot;
            Vector3 transformedPoint2 = Vector3.Transform(transformOffset, totalRot);
            transformedPoint2 += doorPos;

            float distance1Sq = Vector3.DistanceSquared(transformedPoint1, closestPoint);
            float distance2Sq = Vector3.DistanceSquared(transformedPoint2, closestPoint);

            // We want to return the transformed point which is closest to the point the user desires.
            return (distance1Sq > distance2Sq) ? transformedPoint2 : transformedPoint1;
        }

        /// <summary>
        /// Determines whether [is in region] [the specified position].
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public bool IsInRegion(Vector3 position)
        {
            if (!b_enabled)
                return false;

            Vector2 pos2D = new Vector2(position.X, position.Z);
            if (_trigger.ContainsPoint(pos2D))
                return true;
            return false;
        }

        /// <summary>
        /// Determines whether [is in region].
        /// </summary>
        /// <returns></returns>
        public bool IsInRegion()
        {
            return IsInRegion(p_userPlayer.Position);
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="doorObj">The door object.</param>
        /// <param name="doorRadius">The door radius.</param>
        /// <param name="gameSys">The game system.</param>
        public void LoadContent(BaseLogic.Object.DoorObj doorObj, float doorRadius, GameSystem gameSys)
        {
            p_userPlayer = gameSys.GetUserPlayer();
            Vector2 doorPos2D = new Vector2(doorObj.Position.X, doorObj.Position.Z);
            _trigger = new CircleShape(doorPos2D, doorRadius);
            p_doorObj = doorObj;

            string id = p_doorObj.ActorID;
            i_to = Map.MapMgr.GetDoorToNumberInt(id);
            i_from = Map.MapMgr.GetDoorFromNumberInt(id);

            // Get the associated wall.
            var walls = from gameObj in gameSys.ObjMgr.GetDataElements()
                        where gameObj is StaticObj
                        where Map.MapMgr.IsWallId(gameObj.ActorID)
                        select gameObj as StaticObj;

            float closest = float.MaxValue;
            StaticObj closestWall = null;
            foreach (StaticObj wall in walls)
            {
                float distSq = Vector3.DistanceSquared(wall.Position, doorObj.Position);
                if (closest > distSq)
                {
                    closest = distSq;
                    closestWall = wall;
                }
            }

            p_associatedWall = closestWall;
        }

        /// <summary>
        /// Called when [close door].
        /// </summary>
        /// <param name="onCloseComplete">The on close complete.</param>
        public void OnCloseDoor(Action onCloseComplete = null)
        {
            if (!InAnimation)
            {
                _objModifierProc = p_doorObj.GetModifierProc(false);
                if (onCloseComplete != null)
                    _objModifierProc.OnModificationFinished += onCloseComplete;
                GameSystem.GameSys_Instance.AddGameProcess(_objModifierProc);
                b_openState = false;

                XnaGame.Game_Instance.SoundHelper.Play3DSoundEffect("DoorSlidingShort", false, this.Position, 1.0f);
            }
        }

        /// <summary>
        /// Called when [open door].
        /// </summary>
        /// <param name="onOpenComplete">The on open complete.</param>
        public void OnOpenDoor(Action onOpenComplete = null)
        {
            if (!InAnimation)
            {
                _objModifierProc = p_doorObj.GetModifierProc(true);
                if (onOpenComplete != null)
                    _objModifierProc.OnModificationFinished += onOpenComplete;
                GameSystem.GameSys_Instance.AddGameProcess(_objModifierProc);
                b_openState = true;

                XnaGame.Game_Instance.SoundHelper.Play3DSoundEffect("DoorSlidingShort", false, this.Position, 1.0f);
            }
        }

        /// <summary>
        /// Toggles the state of the door.
        /// </summary>
        public void ToggleDoorState()
        {
            if (b_openState)
            {
                OnCloseDoor();
            }
            else
            {
                OnOpenDoor();
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string openStr = b_openState ? "open" : "closed";
            string enabledStr = b_enabled ? "" : "disabled,";
            return openStr + "," + enabledStr + i_from.ToString() + ":" + i_to.ToString();
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            _trigger.Center = new Vector2(p_doorObj.Position.X, p_doorObj.Position.Z);
            if (InAnimation && _objModifierProc.Kill)
            {
                _objModifierProc = null;
            }
        }
    }
}