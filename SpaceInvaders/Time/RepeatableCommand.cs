using System.Diagnostics;

namespace SpaceInvaders
{
    class RepeatableCommand: Command
    {
        private float repeatBetweenInterval;

        public RepeatableCommand(float interval)
        {
            repeatBetweenInterval = interval;
        }

        public override void Run(float deltatime)
        {
            Debug.WriteLine("Time Event Manager Current Time = {0}", TimeEventManager.getCurrentTime());
            TimeEventManager.Add(TimeEvent.Event.RepeatEvent1, this, repeatBetweenInterval);

        }
    }
}
