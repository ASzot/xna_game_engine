#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using RenderingSystem;
using RenderingSystem.Graphics.Forms;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class GameLightEditorForm : Form
    {
        /// <summary>
        /// The form update funcs
        /// </summary>
        public List<Action> FormUpdateFuncs;

        /// <summary>
        /// The b_previous color had focus
        /// </summary>
        private bool b_previousColorHadFocus = false;

        /// <summary>
        /// The i_previous selected index
        /// </summary>
        private int i_previousSelectedIndex = -1;

        /// <summary>
        /// The p_light MGR
        /// </summary>
        private Manager.LightManager p_lightMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLightEditorForm"/> class.
        /// </summary>
        public GameLightEditorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates the dir light panel.
        /// </summary>
        public void CreateDirLightPanel()
        {
            TextBox xRotTxtBox = CreateTextBox(40, 40, 20, 100, "xRotTxtBox", "");
            Label xRotLbl = CreateLabel(0, 40, "RX:");

            TextBox yRotTxtBox = CreateTextBox(40, 70, 20, 100, "yRotTxtBox", "");
            Label yRotLbl = CreateLabel(0, 70, "RY:");

            TextBox zRotTxtBox = CreateTextBox(40, 100, 20, 100, "zRotTxtBox", "");
            Label zRotLbl = CreateLabel(0, 100, "RZ:");

            TextBox intenTxtBox = CreateTextBox(40, 130, 20, 100, "intensityTxtBox", "");
            Label intenLbl = CreateLabel(0, 130, "Inten:");

            TextBox specIntenTxtBox = CreateTextBox(40, 160, 20, 100, "specIntenTxtBox", "");
            Label specIntenLbl = CreateLabel(0, 160, "SInten:");

            TextBox depthBias = CreateTextBox(40, 190, 20, 100, "depthBias", "");
            Label depthLbl = CreateLabel(0, 190, "Bias:");
            CheckBox castShadows = CreateCheckBox(40, 210, 20, 100, "castShadows", "Shadows");

            TextBox shadowDistTxtBox = CreateTextBox(40, 240, 20, 100, "shadowDistanceTxtBox", "");
            Label shadowDistLbl = CreateLabel(0, 240, "SwDst:");

            TextBox id = CreateTextBox(40, 270, 20, 100, "idTxtBox", "");
            Label idLbl = CreateLabel(0, 270, "ID:");

            CheckBox useFlareCheckBox = CreateCheckBox(40, 300, 20, 100, "useFlareCheckBox", "Use Flare");

            TextBox flareGlowSize = CreateTextBox(40, 330, 20, 100, "flareGlowSizeTxtBox", "");
            Label flareGlowSizeLbl = CreateLabel(0, 330, "Glow:");

            TextBox flareQuerySize = CreateTextBox(40, 360, 20, 100, "flareQuerySizeTxtBox", "");
            Label flareQuerySizeLbl = CreateLabel(0, 360, "Query:");

            ComboBox colorCB = CreateComboBox(40, 390, 20, 100, "colorComboBox");
            WindowsFormHelper.SetColorComboBox(colorCB);
            Label colorLbl = CreateLabel(0, 390, "Color:");

            lightDataPanel.Controls.Clear();
            lightDataPanel.Controls.Add(xRotTxtBox);
            lightDataPanel.Controls.Add(xRotLbl);
            lightDataPanel.Controls.Add(yRotTxtBox);
            lightDataPanel.Controls.Add(yRotLbl);
            lightDataPanel.Controls.Add(zRotTxtBox);
            lightDataPanel.Controls.Add(zRotLbl);
            lightDataPanel.Controls.Add(intenTxtBox);
            lightDataPanel.Controls.Add(intenLbl);
            lightDataPanel.Controls.Add(specIntenTxtBox);
            lightDataPanel.Controls.Add(specIntenLbl);
            lightDataPanel.Controls.Add(depthBias);
            lightDataPanel.Controls.Add(depthLbl);
            lightDataPanel.Controls.Add(castShadows);
            lightDataPanel.Controls.Add(shadowDistTxtBox);
            lightDataPanel.Controls.Add(shadowDistLbl);
            lightDataPanel.Controls.Add(id);
            lightDataPanel.Controls.Add(idLbl);
            lightDataPanel.Controls.Add(flareQuerySize);
            lightDataPanel.Controls.Add(flareQuerySizeLbl);
            lightDataPanel.Controls.Add(flareGlowSize);
            lightDataPanel.Controls.Add(flareGlowSizeLbl);
            lightDataPanel.Controls.Add(useFlareCheckBox);
            lightDataPanel.Controls.Add(colorCB);
            lightDataPanel.Controls.Add(colorLbl);
        }

        /// <summary>
        /// Sets the light MGR.
        /// </summary>
        /// <param name="pLightMgr">The p light MGR.</param>
        public void SetLightMgr(Manager.LightManager pLightMgr)
        {
            p_lightMgr = pLightMgr;

            CreateLightListBox();

            lightDataPanel.Visible = false;
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        /// <exception cref="System.ArgumentException"></exception>
        public void UpdateUI()
        {
            if (p_lightMgr.GetNumberOfDataElements() != lightListBox.Items.Count)
                CreateLightListBox();

            int selectedIndex = lightListBox.SelectedIndex;

            if (selectedIndex == -1)
            {
                lightDataPanel.Visible = false;
                deleteLightBtn.Visible = false;
                lightDataPanel.Controls.Clear();
                return;
            }
            else
            {
                lightDataPanel.Visible = true;
                deleteLightBtn.Visible = true;
            }

            GameLight light = p_lightMgr.GetDataElementAt(selectedIndex);

            if (i_previousSelectedIndex != selectedIndex)
            {
                if (light is PointLight)
                {
                    CreatePointLightPanel();
                }
                else if (light is SpotLight)
                {
                    CreateSpotLightPanel();
                }
                else if (light is DirLight)
                {
                    CreateDirLightPanel();
                }
                else
                {
                    Label lbl = new Label();
                    lbl.Text = "Not supported.";
                    lightDataPanel.Controls.Clear();
                    lightDataPanel.Controls.Add(lbl);
                }
            }

            i_previousSelectedIndex = selectedIndex;

            if (!GetControl("idTxtBox").Focused)
            {
                GetControl("idTxtBox").Text = light.LightID;
            }

            if (light is PointLight)
            {
                PointLight pl = light as PointLight;

                if (!GetControl("xPosTxtBox").Focused)
                    GetControl("xPosTxtBox").Text = pl.Position.X.ToString();
                if (!GetControl("yPosTxtBox").Focused)
                    GetControl("yPosTxtBox").Text = pl.Position.Y.ToString();
                if (!GetControl("zPosTxtBox").Focused)
                    GetControl("zPosTxtBox").Text = pl.Position.Z.ToString();
                if (!GetControl("rangeTxtBox").Focused)
                    GetControl("rangeTxtBox").Text = pl.Range.ToString();
                if (!GetControl("intensityTxtBox").Focused)
                    GetControl("intensityTxtBox").Text = pl.DiffuseIntensity.ToString();
                if (!GetControl("specIntenTxtBox").Focused)
                    GetControl("specIntenTxtBox").Text = pl.SpecularIntensity.ToString();
                if (!GetControl("colorComboBox").Focused && b_previousColorHadFocus)
                    WindowsFormHelper.SetColorComboBoxColor(GetControl("colorComboBox") as ComboBox, pl.DiffuseColor);
            }
            else if (light is SpotLight)
            {
                SpotLight sl = light as SpotLight;

                if (!GetControl("xPosTxtBox").Focused)
                    GetControl("xPosTxtBox").Text = sl.Position.X.ToString();
                if (!GetControl("yPosTxtBox").Focused)
                    GetControl("yPosTxtBox").Text = sl.Position.Y.ToString();
                if (!GetControl("zPosTxtBox").Focused)
                    GetControl("zPosTxtBox").Text = sl.Position.Z.ToString();
                if (!GetControl("xRotTxtBox").Focused)
                    GetControl("xRotTxtBox").Text = MathHelper.ToDegrees(sl.Rotation.X).ToString();
                if (!GetControl("yRotTxtBox").Focused)
                    GetControl("yRotTxtBox").Text = MathHelper.ToDegrees(sl.Rotation.Y).ToString();
                if (!GetControl("zRotTxtBox").Focused)
                    GetControl("zRotTxtBox").Text = MathHelper.ToDegrees(sl.Rotation.Z).ToString();
                if (!GetControl("rangeTxtBox").Focused)
                    GetControl("rangeTxtBox").Text = sl.Range.ToString();
                if (!GetControl("intensityTxtBox").Focused)
                    GetControl("intensityTxtBox").Text = sl.DiffuseIntensity.ToString();
                if (!GetControl("depthBias").Focused)
                    GetControl("depthBias").Text = sl.DepthBias.ToString();
                if (!GetControl("spotExponent").Focused)
                    GetControl("spotExponent").Text = sl.SpotExponent.ToString();
                if (!GetControl("spotAngle").Focused)
                    GetControl("spotAngle").Text = sl.SpotConeAngle.ToString();
                if (!GetControl("castShadows").Focused)
                    (GetControl("castShadows") as CheckBox).Checked = sl.CastShadows;
                if (!GetControl("specIntenTxtBox").Focused)
                    GetControl("specIntenTxtBox").Text = sl.SpecularIntensity.ToString();
                if (!GetControl("flareGlowSizeTxtBox").Focused)
                    GetControl("flareGlowSizeTxtBox").Text = sl.FlareGlowSize.ToString();
                if (!GetControl("flareQuerySizeTxtBox").Focused)
                    GetControl("flareQuerySizeTxtBox").Text = sl.FlareQuerySize.ToString();
                if (!GetControl("useFlareCheckBox").Focused)
                    (GetControl("useFlareCheckBox") as CheckBox).Checked = sl.UseLensFlare;
                if (!GetControl("colorComboBox").Focused && b_previousColorHadFocus)
                    WindowsFormHelper.SetColorComboBoxColor(GetControl("colorComboBox") as ComboBox, sl.DiffuseColor);
            }
            else if (light is DirLight)
            {
                DirLight dl = light as DirLight;

                if (!GetControl("xRotTxtBox").Focused)
                    GetControl("xRotTxtBox").Text = MathHelper.ToDegrees(dl.Rotation.X).ToString();
                if (!GetControl("yRotTxtBox").Focused)
                    GetControl("yRotTxtBox").Text = MathHelper.ToDegrees(dl.Rotation.Y).ToString();
                if (!GetControl("zRotTxtBox").Focused)
                    GetControl("zRotTxtBox").Text = MathHelper.ToDegrees(dl.Rotation.Z).ToString();
                if (!GetControl("intensityTxtBox").Focused)
                    GetControl("intensityTxtBox").Text = dl.DiffuseIntensity.ToString();
                if (!GetControl("specIntenTxtBox").Focused)
                    GetControl("specIntenTxtBox").Text = dl.SpecularIntensity.ToString();
                if (!GetControl("shadowDistanceTxtBox").Focused)
                    GetControl("shadowDistanceTxtBox").Text = dl.ShadowDistance.ToString();
                if (!GetControl("castShadows").Focused)
                    (GetControl("castShadows") as CheckBox).Checked = dl.CastShadows;
                if (!GetControl("depthBias").Focused)
                    GetControl("depthBias").Text = dl.ShadowBias.ToString();
                if (!GetControl("flareGlowSizeTxtBox").Focused)
                    GetControl("flareGlowSizeTxtBox").Text = dl.FlareGlowSize.ToString();
                if (!GetControl("flareQuerySizeTxtBox").Focused)
                    GetControl("flareQuerySizeTxtBox").Text = dl.FlareQuerySize.ToString();
                if (!GetControl("useFlareCheckBox").Focused)
                    (GetControl("useFlareCheckBox") as CheckBox).Checked = dl.UseLensFlare;
                if (!GetControl("colorComboBox").Focused && b_previousColorHadFocus)
                    WindowsFormHelper.SetColorComboBoxColor(GetControl("colorComboBox") as ComboBox, dl.DiffuseColor);
            }
            else
            {
                throw new ArgumentException();
            }

            if (GetControl("colorComboBox") != null)
                b_previousColorHadFocus = GetControl("colorComboBox").Focused;
        }

        /// <summary>
        /// Handles the Click event of the addLightBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void addLightBtn_Click(object sender, EventArgs e)
        {
            CreateLightStartForm clsf = new CreateLightStartForm(p_lightMgr);
            clsf.ShowDialog();

            CreateLightListBox();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the checkBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.ArgumentException"></exception>
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string name = checkBox.Name;

            int selectedIndex = lightListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            GameLight selectedLight = p_lightMgr.GetDataElementAt(selectedIndex);

            bool check = checkBox.Checked;

            if (name == "castShadows")
            {
                if (selectedLight is SpotLight)
                    (selectedLight as SpotLight).CastShadows = check;
                else if (selectedLight is DirLight)
                    (selectedLight as DirLight).CastShadows = check;
            }
            else if (name == "useFlareCheckBox")
            {
                if (selectedLight is DirLight)
                    (selectedLight as DirLight).UseLensFlare = check;
                else if (selectedLight is SpotLight)
                    (selectedLight as SpotLight).UseLensFlare = check;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Handles the Click event of the closeBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            FormUpdateFuncs.Remove(UpdateUI);

            this.Close();
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the comboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string name = comboBox.Name;

            int selectedIndex = lightListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            GameLight selectedLight = p_lightMgr.GetDataElementAt(selectedIndex);

            Microsoft.Xna.Framework.Color color;
            bool colorParse = WindowsFormHelper.GetColorComboBoxInput(comboBox.Text, out color);

            if (name == "colorComboBox")
            {
                if (colorParse)
                {
                    if (selectedLight is DirLight)
                        (selectedLight as DirLight).DiffuseColor = color;
                    else if (selectedLight is SpotLight)
                        (selectedLight as SpotLight).DiffuseColor = color;
                    else if (selectedLight is PointLight)
                        (selectedLight as PointLight).DiffuseColor = color;
                }
            }
        }

        /// <summary>
        /// Creates the CheckBox.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <param name="name">The name.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private CheckBox CreateCheckBox(int x, int y, int height, int width, string name, string text)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Name = name;
            checkBox.Text = text;
            checkBox.Top = y;
            checkBox.Left = x;
            checkBox.Height = height;
            checkBox.CheckedChanged += new EventHandler(checkBox_CheckedChanged);

            return checkBox;
        }

        /// <summary>
        /// Creates the ComboBox.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private ComboBox CreateComboBox(int x, int y, int height, int width, string name)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Name = name;
            comboBox.Width = width;
            comboBox.Height = height;
            comboBox.Top = y;
            comboBox.Left = x;
            comboBox.SelectedIndexChanged += new EventHandler(comboBox_SelectionChangeCommitted);

            return comboBox;
        }

        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private Label CreateLabel(int x, int y, string text)
        {
            Label lbl = new Label();
            lbl.Top = y;
            lbl.Left = x;
            lbl.Text = text;
            return lbl;
        }

        /// <summary>
        /// Creates the light ListBox.
        /// </summary>
        private void CreateLightListBox()
        {
            int selectedIndex = lightListBox.SelectedIndex;
            int previousCount = lightListBox.Items.Count;

            lightListBox.Items.Clear();

            string[] lightNames = new string[p_lightMgr.GetNumberOfDataElements()];

            for (int i = 0; i < p_lightMgr.GetNumberOfDataElements(); ++i)
            {
                lightNames[i] = p_lightMgr.GetDataElementAt(i).LightID;
            }
            lightListBox.Items.AddRange(lightNames);

            if (lightListBox.Items.Count >= previousCount)
            {
                // Preserve the selected index.
                lightListBox.SelectedIndex = selectedIndex;
            }
        }

        /// <summary>
        /// Creates the point light panel.
        /// </summary>
        private void CreatePointLightPanel()
        {
            Label lbl = new Label();
            lbl.Top = 10;
            lbl.Text = "Point Light";

            TextBox xPosTxtBox = CreateTextBox(40, 40, 20, 100, "xPosTxtBox", "");
            Label xPosLbl = CreateLabel(0, 40, "X:");
            TextBox yPosTxtBox = CreateTextBox(40, 70, 20, 100, "yPosTxtBox", "");
            Label yPosLbl = CreateLabel(0, 70, "Y:");
            TextBox zPosTxtBox = CreateTextBox(40, 100, 20, 100, "zPosTxtBox", "");
            Label zPosLbl = CreateLabel(0, 100, "Z:");

            TextBox range = CreateTextBox(40, 130, 20, 100, "rangeTxtBox", "");
            Label rangeLbl = CreateLabel(0, 130, "Range:");
            TextBox intensity = CreateTextBox(40, 160, 20, 100, "intensityTxtBox", "");
            Label intenLbl = CreateLabel(0, 160, "Inten:");

            TextBox specIntenTxtBox = CreateTextBox(40, 190, 20, 100, "specIntenTxtBox", "");
            Label specIntenLbl = CreateLabel(0, 190, "SInten:");

            TextBox id = CreateTextBox(40, 220, 20, 100, "idTxtBox", "");
            Label idLbl = CreateLabel(0, 220, "ID:");

            ComboBox colorCB = CreateComboBox(40, 250, 20, 100, "colorComboBox");
            WindowsFormHelper.SetColorComboBox(colorCB);
            Label colorLbl = CreateLabel(0, 250, "Color:");

            lightDataPanel.Controls.Clear();
            lightDataPanel.Controls.Add(lbl);
            lightDataPanel.Controls.Add(xPosTxtBox);
            lightDataPanel.Controls.Add(yPosTxtBox);
            lightDataPanel.Controls.Add(zPosTxtBox);
            lightDataPanel.Controls.Add(range);
            lightDataPanel.Controls.Add(intensity);
            lightDataPanel.Controls.Add(specIntenTxtBox);
            lightDataPanel.Controls.Add(id);
            lightDataPanel.Controls.Add(xPosLbl);
            lightDataPanel.Controls.Add(yPosLbl);
            lightDataPanel.Controls.Add(zPosLbl);
            lightDataPanel.Controls.Add(rangeLbl);
            lightDataPanel.Controls.Add(intenLbl);
            lightDataPanel.Controls.Add(idLbl);
            lightDataPanel.Controls.Add(specIntenLbl);
            lightDataPanel.Controls.Add(colorCB);
            lightDataPanel.Controls.Add(colorLbl);
        }

        /// <summary>
        /// Creates the spot light panel.
        /// </summary>
        private void CreateSpotLightPanel()
        {
            Label lbl = new Label();
            lbl.Top = 10;
            lbl.Text = "Spot Light";

            const int txtBoxX = 40;

            TextBox xPosTxtBox = CreateTextBox(txtBoxX, 40, 20, 100, "xPosTxtBox", "");
            Label xPosLbl = CreateLabel(0, 40, "X:");
            TextBox yPosTxtBox = CreateTextBox(txtBoxX, 70, 20, 100, "yPosTxtBox", "");
            Label yPosLbl = CreateLabel(0, 70, "Y:");
            TextBox zPosTxtBox = CreateTextBox(txtBoxX, 100, 20, 100, "zPosTxtBox", "");
            Label zPosLbl = CreateLabel(0, 100, "Z:");

            TextBox range = CreateTextBox(txtBoxX, 130, 20, 100, "rangeTxtBox", "");
            Label rangeLbl = CreateLabel(0, 130, "Range:");
            TextBox intensity = CreateTextBox(txtBoxX, 160, 20, 100, "intensityTxtBox", "");
            Label intenLbl = CreateLabel(0, 160, "Inten:");

            TextBox specIntenTxtBox = CreateTextBox(40, 190, 20, 100, "specIntenTxtBox", "");
            Label specIntenLbl = CreateLabel(0, 190, "SInten:");

            TextBox spotAngle = CreateTextBox(txtBoxX, 220, 20, 100, "spotAngle", "");
            Label spotAngleLbl = CreateLabel(0, 220, "Angle:");
            TextBox depthBias = CreateTextBox(txtBoxX, 250, 20, 100, "depthBias", "");
            Label depthLbl = CreateLabel(0, 250, "Bias:");
            CheckBox castShadows = CreateCheckBox(txtBoxX, 280, 20, 100, "castShadows", "Shadows");
            TextBox exp = CreateTextBox(txtBoxX, 310, 20, 100, "spotExponent", "");
            Label expLbl = CreateLabel(0, 310, "Exp:");

            TextBox xRotTxtBox = CreateTextBox(txtBoxX, 340, 20, 100, "xRotTxtBox", "");
            Label xRotLbl = CreateLabel(0, 340, "RX:");
            TextBox yRotTxtBox = CreateTextBox(txtBoxX, 370, 20, 100, "yRotTxtBox", "");
            Label yRotLbl = CreateLabel(0, 370, "RY:");
            TextBox zRotTxtBox = CreateTextBox(txtBoxX, 400, 20, 100, "zRotTxtBox", "");
            Label zRotLbl = CreateLabel(0, 400, "RZ:");

            TextBox id = CreateTextBox(txtBoxX, 430, 20, 100, "idTxtBox", "");
            Label idLbl = CreateLabel(0, 430, "ID:");

            CheckBox useFlareCheckBox = CreateCheckBox(txtBoxX, 460, 20, 100, "useFlareCheckBox", "Use Flare");

            TextBox flareGlowSize = CreateTextBox(txtBoxX, 490, 20, 100, "flareGlowSizeTxtBox", "");
            Label flareGlowSizeLbl = CreateLabel(0, 490, "Glow:");

            TextBox flareQuerySize = CreateTextBox(txtBoxX, 520, 20, 100, "flareQuerySizeTxtBox", "");
            Label flareQuerySizeLbl = CreateLabel(0, 520, "Query:");

            ComboBox colorCB = CreateComboBox(40, 550, 20, 100, "colorComboBox");
            WindowsFormHelper.SetColorComboBox(colorCB);
            Label colorLbl = CreateLabel(0, 550, "Color:");

            lightDataPanel.Controls.Clear();
            lightDataPanel.Controls.Add(lbl);
            lightDataPanel.Controls.Add(xPosTxtBox);
            lightDataPanel.Controls.Add(yPosTxtBox);
            lightDataPanel.Controls.Add(zPosTxtBox);
            lightDataPanel.Controls.Add(range);
            lightDataPanel.Controls.Add(intensity);

            lightDataPanel.Controls.Add(xPosLbl);
            lightDataPanel.Controls.Add(yPosLbl);
            lightDataPanel.Controls.Add(zPosLbl);
            lightDataPanel.Controls.Add(rangeLbl);
            lightDataPanel.Controls.Add(intenLbl);

            lightDataPanel.Controls.Add(spotAngle);
            lightDataPanel.Controls.Add(spotAngleLbl);
            lightDataPanel.Controls.Add(depthBias);
            lightDataPanel.Controls.Add(depthLbl);
            lightDataPanel.Controls.Add(castShadows);
            lightDataPanel.Controls.Add(exp);
            lightDataPanel.Controls.Add(expLbl);
            lightDataPanel.Controls.Add(specIntenTxtBox);
            lightDataPanel.Controls.Add(specIntenLbl);

            lightDataPanel.Controls.Add(xRotTxtBox);
            lightDataPanel.Controls.Add(xRotLbl);
            lightDataPanel.Controls.Add(yRotTxtBox);
            lightDataPanel.Controls.Add(yRotLbl);
            lightDataPanel.Controls.Add(zRotTxtBox);
            lightDataPanel.Controls.Add(zRotLbl);
            lightDataPanel.Controls.Add(id);
            lightDataPanel.Controls.Add(idLbl);

            lightDataPanel.Controls.Add(flareQuerySize);
            lightDataPanel.Controls.Add(flareQuerySizeLbl);
            lightDataPanel.Controls.Add(flareGlowSize);
            lightDataPanel.Controls.Add(flareGlowSizeLbl);
            lightDataPanel.Controls.Add(useFlareCheckBox);
            lightDataPanel.Controls.Add(colorCB);
            lightDataPanel.Controls.Add(colorLbl);
        }

        /// <summary>
        /// Creates the text box.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <param name="name">The name.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private TextBox CreateTextBox(int x, int y, int height, int width, string name, string text)
        {
            TextBox txtBox = new TextBox();
            txtBox.Top = y;
            txtBox.Left = x;
            txtBox.Width = width;
            txtBox.Height = height;
            txtBox.Name = name;
            txtBox.Text = text;
            txtBox.TextChanged += new EventHandler(txtBox_TextChanged);

            return txtBox;
        }

        /// <summary>
        /// Handles the Click event of the deleteLightBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void deleteLightBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = lightListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            p_lightMgr.RemoveDataElement(selectedIndex);

            if (p_lightMgr.GetNumberOfDataElements() > 0)
                lightListBox.SelectedIndex = selectedIndex - 1;
            else
                lightListBox.SelectedIndex = -1;

            i_previousSelectedIndex = -1;

            CreateLightListBox();
        }

        /// <summary>
        /// Handles the Click event of the editModifiersBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void editModifiersBtn_Click(object sender, EventArgs e)
        {
            LightModifierEditor lme = new LightModifierEditor(p_lightMgr);
            lme.ShowDialog();
        }

        /// <summary>
        /// Gets the control.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private Control GetControl(string name)
        {
            foreach (Control control in lightDataPanel.Controls)
            {
                if (control.Name == name)
                    return control;
            }

            return null;
        }

        /// <summary>
        /// Handles the KeyPress event of the lightListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void lightListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the user hit the backspace.
            if (e.KeyChar == '\b')
            {
                deleteLightBtn_Click(null, null);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the lightListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void lightListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();

            // Make sure the color is properly updated.
            b_previousColorHadFocus = true;
        }

        /// <summary>
        /// Handles the TextChanged event of the txtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            int selectedIndex = lightListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            string str = ((TextBox)sender).Text;

            string name = ((TextBox)sender).Name;

            GameLight selectedLight = p_lightMgr.GetDataElementAt(selectedIndex);

            // Lucky for us the data types are all floats.
            float fData;
            bool parseStatus = true;
            if (!float.TryParse(str, out fData))
                parseStatus = false;

            if (name == "xPosTxtBox")
            {
                if (!parseStatus)
                    return;

                if (selectedLight is SpotLight)
                    (selectedLight as SpotLight).Position.X = fData;
                else if (selectedLight is PointLight)
                    (selectedLight as PointLight).Position.X = fData;
                else
                    throw new ArgumentException();
            }
            else if (name == "yPosTxtBox")
            {
                if (!parseStatus)
                    return;

                if (selectedLight is SpotLight)
                    (selectedLight as SpotLight).Position.Y = fData;
                else if (selectedLight is PointLight)
                    (selectedLight as PointLight).Position.Y = fData;
                else
                    throw new ArgumentException();
            }
            else if (name == "zPosTxtBox")
            {
                if (!parseStatus)
                    return;

                if (selectedLight is SpotLight)
                    (selectedLight as SpotLight).Position.Z = fData;
                else if (selectedLight is PointLight)
                    (selectedLight as PointLight).Position.Z = fData;
                else
                    throw new ArgumentException();
            }
            else if (name == "rangeTxtBox")
            {
                if (!parseStatus)
                    return;

                if (selectedLight is SpotLight)
                    (selectedLight as SpotLight).Range = fData;
                else if (selectedLight is PointLight)
                    (selectedLight as PointLight).Range = fData;
                else
                    throw new ArgumentException();
            }
            else if (name == "intensityTxtBox")
            {
                if (!parseStatus)
                    return;

                if (selectedLight is SpotLight)
                    (selectedLight as SpotLight).DiffuseIntensity = fData;
                else if (selectedLight is PointLight)
                    (selectedLight as PointLight).DiffuseIntensity = fData;
                else if (selectedLight is DirLight)
                    (selectedLight as DirLight).DiffuseIntensity = fData;
                else
                    throw new ArgumentException();
            }
            else if (name == "specIntenTxtBox")
            {
                if (!parseStatus)
                    return;

                if (selectedLight is SpotLight)
                    (selectedLight as SpotLight).SpecularIntensity = fData;
                else if (selectedLight is PointLight)
                    (selectedLight as PointLight).SpecularIntensity = fData;
                else if (selectedLight is DirLight)
                    (selectedLight as DirLight).SpecularIntensity = fData;
                else
                    throw new ArgumentException();
            }
            else if (name == "spotAngle")
            {
                if (!parseStatus)
                    return;

                if (fData < 0f)
                {
                    fData = 0f;
                    GetControl("spotAngle").Text = "0";
                }
                else if (fData > 90f)
                {
                    fData = 90f;
                    GetControl("spotAngle").Text = "90";
                }

                (selectedLight as SpotLight).SpotConeAngle = fData;
            }
            else if (name == "depthBias")
            {
                if (!parseStatus)
                    return;

                if (selectedLight is SpotLight)
                    (selectedLight as SpotLight).DepthBias = fData;
                else if (selectedLight is DirLight)
                    (selectedLight as DirLight).ShadowBias = fData;
            }
            else if (name == "spotExponent")
            {
                if (!parseStatus)
                    return;

                (selectedLight as SpotLight).SpotExponent = fData;
            }
            else if (name == "xRotTxtBox")
            {
                if (!parseStatus)
                    return;

                fData = MathHelper.ToRadians(fData);

                if (selectedLight is SpotLight)
                    (selectedLight as SpotLight).Rotation.X = fData;
                else if (selectedLight is DirLight)
                    (selectedLight as DirLight).Rotation.X = fData;
            }
            else if (name == "yRotTxtBox")
            {
                if (!parseStatus)
                    return;

                fData = MathHelper.ToRadians(fData);

                if (selectedLight is SpotLight)
                    (selectedLight as SpotLight).Rotation.Y = fData;
                else if (selectedLight is DirLight)
                    (selectedLight as DirLight).Rotation.Y = fData;
            }
            else if (name == "zRotTxtBox")
            {
                if (!parseStatus)
                    return;

                fData = MathHelper.ToRadians(fData);

                if (selectedLight is SpotLight)
                    (selectedLight as SpotLight).Rotation.Z = fData;
                else if (selectedLight is DirLight)
                    (selectedLight as DirLight).Rotation.Z = fData;
            }
            else if (name == "shadowDistanceTxtBox")
            {
                if (!parseStatus)
                    return;

                (selectedLight as DirLight).ShadowDistance = fData;
            }
            else if (name == "flareGlowSizeTxtBox")
            {
                if (!parseStatus)
                    return;

                if (selectedLight is DirLight)
                {
                    (selectedLight as DirLight).FlareGlowSize = fData;
                }
                else if (selectedLight is SpotLight)
                {
                    (selectedLight as SpotLight).FlareGlowSize = fData;
                }
            }
            else if (name == "flareQuerySizeTxtBox")
            {
                if (!parseStatus)
                    return;

                if (selectedLight is DirLight)
                {
                    (selectedLight as DirLight).FlareQuerySize = fData;
                }
                else if (selectedLight is SpotLight)
                {
                    (selectedLight as SpotLight).FlareQuerySize = fData;
                }
            }
            else if (name == "idTxtBox")
            {
                string idStr = GetControl("idTxtBox").Text;
                if (idStr != "" && !p_lightMgr.LightIDExists(idStr))
                    (selectedLight).LightID = idStr;
                CreateLightListBox();
            }
            else
                throw new ArgumentException();
        }
    }
}