using System.Diagnostics;
namespace SpaceInvaders
{
    abstract class BaseSprite: DLink
    {
        private SpriteNode backTrackSpriteNode;
        public BaseSprite() : base()
        {
            backTrackSpriteNode = null;
        }

        public SpriteNode GetSpriteNode()
        {
            Debug.Assert(backTrackSpriteNode != null);
            return backTrackSpriteNode;
        }

        public void SetBackTrackSpriteNode(SpriteNode _backTrackBatchNode)
        {
            Debug.Assert(_backTrackBatchNode != null);
            this.backTrackSpriteNode = _backTrackBatchNode;
        }

        abstract public void Update();
        abstract public void Render();
    }
}
