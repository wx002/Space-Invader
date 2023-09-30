using System.Diagnostics;

namespace SpaceInvaders
{
    class GameObjectNode: DLink
    {
        public GameObject gameObj;

        public GameObjectNode() : base()
        {
            Clean();
        }

        public void Set(GameObject g)
        {
            Debug.Assert(g != null);
            gameObj = g;
        }

        public override void Clear()
        {
            Clean();
        }

        public override object GetData()
        {
            if (gameObj != null)
            {
                return gameObj.GetData();
            }
            return null;
        }

        public override void Print()
        {
            Debug.WriteLine("GameObjectNode hash: {0}", this.GetHashCode());

            // Data:
            if (this.gameObj != null)
            {
                Debug.WriteLine("GameObject.name: {0}, hashcode: {1}", this.gameObj.GetData(), this.gameObj.GetHashCode());
            }
            else
            {
                Debug.WriteLine("GameObject is null!");
            }

            base.DLinkPrint();
        }

        private void Clean()
        {
            gameObj = null;
            base.Clear();
        }
    }
}
