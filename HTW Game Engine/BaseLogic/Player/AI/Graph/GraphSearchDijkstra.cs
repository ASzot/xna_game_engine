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
    ///
    /// </summary>
    public class GraphSearchDijkstra : SearchAlgorithim
    {
        /// <summary>
        /// The _cost to this node
        /// </summary>
        private List<float> _costToThisNode = new List<float>();

        /// <summary>
        /// The _graph
        /// </summary>
        private SparseGraph _graph;

        /// <summary>
        /// The _search frontier
        /// </summary>
        private List<GraphEdge> _searchFrontier = new List<GraphEdge>();

        /// <summary>
        /// The _shortest path tree
        /// </summary>
        private List<GraphEdge> _shortestPathTree = new List<GraphEdge>();

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
        /// Initializes a new instance of the <see cref="GraphSearchDijkstra"/> class.
        /// </summary>
        public GraphSearchDijkstra()
        {
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

            int numNodes = graph.GetNumNodes();

            for (int i = 0; i < numNodes; ++i)
            {
                _searchFrontier.Add(null);
                _shortestPathTree.Add(null);
                _costToThisNode.Add(0f);
            }

            b_found = Search();
        }

        /// <summary>
        /// Gets the cost to target.
        /// </summary>
        /// <returns></returns>
        public float GetCostToTarget()
        {
            return _costToThisNode[i_target];
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

            path.Add(nd);
            while (nd != i_source && !(_shortestPathTree[nd] == null))
            {
                nd = _shortestPathTree[nd].From;

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
            IndexedPriorityQLow pq = new IndexedPriorityQLow(_costToThisNode, _graph.GetNumNodes());

            pq.Insert(i_source);

            while (!pq.Empty())
            {
                int nextClosestNode = pq.Pop();

                _shortestPathTree[nextClosestNode] = _searchFrontier[nextClosestNode];

                if (nextClosestNode == i_target)
                    return true;

                IEnumerable<GraphEdge> nodeEdges = _graph.GetNodeEdges(nextClosestNode);
                foreach (GraphEdge edge in nodeEdges)
                {
                    float newCost = _costToThisNode[nextClosestNode] + edge.Cost;

                    if (_searchFrontier[edge.To] == null)
                    {
                        _costToThisNode[edge.To] = newCost;
                        pq.Insert(edge.To);
                        _searchFrontier[edge.To] = edge;
                    }
                    else if ((newCost < _costToThisNode[edge.To]) && (_shortestPathTree[edge.To] == null))
                    {
                        _costToThisNode[edge.To] = newCost;
                        pq.ChangePriority(edge.To);
                        _searchFrontier[edge.To] = edge;
                    }
                }
            }

            return false;
        }
    }
}