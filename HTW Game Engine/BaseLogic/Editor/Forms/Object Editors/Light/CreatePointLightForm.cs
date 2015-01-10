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
    public partial class CreatePointLightForm : Form
    {
        /// <summary>
        /// The b_add to list
        /// </summary>
        private bool b_addToList = true;

        /// <summary>
        /// The b_edit mode
        /// </summary>
        private bool b_editMode = false;

        /// <summary>
        /// The p_light MGR
        /// </summary>
        private Manager.LightManager p_lightMgr;

        /// <summary>
        /// The p_point light
        /// </summary>
        private RenderingSystem.PointLight p_pointLight;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePointLightForm"/> class.
        /// </summary>
        /// <param name="pLightMgr">The p light MGR.</param>
        /// <param name="pointLight">The point light.</param>
        /// <param name="addToList">if set to <c>true</c> [add to list].</param>
        public CreatePointLightForm(Manager.LightManager pLightMgr, RenderingSystem.PointLight pointLight = null, bool addToList = true)
        {
            b_addToList = addToList;
            InitializeComponent();

            WindowsFormHelper.SetColorComboBox(colorComboBox);

            p_lightMgr = pLightMgr;

            if (pointLight != null)
            {
                createBtn.Visible = false;
                this.Text = "Edit Point Light";
                p_pointLight = pointLight;
                b_editMode = true;

                intensityTxtBox.Text = p_pointLight.DiffuseIntensity.ToString();
                specIntenTxtBox.Text = p_pointLight.SpecularIntensity.ToString();
                rangeTxtBox.Text = p_pointLight.Range.ToString();
                idTxtBox.Text = p_pointLight.LightID;
                posXTxtBox.Text = p_pointLight.Position.X.ToString();
                posYTxtBox.Text = p_pointLight.Position.Y.ToString();
                posZTxtBox.Text = p_pointLight.Position.Z.ToString();

                WindowsFormHelper.SetColorComboBoxColor(colorComboBox, p_pointLight.DiffuseColor);
            }
        }

        /// <summary>
        /// Disables the light identifier.
        /// </summary>
        public void DisableLightID()
        {
            idTxtBox.Enabled = false;
        }

        /// <summary>
        /// Sets the position.
        /// </summary>
        /// <param name="pos">The position.</param>
        public void SetPosition(Microsoft.Xna.Framework.Vector3 pos)
        {
            posXTxtBox.Text = pos.X.ToString();
            posYTxtBox.Text = pos.Y.ToString();
            posZTxtBox.Text = pos.Z.ToString();
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
        /// Handles the SelectedIndexChanged event of the colorComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void colorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!b_editMode)
                return;

            Microsoft.Xna.Framework.Color color;
            if (WindowsFormHelper.GetColorComboBoxInput(colorComboBox.Text, out color))
                p_pointLight.DiffuseColor = color;
        }

        /// <summary>
        /// Handles the Click event of the createBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void createBtn_Click(object sender, EventArgs e)
        {
            float px, py, pz, intensity, specInten, range;

            bool success = true;
            if (!float.TryParse(posXTxtBox.Text, out px))
                success = false;
            if (!float.TryParse(posYTxtBox.Text, out py))
                success = false;
            if (!float.TryParse(posZTxtBox.Text, out pz))
                success = false;
            if (!float.TryParse(intensityTxtBox.Text, out intensity))
                success = false;
            if (!float.TryParse(rangeTxtBox.Text, out range))
                success = false;
            if (!float.TryParse(specIntenTxtBox.Text, out specInten))
                success = false;

            if (!success)
            {
                MessageBox.Show("Invalid data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedName = colorComboBox.Text;
            Microsoft.Xna.Framework.Color diffuseColor;
            if (!WindowsFormHelper.GetColorComboBoxInput(selectedName, out diffuseColor))
            {
                MessageBox.Show("Incorrect data value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string lightID;
            if (idTxtBox.Enabled == true)
            {
                lightID = idTxtBox.Text;
                if (p_lightMgr.LightIDExists(lightID))
                {
                    MessageBox.Show("Invalid light ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
                lightID = Guid.NewGuid().ToString();

            RenderingSystem.PointLight pointLight = new RenderingSystem.PointLight(new Microsoft.Xna.Framework.Vector3(px, py, pz), range, intensity, specInten,
                diffuseColor, lightID);
            if (b_addToList)
                p_lightMgr.AddToList(pointLight);

            this.Close();
        }

        /// <summary>
        /// Handles the TextChanged event of the txtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.ArgumentException">Unrecognized text box!</exception>
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            if (!b_editMode)
                return;

            TextBox txtBox = (TextBox)sender;
            string txt = txtBox.Text;
            string name = txtBox.Name;

            var pos = p_pointLight.Position;

            if (name == "idTxtBox" && txt != "" && !p_lightMgr.LightIDExists(txt))
            {
                p_pointLight.LightID = txt;
            }

            float fParsed;
            bool fParseSuccess = float.TryParse(txt, out fParsed);

            if (!fParseSuccess)
                return;

            if (name == "intensityTxtBox")
            {
                p_pointLight.DiffuseIntensity = fParsed;
            }
            else if (name == "specIntenTxtBox")
            {
                p_pointLight.SpecularIntensity = fParsed;
            }
            else if (name == "rangeTxtBox")
            {
                p_pointLight.Range = fParsed;
            }
            else if (name == "posXTxtBox")
            {
                p_pointLight.Position = new Microsoft.Xna.Framework.Vector3(fParsed, pos.Y, pos.Z);
            }
            else if (name == "posYTxtBox")
            {
                p_pointLight.Position = new Microsoft.Xna.Framework.Vector3(pos.X, fParsed, pos.Z);
            }
            else if (name == "posZTxtBox")
            {
                p_pointLight.Position = new Microsoft.Xna.Framework.Vector3(pos.X, pos.Y, fParsed);
            }
            else
            {
                throw new ArgumentException("Unrecognized text box!");
            }
        }
    }
}