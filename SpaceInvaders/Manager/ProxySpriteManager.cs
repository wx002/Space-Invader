using System.Diagnostics;

namespace SpaceInvaders
{
    class ProxySpriteManager: Manager
    {
        private readonly ProxySprite proxySprite;
        private static ProxySpriteManager psManager;

        public ProxySpriteManager(int reserveSize, int growthSize):base(new DList(), new DList(), reserveSize, growthSize)
        // LTN - ProxySpriteManager
        // DList for the active/reserve object pooling
        // Owner: ProxySpriteManager
        {
            // LTN - ProxySpriteManager
            // proxy sprite object for find
            // Owner: ProxySpriteManager
            this.proxySprite = new ProxySprite();
            this.proxySprite.spriteObj = SpriteManager.Find(Sprite.SpriteName.NULL);
        }

        public static void CreateProxySpriteManager(int reserveSize, int growthSize)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(growthSize > 0);
            Debug.Assert(psManager == null);
            if (psManager == null)
            {
                // LTN - ProxySpriteManager
                // Singleton instance of the manager
                // Owner: ProxySpriteManager
                psManager = new ProxySpriteManager(reserveSize, growthSize);
            }
            Add(Sprite.SpriteName.NULL);
        }

        public static void Destroy()
        {
            ProxySpriteManager m = GetProxySpriteManager();
            Debug.Assert(m != null);
            m = null;
            Debug.Assert(m == null);
        }


        public static ProxySprite Add(Sprite.SpriteName name)
        {
            ProxySpriteManager psManager = GetProxySpriteManager();
            Debug.Assert(psManager != null);

            ProxySprite pSprite = (ProxySprite)psManager.ManagerAddFront();
            Debug.Assert(pSprite != null);

            pSprite.Set(name);
            return pSprite;
        }

        public static ProxySprite Find(Sprite.SpriteName name)
        {
            ProxySpriteManager manager = GetProxySpriteManager();
            Debug.Assert(manager != null);
            manager.proxySprite.spriteObj.name = name;
            ProxySprite node = (ProxySprite)manager.ManagerFind(manager.proxySprite);
            return node;
        }


        public static void Remove(ProxySprite p)
        {
            Debug.Assert(p != null);
            ProxySpriteManager m = GetProxySpriteManager();
            Debug.Assert(m != null);
            m.ManagerRemove(p);
        }


        public static void Print()
        {
            ProxySpriteManager m = GetProxySpriteManager();
            Debug.Assert(m != null);
            m.ManagerPrint();
        }

        public static void PrintSimple()
        {
            ProxySpriteManager m = GetProxySpriteManager();
            Debug.Assert(m != null);
            m.ManagerPrintSimple();
        }


        private static ProxySpriteManager GetProxySpriteManager()
        {
            Debug.Assert(psManager != null);
            return psManager;
        }

        protected override bool Compare(NodeBase nodeA, NodeBase nodeB)
        {
            Debug.Assert(nodeA != null);
            Debug.Assert(nodeB != null);
            ProxySprite n1 = (ProxySprite)nodeA;
            ProxySprite n2 = (ProxySprite)nodeB;
            bool cmp = false;
            if (n1.spriteObj.name == n2.spriteObj.name)
            {
                cmp = true;
            }
            return cmp;
        }

        protected override NodeBase CreateNode()
        {
            // LTN - ProxySpriteManager
            // Nodes created for object pooling design pattern
            // Owner: ProxySpriteManager, use to fill the reserve list, so it can be used in the active
            // Nodes gets recycle upon remove from active list
            NodeBase n = new ProxySprite();
            Debug.Assert(n != null);
            return n;
        }
    }
}
