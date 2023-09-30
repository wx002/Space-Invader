using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipMoveObserver : CollisionObserver
    {
        public override void Notify()
        {
            Ship ship = ShipManager.GetShip();
            BumperCategory cat = (BumperCategory)this.collisionSubject.subject1;
            if(cat.GetBumperType() == BumperCategory.BumperTypes.Left)
            {
                Debug.WriteLine("Collided with Left Bumper, disable left movement");
                ship.SetShipState(ShipManager.MoveStates.MoveRight);
                ship.PrintCurrentState();
            }else if(cat.GetBumperType() == BumperCategory.BumperTypes.Right)
            {
                Debug.WriteLine("Collided with Right Bumper, disable Right movement");
                ship.SetShipState(ShipManager.MoveStates.MoveLeft);
                ship.PrintCurrentState();
            }
            
            
        }

        public override void Print()
        {
            
        }
    }
}
