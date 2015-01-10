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
using Microsoft.Xna.Framework.Graphics;
using RenderingSystem;
using GameColor = Microsoft.Xna.Framework.Color;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class MaterialEditorForm : Form
    {
        /// <summary>
        /// The apply subset material
        /// </summary>
        public Action<SubsetMaterial> ApplySubsetMaterial;

        /// <summary>
        /// The _clipboard material
        /// </summary>
        private static SubsetMaterial _clipboardMaterial = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialEditorForm"/> class.
        /// </summary>
        /// <param name="subsetMaterial">The subset material.</param>
        public MaterialEditorForm(SubsetMaterial subsetMaterial)
        {
            InitializeComponent();

            SetUISubsetMaterial(subsetMaterial);

            pasteBtn.Enabled = (_clipboardMaterial != null);
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
            SubsetMaterial sm = ParseSubsetMaterial();
            if (sm == null)
                return;

            ApplySubsetMaterial(sm);
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
        /// Handles the Click event of the copyBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void copyBtn_Click(object sender, EventArgs e)
        {
            SubsetMaterial sm = ParseSubsetMaterial();
            if (sm == null)
                return;

            _clipboardMaterial = sm;

            pasteBtn.Enabled = true;
        }

        /// <summary>
        /// Parses the subset material.
        /// </summary>
        /// <returns></returns>
        private SubsetMaterial ParseSubsetMaterial()
        {
            SubsetMaterial subsetMat = new SubsetMaterial();

            Texture2D diffuseMap, specularMap, normalMap;

            string errorMsg = "";

            if (diffuseMapTxtBox.Text == "")
                diffuseMap = null;
            else
            {
                diffuseMap = ResourceMgr.LoadTextureAbsoluteFilename(diffuseMapTxtBox.Text);
                if (diffuseMap == null)
                    errorMsg += "Couldn't create diffuse map!" + Environment.NewLine;
            }

            if (specularMapTxtBox.Text == "")
                specularMap = null;
            else
            {
                specularMap = ResourceMgr.LoadTextureAbsoluteFilename(specularMapTxtBox.Text);
                if (specularMap == null)
                    errorMsg += "Couldn't create specular map!" + Environment.NewLine;
            }

            if (normalMapTxtBox.Text == "")
                normalMap = null;
            else
            {
                normalMap = ResourceMgr.LoadTextureAbsoluteFilename(normalMapTxtBox.Text);
                if (normalMap == null)
                    errorMsg += "Couldn't create normal map!" + Environment.NewLine;
            }

            if (errorMsg != "")
            {
                MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            bool success = true;

            float tx, ty, tz, rx, ry, rz, sx, sy, sz, ar;
            int dmx, dmy, dmz, dmw, smx, smy, smz;
            if (!float.TryParse(translXTxtBox.Text, out tx))
                success = false;
            if (!float.TryParse(translYTxtBox.Text, out ty))
                success = false;
            if (!float.TryParse(translZTxtBox.Text, out tz))
                success = false;
            if (!float.TryParse(rotXTxtBox.Text, out rx))
                success = false;
            if (!float.TryParse(rotYTxtBox.Text, out ry))
                success = false;
            if (!float.TryParse(rotZTxtBox.Text, out rz))
                success = false;
            if (!float.TryParse(scaleXTxtBox.Text, out sx))
                success = false;
            if (!float.TryParse(scaleYTxtBox.Text, out sy))
                success = false;
            if (!float.TryParse(scaleZTxtBox.Text, out sz))
                success = false;
            if (!int.TryParse(diffuseRTxtBox.Text, out dmx))
                success = false;
            if (!int.TryParse(diffuseGTxtBox.Text, out dmy))
                success = false;
            if (!int.TryParse(diffuseBTxtBox.Text, out dmz))
                success = false;
            if (!int.TryParse(diffuseATxtBox.Text, out dmw))
                success = false;
            if (!int.TryParse(specularRTxtBox.Text, out smx))
                success = false;
            if (!int.TryParse(specularGTxtBox.Text, out smy))
                success = false;
            if (!int.TryParse(specularBTxtBox.Text, out smz))
                success = false;
            if (!float.TryParse(alphaRefTxtBox.Text, out ar))
                success = false;

            if (!success)
            {
                MessageBox.Show("Couldn't create texture transform!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            rx = MathHelper.ToRadians(rx);
            ry = MathHelper.ToRadians(ry);
            rz = MathHelper.ToRadians(rz);

            subsetMat.DiffuseMap = new GameTexture(diffuseMap, diffuseMapTxtBox.Text);
            subsetMat.SpecularMap = new GameTexture(specularMap, specularMapTxtBox.Text); ;
            subsetMat.NormalMap = new GameTexture(normalMap, normalMapTxtBox.Text); ;
            subsetMat.TexTransform = Matrix.CreateScale(sx, sy, sz) * Matrix.CreateRotationX(rx) *
                Matrix.CreateRotationY(ry) * Matrix.CreateRotationZ(rz) * Matrix.CreateTranslation(tx, ty, tz);

            GameColor diffuseMat = new GameColor(dmx, dmy, dmz, dmw);
            GameColor specularMat = new GameColor(smx, smy, smz);

            subsetMat.DiffuseMat = diffuseMat.ToVector4();
            subsetMat.SpecularMat = specularMat.ToVector3();

            subsetMat.AlphaReference = ar;

            subsetMat.UseDiffuseMat = useDiffuseMatCheckBox.Checked;
            subsetMat.UseSpecularMat = useSpecularMatCheckBox.Checked;
            subsetMat.UseAlphaMask = alphaClippingCheckBox.Checked;

            return subsetMat;
        }

        /// <summary>
        /// Handles the Click event of the pasteBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pasteBtn_Click(object sender, EventArgs e)
        {
            if (_clipboardMaterial != null)
                SetUISubsetMaterial(_clipboardMaterial);
        }

        /// <summary>
        /// Handles the Click event of the selectColorBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void selectColorBtn_Click(object sender, EventArgs e)
        {
            ColorSelectorForm csf = new ColorSelectorForm();
            csf.OnColorAccepted += (Microsoft.Xna.Framework.Color color) =>
                {
                    diffuseRTxtBox.Text = color.R.ToString();
                    diffuseGTxtBox.Text = color.G.ToString();
                    diffuseBTxtBox.Text = color.B.ToString();
                    diffuseATxtBox.Text = color.A.ToString();
                };
            csf.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the selectColorBtn2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void selectColorBtn2_Click(object sender, EventArgs e)
        {
            ColorSelectorForm csf = new ColorSelectorForm();
            csf.OnColorAccepted += (Microsoft.Xna.Framework.Color color) =>
            {
                specularRTxtBox.Text = color.R.ToString();
                specularGTxtBox.Text = color.G.ToString();
                specularBTxtBox.Text = color.B.ToString();
            };
            csf.ShowDialog();
        }

        /// <summary>
        /// Sets the UI subset material.
        /// </summary>
        /// <param name="subsetMaterial">The subset material.</param>
        /// <exception cref="System.ArgumentException"></exception>
        private void SetUISubsetMaterial(SubsetMaterial subsetMaterial)
        {
            Vector3 texTrans, scale;
            Quaternion rot;
            if (!subsetMaterial.TexTransform.Decompose(out scale, out rot, out texTrans))
                throw new ArgumentException();

            translXTxtBox.Text = texTrans.X.ToString();
            translYTxtBox.Text = texTrans.Y.ToString();
            translZTxtBox.Text = texTrans.Z.ToString();

            rotXTxtBox.Text = "0";
            rotYTxtBox.Text = "0";
            rotZTxtBox.Text = "0";

            scaleXTxtBox.Text = scale.X.ToString();
            scaleYTxtBox.Text = scale.Y.ToString();
            scaleZTxtBox.Text = scale.Z.ToString();

            diffuseMapTxtBox.Text = subsetMaterial.DiffuseMap.Filename;
            specularMapTxtBox.Text = subsetMaterial.SpecularMap.Filename;
            normalMapTxtBox.Text = subsetMaterial.NormalMap.Filename;

            GameColor diffuse = new GameColor(subsetMaterial.DiffuseMat);
            diffuseRTxtBox.Text = diffuse.R.ToString();
            diffuseGTxtBox.Text = diffuse.G.ToString();
            diffuseBTxtBox.Text = diffuse.B.ToString();
            diffuseATxtBox.Text = diffuse.A.ToString();

            GameColor specular = new GameColor(subsetMaterial.SpecularMat);
            specularRTxtBox.Text = specular.R.ToString();
            specularGTxtBox.Text = specular.G.ToString();
            specularBTxtBox.Text = specular.B.ToString();

            alphaRefTxtBox.Text = subsetMaterial.AlphaReference.ToString();

            useDiffuseMatCheckBox.Checked = subsetMaterial.UseDiffuseMat;
            useSpecularMatCheckBox.Checked = subsetMaterial.UseSpecularMat;
            alphaClippingCheckBox.Checked = subsetMaterial.UseAlphaMask;
        }
    }
}