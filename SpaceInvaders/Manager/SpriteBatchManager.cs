using System.Diagnostics;

namespace SpaceInvaders
{
    class SpriteBatchManager: Manager
    {
        private SpriteBatchNode nodeInstance;
        private static SpriteBatchManager sbManager;
        private static SpriteBatchManager activeBatchManager;

        public SpriteBatchManager(int reserveSize = 5, int growthSize = 2) : base(new DList(), new DList(), reserveSize, growthSize)
        // LTN - SpriteBatchManager
        // DList objects for object pooling
        // Owner: SpriteBatchManager
        {
            // LTN - SpriteBatchManager
            // Object instance for find
            // Owner: SpriteBatchManager
            this.nodeInstance = new SpriteBatchNode();
        }

        public static void CreateSpriteBatchManager(int reserveSize = 5, int growthSize = 2)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(growthSize > 0);

            Debug.Assert(sbManager == null);
            if(sbManager == null)
            {
                // LTN - SpriteBatchManager
                // Singleton instance of the manager
                // Owner: SpriteBatchManager
                sbManager = new SpriteBatchManager(reserveSize, growthSize);
            }
        }

        public static void Destroy()
        {
            SpriteBatchManager m = SpriteBatchManager.activeBatchManager;
            Debug.Assert(sbManager != null);
            m = null;
            Debug.Assert(m == null);
        }

        public static SpriteBatchNode Add(SpriteBatchNode.GroupTypes name, int reserveSize, int growthSize, int proroity)
        {
            SpriteBatchManager s = SpriteBatchManager.activeBatchManager;
            Debug.Assert(s != null);
            SpriteBatchNode sbNode = (SpriteBatchNode)s.ManagerAddFront();
            

            sbNode.SetGroup(name, reserveSize, growthSize, proroity);
            return sbNode;
        }

        public static void SetActiveBatchManager(SpriteBatchManager m)
        {
            //SpriteBatchManager manager = GetSpriteBatchManager();
            //Debug.Assert(manager != null);
            Debug.Assert(m != null);
            SpriteBatchManager.activeBatchManager = m;
        }

        public static void Remove(SpriteNode spriteNode)
        {
            Debug.Assert(spriteNode != null);
            SpriteNodeManager nodeManager = spriteNode.GetSpriteNodeManager();

            Debug.Assert(nodeManager != null);
            nodeManager.Remove(spriteNode);
        }

        public static SpriteBatchNode AddByPriority(SpriteBatchNode.GroupTypes name, int reserveSize, int growthSize, int priority)
        {
            SpriteBatchManager s = SpriteBatchManager.activeBatchManager;
            Debug.Assert(s != null);
            Iterator iter = s.ManagerGetActiveIterator();
            SpriteBatchNode compare = (SpriteBatchNode)iter.First();
            SpriteBatchNode nodeToAdd;
            if (compare == null)
            {
                nodeToAdd = (SpriteBatchNode)s.ManagerAddFront();
            }
            else
            {
                if (priority >= compare.getPrority())
                {
                    nodeToAdd = (SpriteBatchNode)s.ManagerAddFront();
                }
                else
                {
                    nodeToAdd = (SpriteBatchNode)s.ManagerAddEnd();
                }
            }
            nodeToAdd.SetGroup(name, reserveSize, growthSize, priority);
            return nodeToAdd;
        }


        public static void Draw()
        {
            SpriteBatchManager sbM = SpriteBatchManager.activeBatchManager;
            Debug.Assert(sbM != null);
            Iterator itr = sbM.ManagerGetActiveIterator();
            Debug.Assert(itr != null);

            SpriteBatchNode walk = (SpriteBatchNode)itr.First();
            //shield -> Text -> Ship -> Walls -> Aliens
            while (!itr.IsDone())
            {
                if (walk.GetIsDrawable())
                {
                    SpriteNodeManager snM = walk.GetGroupManager();
                    Debug.Assert(snM != null);
                    snM.Draw();
                }
                walk = (SpriteBatchNode)itr.Next();
            }
        }

        public static SpriteBatchNode Find(SpriteBatchNode.GroupTypes name)
        {
            SpriteBatchManager sbM = SpriteBatchManager.activeBatchManager;
            Debug.Assert(sbM != null);
            sbM.nodeInstance.name = name;
            SpriteBatchNode node = (SpriteBatchNode)sbM.ManagerFind(sbM.nodeInstance);
            return node;
        }

        public static void RemoveBatch(SpriteBatchNode.GroupTypes name)
        {
            SpriteBatchManager sbM = SpriteBatchManager.activeBatchManager;
            Debug.Assert(sbM != null);
            //find it
            SpriteBatchNode remove = Find(name);
            remove.GetGroupManager().Destroy();//remove the sprite node manager
            sbM.ManagerRemove(remove);

        }

        public static void Print()
        {
            SpriteBatchManager sbM = SpriteBatchManager.activeBatchManager;
            Debug.Assert(sbM != null);
            sbM.ManagerPrint();
        }

        private static SpriteBatchManager GetSpriteBatchManager()
        {
            Debug.Assert(sbManager != null);
            return sbManager;
        }

        protected override bool Compare(NodeBase nodeA, NodeBase nodeB)
        {
            Debug.Assert(nodeA != null);
            Debug.Assert(nodeB != null);
            SpriteBatchNode spA = (SpriteBatchNode)nodeA;
            SpriteBatchNode spB = (SpriteBatchNode)nodeB;
            bool cmp = false;
            if (spA.name == spB.name)
            {
                cmp = true;
            }
            return cmp;
        }

        protected override NodeBase CreateNode()
        {
            // LTN - SpriteBatchManager
            // Nodes created for object pooling design pattern
            // Owner: SpriteBatchManager, use to fill the reserve list, so it can be used in the active
            // Nodes gets recycle upon remove from active list
            NodeBase node = new SpriteBatchNode();
            Debug.Assert(node != null);
            return node;
        }

        public static void ResetBatch(SpriteBatchNode.GroupTypes name)
        {
            SpriteBatchNode node = SpriteBatchManager.Find(name);
            Debug.Assert(node != null);
            node.GetGroupManager().ResetManager();
        }
    }
}
