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
    public partial class WaterObjEditorForm : Form
    {
        /// <summary>
        /// The p_water object
        /// </summary>
        private Object.WaterObject p_waterObj;

        /// <summary>
        /// Initializes a new instance of the <see cref="WaterObjEditorForm"/> class.
        /// </summary>
        /// <param name="pWaterObj">The p water object.</param>
        public WaterObjEditorForm(Object.WaterObject pWaterObj)
        {
            InitializeComponent();

            p_waterObj = pWaterObj;

            SetUIData();
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
        /// Sets the UI data.
        /// </summary>
        private void SetUIData()
        {
            waterColorFactorTxtBox.Text = p_waterObj.WaterColorFactor.ToString();
            translationSpeedTxtBox.Text = p_waterObj.TransSpeed.ToString();
            texScaleXTxtBox.Text = p_waterObj.TexScaleX.ToString();
            texScaleYTxtBox.Text = p_waterObj.TexScaleY.ToString();
            waterColorRTxtBox.Text = p_waterObj.WaterColor.X.ToString();
            waterColorGTxtBox.Text = p_waterObj.WaterColor.Y.ToString();
            waterColorBTxtBox.Text = p_waterObj.WaterColor.Z.ToString();
            waterColorATxtBox.Text = p_waterObj.WaterColor.W.ToString();
            translationDirXTxtBox.Text = p_waterObj.TransDir.X.ToString();
            translationDirYTxtBox.Text = p_waterObj.TransDir.Y.ToString();
            translationDirZTxtBox.Text = p_waterObj.TransDir.Z.ToString();
            scaleXTxtBox.Text = p_waterObj.ScaleX.ToString();
            scaleZTxtBox.Text = p_waterObj.ScaleZ.ToString();
            waveLengthTxtBox.Text = p_waterObj.WaveLength.ToString();
            waveHeightTxtBox.Text = p_waterObj.WaveHeight.ToString();
        }

        /// <summary>
        /// Handles the TextChanged event of the txtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.ArgumentException">Unaccounted for text box!</exception>
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string name = textBox.Name;
            string text = textBox.Text;

            float fParsed;
            bool fParseSuccess = float.TryParse(text, out fParsed);

            var color = p_waterObj.WaterColor;
            var dir = p_waterObj.TransDir;

            if (name == "waterColorFactorTxtBox")
            {
                p_waterObj.WaterColorFactor = fParsed;
            }
            else if (name == "translationSpeedTxtBox")
            {
                p_waterObj.TransSpeed = fParsed;
            }
            else if (name == "texScaleXTxtBox")
            {
                p_waterObj.TexScaleX = fParsed;
            }
            else if (name == "texScaleYTxtBox")
            {
                p_waterObj.TexScaleY = fParsed;
            }
            else if (name == "waterColorRTxtBox")
            {
                color.X = fParsed;
                p_waterObj.WaterColor = color;
            }
            else if (name == "waterColorGTxtBox")
            {
                color.Y = fParsed;
                p_waterObj.WaterColor = color;
            }
            else if (name == "waterColorBTxtBox")
            {
                color.Z = fParsed;
                p_waterObj.WaterColor = color;
            }
            else if (name == "waterColorATxtBox")
            {
                color.W = fParsed;
                p_waterObj.WaterColor = color;
            }
            else if (name == "translationDirXTxtBox")
            {
                dir.X = fParsed;
                p_waterObj.TransDir = dir;
            }
            else if (name == "translationDirYTxtBox")
            {
                dir.Y = fParsed;
                p_waterObj.TransDir = dir;
            }
            else if (name == "translationDirZTxtBox")
            {
                dir.Z = fParsed;
                p_waterObj.TransDir = dir;
            }
            else if (name == "scaleXTxtBox")
            {
                p_waterObj.ScaleX = fParsed;
            }
            else if (name == "scaleZTxtBox")
            {
                p_waterObj.ScaleZ = fParsed;
            }
            else if (name == "waveLengthTxtBox")
            {
                p_waterObj.WaveLength = fParsed;
            }
            else if (name == "waveHeightTxtBox")
            {
                p_waterObj.WaveHeight = fParsed;
            }
            else
            {
                throw new ArgumentException("Unaccounted for text box!");
            }
        }
    }
}