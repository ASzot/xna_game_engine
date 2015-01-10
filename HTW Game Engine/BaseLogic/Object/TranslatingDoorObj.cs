#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using Microsoft.Xna.Framework;

namespace BaseLogic.Object
{
    /// <summary>
    ///
    /// </summary>
    public class TranslatingDoorObj : DoorObj
    {
        /// <summary>
        /// The close d_ heigh t_ definition
        /// </summary>
        private const float CLOSED_HEIGHT_DEF = 0f;

        /// <summary>
        /// The ope n_ heigh t_ definition
        /// </summary>
        private const float OPEN_HEIGHT_DEF = -5f;

        /// <summary>
        /// The spee d_ definition
        /// </summary>
        private const float SPEED_DEF = 1f;

        /// <summary>
        /// The f_closed height
        /// </summary>
        private float f_closedHeight = CLOSED_HEIGHT_DEF;

        /// <summary>
        /// The f_open height
        /// </summary>
        private float f_openHeight = OPEN_HEIGHT_DEF;

        /// <summary>
        /// The f_speed
        /// </summary>
        private float f_speed = SPEED_DEF;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslatingDoorObj"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public TranslatingDoorObj(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslatingDoorObj"/> class.
        /// </summary>
        /// <param name="staticObj">The static object.</param>
        /// <param name="objMgr">The object MGR.</param>
        public TranslatingDoorObj(StaticObj staticObj, Manager.ObjectMgr objMgr)
            : base(staticObj, objMgr)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslatingDoorObj"/> class.
        /// </summary>
        /// <param name="saveData">The save data.</param>
        public TranslatingDoorObj(Object.TranslatingDoorObjSaveData saveData)
            : this(saveData.StaticObjSaveData.ActorID)
        {
            FromDoorObjSaveData(saveData);
        }

        /// <summary>
        /// Gets or sets the height of the closed.
        /// </summary>
        /// <value>
        /// The height of the closed.
        /// </value>
        public float ClosedHeight
        {
            get { return f_closedHeight; }
            set { f_closedHeight = value; }
        }

        /// <summary>
        /// Gets or sets the height of the open.
        /// </summary>
        /// <value>
        /// The height of the open.
        /// </value>
        public float OpenHeight
        {
            get { return f_openHeight; }
            set { f_openHeight = value; }
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
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override RenderingSystem.GameObj Clone()
        {
            StaticObj staticObj = base.CloneStaticObj();

            TranslatingDoorObj doorObj = new TranslatingDoorObj(staticObj, GameSystem.GameSys_Instance.ObjMgr);
            doorObj.f_closedHeight = f_closedHeight;
            doorObj.f_openHeight = f_openHeight;
            doorObj.f_speed = f_speed;

            return doorObj;
        }

        /// <summary>
        /// Froms the door object save data.
        /// </summary>
        /// <param name="saveData">The save data.</param>
        public void FromDoorObjSaveData(Object.TranslatingDoorObjSaveData saveData)
        {
            FromStaticObjSaveData(saveData.StaticObjSaveData);
            f_speed = saveData.Speed;
            f_openHeight = saveData.OpenHeight;
            f_closedHeight = saveData.ClosedHeight;
        }

        /// <summary>
        /// Gets the closed value.
        /// </summary>
        /// <returns></returns>
        public override float GetClosedValue()
        {
            return f_closedHeight;
        }

        /// <summary>
        /// Gets the door object save data.
        /// </summary>
        /// <returns></returns>
        public Object.TranslatingDoorObjSaveData GetDoorObjSaveData()
        {
            Object.TranslatingDoorObjSaveData saveData;
            saveData.StaticObjSaveData = GetStaticObjSaveData();
            saveData.Speed = f_speed;
            saveData.OpenHeight = f_openHeight;
            saveData.ClosedHeight = f_closedHeight;

            return saveData;
        }

        /// <summary>
        /// Gets the modifier proc.
        /// </summary>
        /// <param name="opening">if set to <c>true</c> [opening].</param>
        /// <returns></returns>
        public override Process.ObjModifierProcess GetModifierProc(bool opening)
        {
            Vector3 pos = Position;

            Vector3 endPos = pos;
            if (opening)
            {
                endPos.Y = f_openHeight;
            }
            else
            {
                endPos.Y = f_closedHeight;
            }

            return new Process.TranslationModifierProcess(endPos, this);
        }

        /// <summary>
        /// Gets the open value.
        /// </summary>
        /// <returns></returns>
        public override float GetOpenValue()
        {
            return f_openHeight;
        }
    }
}