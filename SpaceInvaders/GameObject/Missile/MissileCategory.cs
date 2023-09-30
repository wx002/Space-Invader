using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class MissileCategory: Leaf
    {
        public enum MissileTypes
        {
            Missile,
            MissileGroup,
            Bomb,
            Uninitialized
        }
        protected MissileCategory(GameObject.GOName gameName, Sprite.SpriteName spriteName, float _x, float _y)
        : base(gameName, spriteName, _x, _y)
        {
        }
    }
}
