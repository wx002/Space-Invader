using System.Diagnostics;

namespace SpaceInvaders
{
    class CompositeIteratorForward: CompositeIterator
    {
        private Component pCurr;
        private Component pRoot;
        public CompositeIteratorForward(Component pStart)
        {
            Debug.Assert(pStart != null);
            Debug.Assert(pStart.type == Component.Container.COMPOSITE);

            pCurr = pStart;
            pRoot = pStart;
        }
        public override Component Current()
        {
            return this.pCurr;
        }
        public override Component First()
        {
            Debug.Assert(pRoot != null);
            Component walk = pRoot;
            Debug.Assert(walk != null);
            this.pCurr = walk;
            return pCurr;
        }

        public override Component Next()
        {
            //using our private helper function
            Debug.Assert(pCurr != null);

            Component walk = pCurr;

            //get the inital nodes around pCurr
            Component child = GetChildNode(pCurr);
            Component sibling = GetSiblingNode(pCurr);
            Component parent = GetParentNode(pCurr);
            walk = priGetNextNode(walk, parent, child, sibling);
            this.pCurr = walk;
            return this.pCurr;
        }

        public override bool IsDone()
        {
            //when current is null, we finish walking the tree
            return (this.pCurr == null);
        }
        
        public static Component GetChildNode(Component node)
        {
            Debug.Assert(node != null);
            Component child;

            if(node.type == Component.Container.COMPOSITE)
            {
                child = ((Composite)node).GetHead();
            }
            else
            {
                child = null;
            }
            return child;
        }

        public static Component GetSiblingNode(Component node)
        {
            //Inside a composite iteration
            Debug.Assert(node != null);
            return (Component)node.pNext;
        }

        public static Component GetParentNode(Component node)
        {
            //Inside a node, backtracking
            Debug.Assert(node != null);
            return node.parent;
        }


        private Component priGetNextNode(Component walkNode, Component parentNode, Component childNode, Component siblingNode)
        {
            walkNode = null;
            if (childNode != null) //check child, if child not null, its a leaf, return
            {
                walkNode = childNode;
            }
            else //otherwise, its a composite
            {
                if (siblingNode != null) //walk the composite dlist
                {
                    walkNode = siblingNode;
                }
                else //otherwise, all the composite list are wallked, we backtrack for parents
                {
                    while (parentNode != null)
                    {
                        walkNode = GetSiblingNode(parentNode); //found a new parent(composite type), we grab it
                        if (walkNode != null)
                        {
                            break; //found it, break while loop
                        }
                        else
                        {
                            parentNode = GetParentNode(parentNode); //not found, we keep going to backtrack for parent nodes
                        }
                    }
                }
            }
            return walkNode; //return the node
        }
    }
}
