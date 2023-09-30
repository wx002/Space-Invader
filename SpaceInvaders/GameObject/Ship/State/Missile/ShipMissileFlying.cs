using System.Diagnostics;


namespace SpaceInvaders
{
    class ShipMissileFlying: ShipMissileState
    {
        public override void Handle(Ship pShip)
        {
            //Nothing
        }

        public override void ShootMissile(Ship pShip)
        {
            //Missile already launched, do nothing
        }

    }
}
