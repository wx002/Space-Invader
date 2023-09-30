namespace SpaceInvaders
{
    abstract class CompositeIterator
    {
        abstract public Component Next();
        abstract public bool IsDone();
        abstract public Component First();
        abstract public Component Current();
    }
}
