#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Windows.Forms;

using RenderingSystem.Graphics.Forms;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class ColorSelectorForm : Form
    {
        /// <summary>
        /// The on color accepted
        /// </summary>
        public Action<Microsoft.Xna.Framework.Color> OnColorAccepted;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorSelectorForm"/> class.
        /// </summary>
        public ColorSelectorForm()
        {
            InitializeComponent();

            WindowsFormHelper.SetColorComboBox(colorComboBox);
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
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void okBtn_Click(object sender, EventArgs e)
        {
            string selectedName = colorComboBox.Text;
            Microsoft.Xna.Framework.Color color;
            if (!WindowsFormHelper.GetColorComboBoxInput(selectedName, out color))
            {
                MessageBox.Show("Invalid color!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (OnColorAccepted != null)
                OnColorAccepted(color);

            closeBtn_Click(null, null);
        }
    }
}