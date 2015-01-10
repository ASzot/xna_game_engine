#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Windows.Forms;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class TranslatingDoorObjEditorForm : Form
    {
        /// <summary>
        /// The p_door
        /// </summary>
        private Object.TranslatingDoorObj p_door;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslatingDoorObjEditorForm"/> class.
        /// </summary>
        /// <param name="pDoor">The p door.</param>
        public TranslatingDoorObjEditorForm(Object.TranslatingDoorObj pDoor)
        {
            InitializeComponent();

            p_door = pDoor;

            closeHeightTxtBox.Text = p_door.ClosedHeight.ToString();
            openHeightTxtBox.Text = p_door.OpenHeight.ToString();
            translateSpeedTxtBox.Text = p_door.Speed.ToString();
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
        /// <exception cref="System.ArgumentException">Unaccounted for text box!</exception>
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            string txtStr = txtBox.Text;
            string name = txtBox.Name;

            float fParsed;
            bool fParseSuccess = float.TryParse(txtStr, out fParsed);

            if (!fParseSuccess)
                return;

            if (name == "closeHeightTxtBox")
            {
                p_door.ClosedHeight = fParsed;
            }
            else if (name == "openHeightTxtBox")
            {
                p_door.OpenHeight = fParsed;
            }
            else if (name == "translateSpeedTxtBox")
            {
                p_door.Speed = fParsed;
            }
            else
            {
                throw new ArgumentException("Unaccounted for text box!");
            }
        }
    }
}