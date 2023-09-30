using System.Diagnostics;

namespace SpaceInvaders
{
    class FallZigZag : FallStrategy
    {
        private float oldHeight;
        public FallZigZag()
        {
            this.oldHeight = 0.0f;
        }

        public override void Reset(float posY)
        {
            this.oldHeight = posY;
        }

        public override void Fall(Bomb bomb)
        {
            Debug.Assert(bomb != null);

            float targetY = oldHeight - 1.0f * bomb.GetCollisionBoxHeight();

            if (bomb.y < targetY)
            {
                bomb.MultiplyScaling(-1.0f, 1.0f);
                oldHeight = targetY;
            }
        }        
    }
}
