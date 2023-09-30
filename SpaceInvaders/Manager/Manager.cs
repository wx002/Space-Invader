using System.Diagnostics;
namespace SpaceInvaders
{
    abstract class Manager
    {
        protected ListBase pReserved;
        protected ListBase pActive;

        private int mNumActive;
        private int mNumReserved;

        private int growthSize;

        public Manager(ListBase _pActive, ListBase _pReserved, int reserveSize, int growthSize)
        {
            mNumReserved = 0;
            mNumActive = 0;

            this.growthSize = growthSize;
            pActive = _pActive;
            pReserved = _pReserved;

            //init Reserve list
            refillReserveList(reserveSize);
        }

        /**
         * Add the node from reserved to active 
         */
        public NodeBase ManagerAddFront(){
            if(mNumReserved == 0)
            {
                refillReserveList(growthSize);
            }
            if(mNumReserved >0 && pReserved.GetHead() == null)
            {
                refillReserveList(mNumReserved);
            }
            NodeBase node = pReserved.RemoveFromFront();
            mNumReserved -= 1;
            pActive.AddToFront(node);
            mNumActive += 1;
            return node;
        }

        public NodeBase ManagerAddEnd()
        {
            if (mNumReserved == 0)
            {
                refillReserveList(growthSize);
            }
            NodeBase node = pReserved.RemoveFromFront();
            pActive.AddToEnd(node);
            mNumActive += 1;
            mNumReserved -= 1;
            return node;
        }

        public void ResetManager()
        {
            this.mNumActive = 0;
            this.pActive = new DList();
        }

        protected NodeBase ManagerFind(NodeBase target) 
        {
            // using Iterator
            Iterator pIterator = pActive.GetIterator();

            NodeBase node = pIterator.First();
            while (!pIterator.IsDone())
            {
                if(Compare(node, target))
                {
                    break;
                }
                node = pIterator.Next();
            }
            return node;
        }

        abstract protected bool Compare(NodeBase nodeA, NodeBase nodeB);
        abstract protected NodeBase CreateNode();


        public void ManagerRemove(NodeBase node)
        {
            pActive.Remove(node);
            mNumActive -= 1;
            Debug.Assert(node != null);
            pReserved.AddToFront(node);
            mNumReserved += 1;
        }

        private void refillReserveList(int numNodes)
        {
            for (int i = 0; i < numNodes; i++)
            {
                NodeBase t = this.CreateNode();
                pReserved.AddToFront(t);
            }
            mNumReserved += numNodes;   
        }

        public void ManagerInitReserve(int reserveSize, int growthSize)
        {
            this.growthSize = growthSize;
            if(reserveSize > mNumReserved)
            {
                refillReserveList(reserveSize - mNumReserved);
            }
        }

        public Iterator ManagerGetActiveIterator()
        {
            return pActive.GetIterator();
        }

        public NodeBase ManagerInsert(NodeBase nodePrev)
        {
            if (mNumReserved == 0)
            {
                refillReserveList(growthSize);
            }
            NodeBase node = pReserved.RemoveFromFront();
            pActive.InsertNode(node, nodePrev);
            mNumActive++;
            mNumReserved--;
            return node;
        }

        public void ManagerPrint()
        {
            Debug.WriteLine("growth Size: {0} ", growthSize);
            Debug.WriteLine("mNumReserved: {0} ", mNumReserved);
            Debug.WriteLine("mNumActive: {0} \n", mNumActive);

            Iterator pItActive = pActive.GetIterator();
            Debug.Assert(pItActive != null);

            NodeBase pNodeActive = pItActive.First();
            if (pNodeActive == null)
            {
                Debug.WriteLine("Active Head Node: null");
            }
            else
            {
                Debug.WriteLine("Active Head Node : ({0})", pNodeActive.GetHashCode());
            }

            Iterator pItReserve = pReserved.GetIterator();
            Debug.Assert(pItReserve != null);

            NodeBase pNodeReserve = pItReserve.First();
            if (pNodeReserve == null)
            {
                Debug.WriteLine("Reserve Head Node: null\n");
            }
            else
            {
                Debug.WriteLine("Reserve Head Node: ({0})\n", pNodeReserve.GetHashCode());
            }

            Debug.WriteLine("Active List:\n");


            int i = 0;
            NodeBase pData = pItActive.First();
            while (!pItActive.IsDone())
            {
                Debug.WriteLine("Node num: {0}", i);
                pData.Print();
                i++;
                pData = pItActive.Next();
            }

            Debug.WriteLine("Reserve List:\n");
            i = 0;
            pData = pItReserve.First();
            while (!pItReserve.IsDone())
            {
                Debug.WriteLine("Node num: {0}", i);
                pData.Print();
                i++;
                pData = pItReserve.Next();
            }
            Debug.WriteLine("---------- End -----------\n\n");
        }

        public void ManagerPrintSimple()
        {
            Debug.WriteLine("growth Size: {0} ", growthSize);
            Debug.WriteLine("mNumReserved: {0} ", mNumReserved);
            Debug.WriteLine("mNumActive: {0} \n", mNumActive);

            Iterator pItActive = pActive.GetIterator();
            Debug.Assert(pItActive != null);

            NodeBase pNodeActive = pItActive.First();
            if (pNodeActive == null)
            {
                Debug.WriteLine("Active Head Node: null");
            }
            else
            {
                Debug.WriteLine("Active Head Node : ({0})", pNodeActive.GetHashCode());
            }

            Debug.WriteLine("Active List:\n");
            int i = 0;
            NodeBase pData = pItActive.First();
            while (!pItActive.IsDone())
            {
                Debug.WriteLine("Node num: {0}", i);
                pData.Print();
                i++;
                pData = pItActive.Next();
            }
            Debug.WriteLine("---------- End -----------\n\n");
        }
    }

    
}
