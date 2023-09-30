using System.Diagnostics;


namespace SpaceInvaders
{
    class RemoveBombObserver : CollisionObserver
    {
        private GameObject alienBomb;

        public RemoveBombObserver()
        {
            alienBomb = null;
        }

        public RemoveBombObserver(RemoveBombObserver bombObs)
        {
            Debug.Assert(bombObs.alienBomb != null);
            this.alienBomb = bombObs.alienBomb;
        }

        public override void Notify()
        {
            this.alienBomb = (Bomb)this.collisionSubject.subject1;

            //Debug.WriteLine("Missile Removal: --> delete Missile {0}", this.missile);
            if (this.alienBomb.isDead == false)
            {
                this.alienBomb.isDead = true;
                //we remove, and then added back
                RemoveBombObserver obs = new RemoveBombObserver(this);
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
