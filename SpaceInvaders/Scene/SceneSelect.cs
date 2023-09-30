using System.Diagnostics;

namespace SpaceInvaders
{
    class SceneSelect : SceneState
    {
        SpriteBatchManager selectBatchManager;
        TimeEventManager tInstance;
        GameObjectNodeManager gameManager;

        bool oneHistory = false;
        bool twoHistory = false;
        public SceneSelect()
        {
            Initialize();
        }
        public override void Draw()
        {
            SpriteBatchManager.Draw();
        }

        public override void Handle()
        {
            
        }

        public override void Initialize()
        {
            this.tInstance = new TimeEventManager(1, 1);
            this.gameManager = new GameObjectNodeManager(1, 1);
            this.selectBatchManager = new SpriteBatchManager(1, 1);

            TimeEventManager.SetActiveTimeEventManager(this.tInstance);
            GameObjectNodeManager.SetActiveGameObjectManager(gameManager);
            SpriteBatchManager.SetActiveBatchManager(this.selectBatchManager);
            
        }


        public override void Update(float sysTime)
        {

            /*
             Have to stick w/ this kind of input setup because game session need to be initalized based on key press
             */
            Simulation.Update(sysTime);
            if (Simulation.GetTimeStep() > 0.0f)
            {
                TimeEventManager.ExcuteTimeEvents(Simulation.GetTotalTime());
                GameObjectNodeManager.Update();
                bool oneCurrent = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_1);
                bool twoCurrent = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_2);
                if (oneCurrent && !oneHistory)
                {
                    GameSession.InitSession(GameMode.Single);
                    SceneContext.SetSceneState(SceneContext.SceneTypes.Play);
                }
                if (twoCurrent && !twoHistory)
                {
                    GameSession.InitSession(GameMode.TwoPlayer);
                    SceneContext.SetSceneState(SceneContext.SceneTypes.Play);
                }
                oneHistory = oneCurrent;
                twoHistory = twoCurrent;
            }
            
        }

        private void LoadDisplay()
        {
            TimeCharFactory.CreateTimeCharEvent("PLAY", 2.0f, 0.10f, 550, 600, 0.9f, 0.9f, 0.9f);
            TimeCharFactory.CreateTimeCharEvent("SPACE  INVADERS", 4.0f, 0.10f, 450, 500, 0.9f, 0.9f, 0.9f);

            DisplayTextCommand scoreLabelString = new DisplayTextCommand("*SCORE ADVANCE TABLE*", 400, 400);
            TimeEventManager.Add(TimeEvent.Event.SimEvent, scoreLabelString, 6.0f);

            DisplayAlienCommand alienCommand = new DisplayAlienCommand();
            TimeEventManager.Add(TimeEvent.Event.SimEvent, alienCommand, 6.5f);

            TimeCharFactory.CreateTimeCharEvent("= ? MYSTERY", 7.0f, 0.10f, 520, 300, 0.9f, 0.9f, 0.9f);
            TimeCharFactory.CreateTimeCharEvent("= 30 POINTS", 8.0f, 0.10f, 520, 250, 0.9f, 0.9f, 0.9f);
            TimeCharFactory.CreateTimeCharEvent("= 20 POINTS", 10.0f, 0.10f, 520, 200, 0.9f, 0.9f, 0.9f);
            TimeCharFactory.CreateTimeCharEvent("= 10 POINTS", 12.0f, 0.10f, 520, 150, 0.2f, 0.8f, 0.2f);
        }

        public override void Entering()
        {
            //Get the pause time
            Simulation.SetState(Simulation.State.Pause);
            float current = GlobalTimer.GetTime();
            float timePause = this.timePause;
            float deltaTime = current - timePause;

            //set the current managers to active
            TimeEventManager.SetActiveTimeEventManager(this.tInstance);
            GameObjectNodeManager.SetActiveGameObjectManager(gameManager);
            SpriteBatchManager.SetActiveBatchManager(this.selectBatchManager);

            //create batches
            SpriteBatchManager.Add(SpriteBatchNode.GroupTypes.Aliens, 5, 2, 50);
            SpriteBatchManager.Add(SpriteBatchNode.GroupTypes.Text, 5, 2, 50);

            //load the assets while is still paused
            //this way, time event manager will have events to update the delta time
            ResetFonts();
            this.LoadDisplay();

            //update the pause time, 
            TimeEventManager.PauseUpdate(deltaTime);

            
            //let the screen play
            Simulation.SetState(Simulation.State.RealTime);
            
            
        }

        public override void Leaving()
        {
            //Since deletion needs to occur, set the active batch to ensure
            // deletion don't occur on the wrong active manager
            TimeEventManager.SetActiveTimeEventManager(this.tInstance);
            GameObjectNodeManager.SetActiveGameObjectManager(gameManager);
            SpriteBatchManager.SetActiveBatchManager(this.selectBatchManager);
            SpriteBatchManager.RemoveBatch(SpriteBatchNode.GroupTypes.Text);
            SpriteBatchManager.RemoveBatch(SpriteBatchNode.GroupTypes.Aliens);
            timePause = GlobalTimer.GetTime();
            Simulation.SetState(Simulation.State.Pause);
        }

        public void ResetFonts()
        {
            FontManager.Add(Font.FontName.p1Label, SpriteBatchNode.GroupTypes.Text, "SCORE < 1 >", Glyph.GlyphName.Consolas36pt, 50, 770);
            FontManager.Add(Font.FontName.p1ScoreSelect, SpriteBatchNode.GroupTypes.Text, "0000", Glyph.GlyphName.Consolas36pt, 100, 740);

            FontManager.Add(Font.FontName.highScoreLabel, SpriteBatchNode.GroupTypes.Text, "HighScore", Glyph.GlyphName.Consolas36pt, 500, 770);
            FontManager.Add(Font.FontName.highScore, SpriteBatchNode.GroupTypes.Text, GameSession.highSchore.ToString("0000"), Glyph.GlyphName.Consolas36pt, 550, 740);

            FontManager.Add(Font.FontName.p2Label, SpriteBatchNode.GroupTypes.Text, "SCORE < 2 >", Glyph.GlyphName.Consolas36pt, 950, 770);
            FontManager.Add(Font.FontName.p2ScoreSelect, SpriteBatchNode.GroupTypes.Text, "0000", Glyph.GlyphName.Consolas36pt, 1000, 740);
        }
    }
}
