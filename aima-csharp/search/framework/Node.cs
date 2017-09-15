using System.Collections.Generic;
using aima.core.agent;

namespace aima.core.search.framework
{
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): Figure 3.10, page
     * 79.<br>
     * 
     * Figure 3.10 Nodes are the data structures from which the search tree is
     * constructed. Each has a parent, a state, and various bookkeeping fields.
     * Arrows point from child to parent.<br>
     * <br>
     * Search algorithms require a data structure to keep track of the search tree
     * that is being constructed. For each node n of the tree, we have a structure
     * that contains four components:
     * <ul>
     * <li>n.STATE: the state in the state space to which the node corresponds;</li>
     * <li>n.PARENT: the node in the search tree that generated this node;</li>
     * <li>n.ACTION: the action that was applied to the parent to generate the node;
     * </li>
     * <li>n.PATH-COST: the cost, traditionally denoted by g(n), of the path from
     * the initial state to the node, as indicated by the parent pointers.</li>
     * </ul>
     * 
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     * @author Mike Stampone
     */
     public class Node
    {
        // n.STATE: the state in the state space to which the node corresponds;
        private System.Object state;

        // n.PARENT: the node in the search tree that generated this node;
        private Node parent;

        // n.ACTION: the action that was applied to the parent to generate the node;
        private Action action;

        // n.PATH-COST: the cost, traditionally denoted by g(n), of the path from
        // the initial state to the node, as indicated by the parent pointers.
        private double pathCost;

        /**
	 * Constructs a node with the specified state.
	 * 
	 * @param state
	 *            the state in the state space to which the node corresponds.
         */
        public Node(System.Object state)
        {
            this.state = state;
            this.pathCost = 0.0;
        }

        /**
	 * Constructs a node with the specified state, parent, action, and path
	 * cost.
	 * 
	 * @param state
	 *            the state in the state space to which the node corresponds.
	 * @param parent
    	 *            the node in the search tree that generated the node.
    	 * @param action
    	 *            the action that was applied to the parent to generate the
    	 *            node.
    	 * @param pathCost
    	 *            full pathCost from the root node to here, typically
    	 *            the root's path costs plus the step costs for executing
    	 *            the the specified action.
    	 */
        public Node(System.Object state, Node parent, Action action, double stepCost) : this(state) 
        {            
            this.parent = parent;
            this.action = action;
            this.pathCost = parent.pathCost + stepCost;
        }

        /**
	 * Returns the state in the state space to which the node corresponds.
	 * 
	 * @return the state in the state space to which the node corresponds.
	 */
        public System.Object getState()
        {
            return state;
        }

        /**
    	 * Returns this node's parent node, from which this node was generated.
    	 * 
    	 * @return the node's parenet node, from which this node was generated.
    	 */
        public Node getParent()
        {
            return parent;
        }

        /**
	 * Returns the action that was applied to the parent to generate the node.
	 * 
    	 * @return the action that was applied to the parent to generate the node.
    	 */
        public Action getAction()
        {
            return action;
        }

        /**
	 * Returns the cost of the path from the initial state to this node as
	 * indicated by the parent pointers.
    	 * 
	 * @return the cost of the path from the initial state to this node as
    	 *         indicated by the parent pointers.
    	 */
        public double getPathCost()
        {
            return pathCost;
        }

        /**
	 * Returns <code>true</code> if the node has no parent.
	 * 
	 * @return <code>true</code> if the node has no parent.
	 */
        public bool isRootNode()
        {
            return parent == null;
        }

        /**
	 * Returns the path from the root node to this node.
	 * 
    	 * @return the path from the root node to this node.
    	 */
        public List<Node> getPathFromRoot()
        {
            List<Node> path = new List<Node>();
            Node current = this;
            while (!current.isRootNode())
            {
                path.Insert(0, current);
                current = current.getParent();
            }
            // ensure the root node is added
            path.Insert(0, current);
            return path;
        }

        public override System.String ToString()
        {
            return "[parent=" + parent + ", action=" + action + ", state="
                    + getState() + ", pathCost=" + pathCost + "]";
        }

        public override bool Equals(object obj)
        {
            var other = obj as Node;

            if(other == null)
            {
                return false;
            }

            // one node per world state
            return Equals(getState(), other.getState());
        }

        public static bool operator ==(Node a, Node b)
        {
            return Equals(a, b);
        }

        public static bool operator !=(Node a, Node b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return getState().GetHashCode();
        }
    }
}