using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class ShieldCategory: Leaf
    {
        public enum ShieldType
        {
            Root,
            Column,
            Brick,
            Grid,

            LeftTop0,
            LeftTop1,
            LeftBottom,

            RightTop0,
            RightTop1,
            RightBottom,

            Uninitalized
        }

        protected ShieldType shieldType;

        protected ShieldCategory(GameObject.GOName name, Sprite.SpriteName spriteName, float x, float y, ShieldType t):
            base(name, spriteName, x, y)
        {
            this.shieldType = t;
        }

        public ShieldType GetShieldType()
        {
            return this.shieldType;
        }
    }
}
