#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System.Collections.Generic;
using System.Linq;

using BaseLogic.Player;
using BaseLogic.Player.AI.Graph;

namespace Xna_Game_AI
{
    /// <summary>
    /// Controls the graphs for the AI to use.
    /// </summary>
    public class AIMgrImpl : AIMgr
    {
        /// <summary>
        /// The _graphs
        /// </summary>
        private List<SparseGraph> _graphs = new List<SparseGraph>();

        /// <summary>
        /// The i_selected edge
        /// </summary>
        private int i_selectedEdge = -1;

        /// <summary>
        /// The i_selected graph
        /// </summary>
        private int i_selectedGraph = -1;

        /// <summary>
        /// The i_selected node
        /// </summary>
        private int i_selectedNode = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="AIMgrImpl"/> class.
        /// </summary>
        public AIMgrImpl()
        {
        }

        /// <summary>
        /// Gets or sets the path graphs.
        /// </summary>
        /// <value>
        /// The path graphs.
        /// </value>
        public List<SparseGraph> PathGraphs
        {
            get { return _graphs; }
            set { _graphs = value; }
        }

        /// <summary>
        /// Gets or sets the selected edge.
        /// </summary>
        /// <value>
        /// The selected edge.
        /// </value>
        public int SelectedEdge
        {
            get { return i_selectedEdge; }
            set { i_selectedEdge = value; }
        }

        /// <summary>
        /// Gets or sets the selected graph.
        /// </summary>
        /// <value>
        /// The selected graph.
        /// </value>
        public int SelectedGraph
        {
            get { return i_selectedGraph; }
            set { i_selectedGraph = value; }
        }

        /// <summary>
        /// Gets or sets the selected node.
        /// </summary>
        /// <value>
        /// The selected node.
        /// </value>
        public int SelectedNode
        {
            get { return i_selectedNode; }
            set { i_selectedNode = value; }
        }

        /// <summary>
        /// Creates the graph editor.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        public void CreateGraphEditor(BaseLogic.GameSystem gameSystem)
        {
            Forms.GraphEditorForm graphEditorForm = new Forms.GraphEditorForm(this);

            gameSystem.AddFormForUpdating(graphEditorForm);

            graphEditorForm.Show();
        }

        /// <summary>
        /// Gets the ai save data.
        /// </summary>
        /// <returns></returns>
        public BaseLogic.Player.AISaveData GetAISaveData()
        {
            BaseLogic.Player.AISaveData saveData = new AISaveData();
            var graphSaveData = from g in _graphs
                                where g.Serilize
                                select g.GetGraphSaveData();
            saveData.Graphs = graphSaveData.ToArray();

            return saveData;
        }

        /// <summary>
        /// Sets the ai load data.
        /// </summary>
        /// <param name="saveData">The save data.</param>
        public void SetAILoadData(BaseLogic.Player.AISaveData saveData)
        {
            foreach (GraphSaveData gsd in saveData.Graphs)
            {
                SparseGraph graph = new SparseGraph(false);
                graph.SetGraphLoadData(gsd);
                _graphs.Add(graph);
            }
        }
    }
}