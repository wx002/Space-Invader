using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class ShipMoveState
    {
        public abstract void Handle(Ship pShip);
        public abstract void MoveRight(Ship pShip);
        public abstract void MoveLeft(Ship pShip);
    }
}
