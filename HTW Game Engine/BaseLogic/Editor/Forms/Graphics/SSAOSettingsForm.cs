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
    public partial class SSAOSettingsForm : Form
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
        /// Initializes a new instance of the <see cref="SSAOSettingsForm"/> class.
        /// </summary>
        /// <param name="pRenderAccess">The p render access.</param>
        public SSAOSettingsForm(RendererAccess pRenderAccess)
        {
            InitializeComponent();
            _renderAccess = pRenderAccess;

            int blurCount;
            float radius, randomTile, intensity, maxRadius, bias;

            _renderAccess.GetSSAORenderInfo(out randomTile, out radius, out maxRadius, out bias, out intensity, out blurCount);

            randomTileTxtBox.Text = randomTile.ToString("R");
            radiusTxtBox.Text = radius.ToString("R");
            maxRadiusTxtBox.Text = maxRadius.ToString("R");
            biasTxtBox.Text = bias.ToString("R");
            intensityTxtBox.Text = intensity.ToString("R");
            blurCountTxtBox.Text = blurCount.ToString();

            enabledCheckBox.Checked = _renderAccess.UseSSAO;
        }

        /// <summary>
        /// Handles the Click event of the acceptBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void acceptBtn_Click(object sender, EventArgs e)
        {
            applyBtn_Click(null, null);

            closeBtn_Click(null, null);
        }

        /// <summary>
        /// Handles the Click event of the applyBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void applyBtn_Click(object sender, EventArgs e)
        {
            int blurCount;
            float radius, randomTile, intensity, maxRadius, bias;

            if (!enabledCheckBox.Checked)
            {
                _renderAccess.UseSSAO = false;
                return;
            }

            bool success = true;
            if (!float.TryParse(radiusTxtBox.Text, out radius))
                success = false;
            if (!float.TryParse(randomTileTxtBox.Text, out randomTile))
                success = false;
            if (!float.TryParse(intensityTxtBox.Text, out intensity))
                success = false;
            if (!float.TryParse(maxRadiusTxtBox.Text, out maxRadius))
                success = false;
            if (!float.TryParse(biasTxtBox.Text, out bias))
                success = false;
            if (!int.TryParse(blurCountTxtBox.Text, out blurCount))
                success = false;

            if (!success)
            {
                MessageBox.Show("Invalid data input!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _renderAccess.UseSSAO = true;
            _renderAccess.SetSSAORenderInfo(randomTile, radius, maxRadius, bias, intensity, blurCount);
        }

        /// <summary>
        /// Handles the Click event of the closeBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            OnDlgClose();
            this.Close();
        }
    }
}