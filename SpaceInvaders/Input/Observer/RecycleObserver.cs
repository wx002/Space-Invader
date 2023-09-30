using System.Diagnostics;

namespace SpaceInvaders
{
    class RecycleObserver: InputObserver
    {
        public override void Notify()
        {
            
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
