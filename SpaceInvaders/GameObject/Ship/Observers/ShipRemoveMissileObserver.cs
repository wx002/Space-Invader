using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipRemoveMissileObserver: CollisionObserver
    {
        private GameObject missile;

        public ShipRemoveMissileObserver() {
            missile = null;
        }

        public ShipRemoveMissileObserver(ShipRemoveMissileObserver m)
        {
            Debug.Assert(m.missile != null);
            this.missile = m.missile;
        }

        public override void Execute()
        {
            missile.Remove();
        }

        public override void Notify()
        {
            //Here is where the deletion occur using delayobject manager

            this.missile = (Missile)this.collisionSubject.subject1;

            //Debug.WriteLine("Missile Removal: --> delete Missile {0}", this.missile);
            if(this.missile.isDead == false)
            {
                this.missile.isDead = true;
                //we remove, and then added back
                ShipRemoveMissileObserver obs = new ShipRemoveMissileObserver(this);
                DelayObjectManager.AddObserver(obs);
            }
        }

        public override void Print()
        {
            Debug.Assert(false);
        }
    }
}
