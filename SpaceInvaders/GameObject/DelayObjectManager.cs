using System.Diagnostics;

namespace SpaceInvaders
{
    class DelayObjectManager
    {
        private DList list;
        private static DelayObjectManager manager = null; //singleton
        //private static DelayObjectManager activeDelayManager = null;

        public static void AddObserver(CollisionObserver obs)
        {
            Debug.Assert(obs != null);
            DelayObjectManager m = GetDelayObjectManager();
            Debug.Assert(m != null);
            m.list.AddToFront(obs);
        }

        public static void Proccess()
        {
            DelayObjectManager m = GetDelayObjectManager();
            Debug.Assert(m != null);
            Iterator itr = m.list.GetIterator();
            CollisionObserver walk = (CollisionObserver)itr.First();

            //run first
            while (!itr.IsDone())
            {
                walk.Execute();
                walk = (CollisionObserver)itr.Next();
            }

            //post run, now we delete
            CollisionObserver tmpObs = null;
            itr = m.list.GetIterator();
            walk = (CollisionObserver)itr.First();

            //run first
            while (!itr.IsDone())
            {
                tmpObs = walk;
                walk = (CollisionObserver)itr.Next();

                //remove from the list
                m.list.Remove(tmpObs);
            }
        }

        public DelayObjectManager()
        {
            list = new DList();
        }

        public static void SetActiveManager(DelayObjectManager delayManager)
        {
            Debug.Assert(delayManager != null);
            manager = delayManager;
        }

        private static DelayObjectManager GetDelayObjectManager()
        {
            if(manager == null)
            {
                manager = new DelayObjectManager();
            }
            Debug.Assert(manager != null);
            return manager;
        }
    }
}
