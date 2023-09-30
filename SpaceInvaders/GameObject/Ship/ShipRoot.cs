using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipRoot:Composite
    {
        public ShipRoot(GameObject.GOName name, Sprite.SpriteName spriteName, float x, float y):
            base(name, spriteName)
        {
            this.x = x;
            this.y = y;

            this.GetCollisionObject().collisionBox.SetColor(0, 0, 1);
        }

        ~ShipRoot() { }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitShipRoot(this);
        }

        public override void Update()
        {
            UpdateBondingBox(this);
            base.Update();
        }

        public override void VisitBumperRoot(BumperRoot bump)
        {
            GameObject g = (GameObject)CompositeIteratorForward.GetChildNode(this);
            CollisionPair.Collide(bump, g);
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


    }
}
