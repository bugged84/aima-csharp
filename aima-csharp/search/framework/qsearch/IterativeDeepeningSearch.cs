using aima.core.search.framework.qsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aima.core.search.framework.problem;
using Action = aima.core.agent.Action;

namespace aima_csharp.search.framework.qsearch
{
    public class IterativeDeepeningSearch : GraphSearch
    {
        private Action<IEnumerable<object>> m_openListTrace;
        private Action<IEnumerable<object>> m_closedListTrace;
        private Action<int> m_depthListTrace;

        public IterativeDeepeningSearch(Action<int> depthListTrace = null, Action<IEnumerable<object>> openListTrace = null, Action<IEnumerable<object>> closedListTrace = null)
        {
            m_depthListTrace = depthListTrace;
            m_openListTrace = openListTrace;
            m_closedListTrace = closedListTrace;
        }

        public override List<Action> Search(Problem problem)
        {
            for (uint d = 0; ; d++)
            {
                m_depthListTrace?.Invoke((int)d);
                var dls = new DepthLimitedSearch(d, m_openListTrace, m_closedListTrace);
                var solution = dls.Search(problem);

                if (solution.Count > 0)
                {
                    return solution;
                }
            }
        }

        public override string ToString()
        {
            return nameof(IterativeDeepeningSearch);
        }
    }
}
