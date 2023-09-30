using System.Diagnostics;

namespace SpaceInvaders
{
    class GlobalTimer
    {
        private static GlobalTimer timer = null;
        private float currentTime;
        GlobalTimer()
        {
            currentTime = 0.0f;
        }

        public static void Update(float time)
        {
            GlobalTimer timer = GetTimer();
            timer.currentTime = time;
        }

        public static float GetTime()
        {
            GlobalTimer timer = GetTimer();
            return timer.currentTime;
        }

        private static GlobalTimer GetTimer()
        {
            if(timer == null)
            {
                timer = new GlobalTimer();
            }
            Debug.Assert(timer != null);
            return timer;
        }

    }
}
