using System.Diagnostics;

namespace SpaceInvaders
{
    class MissileGroup : Composite
    {
        
        public MissileGroup() : base()
        {
            this.name = GOName.MissileGroup;
            this.GetCollisionObject().collisionBox.SetColor(0, 0, 1);
        }

        ~MissileGroup() { }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitMissileGroup(this);
        }

        public override void Update()
        {
            UpdateBondingBox(this);
            base.Update();
        }
    }
}
