using System.Diagnostics;

namespace SpaceInvaders
{
    class SceneContext
    {
        public enum SceneTypes
        {
            Play,
            GameOver,
            Select,
            PlayP2
        }

        public static SceneContext MainContext = null;
        static SceneState sceneState;
        static ScenePlayGame playGame;
        static ScenePlayGameP2 playGameP2;
        static SceneSelect select;
        static SceneGameOver gameOver;

        public SceneContext()
        {
            select = new SceneSelect();
            gameOver = new SceneGameOver();
            
            sceneState = select;
            sceneState.Entering();
        }

        public static void InitSceneContext()
        {
            if(MainContext == null)
            {
                MainContext = new SceneContext();
            }
        }

        public static SceneState GetSceneState()
        {
            return sceneState;
        }

        public static void SetSceneState(SceneTypes type)
        {
            switch (type)
            {
                case SceneTypes.Play:
                    if(playGame == null)
                    {
                        playGame = new ScenePlayGame();
                        playGame.timePause = 0.0f;
                    }
                    sceneState.Leaving();
                    sceneState = playGame;
                    sceneState.Entering();
                    break;
                case SceneTypes.Select:
                    sceneState.Leaving();
                    sceneState = select;
                    sceneState.Entering();
                    break;
                case SceneTypes.GameOver:
                    sceneState.Leaving();
                    sceneState = gameOver;
                    sceneState.Entering();
                    break;
                case SceneTypes.PlayP2:
                    if (playGameP2 == null)
                    {
                        playGameP2 = new ScenePlayGameP2();
                        playGameP2.timePause = 0.0f;
                    }
                    sceneState.Leaving();
                    sceneState = playGameP2;
                    sceneState.Entering();
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        public static void Reset()
        {
            playGame = null;
            playGameP2 = null;
        }
    }
}
