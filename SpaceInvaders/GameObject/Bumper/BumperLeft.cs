using System.Diagnostics;

namespace SpaceInvaders
{
    class BumperLeft: BumperCategory
    {
        public BumperLeft(GOName name, Sprite.SpriteName spriteName, float x, float y, float width, float height)
            : base(name, spriteName, x, y, BumperTypes.Left)
        {
            this.GetCollisionObject().collisionRect.Set(x, y, width, height);
            this.x = x;
            this.y = y;

            this.GetCollisionObject().collisionBox.SetColor(1, 1, 0);
        }

        ~BumperLeft()
        {
        }
        public override void Accept(CollisionVisitor other)
        {      
            other.VisitBumperLeft(this);
        }

        public override void ComponentPrint()
        {
            base.Print();
        }

        public override void Move(float x, float y)
        {
            this.x += x;
            this.y += y;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
