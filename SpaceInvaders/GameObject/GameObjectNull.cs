using System.Diagnostics;


namespace SpaceInvaders
{
    class GameObjectNull: Leaf
    {
        // LTN - GameObjectNull
        // Null object for constrcutor delegation
        // Owner: GameObjectNull
        private static ProxySpriteNull nullProxy = new ProxySpriteNull();
        public GameObjectNull():base(GOName.NULL, Sprite.SpriteName.NULL, 0,0)
        {

        }

        public override void ComponentPrint()
        {
            //Nothing
        }

        public override void Move(float x, float y)
        {
            // do nothing
        }

        public override void Update()
        {

        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitNullGameObject(this);
        }
    }
}
