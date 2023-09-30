using System.Diagnostics;
namespace SpaceInvaders
{
    class WallRight: WallType
    {
        public WallRight(GameObject.GOName name, Sprite.SpriteName spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, posX, posY, WallType.Types.Left)
        {
            this.GetCollisionObject().collisionRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;


            this.GetCollisionObject().collisionBox.SetColor(1, 0, 0);
        }
        ~WallRight() { }
        public override void Update()
        {
            base.Update();
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitWallRight(this);
        }

        public override void VisitAlienGrid(AlienGrid ag)
        {
            if (GameSession.GetSpeed() > 0 && ag.name == GOName.AlienGrid)
            {
                GameSession.ChangeDirection();
            }
            else
            {
                CollisionPair cp = CollisionPairManager.GetActiveCollisionPair();
                Debug.Assert(cp != null);

                cp.SetCollisionPair(ag, this);
                cp.NotifyListeners();
            }
        }

        public override void VisitUFOBox(UFOBox u)
        {

            GameObject obj = (GameObject)CompositeIteratorForward.GetChildNode(u);
            CollisionPair.Collide(obj, this);
        }

        public override void VisitUFOColumn(UFOColumn uFOColumn)
        {
            Debug.WriteLine("Column Visit");
            GameObject obj = (GameObject)CompositeIteratorForward.GetChildNode(uFOColumn);
            CollisionPair.Collide(obj, this);
        }

        public override void VisitColumn(AlienColumn b)
        {
            GameObject obj = (GameObject)CompositeIteratorForward.GetChildNode(b);
            CollisionPair.Collide(obj, this);
        }

        public override void VisitUFO(UFO u)
        {
            CollisionPair cp = CollisionPairManager.GetActiveCollisionPair();
            Debug.Assert(cp != null);

            cp.SetCollisionPair(u, this);
            cp.NotifyListeners();
        }

        public override void VisitBombRoot(BombRoot bombRoot)
        {
            //Nothing happens
        }

        public override void VisitBomb(Bomb bomb)
        {
            //Nothing happens
        }

        public override void ComponentPrint()
        {
            base.Print();
        }

        public override void Move(float x, float y)
        {
            //walls dont move
        }
    }
}
