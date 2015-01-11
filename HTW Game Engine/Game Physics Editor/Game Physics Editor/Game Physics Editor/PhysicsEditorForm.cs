#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Xna.Framework;

namespace Game_Physics_Editor
{
    /// <summary>
    /// The user interaction panel where the user makes can input the modifications to the physics data.
    /// </summary>
    public partial class PhysicsEditorForm : Form
    {
        /// <summary>
        /// Store a reference to the main game instance as all of the physics data needs to be accessed.
        /// </summary>
        private MainGame p_game;

        /// <summary>
        /// Construct a new instance of the physics editor form.
        /// </summary>
        /// <param name="pGame">A reference to the MainGame instance.</param>
        public PhysicsEditorForm(MainGame pGame)
        {
            p_game = pGame;
            InitializeComponent();

            UpdateListBox();

            wireframeCheckBox.Checked = p_game.RenderWireFrame;
        }

        /// <summary>
        /// Update all of the bounding boxes in the list box.
        /// </summary>
        public void UpdateListBox()
        {
            bbListBox.Items.Clear();

            for (int i = 0; i < p_game.GameBBs.Count; ++i)
            {
                bbListBox.Items.Add("bounding box " + i.ToString());
            }
        }

        /// <summary>
        /// Update the UI with all of the information about the selected bounding box.
        /// </summary>
        public void UpdateUI()
        {
            int selectedIndex = bbListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            BoundingBox bb = p_game.GameBBs[selectedIndex];

            if (!minPosX.Focused)
            {
                minPosX.Text = bb.Min.X.ToString();
            }
            if (!minPosY.Focused)
            {
                minPosY.Text = bb.Min.Y.ToString();
            }
            if (!minPosZ.Focused)
            {
                minPosZ.Text = bb.Min.Z.ToString();
            }
            if (!maxPosX.Focused)
            {
                maxPosX.Text = bb.Max.X.ToString();
            }
            if (!maxPosY.Focused)
            {
                maxPosY.Text = bb.Max.Y.ToString();
            }
            if (!maxPosZ.Focused)
            {
                maxPosZ.Text = bb.Max.Z.ToString();
            }
        }

        /// <summary>
        /// Add a new bounding box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addBtn_Click(object sender, EventArgs e)
        {
            p_game.GameBBs.Add(new Microsoft.Xna.Framework.BoundingBox(new Vector3(-1f), new Vector3(1f)));

            UpdateListBox();
        }

        /// <summary>
        /// Delete the selected bounding box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = bbListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            p_game.GameBBs.RemoveAt(selectedIndex);

            UpdateListBox();
        }

        /// <summary>
        /// Close the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// When the user enters new physics information into the text box update the objects physics 
        /// data accordinly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            int selectedIndex = bbListBox.SelectedIndex;
            if (selectedIndex == -1)
                return;

            TextBox textbox = (TextBox)sender;

            string name = textbox.Name;
            string text = textbox.Text;

            float fParsed;
            bool fParseSuccess = float.TryParse(text, out fParsed);

            BoundingBox bb = p_game.GameBBs[selectedIndex];

            if (name == "minPosX" && fParseSuccess)
            {
                bb.Min.X = fParsed;
            }
            else if (name == "minPosY" && fParseSuccess)
            {
                bb.Min.Y = fParsed;
            }
            else if (name == "minPosZ" && fParseSuccess)
            {
                bb.Min.Z = fParsed;
            }
            else if (name == "maxPosX" && fParseSuccess)
            {
                bb.Max.X = fParsed;
            }
            else if (name == "maxPosY" && fParseSuccess)
            {
                bb.Max.Y = fParsed;
            }
            else if (name == "maxPosZ" && fParseSuccess)
            {
                bb.Max.Z = fParsed;
            }

            p_game.GameBBs.RemoveAt(selectedIndex);
            p_game.GameBBs.Insert(selectedIndex, bb);
        }

        /// <summary>
        /// Update the UI with the newly selected bounding box data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void wireframeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            bool check = checkBox.Checked;

            p_game.RenderWireFrame = check;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            bool success = p_game.SaveData();

            if (success)
            {
                MessageBox.Show("Save successful!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Save unsuccessful!", "Not saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
