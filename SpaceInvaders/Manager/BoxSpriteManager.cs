using System.Diagnostics;
namespace SpaceInvaders
{
    class BoxSpriteManager: Manager
    {
        private readonly BoxSprite spriteBox;
        private static BoxSpriteManager sBoxManagerInst;

        public BoxSpriteManager(int reserveSize, int growthSize) :
            base(new DList(), new DList(), reserveSize, growthSize)
            // LTN - BoxSpriteManager
            // DList object to inialize the manager
            // Owner: BoxSpriteManager, use to manage to reserve/active lists
        {
            // LTN - BoxSpriteManager
            // Sprite Box object for find 
            // Owner: BoxSpriteManager
            this.spriteBox = new BoxSprite();
        }

        public static void CreateSpriteBoxManager(int reserveSize, int growthSize)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(growthSize > 0);

            Debug.Assert(sBoxManagerInst == null);

            if (sBoxManagerInst == null)
            {
                // LTN - BoxSpriteManager
                // Singleton instance of the manager
                // Owner: BoxSpriteManager, it owns this singleton instance
                sBoxManagerInst = new BoxSpriteManager(reserveSize, growthSize);
            }
        }

        public static void Destroy()
        {
            Debug.Assert(sBoxManagerInst != null);
            BoxSpriteManager m = GetSpriteBoxManager();
            m = null;
            Debug.Assert(sBoxManagerInst == null);
        }

        public static BoxSprite Add(BoxSprite.BoxName name, float imageX, float imageY, float width, float height, int red=1, int green=1, int blue=1)
        {
            BoxSpriteManager sbMan = GetSpriteBoxManager();
            Debug.Assert(sbMan != null);

            BoxSprite sbox = (BoxSprite)sbMan.ManagerAddFront();
            Debug.Assert(sbox != null);

            Azul.Color c = new Azul.Color(red, green, blue);
            sbox.SetData(name, imageX, imageY, width, height, c);
            return sbox;
        }

        public static BoxSprite Find(BoxSprite.BoxName name)
        {
            BoxSpriteManager sMan = GetSpriteBoxManager();
            Debug.Assert(sMan != null);
            sMan.spriteBox.name = name;
            BoxSprite foundImage = (BoxSprite)sMan.ManagerFind(sMan.spriteBox);
            return foundImage;
        }

        public static void Remove(BoxSprite sb)
        {
            Debug.Assert(sb != null);
            BoxSpriteManager sMan = GetSpriteBoxManager();
            Debug.Assert(sMan != null);
            sMan.ManagerRemove(sb);
        }

        public static void Print()
        {
            BoxSpriteManager sMan = GetSpriteBoxManager();
            Debug.Assert(sMan != null);
            sMan.ManagerPrint();
        }

        public static void PrintSimple()
        {
            BoxSpriteManager sMan = GetSpriteBoxManager();
            Debug.Assert(sMan != null);
            sMan.ManagerPrintSimple();
        }

        private static BoxSpriteManager GetSpriteBoxManager()
        {
            Debug.Assert(sBoxManagerInst != null);
            return sBoxManagerInst;
        }

        protected override bool Compare(NodeBase nodeA, NodeBase nodeB)
        {
            Debug.Assert(nodeA != null);
            Debug.Assert(nodeB != null);
            BoxSprite sbA = (BoxSprite)nodeA;
            BoxSprite sbB = (BoxSprite)nodeB;
            bool cmp = false;
            if (sbA.name == sbB.name)
            {
                cmp = true;
            }
            return cmp;
        }

        protected override NodeBase CreateNode()
        {
            // LTN - BoxSpriteManager
            // Nodes created for object pooling design pattern
            // Owner: BoxSpriteManager, use to fill the reserve list, so it can be used in the active
            // Nodes gets recycle upon remove from active list
            NodeBase n = new BoxSprite();
            Debug.Assert(n != null);
            return n;
        }
    }
}

