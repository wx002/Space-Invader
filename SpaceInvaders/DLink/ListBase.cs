namespace SpaceInvaders
{
    abstract class ListBase
    {
        abstract public void AddToFront(NodeBase pNode);
        abstract public void AddToEnd(NodeBase pNode);
        abstract public void Remove(NodeBase pNode);
        abstract public void InsertNode(NodeBase nodeToInsert, NodeBase nodePrev);
        abstract public NodeBase RemoveFromFront();
        abstract public Iterator GetIterator();

        abstract public DLink GetHead();
    }
}
