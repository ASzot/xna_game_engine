#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System.Collections.Generic;

namespace BaseLogic.Player.AI.Graph
{
    /// <summary>
    /// Breadth First Search algorithm.
    /// </summary>
    public class GraphSearchBFS : SearchAlgorithim
    {
        /// <summary>
        /// The _graph
        /// </summary>
        private SparseGraph _graph;

        /// <summary>
        /// The _route
        /// </summary>
        private List<int> _route;

        /// <summary>
        /// The _visited
        /// </summary>
        private List<int> _visited;

        /// <summary>
        /// The b_found
        /// </summary>
        private bool b_found;

        /// <summary>
        /// The i_source
        /// </summary>
        private int i_source;

        /// <summary>
        /// The i_target
        /// </summary>
        private int i_target;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphSearchBFS"/> class.
        /// </summary>
        public GraphSearchBFS()
        {
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="GraphSearchBFS"/> is found.
        /// </summary>
        /// <value>
        ///   <c>true</c> if found; otherwise, <c>false</c>.
        /// </value>
        public bool Found
        {
            get { return b_found; }
        }

        /// <summary>
        /// Constructs the path.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        public override void ConstructPath(SparseGraph graph, int source, int target)
        {
            _graph = graph;
            i_source = source;
            i_target = target;
            _visited = new List<int>(_graph.GetNumNodes());
            for (int i = 0; i < _graph.GetNumNodes(); ++i)
                _visited.Add(UNVISITED);
            _route = new List<int>(_graph.GetNumEdges());
            for (int i = 0; i < _graph.GetNumEdges(); ++i)
                _route.Add(NO_PARENT);

            b_found = Search();
        }

        /// <summary>
        /// Gets the path to target.
        /// </summary>
        /// <returns></returns>
        public override List<int> GetPathToTarget()
        {
            List<int> path = new List<int>();

            if (!b_found || i_target < 0)
                return null;

            int nd = i_target;

            path.Insert(0, nd);

            while (nd != i_source)
            {
                nd = _route[nd];
                path.Insert(0, nd);
            }

            return path;
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <returns></returns>
        private bool Search()
        {
            Queue<GraphEdge> stack = new Queue<GraphEdge>();

            GraphEdge dummyEdge = new GraphEdge(i_source, i_source, 0);

            stack.Enqueue(dummyEdge);

            while (stack.Count != 0)
            {
                GraphEdge next = stack.Dequeue();

                _route[next.To] = next.From;

                _visited[next.To] = VISITED;

                if (next.To == i_target)
                {
                    return true;
                }

                // Get the all of the edges which branch off this node.
                IEnumerable<GraphEdge> nodeEdges = _graph.GetNodeEdges(next.To);
                foreach (GraphEdge graphEdge in nodeEdges)
                {
                    if (_visited[graphEdge.To] == UNVISITED)
                    {
                        stack.Enqueue(graphEdge);
                        _visited[graphEdge.To] = VISITED;
                    }
                }
            }

            return false;
        }
    }
}