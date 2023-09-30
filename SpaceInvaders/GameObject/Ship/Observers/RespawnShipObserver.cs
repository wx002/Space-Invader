using System.Diagnostics;

namespace SpaceInvaders
{
    class RespawnShipObserver: CollisionObserver
    {
        private GameObject ship;

        public RespawnShipObserver()
        {
            ship = null;
        }

        public RespawnShipObserver(RespawnShipObserver shipObs)
        {
            Debug.Assert(shipObs.ship != null);
            this.ship = shipObs.ship;
        }

        public void Notify2()
        {
            this.ship = (Ship)this.collisionSubject.subject2;

            //Debug.WriteLine("Missile Removal: --> delete Missile {0}", this.missile);
            if (this.ship.isDead == true)
            {
                this.ship.isDead = false;
                //we remove, and then added back
                RespawnShipObserver obs = new RespawnShipObserver(this);
                DelayObjectManager.AddObserver(obs);
            }
        }

        public override void Notify()
        {
            Debug.WriteLine("Notified!");
            this.ship = (Ship)this.collisionSubject.subject2;
            if (this.ship.isDead == true)
            {
                if (GameSession.p1NumLives >= 0)
                {
                    RespawnShipObserver obs = new RespawnShipObserver(this);
                    DelayObjectManager.AddObserver(obs);
                }
            }
        }

        public override void Execute()
        {
            if(GameSession.p1NumLives > 0)
            { 
                GameSession.p1NumLives -= 1;
                Font liveUpdate = FontManager.Find(Font.FontName.NumLivesP1);
                liveUpdate.UpdateMessage(GameSession.p1NumLives.ToString());
                ShipManager.ActivateShip();
                //ship.SetShipState(ShipManager.MissileStates.Ready);
                //ship.SetShipState(ShipManager.MoveStates.MoveBoth);
            }
            else
            {
                SceneContext.SetSceneState(SceneContext.SceneTypes.GameOver);
            }
        }

        public override void Print()
        {
            Debug.Assert(false);
        }
    }
}
