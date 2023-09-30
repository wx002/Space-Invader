using System.Diagnostics;

namespace SpaceInvaders
{
    class Ship: ShipCategory
    {
        public float shipSpeed;
        private ShipMissileState missileState;
        private ShipMoveState moveState;
        public Ship(GameObject.GOName name, Sprite.SpriteName spriteName, float posX, float posY)
         : base(name, spriteName, posX, posY, ShipCategory.ShipTypes.Ship)
        {
            this.x = posX;
            this.y = posY;
            

            this.shipSpeed = 5.0f;
            this.missileState = null;
            this.moveState = null;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitShip(this);
        }

        public override void Move(float x, float y)
        {
            this.x += x;
            this.y += y;
        }

        public override void ComponentPrint()
        {
            base.Print();
        }

        public void MoveRight()
        {
            this.moveState.MoveRight(this);
        }

        public void MoveLeft()
        {
            this.moveState.MoveLeft(this);
        }

        public void ShootMissile()
        {
            this.missileState.ShootMissile(this);
        }


        public void SetShipState(ShipManager.MissileStates s)
        {
            this.missileState = ShipManager.GetShipState(s);
        }

        public void SetShipState(ShipManager.MoveStates s)
        {
            this.moveState = ShipManager.GetShipState(s);
        }


        public override void VisitBumperRoot(BumperRoot bump)
        {
            GameObject g = (GameObject)CompositeIteratorForward.GetChildNode(bump);
            CollisionPair.Collide(g, this);
        }

        public override void VisitBumperLeft(BumperLeft bump)
        {
            CollisionPair cp = CollisionPairManager.GetActiveCollisionPair();
            Debug.Assert(cp != null);

            cp.SetCollisionPair(bump, this);
            cp.NotifyListeners();
        }

        public override void VisitBumperRight(BumperRight bump)
        {
            CollisionPair cp = CollisionPairManager.GetActiveCollisionPair();
            Debug.Assert(cp != null);

            cp.SetCollisionPair(bump, this);
            cp.NotifyListeners();
        }

        public override void VisitBomb(Bomb bomb)
        {
            CollisionPair p = CollisionPairManager.GetActiveCollisionPair();
            p.SetCollisionPair(bomb, this);
            p.NotifyListeners(); //notify the observers
        }

        public void PrintCurrentState()
        {
            Debug.WriteLine("Move State: {0}", this.moveState);
            Debug.WriteLine("Missile State: {0}", this.missileState);
        }

        public void Revive(float x, float y)
        {
            this.x = x;
            this.y = y;

            this.SetShipState(ShipManager.MoveStates.MoveBoth);
            this.SetShipState(ShipManager.MissileStates.Ready);

            base.Revive();
        }

        public override void Remove()
        {
            this.SetShipState(ShipManager.MoveStates.Dead);
            this.SetShipState(ShipManager.MissileStates.MissileFlying);
            base.Remove();
        }


    }
}
