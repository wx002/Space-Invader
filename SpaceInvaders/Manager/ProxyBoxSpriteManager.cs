using System.Diagnostics;

namespace SpaceInvaders
{
    class ProxyBoxSpriteManager: Manager
    {
        private readonly ProxyBoxSprite proxyBoxSprite;
        private static ProxyBoxSpriteManager pbManager;

        public ProxyBoxSpriteManager(int reserveSize, int growthSize):base(new DList(), new DList(), reserveSize, growthSize)
        // LTN - ProxyBoxSpriteManager
        // DList objects for object pooling
        // Owner: ProxyBoxSpriteManager
        {
            // Long Term New
            // ProxyBoxSprite for find
            // Owner: ProxyBoxSpriteManager
            this.proxyBoxSprite = new ProxyBoxSprite();
        }

        public static void CreateProxyBoxSpriteManager(int reserveSize, int growthSize)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(growthSize > 0);
            Debug.Assert(pbManager == null);
            if (pbManager == null)
            {
                // LTN - ProxyBoxSpriteManager
                // Singleton instance of the manager
                // ProxyBoxSpriteManager
                pbManager = new ProxyBoxSpriteManager(reserveSize, growthSize);
            }
        }

        public static ProxyBoxSprite Find(ProxyBoxSprite.ProxyBoxName name)
        {
            ProxyBoxSpriteManager pbsManager = GetProxyBoxSpriteManager();
            Debug.Assert(pbsManager != null);
            pbsManager.proxyBoxSprite.name = name;
            ProxyBoxSprite pbSprite = (ProxyBoxSprite)pbsManager.ManagerFind(pbsManager.proxyBoxSprite);
            Debug.Assert(pbSprite != null);
            return pbSprite;
        }

        public static void Destroy()
        {
            ProxyBoxSpriteManager m = GetProxyBoxSpriteManager();
            Debug.Assert(m != null);
            m = null;
            Debug.Assert(m == null);
        }


        public static ProxyBoxSprite Add(BoxSprite.BoxName name)
        {
            ProxyBoxSpriteManager pbManager = GetProxyBoxSpriteManager();
            Debug.Assert(pbManager != null);

            ProxyBoxSprite pSprite = (ProxyBoxSprite)pbManager.ManagerAddFront();
            Debug.Assert(pSprite != null);

            pSprite.Set(name);
            return pSprite;
        }


        public static void Remove(ProxySprite p)
        {
            Debug.Assert(p != null);
            ProxyBoxSpriteManager m = GetProxyBoxSpriteManager();
            Debug.Assert(m != null);
            m.ManagerRemove(p);
        }


        public static void Print()
        {
            ProxyBoxSpriteManager m = GetProxyBoxSpriteManager();
            Debug.Assert(m != null);
            m.ManagerPrint();
        }

        public static void PrintSimple()
        {
            ProxyBoxSpriteManager m = GetProxyBoxSpriteManager();
            Debug.Assert(m != null);
            m.ManagerPrintSimple();
        }


        private static ProxyBoxSpriteManager GetProxyBoxSpriteManager()
        {
            Debug.Assert(pbManager != null);
            return pbManager;
        }

        protected override bool Compare(NodeBase nodeA, NodeBase nodeB)
        {
            Debug.Assert(nodeA != null);
            Debug.Assert(nodeB != null);
            ProxySprite n1 = (ProxySprite)nodeA;
            ProxySprite n2 = (ProxySprite)nodeB;
            bool cmp = false;
            if (n1.name == n2.name)
            {
                cmp = true;
            }
            return cmp;
        }

        protected override NodeBase CreateNode()
        {
            // LTN - ProxyBoxSpriteManager
            // Nodes created for object pooling design pattern
            // Owner: ProxyBoxSpriteManager, use to fill the reserve list, so it can be used in the active
            // Nodes gets recycle upon remove from active list
            NodeBase n = new ProxyBoxSprite();
            Debug.Assert(n != null);
            return n;
        }
    }
}
