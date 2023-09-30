using System.Diagnostics;

namespace SpaceInvaders
{
    class CompositeIteratorBackward: CompositeIterator
    {
        private Component pRoot;
        private Component pCurr;
        private Component pPrev;
        public CompositeIteratorBackward(Component pStart)
        {
            Debug.Assert(pStart != null);
            Debug.Assert(pStart.type == Component.Container.COMPOSITE);

            //LTN - CompositeIteratorBackward
            //Using the horrible hack, will fix once understand how this really works
            CompositeIteratorForward forwardItr = new CompositeIteratorForward(pStart);

            // init data
            this.pRoot = pStart;
            this.pCurr = this.pRoot;
            this.pPrev = null;

            //idea of this hack:
            // walk the tree forward and mark down each iteration
            // add reverse reference to each node so we can call reverse to know how walk backward

            Component prev = pRoot;

            //reverse walk node
            Component walk = forwardItr.First();
            while (!forwardItr.IsDone())
            {
                prev = walk;
                walk = forwardItr.Next();
                if(walk != null)
                {
                    walk.reverse = prev;
                }
            }
            pRoot.reverse = prev;
        }

        public override Component First()
        {
            Debug.Assert(pRoot != null);
            pCurr = pRoot.reverse;
            return pCurr;
        }
        public override Component Current()
        {
            return pCurr;
        }
        public override Component Next()
        {
            Debug.Assert(pCurr != null);

            // prev became current
            pPrev = pCurr;

            // current is the reverse node ref
            pCurr = pCurr.reverse;
            return pCurr;
        }

        public override bool IsDone()
        {
            //when prev heads back to the root node, we finish walking the tree
            return (this.pPrev == this.pRoot);
        }


    }
}
