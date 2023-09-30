using System.Diagnostics;

namespace SpaceInvaders
{
    class Shield: Composite
    {
        public Shield(GameObject.GOName name, Sprite.SpriteName spriteName, float x, float y) : base(name, spriteName) 
        {
            this.x = x;
            this.y = y;
        }

        ~Shield() { }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitShield(this);
        }

        public override void ComponentPrint()
        {
            base.Print();
        }

        public override void VisitMissileGroup(MissileGroup missileGroup)
        {
            GameObject g = (GameObject)CompositeIteratorForward.GetChildNode(this);
            CollisionPair.Collide(missileGroup, g);
        }

        public override void Update()
        {
            UpdateBondingBox(this);
            base.Update();
        }
    }
}
