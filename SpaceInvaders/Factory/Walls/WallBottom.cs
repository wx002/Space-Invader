using System.Diagnostics;

namespace SpaceInvaders
{
    class WallBottom : WallType
    {
        public WallBottom(GOName name, Sprite.SpriteName spriteName, float x, float y, float width, float height)
            : base(name, spriteName, x, y, WallType.Types.Bottom)
        {
            this.GetCollisionObject().collisionRect.Set(x, y, width, height);

            this.x = x;
            this.y = y;

            this.GetCollisionObject().collisionBox.SetColor(1, 1, 0);
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitBottomWall(this);
        }

        public override void VisitBomb(Bomb b)
        {
            CollisionPair pColPair = CollisionPairManager.GetActiveCollisionPair();
            pColPair.SetCollisionPair(b, this);
            pColPair.NotifyListeners();
        }

        public override void ComponentPrint()
        {
            base.Print();
        }

        public override void VisitAlienGrid(AlienGrid ag)
        {
            switch (GameSession.player)
            {
                case Player.P1:
                    GameSession.p1NumLives = 0;
                    break;
                case Player.P2:
                    GameSession.p2NumLives = 0;
                    break;
            }
            SceneContext.GetSceneState().Handle();
        }

        public override void Move(float x, float y)
        {
            Debug.Assert(false);
        }
    }
}
