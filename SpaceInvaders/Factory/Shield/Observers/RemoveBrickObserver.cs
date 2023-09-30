using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveBrickObserver: CollisionObserver
    {
        private GameObject brick;

        public RemoveBrickObserver()
        {
            this.brick = null;
        }

        public RemoveBrickObserver(RemoveBrickObserver obs)
        {
            Debug.Assert(obs != null);
            this.brick = obs.brick;
        }
        public override void Notify()
        {
            this.brick = (Brick)this.collisionSubject.subject2;
            Debug.Assert(brick != null);
            if (this.brick.isDead == false)
            {
                this.brick.isDead = true;
                RemoveBrickObserver obs = new RemoveBrickObserver(this);
                DelayObjectManager.AddObserver(obs);
            }

        }

        public override void Execute()
        {
            GameObject objA = (GameObject)this.brick;
            GameObject objB = (GameObject)CompositeIteratorForward.GetParentNode(objA);
            objA.Remove();
            if (checkParent(objB) == true) //true when all the child nodes are deleted, so we can delete the column
            {
                objB.Remove();
            }

        }

        private bool checkParent(GameObject obj)
        {
            //If no child, return true
            GameObject gObj = (GameObject)CompositeIteratorForward.GetChildNode(obj);
            if (gObj == null)
            {
                return true;
            }
            return false;
        }

        public override void Print()
        {
            Debug.Assert(false);
        }
    }
}
