using System.Diagnostics;

namespace SpaceInvaders
{
    class Font: DLink
    {
        public enum FontName
        {
            Text,
            NULL,
            Uninitalized,
            TestOneOff,
            BOOM,

            p1Label,
            p1Score,
            p2Label,
            p2ScoreDisplayP2,
            highScoreLabel,
            highScore,

            PlayMessage,
            SquidPoint,
            CrabPoint,
            OctupusPoint,
            CrabScore,
            Display,
            p1ScoreDisplayP2,
            TimeChars,
            p1LabelFinal,
            p1ScoreFinal,
            p2LabelFinal,
            p2ScoreDisplayFinal,
            NumLivesLabel,
            NumLivesP1,
            NumLivesP2,
            p1ScoreDisplay,
            p1ScoreDisplayP1,
            p2ScoreDisplayP1,
            p1ScoreSelect,
            p2ScoreSelect,
            p2Score
        }

        public FontName name;
        public FontSprite fontSprite;
        static string nullString = "null";

        public Font()
        {
            this.name = FontName.Uninitalized;
            fontSprite = new FontSprite();
        }

        public void Set(Font.FontName name, string msg, Glyph.GlyphName glyphName, float xStart, float yStart)
        {
            Debug.Assert(msg != null);
            this.name = name;
            fontSprite.Set(name, msg, glyphName, xStart, yStart);
        }

        public void UpdateMessage(string msg)
        {
            Debug.Assert(msg != null);
            Debug.Assert(fontSprite != null);
            fontSprite.UpdateMessage(msg);
        }

        public override void Clear()
        {
            Clean();
        }

        public override object GetData()
        {
            return name;
        }

        public override void Print()
        {
            Debug.WriteLine("Font: {0},{1}", name, GetHashCode());
        }

        public void SetColor(float r, float g, float b)
        {
            this.fontSprite.SetColor(r, g, b);
        }

        private void Clean()
        {
            name = FontName.Uninitalized;
            fontSprite.Set(FontName.NULL, nullString, Glyph.GlyphName.Null_Object, 0, 0);
        }
    }
}
