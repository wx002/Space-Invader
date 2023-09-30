using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class ShipMissileState
    {
        public abstract void Handle(Ship pShip);
        public abstract void ShootMissile(Ship pShip);
    }
}
