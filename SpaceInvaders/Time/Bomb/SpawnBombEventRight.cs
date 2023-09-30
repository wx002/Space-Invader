using System.Diagnostics;
using System;

namespace SpaceInvaders
{
    class SpawnBombEventRight: Command
    {
        GameObject bombRoot;
        SpriteBatchNode bombBatch;
        SpriteBatchNode boxBatch;
        Random pRandom = GameSession.random;
        AlienGrid grid;
        public SpawnBombEventRight()
        {
            this.bombRoot = GameObjectNodeManager.Find(GameObject.GOName.BombRoot);
            Debug.Assert(this.bombRoot != null);

            this.bombBatch = SpriteBatchManager.Find(SpriteBatchNode.GroupTypes.Bombs);
            Debug.Assert(this.bombBatch != null);

            this.boxBatch = SpriteBatchManager.Find(SpriteBatchNode.GroupTypes.Boxes);
            Debug.Assert(this.boxBatch != null);

            this.grid = (AlienGrid)GameObjectNodeManager.Find(GameObject.GOName.AlienGrid);
            Debug.Assert(grid != null);
        }
        public override void Run(float deltatime)
        {
            Iterator itr = grid.GetIterator();
            AlienColumn cols = (AlienColumn)itr.First();
            while (!itr.IsDone())
            {
                cols.DropBomb(bombRoot, pRandom.Next(0,3), pRandom.Next(1,5));
                cols = (AlienColumn)itr.Next();
            }
            if(GameSession.GetAliensAlive() > 0)
            {
                TimeEventManager.Add(TimeEvent.Event.BombStraightEvent, this, 5);
            }
        }
    }
}
