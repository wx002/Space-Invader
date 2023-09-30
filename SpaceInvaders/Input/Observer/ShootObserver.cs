using System.Diagnostics;

namespace SpaceInvaders
{
    class ShootObserver: InputObserver
    {
        public override void Notify()
        {
            Ship ship = ShipManager.GetShip();
            ship.ShootMissile();
        }

        public override void Print()
        {
            base.DLinkPrint();
        }
    }
}
