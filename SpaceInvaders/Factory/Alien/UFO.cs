using System.Diagnostics;

namespace SpaceInvaders
{
    class UFO: AlienBase
    {
        public float moveSpeed;
        public UFO(Sprite.SpriteName name, float x, float y):base(GameObject.GOName.UFO, name, x, y){
            this.moveSpeed = 1.0f;
        }

        public override void Move(float x, float y)
        {
            this.x += x;
            this.y += y;
        }

        public override void Update()
        {
            base.Update();
            this.x += moveSpeed;
        }

        public override void ComponentPrint()
        {
            base.Print();
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitUFO(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Missile vs Crab
            GameObject pGameObj = (GameObject)CompositeIteratorForward.GetChildNode(m);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Missile vs Crab
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
            cp.SetCollisionPair(this, b);
            cp.NotifyListeners();
        }

        public override void Remove()
        {
            this.GetCollisionObject().collisionRect.Set(0, 0, 0, 0);
            base.Update(); //update the missile

            //get the parent
            GameObject UFOGroup = (GameObject)this.parent;
            UFOGroup.Update(); //update the parent
            base.Remove();
        }
    }
}
