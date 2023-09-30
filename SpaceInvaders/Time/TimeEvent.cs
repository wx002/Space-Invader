using System.Diagnostics;

namespace SpaceInvaders
{
    class TimeEvent: DLink
    {
        //enum event name
        public enum Event
        {
            Sample1,
            Sample2,
            RepeatEvent1,

            SpriteAnimation,
            MovementHorizontal,

            Uninitalized,
            SimEvent,
            TimeCharEvent,
            BombStraightEvent,
            MovementVertical,
            UFOEvent,
            Erase,
            Reset,
            SceneTrans
        }

        public Event name;
        public Command cmd;
        public float startTime;
        public float intervalTime;

        public TimeEvent() : base()
        {
            name = Event.Uninitalized;
            cmd = null;
            startTime = 0.0f;
            intervalTime = 0.0f;
        }

        public void SetTimeEvent(Event name, Command command, float interval)
        {
            Debug.Assert(command != null);

            this.name = name;
            this.cmd = command;
            this.startTime = TimeEventManager.getCurrentTime() + interval;
            this.intervalTime = interval;
        }

        public void Run()
        {
            Debug.Assert(cmd != null);
            cmd.Run(intervalTime);
        }

        public override void Clear()
        {
            name = Event.Uninitalized;
            cmd = null;
            startTime = 0.0f;
            intervalTime = 0.0f;
            base.Clear();
        }

        public Command GetCommand()
        {
            return this.cmd;
        }

        public override void Print()
        {
            Debug.WriteLine("Current Event: {0}\nInterval Time: {1}\nStart Time: {2}", this.name, this.intervalTime, this.startTime);
            base.DLinkPrint();
        }

        public override object GetData()
        {
            return this.name;
        }
    }


}
