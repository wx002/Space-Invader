using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class SceneState
    {
        public float timePause;

        public SceneState()
        {
            timePause = GlobalTimer.GetTime();
        }

        public abstract void Handle();

        public abstract void Initialize();

        public abstract void Update(float sysTime);

        public abstract void Draw();

        public abstract void Entering();

        public abstract void Leaving();


    }
}
