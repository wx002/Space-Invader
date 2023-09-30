﻿using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipMoveBothState : ShipMoveState
    {
        
        public override void Handle(Ship pShip)
        {
            
        }

        public override void MoveLeft(Ship pShip)
        {
            pShip.x -= pShip.shipSpeed;
        }

        public override void MoveRight(Ship pShip)
        {
            pShip.x += pShip.shipSpeed;
        }
    }
}
