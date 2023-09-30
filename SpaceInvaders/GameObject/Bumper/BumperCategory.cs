using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class BumperCategory: Leaf
    {
        public enum BumperTypes
        {
            Left,
            Right,

            Uninitialized
        }

        protected BumperTypes bumperType;

        protected BumperCategory(GOName name, Sprite.SpriteName sName, float x, float y, BumperTypes bumperType):
            base(name, sName, x, y)
        {
            this.bumperType = bumperType;
        }

        ~BumperCategory() { }

        public BumperTypes GetBumperType()
        {
            return this.bumperType;
        }
    }
}
