#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Microsoft.Xna.Framework;

using RenderingSystem;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class ObjGroupEditorForm : Form
    {
        /// <summary>
        /// The p_obj MGR
        /// </summary>
        private Manager.ObjectMgr p_objMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjGroupEditorForm"/> class.
        /// </summary>
        /// <param name="pObjMgr">The p object MGR.</param>
        public ObjGroupEditorForm(Manager.ObjectMgr pObjMgr)
        {
            InitializeComponent();

            p_objMgr = pObjMgr;

            objListBox.SelectionMode = SelectionMode.MultiExtended;

            UpdateListBox();
        }

        /// <summary>
        /// Handles the Click event of the applyPositionBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void applyPositionBtn_Click(object sender, EventArgs e)
        {
            Vector3 offsetPos;
            bool success = true;

            if (posXTxtBox.Text == "")
                posXTxtBox.Text = "0";
            if (posYTxtBox.Text == "")
                posYTxtBox.Text = "0";
            if (posZTxtBox.Text == "")
                posZTxtBox.Text = "0";

            if (!float.TryParse(posXTxtBox.Text, out offsetPos.X))
                success = false;
            if (!float.TryParse(posYTxtBox.Text, out offsetPos.Y))
                success = false;
            if (!float.TryParse(posZTxtBox.Text, out offsetPos.Z))
                success = false;

            if (!success)
            {
                RenderingSystem.Graphics.Forms.WindowsFormHelper.DisplayParseErrorMsg();
                return;
            }

            IEnumerable<int> selectedGameObjIndices = GetSelectedGameObjIndices();
            foreach (int gameObjIndex in selectedGameObjIndices)
            {
                p_objMgr.GetDataElementAt(gameObjIndex).Position += offsetPos;
            }
        }

        /// <summary>
        /// Handles the Click event of the applyRotationBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void applyRotationBtn_Click(object sender, EventArgs e)
        {
            Vector3 offsetRot;
            bool success = true;

            if (rotXTxtBox.Text == "")
                rotXTxtBox.Text = "0";
            if (rotYTxtBox.Text == "")
                rotYTxtBox.Text = "0";
            if (rotZTxtBox.Text == "")
                rotZTxtBox.Text = "0";

            if (!float.TryParse(rotXTxtBox.Text, out offsetRot.X))
                success = false;
            if (!float.TryParse(rotYTxtBox.Text, out offsetRot.Y))
                success = false;
            if (!float.TryParse(rotZTxtBox.Text, out offsetRot.Z))
                success = false;

            if (!success)
            {
                RenderingSystem.Graphics.Forms.WindowsFormHelper.DisplayParseErrorMsg();
                return;
            }

            // Convert Euler rotation to quaternion.
            Quaternion rotQuat = Quaternion.CreateFromYawPitchRoll(offsetRot.Y, offsetRot.X, offsetRot.Z);

            IEnumerable<int> selectedGameObjIndices = GetSelectedGameObjIndices();
            foreach (int gameObjIndex in selectedGameObjIndices)
            {
                p_objMgr.GetDataElementAt(gameObjIndex).Rotation *= rotQuat;
            }
        }

        /// <summary>
        /// Handles the Click event of the applyScaleBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void applyScaleBtn_Click(object sender, EventArgs e)
        {
            float scale;
            bool success = true;

            if (!float.TryParse(scaleXYZTxtBox.Text, out scale))
                success = false;

            if (!success)
            {
                RenderingSystem.Graphics.Forms.WindowsFormHelper.DisplayParseErrorMsg();
                return;
            }

            IEnumerable<int> selectedGameObjsIndicies = GetSelectedGameObjIndices();
            foreach (int gameObjIndex in selectedGameObjsIndicies)
            {
                p_objMgr.GetDataElementAt(gameObjIndex).Scale = scale;
            }
        }

        /// <summary>
        /// Handles the Click event of the centralRotBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void centralRotBtn_Click(object sender, EventArgs e)
        {
            bool success = true;

            Vector3 rotAxis = new Vector3();
            float rotAngle = 0f;

            if (!float.TryParse(centralRotXTxtBox.Text, out rotAxis.X))
                success = false;
            if (!float.TryParse(centralRotYTxtBox.Text, out rotAxis.Y))
                success = false;
            if (!float.TryParse(centralRotZTxtBox.Text, out rotAxis.Z))
                success = false;
            if (!float.TryParse(centralRotAngleTxtBox.Text, out rotAngle))
                success = false;

            rotAngle = MathHelper.ToRadians(rotAngle);

            if (!success)
            {
                RenderingSystem.Graphics.Forms.WindowsFormHelper.DisplayParseErrorMsg();
                return;
            }

            // Find the central point amoungst all the selected objects.
            IEnumerable<int> selectedGameObjsIndices = GetSelectedGameObjIndices();
            IEnumerable<GameObj> selectedGameObjs = from i in selectedGameObjsIndices
                                                    select p_objMgr.GetDataElementAt(i);
            Vector3 centralPos = new Vector3(0f, 0f, 0f);
            foreach (GameObj gameObj in selectedGameObjs)
            {
                centralPos += gameObj.Position;
            }

            centralPos /= (float)selectedGameObjs.Count();

            Matrix transformMat = Matrix.CreateTranslation(centralPos) *
                Matrix.CreateFromAxisAngle(rotAxis, rotAngle);

            Vector3 pos, scale;
            Quaternion rot;

            transformMat.Decompose(out scale, out rot, out pos);

            foreach (GameObj gameObj in selectedGameObjs)
            {
                gameObj.Position += pos;
                gameObj.Rotation *= rot;
            }
        }

        /// <summary>
        /// Handles the Click event of the closeBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the copyGroupBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void copyGroupBtn_Click(object sender, EventArgs e)
        {
            IEnumerable<int> selectedIndices = GetSelectedGameObjIndices();

            DuplicateGroupForm duplicateGroupForm = new DuplicateGroupForm(p_objMgr);
            duplicateGroupForm.SetSelectedIndicies(selectedIndices);
            duplicateGroupForm.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the deleteGroupBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void deleteGroupBtn_Click(object sender, EventArgs e)
        {
            IEnumerable<int> selectedGameObjsIndices = GetSelectedGameObjIndices();
            IEnumerable<GameObj> tmpSelectedGameObjs = from i in selectedGameObjsIndices
                                                       select p_objMgr.GetDataElementAt(i);
            List<GameObj> selectedGameObjs = tmpSelectedGameObjs.ToList();
            foreach (GameObj gameObj in selectedGameObjs)
            {
                p_objMgr.RemoveDataElement(gameObj);
            }

            UpdateListBox();
        }

        /// <summary>
        /// Gets the selected game object indices.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<int> GetSelectedGameObjIndices()
        {
            List<int> gameObjs = new List<int>();

            int numItems = objListBox.Items.Count;
            for (int i = 0; i < numItems; ++i)
            {
                if (objListBox.GetSelected(i))
                {
                    gameObjs.Add(i);
                }
            }

            return gameObjs;
        }

        /// <summary>
        /// Updates the ListBox.
        /// </summary>
        private void UpdateListBox()
        {
            objListBox.Items.Clear();
            foreach (RenderingSystem.GameObj gameObj in p_objMgr.GetDataElements())
            {
                objListBox.Items.Add(gameObj.ActorID);
            }
        }
    }
}