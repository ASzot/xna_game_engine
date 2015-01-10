#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RenderingSystem.Graphics.Forms
{
    /// <summary>
    /// For the editor allows the user to modify the anti aliasing settings of the scene.
    /// </summary>
    partial class AASettingsForm : Form
    {
        /// <summary>
        /// A pointer to the anti-alaising effect.
        /// </summary>
        private FxaaPPE p_AA;

        /// <summary>
        /// Create the components of the form with the supplied anti-alaising effect's data.
        /// </summary>
        /// <param name="pAA">A pointer to the used anti-alaising effect.</param>
        public AASettingsForm(FxaaPPE pAA)
        {
            // Create the form.
            InitializeComponent();
            // Store the pointer.
            p_AA = pAA;

            // Update the UI with the data of the anti-alaising effect.
            countTxtBox.Text = p_AA.Count.ToString();
            edgeThresholdMinTxtBox.Text = p_AA.EdgeThresholdMin.ToString();
            edgeThresholdTxtBox.Text = p_AA.EdgeThreshold.ToString();
            subPixelTxtBox.Text = p_AA.SubPixelAliasingRemoval.ToString();

            enabledCheckBox.Checked = p_AA.Enabled;
        }

        /// <summary>
        /// Updates the data of the anti-alaising effect real time whenever the user makes a change in the editor.
        /// </summary>
        /// <param name="sender">The txt box the user changed.</param>
        /// <param name="e"></param>
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            // Get the text box changed.
            TextBox textBox = (TextBox)sender;
            string name = textBox.Name;
            string text = textBox.Text;

            // Parse the data.
            float fParsed;
            bool fParseSuccess = float.TryParse(text, out fParsed);

            // Update the appropriate data of the anti-alaising effect.
            if (name == "countTxtBox" && fParseSuccess)
            {
                p_AA.Count = fParsed;
            }
            else if (name == "edgeThresholdMinTxtBox" && fParseSuccess)
            {
                p_AA.EdgeThresholdMin = fParsed;
            }
            else if (name == "edgeThresholdTxtBox" && fParseSuccess)
            {
                p_AA.EdgeThreshold = fParsed;
            }
            else if (name == "subPixelTxtBox" && fParseSuccess)
            {
                p_AA.SubPixelAliasingRemoval = fParsed;
            }
            else
            {
                // The name of the text box was incorrect.
                throw new ArgumentException("Unaccounted text box!");
            }
        }

        /// <summary>
        /// Updates the data of the anti-alaising effect real time whenever the user changes the check box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Get the check box.
            CheckBox checkBox = (CheckBox)sender;
            // Update whether anti-alaising is enabled through the value of the check box.
            p_AA.Enabled = checkBox.Checked;
        }

        /// <summary>
        /// When the user clicks the close button on the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            // Close the form.
            this.Close();
        }
    }
}
