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
    public partial class EditFlashingLightForm : Form
    {
        /// <summary>
        /// The on accept
        /// </summary>
        public Action OnAccept;

        /// <summary>
        /// The p_flashing light proc
        /// </summary>
        private Process.FlashingLightProcess p_flashingLightProc;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditFlashingLightForm"/> class.
        /// </summary>
        /// <param name="createForm">if set to <c>true</c> [create form].</param>
        /// <param name="pFlashingLightProc">The p flashing light proc.</param>
        public EditFlashingLightForm(bool createForm, Process.FlashingLightProcess pFlashingLightProc)
        {
            InitializeComponent();

            p_flashingLightProc = pFlashingLightProc;

            if (!createForm)
            {
                acceptBtn.Visible = false;
                flashOutDurTxtBox.Text = p_flashingLightProc.FlashOutDur.ToString();
                flashOutFreqTxtBox.Text = p_flashingLightProc.FlashOutFreq.ToString();
            }
        }

        /// <summary>
        /// Handles the Click event of the acceptBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void acceptBtn_Click(object sender, EventArgs e)
        {
            OnAccept();

            closeBtn_Click(null, null);
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
        /// Handles the TextChanged event of the flashOutDurTxtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void flashOutDurTxtBox_TextChanged(object sender, EventArgs e)
        {
            string text = ((TextBox)sender).Text;
            uint uiParsed;
            bool success = uint.TryParse(text, out uiParsed);

            if (!success)
                return;

            p_flashingLightProc.FlashOutDur = uiParsed;
        }

        /// <summary>
        /// Handles the TextChanged event of the flashOutFreqTxtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void flashOutFreqTxtBox_TextChanged(object sender, EventArgs e)
        {
            string text = ((TextBox)sender).Text;
            uint uiParsed;
            bool success = uint.TryParse(text, out uiParsed);

            if (!success)
                return;

            p_flashingLightProc.FlashOutFreq = uiParsed;
        }
    }
}