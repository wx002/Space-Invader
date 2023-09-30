using System.Diagnostics;
namespace SpaceInvaders
{
    class GhostManager : Manager
    {
        private readonly GameObjectNode instance;
        private readonly GameObjectNull nullInstance;
        private static GhostManager managerInst;

        public GhostManager(int reserveSize, int growthSize):
            base(new DList(), new DList(), reserveSize, growthSize)
        // LTN - GhostManager
        // DLists for manage the active/reserve for object pooling
        // Owner: GhostManager
        {
            // LTN - GhostManager
            // GameObjectNode object instance for find
            // Owner: GhostManager
            this.instance = new GameObjectNode();
            this.nullInstance = new GameObjectNull();
            this.instance.gameObj = nullInstance;
        }

        public static void SetActiveManager(GhostManager manager)
        {
            Debug.Assert(manager != null);
            managerInst = manager;
        }

        public static void CreateGhostManager(int reserveSize, int growthSize)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(growthSize > 0);

            Debug.Assert(managerInst == null);

            if(managerInst == null)
            {
                managerInst = new GhostManager(reserveSize, growthSize);
            }            
        }

        public static void Destroy()
        {
            Debug.Assert(managerInst != null);
            GhostManager m = GetGhostManager();
            m = null;
            Debug.Assert(managerInst == null);
        }

        public static GameObjectNode Add(GameObject gameObj)
        {
            GhostManager iMan = GetGhostManager();
            Debug.Assert(iMan != null);

            GameObjectNode pGameObjectNode = (GameObjectNode)iMan.ManagerAddFront();
            Debug.Assert(pGameObjectNode != null);

            pGameObjectNode.Set(gameObj);
            return pGameObjectNode;
        }

        public static GameObjectNode Find(GameObject.GOName name)
        {
            GhostManager m = GetGhostManager();
            Debug.Assert(m != null);
            m.instance.gameObj.name = name;
            GameObjectNode foundGameObjectNode = (GameObjectNode)m.ManagerFind(m.instance);
            return foundGameObjectNode;
        }

        public static void Remove(GameObjectNode node)
        {
            Debug.Assert(node != null);
            GhostManager m = GetGhostManager();
            Debug.Assert(m != null);
            m.ManagerRemove(node);
        }

        public static void Print()
        {
            managerInst.ManagerPrint();
        }

        public static void PrintSimple()
        {
            managerInst.ManagerPrintSimple();
        }

        private static GhostManager GetGhostManager()
        {
            Debug.Assert(managerInst != null);
            return managerInst;
        }

        protected override bool Compare(NodeBase nodeA, NodeBase nodeB)
        {
            Debug.Assert(nodeA != null);
            Debug.Assert(nodeB != null);
            GameObjectNode itemA = (GameObjectNode)nodeA;
            GameObjectNode itemB = (GameObjectNode)nodeB;
            bool cmp = false;
            if (itemA.gameObj.name == itemB.gameObj.name)
            {
                cmp = true;
            }
            return cmp;
        }

        protected override NodeBase CreateNode()
        {
            // LTN - GhostManager
            // Nodes created for object pooling design pattern
            // Owner: GhostManager, use to fill the reserve list, so it can be used in the active
            // Nodes gets recycle upon remove from active list
            NodeBase n = new GameObjectNode();
            Debug.Assert(n != null);
            return n;
        }
    }
}
