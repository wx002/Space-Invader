using System.Diagnostics;

namespace SpaceInvaders
{
    class MoveVerticalCommand: Command
    {
        private GameObject grid;
        private float moveSpeed;

        public MoveVerticalCommand(GameObject.GOName objName)
        {
            grid = GameObjectNodeManager.Find(objName);
            Debug.Assert(grid != null);
            moveSpeed = -10.0f;
        }

        public override void Run(float deltatime)
        {
            grid.Move(10, moveSpeed);
            //TimeEventManager.Add(TimeEvent.Event.MovementHorizontal, this, deltatime);
        }

        public void UpdateMoveSpeed(float speed)
        {
            this.moveSpeed = speed;
        }
    }
}
