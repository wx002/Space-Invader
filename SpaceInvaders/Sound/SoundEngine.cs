using System.Diagnostics;

namespace SpaceInvaders
{
    class SoundEngine
    {
        public enum Sound
        {
            Explosion,
            Shoot,
            UFO,
            vader0,
            vader1,
            vader2,
            vader3,
            Death
        }

        private static IrrKlang.ISoundEngine soundEngine = null;
        private static IrrKlang.ISoundSource explode;
        private static IrrKlang.ISoundSource shoot;
        private static IrrKlang.ISound UFO;
        private static IrrKlang.ISoundSource alienDeath;

        //March songs
        static IrrKlang.ISoundSource sndVader0 = null;
        static IrrKlang.ISoundSource sndVader1 = null;
        static IrrKlang.ISoundSource sndVader2 = null;
        static IrrKlang.ISoundSource sndVader3 = null;
        public SoundEngine(){}

        public static void CreateSoundEngine()
        {
            Debug.Assert(soundEngine == null);//ensure is null on creation and force the singleton
            if(soundEngine  == null)
            {
                soundEngine = new IrrKlang.ISoundEngine();
                soundEngine.SoundVolume = 0.2f;
                //pre load sounds
                explode = soundEngine.AddSoundSourceFromFile("explosion.wav");
                explode.DefaultVolume = 0.05f;
                shoot = soundEngine.AddSoundSourceFromFile("shoot.wav");

                //March sounds preload
                sndVader0 = soundEngine.AddSoundSourceFromFile("fastinvader1.wav");
                sndVader1 = soundEngine.AddSoundSourceFromFile("fastinvader2.wav");
                sndVader2 = soundEngine.AddSoundSourceFromFile("fastinvader3.wav");
                sndVader3 = soundEngine.AddSoundSourceFromFile("fastinvader4.wav");

                //alien death
                alienDeath = soundEngine.AddSoundSourceFromFile("invaderkilled.wav");
            }
        }

        public static void PlaySound(Sound type)
        {
            IrrKlang.ISoundEngine engine = GetEngine();
            Debug.Assert(engine != null);
            switch (type)
            {
                case Sound.Explosion:
                    soundEngine.Play2D(explode, false, false, false);
                    break;
                case Sound.Shoot:
                    soundEngine.Play2D(shoot, false, false, false);
                    break;
                case Sound.UFO:
                    if(UFO == null)
                    {
                        UFO = soundEngine.Play2D("ufo_lowpitch.wav", true);
                    }
                    else
                    {
                        UFO.Volume = 2.0f;
                    }
                    break;
                case Sound.Death:
                    soundEngine.Play2D(alienDeath, false, false, false);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        public static void EndSound(Sound type)
        {
            IrrKlang.ISoundEngine engine = GetEngine();
            Debug.Assert(engine != null);
            switch (type)
            {
                case Sound.UFO:
                    UFO.Volume = 0.0f;
                    break;
            }
        }

        public static void PlayMarch(int val)
        {
            switch (val)
            {
                case 0:
                    soundEngine.Play2D(sndVader0, false, false, false);
                    break;
                case 1:
                    soundEngine.Play2D(sndVader1, false, false, false);
                    break;
                case 2:
                    soundEngine.Play2D(sndVader2, false, false, false);
                    break;
                case 3:
                    soundEngine.Play2D(sndVader3, false, false, false);
                    break;
            }
        }


        private static IrrKlang.ISoundEngine GetEngine()
        {
            Debug.Assert(soundEngine != null);
            return soundEngine;
        }
    }
}
