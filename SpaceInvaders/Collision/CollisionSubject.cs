using System.Diagnostics;

namespace SpaceInvaders
{
    class CollisionSubject
    {
        public GameObject subject1;
        public GameObject subject2;

        private DList list;

        public CollisionSubject()
        {
            subject1 = null;
            subject2 = null;

            list = new DList();
            Debug.Assert(list != null);
        }

        ~CollisionSubject()
        {
            subject1 = null;
            subject2 = null;
        }

        public void AddObserver(CollisionObserver obs)
        {
            Debug.Assert(obs != null);
            obs.collisionSubject = this;
            //add the observer to the list
            list.AddToFront(obs);
        }

        public void Notify()
        {
            //loop thru the list of the observers
            Iterator itr = list.GetIterator();

            //start with the current
            CollisionObserver cObs = (CollisionObserver)itr.Current();

            while (!itr.IsDone())
            {
                cObs.Notify();
                cObs = (CollisionObserver)itr.Next();
            }
        }
    }
}
