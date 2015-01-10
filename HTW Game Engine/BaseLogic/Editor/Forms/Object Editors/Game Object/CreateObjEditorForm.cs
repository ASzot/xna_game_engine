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

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class CreateObjEditorForm : Form
    {
        /// <summary>
        /// The p_game system
        /// </summary>
        private GameSystem p_gameSys;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateObjEditorForm"/> class.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        public CreateObjEditorForm(GameSystem gameSystem)
        {
            p_gameSys = gameSystem;

            InitializeComponent();

            rotXTxtBox.Text = "0";
            rotYTxtBox.Text = "0";
            rotZTxtBox.Text = "0";

            scaleTxtBox.Text = "1";
        }

        /// <summary>
        /// Handles the Load event of the CreateObjEditorForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CreateObjEditorForm_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the okBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void okBtn_Click(object sender, EventArgs e)
        {
            // Get the information and add the object.

            bool success = true;

            float x, y, z, rx, ry, rz;
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

            StaticObj staticObj = p_gameSys.CreateStaticObjAbsoluteFilename(filenameTxtBox.Text, idTxtBox.Text);
            if (staticObj == null)
            {
                MessageBox.Show("Couldn't find filename", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            staticObj.Rotation = Quaternion.CreateFromRotationMatrix(rotMat);
            staticObj.Position = new Microsoft.Xna.Framework.Vector3(x, y, z);
            staticObj.Scale = s;

            this.Close();
        }
    }
}