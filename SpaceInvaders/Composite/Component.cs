

namespace SpaceInvaders
{
    abstract class Component: CollisionVisitor
    {
        public enum Container
        {
            LEAF,
            COMPOSITE,
            UNKNOWN
        }

        public Container type;
        public Component parent;
        public Component reverse;

        public Component(Container c)
        {
            type = c;
            parent = null;
            reverse = null;
        }

        public abstract void ComponentPrint();
        public abstract void Move(float x, float y);
        public abstract void Add(Component c);

        public abstract void Remove(Component c);

        public virtual void Revive()
        {
            this.parent = null;
            this.reverse = null;
        }
    }
}
