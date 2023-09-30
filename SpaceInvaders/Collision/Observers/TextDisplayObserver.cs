using System.Diagnostics;

namespace SpaceInvaders
{
    class TextDisplayObserver : CollisionObserver
    {
        bool isDisplayed = false;
        public override void Notify()
        {
            Debug.WriteLine("Text Observer: {0}, {1}", collisionSubject.subject1, collisionSubject.subject2);
            if (!isDisplayed)
            {
                FontManager.Add(Font.FontName.BOOM, SpriteBatchNode.GroupTypes.Text, "BOOM!", Glyph.GlyphName.Consolas36pt, 200, 200);
                isDisplayed = true;
            }
        }

        public override void Print()
        {
            
        }
    }
}
