using System.Diagnostics;

namespace SpaceInvaders
{
    class TimeCharFactory
    {
        public static void CreateTimeCharEvent(string msg, 
            float startTime, float delayTime, float x, float y, float r, float g, float b)
        {
            TimeCharacterCommand oldCmd = null;
            for(int i = 0; i < msg.Length; i++)
            {
                string displayMsg = msg.Substring(0, i+1);
                TimeCharacterCommand cmd = new TimeCharacterCommand(displayMsg, x, y, r, g, b, oldCmd);
                float cmdStartTime = startTime + i * delayTime;
                TimeEventManager.Add(TimeEvent.Event.TimeCharEvent, cmd, cmdStartTime);
                oldCmd = cmd;
            }
        }
    }
}
