using System.Diagnostics;

namespace SpaceInvaders
{
    class ProxySprite: BaseSprite
    {
        public enum ProxyName
        {
            Proxy,
            Null,
            Uninitialized
        }

        // Data fields
        public ProxyName name;
        public float locX;
        public float locY;
        public float width;
        public float height;
        public Sprite spriteObj;


        public ProxySprite():base()
        {
            this.Clean();
        }

        protected ProxySprite(ProxyName n): base()
        {
            this.name = n;
            this.Clean();
        }

        public void Set(Sprite.SpriteName name)
        {
            this.name = ProxyName.Proxy;

            locX = 0.0f;
            locY = 0.0f;

            this.width = 1.0f;
            this.height = 1.0f;

            spriteObj = SpriteManager.Find(name);
            Debug.Assert(spriteObj != null);
        }

        public override void Print()
        {
            Debug.WriteLine("{0}: Hashcode: {1}", this.name, this.GetHashCode());
            if(spriteObj != null)
            {
                spriteObj.Print();
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
            UpdateRealSprite();
            spriteObj.Update();
            spriteObj.Render();
        }

        public override void Update()
        {
            UpdateRealSprite();
            spriteObj.Update();
        }

        public override void Clear()
        {
            this.Clean();
        }


        public override object GetData()
        {
            return name;
        }


        private void Clean()
        {
            name = ProxyName.Uninitialized;
            locX = 0.0f;
            locY = 0.0f;

            spriteObj = null;
        }

        private void UpdateRealSprite()
        {
            spriteObj.imageX = this.locX;
            spriteObj.imageY = this.locY;

            spriteObj.width = this.width;
            spriteObj.height = this.height;
        }
    }
}
