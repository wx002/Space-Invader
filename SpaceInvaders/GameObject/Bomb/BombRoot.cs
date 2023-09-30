using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BombRoot : Composite
    {
        public BombRoot(GameObject.GOName name, Sprite.SpriteName spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.GetCollisionObject().collisionBox.SetColor(1, 1, 1);
        }

        ~BombRoot()
        {
        }

        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitBombRoot(this);
        }

        public override void VisitMissileGroup(MissileGroup missileGroup)
        {
            GameObject g = (GameObject)CompositeIteratorForward.GetChildNode(missileGroup);
            CollisionPair.Collide(g, this); //test for collision
        }

        public override void VisitMissile(Missile missile)
        {
            GameObject g = (GameObject)CompositeIteratorForward.GetChildNode(this);
            CollisionPair.Collide(missile, g);
        }

        public override void Update()
        {
            // Go to first child
            UpdateBondingBox(this);
            base.Update();
        }
    }
}