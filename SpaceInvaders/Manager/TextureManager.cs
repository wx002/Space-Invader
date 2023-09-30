using System.Diagnostics;

namespace SpaceInvaders
{
    class TextureManager : Manager
    {
        private readonly Texture texture;
        private static TextureManager tMan = null;

        public TextureManager(int reserveSize, int growthSize): base(new DList(), new DList(), reserveSize, growthSize)
        // LTN - TextureManager
        {
            // LTN - SpriteNodeManager
            // object instance for find
            // Owner: TextureManager
            texture = new Texture();
        }

        public static void CreateTextureManager(int reserveSize, int growthSize)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(growthSize > 0);

            Debug.Assert(tMan == null);
            if(tMan == null)
            {
                // LTN - TextureManager
                tMan = new TextureManager(reserveSize, growthSize);
            }

            // Init hotpink texture for null objects
            Texture hotpink = Add(Texture.TextureName.HotPink, "HotPink.tga");
            Debug.Assert(hotpink != null);
        }

        public void Destroy()
        {
            Debug.Assert(tMan != null);
            TextureManager s = GetTextureManager();
            s = null;
            Debug.Assert(tMan == null);
        }

        private static TextureManager GetTextureManager()
        {
            Debug.Assert(tMan != null);
            return tMan;
        }

        public static Texture Add(Texture.TextureName _name, string textureName)
        {
            TextureManager tMan = GetTextureManager();
            Debug.Assert(tMan != null);

            Texture tNode = (Texture)tMan.ManagerAddFront();
            Debug.Assert(tNode != null);

            tNode.SetData(_name, textureName);
            return tNode;
        }

        public static Texture Find(Texture.TextureName n)
        {
            TextureManager tMan = GetTextureManager();
            Debug.Assert(tMan != null);

            tMan.texture.name = n;
            Texture found = (Texture)tMan.ManagerFind(tMan.texture);
            return found;
        }

        public static void Remove(Texture pTexture)
        {
            Debug.Assert(pTexture != null);
            TextureManager tMan = GetTextureManager();
            Debug.Assert(tMan != null);
            tMan.ManagerRemove(pTexture);
        }
        
        
        protected override bool Compare(NodeBase nodeA, NodeBase nodeB)
        {
            Debug.Assert(nodeA != null);
            Debug.Assert(nodeB != null);
            Texture tA = (Texture)nodeA;
            Texture tB = (Texture)nodeB;
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
            NodeBase n = new Texture();
            Debug.Assert(n != null);
            return n;
        }
    }
}
