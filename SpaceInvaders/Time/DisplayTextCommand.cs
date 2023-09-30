using System.Diagnostics;

namespace SpaceInvaders
{
    class DisplayTextCommand: Command
    {
        private Command next;
        private string text;
        private float xStart;
        private float yStart;

        public DisplayTextCommand(string text, float xStart, float yStart, Command next=null)
        {
            this.next = next;
            this.text = text;
            this.xStart = xStart;
            this.yStart = yStart;
        }

        public override void Run(float deltatime)
        {
            FontManager.Add(Font.FontName.PlayMessage, SpriteBatchNode.GroupTypes.Text, this.text, Glyph.GlyphName.Consolas36pt, xStart, yStart);
            if (next != null)
            {
                TimeEventManager.Add(TimeEvent.Event.SimEvent, next, deltatime);
            }
        }
    }
}
