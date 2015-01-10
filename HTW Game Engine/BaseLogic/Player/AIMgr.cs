#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using Microsoft.Xna.Framework;

namespace BaseLogic.Player
{
    /// <summary>
    ///
    /// </summary>
    public interface AIMgr
    {
        /// <summary>
        /// Creates the graph editor.
        /// </summary>
        /// <param name="gameSystem">The game system.</param>
        void CreateGraphEditor(GameSystem gameSystem);

        /// <summary>
        /// Gets the ai save data.
        /// </summary>
        /// <returns></returns>
        AISaveData GetAISaveData();

        /// <summary>
        /// Sets the ai load data.
        /// </summary>
        /// <param name="saveData">The save data.</param>
        void SetAILoadData(AISaveData saveData);
    }

    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public struct AISaveData
    {
        /// <summary>
        /// The graphs
        /// </summary>
        public GraphSaveData[] Graphs;
    }

    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public struct EdgeSaveData
    {
        /// <summary>
        /// The cost
        /// </summary>
        public float Cost;

        /// <summary>
        /// From
        /// </summary>
        public int From;

        /// <summary>
        /// To
        /// </summary>
        public int To;
    }

    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public struct GraphSaveData
    {
        /// <summary>
        /// The digraph
        /// </summary>
        public bool Digraph;

        /// <summary>
        /// The edge save data
        /// </summary>
        public EdgeSaveData[] EdgeSaveData;

        /// <summary>
        /// The next node index
        /// </summary>
        public int NextNodeIndex;

        /// <summary>
        /// The node save data
        /// </summary>
        public NodeSaveData[] NodeSaveData;
    }

    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public struct NodeSaveData
    {
        /// <summary>
        /// The index
        /// </summary>
        public int Index;

        /// <summary>
        /// The position
        /// </summary>
        public Vector3 Position;
    }
}