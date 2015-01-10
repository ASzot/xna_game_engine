#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

namespace BaseLogic.Player.AI.Graph
{
    /// <summary>
    ///
    /// </summary>
    public class GraphEdge
    {
        /// <summary>
        /// The f_cost
        /// </summary>
        protected float f_cost;

        /// <summary>
        /// The i_from
        /// </summary>
        protected int i_from;

        /// <summary>
        /// The i_to
        /// </summary>
        protected int i_to;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphEdge"/> class.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="cost">The cost.</param>
        public GraphEdge(int from, int to, float cost)
        {
            i_from = from;
            i_to = to;
            f_cost = cost;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphEdge"/> class.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public GraphEdge(int from, int to)
            : this(from, to, 1.0f)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphEdge"/> class.
        /// </summary>
        public GraphEdge()
            : this(GraphNode.INVALID_ID, GraphNode.INVALID_ID, 1.0f)
        {
        }

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        public float Cost
        {
            get { return f_cost; }
            set { f_cost = value; }
        }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public int From
        {
            get { return i_from; }
            set { i_from = value; }
        }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        public int To
        {
            get { return i_to; }
            set { i_to = value; }
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(GraphEdge e1, GraphEdge e2)
        {
            return !(e1 == e2);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(GraphEdge e1, GraphEdge e2)
        {
            if ((object)e1 == null && (object)e2 == null)
                return true;
            else if ((object)e1 == null || (object)e2 == null)
                return false;

            return (e1.To == e2.To && e1.From == e2.From);
        }

        /// <summary>
        /// Calculates the dist sq cost.
        /// </summary>
        /// <param name="graph">The graph.</param>
        public void CalculateDistSqCost(SparseGraph graph)
        {
            GraphNode n1 = graph.GetNode(From);
            GraphNode n2 = graph.GetNode(To);

            Microsoft.Xna.Framework.Vector3 start = n1.Position;
            Microsoft.Xna.Framework.Vector3 end = n2.Position;

            f_cost = Microsoft.Xna.Framework.Vector3.DistanceSquared(start, end);
        }

        /// <summary>
        /// Gets the edge save data.
        /// </summary>
        /// <returns></returns>
        public EdgeSaveData GetEdgeSaveData()
        {
            EdgeSaveData edgeSaveData;
            edgeSaveData.From = i_from;
            edgeSaveData.To = i_to;
            edgeSaveData.Cost = f_cost;

            return edgeSaveData;
        }

        /// <summary>
        /// Sets the edge load data.
        /// </summary>
        /// <param name="esd">The esd.</param>
        public void SetEdgeLoadData(EdgeSaveData esd)
        {
            From = esd.From;
            To = esd.To;
            Cost = esd.Cost;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return i_from.ToString() + " - " + i_to.ToString();
        }
    }
}