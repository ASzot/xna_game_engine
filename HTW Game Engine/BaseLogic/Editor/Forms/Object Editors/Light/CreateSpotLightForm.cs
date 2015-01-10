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
    public partial class CreateSpotLightForm : Form
    {
        /// <summary>
        /// The p_light MGR
        /// </summary>
        private Manager.LightManager p_lightMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSpotLightForm"/> class.
        /// </summary>
        /// <param name="pLightMgr">The p light MGR.</param>
        public CreateSpotLightForm(Manager.LightManager pLightMgr)
        {
            InitializeComponent();

            p_lightMgr = pLightMgr;

            depthBiasTxtBox.Text = "0.0005";
            spotAngleTxtBox.Text = "45";
            spotExpTxtBox.Text = "10";
            querySizeTxtBox.Text = "100";
            glowSizeTxtBox.Text = "400";

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
        /// Handles the Click event of the createBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void createBtn_Click(object sender, EventArgs e)
        {
            // Get all of the data from the UI.
            float px, py, pz, rx, ry, rz, intensity, specInten, spotAngle, spotExp, depthBias, querySize, glowSize, range;
            Microsoft.Xna.Framework.Color diffuseColor;
            bool useLensFlare, castShadows;

            bool success = true;
            if (!float.TryParse(posXTxtBox.Text, out px))
                success = false;
            if (!float.TryParse(posYTxtBox.Text, out py))
                success = false;
            if (!float.TryParse(posZTxtBox.Text, out pz))
                success = false;
            if (!float.TryParse(rotXTxtBox.Text, out rx))
                success = false;
            if (!float.TryParse(rotYTxtBox.Text, out ry))
                success = false;
            if (!float.TryParse(rotZTxtBox.Text, out rz))
                success = false;
            if (!float.TryParse(intensityTxtBox.Text, out intensity))
                success = false;
            if (!float.TryParse(spotAngleTxtBox.Text, out spotAngle))
                success = false;
            if (!float.TryParse(spotExpTxtBox.Text, out spotExp))
                success = false;
            if (!float.TryParse(depthBiasTxtBox.Text, out depthBias))
                success = false;
            if (!float.TryParse(querySizeTxtBox.Text, out querySize))
                success = false;
            if (!float.TryParse(glowSizeTxtBox.Text, out glowSize))
                success = false;
            if (!float.TryParse(rangeTxtBox.Text, out range))
                success = false;
            if (!float.TryParse(specIntenTxtBox.Text, out specInten))
                success = false;

            if (!success)
            {
                MessageBox.Show("Incorrect data value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            useLensFlare = useLensFlareCheckBox.Checked;
            castShadows = castShadowsCheckBox.Checked;

            string selectedText = colorComboBox.Text;

            if (!WindowsFormHelper.GetColorComboBoxInput(selectedText, out diffuseColor))
            {
                MessageBox.Show("Incorrect data value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string lightID = idTxtBox.Text;
            if (p_lightMgr.LightIDExists(lightID))
            {
                MessageBox.Show("Invalid light ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            rx = MathHelper.ToRadians(rx);
            ry = MathHelper.ToRadians(ry);
            rz = MathHelper.ToRadians(rz);

            RenderingSystem.SpotLight spotLight = new RenderingSystem.SpotLight(new Vector3(px, py, pz), new Vector3(rx, ry, rz), diffuseColor, range, intensity, specInten, castShadows, lightID);
            spotLight.UseLensFlare = useLensFlare;
            spotLight.FlareGlowSize = glowSize;
            spotLight.FlareQuerySize = querySize;
            spotLight.SpotConeAngle = spotAngle;
            spotLight.SpotExponent = spotExp;
            spotLight.DepthBias = depthBias;
            spotLight.FlareGlowSize = glowSize;
            spotLight.FlareQuerySize = querySize;

            p_lightMgr.AddToList(spotLight);

            this.Close();
        }
    }
}