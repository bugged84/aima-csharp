using aima.core.search.framework.problem;
using System;
using System.Collections.Generic;
using System.Linq;
using Action = aima.core.agent.Action;

namespace aima.core.search.framework.qsearch
{
    /// <summary>
    ///     @author Richard Waliser
    /// </summary>
    public class AAStar : GraphSearch
    {
        private PriorityQueue<Node> m_frontier;
        private HeuristicFunction m_heuristic;
        private Action<IEnumerable<object>> m_openListTrace;
        private Action<IEnumerable<object>> m_closedListTrace;

        public AAStar(HeuristicFunction heuristic, Action<IEnumerable<object>> openListTrace = null, Action<IEnumerable<object>> closedListTrace = null)
        {
            m_heuristic = heuristic;
            m_openListTrace= openListTrace;
            m_closedListTrace = closedListTrace;
        }

        public override List<Action> Search(Problem problem)
        {
            // a priority queue ordered by f(n)
            m_frontier = new PriorityQueue<Node>(node => node.getPathCost() + m_heuristic.h(node.getState()));

            return base.search(problem, m_frontier);
        }

        protected override void OnNodeExpanded()
        {
            base.OnNodeExpanded();

            m_openListTrace?.Invoke(frontier.Select(x => x.getState()));
            m_closedListTrace?.Invoke(explored.Select(x => x));
        }

        protected override void addToFrontier(Node node)
        {
            // Figure 3.14 (R&N): "if... is in frontier with higher PATH-COST then replace that frontier node..."
            double currentPathCost;
            if (m_frontier.TryGetPriority(node, out currentPathCost))
            {
                if (currentPathCost > node.getPathCost())
                {
                    m_frontier.Remove(node);
                    m_frontier.Enqueue(node);
                }
            }
            else
            {
                base.addToFrontier(node);
            }
        }

        public override string ToString()
        {
            return nameof(AAStar);
        }
    }
}
