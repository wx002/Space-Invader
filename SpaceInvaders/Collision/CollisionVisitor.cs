using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class CollisionVisitor: DLink
    {
        public virtual void VisitGroup(AlienGrid b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by BirdGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitColumn(AlienColumn b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by BirdColumn not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitUFOColumn(UFOColumn uFOColumn)
        {
            Debug.Assert(false);
        }

        public virtual void VisitUFOBox(UFOBox uFOBox)
        {
            Debug.Assert(false);
        }

        public virtual void VisitShipRoot(ShipRoot shipRoot)
        {
            Debug.Assert(false);
        }

        public virtual void VisitBombRoot(BombRoot bombRoot)
        {
            Debug.Assert(false);
        }

        public virtual void VisitWallGroup(WallGroup wallGroup)
        {
            Debug.WriteLine("Visit by WallGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallTop(WallTop wallTop)
        {
            Debug.Assert(false);
        }

        public virtual void VisitMissile(Missile missile)
        {
            Debug.Assert(false);
        }

        public virtual void VisitShip(Ship ship)
        {
            Debug.Assert(false);
        }

        public virtual void VisitWallRight(WallRight wallRight)
        {
            Debug.WriteLine("Visit by WallRight not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallLeft(WallLeft wallLeft)
        {
            Debug.WriteLine("Visit by WallLeft not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitMissileGroup(MissileGroup missileGroup)
        {
            Debug.WriteLine("Visit by missileGroup");
            Debug.Assert(false);
        }

        public virtual void VisitOctupus(Octupus b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by RedBird not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitSquid(Squid b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by YellowBird not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitCrab(Crab b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by Octopus not implemented");
            Debug.Assert(false);
        }


        public virtual void VisitNullGameObject(GameObjectNull n)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by NullGameObject not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShield(Shield s)
        {
            Debug.WriteLine("Visit by Sheild not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBrick(Brick b)
        {
            Debug.WriteLine("Visit by brick not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBomb(Bomb bomb)
        {
            Debug.WriteLine("Visit by bomb not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShieldRoot(ShieldRoot s)
        {
            Debug.WriteLine("Visit by ShieldRoot not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBrickColumn(BrickColumn b)
        {
            Debug.WriteLine("Visit by BrickColumn not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitAlienGrid(AlienGrid ag)
        {
            Debug.WriteLine("Visit by Alien Grid not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBumperRoot(BumperRoot bump)
        {
            Debug.Assert(false);
        }

        public virtual void VisitBumperLeft(BumperLeft bump)
        {
            Debug.Assert(false);
        }

        public virtual void VisitBumperRight(BumperRight bump)
        {
            Debug.Assert(false);
        }

        public virtual void VisitBottomWall(WallBottom w)
        {
            Debug.Assert(false);
        }

        public virtual void VisitUFO(UFO u)
        {
            Debug.Assert(false);
        }

        abstract public void Accept(CollisionVisitor other);
    }
}
