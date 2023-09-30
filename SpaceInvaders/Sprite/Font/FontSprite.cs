using System.Diagnostics;
using System;

namespace SpaceInvaders
{
    class FontSprite:BaseSprite
    {
        
        public Azul.Sprite azulSprite;
        public Azul.Rect azulRect;
        public Azul.Color textColor;

        private string displayMsg;
        public Glyph.GlyphName glyphName;
        public Font.FontName name;

        //location
        public float x;
        public float y;

        public FontSprite() : base()
        {
            azulSprite = new Azul.Sprite();
            azulRect = new Azul.Rect();
            textColor = new Azul.Color(1, 1, 1);

            displayMsg = null;
            glyphName = Glyph.GlyphName.Uninitialized;
        }

        public void Set(Font.FontName name, string msg, Glyph.GlyphName glyphName, float xStart, float yStart)
        {
            Debug.Assert(msg != null);
            displayMsg = msg;

            x = xStart;
            y = yStart;

            this.name = name;
            this.glyphName = glyphName;

            Debug.Assert(textColor != null);
            //make it white
            textColor.Set(1, 1, 1);
        }

        public void SetColor(float r, float g, float b, float alpha = 1.0f)
        {
            Debug.Assert(textColor != null);
            textColor.Set(r, g, b, alpha);
        }

        public void UpdateMessage(string msg)
        {
            Debug.Assert(msg != null);
            displayMsg = msg;
        }

        public override void Update()
        {
            Debug.Assert(azulSprite != null);
        }

        public override void Render()
        {
            Debug.Assert(azulRect != null);
            Debug.Assert(azulSprite != null);
            Debug.Assert(textColor != null);
            Debug.Assert(displayMsg != null);
            Debug.Assert(displayMsg.Length > 0);

            float tmpX = x;
            float tmpY = y;

            float endX = x;
            for (int i = 0; i < displayMsg.Length; i++)
            {
                int key = Convert.ToByte(displayMsg[i]);
                Glyph glyph = GlyphManager.ArrayFind(key);
                Debug.Assert(glyph != null);
                tmpX = endX + glyph.GetAzulRect().width / 2; //extends the rect by each glyph
                this.azulRect.Set(tmpX, tmpY, glyph.GetAzulRect().width, glyph.GetAzulRect().height);

                //swap the textures
                azulSprite.Swap(glyph.GetAzulTexture(), glyph.GetAzulRect(), this.azulRect, this.textColor);

                azulSprite.Update();
                azulSprite.Render();
                endX = glyph.GetAzulRect().width / 2 + tmpX; //extends it for the next glyph char
            }


        }

        public override object GetData()
        {
            return this.name;
        }

        public override void Print()
        {
            Debug.WriteLine("Font Sprite: {0}, {1}", name, GetHashCode());
        }

        public override void Clear()
        {
            Clean();
        }

        private void Clean()
        {
            Debug.Assert(azulRect != null);
            Debug.Assert(azulSprite != null);
            Debug.Assert(textColor != null);
            Debug.Assert(displayMsg != null);

            azulRect.Set(0, 0, 0, 0);
            textColor.Set(1, 1, 1);
            displayMsg = null;
            glyphName = Glyph.GlyphName.Uninitialized;
            x = 0.0f;
            y = 0.0f;

        }

        public string getFontString()
        {
            return this.displayMsg;
        }
    }
}
