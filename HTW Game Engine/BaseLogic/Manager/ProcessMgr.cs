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

using BaseLogic.Process;

namespace BaseLogic.Manager
{
    /// <summary>
    /// Manages all of the processes in the game.
    /// </summary>
    public class ProcessMgr
    {
        /// <summary>
        /// All the proccesses.
        /// </summary>
        private List<GameProcess> _gameProcesses = new List<GameProcess>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessMgr"/> class.
        /// </summary>
        public ProcessMgr()
        {
        }

        /// <summary>
        /// Gets the game processes.
        /// </summary>
        /// <value>
        /// The game processes.
        /// </value>
        public List<GameProcess> GameProcesses
        {
            get { return _gameProcesses; }
        }

        /// <summary>
        /// Adds the process.
        /// </summary>
        /// <param name="processToAdd">The process to add.</param>
        public void AddProcess(GameProcess processToAdd)
        {
            _gameProcesses.Add(processToAdd);
        }

        /// <summary>
        /// Gets the type of the processes of.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public IEnumerable<GameProcess> GetProcessesOfType(Type type)
        {
            var procsOfType = from gp in _gameProcesses
                              where gp.GetType() == type
                              select gp;
            return procsOfType;
        }

        /// <summary>
        /// Gets the real time trigger events.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RealTimeEventTrigger> GetRealTimeTriggerEvents()
        {
            var rteTriggers = from gp in _gameProcesses
                              where gp is RealTimeEventTrigger
                              select gp as RealTimeEventTrigger;
            return rteTriggers;
        }

        /// <summary>
        /// Updates the proccesses.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void UpdateProccesses(Microsoft.Xna.Framework.GameTime gameTime)
        {
            for (int i = 0; i < _gameProcesses.Count; ++i)
            {
                GameProcess curProc = _gameProcesses.ElementAt(i);
                if (curProc.Kill)
                {
                    curProc.OnKill();
                    _gameProcesses.Remove(curProc);
                    if (curProc.Next != null)
                    {
                        GameProcess nextProc = curProc.Next;
                        _gameProcesses.Insert(i, nextProc);
                    }
                    continue;
                }

                if (!curProc.Paused)
                    curProc.Update(gameTime);
            }
        }
    }
}