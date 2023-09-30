using System.Diagnostics;

namespace SpaceInvaders
{
    class BumperRight: BumperCategory
    {
        public BumperRight(GOName name, Sprite.SpriteName spriteName, float x, float y, float width, float height)
            : base(name, spriteName, x, y, BumperTypes.Right)
        {
            this.GetCollisionObject().collisionRect.Set(x, y, width, height);
            this.x = x;
            this.y = y;

            this.GetCollisionObject().collisionBox.SetColor(1, 1, 0);
        }

        ~BumperRight()
        {
        }
        public override void Accept(CollisionVisitor other)
        {
            other.VisitBumperRight(this);
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
