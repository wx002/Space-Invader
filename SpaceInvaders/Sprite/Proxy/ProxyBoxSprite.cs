using System.Diagnostics;


namespace SpaceInvaders
{
    class ProxyBoxSprite: BaseSprite
    {
        public enum ProxyBoxName
        {
            ProxyBox,
            Uninitialized
        }

        //Data
        public ProxyBoxName name;
        public float locX;
        public float locY;
        public BoxSprite spriteBoxObj;

        public ProxyBoxSprite(): base()
        {
            this.Clean();
        }

        public void Set(BoxSprite.BoxName name)
        {
            this.name = ProxyBoxName.ProxyBox;

            locX = 0.0f;
            locY = 0.0f;

            spriteBoxObj = BoxSpriteManager.Find(name);
            Debug.Assert(spriteBoxObj != null);
        }

        public override void Print()
        {
            Debug.WriteLine("{0}: Hashcode: {1}", this.name, this.GetHashCode());
            if (spriteBoxObj != null)
            {
                spriteBoxObj.Print();
            }
            else
            {
                Debug.WriteLine("SpriteObj = null");
            }
            Debug.WriteLine("Proxy (X,Y) = ({0},{1})", locX, locY);
            base.DLinkPrint();
        }

        public override void Render()
        {
            UpdateRealBoxSprite();
            spriteBoxObj.Update();
            spriteBoxObj.Render();
        }

        public override void Update()
        {
            UpdateRealBoxSprite();
            spriteBoxObj.Update();
        }

        public override void Clear()
        {
            Clean();
        }

        public override object GetData()
        {
            return this.name;
        }




        private void Clean()
        {
            name = ProxyBoxName.Uninitialized;
            locX = 0.0f;
            locY = 0.0f;

            spriteBoxObj = null;
        }

        private void UpdateRealBoxSprite()
        {
            spriteBoxObj.imageX = locX;
            spriteBoxObj.imageY = locY;
        }

        
    }
}
