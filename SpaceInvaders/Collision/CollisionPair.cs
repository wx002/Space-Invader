using System.Diagnostics;

namespace SpaceInvaders
{
    class CollisionPair: DLink
    {
        public enum PairName
        {
            Alien_Missile,

            NULL,
            uninitalized,
            Bomb_Shield,
            Missile_Wall,
            Ship_Bumper,
            Missile_Shield,
            Erase_Wall,
            Grid_Wall,
            Alien_Shield1,
            Alien_Shield2,
            Alien_Shield3,
            Alien_Shield4,
            Missile_Bomb,
            Bomb_Ship,
            UFO_Missile,
            UFO_Wall,
            UFO_Shield,
            EraserShield1
        }

        public GameObject treeA;
        public GameObject treeB;
        public PairName name;
        public CollisionSubject collisionSubject; //add collision subject to support observers

        public CollisionPair() : base()
        {
            Clean();
        }

        ~CollisionPair() { }

        public void Set(PairName name, GameObject treeANode, GameObject treeBNode)
        {
            Debug.Assert(treeANode != null);
            Debug.Assert(treeBNode != null);

            this.name = name;
            treeA = treeANode;
            treeB = treeBNode;
        }

        public void ProcessCollision()
        {
            Collide(treeA, treeB);
        }

        public static void Collide(GameObject treeANode, GameObject treeBNode)
        {
            //comparing the two trees using the collision boxes
            GameObject pNodeA = treeANode;
            while (pNodeA != null)
            {
                // always start w/ root node for tree B
                GameObject pNodeB = treeBNode;
                while (pNodeB != null)
                {
                    // Get the respective collision rectangles
                    CollisionRect rectA = pNodeA.GetCollisionObject().collisionRect;
                    CollisionRect rectB = pNodeB.GetCollisionObject().collisionRect;
                    //pNodeB.Print();
                    //Debug.WriteLine("Rect A compare: {0},{1}", rectA.x, rectA.y);
                    //Debug.WriteLine("Rect B compare: {0},{1}", rectB.x, rectB.y);
                    // Do they intersect? If they do, a collision has occured!
                    if (CollisionRect.Intersect(rectA, rectB))
                    {
                        //Accept the visitor to perform collision operation
                        pNodeA.Accept(pNodeB);
                        break;
                    }
                    // if not, move on to the next node
                    pNodeB = (GameObject)CompositeIteratorForward.GetSiblingNode(pNodeB);
                }
                // move on again
                pNodeA = (GameObject)CompositeIteratorForward.GetSiblingNode(pNodeA);
            }
        }

        public void AddObserver(CollisionObserver obs)
        {
            collisionSubject.AddObserver(obs);
        }

        public void NotifyListeners()
        {
            collisionSubject.Notify();
        }

        public void SetCollisionPair(GameObject objA, GameObject objB)
        {
            Debug.Assert(objA != null);
            Debug.Assert(objB != null);

            this.collisionSubject.subject1 = objA;
            this.collisionSubject.subject2 = objB;
        }

        public override object GetData()
        {
            return this.name;
        }

        public override void Clear()
        {
            Clean();
        }

        public override void Print()
        {
            Debug.WriteLine("Name: {0}, HashCode: {1}", this.name, this.GetHashCode());
            Debug.WriteLine("Tree A GameObject {0}", treeA.GetHashCode());
            treeA.Print();
            Debug.WriteLine("Tree B GameObject {0}", treeB.GetHashCode());
            treeB.Print();
            base.DLinkPrint();
        }

        private void Clean()
        {
            treeA = null;
            treeB = null;
            name = PairName.uninitalized;
            collisionSubject = new CollisionSubject();
            Debug.Assert(collisionSubject != null);
        }
    }
}
