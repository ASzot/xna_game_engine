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
    public partial class SwingingDoorObjEditorForm : Form
    {
        /// <summary>
        /// The p_swinging door object
        /// </summary>
        private Object.SwingingDoorObj p_swingingDoorObj;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwingingDoorObjEditorForm"/> class.
        /// </summary>
        /// <param name="pSwingingDoorObj">The p swinging door object.</param>
        public SwingingDoorObjEditorForm(Object.SwingingDoorObj pSwingingDoorObj)
        {
            InitializeComponent();
            p_swingingDoorObj = pSwingingDoorObj;

            float closedAngleDeg = MathHelper.ToDegrees(p_swingingDoorObj.ClosedAngle);
            float openAngleDeg = MathHelper.ToDegrees(p_swingingDoorObj.OpenAngle);

            closedAngleTxtBox.Text = closedAngleDeg.ToString();
            openAngleTxtBox.Text = openAngleDeg.ToString();
            xAxisRotTxtBox.Text = p_swingingDoorObj.RotAxis.X.ToString();
            yAxisRotTxtBox.Text = p_swingingDoorObj.RotAxis.Y.ToString();
            zAxisRotTxtBox.Text = p_swingingDoorObj.RotAxis.Z.ToString();
            swingSpeedTxtBox.Text = p_swingingDoorObj.RotSpeed.ToString();
            xOffsetTxtBox.Text = p_swingingDoorObj.RotAxisOffset.X.ToString();
            yOffsetTxtBox.Text = p_swingingDoorObj.RotAxisOffset.Y.ToString();
            zOffsetTxtBox.Text = p_swingingDoorObj.RotAxisOffset.Z.ToString();
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
        /// Handles the TextChanged event of the txtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.ArgumentException"></exception>
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            string name = txtBox.Name;
            string txtStr = txtBox.Text;

            float fParsed;
            bool fParseSuccess = float.TryParse(txtStr, out fParsed);

            if (!fParseSuccess)
                return;

            var rotAxis = p_swingingDoorObj.RotAxis;
            var offset = p_swingingDoorObj.RotAxisOffset;

            if (name == "closedAngleTxtBox")
            {
                p_swingingDoorObj.ClosedAngle = MathHelper.ToRadians(fParsed);
            }
            else if (name == "openAngleTxtBox")
            {
                p_swingingDoorObj.OpenAngle = MathHelper.ToRadians(fParsed);
            }
            else if (name == "xAxisRotTxtBox")
            {
                p_swingingDoorObj.RotAxis = new Vector3(fParsed, rotAxis.Y, rotAxis.Z);
            }
            else if (name == "yAxisRotTxtBox")
            {
                p_swingingDoorObj.RotAxis = new Vector3(rotAxis.X, fParsed, rotAxis.Z);
            }
            else if (name == "zAxisRotTxtBox")
            {
                p_swingingDoorObj.RotAxis = new Vector3(rotAxis.X, rotAxis.Y, fParsed);
            }
            else if (name == "swingSpeedTxtBox")
            {
                p_swingingDoorObj.RotSpeed = fParsed;
            }
            else if (name == "xOffsetTxtBox")
            {
                p_swingingDoorObj.RotAxisOffset = new Vector3(fParsed, offset.Y, offset.Z);
            }
            else if (name == "yOffsetTxtBox")
            {
                p_swingingDoorObj.RotAxisOffset = new Vector3(offset.X, fParsed, offset.Z);
            }
            else if (name == "zOffsetTxtBox")
            {
                p_swingingDoorObj.RotAxisOffset = new Vector3(offset.X, offset.Y, fParsed);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}