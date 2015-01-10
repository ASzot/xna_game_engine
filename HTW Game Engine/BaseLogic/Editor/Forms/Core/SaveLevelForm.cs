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
    public partial class SaveLevelForm : Form
    {
        /// <summary>
        /// The on save accepted
        /// </summary>
        public Action<string> OnSaveAccepted;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveLevelForm"/> class.
        /// </summary>
        public SaveLevelForm()
        {
            InitializeComponent();

            string completeFolderPath = Object.FileHelper.GetSaveLevelLocationCompleteFilename();
            string[] filePaths = Directory.GetFiles(completeFolderPath, "*.sav");

            var fileNames = from fp in filePaths
                            select Object.FileHelper.StripName(fp);

            existingFilesListBox.Items.Clear();
            existingFilesListBox.Items.AddRange(fileNames.ToArray());
        }

        /// <summary>
        /// Handles the Click event of the acceptBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void acceptBtn_Click(object sender, EventArgs e)
        {
            string text = filenameTxtBox.Text;
            if (existingFilesListBox.Items.Contains((object)text))
            {
                DialogResult dlgResult = MessageBox.Show("Override existing save?", "Continue?", MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                if (dlgResult != System.Windows.Forms.DialogResult.Yes)
                    return;
            }
            OnSaveAccepted(text);

            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the cancelBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            OnSaveAccepted(null);
            this.Close();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the existingFilesListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void existingFilesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = (string)existingFilesListBox.SelectedItem;

            filenameTxtBox.Text = selectedItem;
        }
    }
}