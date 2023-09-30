using System.Diagnostics;


namespace SpaceInvaders
{
    class CollisionRect: Azul.Rect
    {
        public CollisionRect(float x, float y, float width, float height) : base(x, y, width, height) { }

        public CollisionRect(Azul.Rect azulRect):base(azulRect) { }

        public CollisionRect(CollisionRect collisionRect) : base(collisionRect) { }

        public CollisionRect() : base() { }

        public static bool Intersect(CollisionRect collisionRectA, CollisionRect colllisionRectB)
        {
            bool status;

            float A_minx = collisionRectA.x - collisionRectA.width / 2;
            float A_maxx = collisionRectA.x + collisionRectA.width / 2;
            float A_miny = collisionRectA.y - collisionRectA.height / 2;
            float A_maxy = collisionRectA.y + collisionRectA.height / 2;

            float B_minx = colllisionRectB.x - colllisionRectB.width / 2;
            float B_maxx = colllisionRectB.x + colllisionRectB.width / 2;
            float B_miny = colllisionRectB.y - colllisionRectB.height / 2;
            float B_maxy = colllisionRectB.y + colllisionRectB.height / 2;

            // Trivial reject
            if ((B_maxx < A_minx) || (B_minx > A_maxx) || (B_maxy < A_miny) || (B_miny > A_maxy))
            {
                status = false;
            }
            else
            {
                status = true;
            }

            return status;
        }

        public void Union(CollisionRect collisionRect)
        {
            float minX;
            float minY;
            float maxX;
            float maxY;

            if ((this.x - this.width / 2) < (collisionRect.x - collisionRect.width / 2))
            {
                minX = (this.x - this.width / 2);
            }
            else
            {
                minX = (collisionRect.x - collisionRect.width / 2);
            }

            if ((this.x + this.width / 2) > (collisionRect.x + collisionRect.width / 2))
            {
                maxX = (this.x + this.width / 2);
            }
            else
            {
                maxX = (collisionRect.x + collisionRect.width / 2);
            }

            if ((this.y + this.height / 2) > (collisionRect.y + collisionRect.height / 2))
            {
                maxY = (this.y + this.height / 2);
            }
            else
            {
                maxY = (collisionRect.y + collisionRect.height / 2);
            }

            if ((this.y - this.height / 2) < (collisionRect.y - collisionRect.height / 2))
            {
                minY = (this.y - this.height / 2);
            }
            else
            {
                minY = (collisionRect.y - collisionRect.height / 2);
            }

            this.width = (maxX - minX);
            this.height = (maxY - minY);
            this.x = minX + this.width / 2;
            this.y = minY + this.height / 2;
        }
    }
}
