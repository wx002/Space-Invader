using System.Diagnostics;

namespace SpaceInvaders
{
    class Glyph:DLink
    {
        public enum GlyphName
        {
            Consolas36pt,

            Null_Object,
            Uninitialized
        }

        public GlyphName name;
        public Texture glyphTexture;
        public Azul.Rect azulRect;
        public int key;
        public Glyph() : base()
        {
            name = GlyphName.Uninitialized;
            glyphTexture = null;
            azulRect = new Azul.Rect();
            key = 0;
        }

        public void Set(GlyphName name, int key, Texture.TextureName textureName, float x, float y, float width, float height)
        {
            Debug.Assert(azulRect != null);
            this.name = name;

            this.glyphTexture = TextureManager.Find(textureName);
            Debug.Assert(glyphTexture != null);

            this.azulRect.Set(x, y, width, height);
            this.key = key;
        }

        public Azul.Rect GetAzulRect()
        {
            Debug.Assert(azulRect != null);
            return azulRect;
        }

        public Azul.Texture GetAzulTexture()
        {
            Debug.Assert(glyphTexture != null);
            return glyphTexture.azulTexture;
        }

        public override void Clear()
        {
            Clean();
        }

        public override object GetData()
        {
            return this.name;
        }

        public override void Print()
        {
            Debug.WriteLine("Name: {0}, Glyph Hash: {1}", name, this.GetHashCode());
            if (this.glyphTexture != null)
            {
                Debug.WriteLine("texture: {0}", this.glyphTexture.GetData());
            }
            else
            {
                Debug.WriteLine("texture: null");
            }
            Debug.WriteLine("pRect: {0}, {1}, {2}, {3}", this.azulRect.x, this.azulRect.y, this.azulRect.width, this.azulRect.height);

            base.DLinkPrint();

        }

        private void Clean()
        {
            name = GlyphName.Uninitialized;
            glyphTexture = null;
            azulRect.Set(0, 0, 1, 1);
            key = 0;
        }
    }
}
