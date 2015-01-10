#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Linq;
using System.Windows.Forms;

using BaseLogic.Player.AI.Graph;

namespace Xna_Game_AI.Forms
{
    /// <summary>
    ///
    /// </summary>
    public partial class GraphEditorForm : Form, RenderingSystem.UpdatingForm
    {
        /// <summary>
        /// The b_in node editor
        /// </summary>
        private bool b_inNodeEditor = true;

        /// <summary>
        /// The b_is closed
        /// </summary>
        private bool b_isClosed = false;

        /// <summary>
        /// The p_ai MGR
        /// </summary>
        private AIMgrImpl p_aiMgr = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphEditorForm"/> class.
        /// </summary>
        /// <param name="pAIMgr">The p ai MGR.</param>
        public GraphEditorForm(AIMgrImpl pAIMgr)
        {
            InitializeComponent();

            p_aiMgr = pAIMgr;

            edgeListBox.Visible = false;
            nodeListBox.Visible = false;
            nodeEditorPanel.Visible = false;
            edgeEditorPanel.Visible = false;
            addElementBtn.Enabled = false;
            deleteElementBtn.Enabled = false;
            deleteGraphBtn.Enabled = false;

            p_aiMgr.SelectedGraph = -1;
            p_aiMgr.SelectedEdge = -1;
            p_aiMgr.SelectedNode = -1;

            SparseGraph[] graphs = p_aiMgr.PathGraphs.ToArray();
            int numGraphs = p_aiMgr.PathGraphs.Count;

            string[] graphNames = new string[graphs.Count()];
            for (int i = 0; i < numGraphs; ++i)
                graphNames[i] = "graph " + i.ToString();

            graphListBox.Items.Clear();
            graphListBox.Items.AddRange(graphNames);

            UpdateListBoxes();
        }

