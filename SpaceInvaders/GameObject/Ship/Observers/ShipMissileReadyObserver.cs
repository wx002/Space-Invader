using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipMissileReadyObserver: CollisionObserver
    {
        public override void Notify()
        {
            Ship ship = ShipManager.GetShip();
            ship.SetShipState(ShipManager.MissileStates.Ready);
        }

        public override void Print()
        {
            Debug.Assert(false);
        }
    }
}
