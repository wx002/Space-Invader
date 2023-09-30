using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipDeadState : ShipMoveState
    {
        public override void Handle(Ship pShip)
        {
            //pShip.SetShipState(ShipManager.States.Ready);
        }

        public override void MoveLeft(Ship pShip)
        {
        }

        public override void MoveRight(Ship pShip)
        {
        }
    }
}
