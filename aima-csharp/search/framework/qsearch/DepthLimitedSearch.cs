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
                var currentDepth = node.getPathFromRoot().Count;

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

        private uint DepthLimit { get; }

        public DepthLimitedSearch(uint depthLimit = DefaultDepthLimit) : base(new DepthLimitedNodeExpander(depthLimit))
        {
            DepthLimit = depthLimit;
        }

        public override List<Action> Search(Problem problem)
        {
            var frontier = new LIFOQueue<Node>();

            return base.search(problem, frontier);
        }

        public override string ToString()
        {
            return $"{nameof(DepthLimitedSearch)}, Limit: {DepthLimit}{(DepthLimit == DefaultDepthLimit ? " (Default)" : string.Empty)}";
        }

        public const uint DefaultDepthLimit = 50;
    }
}
