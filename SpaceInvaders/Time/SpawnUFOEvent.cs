using System;

namespace SpaceInvaders
{
    class SpawnUFOEvent: Command
    {
        public static Random rand = new Random();
        public override void Run(float deltatime)
        {
            UFO u = (UFO)AlienFactory.RecycleCreate(GameObject.GOName.UFO, SpriteBatchNode.GroupTypes.UFO,SpriteBatchNode.GroupTypes.Boxes,50, 700);
            UFOColumn ufoCol = (UFOColumn)AlienFactory.RecycleCreate(GameObject.GOName.UFOColumn, SpriteBatchNode.GroupTypes.Aliens, SpriteBatchNode.GroupTypes.Boxes,0,0);
            ufoCol.Add(u);
            SoundEngine.PlaySound(SoundEngine.Sound.UFO);
            //Find the box
            UFOBox ufoBox = (UFOBox)GameObjectNodeManager.Find(GameObject.GOName.UFOBox);
            ufoBox.Add(ufoCol);
        }
    }
}
