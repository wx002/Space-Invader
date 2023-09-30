using System;

namespace SpaceInvaders
{
    class CollisionBoxToggleObserver: InputObserver
    {
        public override void Notify()
        {
            SpriteBatchNode boxNode = SpriteBatchManager.Find(SpriteBatchNode.GroupTypes.Boxes);
            boxNode.SetDrawable(!boxNode.GetIsDrawable());
        }

        public override void Print()
        {
            base.DLinkPrint();
        }
    }
}
