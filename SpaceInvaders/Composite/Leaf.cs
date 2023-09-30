using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class Leaf: GameObject
    {
        public Leaf(GameObject.GOName gameName, Sprite.SpriteName sName, float x, float y)
            : base(Container.LEAF, gameName, sName, x, y)
        {
        }

        public override void Print()
        {
            base.Print();
        }

        public override void Clear()
        {
            //base.Print();
            base.Clear();
        }

        public override void Add(Component c)
        {
            Debug.Assert(false); //should not be used for Leaf type nodes
        }

        public override void Remove(Component c)
        {
            Debug.Assert(false);
        }

        public override void Revive()
        {
            base.Revive();
        }
    }
}
