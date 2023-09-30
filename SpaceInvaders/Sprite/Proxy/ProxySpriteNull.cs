using System;

namespace SpaceInvaders
{
    class ProxySpriteNull: ProxySprite
    {
        public ProxySpriteNull() : base(ProxySpriteNull.ProxyName.Null)
        {

        }

        public override void Render()
        {
            // do nothing
        }

        public override void Update()
        {
            // do nothing
        }
    }
}
