#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoadLevelForm : Form
    {
        /// <summary>
        /// The on load accepted
        /// </summary>
        public Action<string> OnLoadAccepted;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadLevelForm"/> class.
        /// </summary>
        public LoadLevelForm()
        {
            InitializeComponent();

            loadBtn.Enabled = false;

            string completeFolderPath = Object.FileHelper.GetSaveLevelLocationCompleteFilename();
            string[] filePaths = Directory.GetFiles(completeFolderPath, "*.sav");

            var fileNames = from fp in filePaths
                            select Object.FileHelper.StripName(fp);

            filesListBox.Items.Clear();
            filesListBox.Items.AddRange(fileNames.ToArray());
        }

        /// <summary>
        /// Handles the Click event of the cancelBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            OnLoadAccepted(null);
            this.Close();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the filesListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void filesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filesListBox.SelectedIndex != -1)
            {
                loadBtn.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the loadBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void loadBtn_Click(object sender, EventArgs e)
        {
            string selectedFileStr = (string)filesListBox.SelectedItem;

            OnLoadAccepted(selectedFileStr);
            this.Close();
        }
    }
}