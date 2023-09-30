using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienGrid: Composite
    {
        public AlienGrid(GOName name, Sprite.SpriteName spriteName, float x, float y) : 
            base(name, spriteName)
        {
            this.spriteName = spriteName;
            this.x = x;
            this.y = y;
            this.name = name;
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitAlienGrid(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // BirdGroup vs MissileGroup
            // Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)CompositeIteratorForward.GetChildNode(m);
            CollisionPair.Collide(pGameObj, this);
        }

        public void Revive(float x, float y)
        {
            this.x = x;
            this.y = y;
            base.Revive();
        }

        public override void VisitMissile(Missile missile)
        {
            //Debug.WriteLine(" collide: {0} <-> {1}", missile.name, this.name);

            GameObject gameObject = (GameObject)CompositeIteratorForward.GetChildNode(this);
            CollisionPair.Collide(missile, gameObject);
        }

        public override void Update()
        {
            UpdateBondingBox(this);
            base.Update();
            //Debug.WriteLine("Pos: {0},{1}");
        }

        public Iterator GetIterator()
        {
            return this.list.GetIterator();
        }

        public void PrintGrid()
        {
            CompositeIteratorForward pFor = new CompositeIteratorForward(this);

            Component pNode = pFor.First();
            while (!pFor.IsDone())
            {
                pNode.Print();
                pNode = pFor.Next();
            }
        }
    }
}
