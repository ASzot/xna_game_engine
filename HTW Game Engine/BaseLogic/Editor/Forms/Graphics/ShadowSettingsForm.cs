#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Windows.Forms;

using RenderingSystem;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class ShadowSettingsForm : Form
    {
        /// <summary>
        /// The on dialog close
        /// </summary>
        public Action OnDlgClose;

        /// <summary>
        /// The _render access
        /// </summary>
        private RendererAccess _renderAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShadowSettingsForm"/> class.
        /// </summary>
        /// <param name="renderAccess">The render access.</param>
        public ShadowSettingsForm(RendererAccess renderAccess)
        {
            InitializeComponent();

            _renderAccess = renderAccess;

            int csmRes, csmDiv, maxCsm, maxSpot, spotRes;
            _renderAccess.GetShadowInfo(out csmRes, out csmDiv, out maxSpot, out maxCsm, out spotRes);

            cascadeResTxtBox.Text = csmRes.ToString();
            cascadeDivTxtBox.Text = csmDiv.ToString();
            maxCascadeMapsTxtBox.Text = maxCsm.ToString();
            maxSpotShadowMapsTxtBox.Text = maxSpot.ToString();
            spotShadowMapResTxtBox.Text = spotRes.ToString();
        }

        /// <summary>
        /// Handles the Click event of the acceptBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void acceptBtn_Click(object sender, EventArgs e)
        {
            applyBtn_Click(null, null);
            cancelBtn_Click(null, null);
        }

        /// <summary>
        /// Handles the Click event of the applyBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void applyBtn_Click(object sender, EventArgs e)
        {
            int csmRes, csmDiv, maxCsm, maxSpot, spotRes;

            bool success = true;
            if (!int.TryParse(cascadeResTxtBox.Text, out csmRes))
                success = false;
            if (!int.TryParse(cascadeDivTxtBox.Text, out csmDiv))
                success = false;
            if (!int.TryParse(maxCascadeMapsTxtBox.Text, out maxCsm))
                success = false;
            if (!int.TryParse(maxSpotShadowMapsTxtBox.Text, out maxSpot))
                success = false;
            if (!int.TryParse(spotShadowMapResTxtBox.Text, out spotRes))
                success = false;

            if (!success)
            {
                MessageBox.Show("Invalid data input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _renderAccess.SetShadowInfo(csmRes, csmDiv, maxSpot, maxCsm, spotRes);
        }

        /// <summary>
        /// Handles the Click event of the cancelBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            OnDlgClose();
            this.Close();
        }
    }
}