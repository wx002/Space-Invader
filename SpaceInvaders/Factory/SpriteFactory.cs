using System.Diagnostics;

namespace SpaceInvaders
{
    class SpriteFactory
    {
        public SpriteBatchNode batchNode;
        public SpriteFactory(SpriteBatchNode.GroupTypes name)
        {
            batchNode = SpriteBatchManager.Find(name);
            Debug.Assert(batchNode != null);
        }

        public void CreateSprite(Sprite.SpriteName name, Image.ImageName imageName, float imageX, float imageY, float width, float height)
        {
            SpriteManager.Add(name, imageName, imageX, imageY, width, height);
        }

        public void CreateProxySprite(Sprite.SpriteName name, float locX, float locY)
        {
            ProxySprite ps = ProxySpriteManager.Add(name);
            ps.locX = locX;
            ps.locY = locY;
            batchNode.AddNodeToBatch(ps);
        }
    }
}
