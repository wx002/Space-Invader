using System.Diagnostics;


namespace SpaceInvaders
{
    class SpriteManager : Manager
    {
        private readonly Sprite spriteInstance;
        private static SpriteManager sManagerInstance = null;
        public SpriteManager(int reserveSize, int growthSize):base(new DList(), new DList(), reserveSize, growthSize)
        // LTN - SpriteManager
        // DList for the active/reserve object pooling
        // Owner: SpriteManager
        {
            // LTN - SpriteManager
            // object instance for find
            // Owner: SpriteManager
            spriteInstance = new Sprite();
        }

        public static void CreateSpriteManager(int reserveSize, int growthSize)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(growthSize > 0);

            Debug.Assert(sManagerInstance == null);
            if(sManagerInstance == null)
            {
                // LTN - SpriteManager
                sManagerInstance = new SpriteManager(reserveSize, growthSize);
            }

            //Add null sprite into manager
            Add(Sprite.SpriteName.NULL, Image.ImageName.HotPink, 0.0f, 0.0f, 0.0f, 0.0f);
        }

        public static void Destroy()
        {
            Debug.Assert(sManagerInstance != null);
            SpriteManager s = GetSpriteManager();
            s = null;
            Debug.Assert(sManagerInstance == null);

        }

        private static SpriteManager GetSpriteManager()
        {
            Debug.Assert(sManagerInstance != null);
            return sManagerInstance;
        }

        public static Sprite Add(Sprite.SpriteName name, Image.ImageName imageName, float x, float y, float width, float height)
        {
            SpriteManager s = GetSpriteManager();
            Debug.Assert(s != null);
            Image img = ImageManager.Find(imageName);

            Debug.Assert(img != null);

            Sprite spriteNode = (Sprite)s.ManagerAddFront();
            Debug.Assert(spriteNode != null);

            spriteNode.Set(name, img, x, y, width, height);
            return spriteNode;
        }
        
        public static Sprite Find(Sprite.SpriteName name)
        {
            SpriteManager sm = GetSpriteManager();
            Debug.Assert(sm != null);
            sm.spriteInstance.name = name;
            Sprite foundImage = (Sprite)sm.ManagerFind(sm.spriteInstance);
            return foundImage;
        }

        public static void Print()
        {
            SpriteManager sM = GetSpriteManager();
            sM.ManagerPrint();
        }

        public static void PrintSimple()
        {
            SpriteManager sM = GetSpriteManager();
            sM.ManagerPrintSimple();
        }
        
        
        protected override bool Compare(NodeBase nodeA, NodeBase nodeB)
        {
            Debug.Assert(nodeA != null);
            Debug.Assert(nodeB != null);
            Sprite spA = (Sprite)nodeA;
            Sprite spB = (Sprite)nodeB;
            bool cmp = false;
            if (spA.name == spB.name)
            {
                cmp = true;
            }
            return cmp;
        }

        protected override NodeBase CreateNode()
        {
            // LTN - SpriteManager
            NodeBase n = new Sprite();
            Debug.Assert(n != null);
            return n;
        }
    }
}
