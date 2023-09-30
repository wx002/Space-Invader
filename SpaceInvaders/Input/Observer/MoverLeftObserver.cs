using System.Diagnostics;

namespace SpaceInvaders
{
    class MoverLeftObserver: InputObserver
    {
        public override void Notify()
        {
            //Debug.WriteLine("Move Left!");
            Ship ship = ShipManager.GetShip();
            ship.MoveLeft();

        }

        public override void Print()
        {
            base.DLinkPrint();
        }
    }
}
