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
    /// The editor form used to modify the nature of the bloom effect being applied.
    /// </summary>
	public partial class BloomSettingsForm : Form
	{
        /// <summary>
        /// Fired when the user clicks on accept or applied.
        /// </summary>
        public Action<BloomSettings> OnSettingsAccept;

        /// <summary>
        /// Create the form with the data of the bloom settings supplied.
        /// </summary>
        /// <param name="pBloomSettings">A pointer to the current bloom settings.</param>
		public BloomSettingsForm(BloomSettings pBloomSettings)
		{
            // Initialize the UI.
			InitializeComponent();

            // Populate UI with data of the bloom settings supplied.
            bloomThresholdTxtBox.Text = pBloomSettings.BloomThreshold.ToString();
            blurAmountTxtBox.Text = pBloomSettings.BlurAmount.ToString();
            bloomIntensityTxtBox.Text = pBloomSettings.BloomIntensity.ToString();
            baseIntensityTxtBox.Text = pBloomSettings.BaseIntensity.ToString();
            bloomSaturationTxtBox.Text = pBloomSettings.BloomSaturation.ToString();
            baseSaturationTxtBox.Text = pBloomSettings.BloomSaturation.ToString();
		}

        /// <summary>
        /// On the user clicking the close button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void cancelBtn_Click(object sender, EventArgs e)
		{
			this.Close();
		}

        /// <summary>
        /// On the user clickign the accept. Update bloom and close form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void acceptBtn_Click(object sender, EventArgs e)
		{
            // Apply the edited bloom settings to the actual bloom.
			applyBtn_Click(null, null);

            // Close the form.
			cancelBtn_Click(null, null);
		}

        /// <summary>
        /// On the user clicking the apply button.
        /// Apply the bloom from the editor to the applied screen bloom.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void applyBtn_Click(object sender, EventArgs e)
		{
			float bloomThreshold, blurAmount, bloomIntensity, baseIntensity, bloomSaturation, baseSaturation;

			bool success = true;
			if (!float.TryParse(bloomThresholdTxtBox.Text, out bloomThreshold))
				success = false;
			if (!float.TryParse(blurAmountTxtBox.Text, out blurAmount))
				success = false;
			if (!float.TryParse(bloomIntensityTxtBox.Text, out bloomIntensity))
				success = false;
			if (!float.TryParse(baseIntensityTxtBox.Text, out baseIntensity))
				success = false;
			if (!float.TryParse(bloomSaturationTxtBox.Text, out bloomSaturation))
				success = false;
			if (!float.TryParse(baseSaturationTxtBox.Text, out baseSaturation))
				success = false;

			if (!success)
			{
				MessageBox.Show("Invalid data input!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			BloomSettings tmpSettings = new BloomSettings("Custom Bloom", bloomThreshold, blurAmount, bloomIntensity, baseIntensity, bloomSaturation, baseSaturation);

            OnSettingsAccept(tmpSettings);
		}
	}
}
