using System.Diagnostics;
namespace SpaceInvaders
{
    class ImageManager : Manager
    {
        private readonly Image imageInstance;
        private static ImageManager imgManagerInst;

        public ImageManager(int reserveSize, int growthSize):
            base(new DList(), new DList(), reserveSize, growthSize)
        // LTN - ImageManager
        // DLists for manage the active/reserve for object pooling
        // Owner: ImageManager
        {
            // LTN - ImageManager
            // Image object instance for find
            // Owner: ImageManager
            this.imageInstance = new Image();
        }

        public static void CreateImageManager(int reserveSize, int growthSize)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(growthSize > 0);

            Debug.Assert(imgManagerInst == null);

            if(imgManagerInst == null)
            {
                imgManagerInst = new ImageManager(reserveSize, growthSize);
            }

            //init hotpink image
            Image hotpink = Add(Image.ImageName.HotPink, Texture.TextureName.HotPink, 0, 0, 128, 128);
            Debug.Assert(hotpink != null);
            Image hotpinkNULL = Add(Image.ImageName.HotPink, Texture.TextureName.HotPink, 0, 0, 0, 0);
            Debug.Assert(hotpinkNULL != null);
        }

        public static void Destroy()
        {
            Debug.Assert(imgManagerInst != null);
            ImageManager m = GetImageManager();
            m = null;
            Debug.Assert(imgManagerInst == null);
        }

        public static Image Add(Image.ImageName name, Texture.TextureName texture, float x, float y, float width, float height)
        {
            ImageManager iMan = GetImageManager();
            Debug.Assert(iMan != null);

            Image pImage = (Image)iMan.ManagerAddFront();
            Debug.Assert(pImage != null);

            Texture t = TextureManager.Find(texture);

            pImage.SetData(name, t, x, y, width, height);
            return pImage;
        }

        public static Image Find(Image.ImageName name)
        {
            ImageManager iMan = GetImageManager();
            Debug.Assert(iMan != null);
            iMan.imageInstance.name = name;
            Image foundImage = (Image)iMan.ManagerFind(iMan.imageInstance);
            return foundImage;
        }

        public static void Remove(Image img)
        {
            Debug.Assert(img != null);
            ImageManager iMan = GetImageManager();
            Debug.Assert(iMan != null);
            iMan.ManagerRemove(img);
        }

        public static void Print()
        {
            imgManagerInst.ManagerPrint();
        }

        private static ImageManager GetImageManager()
        {
            Debug.Assert(imgManagerInst != null);
            return imgManagerInst;
        }

        protected override bool Compare(NodeBase nodeA, NodeBase nodeB)
        {
            Debug.Assert(nodeA != null);
            Debug.Assert(nodeB != null);
            Image imgA = (Image)nodeA;
            Image imgB = (Image)nodeB;
            bool cmp = false;
            if (imgA.name == imgB.name)
            {
                cmp = true;
            }
            return cmp;
        }

        protected override NodeBase CreateNode()
        {
            // LTN - ImageManager
            // Nodes created for object pooling design pattern
            // Owner: ImageManager, use to fill the reserve list, so it can be used in the active
            // Nodes gets recycle upon remove from active list
            NodeBase n = new Image();
            Debug.Assert(n != null);
            return n;
        }
    }
}
