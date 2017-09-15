using aima.core.search.framework;
using aima.core.search.framework.problem;
using aima.core.search.framework.qsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using Action = aima.core.agent.Action;

namespace aima.core.search.framework.qsearch
{
    /// <summary>
    ///     @author Richard Waliser
    /// </summary>
    public class DepthFirstSearch : GraphSearch
    {
        private class DepthFirstNodeExpander : NodeExpander
        {
            public override List<Node> expand(Node node, Problem problem)
            {
                var successors = base.expand(node, problem);
                successors.Reverse(); // ensure left-most successor is last in (i.e. first out)

                return successors;
            }
        }

        public DepthFirstSearch() : base(new DepthFirstNodeExpander())
        {
        }

        public override List<Action> Search(Problem problem)
        {
            var frontier = new LIFOQueue<Node>();

            return base.search(problem, frontier);
        }

        public override string ToString()
        {
            return nameof(DepthFirstSearch);
        }
    }
}
