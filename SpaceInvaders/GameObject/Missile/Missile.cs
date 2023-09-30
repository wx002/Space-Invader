using System.Diagnostics;

namespace SpaceInvaders
{
    class Missile: MissileCategory
    {
        public float speedDelta;
        public Missile(Sprite.SpriteName sName, float x, float y):base(GameObject.GOName.Missile, sName, x, y)
        {
            this.x = x;
            this.y = y;

            this.speedDelta = 8.0f;
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

        public override void Accept(CollisionVisitor other)
        {
            other.VisitMissile(this);
        }

        public override void Update()
        {
            this.y += speedDelta;
            base.Update();
        }

        ~Missile() { }

        public override void Remove()
        {
            //Delete

            //change the size
            this.GetCollisionObject().collisionRect.Set(0, 0, 0, 0);
            base.Update(); //update the missile

            //get the parent
            GameObject missileGroup = (GameObject)this.parent;
            missileGroup.Update(); //update the parent

            //remove it
            base.Remove(); //using the gameobject remove
            
        }

        public void Revive(float x, float y)
        {
            this.x = x;
            this.y = y;
            //this.speedDelta = 15.0f;

            base.Revive();
            this.GetCollisionObject().collisionBox.SetColor(1, 1, 0);
        }

        public void SetPosition(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
