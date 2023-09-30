using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipManager
    {
        public enum MissileStates
        {
            Ready,
            MissileFlying
        }

        public enum MoveStates
        {
            MoveRight,
            MoveLeft,
            MoveBoth,
            Dead
        }
        private ShipMissileReady missileReady;
        private ShipMissileFlying missileFlying;

        private ShipMoveBothState moveBoth;
        private ShipMoveLeftState moveLeft;
        private ShipMoveRightState moveRight;


        private readonly ShipDeadState dead;

        private static ShipManager manager = null;
        private Ship ship;
        private Missile missile;

        public ShipManager()
        {
            missileReady = new ShipMissileReady();
            missileFlying = new ShipMissileFlying();

            moveBoth = new ShipMoveBothState();
            moveLeft = new ShipMoveLeftState();
            moveRight = new ShipMoveRightState();

            dead = new ShipDeadState();

            this.ship = null;
            this.missile = null;
        }

        public static void SetActiveShipManager(ShipManager man)
        {
            Debug.Assert(man != null);
            manager = man;
        }

        public static void CreateShip()
        {
            Debug.Assert(manager != null);
            manager.ship = ActivateShip();
            manager.ship.SetShipState(MissileStates.Ready);
            manager.ship.SetShipState(MoveStates.MoveBoth);
        }

        public static ShipMissileState GetShipState(MissileStates state)
        {
            ShipManager m = GetShipManager();
            Debug.Assert(m != null);

            ShipMissileState s = null;
            switch (state)
            {
                case MissileStates.Ready:
                    s = m.missileReady;
                    break;
                case MissileStates.MissileFlying:
                    s = m.missileFlying;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            return s;
        }

        public static ShipMoveState GetShipState(MoveStates state)
        {
            ShipManager m = GetShipManager();
            Debug.Assert(m != null);

            ShipMoveState s = null;
            switch (state)
            {
                case MoveStates.MoveBoth:
                    s = m.moveBoth;
                    break;
                case MoveStates.MoveLeft:
                    s = m.moveLeft;
                    break;
                case MoveStates.MoveRight:
                    s = m.moveRight;
                    break;
                case MoveStates.Dead:
                    s = m.dead;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            return s;
        }

        public static Missile ActivateMissile()
        {
            ShipManager m = GetShipManager();
            Debug.Assert(m != null);

            //Missile creation
            Missile missile = null;
            GameObjectNode missileGameObj = GhostManager.Find(GameObject.GOName.Missile);
            if (missileGameObj == null)
            {
                missile = new Missile(Sprite.SpriteName.Missile, 400, 100);
            }
            else
            {
                missile = (Missile)missileGameObj.gameObj;
                GhostManager.Remove(missileGameObj);
                missile.Revive(400, 100);
            }
            m.missile = missile;
            missile.proxySprite.spriteObj.SwapColor(0, 0, 255); //blue missile

            //added to the correct batch
            missile.ActivateSprite(SpriteBatchNode.GroupTypes.Missiles);
            missile.ActivateCollisionSprite(SpriteBatchNode.GroupTypes.Boxes);
            //missile.GetCollisionObject().collisionBox.SetColor(1, 1, 1);

            GameObject missileGroup = GameObjectNodeManager.Find(GameObject.GOName.MissileGroup);
            Debug.Assert(missileGroup != null);
            missileGroup.Add(m.missile);

            return m.missile;
        }

        public static Ship ActivateShip()
        {
            ShipManager m = GetShipManager();
            Debug.Assert(m != null);

            //create the ship object
            Ship ship;
            GameObjectNode shipObjNode = GhostManager.Find(GameObject.GOName.Ship);
            if(shipObjNode == null)
            {
                ship = new Ship(GameObject.GOName.Ship, Sprite.SpriteName.Ship, 150, 50);
            }
            else
            {
                ship = (Ship)shipObjNode.gameObj;
                GhostManager.Remove(shipObjNode);
                ship.Revive(150,50);
            }
                
            m.ship = ship;

            //added to the correct batch
            ship.ActivateSprite(SpriteBatchNode.GroupTypes.Ship);
            ship.ActivateCollisionSprite(SpriteBatchNode.GroupTypes.Boxes);

            GameObject shipRoot = GameObjectNodeManager.Find(GameObject.GOName.ShipRoot);
            Debug.Assert(shipRoot != null);

            shipRoot.Add(m.ship);
            m.ship.SetShipState(MissileStates.Ready);
            m.ship.SetShipState(MoveStates.MoveBoth);
            return m.ship;
        }

        public static Ship GetShip()
        {
            ShipManager m = GetShipManager();
            Debug.Assert(m != null);
            Debug.Assert(m.ship != null);

            return m.ship;
        }
        

        private static ShipManager GetShipManager()
        {
            Debug.Assert(manager != null);
            return manager;
        }
    }
}
