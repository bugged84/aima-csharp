using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aima.core.search.framework.qsearch
{
    /// <summary>
    ///     @author Richard Waliser
    /// </summary>
    public class PriorityQueue<T> : IQueue<T>
    {
        private Func<T, double> m_priorityFunc;
        private SimplePriorityQueue<T, double> m_queue = new SimplePriorityQueue<T, double>();        

        public PriorityQueue(Func<T, double> priorityFunc)
        {
            m_priorityFunc = priorityFunc;
        }

        public bool Contains(T item) => m_queue.Contains(item);
        public int Count => m_queue.Count;
        public T Dequeue() => m_queue.Dequeue();
        public void Enqueue(T item) => m_queue.Enqueue(item, m_priorityFunc(item));
        public T Peek() => m_queue.First();

        public void Remove(T item) => m_queue.Remove(item);
        public bool TryGetPriority(T item, out double priority) => m_queue.TryGetPriority(item, out priority);
    }
}
