using System;
using System.Collections.Generic;

namespace aima.core.search.framework.qsearch
{
    public class FIFOQueue<T> : IQueue<T>
    {
        private Queue<T> m_queue = new Queue<T>();

        public int Count => m_queue.Count;
        public T Dequeue() => m_queue.Dequeue();
        public void Enqueue(T item) => m_queue.Enqueue(item);
        public T Peek() => m_queue.Peek();
    }
}
