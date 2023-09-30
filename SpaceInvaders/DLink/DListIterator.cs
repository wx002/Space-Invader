namespace SpaceInvaders
{
    class DListIterator: Iterator
    {
        public NodeBase current;
        public bool isDone;
        public NodeBase pHead;

        public DListIterator()
        {
            this.current = null;
            this.pHead = null;
            this.isDone = true;
        }

        public override NodeBase Next()
        {
            DLink walk = (DLink)this.current;
            if (walk != null)
            {
                walk = walk.pNext;
            }

            this.current = walk;
            if(walk == null)
            {
                isDone = true;
            }
            else
            {
                isDone = false;
            }
            return walk;
        }

        public override bool IsDone()
        {
            return isDone;
        }

        public override NodeBase First()
        {
            if(this.pHead != null)
            {
                isDone = false;
                this.current = this.pHead;
            }
            else
            {
                pHead = null;
                current = null;
                isDone = true;
            }
            return current;
        }

        public void Reset(DLink head)
        {
            if(head != null)
            {
                this.isDone = false;
                this.current = head;
                this.pHead = head;
            }
            else
            {
                this.current = null;
                this.pHead = null;
                this.isDone = true;
            }
        }

        public override NodeBase Current()
        {
            NodeBase node = this.current;
            return node;
        }
    }
}
