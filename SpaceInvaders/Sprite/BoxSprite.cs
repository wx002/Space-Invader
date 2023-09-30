using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BoxSprite: BaseSprite
    {
        public enum BoxName
        {
            Box1,
            Box2,
            Box3,
            Box4,

            RedBox,
            GreenBox,
            YellowBox,
            WhiteBox,

            Uninitialized
        }

        public BoxName name;
        public Azul.Color boxColor;
        private Azul.SpriteBox azulSpriteBox;
        private Azul.Rect azulRect = new Azul.Rect();

        public float imageX;
        public float imageY;
        public float width;
        public float height;
        public float angle;

        public BoxSprite(): base()
        {
            this.name = BoxName.Uninitialized;
            Debug.Assert(azulRect != null);

            azulRect.Set(0, 0, 1, 1);
            // LTN - BoxSprite
            boxColor = new Azul.Color(1, 1, 1);
            // LTN - BoxSprite
            azulSpriteBox = new Azul.SpriteBox(azulRect, boxColor);

            this.imageX = azulSpriteBox.x;
            this.imageY = azulSpriteBox.y;
            this.width = azulSpriteBox.sx;
            this.height = azulSpriteBox.sy;
            this.angle = azulSpriteBox.angle;
        }

        public void SetRect(float x, float y, float width, float height)
        {
            this.SetData(this.name, x, y, width, height, null);
        }

        public void SetData(BoxName name, float imageX, float imageY, float width, float height, Azul.Color color)
        {
            Debug.Assert(boxColor != null);
            Debug.Assert(azulSpriteBox != null);
            Debug.Assert(azulRect != null);
            azulRect.Set(imageX, imageY, width, height);

            this.name = name;
            if( color == null)
            {
                this.boxColor.Set(255, 0, 0);
            }
            else
            {
                this.boxColor.Set(color);
            }

            azulSpriteBox.Swap(azulRect, boxColor);

            this.imageX = azulSpriteBox.x;
            this.imageY = azulSpriteBox.y;
            this.width = azulSpriteBox.sx;
            this.height = azulSpriteBox.sy;
            this.angle = azulSpriteBox.angle;
        }


        public override void Clear()
        {
            this.Reset();
        }

        public void SwapColor(float red, float green, float blue)
        {
            Debug.Assert(red >= 0);
            Debug.Assert(green >= 0);
            Debug.Assert(blue >= 0);
            boxColor.Set(red, green, blue);
            azulSpriteBox.SwapColor(boxColor);
        }

        public void SetColor(float red, float green, float blue, float alpha = 0)
        {
            Debug.Assert(boxColor != null);
            Debug.Assert(red >= 0);
            Debug.Assert(green >= 0);
            Debug.Assert(blue >= 0);
            boxColor.Set(red, green, blue, alpha);
            azulSpriteBox.SwapColor(boxColor);
        }

        private void Reset()
        {
            this.name = BoxName.Uninitialized;
            boxColor.Set(1, 1, 1);
            this.imageX = 0.0f;
            this.imageY = 0.0f;
            this.width = 1.0f;
            this.height = 1.0f;
            this.angle = 0.0f;
        }

        public override void Update()
        {
            this.azulSpriteBox.x = this.imageX;
            this.azulSpriteBox.y = this.imageY;
            this.azulSpriteBox.sx = this.width;
            this.azulSpriteBox.sy = this.height;
            this.azulSpriteBox.angle = this.angle;

            this.azulSpriteBox.Update();
        }

        public override void Render()
        {
            azulSpriteBox.Render();
        }

        public override object GetData()
        {
            return this.name;
        }

        public override void Print()
        {
            Debug.WriteLine("Sprite Name: {0}, Hashcode: {1}", this.name, this.GetHashCode());
            Debug.WriteLine("Azul Sprite Box Hashcode: {0}", this.azulSpriteBox.GetHashCode());
            Debug.WriteLine("Color(rgb): ({0},{1},{2})", boxColor.red, boxColor.green, boxColor.blue);
            Debug.WriteLine("(Image X, Image Y): ({0}, {1})", imageX, imageY);
            Debug.WriteLine("(Width, Height): ({0}, {1})", width, height);
            Debug.WriteLine("Angle: {0}", angle);
            this.DLinkPrint();
        }
    }
}
