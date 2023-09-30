using System.Diagnostics;

namespace SpaceInvaders
{
    class ShieldFactory
    {
        
        static SpriteBatchNode.GroupTypes batchNodeShield;
        static SpriteBatchNode.GroupTypes batchNodeCollisionBox;

        
        public static void SetBatchnodes(SpriteBatchNode.GroupTypes shieldNode, SpriteBatchNode.GroupTypes boxNode)
        {
            batchNodeShield = shieldNode;
            batchNodeCollisionBox = boxNode;
        }

        

        public static GameObject CreateNew(ShieldCategory.ShieldType shieldType, GameObject.GOName gameObjName, float x = 0.0f, float y = 0.0f)
        {
            GameObject g = null;
            switch (shieldType)
            {
                case ShieldCategory.ShieldType.Brick:
                    g = new Brick(gameObjName, Sprite.SpriteName.Brick, x, y);
                    break;
                case ShieldCategory.ShieldType.LeftTop0:
                    g = new Brick(gameObjName, Sprite.SpriteName.BrickLeftTop0, x, y);
                    break;
                case ShieldCategory.ShieldType.LeftTop1:
                    g = new Brick(gameObjName, Sprite.SpriteName.BrickLeftTop1, x, y);
                    break;
                case ShieldCategory.ShieldType.LeftBottom:
                    g = new Brick(gameObjName, Sprite.SpriteName.BrickLeftBottom, x, y);
                    break;

                case ShieldCategory.ShieldType.RightTop0:
                    g = new Brick(gameObjName, Sprite.SpriteName.BrickRightTop0, x, y);
                    break;
                case ShieldCategory.ShieldType.RightTop1:
                    g = new Brick(gameObjName, Sprite.SpriteName.BrickRightTop1, x, y);
                    break;
                case ShieldCategory.ShieldType.RightBottom:
                    g = new Brick(gameObjName, Sprite.SpriteName.BrickRightBottom, x, y);
                    break;
                case ShieldCategory.ShieldType.Grid:
                    g = new Shield(gameObjName, Sprite.SpriteName.NULL, x, y);
                    break;
                case ShieldCategory.ShieldType.Column:
                    g = new BrickColumn(gameObjName, Sprite.SpriteName.NULL, x, y);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            g.ActivateSprite(batchNodeShield);
            g.ActivateCollisionSprite(batchNodeCollisionBox);
            return g;
        }

        public static GameObject Create(ShieldCategory.ShieldType shieldType, GameObject.GOName gameObjName, float x = 0.0f, float y = 0.0f)
        {
            GameObjectNode shieldObjs = GhostManager.Find(gameObjName);
            GameObject shieldParts = null;
            if (shieldObjs != null)
            {
                shieldParts = shieldObjs.gameObj;
                GhostManager.Remove(shieldObjs);
                shieldParts.x = x;
                shieldParts.y = y;
                shieldParts.Revive();
                shieldParts.ActivateSprite(ShieldFactory.batchNodeShield);
                shieldParts.ActivateCollisionSprite(ShieldFactory.batchNodeCollisionBox);
            }
            else
            {
                shieldParts = CreateNew(shieldType, gameObjName, x, y);
            }
            return shieldParts;
        }

        public static ShieldRoot CreateShieldRoot(GameObject.GOName gameObjName, float x = 0.0f, float y = 0.0f)
        {
            ShieldRoot g = new ShieldRoot(gameObjName, Sprite.SpriteName.NULL, x, y);
            return g;
        }

        public static GameObject CreateSingleShield(GameObject.GOName name, float xStart, float yStart, ShieldRoot root = null)
        {
            ShieldRoot shieldRoot;
            if (root != null)
            {
                shieldRoot = root;
            }
            else
            {
                shieldRoot = CreateShieldRoot(name, 0, 0);
            }
            float start_x = xStart;
            float start_y = yStart;
            float off_x = 0;
            float brickWidth = 30.0f;
            float brickHeight = 15.0f;

            //Create the left starting column first
            int j = 0;
            BrickColumn column = (BrickColumn)Create(ShieldCategory.ShieldType.Column, GameObject.GOName.ShieldColumn0 + j++);
            column.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x, start_y));
            column.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x, start_y + brickHeight));
            column.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x, start_y + 2 * brickHeight));
            column.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x, start_y + 3 * brickHeight));
            column.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x, start_y + 4 * brickHeight));
            column.Add(Create(ShieldCategory.ShieldType.LeftTop1, GameObject.GOName.LeftTop1, start_x, start_y + 5 * brickHeight));
            column.Add(Create(ShieldCategory.ShieldType.LeftTop0, GameObject.GOName.LeftTop0, start_x, start_y + 6 * brickHeight));

            shieldRoot.Add(column);

            j = 1;
            off_x += brickWidth;
            BrickColumn column2 = (BrickColumn)Create(ShieldCategory.ShieldType.Column, GameObject.GOName.ShieldColumn0 + j++);
            column2.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y));
            column2.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 1 * brickHeight));
            column2.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 2 * brickHeight));
            column2.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 3 * brickHeight));
            column2.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 4 * brickHeight));
            column2.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 5 * brickHeight));
            column2.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 6 * brickHeight));
            column2.Add(Create(ShieldCategory.ShieldType.LeftTop1, GameObject.GOName.LeftTop1, start_x + off_x, start_y + 7 * brickHeight));
            column2.Add(Create(ShieldCategory.ShieldType.LeftTop0, GameObject.GOName.LeftTop0, start_x + off_x, start_y + 8 * brickHeight));
            shieldRoot.Add(column2);

            j = 2;
            off_x += brickWidth;
            BrickColumn column3 = (BrickColumn)Create(ShieldCategory.ShieldType.Column, GameObject.GOName.ShieldColumn0 + j++);
            column3.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 2 * brickHeight));
            column3.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 3 * brickHeight));
            column3.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 4 * brickHeight));
            column3.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 5 * brickHeight));
            column3.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 6 * brickHeight));
            column3.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 7 * brickHeight));
            column3.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 8 * brickHeight));
            shieldRoot.Add(column3);

            j = 3;
            off_x += brickWidth;
            BrickColumn column4 = (BrickColumn)Create(ShieldCategory.ShieldType.Column, GameObject.GOName.ShieldColumn0 + j++);
            column4.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 2 * brickHeight));
            column4.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 3 * brickHeight));
            column4.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 4 * brickHeight));
            column4.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 5 * brickHeight));
            column4.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 6 * brickHeight));
            column4.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 7 * brickHeight));
            column4.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 8 * brickHeight));
            shieldRoot.Add(column4);

            j = 4;
            off_x += brickWidth;
            BrickColumn column5 = (BrickColumn)Create(ShieldCategory.ShieldType.Column, GameObject.GOName.ShieldColumn0 + j++);
            column5.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y));
            column5.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 1 * brickHeight));
            column5.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 2 * brickHeight));
            column5.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 3 * brickHeight));
            column5.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 4 * brickHeight));
            column5.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 5 * brickHeight));
            column5.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 6 * brickHeight));
            column5.Add(Create(ShieldCategory.ShieldType.RightTop1, GameObject.GOName.RightTop1, start_x + off_x, start_y + 7 * brickHeight));
            column5.Add(Create(ShieldCategory.ShieldType.RightTop0, GameObject.GOName.RightTop0, start_x + off_x, start_y + 8 * brickHeight));
            shieldRoot.Add(column5);

            j = 5;
            off_x += brickWidth;
            BrickColumn column6 = (BrickColumn)Create(ShieldCategory.ShieldType.Column, GameObject.GOName.ShieldColumn0 + j++);
            column6.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y));
            column6.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + brickHeight));
            column6.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 2 * brickHeight));
            column6.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 3 * brickHeight));
            column6.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 4 * brickHeight));
            column6.Add(Create(ShieldCategory.ShieldType.RightTop1, GameObject.GOName.RightTop1, start_x + off_x, start_y + 5 * brickHeight));
            column6.Add(Create(ShieldCategory.ShieldType.RightTop0, GameObject.GOName.RightTop0, start_x + off_x, start_y + 6 * brickHeight));
            shieldRoot.Add(column6);

            return shieldRoot;
        }

        public static void ResetShield(GameObject.GOName shieldRootName, float xStart, float yStart)
        {
            ShieldRoot shieldRoot = (ShieldRoot)GameObjectNodeManager.Find(shieldRootName);
            float start_x = xStart;
            float start_y = yStart;
            float off_x = 0;
            float brickWidth = 30.0f;
            float brickHeight = 15.0f;

            //Create the left starting column first
            int j = 0;
            BrickColumn column = (BrickColumn)Create(ShieldCategory.ShieldType.Column, GameObject.GOName.ShieldColumn0 + j++);
            column.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x, start_y));
            column.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x, start_y + brickHeight));
            column.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x, start_y + 2 * brickHeight));
            column.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x, start_y + 3 * brickHeight));
            column.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x, start_y + 4 * brickHeight));
            column.Add(Create(ShieldCategory.ShieldType.LeftTop1, GameObject.GOName.LeftTop1, start_x, start_y + 5 * brickHeight));
            column.Add(Create(ShieldCategory.ShieldType.LeftTop0, GameObject.GOName.LeftTop0, start_x, start_y + 6 * brickHeight));

            shieldRoot.Add(column);

            j = 1;
            off_x += brickWidth;
            BrickColumn column2 = (BrickColumn)Create(ShieldCategory.ShieldType.Column, GameObject.GOName.ShieldColumn0 + j++);
            column2.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y));
            column2.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 1 * brickHeight));
            column2.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 2 * brickHeight));
            column2.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 3 * brickHeight));
            column2.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 4 * brickHeight));
            column2.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 5 * brickHeight));
            column2.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 6 * brickHeight));
            column2.Add(Create(ShieldCategory.ShieldType.LeftTop1, GameObject.GOName.LeftTop1, start_x + off_x, start_y + 7 * brickHeight));
            column2.Add(Create(ShieldCategory.ShieldType.LeftTop0, GameObject.GOName.LeftTop0, start_x + off_x, start_y + 8 * brickHeight));
            shieldRoot.Add(column2);

            j = 2;
            off_x += brickWidth;
            BrickColumn column3 = (BrickColumn)Create(ShieldCategory.ShieldType.Column, GameObject.GOName.ShieldColumn0 + j++);
            column3.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 2 * brickHeight));
            column3.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 3 * brickHeight));
            column3.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 4 * brickHeight));
            column3.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 5 * brickHeight));
            column3.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 6 * brickHeight));
            column3.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 7 * brickHeight));
            column3.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 8 * brickHeight));
            shieldRoot.Add(column3);

            j = 3;
            off_x += brickWidth;
            BrickColumn column4 = (BrickColumn)Create(ShieldCategory.ShieldType.Column, GameObject.GOName.ShieldColumn0 + j++);
            column4.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 2 * brickHeight));
            column4.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 3 * brickHeight));
            column4.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 4 * brickHeight));
            column4.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 5 * brickHeight));
            column4.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 6 * brickHeight));
            column4.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 7 * brickHeight));
            column4.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 8 * brickHeight));
            shieldRoot.Add(column4);

            j = 4;
            off_x += brickWidth;
            BrickColumn column5 = (BrickColumn)Create(ShieldCategory.ShieldType.Column, GameObject.GOName.ShieldColumn0 + j++);
            column5.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y));
            column5.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 1 * brickHeight));
            column5.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 2 * brickHeight));
            column5.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 3 * brickHeight));
            column5.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 4 * brickHeight));
            column5.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 5 * brickHeight));
            column5.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 6 * brickHeight));
            column5.Add(Create(ShieldCategory.ShieldType.RightTop1, GameObject.GOName.RightTop1, start_x + off_x, start_y + 7 * brickHeight));
            column5.Add(Create(ShieldCategory.ShieldType.RightTop0, GameObject.GOName.RightTop0, start_x + off_x, start_y + 8 * brickHeight));
            shieldRoot.Add(column5);

            j = 5;
            off_x += brickWidth;
            BrickColumn column6 = (BrickColumn)Create(ShieldCategory.ShieldType.Column, GameObject.GOName.ShieldColumn0 + j++);
            column6.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y));
            column6.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + brickHeight));
            column6.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 2 * brickHeight));
            column6.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 3 * brickHeight));
            column6.Add(Create(ShieldCategory.ShieldType.Brick, GameObject.GOName.Brick, start_x + off_x, start_y + 4 * brickHeight));
            column6.Add(Create(ShieldCategory.ShieldType.RightTop1, GameObject.GOName.RightTop1, start_x + off_x, start_y + 5 * brickHeight));
            column6.Add(Create(ShieldCategory.ShieldType.RightTop0, GameObject.GOName.RightTop0, start_x + off_x, start_y + 6 * brickHeight));
            shieldRoot.Add(column6);
        }
    }
}

