using System.Diagnostics;

namespace SpaceInvaders
{
    class ImageNode: DLink
    {
        private Image img;

        public ImageNode(Image i):base()
        {
            Debug.Assert(i != null);
            img = i;
        }


        public override void Print()
        {
            Debug.WriteLine("Image Name: {0}", img.name);
            base.DLinkPrint();
        }

        public override void Clear()
        {
            img = null;
            base.Clear();
        }

        public Image.ImageName getName()
        {
            return img.name;
        }

        public Image GetImage()
        {
            return this.img;
        }
        
    }
}