        /// <summary>
        /// Gets a value indicating whether this instance is form closed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is form closed; otherwise, <c>false</c>.
        /// </value>
        public bool IsFormClosed
        {
            get { return b_isClosed; }
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        public void UpdateUI()
        {
            if (p_aiMgr.SelectedGraph == -1)
            {
                return;
            }
            SparseGraph[] graphs = p_aiMgr.PathGraphs.ToArray();
            SparseGraph selectedGraph = graphs[p_aiMgr.SelectedGraph];

            if (p_aiMgr.SelectedEdge != -1 && !b_inNodeEditor)
            {
                GraphEdge edge = selectedGraph.Edges[p_aiMgr.SelectedEdge];

                toTxtBox.Text = edge.To.ToString();
                fromTxtBox.Text = edge.From.ToString();
            }
            if (p_aiMgr.SelectedNode != -1 && b_inNodeEditor)
            {
                GraphNode node = selectedGraph.Nodes[p_aiMgr.SelectedNode];

                xPosTxtBox.Text = node.Position.X.ToString();
                yPosTxtBox.Text = node.Position.Y.ToString();
                zPosTxtBox.Text = node.Position.Z.ToString();

                indexTxtBox.Text = node.Index.ToString();
            }
        }

        /// <summary>
        /// Handles the Click event of the addElementBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void addElementBtn_Click(object sender, EventArgs e)
        {
            if (p_aiMgr.SelectedGraph == -1)
                return;

            SparseGraph selectedGraph = p_aiMgr.PathGraphs[p_aiMgr.SelectedGraph];

            if (b_inNodeEditor)
            {
                AddNodeForm addNodeForm = new AddNodeForm(selectedGraph);
                addNodeForm.ShowDialog();
            }
            else
            {
                AddEdgeForm addEdgeForm = new AddEdgeForm(selectedGraph);
                addEdgeForm.ShowDialog();
            }

            UpdateListBoxes();
        }

        /// <summary>
        /// Handles the Click event of the addGraphBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void addGraphBtn_Click(object sender, EventArgs e)
        {
            SparseGraph sparseGraph = new SparseGraph(true);

            p_aiMgr.PathGraphs.Add(sparseGraph);

            UpdateListBoxes();
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
        /// Handles the Click event of the deleteElementBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void deleteElementBtn_Click(object sender, EventArgs e)
        {
            if (p_aiMgr.SelectedGraph == -1)
                return;

            SparseGraph selectedGraph = p_aiMgr.PathGraphs[p_aiMgr.SelectedGraph];

            if (b_inNodeEditor)
            {
                if (p_aiMgr.SelectedNode == -1)
                    return;

                int index = int.Parse(indexTxtBox.Text);

                selectedGraph.RemoveNode(index);
                selectedGraph.RemoveAssociatedSides(index);

                p_aiMgr.SelectedNode = -1;
            }
            else
            {
                if (p_aiMgr.SelectedEdge == -1)
                    return;

                int to = int.Parse(toTxtBox.Text);
                int from = int.Parse(fromTxtBox.Text);

                selectedGraph.RemoveEdge(from, to);
                selectedGraph.RemoveEdge(to, from);

                p_aiMgr.SelectedEdge = -1;
            }

            UpdateListBoxes();
        }

        /// <summary>
        /// Handles the Click event of the deleteGraphBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void deleteGraphBtn_Click(object sender, EventArgs e)
        {
            if (p_aiMgr.SelectedGraph == -1)
                return;

            p_aiMgr.PathGraphs.RemoveAt(p_aiMgr.SelectedGraph);

            p_aiMgr.SelectedGraph = -1;
            p_aiMgr.SelectedNode = -1;
            p_aiMgr.SelectedEdge = -1;

            nodeListBox.Items.Clear();
            edgeListBox.Items.Clear();

            UpdateListBoxes();
        }

        /// <summary>
        /// Handles the MouseUp event of the edgeListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void edgeListBox_MouseUp(object sender, MouseEventArgs e)
        {
            addElementBtn.Text = "Add Edge";
            deleteElementBtn.Text = "Delete Edge";

            b_inNodeEditor = false;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the edgeListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void edgeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedEdge = edgeListBox.SelectedIndex;
            p_aiMgr.SelectedEdge = selectedEdge;

            if (selectedEdge == -1)
                return;

            addElementBtn.Text = "Add Edge";
            deleteElementBtn.Text = "Delete Edge";

            b_inNodeEditor = false;
        }

        /// <summary>
        /// Handles the Click event of the generateGraphBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void generateGraphBtn_Click(object sender, EventArgs e)
        {
            AutoGenerateGraphForm aggf = new AutoGenerateGraphForm(p_aiMgr);
            aggf.ShowDialog();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the graphListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void graphListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedGraph = graphListBox.SelectedIndex;
            p_aiMgr.SelectedGraph = selectedGraph;

            if (selectedGraph == -1)
            {
                edgeEditorPanel.Visible = false;
                nodeEditorPanel.Visible = false;
                nodeListBox.Visible = false;
                edgeListBox.Visible = false;

                addElementBtn.Enabled = false;
                deleteElementBtn.Enabled = false;

                deleteGraphBtn.Enabled = false;
            }

            edgeEditorPanel.Visible = true;
            nodeEditorPanel.Visible = true;
            nodeListBox.Visible = true;
            edgeListBox.Visible = true;
            addElementBtn.Enabled = true;
            deleteElementBtn.Enabled = true;
            deleteGraphBtn.Enabled = true;

            UpdateListBoxes();
        }

        /// <summary>
        /// Handles the MouseUp event of the nodeListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void nodeListBox_MouseUp(object sender, MouseEventArgs e)
        {
            addElementBtn.Text = "Add Node";
            deleteElementBtn.Text = "Delete Node";

            b_inNodeEditor = true;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the nodeListBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void nodeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedNode = nodeListBox.SelectedIndex;
            p_aiMgr.SelectedNode = selectedNode;

            if (selectedNode == -1)
                return;

            addElementBtn.Text = "Add Node";
            deleteElementBtn.Text = "Delete Node";

            b_inNodeEditor = true;
        }

        /// <summary>
        /// Handles the TxtChanged event of the txtBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtBox_TxtChanged(object sender, EventArgs e)
        {
            if (p_aiMgr.SelectedGraph == -1)
                return;

            SparseGraph graph = p_aiMgr.PathGraphs[p_aiMgr.SelectedGraph];

            TextBox textBox = sender as TextBox;
            string txtBoxName = textBox.Name;

            float fParsed;
            int iParsed;
            bool fParsedSuccess;
            bool iParsedSuccess;
            fParsedSuccess = float.TryParse(textBox.Text, out fParsed);
            iParsedSuccess = int.TryParse(textBox.Text, out iParsed);

            Microsoft.Xna.Framework.Vector3 pos = Microsoft.Xna.Framework.Vector3.Zero;
            if (p_aiMgr.SelectedNode != -1)
                pos = (graph.Nodes[p_aiMgr.SelectedNode]).Position;

            if (txtBoxName == "xPosTxtBox")
            {
                if (!fParsedSuccess || p_aiMgr.SelectedNode == -1)
                    return;

                GraphNode node = graph.Nodes[p_aiMgr.SelectedNode];
                node.Position = new Microsoft.Xna.Framework.Vector3(fParsed, pos.Y, pos.Z);
            }
            else if (txtBoxName == "yPosTxtBox")
            {
                if (!fParsedSuccess || p_aiMgr.SelectedNode == -1)
                    return;

                GraphNode node = graph.Nodes[p_aiMgr.SelectedNode];
                node.Position = new Microsoft.Xna.Framework.Vector3(pos.X, fParsed, pos.Z);
            }
            else if (txtBoxName == "zPosTxtBox")
            {
                if (!fParsedSuccess || p_aiMgr.SelectedNode == -1)
                    return;

                GraphNode node = graph.Nodes[p_aiMgr.SelectedNode];
                node.Position = new Microsoft.Xna.Framework.Vector3(pos.X, pos.Y, fParsed);
            }
            else if (txtBoxName == "toTxtBox")
            {
                if (!iParsedSuccess || p_aiMgr.SelectedEdge == -1)
                    return;

                GraphEdge edge = graph.Edges[p_aiMgr.SelectedEdge];
                edge.To = iParsed;
            }
            else if (txtBoxName == "fromTxtBox")
            {
                if (!iParsedSuccess || p_aiMgr.SelectedEdge == -1)
                    return;

                GraphEdge edge = graph.Edges[p_aiMgr.SelectedEdge];
                edge.From = iParsed;
            }
        }

        /// <summary>
        /// Updates the list boxes.
        /// </summary>
        private void UpdateListBoxes()
        {
            SparseGraph[] graphs = p_aiMgr.PathGraphs.ToArray();
            int numGraphs = p_aiMgr.PathGraphs.Count;

            if (!graphListBox.Focused)
            {
                string[] graphNames = new string[graphs.Count()];
                for (int i = 0; i < numGraphs; ++i)
                    graphNames[i] = "graph " + i.ToString();

                graphListBox.Items.Clear();
                graphListBox.Items.AddRange(graphNames);
            }

            if (p_aiMgr.SelectedGraph == -1)
            {
                return;
            }
            SparseGraph selectedGraph = graphs[p_aiMgr.SelectedGraph];

            if (!nodeListBox.Focused)
            {
                int numNodes = selectedGraph.GetNumActiveNodes();
                string[] nodeNames = new string[numNodes];
                for (int i = 0; i < numNodes; ++i)
                    nodeNames[i] = "node " + i.ToString();

                nodeListBox.Items.Clear();
                nodeListBox.Items.AddRange(nodeNames);
            }
            if (!edgeListBox.Focused)
            {
                int numEdges = selectedGraph.GetNumEdges();
                string[] edgeNames = new string[numEdges];
                for (int i = 0; i < numEdges; ++i)
                    edgeNames[i] = "edge " + i.ToString();

                edgeListBox.Items.Clear();
                edgeListBox.Items.AddRange(edgeNames);
            }
        }
    }
}