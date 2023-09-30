using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class WallType: Leaf
    {
        public enum Types
        {
            Group,
            Right,
            Left,
            Bottom,
            Top,

            Uninitalized
        }

        protected Types wallType;

        protected WallType(GameObject.GOName name, Sprite.SpriteName spriteName, float x, float y, Types t):
            base(name, spriteName, x, y)
        {
            wallType = t;
        }

        ~WallType() { }

        public Types GetWallType()
        {
            return wallType;
        }
    }
}
