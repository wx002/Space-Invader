using System.Diagnostics;

namespace SpaceInvaders
{
    class DList: ListBase
    {
        private DLink pHead;
        private DLink pTail;
        private DListIterator iterator;

        public DList()
        {
            pHead = null;
            pTail = null;
            iterator = new DListIterator();
        }

        public override void AddToFront(NodeBase _d)
        {
            Debug.Assert(_d != null);
            DLink pNode = (DLink)_d;
            Debug.Assert(pNode.pPrev == null);
            Debug.Assert(pNode.pNext == null);


            if (pHead == null)
            {
                pHead = pNode;
                pTail = pNode;
                pNode.pNext = null;
                pNode.pPrev = null;
            }
            else
            {
                pNode.pPrev = null;
                pNode.pNext = pHead;
                pHead.pPrev = pNode;
                pHead = pNode;
                //tail dont change since we add the node to the front of the list
            }
            Debug.Assert(pHead != null);
        }

        public override void AddToEnd(NodeBase _d)
        {
            Debug.Assert(_d != null);
            DLink pNode = (DLink)_d;
            Debug.Assert(pNode.pPrev == null);
            Debug.Assert(pNode.pNext == null);
            if (pHead == null)
            {
                pNode.pPrev = null;
                pHead = pNode;
                pTail = pNode;
            }
            else
            {
                pNode.pNext = null;
                pTail.pNext = pNode;
                pNode.pPrev = pTail;
                pTail = pNode;
                // head dont change since adding to tail
            }
            Debug.Assert(pHead != null);
        }

        public override void InsertNode(NodeBase nodeToInsert, NodeBase nodePrev)
        {
            Debug.Assert(nodeToInsert != null);
            Debug.Assert(nodePrev != null);
            

            DLink node = (DLink)nodeToInsert;
            Debug.Assert(node.pPrev == null);
            Debug.Assert(node.pNext == null);
            DLink prevNode = (DLink)nodePrev;
            DLink nextNode = (DLink)prevNode.pNext;

            prevNode.pNext = node;
            nextNode.pPrev = node;

            node.pNext = nextNode;
            node.pPrev = (DLink)nodePrev;
            if(node.pNext == null)
            {
                pTail = node;
            }
            
        }

        public override NodeBase RemoveFromFront()
        {
            Debug.Assert(pHead != null);
            DLink remove = pHead;
            pHead = pHead.pNext;
            if (pHead != null)
            {
                pHead.pPrev = null;
            }
            remove.Clear();
            remove.pNext = null;
            remove.pPrev = null;
            Debug.Assert(remove.pNext == null);
            Debug.Assert(remove.pPrev == null);
            return remove;
        }


        public override void Remove(NodeBase _pNode)
        {
            Debug.Assert(_pNode != null);
            DLink pNode = (DLink)_pNode;
            if(pNode.pPrev == null && pNode.pNext == null)
            {
                //head node
                pHead = null;
                pTail = null;
            }else if(pNode.pPrev == null && pNode.pNext != null)
            {
                pHead = pNode.pNext;
                if (pHead != null)
                {
                    pHead.pPrev = null;
                }
                pNode.pNext.pPrev = pHead.pPrev;
                
            }else if (pNode.pPrev != null & pNode.pNext == null)
            {
                //Last Node
                pNode.pPrev.pNext = pNode.pNext;
                pTail = pNode.pPrev;
                
            }
            else
            {
                //Middle nodes
                pNode.pNext.pPrev = pNode.pPrev;
                pNode.pPrev.pNext = pNode.pNext;
            }
            pNode.Clear();
            pNode.pNext = null;
            pNode.pPrev = null;
            Debug.Assert(pNode.pNext == null);
            Debug.Assert(pNode.pPrev == null);
        }

        public DLink getPhead()
        {
            return pHead;
        }

        public override Iterator GetIterator()
        {
            // STN
            // Iterator instance when needs to walk thru the list
            // This is short term new because it only use to iterator list and its done, no recycling process
            // This required because for nodes to be inserted back for time events, the location of the list iteration needs to be
            // maintained at different point, so it is neccessary to have this
            DListIterator itr = new DListIterator();
            itr.Reset(this.pHead);
            return itr;
        }

        public void DListPrint()
        {
            Iterator iter = GetIterator();
            NodeBase walk = iter.First();
            while (!iter.IsDone())
            {
                walk.Print();
                walk = iter.Next();
            }
        }

        public override DLink GetHead()
        {
            return this.pHead;
        }
    }
}
