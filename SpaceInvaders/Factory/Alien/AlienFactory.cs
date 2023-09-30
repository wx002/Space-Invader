using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienFactory
    {
        public static GameObject NoCollisionCreate(GameObject.GOName type, SpriteBatchNode.GroupTypes batchNodeAlien, float x = 0, float y = 0)
        {
            GameObject gameObj = null;
            switch (type)
            {
                case GameObject.GOName.Crab:
                    // LTN - AlienFactory
                    // Crab object from the factory
                    // Owner: Factory, created from factory so it becomes a game object thru the composite design pattern
                    gameObj = new Crab(Sprite.SpriteName.Crab, x, y);
                    break;
                case GameObject.GOName.Octupus:
                    // LTN - AlienFactory
                    // Octupus object from the factory
                    // Owner: Factory, created from factory so it becomes a game object thru the composite design pattern
                    gameObj = new Octupus(Sprite.SpriteName.Octupus, x, y);
                    break;
                case GameObject.GOName.Squid:
                    // LTN - AlienFactory
                    // Octupus object from the factory
                    // Owner: Factory, created from factory so it becomes a game object thru the composite design pattern
                    gameObj = new Squid(Sprite.SpriteName.Squid, x, y);
                    break;
                case GameObject.GOName.AlienGrid:
                    gameObj = new AlienGrid(GameObject.GOName.AlienGrid, Sprite.SpriteName.NULL, x, y);
                    break;
                case GameObject.GOName.AlienColumn:
                    gameObj = new AlienColumn(GameObject.GOName.AlienColumn, Sprite.SpriteName.NULL, x, y);
                    break;
                case GameObject.GOName.GreenSquid:
                    gameObj = new Squid(Sprite.SpriteName.GreenUFO, x, y);
                    break;
                case GameObject.GOName.GreenOctupus:
                    gameObj = new Octupus(Sprite.SpriteName.GreenOctupus, x, y);
                    break;
                case GameObject.GOName.GreenCrab:
                    gameObj = new Crab(Sprite.SpriteName.GreenOctupus, x, y);
                    break;
                case GameObject.GOName.UFO:
                    gameObj = new UFO(Sprite.SpriteName.UFO, x, y);
                    UFO u = (UFO)gameObj;
                    u.moveSpeed = 0.0f;
                    gameObj = u;
                    break;
                case GameObject.GOName.GreenUFO:
                    gameObj = new UFO(Sprite.SpriteName.GreenUFO, x, y);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            gameObj.ActivateSprite(batchNodeAlien);
            return gameObj;
        }

        public static GameObject Create(GameObject.GOName type, SpriteBatchNode.GroupTypes batchNodeAlien, SpriteBatchNode.GroupTypes batchNodeBox, float x=0, float y=0)
        {
            GameObject gameObj = null;
            switch (type)
            {
                case GameObject.GOName.Crab:
                    // LTN - AlienFactory
                    // Crab object from the factory
                    // Owner: Factory, created from factory so it becomes a game object thru the composite design pattern
                    gameObj = new Crab(Sprite.SpriteName.Crab, x, y);
                    break;
                case GameObject.GOName.Octupus:
                    // LTN - AlienFactory
                    // Octupus object from the factory
                    // Owner: Factory, created from factory so it becomes a game object thru the composite design pattern
                    gameObj = new Octupus(Sprite.SpriteName.Octupus, x, y);
                    break;
                case GameObject.GOName.Squid:
                    // LTN - AlienFactory
                    // Octupus object from the factory
                    // Owner: Factory, created from factory so it becomes a game object thru the composite design pattern
                    gameObj = new Squid(Sprite.SpriteName.Squid, x, y);
                    break;
                case GameObject.GOName.AlienGrid:
                    gameObj = new AlienGrid(GameObject.GOName.AlienGrid, Sprite.SpriteName.NULL, x, y);
                    break;
                case GameObject.GOName.AlienColumn:
                    gameObj = new AlienColumn(GameObject.GOName.AlienColumn, Sprite.SpriteName.NULL, x, y);
                    break;
                case GameObject.GOName.UFOColumn:
                    gameObj = new UFOColumn(GameObject.GOName.UFOColumn, Sprite.SpriteName.NULL, x, y);
                    break;
                case GameObject.GOName.UFO:
                    gameObj = new UFO(Sprite.SpriteName.UFO, x, y);
                    break;
                case GameObject.GOName.UFOBox:
                    gameObj = new AlienGrid(type, Sprite.SpriteName.NULL, x, y);
                    break;
                case GameObject.GOName.GreenCrab:
                    gameObj = new Crab(Sprite.SpriteName.GreenCrab, x, y);
                    break;
                case GameObject.GOName.GreenOctupus:
                    gameObj = new Octupus(Sprite.SpriteName.GreenOctupus, x, y);
                    break;
                case GameObject.GOName.GreenSquid:
                    gameObj = new Squid(Sprite.SpriteName.GreenSquid, x, y);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            gameObj.ActivateSprite(batchNodeAlien);
            gameObj.ActivateCollisionSprite(batchNodeBox);
            return gameObj;
        }

        public static GameObject RecycleCreate(GameObject.GOName type, SpriteBatchNode.GroupTypes batchNodeAlien, SpriteBatchNode.GroupTypes batchNodeBox, float x = 0.0f, float y = 0.0f)
        {
            GameObject gameObj = null;
            GameObjectNode gameObjNode = GhostManager.Find(type);
            
            if (gameObjNode != null)
            {
                gameObj = gameObjNode.gameObj;
                //we used from ghost manager, so we remove it
                GhostManager.Remove(gameObjNode);
                gameObj.x = x;
                gameObj.y = y;
                gameObj.Revive();
                gameObj.ActivateSprite(batchNodeAlien);
                gameObj.ActivateCollisionSprite(batchNodeBox);
                return gameObj;
            }
            else
            {
                gameObj = Create(type, batchNodeAlien, batchNodeBox, x, y);
            }
            return gameObj;
        }



        public static void InitGrid(float baseX, float baseY, SpriteBatchNode.GroupTypes batchNodeAlien, SpriteBatchNode.GroupTypes batchNodeBox)
        {
           
            AlienGrid gameGrid = new AlienGrid(GameObject.GOName.AlienGrid, Sprite.SpriteName.NULL,0, 0);
            GameObjectNodeManager.Add(gameGrid);
            
            for (int i = 0; i < 11; i++)
            {
                AlienColumn aCol = (AlienColumn)RecycleCreate(GameObject.GOName.AlienColumn, batchNodeAlien, batchNodeBox, 0, 0);
                aCol.Add(RecycleCreate(GameObject.GOName.Octupus, batchNodeAlien, batchNodeBox, baseX + i * 60.0f, baseY));
                aCol.Add(RecycleCreate(GameObject.GOName.Octupus, batchNodeAlien, batchNodeBox, baseX + i * 60.0f, (baseY - 60)));
                aCol.Add(RecycleCreate(GameObject.GOName.Crab, batchNodeAlien, batchNodeBox, baseX + i * 60.0f, (baseY - 60 * 2)));
                aCol.Add(RecycleCreate(GameObject.GOName.Crab, batchNodeAlien, batchNodeBox, baseX + i * 60.0f, (baseY - 60 * 3)));
                aCol.Add(RecycleCreate(GameObject.GOName.Squid, batchNodeAlien, batchNodeBox, baseX + i * 60.0f, (baseY - 60 * 4)));
                gameGrid.Add(aCol);
            }
        }

        
        public static void ResetGrid(float baseX, float baseY, SpriteBatchNode.GroupTypes batchNodeAlien, SpriteBatchNode.GroupTypes batchNodeBox)
        {
            AlienGrid gameGrid = (AlienGrid)GameObjectNodeManager.Find(GameObject.GOName.AlienGrid);
            for (int i = 0; i < 11; i++)
            {
                AlienColumn aCol = (AlienColumn)RecycleCreate(GameObject.GOName.AlienColumn, batchNodeAlien, batchNodeBox, 0, 0);
                aCol.Add(RecycleCreate(GameObject.GOName.Octupus, batchNodeAlien, batchNodeBox, baseX + i * 60.0f, baseY));
                aCol.Add(RecycleCreate(GameObject.GOName.Octupus, batchNodeAlien, batchNodeBox, baseX + i * 60.0f, (baseY - 60)));
                aCol.Add(RecycleCreate(GameObject.GOName.Crab, batchNodeAlien, batchNodeBox, baseX + i * 60.0f, (baseY - 60 * 2)));
                aCol.Add(RecycleCreate(GameObject.GOName.Crab, batchNodeAlien, batchNodeBox, baseX + i * 60.0f, (baseY - 60 * 3)));
                aCol.Add(RecycleCreate(GameObject.GOName.Squid, batchNodeAlien, batchNodeBox, baseX + i * 60.0f, (baseY - 60 * 4)));
                gameGrid.Add(aCol);
            }

        }
    }
}
