using System.Diagnostics;


namespace SpaceInvaders
{
    class Bomb: BombCategory
    {
        public float fallSpeed;
        FallStrategy fallStrategy;

        public Bomb(GameObject.GOName name, Sprite.SpriteName spriteName, FallStrategy _pStrategy, float posX, float posY)
            : base(name, spriteName, posX, posY, BombCategory.BombTypes.Bomb)
        {
            this.x = posX;
            this.y = posY;
            this.fallSpeed = 1.0f;

            Debug.Assert(_pStrategy != null);
            this.fallStrategy = _pStrategy;

            this.fallStrategy.Reset(this.y);

            this.GetCollisionObject().collisionBox.SetColor(1, 1, 0);
        }

        public void Reset()
        {
            //this.y = this.y;
            this.fallStrategy.Reset(this.y);
        }

        public override void Remove()
        {
            this.GetCollisionObject().collisionRect.Set(0, 0, 0, 0);
            base.Update();
            GameObject parent = (GameObject)this.parent;
            parent.Update();

            base.Remove();
        }

        public override void Update()
        {
            base.Update();
            this.y -= fallSpeed;

            this.fallStrategy.Fall(this);
        }

        public void SetPosition(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void MultiplyScaling(float sx, float sy)
        {
            Debug.Assert(this.proxySprite != null);
            this.proxySprite.width *= sx;
            this.proxySprite.height *= sy;
        }

        public override void ComponentPrint()
        {
            throw new System.NotImplementedException();
        }

        public override void Move(float x, float y)
        {
            throw new System.NotImplementedException();
        }

        public override void Accept(CollisionVisitor other)
        {
            other.VisitBomb(this);
        }

        public override void VisitMissile(Missile missile)
        {
            CollisionPair pColPair = CollisionPairManager.GetActiveCollisionPair();
            pColPair.SetCollisionPair(missile, this);
            pColPair.NotifyListeners();
        }

        public float GetCollisionBoxHeight()
        {
            return this.GetCollisionObject().collisionRect.height;
        }

        public void Revive(float x, float y)
        {
            this.x = x;
            this.y = y;

            base.Revive();
        }
    }
}
