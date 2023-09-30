using System.Diagnostics;

namespace SpaceInvaders
{
    class BoxSpriteFactory
    {
        public SpriteBatchNode batchNode;
        public BoxSpriteFactory(SpriteBatchNode.GroupTypes name)
        {
            batchNode = SpriteBatchManager.Find(name);
            Debug.Assert(batchNode != null);
        }

        public void CreateBoxSprite(BoxSprite.BoxName name, float imageX, float imageY, float width, float height, int red, int green, int blue)
        {
            BoxSpriteManager.Add(name, imageX, imageY, width, height, red, green, blue);
        }

        public void CreateProxyBoxSprite(BoxSprite.BoxName name, float locX, float locY)
        {
            ProxyBoxSprite ps = ProxyBoxSpriteManager.Add(name);
            ps.locX = locX;
            ps.locY = locY;
            batchNode.AddNodeToBatch(ps);
        }
    }
}
