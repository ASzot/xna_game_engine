#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;

namespace BaseLogic.Player.AI.Graph
{
    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public class SparseGraph
    {
        /// <summary>
        /// The _edges
        /// </summary>
        private List<GraphEdge> _edges = new List<GraphEdge>();

        /// <summary>
        /// The _nodes
        /// </summary>
        private List<GraphNode> _nodes = new List<GraphNode>();

        /// <summary>
        /// The b_digraph
        /// </summary>
        private bool b_digraph;

        /// <summary>
        /// The b_serilize
        /// </summary>
        private bool b_serilize = true;

        /// <summary>
        /// The i_next node index
        /// </summary>
        private int i_nextNodeIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="SparseGraph"/> class.
        /// </summary>
        /// <param name="digraph">if set to <c>true</c> [digraph].</param>
        public SparseGraph(bool digraph)
        {
            i_nextNodeIndex = 0;
            b_digraph = digraph;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="SparseGraph"/> is digraph.
        /// </summary>
        /// <value>
        ///   <c>true</c> if digraph; otherwise, <c>false</c>.
        /// </value>
        public bool Digraph
        {
            get { return b_digraph; }
        }

        /// <summary>
        /// Gets the edges.
        /// </summary>
        /// <value>
        /// The edges.
        /// </value>
        public List<GraphEdge> Edges
        {
            get { return _edges; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmpty
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the nodes.
        /// </summary>
        /// <value>
        /// The nodes.
        /// </value>
        public List<GraphNode> Nodes
        {
            get { return _nodes; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SparseGraph"/> is serilize.
        /// </summary>
        /// <value>
        ///   <c>true</c> if serilize; otherwise, <c>false</c>.
        /// </value>
        public bool Serilize
        {
            get { return b_serilize; }
            set { b_serilize = value; }
        }

        /// <summary>
        /// Generates the grid graph.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <param name="spacing">The spacing.</param>
        /// <param name="centerOffset">The center offset.</param>
        /// <returns></returns>
        public static SparseGraph GenerateGridGraph(int m, int n, float spacing, Vector3 centerOffset)
        {
            GraphNode[,] nodes = GenerateGraphNodes(n, m, spacing, centerOffset);
            // Copy over to the sparse graph.
            SparseGraph sparseGraph = new SparseGraph(false);
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                    sparseGraph.Nodes.Add(nodes[i, j]);
            }

            List<GraphEdge> edges = GenerateGraphEdges(nodes, m, n);

            sparseGraph.Edges.AddRange(edges);

            return sparseGraph;
        }

        /// <summary>
        /// Adds the edge.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        public bool AddEdge(int from, int to)
        {
            return AddEdge(from, to, 1.0f);
        }

        /// <summary>
        /// Adds the edge.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="cost">The cost.</param>
        /// <returns></returns>
        public bool AddEdge(int from, int to, float cost)
        {
            return AddEdge(new GraphEdge(from, to, cost));
        }

        /// <summary>
        /// Adds the edge.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <returns></returns>
        public bool AddEdge(GraphEdge edge)
        {
            bool copyExists = false;
            foreach (GraphEdge cEdge in _edges)
            {
                if (cEdge == edge)
                    copyExists = true;
            }
            if (!copyExists)
                _edges.Add(edge);

            return copyExists;
        }

        /// <summary>
        /// Adds the edges for rendering.
        /// </summary>
        /// <param name="primitivesRenderer">The primitives renderer.</param>
        /// <param name="renderColor">Color of the render.</param>
        /// <param name="selectedRenderColor">Color of the selected render.</param>
        /// <param name="selectedIndicies">The selected indicies.</param>
        public void AddEdgesForRendering(BaseLogic.Graphics.PrimitivesRenderer primitivesRenderer,
            Microsoft.Xna.Framework.Color renderColor, Microsoft.Xna.Framework.Color selectedRenderColor, List<int> selectedIndicies)
        {
            for (int i = 0; i < _edges.Count; ++i)
            {
                GraphEdge edge = _edges[i];

                GraphNode node1 = GetNode(edge.To);
                GraphNode node2 = GetNode(edge.From);

                if (selectedIndicies.Contains(i))
                    primitivesRenderer.AddRenderLine(node1.Position, node2.Position, selectedRenderColor);
                else
                    primitivesRenderer.AddRenderLine(node1.Position, node2.Position, renderColor);
            }
        }

        /// <summary>
        /// Adds the node.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <returns></returns>
        public int AddNode(float x, float y, float z)
        {
            return AddNode(new Microsoft.Xna.Framework.Vector3(x, y, z));
        }

        /// <summary>
        /// Adds the node.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <returns></returns>
        public int AddNode(Microsoft.Xna.Framework.Vector3 pos)
        {
            GraphNode graphNode = new GraphNode(i_nextNodeIndex++);
            graphNode.Position = pos;
            _nodes.Add(graphNode);

            return i_nextNodeIndex;
        }

        /// <summary>
        /// Adds the node.
        /// </summary>
        /// <returns></returns>
        public int AddNode()
        {
            GraphNode graphNode = new GraphNode(i_nextNodeIndex++);
            _nodes.Add(graphNode);

            return i_nextNodeIndex;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            _edges.Clear();
            _nodes.Clear();
            i_nextNodeIndex = 0;
        }

        /// <summary>
        /// Generates the distance based costs.
        /// </summary>
        public void GenerateDistanceBasedCosts()
        {
            foreach (GraphEdge edge in _edges)
            {
                GraphNode from = GetNode(edge.From);
                GraphNode to = GetNode(edge.To);

                float distance = Vector3.Distance(from.Position, to.Position);
                edge.Cost = distance;
            }
        }

        /// <summary>
        /// Gets the edge.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        public GraphEdge GetEdge(int from, int to)
        {
            foreach (GraphEdge edge in _edges)
            {
                if (edge.From == from && edge.To == to)
                    return edge;
            }
            return null;
        }

        /// <summary>
        /// Gets the graph save data.
        /// </summary>
        /// <returns></returns>
        public GraphSaveData GetGraphSaveData()
        {
            GraphSaveData graphSaveData;
            var nodeSaveDatas = from n in _nodes
                                select n.GetNodeSaveData();
            var edgeSaveDatas = from e in _edges
                                select e.GetEdgeSaveData();
            graphSaveData.NodeSaveData = nodeSaveDatas.ToArray();
            graphSaveData.EdgeSaveData = edgeSaveDatas.ToArray();
            graphSaveData.NextNodeIndex = i_nextNodeIndex;
            graphSaveData.Digraph = b_digraph;

            return graphSaveData;
        }

        /// <summary>
        /// Gets the next free node.
        /// </summary>
        /// <returns></returns>
        public int GetNextFreeNode()
        {
            return i_nextNodeIndex;
        }

        /// <summary>
        /// Gets the node.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public GraphNode GetNode(int index)
        {
            foreach (GraphNode node in _nodes)
            {
                if (node.Index == index)
                    return node;
            }
            return null;
        }

        /// <summary>
        /// Gets the node closest to position.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <returns></returns>
        public GraphNode GetNodeClosestToPosition(Vector3 pos)
        {
            float closestDistSq = float.MaxValue;
            GraphNode closestNode = null;

            foreach (GraphNode node in _nodes)
            {
                float distanceSq = Vector3.DistanceSquared(pos, node.Position);
                if (distanceSq < closestDistSq)
                {
                    closestNode = node;
                    closestDistSq = distanceSq;
                }
            }

            return closestNode;
        }

        /// <summary>
        /// Gets the node edges.
        /// </summary>
        /// <param name="nodeIndex">Index of the node.</param>
        /// <returns></returns>
        public IEnumerable<GraphEdge> GetNodeEdges(int nodeIndex)
        {
            List<GraphEdge> graphEdges = new List<GraphEdge>();
            foreach (GraphEdge edge in _edges)
            {
                if (edge.From == nodeIndex)
                    graphEdges.Add(edge);
            }

            return graphEdges;
        }

        /// <summary>
        /// Gets the number active nodes.
        /// </summary>
        /// <returns></returns>
        public int GetNumActiveNodes()
        {
            // So far there is no difference between an inactive and active node.
            return GetNumNodes();
        }

        /// <summary>
        /// Gets the number edges.
        /// </summary>
        /// <returns></returns>
        public int GetNumEdges()
        {
            return _edges.Count;
        }

        /// <summary>
        /// Gets the number nodes.
        /// </summary>
        /// <returns></returns>
        public int GetNumNodes()
        {
            return _nodes.Count;
        }

        /// <summary>
        /// Removes the associated sides.
        /// </summary>
        /// <param name="nodeIndex">Index of the node.</param>
        public void RemoveAssociatedSides(int nodeIndex)
        {
            for (int i = 0; i < _edges.Count; ++i)
            {
                GraphEdge edge = _edges.ElementAt(i);
                if (edge.To == nodeIndex || edge.From == nodeIndex)
                    _edges.RemoveAt(i++);
            }
        }

        /// <summary>
        /// Removes the edge.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        public bool RemoveEdge(int from, int to)
        {
            for (int i = 0; i < _edges.Count; ++i)
            {
                GraphEdge edge = _edges[i];
                if (edge.From == from && edge.To == to)
                {
                    _edges.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes nodes which don't have any edges associated with them.
        /// </summary>
        public void RemoveLoneNodes()
        {
            for (int i = 0; i < _nodes.Count; ++i)
            {
                GraphNode node = _nodes[i];
                bool found = false;
                for (int j = 0; j < _edges.Count; ++j)
                {
                    GraphEdge edge = _edges[j];
                    if (edge.From == node.Index || edge.To == node.Index)
                        found = true;
                }
                if (!found)
                    _nodes.RemoveAt(i++);
            }
        }

        /// <summary>
        /// Removes the node.
        /// </summary>
        /// <param name="nodeIndex">Index of the node.</param>
        /// <returns></returns>
        public bool RemoveNode(int nodeIndex)
        {
            for (int i = 0; i < _nodes.Count; ++i)
            {
                GraphNode cNode = _nodes.ElementAt(i);
                if (cNode.Index == nodeIndex)
                {
                    _nodes.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes the obstructing edges.
        /// </summary>
        /// <param name="physics">The physics.</param>
        public void RemoveObstructingEdges(BaseLogic.Manager.GamePhysicsMgr physics)
        {
            for (int i = 0; i < _edges.Count; ++i)
            {
                GraphEdge edge = _edges[i];
                GraphNode fromNode = GetNode(edge.From);
                GraphNode toNode = GetNode(edge.To);
                BaseLogic.Manager.RaytraceFireInfo rayFireInfo =
                    new BaseLogic.Manager.RaytraceFireInfo(fromNode.Position, toNode.Position);

                BaseLogic.Manager.RaytraceIntersectionInfo rii = physics.Raytrace(rayFireInfo);
                if (rii != null)
                {
                    // This edge is obstructing geometry we have to remove it.
                    _edges.RemoveAt(i++);
                }
            }
        }

        /// <summary>
        /// Sets the graph load data.
        /// </summary>
        /// <param name="graphSaveData">The graph save data.</param>
        public void SetGraphLoadData(GraphSaveData graphSaveData)
        {
            i_nextNodeIndex = graphSaveData.NextNodeIndex;
            b_digraph = graphSaveData.Digraph;

            foreach (NodeSaveData nsd in graphSaveData.NodeSaveData)
            {
                GraphNode graphNode = new GraphNode();
                graphNode.SetNodeLoadData(nsd);
                _nodes.Add(graphNode);
            }

            foreach (EdgeSaveData esd in graphSaveData.EdgeSaveData)
            {
                GraphEdge graphEdge = new GraphEdge();
                graphEdge.SetEdgeLoadData(esd);
                _edges.Add(graphEdge);
            }
        }

        /// <summary>
        /// Generates the graph edges.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <returns></returns>
        private static List<GraphEdge> GenerateGraphEdges(GraphNode[,] nodes, int m, int n)
        {
            List<GraphEdge> edges = new List<GraphEdge>();

            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    IEnumerable<GraphEdge> neighborEdges = GetNeighborEdgesOfNode(i, j, nodes, m, n);
                    edges.AddRange(neighborEdges);
                }
            }

            return edges;
        }

        /// <summary>
        /// Generates the graph nodes.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <param name="spacing">The spacing.</param>
        /// <param name="centerOffset">The center offset.</param>
        /// <returns></returns>
        private static GraphNode[,] GenerateGraphNodes(int m, int n, float spacing, Vector3 centerOffset)
        {
            int centerSpotX = m / 2;
            int centerSpotZ = n / 2;

            Vector3 topLeftPos = new Vector3(0f, centerOffset.Y, 0f);
            topLeftPos.X = centerOffset.X - (spacing * (float)centerSpotX);
            topLeftPos.Z = centerOffset.Z - (spacing * (float)centerSpotZ);

            int nodeIndex = 0;

            GraphNode[,] nodes = new GraphNode[m, n];

            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Vector3 pos = new Vector3(spacing * (float)i, 0f, spacing * (float)j);
                    pos += topLeftPos;
                    nodes[i, j] = new GraphNode(nodeIndex++, pos);
                }
            }

            return nodes;
        }

        /// <summary>
        /// Gets the neighbor edges of node.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="nodes">The nodes.</param>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <returns></returns>
        private static IEnumerable<GraphEdge> GetNeighborEdgesOfNode(int i, int j, GraphNode[,] nodes, int m, int n)
        {
            // In thhe depiction below the pluses are the neighbors this function will generate. The '@' is the current node.
            /*
             * + + +
             * + @ +
             * + + +
             */

            GraphNode fromNode = nodes[i, j];

            GraphEdge edge00 = ((i - 1) > 0 && (j - 1) > 0) ? new GraphEdge(fromNode.Index, nodes[i - 1, j - 1].Index) : null;
            GraphEdge edge01 = ((j - 1) > 0) ? new GraphEdge(fromNode.Index, nodes[i, j - 1].Index) : null;
            GraphEdge edge02 = ((i + 1) < m && (j - 1) > 0) ? new GraphEdge(fromNode.Index, nodes[i + 1, j - 1].Index) : null;

            GraphEdge edge10 = (i - 1 > 0) ? new GraphEdge(fromNode.Index, nodes[i - 1, j].Index) : null;
            // We are not going to include the current edge.
            GraphEdge edge13 = (i + 1 < m) ? new GraphEdge(fromNode.Index, nodes[i + 1, j].Index) : null;

            GraphEdge edge20 = (i - 1 > 0 && j + 1 < n) ? new GraphEdge(fromNode.Index, nodes[i - 1, j + 1].Index) : null;
            GraphEdge edge21 = (j + 1 < n) ? new GraphEdge(fromNode.Index, nodes[i, j + 1].Index) : null;
            GraphEdge edge22 = (i + 1 < m && j + 1 < n) ? new GraphEdge(fromNode.Index, nodes[i + 1, j + 1].Index) : null;

            // The order of the array is not relevant.
            GraphEdge[] neighborEdges = { edge00, edge01, edge02, edge10, edge13, edge20, edge21, edge22 };

            IEnumerable<GraphEdge> finalNeighborEdges = from e in neighborEdges
                                                        where e != null
                                                        select e;

            return finalNeighborEdges;
        }
    }
}