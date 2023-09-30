using System.Diagnostics;


namespace SpaceInvaders
{
    class RemoveBombObserverMissile : CollisionObserver
    {
        private GameObject alienBomb;

        public RemoveBombObserverMissile()
        {
            alienBomb = null;
        }

        public RemoveBombObserverMissile(RemoveBombObserverMissile bombObs)
        {
            Debug.Assert(bombObs.alienBomb != null);
            this.alienBomb = bombObs.alienBomb;
        }

        public override void Notify()
        {
            this.alienBomb = (Bomb)this.collisionSubject.subject2;

            //Debug.WriteLine("Missile Removal: --> delete Missile {0}", this.missile);
            if (this.alienBomb.isDead == false)
            {
                this.alienBomb.isDead = true;
                //we remove, and then added back
                RemoveBombObserverMissile obs = new RemoveBombObserverMissile(this);
                DelayObjectManager.AddObserver(obs);
            }
        }

        public override void Execute()
        {
            alienBomb.Remove();
        }

        public override void Print()
        {
            Debug.Assert(false);
        }
    }
}
