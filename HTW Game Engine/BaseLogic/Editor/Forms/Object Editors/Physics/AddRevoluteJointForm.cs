#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Linq;
using System.Windows.Forms;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddRevoluteJointForm : Form
    {
        /// <summary>
        /// The on close
        /// </summary>
        public Action OnClose;

        /// <summary>
        /// The p_game object
        /// </summary>
        private PhysObj p_gameObj;

        /// <summary>
        /// The p_obj MGR
        /// </summary>
        private Manager.ObjectMgr p_objMgr;

        /// <summary>
        /// The p_physics
        /// </summary>
        private Henge3D.Physics.PhysicsManager p_physics;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddRevoluteJointForm"/> class.
        /// </summary>
        /// <param name="pObjMgr">The p object MGR.</param>
        /// <param name="pGameObj">The p game object.</param>
        public AddRevoluteJointForm(Manager.ObjectMgr pObjMgr, PhysObj pGameObj)
        {
            p_gameObj = pGameObj;
            p_physics = pGameObj.RigidBody.Manager;
            p_objMgr = pObjMgr;
            InitializeComponent();

            UpdateListBoxes();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddRevoluteJointForm"/> class.
        /// </summary>
        /// <param name="pObjMgr">The p object MGR.</param>
        /// <param name="pGameObj">The p game object.</param>
        /// <param name="rj">The rj.</param>
        public AddRevoluteJointForm(Manager.ObjectMgr pObjMgr, PhysObj pGameObj, Manager.GameRevoluteJoint rj)
            : this(pObjMgr, pGameObj)
        {
            minAngleTxtBox.Text = rj.MinAngle.ToString();
            maxAngleTxtBox.Text = rj.MaxAngle.ToString();
            pointXTxtBox.Text = rj.WorldPoint.X.ToString();
            pointYTxtBox.Text = rj.WorldPoint.Y.ToString();
            pointZTxtBox.Text = rj.WorldPoint.Z.ToString();
            axisXTxtBox.Text = rj.Axis.X.ToString();
            axisYTxtBox.Text = rj.Axis.Y.ToString();
            axisZTxtBox.Text = rj.Axis.Z.ToString();

            var rb = rj.BodyB;
            var physicsObjs = from po in p_objMgr.GetDataElements()
                              where po is PhysObj
                              select po as PhysObj;
            int selectedIndex = -1;
            for (int i = 0; i < physicsObjs.Count(); ++i)
            {
                var physicObj = physicsObjs.ElementAt(i);
                if (physicObj.RigidBody == rb)
                    selectedIndex = i;
            }
            bodyBListBox.SelectedIndex = selectedIndex;
        }

        /// <summary>
        /// Handles the Click event of the acceptBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void acceptBtn_Click(object sender, EventArgs e)
        {
            float minAngle, maxAngle, pointX, pointY, pointZ, axisX, axisY, axisZ;
            bool success = true;

            if (!float.TryParse(minAngleTxtBox.Text, out minAngle))
                success = false;
            if (!float.TryParse(maxAngleTxtBox.Text, out maxAngle))
                success = false;
            if (!float.TryParse(pointXTxtBox.Text, out pointX))
                success = false;
            if (!float.TryParse(pointYTxtBox.Text, out pointY))
                success = false;
            if (!float.TryParse(pointZTxtBox.Text, out pointZ))
                success = false;
            if (!float.TryParse(axisXTxtBox.Text, out axisX))
                success = false;
            if (!float.TryParse(axisYTxtBox.Text, out axisY))
                success = false;
            if (!float.TryParse(axisZTxtBox.Text, out axisZ))
                success = false;

            int selectedIndex = bodyBListBox.SelectedIndex;
            PhysObj physObj = null;
            if (selectedIndex == -1)
                success = false;
            else
            {
                string actorID = (string)bodyBListBox.Items[selectedIndex];
                if (actorID == null || actorID == "")
                    success = false;

                var gameObj = p_objMgr.GetDataElement(actorID);
                if (!(gameObj is PhysObj))
                    success = false;
                physObj = gameObj as PhysObj;
            }

            if (!success)
            {
                MessageBox.Show("Invalid data input!", "Error");
                return;
            }

            minAngle = Microsoft.Xna.Framework.MathHelper.ToRadians(minAngle);
            maxAngle = Microsoft.Xna.Framework.MathHelper.ToRadians(maxAngle);

            Manager.GameRevoluteJoint rj = new Manager.GameRevoluteJoint(p_gameObj.RigidBody, physObj.RigidBody,
                new Microsoft.Xna.Framework.Vector3(pointX, pointY, pointZ),
                new Microsoft.Xna.Framework.Vector3(axisX, axisY, axisZ),
                minAngle,
                maxAngle);

            p_physics.Add(rj);

            closeBtn_Click(null, null);
        }

        /// <summary>
        /// Handles the Click event of the closeBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            if (OnClose != null)
                OnClose();
            this.Close();
        }

        /// <summary>
        /// Updates the list boxes.
        /// </summary>
        private void UpdateListBoxes()
        {
            var physicsObjNames = from po in p_objMgr.GetDataElements()
                                  where po is PhysObj
                                  select po.ActorID;
            bodyBListBox.Items.Clear();
            bodyBListBox.Items.AddRange(physicsObjNames.ToArray());
        }
    }
}