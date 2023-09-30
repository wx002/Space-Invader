using System.Diagnostics;

namespace SpaceInvaders
{
    class FontManager: Manager
    {
        private readonly Font node;
        private static FontManager manager = null;
        
        private FontManager(int reserveSize, int growthSize):base(new DList(), new DList(), reserveSize, growthSize)
        {
            node = new Font();
        }

        public static void CreateFontManager(int reserveSize, int growthSize)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(growthSize > 0);

            Debug.Assert(manager == null);
            if (manager == null)
            {
                // LTN - FontManager
                manager = new FontManager(reserveSize, growthSize);
            }
        }

        public void Destroy()
        {
            Debug.Assert(manager != null);
            FontManager s = GetFontManager();
            s = null;
            Debug.Assert(manager == null);
        }

        private static FontManager GetFontManager()
        {
            Debug.Assert(manager != null);
            return manager;
        }

        public static Font Add(Font.FontName name, SpriteBatchNode.GroupTypes grpName, string msg, Glyph.GlyphName glyphName, float xStart, float yStart)
        {
            FontManager manager = GetFontManager();
            Debug.Assert(manager != null);

            Font node = (Font)manager.ManagerAddFront();
            Debug.Assert(node != null);

            node.Set(name, msg, glyphName, xStart, yStart);

            //add to the batchNode
            SpriteBatchNode batch = SpriteBatchManager.Find(grpName);
            Debug.Assert(batch != null);
            batch.AddNodeToBatch(node.fontSprite);
            return node;
        }

        public static Font Find(Font.FontName name)
        {
            FontManager m = GetFontManager();
            Debug.Assert(m != null);

            m.node.name = name;
            Font found = (Font)m.ManagerFind(m.node);
            return found;
        }

        public static void Remove(Font g)
        {
            Debug.Assert(g != null);
            FontManager tMan = GetFontManager();
            Debug.Assert(tMan != null);
            tMan.ManagerRemove(g);
        }

        public static void ResetFontManager()
        {
            FontManager inst = GetFontManager();
            Debug.Assert(inst != null);
            Iterator itr = inst.ManagerGetActiveIterator();
            Font walk = (Font)itr.First();
            while (!itr.IsDone())
            {
                inst.ManagerRemove(walk);
                walk = (Font)itr.Next();
            }
        }

        public static void PrintSimple()
        {
            FontManager tMan = GetFontManager();
            tMan.ManagerPrintSimple();
        }


        protected override bool Compare(NodeBase nodeA, NodeBase nodeB)
        {
            Debug.Assert(nodeA != null);
            Debug.Assert(nodeB != null);
            Font tA = (Font)nodeA;
            Font tB = (Font)nodeB;
            bool cmp = false;
            if (tA.name == tB.name)
            {
                cmp = true;
            }
            return cmp;
        }

        protected override NodeBase CreateNode()
        {
            // LTN - TextureManager
            NodeBase n = new Font();
            Debug.Assert(n != null);
            return n;
        }
    }
}
