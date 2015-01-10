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
using Microsoft.Xna.Framework;

namespace Xna_Game_AI
{
    /// <summary>
    /// Plans a path through a graph.
    /// </summary>
    internal class PathPlanner
    {
        /// <summary>
        /// The _graph
        /// </summary>
        private SparseGraph _graph;

        /// <summary>
        /// The _player
        /// </summary>
        private GameAIPlayer _player;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathPlanner"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="graph">The graph.</param>
        public PathPlanner(GameAIPlayer player, SparseGraph graph)
        {
            _player = player;
            _graph = graph;
        }

        /// <summary>
        /// Creates the path to position.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetPos">The target position.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public bool CreatePathToPosition<T>(Vector3 targetPos, out List<Vector3> path) where T : SearchAlgorithim, new()
        {
            path = new List<Vector3>();

            //if (_player.PositionUnobstructed(targetPos))
            //{
            //    path.Add(targetPos);

            //    return true;
            //}

            GraphNode closestNodeToPlayer = _graph.GetNodeClosestToPosition(_player.Position);
            GraphNode closestNodeToTarget = _graph.GetNodeClosestToPosition(targetPos);
            if (closestNodeToPlayer == null || closestNodeToTarget == null)
                return false;

            T search = new T();
            search.ConstructPath(_graph, closestNodeToPlayer.Index, closestNodeToTarget.Index);

            List<int> indiciesPath = search.GetPathToTarget();

            if (indiciesPath == null || indiciesPath.Count == 0)
                return false;

            // Convert the indicies over to the positions.
            var positions = from ind in indiciesPath
                            select _graph.GetNode(ind).Position;

            path.AddRange(positions);
            // Also the path from the last node to the actual position.
            path.Add(targetPos);

            return true;
        }

        /// <summary>
        /// Creates the path to position.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodeTarget">The node target.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public bool CreatePathToPosition<T>(int nodeTarget, out List<Vector3> path) where T : SearchAlgorithim, new()
        {
            path = new List<Vector3>();

            GraphNode closestNodeToPlayer = _graph.GetNodeClosestToPosition(_player.Position);
            GraphNode closestNodeToTarget = _graph.GetNode(nodeTarget);

            if (closestNodeToPlayer == null || closestNodeToTarget == null)
                return false;

            T search = new T();
            search.ConstructPath(_graph, closestNodeToPlayer.Index, closestNodeToTarget.Index);

            List<int> indiciesPath = search.GetPathToTarget();
            if (indiciesPath == null)
                return false;

            if (indiciesPath.Count == 0)
                return false;

            // Convert the indicies over to the positions.
            var positions = from ind in indiciesPath
                            select _graph.GetNode(ind).Position;

            path.AddRange(positions);

            return true;
        }
    }
}