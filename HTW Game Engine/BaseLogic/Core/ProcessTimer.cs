#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using Microsoft.Xna.Framework;

namespace BaseLogic.Core
{
    /// <summary>
    ///
    /// </summary>
    internal class ProcessTimer
    {
        /// <summary>
        /// The ui_current milliseconds
        /// </summary>
        private uint ui_currentMilliseconds = 0;

        /// <summary>
        /// The ui_total milliseconds
        /// </summary>
        private uint ui_totalMilliseconds = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessTimer"/> class.
        /// </summary>
        /// <param name="milliseconds">The milliseconds.</param>
        public ProcessTimer(uint milliseconds)
        {
            ui_totalMilliseconds = milliseconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessTimer"/> class.
        /// </summary>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <param name="startOffset">The start offset.</param>
        public ProcessTimer(uint milliseconds, uint startOffset)
            : this(milliseconds)
        {
            ui_currentMilliseconds = startOffset;
        }

        /// <summary>
        /// Ticks the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <returns></returns>
        public bool Tick(GameTime gameTime)
        {
            ui_currentMilliseconds += (uint)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (ui_totalMilliseconds <= ui_currentMilliseconds)
                return true;
            else
                return false;
        }
    }
}