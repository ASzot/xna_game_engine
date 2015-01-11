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

namespace Game_Physics_Editor
{
    /// <summary>
    /// The form that immediately runs on startup. 
    /// The user enters the model filename relative to the Models folder
    /// and the folder name of the save location.
    /// </summary>
    public partial class ModelSelectorClass : Form
    {
        public Action<string, string> OnSubmission;

        public ModelSelectorClass()
        {
            InitializeComponent();
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            if (filenameTxtBox.Text == "" || saveLocationTxtBox.Text == "")
            {
                MessageBox.Show("Please enter a filename and save location for the physics model.", "Error");
                return;
            }
            OnSubmission(filenameTxtBox.Text, saveLocationTxtBox.Text);
            this.Close();
        }
    }
}
