namespace SpaceInvaders
{
    abstract class Iterator
    {
        abstract public NodeBase Next();
        abstract public bool IsDone();
        abstract public NodeBase First();

        abstract public NodeBase Current();
    }
}
