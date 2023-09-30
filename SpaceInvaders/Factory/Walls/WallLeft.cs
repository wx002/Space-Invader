using System.Diagnostics;
using System;
namespace SpaceInvaders
{
    class WallLeft: WallType
    {
        
        public WallLeft(GameObject.GOName name, Sprite.SpriteName spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, posX, posY, WallType.Types.Left)
        {
            this.GetCollisionObject().collisionRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;


            this.GetCollisionObject().collisionBox.SetColor(1, 0, 0);
        }
        ~WallLeft() { }
        public override void Update()
        {
            base.Update();
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitWallLeft(this);
        }

        public override void VisitBombRoot(BombRoot bombRoot)
        {
            //Nothing happens
        }

        public override void VisitBomb(Bomb bomb)
        {
            //Nothing happens
        }

        public override void VisitMissile(Missile missile)
        {
            //Nothing
        }

        public override void VisitAlienGrid(AlienGrid ag)
        {
            //Change direction
            if (GameSession.GetSpeed() < 0 && ag.name == GOName.AlienGrid)
            {
                GameSession.ChangeDirection();
            }
            //GameSession.UpdateSpeed();
            //Debug.WriteLine("Speed: {0}", GameSession.p1MoveSpeed);

            AlienGrid grid = (AlienGrid)GameObjectNodeManager.Find(GOName.AlienGrid);
            grid.Move(5, -10);
            
            CollisionPair cp = CollisionPairManager.GetActiveCollisionPair();
            Debug.Assert(cp != null);

            cp.SetCollisionPair(ag, this);
            cp.NotifyListeners();
        }

        public override void ComponentPrint()
        {
            base.Print();
        }

        public override void Move(float x, float y)
        {
            //walls dont move
        }

        public override void VisitColumn(AlienColumn b)
        {
            //Nothing
        }

        public override void VisitUFOColumn(UFOColumn uFOColumn)
        {
            //Nothing
        }
    }
}
