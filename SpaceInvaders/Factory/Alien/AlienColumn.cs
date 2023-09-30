using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienColumn:Composite
    {
        public AlienColumn(GameObject.GOName gameObjName, Sprite.SpriteName spriteName, float x, float y):
            base(gameObjName, spriteName) 
        {
            this.name = gameObjName;
            this.x = x;
            this.y = y;
            this.GetCollisionObject().collisionBox.SetColor(0, 0, 1);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitColumn(this);
        }

        public void Revive(float x, float y)
        {
            this.x = x;
            this.y = y;
            base.Revive();
        }

        public override void Update()
        {
            UpdateBondingBox(this);
            base.Update();
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)CompositeIteratorForward.GetChildNode(this);
            CollisionPair.Collide(m, pGameObj);
        }

        public override void VisitMissile(Missile missile)
        {
            GameObject pGameObj = (GameObject)CompositeIteratorForward.GetChildNode(this);
            CollisionPair.Collide(missile, pGameObj);
        }

        public override void VisitBrickColumn(BrickColumn b)
        {
            GameObject pGameObj = (GameObject)CompositeIteratorForward.GetChildNode(this);
            CollisionPair.Collide(b, pGameObj);
        }

        public override void Print()
        {
            Debug.WriteLine("\nAlien Column:");
            if (list != null)
            {
                Iterator itr = list.GetIterator();
                Debug.Assert(itr != null);
                GameObject walk = (GameObject)itr.First();
                while (!itr.IsDone())
                {
                    Debug.Assert(walk != null);
                    walk.Print();
                    walk = (GameObject)itr.Next();
                }
            }
            else
            {
                Debug.WriteLine("Empty Column");
            }
        }

        public void DropBomb(GameObject root, int val, float speed)
        {
            Bomb bomb = BombFactory.CreateBombByValue(val,this.x,this.y-120);
            bomb.fallSpeed = speed;
            bomb.ActivateSprite(SpriteBatchNode.GroupTypes.Bombs);
            bomb.ActivateCollisionSprite(SpriteBatchNode.GroupTypes.Boxes);
            root.Add(bomb);
        }
    }
}
