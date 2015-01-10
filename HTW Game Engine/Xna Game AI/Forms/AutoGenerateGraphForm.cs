#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Windows.Forms;

using BaseLogic.Player.AI.Graph;

namespace Xna_Game_AI.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class AutoGenerateGraphForm : Form
    {
        /// <summary>
        /// The p_ai MGR
        /// </summary>
        private AIMgrImpl p_aiMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoGenerateGraphForm"/> class.
        /// </summary>
        /// <param name="pAIMgr">The p ai MGR.</param>
        public AutoGenerateGraphForm(AIMgrImpl pAIMgr)
        {
            p_aiMgr = pAIMgr;

            InitializeComponent();
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
        /// Handles the Click event of the generateBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void generateBtn_Click(object sender, EventArgs e)
        {
            int m, n;
            float spacing, centerX, centerY, centerZ;

            bool success = true;
            if (!float.TryParse(nodeSpacingTxtBox.Text, out spacing))
                success = false;
            if (!float.TryParse(xCenterTxtBox.Text, out centerX))
                success = false;
            if (!float.TryParse(yCenterTxtBox.Text, out centerY))
                success = false;
            if (!float.TryParse(zCenterTxtBox.Text, out centerZ))
                success = false;
            if (!int.TryParse(rowsTxtBox.Text, out m))
                success = false;
            if (!int.TryParse(columnsTxtBox.Text, out n))
                success = false;

            if (!success)
            {
                MessageBox.Show("Invalid data input!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SparseGraph graphToAdd = SparseGraph.GenerateGridGraph(m, n, spacing, new Microsoft.Xna.Framework.Vector3(centerX, centerY, centerZ));
            p_aiMgr.PathGraphs.Add(graphToAdd);

            closeBtn_Click(null, null);
        }
    }
}