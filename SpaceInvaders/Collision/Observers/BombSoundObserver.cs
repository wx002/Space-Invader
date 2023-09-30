using System.Diagnostics;

namespace SpaceInvaders
{
    class BombSoundObserver:CollisionObserver
    {
        public BombSoundObserver() { }

        public override void Notify()
        {
            SoundEngine.PlaySound(SoundEngine.Sound.Explosion);
        }

        public override void Print()
        {
            DLinkPrint();
        }
    }
}
