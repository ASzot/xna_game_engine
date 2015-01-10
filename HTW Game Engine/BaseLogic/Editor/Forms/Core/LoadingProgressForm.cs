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
    public partial class LoadingProgressForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingProgressForm"/> class.
        /// </summary>
        public LoadingProgressForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the LoadingProgressForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void LoadingProgressForm_Load(object sender, EventArgs e)
        {
            progressBar1.MarqueeAnimationSpeed = 5;
        }
    }
}