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

namespace BaseLogic.Player
{
    /// <summary>
    ///
    /// </summary>
    public struct AIMemorySlot
    {
        /// <summary>
        /// The damages taken
        /// </summary>
        public List<float> DamagesTaken;

        /// <summary>
        /// The players in sight i ds
        /// </summary>
        public List<string> PlayersInSightIDs;

        /// <summary>
        /// The victim identifier
        /// </summary>
        public string VictimID;
    }

    /// <summary>
    ///
    /// </summary>
    public class AIMemory
    {
        /// <summary>
        /// The ma x_ tim e_ seconds
        /// </summary>
        private const double MAX_TIME_SECONDS = 5.0;

        /// <summary>
        /// The _current memory
        /// </summary>
        private AIMemorySlot _currentMemory;

        /// <summary>
        /// The _current time
        /// </summary>
        private TimeSpan _currentTime;

        /// <summary>
        /// The _max time
        /// </summary>
        private TimeSpan _maxTime;

        /// <summary>
        /// The _memories
        /// </summary>
        private List<AIMemorySlot> _memories = new List<AIMemorySlot>();

        /// <summary>
        /// The f_total damage
        /// </summary>
        private float f_totalDamage = 0f;

        /// <summary>
        /// The i_max memory count
        /// </summary>
        private int i_maxMemCount = 60;

        /// <summary>
        /// Initializes a new instance of the <see cref="AIMemory"/> class.
        /// </summary>
        public AIMemory()
        {
            _maxTime = TimeSpan.FromSeconds(MAX_TIME_SECONDS);
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="AIMemory"/> is attacking.
        /// </summary>
        /// <value>
        ///   <c>true</c> if attacking; otherwise, <c>false</c>.
        /// </value>
        public bool Attacking
        {
            get
            {
                for (int i = _memories.Count - 1; i >= 0; --i)
                {
                    AIMemorySlot aims = _memories.ElementAt(i);
                    if (aims.VictimID != null && aims.VictimID != "")
                        return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Sets the maximum time.
        /// </summary>
        /// <value>
        /// The maximum time.
        /// </value>
        public TimeSpan MaxTime
        {
            set { _maxTime = value; }
        }

        /// <summary>
        /// Gets the most recent victim.
        /// </summary>
        /// <value>
        /// The most recent victim.
        /// </value>
        public GamePlayer MostRecentVictim
        {
            get
            {
                string victimID = null;
                for (int i = _memories.Count - 1; i >= 0; --i)
                {
                    AIMemorySlot aims = _memories.ElementAt(i);
                    if (aims.VictimID != null && aims.VictimID != "")
                        victimID = aims.VictimID;
                }

                if (victimID == null)
                    return null;

                return GamePlayer.p_PlayerMgr.GetPlayerOfId(victimID);
            }
        }

        /// <summary>
        /// Forgets the total damage.
        /// </summary>
        public void ForgetTotalDamage()
        {
            f_totalDamage = 0f;
        }

        /// <summary>
        /// Determines whether [has damage been taken].
        /// </summary>
        /// <returns></returns>
        public bool HasDamageBeenTaken()
        {
            if (_currentMemory.DamagesTaken.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Remembers the damage taken.
        /// </summary>
        /// <param name="damageInflicted">The damage inflicted.</param>
        public void RememberDamageTaken(float damageInflicted)
        {
            _currentMemory.DamagesTaken.Add(damageInflicted);
            f_totalDamage += damageInflicted;
        }

        /// <summary>
        /// Remembers the players in sight.
        /// </summary>
        /// <param name="ids">The ids.</param>
        public void RememberPlayersInSight(IEnumerable<string> ids)
        {
            _currentMemory.PlayersInSightIDs.AddRange(ids);
        }

        /// <summary>
        /// Remembers the players in sight.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void RememberPlayersInSight(string id)
        {
            _currentMemory.PlayersInSightIDs.Add(id);
        }

        /// <summary>
        /// Remembers the victim identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void RememberVictimID(string id)
        {
            _currentMemory.VictimID = id;
        }

        /// <summary>
        /// Summates the damages taken.
        /// </summary>
        /// <returns></returns>
        public float SummateDamagesTaken()
        {
            return f_totalDamage;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _currentTime += gameTime.ElapsedGameTime;
            if (_currentTime >= _maxTime)
            {
                _currentTime = TimeSpan.Zero;
                f_totalDamage = 0f;
            }

            if (_memories.Count > i_maxMemCount)
            {
                _memories.RemoveAt(0);
            }

            _memories.Add(_currentMemory);

            _currentMemory = new AIMemorySlot();
            _currentMemory.DamagesTaken = new List<float>();
            _currentMemory.PlayersInSightIDs = new List<string>();
        }
    }
}