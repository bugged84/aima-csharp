using System;
using System.Collections.Generic;
using System.Linq;

namespace aima.core.search.framework.qsearch
{
    public interface IQueue<T>
    {
        /// <summary>
        ///     The number of items in the queue.
        /// </summary>
        int Count { get; }

        /// <summary>
        ///     Remove an item from the queue.
        /// </summary>
        T Dequeue();

        /// <summary>
        ///     Add an item to the queue.
        /// </summary>
        void Enqueue(T item);

        /// <summary>
        ///     Returns the next item to be dequeued (without removing it from the queue).
        /// </summary>
        T Peek();
    }
}
