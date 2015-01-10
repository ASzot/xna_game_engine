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

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class SceneSettingsForm : Form, RenderingSystem.UpdatingForm
    {
        /// <summary>
        /// The b_is form closed
        /// </summary>
        private bool b_isFormClosed = false;

        /// <summary>
        /// The p_renderer access
        /// </summary>
        private RenderingSystem.RendererAccess p_rendererAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneSettingsForm"/> class.
        /// </summary>
        public SceneSettingsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets a value indicating whether this instance is form closed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is form closed; otherwise, <c>false</c>.
        /// </value>
        public bool IsFormClosed
        {
            get { return b_isFormClosed; }
        }

        /// <summary>
        /// Sets the render access.
        /// </summary>
        /// <param name="pRenderAcess">The p render acess.</param>
        public void SetRenderAccess(RenderingSystem.RendererAccess pRenderAcess)
        {
            p_rendererAccess = pRenderAcess;

            var col = p_rendererAccess.AmbientColor;
            ambientRTxtBox.Text = col.X.ToString();
            ambientGTxtBox.Text = col.Y.ToString();
            ambientBTxtBox.Text = col.Z.ToString();
            ambientATxtBox.Text = col.W.ToString();

            useSkymapCheckBox.Checked = p_rendererAccess.RenderSkymap;
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        public void UpdateUI()
        {
            var col = p_rendererAccess.AmbientColor;
            if (!ambientRTxtBox.Focused)
            {
                ambientRTxtBox.Text = col.X.ToString();
            }
            if (!ambientGTxtBox.Focused)
            {
                ambientGTxtBox.Text = col.Y.ToString();
            }
            if (!ambientBTxtBox.Focused)
            {
                ambientBTxtBox.Text = col.Z.ToString();
            }
            if (!ambientATxtBox.Focused)
            {
                ambientATxtBox.Text = col.W.ToString();
            }
            if (!useSkymapCheckBox.Focused)
            {
                useSkymapCheckBox.Checked = p_rendererAccess.RenderSkymap;
            }
            if (!renderCullingBBCheckBox.Focused)
            {
                renderCullingBBCheckBox.Checked = p_rendererAccess.RenderCullingBoundingBoxes;
            }
            if (!renderLinesCheckBox.Focused)
            {
                renderLinesCheckBox.Checked = p_rendererAccess.RenderLines;
            }
            if (!renderQuadsCheckBox.Focused)
            {
                renderQuadsCheckBox.Checked = p_rendererAccess.RenderQuads;
            }
            if (!renderTextCheckBox.Focused)
            {
                renderTextCheckBox.Checked = p_rendererAccess.RenderText;
            }
        }

        /// <summary>
        /// Checks the box check changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.ArgumentException">Check box unaccounted for!</exception>
        private void checkBoxCheckChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string name = checkBox.Name;
            bool check = checkBox.Checked;

            if (name == "useSkymapCheckBox")
            {
                p_rendererAccess.RenderSkymap = check;
            }
            else if (name == "renderCullingBBCheckBox")
            {
                p_rendererAccess.RenderCullingBoundingBoxes = check;
            }
            else if (name == "renderQuadsCheckBox")
            {
                p_rendererAccess.RenderQuads = check;
            }
            else if (name == "renderTextCheckBox")
            {
                p_rendererAccess.RenderText = check;
            }
            else if (name == "renderLinesCheckBox")
            {
                p_rendererAccess.RenderLines = check;
            }
            else
            {
                throw new ArgumentException("Check box unaccounted for!");
            }
        }

        /// <summary>
        /// Handles the Click event of the closeBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            b_isFormClosed = true;

            this.Close();
        }

        /// <summary>
        /// Texts the box text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtBoxTextChanged(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            string name = txtBox.Name;

            float fParsed;
            bool fParsedSuccess = float.TryParse(txtBox.Text, out fParsed);

            var prevCol = p_rendererAccess.AmbientColor;

            if (name == "ambientRTxtBox" && fParsedSuccess)
            {
                p_rendererAccess.AmbientColor = new Vector4(fParsed, prevCol.Y, prevCol.Z, prevCol.W);
            }
            else if (name == "ambientGTxtBox" && fParsedSuccess)
            {
                p_rendererAccess.AmbientColor = new Vector4(prevCol.X, fParsed, prevCol.Z, prevCol.W);
            }
            else if (name == "ambientBTxtBox" && fParsedSuccess)
            {
                p_rendererAccess.AmbientColor = new Vector4(prevCol.X, prevCol.Y, fParsed, prevCol.W);
            }
            else if (name == "ambientATxtBox" && fParsedSuccess)
            {
                p_rendererAccess.AmbientColor = new Vector4(prevCol.X, prevCol.Y, prevCol.Z, fParsed);
            }
        }
    }
}