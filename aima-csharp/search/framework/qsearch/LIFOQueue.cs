﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace aima.core.search.framework.qsearch
{
    public class LIFOQueue<T> : IQueue<T>
    {
        private Stack<T> m_stack = new Stack<T>();

        public int Count => m_stack.Count;
        public T Dequeue() => m_stack.Pop();
        public void Enqueue(T item) => m_stack.Push(item);
        public T Peek() => m_stack.Peek();
    }
}
