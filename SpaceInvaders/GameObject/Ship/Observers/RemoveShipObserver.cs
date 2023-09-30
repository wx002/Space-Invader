using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveShipObserver: CollisionObserver
    {
        private GameObject ship;

        public RemoveShipObserver()
        {
            ship = null;
        }

        public RemoveShipObserver(RemoveShipObserver shipObs)
        {
            Debug.Assert(shipObs.ship != null);
            this.ship = shipObs.ship;
        }

        public override void Notify()
        {
            this.ship = (Ship)this.collisionSubject.subject2;
            Simulation.SetState(Simulation.State.Pause);
            //Debug.WriteLine("Missile Removal: --> delete Missile {0}", this.missile);
            if (this.ship.isDead == false)
            {
                this.ship.isDead = true;
                //ship.Remove();
                RemoveShipObserver obs = new RemoveShipObserver(this);
                DelayObjectManager.AddObserver(obs);
            }
        }

        public override void Execute()
        {
            Simulation.SetState(Simulation.State.Pause);
            ship.Remove();
            GameSession.UpdateLives();
            if(GameSession.GetLives(Player.P1) == 0 && GameSession.GetLives(Player.P2) == 0)
            {
                //SceneContext.SetSceneState(SceneContext.SceneTypes.GameOver);
                TimeEventManager.Add(TimeEvent.Event.SceneTrans, new SceneTransitionEvent(SceneContext.SceneTypes.GameOver), 0);
            }
            else
            {
                //SceneContext.GetSceneState().Handle();
                TimeEventManager.Add(TimeEvent.Event.SceneTrans, new SceneTransitionEvent(SceneContext.SceneTypes.Play), 0);
            }
        }

        public override void Print()
        {
            Debug.Assert(false);
        }
    }
}
