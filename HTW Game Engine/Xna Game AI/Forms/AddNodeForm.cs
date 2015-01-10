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
    public partial class AddNodeForm : Form
    {
        /// <summary>
        /// The p_graph
        /// </summary>
        private SparseGraph p_graph;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddNodeForm"/> class.
        /// </summary>
        /// <param name="pGraph">The p graph.</param>
        public AddNodeForm(SparseGraph pGraph)
        {
            InitializeComponent();

            p_graph = pGraph;
        }

        /// <summary>
        /// Handles the Click event of the addNodeBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void addNodeBtn_Click(object sender, EventArgs e)
        {
            float x, y, z;

            bool success = true;
            if (!float.TryParse(xPosTxtBox.Text, out x))
                success = false;
            if (!float.TryParse(yPosTxtBox.Text, out y))
                success = false;
            if (!float.TryParse(zPosTxtBox.Text, out z))
                success = false;

            if (!success)
            {
                MessageBox.Show("Invalid data input!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            p_graph.AddNode(x, y, z);

            closeBtn_Click(null, null);
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
    }
}