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

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class GameObjEditorForm : Form
    {
        /// <summary>
        /// The form update funcs
        /// </summary>
        public List<Action> FormUpdateFuncs;

        /// <summary>
        /// The p_game system
        /// </summary>
        private GameSystem p_gameSystem;

        /// <summary>
        /// The p_obj MGR
        /// </summary>
        private Manager.ObjectMgr p_objMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObjEditorForm"/> class.
        /// </summary>
        public GameObjEditorForm()
        {
            InitializeComponent();

            string[] gameTagOptions =
            {
                "(Nothing)",
                "swinging door",
                "translating door",
                "holdable"
            };

            tagComboBox.Items.Clear();
            tagComboBox.Items.AddRange(gameTagOptions);
            tagComboBox.SelectedIndex = 0;

            editTypeBtn.Enabled = false;
        }

        /// <summary>
        /// Sets the index of the selected object.
        /// </summary>
        /// <value>
        /// The index of the selected object.
        /// </value>
        public int SelectedObjIndex
        {
            set { objsListBox.SelectedIndex = value; }
        }

        /// <summary>
        /// Sets the object MGR.
        /// </summary>
        /// <param name="pGameSystem">The p game system.</param>
        public void SetObjMgr(GameSystem pGameSystem)
        {
            p_gameSystem = pGameSystem;
            p_objMgr = pGameSystem.ObjMgr;

            CreateObjListBox();

            objModifierPanel.Visible = false;
            deleteSelectedObjTxtBox.Visible = false;
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        public void UpdateUI()
        {
            if (p_objMgr.GetNumberOfDataElements() != objsListBox.Items.Count)
                CreateObjListBox();

            int selectedIndex = objsListBox.SelectedIndex;

            if (selectedIndex == -1)
            {
                ClearUI();
                objModifierPanel.Visible = false;
                deleteSelectedObjTxtBox.Visible = false;
                return;
            }
            else
            {
                objModifierPanel.Visible = true;
                deleteSelectedObjTxtBox.Visible = true;
            }

            GameObj gameObj = p_objMgr.GetDataElementAt(selectedIndex);

            if (OriginMgr.OriginObj != null && OriginMgr.OriginObj.ActorID == gameObj.ActorID)
            {
                originLbl.Text = "(This object is the origin)";
            }
            else if (OriginMgr.OriginObj == null)
            {
                originLbl.Text = "";
            }
            else
            {
                originLbl.Text = OriginMgr.OriginObj.ActorID + " is the origin";
            }

            int subsetMatCount = -1;

            if (gameObj is StaticObj)
            {
                subsetMatCount = (gameObj as StaticObj).SubsetMaterials.Count;
                editPhysBtn.Enabled = true;
                gamePanel.Visible = true;
                if (gameObj is Object.DoorObj)
                {
                    editTypeBtn.Enabled = true;
                    if (gameObj is Object.SwingingDoorObj && !tagComboBox.Focused)
                        tagComboBox.SelectedIndex = 1;
                    else if (gameObj is Object.TranslatingDoorObj && !tagComboBox.Focused)
                        tagComboBox.SelectedIndex = 2;
                }
                else if (gameObj is Object.HoldableObj)
                {
                    editTypeBtn.Enabled = true;
                    tagComboBox.SelectedIndex = 3;
                }
                else
                {
                    if (!tagComboBox.Focused)
                        tagComboBox.SelectedIndex = 0;
                }
            }
            else if (gameObj is AnimatedObj)
            {
                subsetMatCount = (gameObj as AnimatedObj).SubsetMaterials.Count;
                editPhysBtn.Enabled = false;
                gamePanel.Visible = false;
                if (!tagComboBox.Focused)
                    tagComboBox.SelectedIndex = 0;
            }
            else if (gameObj is Object.PhasedAnimObj)
            {
                subsetMatCount = (gameObj as Object.PhasedAnimObj).SubsetMaterials.Count;
                editPhysBtn.Enabled = false;
                gamePanel.Visible = false;
                if (!tagComboBox.Focused)
                    tagComboBox.SelectedIndex = 0;
            }
            else if (gameObj is Object.WaterObject)
            {
                subsetMatCount = 1;
                editPhysBtn.Enabled = false;
                gamePanel.Visible = false;
                if (!tagComboBox.Focused)
                    tagComboBox.SelectedIndex = 0;
            }
            else
            {
                matEditPanel.Visible = false;
                editPhysBtn.Enabled = false;
                gamePanel.Visible = false;
                if (!tagComboBox.Focused)
                    tagComboBox.SelectedIndex = 0;
            }

            if (subsetMatCount != -1 && !selectedMatComboBox.Focused)
            {
                selectedMatComboBox.Items.Clear();
                for (int i = 0; i < subsetMatCount; ++i)
                    selectedMatComboBox.Items.Add(i.ToString());
            }

            Vector3 position = gameObj.Position;
            position = OriginMgr.WorldToLocal(position);

            if (!xPosTxtBox.Focused)
                xPosTxtBox.Text = position.X.ToString();
            if (!yPosTxtBox.Focused)
                yPosTxtBox.Text = position.Y.ToString();
            if (!zPosTxtBox.Focused)
                zPosTxtBox.Text = position.Z.ToString();
            if (!scaleTxtBox.Focused)
                scaleTxtBox.Text = gameObj.Scale.ToString();
            if (!idTxtBox.Focused)
                idTxtBox.Text = gameObj.ActorID;
        }

        /// <summary>
        /// Handles the Click event of the addAnimObjBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void addAnimObjBtn_Click(object sender, EventArgs e)
        {
            CreateAnimatedObjForm caof = new CreateAnimatedObjForm(p_gameSystem);
            caof.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the addNewObjTxtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void addNewObjTxtBox_Click(object sender, EventArgs e)
        {
            CreateObjEditorForm coef = new CreateObjEditorForm(p_gameSystem);
            coef.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the addOtherObjBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void addOtherObjBtn_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the applyBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void applyBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = objsListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            float rx;
            if (rotXTxtBox.Text == "")
                rx = 0.0f;
            else if (!float.TryParse(rotXTxtBox.Text, out rx))
                return;
            rx = MathHelper.ToRadians(rx);

            float ry;
            if (rotYTxtBox.Text == "")
                ry = 0.0f;
            else if (!float.TryParse(rotYTxtBox.Text, out ry))
                return;
            ry = MathHelper.ToRadians(ry);

            float rz;
            if (rotZTxtBox.Text == "")
                rz = 0.0f;
            else if (!float.TryParse(rotZTxtBox.Text, out rz))
                return;
            rz = MathHelper.ToRadians(rz);

            Quaternion rotQuat = Quaternion.CreateFromYawPitchRoll(ry, rx, rz);
            GameObj gameObj = p_objMgr.GetDataElementAt(selectedIndex);
            gameObj.Rotation = rotQuat;

            rotXTxtBox.Clear();
            rotYTxtBox.Clear();
            rotZTxtBox.Clear();
        }

        /// <summary>
        /// Clears the UI.
        /// </summary>
        private void ClearUI()
        {
            xPosTxtBox.Clear();
            yPosTxtBox.Clear();
            zPosTxtBox.Clear();
            scaleTxtBox.Clear();
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
        /// Creates the object ListBox.
        /// </summary>
        private void CreateObjListBox()
        {
            int selectedIndex = objsListBox.SelectedIndex;
            int previousCount = objsListBox.Items.Count;

            objsListBox.Items.Clear();

            string[] gameObjNames = new string[p_objMgr.GetNumberOfDataElements()];

            for (int i = 0; i < p_objMgr.GetNumberOfDataElements(); ++i)
            {
                gameObjNames[i] = p_objMgr.GetDataElementAt(i).ActorID;
            }
            objsListBox.Items.AddRange(gameObjNames);

            if (objsListBox.Items.Count >= previousCount)
            {
                objsListBox.SelectedIndex = selectedIndex;
            }
        }

        /// <summary>
        /// Handles the Click event of the deleteSelectedObjTxtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void deleteSelectedObjTxtBox_Click(object sender, EventArgs e)
        {
            int selectedIndex = objsListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            p_objMgr.RemoveDataElement(selectedIndex);
        }

        /// <summary>
        /// Handles the Click event of the duplicateBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void duplicateBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = objsListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            GameObj gameObj = p_objMgr.GetDataElementAt(selectedIndex);

            DuplicateObjForm dof = new DuplicateObjForm(gameObj, p_objMgr);
            dof.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the duplicateGroupBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void duplicateGroupBtn_Click(object sender, EventArgs e)
        {
            DuplicateGroupForm dgf = new DuplicateGroupForm(p_objMgr);
            dgf.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the editGroupBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void editGroupBtn_Click(object sender, EventArgs e)
        {
            ObjGroupEditorForm ogef = new ObjGroupEditorForm(p_objMgr);
            ogef.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the editMatBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.InvalidOperationException">Invalid game obj type!</exception>
        private void editMatBtn_Click(object sender, EventArgs e)
        {
            int selectedObjIndex = objsListBox.SelectedIndex;
            if (selectedObjIndex == -1)
                return;

            string str = selectedMatComboBox.Text;
            int selectedMatIndex;
            if (!int.TryParse(str, out selectedMatIndex))
                return;

            GameObj gameObj = p_objMgr.GetDataElementAt(selectedObjIndex);

            List<SubsetMaterial> subsetMats;
            if (gameObj is StaticObj)
                subsetMats = (gameObj as StaticObj).SubsetMaterials;
            else if (gameObj is AnimatedObj)
                subsetMats = (gameObj as AnimatedObj).SubsetMaterials;
            else if (gameObj is Object.PhasedAnimObj)
                subsetMats = (gameObj as Object.PhasedAnimObj).SubsetMaterials;
            else if (gameObj is Object.WaterObject)
            {
                WaterObjEditorForm woef = new WaterObjEditorForm(gameObj as Object.WaterObject);
                woef.ShowDialog();
                return;
            }
            else
                throw new InvalidOperationException("Invalid game obj type!");

            MaterialEditorForm matEditorForm = new MaterialEditorForm(subsetMats[selectedMatIndex]);

            Action<SubsetMaterial> onApplySubsetMat = (SubsetMaterial mat) =>
                {
                    subsetMats.RemoveAt(selectedMatIndex);
                    subsetMats.Insert(selectedMatIndex, mat);
                };

            matEditorForm.ApplySubsetMaterial = onApplySubsetMat;
            matEditorForm.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the editPhysBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void editPhysBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = objsListBox.SelectedIndex;

            if (selectedIndex == -1)
                return;

            GameObj gameObj = p_objMgr.GetDataElementAt(selectedIndex);
            if (!(gameObj is StaticObj))
                return;

            StaticObj physObj = gameObj as StaticObj;

            ObjPhysEditorForm opef = new ObjPhysEditorForm(physObj, p_objMgr);
            opef.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the editTypeBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void editTypeBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = objsListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            string txt = tagComboBox.Text;
            if (txt == "swinging door")
            {
                GameObj gameObj = p_objMgr.GetDataElementAt(selectedIndex);
                if (!(gameObj is Object.SwingingDoorObj))
                    return;
                Object.SwingingDoorObj doorObj = gameObj as Object.SwingingDoorObj;
                SwingingDoorObjEditorForm doorObjEditorForm = new SwingingDoorObjEditorForm(doorObj);
                doorObjEditorForm.ShowDialog();
            }
            else if (txt == "translating door")
            {
                GameObj gameObj = p_objMgr.GetDataElementAt(selectedIndex);
                if (!(gameObj is Object.TranslatingDoorObj))
                    return;
                Object.TranslatingDoorObj doorObj = gameObj as Object.TranslatingDoorObj;
                TranslatingDoorObjEditorForm doorObjEditorForm = new TranslatingDoorObjEditorForm(doorObj);
                doorObjEditorForm.ShowDialog();
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the idTxtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void idTxtBox_TextChanged(object sender, EventArgs e)
        {
            int selectedIndex = objsListBox.SelectedIndex;
            string idTxt = idTxtBox.Text;

            if (idTxt != "" && p_objMgr.ActorIDExists(idTxt))
                return;

            GameObj gameObj = p_objMgr.GetDataElementAt(selectedIndex);
            gameObj.ActorID = idTxt;

            CreateObjListBox();
        }

        /// <summary>
        /// Handles the KeyPress event of the objsListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void objsListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the user hit the backspace.
            if (e.KeyChar == '\b')
            {
                deleteSelectedObjTxtBox_Click(null, null);
            }
            if (e.KeyChar == 'f')
            {
                searchBtn_Click(null, null);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the objsListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void objsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = objsListBox.SelectedIndex;

            UpdateUI();
        }

        //////////////////////////////////////////////////////////////////////////////
        // Update the object data as the user enters in the data into the text boxes.

        /// <summary>
        /// Handles the Click event of the resetOriginBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void resetOriginBtn_Click(object sender, EventArgs e)
        {
            OriginMgr.OriginObj = null;
        }

        /// <summary>
        /// Handles the TextChanged event of the scaleTxtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void scaleTxtBox_TextChanged(object sender, EventArgs e)
        {
            int selectedIndex = objsListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            string str = scaleTxtBox.Text;

            float scale;
            // If the parse is unsuccessful don't update the static object.
            if (!float.TryParse(str, out scale))
                return;

            GameObj gameObj = p_objMgr.GetDataElementAt(selectedIndex);

            // Update the game obj based on what the user has input so far...
            // This can mean that the object is updated to incomplete information.
            gameObj.Scale = scale;
        }

        /// <summary>
        /// Handles the Click event of the searchBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void searchBtn_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = new SearchForm((string str) =>
            {
                if (!SetSelectedIndexTo(str))
                    MessageBox.Show("Couldn't find object!", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            });
            searchForm.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the setOriginBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void setOriginBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = objsListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            OriginMgr.OriginObj = p_objMgr.GetDataElementAt(selectedIndex);
        }

        /// <summary>
        /// Sets the selected index to.
        /// </summary>
        /// <param name="selId">The sel identifier.</param>
        /// <returns></returns>
        private bool SetSelectedIndexTo(string selId)
        {
            for (int i = 0; i < objsListBox.Items.Count; ++i)
            {
                string objId = (string)objsListBox.Items[i];

                if (objId == selId)
                {
                    objsListBox.SelectedIndex = i;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the tagComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tagComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            editTypeBtn.Enabled = false;
            int selectedIndex = objsListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            string txt = tagComboBox.Text;
            GameObj gameObj = p_objMgr.GetDataElementAt(selectedIndex);
            if (!(gameObj is StaticObj))
                return;

            StaticObj staticObj = gameObj as StaticObj;

            if (txt == "(Nothing)")
            {
                if (!(staticObj is Object.TranslatingDoorObj) && !(staticObj is Object.SwingingDoorObj))
                    return;

                p_objMgr.RemoveDataElement(staticObj);
                StaticObj actualStaticObj = (staticObj as Object.DoorObj).CloneStaticObj();
                p_objMgr.AddToList(actualStaticObj);
                CreateObjListBox();
                objsListBox.SelectedIndex = p_objMgr.GetNumberOfDataElements() - 1;
                editTypeBtn.Enabled = true;
            }
            else if (txt == "swinging door")
            {
                if (staticObj is Object.SwingingDoorObj)
                    return;

                p_objMgr.AddToList(new Object.SwingingDoorObj(staticObj, p_objMgr));
                // We removed the static object and replaced it with the door object we need to update the list box.
                CreateObjListBox();
                objsListBox.SelectedIndex = p_objMgr.GetNumberOfDataElements() - 1;
                editTypeBtn.Enabled = true;
            }
            else if (txt == "translating door")
            {
                if (staticObj is Object.TranslatingDoorObj)
                    return;

                p_objMgr.AddToList(new Object.TranslatingDoorObj(staticObj, p_objMgr));

                CreateObjListBox();
                objsListBox.SelectedIndex = p_objMgr.GetNumberOfDataElements() - 1;
                editTypeBtn.Enabled = true;
            }
            else if (txt == "holdable")
            {
                if (staticObj is Object.HoldableObj)
                    return;

                p_objMgr.AddToList(new Object.HoldableObj(staticObj, p_objMgr));

                CreateObjListBox();
                objsListBox.SelectedIndex = p_objMgr.GetNumberOfDataElements() - 1;
                editTypeBtn.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the xPosTxtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void xPosTxtBox_TextChanged(object sender, EventArgs e)
        {
            int selectedIndex = objsListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            string str = xPosTxtBox.Text;

            float xPos;
            // If the parse is unsuccessful don't update the static object.
            if (!float.TryParse(str, out xPos))
                return;

            GameObj gameObj = p_objMgr.GetDataElementAt(selectedIndex);

            // Update the game obj based on what the user has input so far...
            // This can mean that the object is updated to incomplete positions.
            Vector3 pos = gameObj.Position;
            pos = new Vector3(xPos, pos.Y, pos.Z);
            gameObj.Position = OriginMgr.LocalToWorld(pos);
        }

        /// <summary>
        /// Handles the TextChanged event of the yPosTxtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void yPosTxtBox_TextChanged(object sender, EventArgs e)
        {
            int selectedIndex = objsListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            string str = yPosTxtBox.Text;

            float yPos;
            // If the parse is unsuccessful don't update the static object.
            if (!float.TryParse(str, out yPos))
                return;

            GameObj gameObj = p_objMgr.GetDataElementAt(selectedIndex);

            // Update the game obj based on what the user has input so far...
            // This can mean that the object is updated to incomplete positions.
            Vector3 pos = gameObj.Position;
            pos = new Vector3(pos.X, yPos, pos.Z);
            pos = OriginMgr.LocalToWorld(pos);
            gameObj.Position = pos;
        }

        /// <summary>
        /// Handles the TextChanged event of the zPosTxtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void zPosTxtBox_TextChanged(object sender, EventArgs e)
        {
            int selectedIndex = objsListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            string str = zPosTxtBox.Text;

            float zPos;
            // If the parse is unsuccessful don't update the static object.
            if (!float.TryParse(str, out zPos))
                return;

            GameObj gameObj = p_objMgr.GetDataElementAt(selectedIndex);

            // Update the game obj based on what the user has input so far...
            // This can mean that the object is updated to incomplete positions.
            Vector3 pos = gameObj.Position;
            pos = new Vector3(pos.X, pos.Y, zPos);
            gameObj.Position = OriginMgr.LocalToWorld(pos);
        }
    }
}