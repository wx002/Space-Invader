using System.Diagnostics;
using System;

namespace SpaceInvaders
{
    public enum GameMode
    {
        Single,
        TwoPlayer
    }

    public enum Player
    {
        P1,
        P2,
        None
    }
    /// <summary>
    /// Data class that holds all game related variables 
    /// Ex: speed, aliens, score, etc
    /// </summary>

    class GameSession
    {
        public static Player player = Player.None;

        public static GameMode gameMode;
        public static int mNumAlienAliveP1;
        public static int p1NumLives;
        public static float p1MoveSpeed;
        public static int p1Score = 0;


        public static int mNumAlienAliveP2;
        public static int p2NumLives;
        public static float p2MoveSpeed;
        public static int p2Score = 0;

        public static int highSchore;

        private static GameSession session;
        private static Font p1ScoreBored;
        private static Font p2ScoreBored;

        private static Font p1LiveBored;
        private static Font p2LiveBored;

        private static Font p1NumLiveBored;
        private static Font p2NumLiveBored;

        public static Random random = new Random();

        private static int p1LiveAccumulator = 1000;
        private static int p2LiveAccumulator = 1000;


        public GameSession(GameMode mode)
        {
            gameMode = mode;
            switch (mode)
            {
                case GameMode.Single:
                    mNumAlienAliveP1 = 55;
                    mNumAlienAliveP2 = 55;
                    p1NumLives = 3;
                    p2NumLives = 0;
                    InitSpeedP1();
                    break;
                case GameMode.TwoPlayer:
                    mNumAlienAliveP1 = 55;
                    mNumAlienAliveP2 = 55;
                    p1NumLives = 3;
                    p2NumLives = 3;
                    InitSpeedP1();
                    InitSpeedP2();
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        public static void InitSession(GameMode mode)
        {
            GameSession.gameMode = mode;
            session = new GameSession(mode);
            p1Score = 0;
            p2Score = 0;
            
        }

        public static void InitSpeedP1()
        {
            p1MoveSpeed = ((150 - mNumAlienAliveP1) / 2.0f + 2.0f);
        }

        public static void InitSpeedP2()
        {
            p2MoveSpeed = ((150 - mNumAlienAliveP2) / 2.0f + 2.0f);
        }

        public static void UpdateSpeed()
        {
            switch (player)
            {
                case Player.P1:
                    if(p1MoveSpeed < 0)
                    {
                        p1MoveSpeed = ((150 - mNumAlienAliveP1) / 2.0f + 2.0f) * -1;
                    }
                    else
                    {
                        p1MoveSpeed = ((150 - mNumAlienAliveP1) / 2.0f + 2.0f);
                    }
                    break;
                case Player.P2:
                    if (p2MoveSpeed < 0)
                    {
                        p2MoveSpeed = ((150 - mNumAlienAliveP2) / 2.0f + 2.0f) * -1;
                    }
                    else
                    {
                        p2MoveSpeed = ((150 - mNumAlienAliveP2) / 2.0f + 2.0f);
                    }
                    break;
            }
        }

        public static void ChangeDirection()
        {
            switch (player)
            {
                case Player.P1:
                    p1MoveSpeed *= -1;
                    break;
                case Player.P2:
                    p2MoveSpeed *= -1;
                    break;
            }
        }

        public static int GetAliensAlive()
        {
            int numAlive = 0;
            switch (player)
            {
                case Player.P1:
                    numAlive = mNumAlienAliveP1;
                    break;
                case Player.P2:
                    numAlive = mNumAlienAliveP2;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            return numAlive;
        }

        public static float GetSpeed()
        {
            float speed = 0.0f;
            switch (player)
            {
                case Player.P1:
                    speed= p1MoveSpeed;
                    break;
                case Player.P2:
                    speed = p2MoveSpeed;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            return speed;
        }

        public static void ResetAlienCounter()
        {
            Debug.Assert(player != Player.None);
            switch (player)
            {
                case Player.P1:
                    mNumAlienAliveP1 = 55;
                    InitSpeedP1();
                    break;
                case Player.P2:
                    mNumAlienAliveP2 = 55;
                    InitSpeedP2();
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            //AnimationCommand crabAnim = new AnimationCommand(Sprite.SpriteName.Crab);
            //AnimationCommand squidAnim = new AnimationCommand(Sprite.SpriteName.Squid);
            //AnimationCommand octupusAnim = new AnimationCommand(Sprite.SpriteName.Octupus);
            //crabAnim.AddImage(Image.ImageName.CloseCrab);
            //crabAnim.AddImage(Image.ImageName.OpenCrab);
            //squidAnim.AddImage(Image.ImageName.CloseSquid);
            //squidAnim.AddImage(Image.ImageName.OpenSquid);
            //octupusAnim.AddImage(Image.ImageName.CloseOctupus);
            //octupusAnim.AddImage(Image.ImageName.OpenOctupus);
            ////add the anime
            //TimeEventManager.Add(TimeEvent.Event.SpriteAnimation, crabAnim, 0.70f);
            //TimeEventManager.Add(TimeEvent.Event.SpriteAnimation, squidAnim, 0.70f);
            //TimeEventManager.Add(TimeEvent.Event.SpriteAnimation, octupusAnim, 0.70f);
            TimeEventManager.Add(TimeEvent.Event.MovementHorizontal, new HorizontalMoveCommand(GameObject.GOName.AlienGrid), 0.75f);
            //TimeEventManager.Add(TimeEvent.Event.BombStraightEvent, new SpawnBombEventRight(), 2);
        }

        public static void SetActivePlayer(Player p)
        {
            player = p;
        }

        public static void UpdateScore(Sprite.SpriteName name)
        {
            switch (player)
            {
                case Player.P1:
                    if(p1ScoreBored == null)
                    {
                        p1ScoreBored = FontManager.Find(Font.FontName.p1ScoreDisplayP1);
                    }
                    Debug.Assert(p1ScoreBored != null);
                    p1Score += GetScoringByAliens(name);
                    p1ScoreBored.UpdateMessage(p1Score.ToString("0000"));
                    if(p1Score >= p1LiveAccumulator)
                    {
                        UpdateLives(Player.P1);
                    }
                    break;
                case Player.P2:
                    if (p2ScoreBored == null)
                    {
                        p2ScoreBored = FontManager.Find(Font.FontName.p2ScoreDisplayP2);
                    }
                    Debug.Assert(p2ScoreBored != null);
                    p2Score += GetScoringByAliens(name);
                    p2ScoreBored.UpdateMessage(p2Score.ToString("0000"));
                    if (p2Score >= p2LiveAccumulator)
                    {
                        UpdateLives(Player.P2);
                    }
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        public static void UpdateLives()
        {
            switch (player)
            {
                case Player.P1:
                    p1NumLives -= 1;
                    if(p1LiveBored == null)
                    {
                        p1LiveBored = FontManager.Find(Font.FontName.NumLivesP1);
                    }
                    Debug.Assert(p1LiveBored != null);
                    p1LiveBored.UpdateMessage(p1NumLives.ToString());
                    break;
                case Player.P2:
                    p2NumLives -= 1;
                    if (p2LiveBored == null)
                    {
                        p2LiveBored = FontManager.Find(Font.FontName.NumLivesP2);
                    }
                    Debug.Assert(p2LiveBored != null);
                    p2LiveBored.UpdateMessage(p2NumLives.ToString());
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        private static int GetScoringByAliens(Sprite.SpriteName name)
        {
            int points = 0;
            switch (name)
            {
                case Sprite.SpriteName.Octupus://30
                    points = 30;
                    break;
                case Sprite.SpriteName.Squid: //10
                    points = 10;
                    break;
                case Sprite.SpriteName.Crab: //20
                    points = 20;
                    break;
                case Sprite.SpriteName.UFO: //100
                    points = 100;
                    break;
            }
            return points;
        }

        public static void UpdateAlienCount()
        {
            switch(player){
                case Player.P1:
                    mNumAlienAliveP1 -= 1;
                    break;
                case Player.P2:
                    mNumAlienAliveP2 -= 1;
                    break;
            }
        }

        public static int GetLives(Player p)
        {
            switch (p)
            {
                case Player.P1:
                    return p1NumLives;
                case Player.P2:
                    return p2NumLives;
                default:
                    Debug.Assert(false);
                    break;
            }
            return 0;
        }

        public static void UpdateLives(Player p)
        {
            switch (p)
            {
                case Player.P1:
                    if(p1NumLiveBored == null)
                    {
                        p1NumLiveBored = FontManager.Find(Font.FontName.NumLivesP1);
                    }
                    p1NumLives += 1;
                    p1LiveAccumulator += 1000;
                    p1NumLiveBored.UpdateMessage(p1NumLives.ToString());
                    break;
                case Player.P2:
                    if (p2NumLiveBored == null)
                    {
                        p2NumLiveBored = FontManager.Find(Font.FontName.NumLivesP2);
                    }
                    p2NumLives += 1;
                    p2LiveAccumulator += 1000;
                    p2NumLiveBored.UpdateMessage(p2NumLives.ToString());
                    break;
            }
        }
    }
}
