using System.Diagnostics;

namespace SpaceInvaders
{
    class CollisionPairManager : Manager
    {

        private readonly CollisionPair cPair;
        private static CollisionPairManager cpManager = null;
        private CollisionPair activePair;
        private static CollisionPairManager activeManager = null;

        public CollisionPairManager(int reserveSize, int growthSize):base(new DList(), new DList(), reserveSize, growthSize)
        {
            //LTN - CollisionPairManager
            cPair = new CollisionPair();

            //active pair, use in proccess
            activePair = null;
        }

        public static void SetActiveManager(CollisionPairManager manager)
        {
            Debug.Assert(manager != null);
            activeManager = manager;
        }

        public static void CreateCollisionPairManager(int reserveSize, int growthSize)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(growthSize > 0);

            Debug.Assert(cpManager == null); //make sure is singleton
            if(cpManager == null)
            {
                cpManager = new CollisionPairManager(reserveSize, growthSize);
            }
        }

        public static void Destroy()
        {
            CollisionPairManager m = GetCollisionPairManager();
            Debug.Assert(m != null);
            m = null;
            Debug.Assert(m == null);
        }

        public static CollisionPair Add(CollisionPair.PairName name, GameObject.GOName treeName1, GameObject.GOName treeName2)
        {
            CollisionPairManager m = activeManager;
            Debug.Assert(m != null);

            GameObject objA = GameObjectNodeManager.Find(treeName1);
            Debug.Assert(objA != null);
            GameObject objB = GameObjectNodeManager.Find(treeName2);
            Debug.Assert(objB != null);

            CollisionPair node = (CollisionPair)m.ManagerAddFront();
            Debug.Assert(node != null);
            node.Set(name, objA, objB);
            return node;
        }

        public static CollisionPair Add(CollisionPair.PairName name, GameObject treeAHead, GameObject treeBHead)
        {
            CollisionPairManager m = activeManager;
            Debug.Assert(m != null);

            CollisionPair node = (CollisionPair)m.ManagerAddFront();
            Debug.Assert(node != null);

            node.Set(name, treeAHead, treeBHead);
            return node;
        }

        public static void ProcessCollision()
        {
            CollisionPairManager m = activeManager;
            Debug.Assert(m != null);

            Iterator itr = m.ManagerGetActiveIterator();
            Debug.Assert(itr != null);
            CollisionPair walk = (CollisionPair)itr.First();
            while (!itr.IsDone())
            {
                //set the active pair here
                m.activePair = walk;
                Debug.Assert(walk != null);
                walk.ProcessCollision();
                walk = (CollisionPair)itr.Next();
            }
        }

        public static CollisionPair GetActiveCollisionPair()
        {
            CollisionPairManager m = activeManager;
            return m.activePair;
        }

        public static CollisionPair Find(CollisionPair.PairName name)
        {
            CollisionPairManager m = activeManager;
            Debug.Assert(m != null);

            m.cPair.name = name;
            CollisionPair found = (CollisionPair)m.ManagerFind(m.cPair);
            return found;
        }

        public static void Remove(CollisionPair rm)
        {
            Debug.Assert(rm != null);
            CollisionPairManager m = activeManager;
            Debug.Assert(m != null);
            m.ManagerRemove(rm);
        }


        public static void Print()
        {
            CollisionPairManager m = activeManager;
            Debug.Assert(m != null);
            m.ManagerPrint();
        }

        public static void PrintSimple()
        {
            CollisionPairManager m = activeManager;
            Debug.Assert(m != null);
            m.ManagerPrintSimple();
        }
        

        private static CollisionPairManager GetCollisionPairManager()
        {
            Debug.Assert(cpManager != null);
            return cpManager;
        }

        protected override bool Compare(NodeBase nodeA, NodeBase nodeB)
        {
            Debug.Assert(nodeA != null);
            Debug.Assert(nodeB != null);
            CollisionPair imgA = (CollisionPair)nodeA;
            CollisionPair imgB = (CollisionPair)nodeB;
            bool cmp = false;
            if (imgA.name == imgB.name)
            {
                cmp = true;
            }
            return cmp;
        }

        protected override NodeBase CreateNode()
        {
            NodeBase node = new CollisionPair();
            Debug.Assert(node != null);
            return node;
        }
    }
}
