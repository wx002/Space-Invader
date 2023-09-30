using System.Diagnostics;

namespace SpaceInvaders
{
    class SpriteNodeManager: Manager
    {
        private SpriteNode spiriteNode;
        public SpriteBatchNode.GroupTypes name;

        //for delete
        private SpriteBatchNode backTrackBatch;



        public SpriteNodeManager(int reserveSize = 5, int growthSize = 2) :
            base(new DList(), new DList(), reserveSize, growthSize)
        // LTN - SpriteNodeManager
        // DList for the active/reserve object pooling
        // Owner: SpriteNodeManager
        {
            // LTN - SpriteNodeManager
            // object instance for find
            // Owner: SpriteNodeManager
            this.backTrackBatch = null;
            spiriteNode = new SpriteNode();
            Debug.Assert(spiriteNode != null);
        }

        public void CreateNodeManager(SpriteBatchNode.GroupTypes gptType, int reserveSize=5, int growthSize=2)
        {
            this.name = gptType;
            Debug.Assert(reserveSize > 0);
            Debug.Assert(growthSize > 0);

            base.ManagerInitReserve(reserveSize, growthSize);
        }
        

        public SpriteNode AddNodeToBatch(BaseSprite b)
        {
            SpriteNode node = (SpriteNode)this.ManagerAddFront();
            Debug.Assert(node != null);
            node.SetSprite(b, this);
            return node;
        }

        public void SetBatchSpriteNode(SpriteBatchNode batchNode)
        {
            //Debug.WriteLine("Set batch node: {0}, SpriteNode Name: {1}", batchNode.name, this.name);
            this.backTrackBatch = batchNode;
        }

        public SpriteBatchNode GetSpriteBatchNode()
        {
            return this.backTrackBatch;
        }

        public void Draw()
        {
            Iterator itr = this.ManagerGetActiveIterator();

            SpriteNode walk = (SpriteNode)itr.First();
            
            while (!itr.IsDone())
            {
                walk.bSprite.Render(); 
                walk = (SpriteNode)itr.Next();
            }
        }

        ~SpriteNodeManager()
        {
            base.ResetManager();
        }

        public void Destroy()
        {
            base.ResetManager();   
            this.backTrackBatch = null;
        }


        public void Remove(SpriteNode n)
        {
            Debug.Assert(n != null);

            this.ManagerRemove(n);
        }




        protected override bool Compare(NodeBase nodeA, NodeBase nodeB)
        {
            Debug.Assert(nodeA != null);
            Debug.Assert(nodeB != null);
            SpriteNode spA = (SpriteNode)nodeA;
            SpriteNode spB = (SpriteNode)nodeB;
            bool cmp = false;
            if (spA.GetData() == spB.GetData())
            {
                cmp = true;
            }
            return cmp;
        }

        

        protected override NodeBase CreateNode()
        {
            // LTN - SpriteNodeManager
            // Nodes created for object pooling design pattern
            // Owner: SpriteManager, use to fill the reserve list, so it can be used in the active
            // Nodes gets recycle upon remove from active list
            NodeBase n = new SpriteNode();
            Debug.Assert(n != null);
            return n;
        }
    }
}
