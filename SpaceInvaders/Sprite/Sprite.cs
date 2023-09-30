using System.Diagnostics;
namespace SpaceInvaders
{
    class Sprite: BaseSprite
    {
        public enum SpriteName
        {
            HotPink,

            GreenPig,

            RedBird,
            BlueBird,
            YellowBird,
            GreenBird,
            WhiteBird,
            BlackBird,

            GreenGhost,
            BlueGhost,
            WhiteGhost,
            RedGhost,

            Crab,
            Octupus,
            Squid,

            Uninitialized,
            NULL,
            Brick,
            BrickLeftTop0,
            BrickLeftTop1,
            BrickLeftBottom,
            BrickRightTop0,
            BrickRightTop1,
            BrickRightBottom,
            Ship,
            Missile,
            AlienDeath,
            GreenUFO,
            GreenCrab,
            GreenOctupus,
            UFO,
            BombStraight,
            BombZigZag,
            BombDagger,
            GreenSquid
        }

        public float imageX;
        public float imageY;
        public float width;
        public float height;
        public float angle;


        public SpriteName name;
        public Image pImage;
        public Azul.Color color;
        private Azul.Sprite azulSprite;
        private Azul.Rect azulRect;

        public Sprite()
        {
            imageX = 0.0f;
            imageY = 0.0f;
            width = 1.0f;
            height = 1.0f;
            angle = 0.0f;
            name = SpriteName.Uninitialized;
            pImage = null;
            //LTN - Sprite
            color = new Azul.Color();
            //LTN - Sprite
            azulSprite = new Azul.Sprite();
            //LTN - Sprite
            azulRect = new Azul.Rect();
        }

        public override void Update()
        {
            azulSprite.x = imageX;
            azulSprite.y = imageY;
            azulSprite.sx = width;
            azulSprite.sy = height;
            azulSprite.angle = angle;

            azulSprite.Update();
        }

        public override void Render()
        {
            azulSprite.Render();
        }

        public void Set(SpriteName _name, Image img, float imageX, float imageY, float width, float height)
        {
            Debug.Assert(img != null);
            Debug.Assert(azulRect != null);
            Debug.Assert(azulSprite != null);
            Debug.Assert(color != null);

            pImage = img;
            name = _name;
            azulRect.Set(imageX,imageY,width, height);

            color.Set(1.0f, 1.0f, 1.0f, 1.0f);
            azulSprite.Swap(pImage.texture.azulTexture, pImage.pRect, azulRect, color);
            azulSprite.Update();

            this.imageX = azulSprite.x;
            this.imageY = azulSprite.y;
            this.width = azulSprite.sx;
            this.height = azulSprite.sy;
            angle = azulSprite.angle;
        }

        private void ClearSprite()
        {
            Debug.Assert(color != null);
            Debug.Assert(azulSprite != null);
            imageX = 0.0f;
            imageY = 0.0f;
            width = 1.0f;
            height = 1.0f;
            angle = 0.0f;
            name = SpriteName.Uninitialized;
            pImage = null;
            color.Set(1.0f, 1.0f, 1.0f, 1.0f);
            azulRect.Set(0.0f, 0.0f, 1.0f, 1.0f);
            Image hotpinkImage = ImageManager.Find(Image.ImageName.HotPink);
            Debug.Assert(hotpinkImage != null);

            azulSprite.Swap(hotpinkImage.texture.azulTexture, hotpinkImage.pRect, azulRect, color);
            azulSprite.Update();
        }

        public override object GetData()
        {
            return name;
        }

        public override void Clear()
        {
            this.ClearSprite();
        }

        public void SwapImage(Image image)
        {
            Debug.Assert(this.azulSprite != null);
            Debug.Assert(image != null);

            this.pImage = image;
            this.azulSprite.SwapTexture(pImage.texture.azulTexture);
            this.azulSprite.SwapTextureRect(pImage.pRect);
        }

        public void SwapColor(float r, float g, float b)
        {
            Azul.Color c = new Azul.Color(r, g, b);
            azulSprite.SwapColor(c);
        }

        public override void Print()
        {
            Debug.WriteLine("Sprite Name: {0}, Hashcode: {1}", this.name, this.GetHashCode());
            Debug.WriteLine("Azul Sprite Hashcode: {0}", this.azulSprite.GetHashCode());
            Debug.WriteLine("(Image X, Image Y): ({0}, {1})", imageX, imageY);
            Debug.WriteLine("(Width, Height): ({0}, {1})", width, height);
            Debug.WriteLine("Angle: {0}", angle);
            this.DLinkPrint();
        }

        public Azul.Rect GetAzulRect()
        {
            Debug.Assert(azulRect != null);
            return azulRect;
        }
    }
}
