using System.Diagnostics;


namespace SpaceInvaders
{
    class ShieldRoot: Composite
    {
        public ShieldRoot(GameObject.GOName name, Sprite.SpriteName spriteName, float x, float y):base(name, spriteName)
        {
            this.x = x;
            this.y = y;
        }

        public Iterator GetIterator()
        {
            return this.list.GetIterator();
        }

        ~ShieldRoot() { }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitShieldRoot(this);
        }

        public override void VisitMissileGroup(MissileGroup missileGroup)
        {
            GameObject g = (GameObject)CompositeIteratorForward.GetChildNode(missileGroup);
            CollisionPair.Collide(g, this); //test for collision
        }

        public override void VisitMissile(Missile missile)
        {
            GameObject g = (GameObject)CompositeIteratorForward.GetChildNode(this);
            CollisionPair.Collide(missile, g);
        }

        public override void VisitBomb(Bomb bomb)
        {
            GameObject g = (GameObject)CompositeIteratorForward.GetChildNode(this);
            CollisionPair.Collide(bomb, g);
        }

        public override void VisitBombRoot(BombRoot bombRoot)
        {
            CollisionPair.Collide((GameObject)CompositeIteratorForward.GetChildNode(bombRoot), this);
        }

        public override void VisitAlienGrid(AlienGrid ag)
        {
            CollisionPair.Collide((GameObject)CompositeIteratorForward.GetChildNode(ag), this);
        }

        public override void VisitColumn(AlienColumn b)
        {
            CollisionPair.Collide((GameObject)CompositeIteratorForward.GetChildNode(this), b);
        }





        public override void Update()
        {
            UpdateBondingBox(this);
            base.Update();
        }
    }
}
