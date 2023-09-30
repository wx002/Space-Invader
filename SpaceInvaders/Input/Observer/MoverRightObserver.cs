using System.Diagnostics;

namespace SpaceInvaders
{
    class MoverRightObserver: InputObserver
    {
        public override void Notify()
        {
            //Debug.WriteLine("Move Right!");
            Ship ship = ShipManager.GetShip();
            ship.MoveRight();
        }

        public override void Print()
        {
            base.DLinkPrint();
        }
    }
}
