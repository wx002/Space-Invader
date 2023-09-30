namespace SpaceInvaders
{
    abstract class NodeBase
    {
        abstract public void Clear();
        abstract public void Print();
        virtual public object GetData()
        {
            return null;
        }
    }
}
