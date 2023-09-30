using System.Diagnostics;

namespace SpaceInvaders
{
    class InputSubject
    {
        private DList list;

        public InputSubject() 
        {
            list = new DList();
            Debug.Assert(list != null);
        }

        public void AddObserver(InputObserver obs)
        {
            Debug.Assert(obs != null);
            obs.inputSubject = this;
            list.AddToFront(obs);
        }

        public void Notify()
        {
            Iterator itr = list.GetIterator();
            InputObserver walk = (InputObserver)itr.First();
            while (!itr.IsDone())
            {
                walk.Notify();
                walk = (InputObserver)itr.Next();
            }
        }

        
    }
}
