using System.Diagnostics;

namespace SpaceInvaders
{
    class Brick: ShieldCategory
    {
        public Brick(GOName gameName, Sprite.SpriteName name, float x, float y) : base(gameName, name, x, y, ShieldType.Brick) 
        {
            this.x = x;
            this.y = y;
        
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

        public override void ComponentPrint()
        {
            base.Print();
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitBrick(this);
        }

        public override void VisitMissile(Missile m)
        {
            CollisionPair pColPair = CollisionPairManager.GetActiveCollisionPair();
            pColPair.SetCollisionPair(m, this);
            pColPair.NotifyListeners();
        }


        public override void VisitBomb(Bomb bomb)
        {
            //bomb.Hit(); //Bomb Hit, stop moving
            CollisionPair p = CollisionPairManager.GetActiveCollisionPair();
            p.SetCollisionPair(bomb, this);
            p.NotifyListeners(); //notify the observers
        }
    }
}
