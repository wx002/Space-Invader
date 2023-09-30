using System.Diagnostics;
using System;

namespace SpaceInvaders
{
    class SpawnBombEventLeft: Command
    {
        GameObject bombRoot;
        SpriteBatchNode bombBatch;
        SpriteBatchNode boxBatch;
        Random pRandom;
        AlienGrid grid;
        public SpawnBombEventLeft(Random pRandom)
        {
            this.bombRoot = GameObjectNodeManager.Find(GameObject.GOName.BombRoot);
            Debug.Assert(this.bombRoot != null);

            this.bombBatch = SpriteBatchManager.Find(SpriteBatchNode.GroupTypes.Bombs);
            Debug.Assert(this.bombBatch != null);

            this.boxBatch = SpriteBatchManager.Find(SpriteBatchNode.GroupTypes.Boxes);
            Debug.Assert(this.boxBatch != null);

            this.grid = (AlienGrid)GameObjectNodeManager.Find(GameObject.GOName.AlienGrid);
            Debug.Assert(grid != null);

            this.pRandom = pRandom;
        }

        public override void Run(float deltatime)
        {
            int bombCounter = 0;
            Bomb bomb;
            float bombX = grid.x-280;
            float bombY = grid.y;

            for (int i = 0; i <5; i++)
            {
                bomb = BombFactory.CreateBombByValue(bombCounter, bombX, bombY+50);
                bomb.fallSpeed = pRandom.Next(1,4);
                bomb.ActivateSprite(SpriteBatchNode.GroupTypes.Bombs);
                bomb.ActivateCollisionSprite(SpriteBatchNode.GroupTypes.Boxes);
                GameObject pBombRoot = GameObjectNodeManager.Find(GameObject.GOName.BombRoot);
                Debug.Assert(pBombRoot != null);
                pBombRoot.Add(bomb);
                bombCounter = (bombCounter + 1) % 3;
                bombX += 100;
            }
            if (GameSession.GetAliensAlive() > 5)
            {
                TimeEventManager.Add(TimeEvent.Event.BombStraightEvent, this, 3);
            }
            
        }

        
    }
}
