using System.Diagnostics;
namespace SpaceInvaders
{
    class Image: DLink
    {
        public enum ImageName
        {
            HotPink,

            RedBird,
            YellowBird,
            GreenBird,
            WhiteBird,
            BlackBird,
            BlueBird,
            
            GreenPig,

            BlueGhost,
            GreenGhost,
            RedGhost,
            WhiteGhost,

            OpenCrab,
            CloseCrab,

            OpenOctupus,
            CloseOctupus,

            OpenSquid,
            CloseSquid,

            Uninitialized,
            Brick,
            BrickLeftTop0,
            BrickLeftTop1,
            BrickLeftBottom,
            BrickRightTop0,
            BrickRightTop1,
            BrickRightBottom,
            Missile,
            Ship,
            AlienDeath,
            UFO,
            BombStraight,
            BombZigZag,
            BombDagger
        }

        public ImageName name;
        public Azul.Rect pRect;
        public Texture texture;
        public static Azul.Color imageColor = new Azul.Color();

        public Image()
        {
            this.name = ImageName.Uninitialized;
            this.texture = null;
            //Long Term New - Image
            this.pRect = new Azul.Rect();
        }

        public void SetData(ImageName name, Texture srcTexture, float imageX, float imageY, float width, float height)
        {
            //input validation
            Debug.Assert(srcTexture != null);
            

            this.name = name;
            this.pRect.Set(imageX, imageY, width, height);
            this.texture = srcTexture;
        }

        private void ClearData()
        {
            Debug.Assert(this.pRect != null);
            this.name = ImageName.Uninitialized;
            this.texture = null;
            this.pRect.Clear();
        }

        public override void Clear()
        {
            this.ClearData();
        }

        public override object GetData()
        {
            return this.name;
        }

        public override void Print()
        {
            Debug.WriteLine("Name: {0}, Hashcode: {1}", this.name, this.GetHashCode());
            Debug.WriteLine("Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("Rect: [{0} {1} {2} {3}] ", this.pRect.x, this.pRect.y, this.pRect.width, this.pRect.height);
            this.DLinkPrint();
        }
    }
}
