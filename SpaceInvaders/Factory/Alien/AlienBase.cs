using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class AlienBase: Leaf
    {
        public enum Type
        {
            Squid,
            Octupus,
            Crab,
            Grid,
            Column
        }

        protected AlienBase(GameObject.GOName name, Sprite.SpriteName spriteName, float x, float y): base(name, spriteName, x, y)
        {
            this.x = x;
            this.y = y;
        }

        public override void Remove()
        {
            this.GetCollisionObject().collisionRect.Set(0, 0, 0, 0);
            base.Update(); //update the missile

            //get the parent
            GameObject alienGroup = (GameObject)this.parent;
            alienGroup.Update(); //update the parent
            base.Remove();
        }
    }
}
