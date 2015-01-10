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
    /// Allows the user to edit the light shaft effect applied to the scene through an editor form.
    /// </summary>
    public partial class LightShaftsSettingsForm : Form
    {
        /// <summary>
        /// A pointer to the applied light shaft post processing effect.
        /// </summary>
        private LightShaftPPE p_lightShaft;

        /// <summary>
        /// Construct the light shaft editor form displaying the UI with the data of the light shaft effect suplied.
        /// </summary>
        /// <param name="pLightShaft">A pointer to the light shaft effect applied to the scene.</param>
        public LightShaftsSettingsForm(LightShaftPPE pLightShaft)
        {
            p_lightShaft = pLightShaft;

            InitializeComponent();

            SetUIData();
        }

        /// <summary>
        /// Create the render target combo box.
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="rts"></param>
        private void InitRTComboBox(ComboBox cb, RenderTargetSize rts)
        {
            cb.Items.Clear();
            cb.Items.Add("Full");
            cb.Items.Add("Half");
            cb.Items.Add("Quarter");

            cb.SelectedIndex = (int)rts;
        }

        /// <summary>
        /// Get the option the user selected for the previously created render target combo box.
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="rtSize"></param>
        /// <returns></returns>
        private bool GetRTComboBox(ComboBox cb, out RenderTargetSize rtSize)
        {
            string selectedTxt = rtSizeComboBox.Text;
            return Enum.TryParse<RenderTargetSize>(selectedTxt, out rtSize);
        }

        /// <summary>
        /// Update the UI based on the light shaft post processing pointer.
        /// </summary>
        private void SetUIData()
        {
            blendTxtBox.Text = p_lightShaft.Blend.ToString();
            contrastTxtBox.Text = p_lightShaft.Contrast.ToString();
            decayTxtBox.Text = p_lightShaft.Decay.ToString();
            exposureTxtBox.Text = p_lightShaft.Exposure.ToString();
            saturationTxtBox.Text = p_lightShaft.Saturation.ToString();
            scaleTxtBox.Text = p_lightShaft.Scale.ToString();
            spreadTxtBox.Text = p_lightShaft.Spread.ToString();

            WindowsFormHelper.SetColorComboBox(colorBalanceComboBox);
            WindowsFormHelper.SetColorComboBox(shaftTintComboBox);
            WindowsFormHelper.SetColorComboBoxColor(colorBalanceComboBox, p_lightShaft.ColorBalance);
            WindowsFormHelper.SetColorComboBoxColor(shaftTintComboBox, p_lightShaft.ShaftTint);

            InitRTComboBox(rtSizeComboBox, p_lightShaft.RTSize);
        }

        /// <summary>
        /// On the user clicking the close button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// On the user changing one of the text boxes containing the information of the light shaft post processing effect.
        /// The actual data of the light shaft effect will be updated real time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string name = textBox.Name;

            float fParsed;
            bool fParsedSuccess = float.TryParse(textBox.Text, out fParsed);

            if (name == "blendTxtBox")
            {
                if (fParsedSuccess)
                    p_lightShaft.Blend = fParsed;
            }
            else if (name == "contrastTxtBox")
            {
                if (fParsedSuccess)
                    p_lightShaft.Contrast = fParsed;
            }
            else if (name == "decayTxtBox")
            {
                if (fParsedSuccess)
                    p_lightShaft.Decay = fParsed;
            }
            else if (name == "exposureTxtBox")
            {
                if (fParsedSuccess)
                    p_lightShaft.Exposure = fParsed;
            }
            else if (name == "saturationTxtBox")
            {
                if (fParsedSuccess)
                    p_lightShaft.Saturation = fParsed;
            }
            else if (name == "scaleTxtBox")
            {
                if (fParsedSuccess)
                    p_lightShaft.Scale = fParsed;
            }
            else if (name == "spreadTxtBox")
            {
                if (fParsedSuccess)
                    p_lightShaft.Spread = fParsed;
            }
            else
            {
                throw new ArgumentException("Unaccounted for text box!");
            }
        }

        /// <summary>
        /// On the user changing a combo box data element for the light shaft effect.
        /// Updates the values of the light shaft effect real time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_SelectionChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string name = comboBox.Name;
            string text = comboBox.Text;
            Microsoft.Xna.Framework.Color colorParsed;

            bool colorParseSuccess = WindowsFormHelper.GetColorComboBoxInput(text, out colorParsed);

            if (name == "colorBalanceComboBox")
            {
                if (colorParseSuccess)
                    p_lightShaft.ColorBalance = colorParsed;
            }
            else if (name == "shaftTintComboBox")
            {
                if (colorParseSuccess)
                    p_lightShaft.ShaftTint = colorParsed;
            }
            else if (name == "rtSizeComboBox")
            {
                RenderTargetSize rtSize;
                if (GetRTComboBox(rtSizeComboBox, out rtSize))
                    p_lightShaft.RTSize = rtSize;
            }
            else
            {
                throw new ArgumentException("Unaccounted for combo box.");
            }
        }
    }
}
