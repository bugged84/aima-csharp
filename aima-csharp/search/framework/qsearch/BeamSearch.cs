using System.Collections.Generic;
using aima.core.agent;
using aima.core.search.framework.problem;
using System;
using Action = aima.core.agent.Action;

namespace aima.core.search.framework.qsearch
{
    /// <summary>
    ///     A greedy algorithm (i.e. heuristic cost only). Beam width=1 is equivalent to hill-climbing.
    ///     <para/>
    ///     @author Richard Waliser
    /// </summary>
    public class BeamSearch : GraphSearch
    {
        private readonly HeuristicFunction m_heuristic;
        private readonly uint m_width;
        
        public BeamSearch(HeuristicFunction heuristic, uint width = DefaultWidth)
        {
            m_heuristic = heuristic;
            m_width = width;
        }

        public override List<Action> Search(Problem problem)
        {
            var frontier = CreatePriorityQueue();

            return base.search(problem, frontier);
        }

        public override string ToString()
        {
            return $"{nameof(BeamSearch)}, Width: {m_width}{(m_width == DefaultWidth ? " (Hill-Climbing)" : string.Empty)}";
        }

        protected override void OnNodeExpanded()
        {
            // frontier has been expanded and prioritized, now let's prune to beam width

            var beam = CreatePriorityQueue();
            var beamWidth = Math.Min(m_width, frontier.Count);

            for (var i = 0; i < beamWidth; i++)
            {
                beam.Enqueue(frontier.Dequeue());
            }

            frontier = beam;
        }

        private PriorityQueue<Node> CreatePriorityQueue()
        {
            // beam search uses only h(n) for sorting
            return new PriorityQueue<Node>(node => m_heuristic.h(node.getState()));
        }

        public const uint DefaultWidth = 1;
    }
}