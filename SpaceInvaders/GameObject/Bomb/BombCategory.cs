using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class BombCategory : Leaf
    {
        public enum BombTypes
        {
            Bomb,
            BoobRoot,

            Uninitialize
        }

        BombTypes bombType;
        protected BombCategory(GameObject.GOName gameName, Sprite.SpriteName spriteName, float _x, float _y, BombTypes t)
        : base(gameName, spriteName, _x, _y)
        {
            this.bombType = t;
        }

        ~BombCategory() { }



    }
}
