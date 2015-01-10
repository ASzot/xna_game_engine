using System;
using System.Collections.Generic;

namespace BaseLogic.Player.AI.Graph
{
    /// <summary>
    ///
    /// </summary>
    public class IndexedPriorityQLow
    {
        /// <summary>
        /// The _heap
        /// </summary>
        private List<int> _heap;

        /// <summary>
        /// The _inv heap
        /// </summary>
        private List<int> _invHeap;

        /// <summary>
        /// The _vec keys
        /// </summary>
        private List<float> _vecKeys;

        /// <summary>
        /// The i_max size
        /// </summary>
        private int i_maxSize;

        /// <summary>
        /// The i_size
        /// </summary>
        private int i_size;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexedPriorityQLow"/> class.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <param name="maxSize">The maximum size.</param>
        public IndexedPriorityQLow(List<float> keys, int maxSize)
        {
            _vecKeys = keys;
            i_maxSize = maxSize;

            i_size = 0;

            int capacity = i_maxSize + 1;

            _heap = new List<int>(capacity);
            _invHeap = new List<int>(capacity);

            for (int i = 0; i < capacity; ++i)
            {
                _heap.Add(0);
                _invHeap.Add(0);
            }
        }

        /// <summary>
        /// Changes the priority.
        /// </summary>
        /// <param name="idx">The index.</param>
        public void ChangePriority(int idx)
        {
            ReorderUpwards(_invHeap[idx]);
        }

        /// <summary>
        /// Empties this instance.
        /// </summary>
        /// <returns></returns>
        public bool Empty()
        {
            return i_size == 0;
        }

        /// <summary>
        /// Inserts the specified index.
        /// </summary>
        /// <param name="idx">The index.</param>
        /// <exception cref="System.InvalidOperationException"></exception>
        public void Insert(int idx)
        {
            if (i_size + 1 > i_maxSize)
                throw new InvalidOperationException();

            ++i_size;

            _heap[i_size] = idx;
            _invHeap[idx] = i_size;

            ReorderUpwards(i_size);
        }

        /// <summary>
        /// Pops this instance.
        /// </summary>
        /// <returns></returns>
        public int Pop()
        {
            Swap(1, i_size);

            ReorderDownwards(1, i_size - 1);

            return _heap[i_size--];
        }

        /// <summary>
        /// Reorders the downwards.
        /// </summary>
        /// <param name="nd">The nd.</param>
        /// <param name="heapSize">Size of the heap.</param>
        private void ReorderDownwards(int nd, int heapSize)
        {
            while (2 * nd <= heapSize)
            {
                int child = 2 * nd;

                if ((child < heapSize) && (_vecKeys[_heap[child]] > _vecKeys[_heap[child + 1]]))
                {
                    ++child;
                }
                if (_vecKeys[_heap[nd]] > _vecKeys[_heap[child]])
                {
                    Swap(child, nd);
                    nd = child;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Reorders the upwards.
        /// </summary>
        /// <param name="nd">The nd.</param>
        private void ReorderUpwards(int nd)
        {
            while ((nd > 1) && (_vecKeys[_heap[nd / 2]] > _vecKeys[_heap[nd]]))
            {
                Swap(nd / 2, nd);
                nd /= 2;
            }
        }

        /// <summary>
        /// Swaps the specified a.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        private void Swap(int a, int b)
        {
            int tmp = _heap[a];
            _heap[a] = _heap[b];
            _heap[b] = tmp;

            _invHeap[_heap[a]] = a;
            _invHeap[_heap[b]] = b;
        }
    }
}