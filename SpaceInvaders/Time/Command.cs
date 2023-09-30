using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Command
    {
        abstract public void Run(float deltatime);
    }
}
