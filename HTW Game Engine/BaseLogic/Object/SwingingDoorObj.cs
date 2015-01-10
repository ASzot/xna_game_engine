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
    public class SwingingDoorObj : DoorObj
    {
        /// <summary>
        /// The doo r_ close d_ angl e_ de g_ definition
        /// </summary>
        private const float DOOR_CLOSED_ANGLE_DEG_DEF = 0f;

        /// <summary>
        /// The doo r_ ope n_ angl e_ de g_ definition
        /// </summary>
        private const float DOOR_OPEN_ANGLE_DEG_DEF = 90f;

        /// <summary>
        /// The doo r_ swin g_ spee d_ definition
        /// </summary>
        private const float DOOR_SWING_SPEED_DEF = 2f;

        /// <summary>
        /// The f_closed angle
        /// </summary>
        private float f_closedAngle;

        /// <summary>
        /// The f_open angle
        /// </summary>
        private float f_openAngle;

        /// <summary>
        /// The f_speed
        /// </summary>
        private float f_speed = DOOR_SWING_SPEED_DEF;

        /// <summary>
        /// The v_rot axis
        /// </summary>
        private Vector3 v_rotAxis = Vector3.UnitY;

        /// <summary>
        /// The v_rot axis offset
        /// </summary>
        private Vector3 v_rotAxisOffset = Vector3.Zero;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwingingDoorObj"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public SwingingDoorObj(string id)
            : base(id)
        {
            f_closedAngle = MathHelper.ToRadians(DOOR_CLOSED_ANGLE_DEG_DEF);
            f_openAngle = MathHelper.ToRadians(DOOR_OPEN_ANGLE_DEG_DEF);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwingingDoorObj"/> class.
        /// </summary>
        /// <param name="staticObj">The static object.</param>
        /// <param name="objMgr">The object MGR.</param>
        public SwingingDoorObj(StaticObj staticObj, Manager.ObjectMgr objMgr)
            : base(staticObj, objMgr)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwingingDoorObj"/> class.
        /// </summary>
        /// <param name="doorObjSaveData">The door object save data.</param>
        public SwingingDoorObj(SwingingDoorObjSaveData doorObjSaveData)
            : base(doorObjSaveData.StaticObjSaveData.ActorID)
        {
            FromDoorObjSaveData(doorObjSaveData);
        }

        /// <summary>
        /// Gets or sets the closed angle.
        /// </summary>
        /// <value>
        /// The closed angle.
        /// </value>
        public float ClosedAngle
        {
            get { return f_closedAngle; }
            set { f_closedAngle = value; }
        }

        /// <summary>
        /// Gets or sets the open angle.
        /// </summary>
        /// <value>
        /// The open angle.
        /// </value>
        public float OpenAngle
        {
            get { return f_openAngle; }
            set { f_openAngle = value; }
        }

        /// <summary>
        /// Gets or sets the rot axis.
        /// </summary>
        /// <value>
        /// The rot axis.
        /// </value>
        public Vector3 RotAxis
        {
            get { return v_rotAxis; }
            set { v_rotAxis = value; }
        }

        /// <summary>
        /// Gets or sets the rot axis offset.
        /// </summary>
        /// <value>
        /// The rot axis offset.
        /// </value>
        public Vector3 RotAxisOffset
        {
            get { return v_rotAxisOffset; }
            set { v_rotAxisOffset = value; }
        }

        /// <summary>
        /// Gets or sets the rot speed.
        /// </summary>
        /// <value>
        /// The rot speed.
        /// </value>
        public float RotSpeed
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
            SwingingDoorObj doorObj = new SwingingDoorObj(staticObj, GameSystem.GameSys_Instance.ObjMgr);
            doorObj.f_closedAngle = f_closedAngle;
            doorObj.f_openAngle = f_openAngle;
            doorObj.f_speed = f_speed;
            doorObj.v_rotAxis = v_rotAxis;
            doorObj.v_rotAxisOffset = v_rotAxisOffset;

            return doorObj;
        }

        /// <summary>
        /// Froms the door object save data.
        /// </summary>
        /// <param name="dosd">The dosd.</param>
        public void FromDoorObjSaveData(Object.SwingingDoorObjSaveData dosd)
        {
            FromStaticObjSaveData(dosd.StaticObjSaveData);
            f_speed = dosd.SwingSpeed;
            f_openAngle = dosd.OpenAngle;
            f_closedAngle = dosd.ClosedAngle;
            v_rotAxis = dosd.RotAxis;
            v_rotAxisOffset = dosd.RotAxisOffset;
        }

        /// <summary>
        /// Gets the closed value.
        /// </summary>
        /// <returns></returns>
        public override float GetClosedValue()
        {
            return f_openAngle;
        }

        /// <summary>
        /// Gets the door object save data.
        /// </summary>
        /// <returns></returns>
        public Object.SwingingDoorObjSaveData GetDoorObjSaveData()
        {
            Object.SwingingDoorObjSaveData dosd;
            dosd.StaticObjSaveData = GetStaticObjSaveData();
            dosd.ClosedAngle = f_closedAngle;
            dosd.OpenAngle = f_openAngle;
            dosd.RotAxis = v_rotAxis;
            dosd.SwingSpeed = f_speed;
            dosd.RotAxisOffset = v_rotAxisOffset;

            return dosd;
        }

        /// <summary>
        /// Gets the modifier proc.
        /// </summary>
        /// <param name="opening">if set to <c>true</c> [opening].</param>
        /// <returns></returns>
        public override Process.ObjModifierProcess GetModifierProc(bool opening)
        {
            float start;
            float stop;

            if (opening)
            {
                stop = f_openAngle;
                start = f_closedAngle;
            }
            else
            {
                stop = -f_openAngle;
                start = f_closedAngle;
            }

            return new Process.RotationModifierProcess(start, stop, this);
        }

        /// <summary>
        /// Gets the open value.
        /// </summary>
        /// <returns></returns>
        public override float GetOpenValue()
        {
            return f_closedAngle;
        }
    }
}