using System.Diagnostics;

namespace SpaceInvaders
{
    class WallTop : WallType
    {
        public WallTop(GameObject.GOName name, Sprite.SpriteName spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, posX, posY, WallType.Types.Top)
        {
            this.GetCollisionObject().collisionRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;

            this.GetCollisionObject().collisionBox.SetColor(0, 0, 0);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitWallTop(this);
        }

        public override void ComponentPrint()
        {
            base.Print();
        }

        public override void VisitMissileGroup(MissileGroup missileGroup)
        {
            GameObject g = (GameObject)CompositeIteratorForward.GetChildNode(missileGroup);
            CollisionPair.Collide(g, this);
        }

        public override void VisitMissile(Missile m)
        {
            CollisionPair cp = CollisionPairManager.GetActiveCollisionPair();
            cp.SetCollisionPair(m, this);
            cp.NotifyListeners();
        }

        public override void Move(float x, float y)
        {
            Debug.Assert(false);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
