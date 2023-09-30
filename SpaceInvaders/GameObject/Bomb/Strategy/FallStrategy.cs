using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class FallStrategy
    {
        abstract public void Fall(Bomb b);

        abstract public void Reset(float t);
    }
}
