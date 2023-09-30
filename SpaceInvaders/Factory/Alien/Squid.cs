﻿using System.Diagnostics;
namespace SpaceInvaders
{
    class Squid: AlienBase
    {
        public Squid(Sprite.SpriteName name, float x, float y):base(GameObject.GOName.Squid, name, x, y){}

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
            other.VisitSquid(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Missile vs Crab
            GameObject pGameObj = (GameObject)CompositeIteratorForward.GetChildNode(m);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitBrickColumn(BrickColumn b)
        {
            GameObject pGameObj = (GameObject)CompositeIteratorForward.GetChildNode(b);
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

        public override void VisitBrick(Brick b)
        {
            CollisionPair cp = CollisionPairManager.GetActiveCollisionPair();
            cp.SetCollisionPair(this, b);
            cp.NotifyListeners();
        }
    }
}
