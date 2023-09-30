using System.Diagnostics;

namespace SpaceInvaders
{
    class DisplayAlienCommand: Command
    {
        public override void Run(float deltatime)
        {
            
            AlienGrid alienGrid = new AlienGrid(GameObject.GOName.AlienGrid, Sprite.SpriteName.NULL, 0, 0);
            AlienColumn column = (AlienColumn)AlienFactory.NoCollisionCreate(GameObject.GOName.AlienColumn,SpriteBatchNode.GroupTypes.Aliens,0,0);
            GameObjectNodeManager.Add(alienGrid);
            column.Add(AlienFactory.NoCollisionCreate(GameObject.GOName.UFO, SpriteBatchNode.GroupTypes.Aliens, 470, 300));
            column.Add(AlienFactory.NoCollisionCreate(GameObject.GOName.Octupus, SpriteBatchNode.GroupTypes.Aliens, 470, 250));
            column.Add(AlienFactory.NoCollisionCreate(GameObject.GOName.Crab, SpriteBatchNode.GroupTypes.Aliens, 470, 200));
            column.Add(AlienFactory.NoCollisionCreate(GameObject.GOName.GreenSquid, SpriteBatchNode.GroupTypes.Aliens, 470, 150));
            alienGrid.Add(column);
        }
    }
}
