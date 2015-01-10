#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using RM = RenderingSystem.ResourceMgr;

namespace BaseLogic.Object
{
    /// <summary>
    ///
    /// </summary>
    public abstract class DoorObj : StaticObj
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoorObj"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public DoorObj(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoorObj"/> class.
        /// </summary>
        /// <param name="staticObj">The static object.</param>
        /// <param name="objMgr">The object MGR.</param>
        public DoorObj(StaticObj staticObj, Manager.ObjectMgr objMgr)
            : this(staticObj.ActorID)
        {
            objMgr.RemoveDataElement(staticObj);

            this.LoadContent(RM.Content, staticObj.Filename);
            this.Position = staticObj.Position;
            this.Rotation = staticObj.Rotation;
            this.Scale = staticObj.Scale;
            this.SubsetMaterials = staticObj.SubsetMaterials;
            this.SerilizeObj = staticObj.SerilizeObj;
            this.b_massModified = staticObj.MassModified;
            this.SetMass(staticObj.RigidBody.MassProperties);
        }

        /// <summary>
        /// Clones the static object.
        /// </summary>
        /// <returns></returns>
        public StaticObj CloneStaticObj()
        {
            return (StaticObj)base.Clone();
        }

        /// <summary>
        /// Creates the door identifier.
        /// </summary>
        /// <param name="objMgr">The object MGR.</param>
        /// <param name="room1">The room1.</param>
        /// <param name="room2">The room2.</param>
        /// <returns></returns>
        public bool CreateDoorID(Manager.ObjectMgr objMgr, int room1, int room2)
        {
            string id = string.Format("Door:{0}:{1}", room1, room2);

            if (objMgr.ActorIDExists(id))
                return false;

            ActorID = id;

            return true;
        }

        /// <summary>
        /// Gets the closed value.
        /// </summary>
        /// <returns></returns>
        public abstract float GetClosedValue();

        /// <summary>
        /// Gets the modifier proc.
        /// </summary>
        /// <param name="opening">if set to <c>true</c> [opening].</param>
        /// <returns></returns>
        public abstract Process.ObjModifierProcess GetModifierProc(bool opening);

        /// <summary>
        /// Gets the open value.
        /// </summary>
        /// <returns></returns>
        public abstract float GetOpenValue();
    }
}