using System.Diagnostics;


namespace SpaceInvaders
{
    abstract class GameObject: Component
    {
        public enum GOName
        {
            AlienGrid,
            AlienColumn,

            Squid,
            Crab,
            Octupus,

            RedBird,
            GreenBird,
            YellowBird,
            WhileBird,

            NULL,

            Uninitialized,
            
            Brick,
            BrickGroup,
            Shield1,
            Shield2,
            Shield3,
            Shield4,

            Bomb,
            ZigZagBomb,
            StraightBomb,
            DaggerBomb,
            MissileGroup,
            ShieldColumn0,
            ShieldColumn1,
            ShieldColumn2,
            ShieldColumn3,
            ShieldColumn4,
            ShieldColumn5,
            ShieldColumn6,
            ShieldColumn7,
            WallGroup,
            LeftWall,
            RightWall,
            Missile,
            Ship,
            ShipRoot,
            TopWall,
            BumperRoot,
            BumperLeft,
            GreenSquid,
            GreenOctupus,
            GreenCrab,
            UFO,
            GreenUFO,
            BombRoot,
            BumperRight,
            UFOBox,
            UFOColumn,
            LeftTop1,
            LeftTop0,
            RightTop1,
            RightTop0,
            Eraser
        }

        public GOName name;
        public Sprite.SpriteName spriteName;
        public float x;
        public float y;
        public ProxySprite proxySprite;
        private CollisionObject collisionObj;
        public bool isDead;

        protected GameObject(Container type, GOName n, Sprite.SpriteName spriteName):base(type)
        {
            name = n;
            this.spriteName = spriteName;
            isDead = false;
            proxySprite = ProxySpriteManager.Find(spriteName);
            Debug.Assert(proxySprite != null);

            collisionObj = new CollisionObject(proxySprite);
            Debug.Assert(collisionObj != null);
            x = 0.0f;
            y = 0.0f;
            //this.Print();
        }

        protected GameObject(Container type, GOName n, Sprite.SpriteName name, float x, float y):base(type)
        {
            this.name = n;
            this.spriteName = name;
            isDead = false;
            this.x = x;
            this.y = y;

            //create the proxy
            proxySprite = ProxySpriteManager.Add(name);
            collisionObj = new CollisionObject(proxySprite);
            Debug.Assert(collisionObj != null);
        }


        public virtual void Update()
        {
            //check proxy sprite is not null, important
            Debug.Assert(this.proxySprite != null);
            this.proxySprite.locX = x;
            this.proxySprite.locY = y;

            Debug.Assert(collisionObj != null);
            this.collisionObj.UpdatePosition(this.x, this.y);
            Debug.Assert(collisionObj.collisionBox != null);
            this.collisionObj.collisionBox.Update();
        }

        public override void Print()
        {
            Debug.WriteLine("----GameObject Print----");
            Debug.WriteLine("Name: {0}, Hash: {1}", name, this.GetHashCode());
            if(proxySprite == null)
            {
                Debug.WriteLine("Proxy Sprite is null, Real Sprite is null");
            }
            else
            {
                Debug.WriteLine("Proxy Sprite Name: {0}", proxySprite.GetData());
                if (proxySprite.spriteObj != null)
                {
                    Debug.WriteLine("Real Sprite: {0}", proxySprite.spriteObj.GetData());
                }
            }
            Debug.WriteLine("Location: {0},{1}", x, y);
            base.DLinkPrint();
        }

        public override object GetData()
        {
            return name;
        }

        public void ActivateSprite(SpriteBatchNode batch)
        {
            Debug.Assert(batch != null); 
            batch.AddNodeToBatch(this.proxySprite);
        }

        public void ActivateSprite(SpriteBatchNode.GroupTypes name)
        {
            SpriteBatchNode batch = SpriteBatchManager.Find(name);
            Debug.Assert(batch != null);
            batch.AddNodeToBatch(this.proxySprite);
        }

        public void ActivateCollisionSprite(SpriteBatchNode.GroupTypes name)
        {
            SpriteBatchNode batch = SpriteBatchManager.Find(name);
            Debug.Assert(batch != null);
            Debug.Assert(collisionObj != null);
            batch.AddNodeToBatch(this.collisionObj.collisionBox);
        }

        public void ActivateCollisionSprite(SpriteBatchNode batch)
        {
            Debug.Assert(batch != null);
            Debug.Assert(collisionObj != null);
            batch.AddNodeToBatch(collisionObj.collisionBox);
        }

        protected void UpdateBondingBox(Component headNode)
        {
            GameObject walk = (GameObject)headNode;
            CollisionRect totalRect = this.collisionObj.collisionRect;

            //get child within composite
            walk = (GameObject)CompositeIteratorForward.GetChildNode(walk);
            if (walk != null)
            {
                totalRect.Set(walk.collisionObj.collisionRect);

                //loop within the composite object's pool
                while (walk != null)
                {
                    totalRect.Union(walk.collisionObj.collisionRect);
                    walk = (GameObject)CompositeIteratorForward.GetSiblingNode(walk);
                }

                this.x = collisionObj.collisionRect.x;
                this.y = collisionObj.collisionRect.y;
            }
        }

        public void SetCollisionBoxColor(float r, float g, float b)
        {
            Debug.Assert(collisionObj != null);
            Debug.Assert(collisionObj.collisionBox != null);
            collisionObj.collisionBox.SetColor(r, g, b);
        }

        public CollisionObject GetCollisionObject()
        {
            Debug.Assert(collisionObj != null);
            return collisionObj;
        }

        public virtual void Remove()
        {
            Debug.Assert(this.proxySprite != null);
            SpriteNode spriteNode = this.proxySprite.GetSpriteNode();
            Debug.Assert(spriteNode != null);
            SpriteBatchManager.Remove(spriteNode);
            //remove collision sprite
            Debug.Assert(this.collisionObj != null);
            Debug.Assert(this.collisionObj.collisionBox != null);
            spriteNode = this.collisionObj.collisionBox.GetSpriteNode();
            Debug.Assert(spriteNode != null);
            SpriteBatchManager.Remove(spriteNode);
            GameObjectNodeManager.Remove(this);
            //add this back for recycle
            GhostManager.Add(this);

        }

        public override void Revive()
        {
            this.isDead = false;
            this.proxySprite = ProxySpriteManager.Add(this.spriteName);
            this.collisionObj = new CollisionObject(this.proxySprite);
            Debug.Assert(this.collisionObj != null);
            base.Revive();
        }

        ~GameObject() { }
    }
}
