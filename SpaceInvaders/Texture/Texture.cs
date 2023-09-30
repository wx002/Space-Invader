using System.Diagnostics;
namespace SpaceInvaders
{
    class Texture: DLink
    {
        public enum TextureName
        {
            Birds,
            HotPink,
            Alien,
            Uninitialized,
            Font,
            More,
            MorePlus
        }
        
        public Azul.Texture azulTexture;
        public TextureName name;

        public Texture()
        {
            name = TextureName.Uninitialized;
            //LTN - SpriteBatchNode
            azulTexture = new Azul.Texture();
            Debug.Assert(azulTexture != null);
        }

        public void SetData(TextureName _name, string textureName)
        {
            Debug.Assert(textureName != null);
            azulTexture.Set(textureName, Azul.Texture_Filter.NEAREST, Azul.Texture_Filter.NEAREST);
            name = _name;
        }

        private void ClearTexture()
        {
            Debug.Assert(azulTexture != null);
            azulTexture.Set("HotPink.tga", Azul.Texture_Filter.NEAREST, Azul.Texture_Filter.NEAREST);

            name = TextureName.Uninitialized;
        }

        public override object GetData()
        {
            return name;
        }

        public override void Clear()
        {
            ClearTexture();
        }

        public override void Print()
        {
            this.DLinkPrint();
        }
    }
}
