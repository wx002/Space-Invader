using System.Diagnostics;


namespace SpaceInvaders
{
    class BrickColumn : Composite
    {
        public BrickColumn(GameObject.GOName name, Sprite.SpriteName spriteName, float x, float y) : 
            base(name, spriteName)
        {
            this.x = x;
            this.y = y;
        }

        ~BrickColumn() { }
        public override void Accept(CollisionVisitor other)
        {
            other.VisitBrickColumn(this);
        }

        public override void VisitBomb(Bomb bomb)
        {
            GameObject g = (GameObject)CompositeIteratorForward.GetChildNode(this);
            CollisionPair.Collide(bomb, g);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs ShieldColumn
            GameObject pGameObj = (GameObject)CompositeIteratorForward.GetChildNode(this);
            CollisionPair.Collide(m, pGameObj);
        }

        public override void VisitColumn(AlienColumn b)
        {
            CollisionPair.Collide((GameObject)CompositeIteratorForward.GetChildNode(b), this);
        }

        public override void Update()
        {
            UpdateBondingBox(this);
            base.Update();
        }

        public override void Print()
        {
            Debug.WriteLine("\nBrick Column:");
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
        }
    }
}
