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
    public partial class AddEdgeForm : Form
    {
        /// <summary>
        /// The p_graph
        /// </summary>
        private SparseGraph p_graph;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddEdgeForm"/> class.
        /// </summary>
        /// <param name="pGraph">The p graph.</param>
        public AddEdgeForm(SparseGraph pGraph)
        {
            p_graph = pGraph;

            InitializeComponent();

            // Set the list boxes.
            SetListBoxNodes(fromNodeListBox);
            SetListBoxNodes(toNodeListBox);
        }

        /// <summary>
        /// Handles the Click event of the addEdgeBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void addEdgeBtn_Click(object sender, EventArgs e)
        {
            int fromIndex = fromNodeListBox.SelectedIndex;
            int toIndex = toNodeListBox.SelectedIndex;

            GraphNode fromNode = p_graph.Nodes[fromIndex];
            GraphNode toNode = p_graph.Nodes[toIndex];

            if (toIndex == -1 || fromIndex == -1)
            {
                MessageBox.Show("A node must be selected for both the to and from nodes!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            p_graph.AddEdge(toNode.Index, fromNode.Index);

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

        /// <summary>
        /// Sets the ListBox nodes.
        /// </summary>
        /// <param name="listBox">The list box.</param>
        private void SetListBoxNodes(ListBox listBox)
        {
            int nodeCount = p_graph.GetNumNodes();
            string[] nodeNames = new string[nodeCount];
            for (int i = 0; i < nodeCount; ++i)
            {
                nodeNames[i] = "node " + i.ToString();
            }

            listBox.Items.Clear();
            listBox.Items.AddRange(nodeNames);
        }
    }
}