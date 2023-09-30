using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class CollisionObserver : DLink
    {
        public abstract void Notify();
        public override void Clear()
        {
            //Debug.Assert(false);
        }

        public virtual void Execute() { }

        public CollisionSubject collisionSubject;
    }
}
