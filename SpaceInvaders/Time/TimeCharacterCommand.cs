using System;

namespace SpaceInvaders
{
    class TimeCharacterCommand: Command
    {
        private string letter;
        private float x;
        private float y;
        private float red;
        private float green;
        private float blue;
        private Font font;
        private TimeCharacterCommand prev;
        public TimeCharacterCommand(string letter, float x, float y, float r, float g, float b, TimeCharacterCommand prev)
        {
            this.letter = letter;
            this.x = x;
            this.y = y;
            this.red = r;
            this.green = g;
            this.blue = b;
            this.font = null;
            this.prev = prev;
        }

        public override void Run(float deltatime)
        {
            if(this.prev != null)
            {
                FontManager.Remove(prev.font);
            }
            Font newFont = FontManager.Add(Font.FontName.TimeChars,
                SpriteBatchNode.GroupTypes.Text, this.letter, Glyph.GlyphName.Consolas36pt, this.x, this.y);
            newFont.SetColor(red,green,blue);
            this.font = newFont;
        }
    }
}
