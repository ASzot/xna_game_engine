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
    public abstract class SearchAlgorithim
    {
        /// <summary>
        /// A node which has no parent (root node).
        /// </summary>
        protected const int NO_PARENT = -3;

        /// <summary>
        /// A node which has not been visited.
        /// </summary>
        protected const int UNVISITED = -2;

        /// <summary>
        /// A node which has been visited.
        /// </summary>
        protected const int VISITED = 0;

        /// <summary>
        /// Constructs the path.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        public virtual void ConstructPath(SparseGraph graph, int source, int target)
        {
        }

        /// <summary>
        /// Gets the path to target.
        /// </summary>
        /// <returns></returns>
        public virtual List<int> GetPathToTarget()
        {
            return null;
        }
    }
}