using System.Diagnostics;

namespace SpaceInvaders
{
    class DeathSoundObserver:CollisionObserver
    {
        public DeathSoundObserver() { }

        public override void Notify()
        {
            SoundEngine.PlaySound(SoundEngine.Sound.Death);
        }

        public override void Print()
        {
            DLinkPrint();
        }
    }
}
