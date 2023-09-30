using System.Diagnostics;
namespace SpaceInvaders
{
    class Octupus: AlienBase
    {
        public Octupus(Sprite.SpriteName name, float x, float y):base(GameObject.GOName.Octupus, name, x, y){}

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
            other.VisitOctupus(this);
        }

        public override void VisitMissile(Missile m)
        {
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            
            CollisionPair cp = CollisionPairManager.GetActiveCollisionPair();
            cp.SetCollisionPair(m, this);
            cp.NotifyListeners();
        }

        public override void VisitBrickColumn(BrickColumn b)
        {
            GameObject pGameObj = (GameObject)CompositeIteratorForward.GetChildNode(b);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitBrick(Brick b)
        {
            CollisionPair cp = CollisionPairManager.GetActiveCollisionPair();
            cp.SetCollisionPair(this,b);
            cp.NotifyListeners();
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Missile vs Crab
            GameObject pGameObj = (GameObject)CompositeIteratorForward.GetChildNode(m);
            CollisionPair.Collide(pGameObj, this);
        }
    }
}
