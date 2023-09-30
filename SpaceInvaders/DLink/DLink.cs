using System.Diagnostics;
namespace SpaceInvaders
{
    /*
     * DLink abstract class
     * **/
    abstract class DLink: NodeBase
    {
        public DLink pNext;
        public DLink pPrev;

        protected DLink(){
            this.pNext = null;
            this.pPrev = null;
        }

        public void DLinkPrint()
        {
            if(this.pPrev == null)
            {
                Debug.WriteLine("Prev: null");
            }
            else
            {
                NodeBase n = this.pPrev;
                Debug.WriteLine("Prev: {0}", n.GetData());
            }
            if(this.pNext == null)
            {
                Debug.WriteLine("Next: null");
            }
            else
            {
                NodeBase n = this.pNext;
                Debug.WriteLine("Next: {0}", n.GetData());
            }
        }

        public override void Clear()
        {
            this.pNext = null;
            this.pPrev = null;
        }
    }
}
