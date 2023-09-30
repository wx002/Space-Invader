using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class InputObserver: DLink
    {
        abstract public void Notify();

        public override void Clear()
        {
            Debug.Assert(false);
        }

        public InputSubject inputSubject;
    }
}
