using System.Diagnostics;
namespace SpaceInvaders
{
    class SpriteNode: DLink
    {
        public BaseSprite bSprite;
        private SpriteNodeManager backTrackNodeManager;

        public SpriteNode(): base()
        {
            this.bSprite = null;
            backTrackNodeManager = null;
        }

        public void SetSprite(BaseSprite bSprite, SpriteNodeManager _backTrackManager)
        {
            Debug.Assert(bSprite != null);
            this.bSprite = bSprite;

            Debug.Assert(this.bSprite != null);
            this.bSprite.SetBackTrackSpriteNode(this);

            Debug.Assert(_backTrackManager != null);
            this.backTrackNodeManager = _backTrackManager;
        }

        public SpriteNodeManager GetSpriteNodeManager()
        {
            Debug.Assert(this.backTrackNodeManager != null);
            return this.backTrackNodeManager;
        }

        public SpriteBatchNode GetSpriteBatchNode()
        {
            return this.backTrackNodeManager.GetSpriteBatchNode();
        }



        private void Delete()
        {
            this.bSprite = null;
        }

        public override void Clear()
        {
            this.Delete();
        }

        public override void Print()
        {
            Debug.WriteLine("Sprite Node Hash: {0}", this.GetHashCode());
            if (bSprite != null)
            {
                Debug.WriteLine("Sprite Node Name: {0}, Hashcode: {1}", bSprite.GetData(), this.GetHashCode());
                base.DLinkPrint();
            }
        }

        public override object GetData()
        {
            if (this.bSprite != null)
            {
                return this.bSprite.GetData();
            }
            return null;

        }
    }
}
