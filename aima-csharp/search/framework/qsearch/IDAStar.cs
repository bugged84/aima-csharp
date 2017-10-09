using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aima.core.search.framework.problem;
using Action = aima.core.agent.Action;
using aima.core.search.framework.qsearch;
using aima.core.search.framework;

namespace aima_csharp.search.framework.qsearch
{
    public class IDAStar : GraphSearch
    {
        private HeuristicFunction m_heuristic;
        private Action<IEnumerable<object>> m_openListTrace;
        private Action<IEnumerable<object>> m_closedListTrace;
        private Action<int> m_depthListTrace;
        private double m_limit;
        private double m_nextLimt = double.MaxValue;

        public IDAStar(HeuristicFunction heuristic, Action<int> depthListTrace = null, Action<IEnumerable<object>> openListTrace = null, Action<IEnumerable<object>> closedListTrace = null)
        {
            m_heuristic = heuristic;
            m_depthListTrace = depthListTrace;
            m_openListTrace = openListTrace;
            m_closedListTrace = closedListTrace;
        }

        public override List<Action> Search(Problem problem)
        {
            for (m_limit = m_heuristic.h(problem.getInitialState()); ;)
            {
                m_depthListTrace?.Invoke((int)m_limit);
                var solution = base.search(problem, new LIFOQueue<Node>());

                if (solution.Count > 0)
                {
                    return solution;
                }

                m_limit = m_nextLimt;
                m_nextLimt = double.MaxValue;
            }
        }

        protected override void OnNodeExpanded()
        {
            base.OnNodeExpanded();

            m_openListTrace?.Invoke(frontier.Select(x => x.getState()));
            m_closedListTrace?.Invoke(explored.Select(x => x));
        }

        protected override void addToFrontier(Node node)
        {
            var g = node.getPathCost();
            var h = m_heuristic.h(node.getState());
            var f = g + h;

            if (f > m_limit)
            {
                m_nextLimt = Math.Min(m_nextLimt, f);
            }
            else
            {
                base.addToFrontier(node);
            }
        }

        public override string ToString()
        {
            return nameof(IDAStar);
        }
    }
}
