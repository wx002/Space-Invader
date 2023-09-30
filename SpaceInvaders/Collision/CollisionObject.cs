using System.Diagnostics;

namespace SpaceInvaders
{
    class CollisionObject
    {
        public BoxSprite collisionBox;
        public CollisionRect collisionRect;
        public CollisionObject(ProxySprite proxy)
        {
            Debug.Assert(proxy != null);

            //get real sprite ref
            Sprite realSprite = proxy.spriteObj;
            Debug.Assert(realSprite != null);
            

            //get the rect
            collisionRect = new CollisionRect(realSprite.GetAzulRect());
            Debug.Assert(this.collisionRect != null);

            collisionBox = BoxSpriteManager.Add(BoxSprite.BoxName.Box1, collisionRect.x, collisionRect.y, collisionRect.width, collisionRect.height);
            Debug.Assert(collisionBox != null);
            collisionBox.SetColor(255.0f, 0, 0);
        }

        public void UpdatePosition(float x, float y)
        {
            this.collisionRect.x = x;
            this.collisionRect.y = y;

            this.collisionBox.imageX = collisionRect.x;
            this.collisionBox.imageY = collisionRect.y;

            this.collisionBox.SetRect(collisionRect.x, collisionRect.y, collisionRect.width, collisionRect.height);
            this.collisionBox.Update();
        }
    }
}
