#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using RenderingSystem;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class CreateAnimatedObjForm : Form
    {
        /// <summary>
        /// The p_game system
        /// </summary>
        private GameSystem p_gameSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAnimatedObjForm"/> class.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        public CreateAnimatedObjForm(GameSystem gameSystem)
        {
            InitializeComponent();

            p_gameSystem = gameSystem;
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
        /// Handles the Click event of the okBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void okBtn_Click(object sender, EventArgs e)
        {
            bool success = true;
            bool phased = phasedCheckBox.Checked;

            float x, y, z, rx, ry, rz;
            Vector3 bbMin = new Vector3(), bbMax = new Vector3();
            if (!float.TryParse(xPosTxtBox.Text, out x))
                success = false;
            if (!float.TryParse(yPosTxtBox.Text, out y))
                success = false;
            if (!float.TryParse(zPosTxtBox.Text, out z))
                success = false;
            if (!float.TryParse(rotXTxtBox.Text, out rx))
                success = false;
            if (!float.TryParse(rotYTxtBox.Text, out ry))
                success = false;
            if (!float.TryParse(rotZTxtBox.Text, out rz))
                success = false;

            if (phased)
            {
                if (!float.TryParse(bbMinXTxtBox.Text, out bbMin.X))
                    success = false;
                if (!float.TryParse(bbMinYTxtBox.Text, out bbMin.Y))
                    success = false;
                if (!float.TryParse(bbMinZTxtBox.Text, out bbMin.Z))
                    success = false;

                if (!float.TryParse(bbMaxXTxtBox.Text, out bbMax.X))
                    success = false;
                if (!float.TryParse(bbMaxYTxtBox.Text, out bbMax.Y))
                    success = false;
                if (!float.TryParse(bbMaxZTxtBox.Text, out bbMax.Z))
                    success = false;
            }

            float s;
            if (!float.TryParse(scaleTxtBox.Text, out s))
                success = false;

            if (!success)
            {
                MessageBox.Show("Invalid data! Object not created!", "Error");
                return;
            }

            rx = MathHelper.ToRadians(rx);
            ry = MathHelper.ToRadians(ry);
            rz = MathHelper.ToRadians(rz);

            Matrix rotMat = Matrix.CreateRotationX(rx) * Matrix.CreateRotationY(ry) * Matrix.CreateRotationZ(rz);

            GameObj animObj;
            if (phased)
            {
                animObj = p_gameSystem.CreatePhasedAnimObj(filenameTxtBox.Text, idTxtBox.Text, clipTxtBox.Text);
            }
            else
            {
                animObj = p_gameSystem.CreateAnimatedObjAbsoluteFilename(filenameTxtBox.Text, idTxtBox.Text, clipTxtBox.Text, bbMin, bbMax);
            }

            if (animObj == null)
            {
                MessageBox.Show("Couldn't find filename", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            animObj.Scale = s;
            animObj.Position = new Vector3(x, y, z);
            animObj.Rotation = Quaternion.CreateFromRotationMatrix(rotMat);

            closeBtn_Click(null, null);
        }

        /// <summary>
        /// Handles the CheckedChanged event of the phasedCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void phasedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            bbMaxGroupBox.Visible = bbMinGroupBox.Visible = checkBox.Checked;
        }
    }
}