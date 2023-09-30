using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipMoveRightState : ShipMoveState
    {
        
        public override void Handle(Ship pShip)
        {
            pShip.SetShipState(ShipManager.MoveStates.MoveBoth);
        }

        public override void MoveLeft(Ship pShip)
        {
            
        }

        public override void MoveRight(Ship pShip)
        {
            pShip.x += pShip.shipSpeed;
            this.Handle(pShip);
        }
    }
}
