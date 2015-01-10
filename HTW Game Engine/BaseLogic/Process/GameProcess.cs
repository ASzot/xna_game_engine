#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

namespace BaseLogic.Process
{
    /// <summary>
    ///
    /// </summary>
    public enum ProcessType
    {
        /// <summary>
        /// The proc type_ none
        /// </summary>
        ProcType_None,

        /// <summary>
        /// The proc type_ sound
        /// </summary>
        ProcType_Sound,

        /// <summary>
        /// The proc type_ graphics
        /// </summary>
        ProcType_Graphics,

        /// <summary>
        /// The proc type_ wait
        /// </summary>
        ProcType_Wait,

        /// <summary>
        /// The proc type_ object
        /// </summary>
        ProcType_Object,
    };

    /// <summary>
    ///
    /// </summary>
    public class GameProcess
    {
        /// <summary>
        /// The b_init update
        /// </summary>
        protected bool b_initUpdate = true;

        /// <summary>
        /// The b_serilize
        /// </summary>
        protected bool b_serilize = true;

        /// <summary>
        /// The _next process
        /// </summary>
        private GameProcess _nextProcess = null;

        /// <summary>
        /// The _type
        /// </summary>
        private ProcessType _type;

        /// <summary>
        /// The b_kill
        /// </summary>
        private bool b_kill = false;

        /// <summary>
        /// The b_paused
        /// </summary>
        private bool b_paused = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameProcess"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public GameProcess(ProcessType type)
        {
            _type = type;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialized
        {
            get { return !b_initUpdate; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GameProcess"/> is kill.
        /// </summary>
        /// <value>
        ///   <c>true</c> if kill; otherwise, <c>false</c>.
        /// </value>
        public bool Kill
        {
            get { return b_kill; }
            set { b_kill = value; }
        }

        /// <summary>
        /// Gets the next.
        /// </summary>
        /// <value>
        /// The next.
        /// </value>
        public GameProcess Next
        {
            get { return _nextProcess; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GameProcess"/> is paused.
        /// </summary>
        /// <value>
        ///   <c>true</c> if paused; otherwise, <c>false</c>.
        /// </value>
        public bool Paused
        {
            get { return b_paused; }
            set { b_paused = value; }
        }

        /// <summary>
        /// Gets the type of the proc.
        /// </summary>
        /// <value>
        /// The type of the proc.
        /// </value>
        public ProcessType ProcType
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GameProcess"/> is serilize.
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
        /// Clears all children.
        /// </summary>
        public void ClearAllChildren()
        {
            this._nextProcess = null;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public virtual GameProcess Clone()
        {
            return null;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Called when [kill].
        /// </summary>
        public virtual void OnKill()
        {
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public virtual void Reset()
        {
            b_initUpdate = true;
        }

        /// <summary>
        /// Sets the next process.
        /// </summary>
        /// <param name="gameProcess">The game process.</param>
        public void SetNextProcess(GameProcess gameProcess)
        {
            _nextProcess = gameProcess;
        }

        /// <summary>
        /// Toggles the paused.
        /// </summary>
        public virtual void TogglePaused()
        {
            b_paused = !b_paused;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (b_initUpdate)
            {
                Initialize();
                b_initUpdate = false;
            }
        }
    }
}