#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RenderingSystem
{
    /// <summary>
    /// Any form which needs to update on a regular basis to display updated information.
    /// </summary>
    public interface UpdatingForm
    {
        void UpdateUI();
		bool IsFormClosed { get; }
    }

	/// <summary>
	/// Base functionality for anything managing a list of objects which need to update and be modified in a moderated process.
	/// </summary>
	/// <typeparam name="T">The type of object to manage.</typeparam>
    public class MgrTemplate<T>
    {
        protected List<T> _dataElements = new List<T>();
        protected bool b_update = true;

		/// <summary>
		/// Whether the objects should be updated on the update function.
		/// </summary>
        public bool UpdateMgr
        {
            get { return b_update; }
            set { b_update = value; }
        }

		/// <summary>
		/// Add a new data element to the internal list.
		/// </summary>
		/// <param name="data"></param>
        public virtual void AddToList(T data)
        {
            _dataElements.Add(data);
        }

		/// <summary>
		/// Add a range of data
		/// </summary>
		/// <param name="datas"></param>
        public void AddRange(T[] datas)
        {
            _dataElements.AddRange(datas);
        }

        public void ClearDataElements()
        {
            _dataElements.Clear();
        }

        public int GetNumberOfDataElements()
        {
            return _dataElements.Count;
        }

        public void InsertElement(T data, int index)
        {
            _dataElements.Insert(index, data);
        }

        public virtual void RemoveDataElement(int i)
        {
            if (i >= _dataElements.Count)
                throw new ArgumentOutOfRangeException();

            _dataElements.RemoveAt(i);
        }

        public virtual void RemoveDataElement(T data)
        {
            _dataElements.Remove(data);
        }

        public T GetDataElementAt(int i)
        {
            return _dataElements.ElementAt(i);
        }

        public IEnumerable<T> GetDataElements()
        {
            return _dataElements;
        }
    }
}
