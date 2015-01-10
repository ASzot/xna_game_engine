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

using RenderingSystem.Graphics.Forms;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class CreateDirLightForm : Form
    {
        /// <summary>
        /// The p_light MGR
        /// </summary>
        private Manager.LightManager p_lightMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDirLightForm"/> class.
        /// </summary>
        /// <param name="pLightMgr">The p light MGR.</param>
        public CreateDirLightForm(Manager.LightManager pLightMgr)
        {
            InitializeComponent();

            p_lightMgr = pLightMgr;

            shadowBiasTxtBox.Text = "0.005";
            shadowDistanceTxtBox.Text = "100";
            flareQuerySizeTxtBox.Text = "400";
            flareGlowSizeTxtBox.Text = "100";

            flarePanel.Visible = false;
            shadowPanel.Visible = false;

            WindowsFormHelper.SetColorComboBox(colorComboBox);
        }

        /// <summary>
        /// Handles the Click event of the cancelBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the castShadowsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void castShadowsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            shadowPanel.Visible = castShadowsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the Click event of the createBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void createBtn_Click(object sender, EventArgs e)
        {
            float rx, ry, rz, sd, sb, fqs, fgs, di, si;
            Microsoft.Xna.Framework.Color lightColor;

            string selectedColorText = colorComboBox.Text;

            bool castShadows = castShadowsCheckBox.Checked;
            bool flareEnabled = flareEnabledCheckBox.Checked;

            bool success = true;
            if (!float.TryParse(rotXTxtBox.Text, out rx))
                success = false;
            if (!float.TryParse(rotYTxtBox.Text, out ry))
                success = false;
            if (!float.TryParse(rotZTxtBox.Text, out rz))
                success = false;
            if (!float.TryParse(intensityTxtBox.Text, out di))
                success = false;
            if (!float.TryParse(specIntensityTxtBox.Text, out si))
                success = false;

            if (castShadows)
            {
                if (!float.TryParse(shadowDistanceTxtBox.Text, out sd))
                    success = false;
                if (!float.TryParse(shadowBiasTxtBox.Text, out sb))
                    success = false;
            }
            else
            {
                sd = 100f;
                sb = 0.001f;
            }

            if (flareEnabled)
            {
                if (!float.TryParse(flareQuerySizeTxtBox.Text, out fqs))
                    success = false;
                if (!float.TryParse(flareGlowSizeTxtBox.Text, out fgs))
                    success = false;
            }
            else
            {
                fqs = 400f;
                fgs = 100f;
            }

            if (!WindowsFormHelper.GetColorComboBoxInput(selectedColorText, out lightColor))
                success = false;

            string lightID = idTxtBox.Text;
            if (p_lightMgr.LightIDExists(lightID))
            {
                MessageBox.Show("Invalid light ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!success)
            {
                MessageBox.Show("Incorrect data value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            rx = MathHelper.ToRadians(rx);
            ry = MathHelper.ToRadians(ry);
            rz = MathHelper.ToRadians(rz);

            RenderingSystem.DirLight dirLight = new RenderingSystem.DirLight(new Vector3(rx, ry, rz), lightColor, di, si, castShadows, lightID);
            dirLight.UseLensFlare = flareEnabled;
            dirLight.FlareGlowSize = fgs;
            dirLight.FlareQuerySize = fqs;
            dirLight.ShadowBias = sb;
            dirLight.ShadowDistance = sd;

            p_lightMgr.AddToList(dirLight);

            cancelBtn_Click(null, null);
        }

        /// <summary>
        /// Handles the CheckedChanged event of the flareEnabledCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void flareEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            flarePanel.Visible = flareEnabledCheckBox.Checked;
        }
    }
}