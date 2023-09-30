using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpriteBatchNode: DLink
    {
        public enum GroupTypes
        {
            Birds,
            Aliens,
            Boxes,
            Shields,

            Uninitialized,
            Missiles,
            Text,
            Walls,
            Ship,
            Bombs,
            ResetBoxes,
            UFO
        }

        private int _groupPrority;
        public GroupTypes name;
        private readonly SpriteNodeManager nodeManager;
        private bool isDrawable;

        
        public SpriteBatchNode()
        {
            _groupPrority = -1;
            name = GroupTypes.Uninitialized;
            //LTN - SpriteBatchNode
            nodeManager = new SpriteNodeManager();
            Debug.Assert(nodeManager != null);
            isDrawable = true; //always true on init
        }

        public void SetGroup(GroupTypes type, int reserveSize = 5, int growthSize = 2, int proroity = -1)
        {
            this.name = type;
            nodeManager.CreateNodeManager(type, reserveSize, growthSize);
            this._groupPrority = proroity;
        }

        public SpriteNodeManager GetGroupManager()
        {
            return nodeManager;
        }

        public SpriteNode AddNodeToBatch(GameObject g)
        {
            Debug.Assert(g != null);
            SpriteNode s = nodeManager.AddNodeToBatch(g.proxySprite);
            s.SetSprite(g.proxySprite, this.nodeManager);

            //backpointer
            this.nodeManager.SetBatchSpriteNode(this);
            return s;
        }

        public SpriteNode AddNodeToBatch(BaseSprite baseSprite)
        {
            SpriteNode node = this.nodeManager.AddNodeToBatch(baseSprite);

            node.SetSprite(baseSprite, this.nodeManager);
            //Debug.WriteLine("set base sprite {0} to node manager {1}", baseSprite, this.nodeManager.name);

            //back pointer
            this.nodeManager.SetBatchSpriteNode(this);
            //Debug.WriteLine("set node manager {0} to batch {1}", nodeManager.name, this.name);
            return node;
        }

        public void UpdatePrority(int num)
        {
            this._groupPrority = num;
        }

        public int getPrority()
        {
            return _groupPrority;
        }

        public override void Clear()
        {
            _groupPrority = -1;
            name = GroupTypes.Uninitialized;
            //this.nodeManager.des
        }

        public override void Print()
        {
            Debug.WriteLine(string.Format("ID: {0}, Prority: {1}, group type: {2}", this.GetHashCode(), _groupPrority, name));
            nodeManager.ManagerPrintSimple();
        }

        public override object GetData()
        {
            return this.name;
        }

        public bool GetIsDrawable()
        {
            return this.isDrawable;
        }

        public void SetDrawable(bool isDrawable)
        {
            this.isDrawable = isDrawable;
        }
    }
}
