using System.Diagnostics;

namespace SpaceInvaders
{
    class BumperRoot : Composite
    {
        public BumperRoot(GameObject.GOName name, Sprite.SpriteName sName, float x, float y):
            base(name, sName)
        {
            this.x = x;
            this.y = y;
            this.GetCollisionObject().collisionBox.SetColor(1, 1, 0);
            this.name = name;
            this.spriteName = sName;
        }

        ~BumperRoot() { }

        public override void Update()
        {
            UpdateBondingBox(this);
            base.Update();
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitBumperRoot(this);
        }
    }
}
