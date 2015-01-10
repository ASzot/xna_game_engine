#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

namespace BaseLogic.Core
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FrameType<T>
    {
        /// <summary>
        /// The _data
        /// </summary>
        private T _data;

        /// <summary>
        /// The _locked data
        /// </summary>
        private T _lockedData;

        /// <summary>
        /// The i_locked for
        /// </summary>
        private int i_lockedFor = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameType{T}"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public FrameType(T data)
        {
            _data = data;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public T Data
        {
            get
            {
                if (i_lockedFor != 0)
                    return _lockedData;
                else
                    return _data;
            }
            set
            {
                if (i_lockedFor == 0)
                    _data = value;
            }
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="FrameType{T}"/> to <see cref="T"/>.
        /// </summary>
        /// <param name="ft">The ft.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator T(FrameType<T> ft)
        {
            return ft.Data;
        }

        /// <summary>
        /// Locks the specified lock data.
        /// </summary>
        /// <param name="lockData">The lock data.</param>
        /// <param name="framesLocked">The frames locked.</param>
        public void Lock(T lockData, int framesLocked)
        {
            _lockedData = lockData;
            i_lockedFor = framesLocked;
        }

        /// <summary>
        /// Updates the frame count.
        /// </summary>
        public void UpdateFrameCount()
        {
            if (i_lockedFor > 0)
                i_lockedFor--;
        }
    }
}