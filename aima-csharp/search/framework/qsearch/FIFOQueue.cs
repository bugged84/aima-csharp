using System;
using System.Collections;
using System.Collections.Generic;

namespace aima.core.search.framework.qsearch
{
    /// <summary>
    ///     @author Richard Waliser
    /// </summary>
    public class FIFOQueue<T> : IQueue<T>
    {
        private Queue<T> m_queue = new Queue<T>();

        public bool Contains(T item) => m_queue.Contains(item);
        public int Count => m_queue.Count;
        public T Dequeue() => m_queue.Dequeue();
        public void Enqueue(T item) => m_queue.Enqueue(item);
        public T Peek() => m_queue.Peek();

        public IEnumerator<T> GetEnumerator() => m_queue.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => m_queue.GetEnumerator();
    }
}
