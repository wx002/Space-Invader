using System.Diagnostics;

namespace SpaceInvaders
{
    class BombFactory
    {
        public enum BombTypes
        {
            Straight,
            ZigZag,
            Dagger
        }
        public static Bomb CreateBomb(BombTypes type, GameObject.GOName gameName, float x, float y)
        {
            GameObjectNode bombNode = GhostManager.Find(gameName);
            Bomb bomb = null;
            if(bombNode != null)
            {
                bomb = (Bomb)bombNode.gameObj;
                GhostManager.Remove(bombNode);
                bomb.Revive(x,y);
                bomb.Reset();
            }
            else
            {
                FallStrategy fallType = null;
                Sprite.SpriteName spriteName = Sprite.SpriteName.Uninitialized;
                switch (type)
                {
                    case BombTypes.Straight:
                        fallType = new FallStraight();
                        spriteName = Sprite.SpriteName.BombStraight;
                        break;
                    case BombTypes.ZigZag:
                        fallType = new FallZigZag();
                        spriteName = Sprite.SpriteName.BombZigZag;
                        break;
                    case BombTypes.Dagger:
                        fallType = new FallDagger();
                        spriteName = Sprite.SpriteName.BombDagger;
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }
                bomb = new Bomb(gameName, spriteName, fallType, x, y);
            }
            return bomb;
        }

        public static Bomb CreateBombByValue(int val, float x, float y)
        {
            Bomb bomb = null;
            switch (val)
            {
                case 0:
                    bomb = BombFactory.CreateBomb(BombFactory.BombTypes.Dagger, GameObject.GOName.DaggerBomb, x, y);
                    break;
                case 1:
                    bomb = BombFactory.CreateBomb(BombFactory.BombTypes.ZigZag, GameObject.GOName.ZigZagBomb, x, y);
                    break;
                case 2:
                    bomb = BombFactory.CreateBomb(BombFactory.BombTypes.Straight, GameObject.GOName.StraightBomb, x, y);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            return bomb;
        }
    }
}
