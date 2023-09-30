using System.Diagnostics;

namespace SpaceInvaders
{
    class GameObjectNodeManager:Manager
    {
        private readonly GameObjectNode goNode;
        private GameObjectNull gameObject;
        private static GameObjectNodeManager goManager = null;
        private static GameObjectNodeManager activeManager = null;

        public GameObjectNodeManager(int reserveSize, int growthSize):base(new DList(), new DList(), reserveSize, growthSize)
            // LTN - GameObjectNodeManager
            // Use to manage to reserve/active listing
            // Owner: GameObjectNodeManager
        {
            // LTN - GameObjectNodeManager
            // Owner: GameObjectNodeManager
            goNode = new GameObjectNode();

            // LTN - GameObjectNodeManager
            //Owner: GameObjectNodeManager
            gameObject = new GameObjectNull();
            goNode.gameObj = gameObject;

        }

        public static void CreateGameObjectNodeManager(int reserveSize, int growthSize)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(growthSize > 0);

            Debug.Assert(goManager == null);

            if(goManager == null)
            {
                goManager = new GameObjectNodeManager(reserveSize, growthSize);
            }
        }

        public static void SetActiveGameObjectManager(GameObjectNodeManager manager)
        {
            Debug.Assert(manager != null);
            activeManager = manager;
        }

        public static void Destroy()
        {
            GameObjectNodeManager m = GetGameObjectNodeManager();
            Debug.Assert(m != null);
            m = null;
            Debug.Assert(m == null);
        }

        public static GameObjectNode Add(GameObject g)
        {
            GameObjectNodeManager m = GameObjectNodeManager.activeManager;
            Debug.Assert(m != null);

            GameObjectNode n = (GameObjectNode)m.ManagerAddFront();
            Debug.Assert(n != null);
            n.Set(g);
            return n;
        }

        public static GameObject Find(GameObject.GOName name) 
        {
            GameObjectNodeManager m = GameObjectNodeManager.activeManager;
            Debug.Assert(m != null);
            Debug.Assert(m.goNode.gameObj != null);
            m.goNode.gameObj.name = name;
            GameObjectNode node = (GameObjectNode)m.ManagerFind(m.goNode);
            Debug.Assert(node != null);
            return node.gameObj;
        }

        public static void Update()
        {
            GameObjectNodeManager m = GameObjectNodeManager.activeManager;
            Debug.Assert(m != null);

            Iterator itr = m.ManagerGetActiveIterator();
            GameObjectNode walk = (GameObjectNode)itr.First();

            while (!itr.IsDone())
            {
                CompositeIteratorBackward backwardItr = new CompositeIteratorBackward(walk.gameObj);
                Component revWalk = backwardItr.First();
                while (!backwardItr.IsDone())
                {
                    GameObject g = (GameObject)revWalk;
                    g.Update();
                    revWalk = backwardItr.Next();
                }
                //walk.gameObj.Update();
                walk = (GameObjectNode)itr.Next();
            }
        }


        public static void Remove(GameObjectNode removeNode)
        {
            Debug.Assert(removeNode != null);
            GameObjectNodeManager m = GameObjectNodeManager.activeManager;
            Debug.Assert(m != null);
            m.ManagerRemove(removeNode);
        }

        public static void Remove(GameObject gameObj)
        {
            Debug.Assert(gameObj != null);

            GameObjectNodeManager manager = GameObjectNodeManager.activeManager;
            Debug.Assert(manager != null);

            GameObject backupNode = gameObj;

            //DList of trees within game object node manager, so we need to iterate tree structure

            // 1. Find the root tree node
            GameObject walk = gameObj;
            GameObject treeRoot = null; //init

            while (walk != null)
            {
                treeRoot = walk; //keep going for the parent until walk node is null
                //this ensure that the root will never be null
                walk = (GameObject)CompositeIteratorForward.GetParentNode(walk);
            }

            // Now we have a root, we need to find it within our manager
            Iterator gameNodeManagerItr = manager.ManagerGetActiveIterator();
            GameObjectNode gameNodeManagerWalk = (GameObjectNode)gameNodeManagerItr.First();

            while (!gameNodeManagerItr.IsDone())
            {
                if (gameNodeManagerWalk.gameObj == treeRoot)
                {
                    //found it
                    break;
                }
                gameNodeManagerWalk = (GameObjectNode)gameNodeManagerItr.Next();
            }

            // remove from the tree that we found
            GameObject parentNode = (GameObject)CompositeIteratorForward.GetParentNode(gameObj);
            GameObject childNode = (GameObject)CompositeIteratorForward.GetChildNode(gameObj);
            Debug.Assert(parentNode != null);
            Debug.Assert(childNode == null); //child should be null
            parentNode.Remove(gameObj);
        }



        public static void Print()
        {
            GameObjectNodeManager m = GameObjectNodeManager.activeManager;
            Debug.Assert(m != null);
            m.ManagerPrint();
        }

        public static void PrintSimple()
        {
            GameObjectNodeManager m = GameObjectNodeManager.activeManager;
            Debug.Assert(m != null);
            m.ManagerPrintSimple();
        }

        protected override bool Compare(NodeBase nodeA, NodeBase nodeB)
        {
            Debug.Assert(nodeA != null);
            Debug.Assert(nodeB != null);
            GameObjectNode tA = (GameObjectNode)nodeA;
            GameObjectNode tB = (GameObjectNode)nodeB;
            bool cmp = false;
            if (tA.gameObj.name == tB.gameObj.name)
            {
                cmp = true;
            }
            return cmp;
        }

        protected override NodeBase CreateNode()
        {
            // LTN - GameObjectNodeManager
            // these nodes only created when needed and constantly being used
            // Owner: GameObjectNodeManager, owned and being used within the active/reserve lists
            NodeBase node = new GameObjectNode();
            Debug.Assert(node != null);
            return node;
        }


        private static GameObjectNodeManager GetGameObjectNodeManager()
        {
            Debug.Assert(goManager != null);
            return goManager;
        }
    }
}
