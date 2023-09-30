using System.Diagnostics;
namespace SpaceInvaders
{
    class DataNode: DLink
    {
        private DataNodeName name;
        private int x;

        public DataNode()
        {
            this.name = DataNodeName.Unitialized;
            this.x = 0;
        }

        

        public void setData(int x, DataNodeName name)
        {
            this.name = name;
            this.x = x;
        }

        public override void Print()
        {
            Debug.WriteLine("Name: " + this.name + ", " + "Value: " + this.x);
        }

        public override void Clear()
        {
            this.name = DataNodeName.Unitialized;
            this.x = 0;
        }

        public DataNodeName getName()
        {
            return name;
        }
    }
}
