using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class Composite: GameObject
    {
        protected DList list;
        public Composite(): base(Container.COMPOSITE, GameObject.GOName.NULL, Sprite.SpriteName.NULL)
        {
            list = new DList();
        }

        public Composite(GameObject.GOName name, Sprite.SpriteName spriteName)
        : base(Component.Container.COMPOSITE,
               name,
               spriteName)
        {
            list = new DList();
        }

        public override void Add(Component c)
        {
            Debug.Assert(c != null);
            Debug.Assert(list != null);
            list.AddToFront(c);
            c.parent = this; //current composite is the parent
        }

        public override void Remove(Component c)
        {
            Debug.Assert(c != null);
            Debug.Assert(list != null);
            list.Remove(c);
        }

        public override void Move(float x, float y)
        {
            Iterator itr = list.GetIterator();
            Debug.Assert(itr != null);

            GameObject walk = (GameObject)itr.First();
            while (!itr.IsDone())
            {
                Debug.Assert(walk != null);
                walk.Move(x, y);
                walk = (GameObject)itr.Next();
            }
        }

        public override void ComponentPrint()
        {
            Debug.WriteLine("\nComposite:");
            Iterator itr = list.GetIterator();
            Debug.Assert(itr != null);
            GameObject walk = (GameObject)itr.First();
            while (!itr.IsDone())
            {
                Debug.Assert(walk != null);
                walk.Print();
                walk = (GameObject)itr.Next();
            }
        }

        public override void Clear()
        {
            base.Clear();
        }

        public Component GetHead()
        {
            Debug.Assert(list != null);
            Component phead = (GameObject)list.getPhead();
            return phead;
        }
    }
}
