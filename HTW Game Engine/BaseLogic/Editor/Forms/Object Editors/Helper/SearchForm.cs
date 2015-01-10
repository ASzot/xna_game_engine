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
    public partial class SearchForm : Form
    {
        /// <summary>
        /// The fn_on search
        /// </summary>
        private Action<string> fn_onSearch;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchForm"/> class.
        /// </summary>
        /// <param name="onSearch">The on search.</param>
        public SearchForm(Action<string> onSearch)
        {
            InitializeComponent();

            fn_onSearch += onSearch;

            searchTxtBox.Focus();
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
        /// Handles the Click event of the searchBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void searchBtn_Click(object sender, EventArgs e)
        {
            fn_onSearch(searchTxtBox.Text);

            this.Close();
        }
    }
}