using System.Diagnostics;
namespace SpaceInvaders
{
    class HorizontalMoveCommand: Command
    {
        private GameObject grid;
        private float moveSpeed;
        private int songCounter = 0;

        public HorizontalMoveCommand(GameObject.GOName objName)
        {
            grid = GameObjectNodeManager.Find(objName);
            Debug.Assert(grid != null);
            moveSpeed = GameSession.GetSpeed();
        }

        public override void Run(float deltatime)
        {
            GameSession.UpdateSpeed();
            this.moveSpeed = GameSession.GetSpeed();
            grid.Move(moveSpeed, 0);
            SoundEngine.PlayMarch(songCounter);
            songCounter = (songCounter + 1) % 4;
            if (GameSession.GetAliensAlive() > 5)
            {
                TimeEventManager.Add(TimeEvent.Event.MovementHorizontal, this, deltatime);
            }
            else if(GameSession.GetAliensAlive() > 0 && GameSession.GetAliensAlive() <=5)
            {
                TimeEventManager.Add(TimeEvent.Event.MovementHorizontal, this, 0.50f);
            }
        }


        public void UpdateMoveSpeed(float speed)
        {
            this.moveSpeed = speed;
        }
    }
}
