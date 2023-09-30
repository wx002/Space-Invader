using System.Diagnostics;
namespace SpaceInvaders
{
    class ShipMissileReady : ShipMissileState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetShipState(ShipManager.MissileStates.MissileFlying);
        }

        public override void ShootMissile(Ship pShip)
        {
            Missile pMissile = ShipManager.ActivateMissile();
            pMissile.SetPosition(pShip.x, pShip.y + 20);
            SoundEngine.PlaySound(SoundEngine.Sound.Shoot);
            this.Handle(pShip);
        }
    }
}
