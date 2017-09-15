using aima.core.search.framework.problem;
using System;
using System.Collections.Generic;
using System.Linq;
using Action = aima.core.agent.Action;

namespace aima.core.search.framework.qsearch
{
    /// <summary>
    ///     Like A/A* but h(n)=0
    ///     <para/>
    ///     @author Richard Waliser
    /// </summary>
    public class BranchAndBoundSearch : GraphSearch
    {
        private PriorityQueue<Node> m_frontier;

        public override List<Action> Search(Problem problem)
        {
            // Figure 3.14 (R&N): "a priority queue ordered by PATH-COST" i.e. h(n)=0
            m_frontier = new PriorityQueue<Node>(node => node.getPathCost());

            return base.search(problem, m_frontier);
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
            return nameof(BranchAndBoundSearch);
        }
    }
}
