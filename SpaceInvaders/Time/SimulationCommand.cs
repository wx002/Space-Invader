using System.Diagnostics;

namespace SpaceInvaders
{
    class SimulationCommand: Command
    {
        private string displayMsg;
        private string msg = "";
        private Font.FontName fontName;
        private float delayMultiple;
        private int index;
        private Command next;

        public SimulationCommand(string msg, Font.FontName fontName, float delay, Command next=null)
        {
            this.displayMsg = msg;
            this.fontName = fontName;
            this.delayMultiple = delay;
            this.index = 0;
            this.next = next;
        }

        public override void Run(float deltatime)
        {
            Font playMsg = FontManager.Find(fontName);
            if(Simulation.GetTotalTime() % delayMultiple == 0)
            {
                if(index < displayMsg.Length)
                {
                    msg = msg + displayMsg[this.index];
                    playMsg.UpdateMessage(msg);
                    Simulation.SetState(Simulation.State.Pause);
                    index++;
                    TimeEventManager.Add(TimeEvent.Event.SimEvent, this, deltatime);
                }
                else
                {
                    //Debug.WriteLine("Finish Time: {0}", Simulation.GetTotalTime());
                    if (next != null)
                    {
                        TimeEventManager.Add(TimeEvent.Event.SimEvent, next, deltatime);
                    }
                }

            }
            
            
        }
    }
}
