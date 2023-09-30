using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class ShipCategory: Leaf
    {
        public enum ShipTypes
        {
            ShipRoot,
            Ship,
            Uninitialized
        }

        protected ShipTypes shipTypes;

        protected ShipCategory(GameObject.GOName name, Sprite.SpriteName spriteName, float x, float y, ShipTypes t):
            base(name, spriteName, x, y)
        {
            this.shipTypes = t;
        }

        public ShipTypes GetShipType()
        {
            return this.shipTypes;
        }
    }
}
