using System.Diagnostics;

namespace SpaceInvaders
{
    class WallGroup: Composite
    {
        public WallGroup(GameObject.GOName name, Sprite.SpriteName spriteName, float x, float y):
            base(name, spriteName)
        {
            this.x = x;
            this.y = y;

            //this.GetCollisionObject().collisionBox.SetColor(1, 1, 1);
            this.name = name;
        }

        ~WallGroup() { }

        //visitor for wall group
        public override void Accept(CollisionVisitor other)
        {
            other.VisitWallGroup(this);
        }

        public override void VisitBombRoot(BombRoot b)
        {
            // BombRoot vs WallRoot
            GameObject pGameObj = (GameObject)CompositeIteratorForward.GetChildNode(b);
            CollisionPair.Collide(pGameObj, this);
        }

        public override void VisitBomb(Bomb b)
        {
            // Bomb vs WallRoot
            GameObject pGameObj = (GameObject)CompositeIteratorForward.GetChildNode(this);
            CollisionPair.Collide(b, pGameObj);
        }

        public override void VisitMissileGroup(MissileGroup missileGroup)
        {
            GameObject g = (GameObject) CompositeIteratorForward.GetChildNode(missileGroup);
            CollisionPair.Collide(g, this);
        }

        public override void VisitMissile(Missile m)
        {
            GameObject g = (GameObject)CompositeIteratorForward.GetChildNode(this);
            CollisionPair.Collide(m, g);
        }

        public override void Update()
        {
            UpdateBondingBox(this);
            base.Update();
        }

        public override void VisitAlienGrid(AlienGrid ag)
        {
            //AlienGrid vs Wall Group
            GameObject g = (GameObject)CompositeIteratorForward.GetChildNode(this);
            CollisionPair.Collide(ag, g);
        }

        public override void VisitUFOBox(UFOBox uFOBox)
        {
            //UFO box vs wall group
            GameObject g = (GameObject)CompositeIteratorForward.GetChildNode(this);
            CollisionPair.Collide(uFOBox, g);
        }
    }
}
