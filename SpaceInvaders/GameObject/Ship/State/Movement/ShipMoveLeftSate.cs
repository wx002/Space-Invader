using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipMoveLeftState : ShipMoveState
    {
        
        public override void Handle(Ship pShip)
        {
            pShip.SetShipState(ShipManager.MoveStates.MoveBoth);
        }

        public override void MoveLeft(Ship pShip)
        {
            pShip.x -= pShip.shipSpeed;
            this.Handle(pShip);
        }

        public override void MoveRight(Ship pShip)
        {
            
        }
    }
}
