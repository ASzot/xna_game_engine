#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using Microsoft.Xna.Framework;

namespace BaseLogic.Player.AI.Graph
{
    /// <summary>
    ///
    /// </summary>
    public class GraphNode
    {
        /// <summary>
        /// The invali d_ identifier
        /// </summary>
        public const int INVALID_ID = -1;

        /// <summary>
        /// The i_index
        /// </summary>
        protected int i_index;

        /// <summary>
        /// The v_pos
        /// </summary>
        protected Vector3 v_pos = Vector3.Zero;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphNode"/> class.
        /// </summary>
        public GraphNode()
        {
            i_index = INVALID_ID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphNode"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="pos">The position.</param>
        public GraphNode(int index, Vector3 pos)
        {
            i_index = index;
            v_pos = pos;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphNode"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        public GraphNode(int index)
        {
            i_index = index;
        }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public int Index
        {
            get { return i_index; }
            set { i_index = value; }
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Vector3 Position
        {
            get { return v_pos; }
            set { v_pos = value; }
        }

        /// <summary>
        /// Gets the node save data.
        /// </summary>
        /// <returns></returns>
        public NodeSaveData GetNodeSaveData()
        {
            NodeSaveData nodeSaveData;
            nodeSaveData.Index = i_index;
            nodeSaveData.Position = v_pos;

            return nodeSaveData;
        }

        /// <summary>
        /// Sets the node load data.
        /// </summary>
        /// <param name="nsd">The NSD.</param>
        public void SetNodeLoadData(NodeSaveData nsd)
        {
            Index = nsd.Index;
            Position = nsd.Position;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return i_index.ToString() + ": " + v_pos.ToString();
        }
    }
}