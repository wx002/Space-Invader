using System.Diagnostics;

namespace SpaceInvaders
{
    class FallStraight: FallStrategy
    {
        private float oldHeight;
        public FallStraight()
        {
            this.oldHeight = 0.0f;
        }

        public override void Reset(float posY)
        {
            this.oldHeight = posY;
        }

        public override void Fall(Bomb pBomb)
        {
            Debug.Assert(pBomb != null);
            //Just fall
        }
    }
}
