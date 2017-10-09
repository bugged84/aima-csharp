using aima.core.search.framework.problem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Action = aima.core.agent.Action;

namespace aima.core.search.framework.qsearch
{
    /// <summary>
    ///     @author Richard Waliser
    /// </summary>
    public class DepthLimitedSearch : GraphSearch
    {
        private class DepthLimitedNodeExpander : NodeExpander
        {
            private uint m_depthLimit;

            public DepthLimitedNodeExpander(uint depth)
            {
                m_depthLimit = depth;
            }

            public override List<Node> expand(Node node, Problem problem)
            {
                var currentDepth = node.getPathFromRoot().Count - 1;

                // don't expand if the depth limit has been reached
                if (currentDepth < m_depthLimit)
                {
                    var successors = base.expand(node, problem);
                    successors.Reverse(); // ensure left-most successor is last in (i.e. first out)

                    return successors;
                }

                return new List<Node>();
            }
        }

        private Action<IEnumerable<object>> m_openListTrace;
        private Action<IEnumerable<object>> m_closedListTrace;

        private uint DepthLimit { get; }

        public DepthLimitedSearch(uint depthLimit = DefaultDepthLimit, Action<IEnumerable<object>> openListTrace = null, Action<IEnumerable<object>> closedListTrace = null)
            : base(new DepthLimitedNodeExpander(depthLimit))
        {
            DepthLimit = depthLimit;
            m_openListTrace = openListTrace;
            m_closedListTrace = closedListTrace;
        }

        public override List<Action> Search(Problem problem)
        {
            var frontier = new LIFOQueue<Node>();

            return base.search(problem, frontier);
        }

        protected override void OnNodeExpanded()
        {
            base.OnNodeExpanded();

            m_openListTrace?.Invoke(frontier.Select(x => x.getState()));
            m_closedListTrace?.Invoke(explored.Select(x => x));
        }

        public override string ToString()
        {
            return $"{nameof(DepthLimitedSearch)}, Limit: {DepthLimit}{(DepthLimit == DefaultDepthLimit ? " (Default)" : string.Empty)}";
        }

        public const uint DefaultDepthLimit = 50;
    }
}
