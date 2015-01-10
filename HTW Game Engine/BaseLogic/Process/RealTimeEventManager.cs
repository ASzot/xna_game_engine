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

namespace BaseLogic.Process
{
    /// <summary>
    ///
    /// </summary>
    public interface RealTimeEventTrigger
    {
        /// <summary>
        /// Adds the real time event.
        /// </summary>
        /// <param name="gameProc">The game proc.</param>
        /// <param name="chainName">Name of the chain.</param>
        void AddRealTimeEvent(GameProcess gameProc, string chainName);

        /// <summary>
        /// Gets all game proccesses.
        /// </summary>
        /// <param name="chainName">Name of the chain.</param>
        /// <returns></returns>
        List<GameProcess> GetAllGameProccesses(string chainName);

        /// <summary>
        /// Gets all real time event chain names.
        /// </summary>
        /// <returns></returns>
        string[] GetAllRealTimeEventChainNames();

        /// <summary>
        /// Removes the real time event.
        /// </summary>
        /// <param name="chainName">Name of the chain.</param>
        /// <param name="gameProc">The game proc.</param>
        void RemoveRealTimeEvent(string chainName, GameProcess gameProc);

        /// <summary>
        /// Removes the real time event.
        /// </summary>
        /// <param name="chainName">Name of the chain.</param>
        /// <param name="index">The index.</param>
        void RemoveRealTimeEvent(string chainName, int index);
    }

    /// <summary>
    ///
    /// </summary>
    public class RealTimeEventMgr
    {
        /// <summary>
        /// The _real time events
        /// </summary>
        private Dictionary<string, List<GameProcess>> _realTimeEvents = new Dictionary<string, List<GameProcess>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RealTimeEventMgr"/> class.
        /// </summary>
        public RealTimeEventMgr()
        {
        }

        /// <summary>
        /// Adds the proccess to chain.
        /// </summary>
        /// <param name="proc">The proc.</param>
        /// <param name="chainID">The chain identifier.</param>
        public void AddProccessToChain(GameProcess proc, string chainID)
        {
            if (_realTimeEvents.ContainsKey(chainID))
            {
                _realTimeEvents[chainID].Add(proc);
            }
            else
            {
                _realTimeEvents[chainID] = new List<GameProcess>();
                _realTimeEvents[chainID].Add(proc);
            }
        }

        /// <summary>
        /// Declares the chain names.
        /// </summary>
        /// <param name="names">The names.</param>
        public void DeclareChainNames(params string[] names)
        {
            foreach (string name in names)
            {
                _realTimeEvents[name] = new List<GameProcess>();
            }
        }

        /// <summary>
        /// Executes the process chain.
        /// </summary>
        /// <param name="chainID">The chain identifier.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void ExecuteProcessChain(string chainID)
        {
            if (!_realTimeEvents.ContainsKey(chainID))
                throw new ArgumentException();

            foreach (GameProcess gameProc in _realTimeEvents[chainID])
            {
                //OPTIMIZE:
                // We have a process resource which is initialized although is just used as the
                // "clone" object.
                GameSystem.GameSys_Instance.AddGameProcess(gameProc.Clone());
            }
        }

        /// <summary>
        /// Gets all chain names.
        /// </summary>
        /// <returns></returns>
        public string[] GetAllChainNames()
        {
            var dictKeys = from kv in _realTimeEvents
                           select kv.Key;
            return dictKeys.ToArray();
        }

        /// <summary>
        /// Gets all processes.
        /// </summary>
        /// <param name="chainName">Name of the chain.</param>
        /// <returns></returns>
        public List<GameProcess> GetAllProcesses(string chainName)
        {
            return _realTimeEvents[chainName];
        }

        /// <summary>
        /// Gets all processes.
        /// </summary>
        /// <returns></returns>
        public List<GameProcess> GetAllProcesses()
        {
            var dictVals = from kv in _realTimeEvents
                           select kv.Value;
            List<GameProcess> procs = new List<GameProcess>();
            // Combine all of the lists.
            foreach (List<GameProcess> chainProcs in dictVals)
            {
                foreach (GameProcess chainProc in chainProcs)
                    procs.Add(chainProc);
            }

            return procs;
        }
    }
}