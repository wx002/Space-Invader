using System.Diagnostics;

namespace SpaceInvaders
{
    class SceneGameOver: SceneState
    {
        SpriteBatchManager gameOverBatchManager;
        TimeEventManager tInstance;
        InputPair transition;
        public SceneGameOver()
        {
            Initialize();
            transition = new InputPair(Azul.AZUL_KEY.KEY_R, new InputSubject(), true);
            SceneTransitionObserver obs = new SceneTransitionObserver();
            obs.setSceneTo(SceneContext.SceneTypes.Select);
            transition.GetInputSubject().AddObserver(obs);
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
            this.gameOverBatchManager = new SpriteBatchManager(3, 1);
            SpriteBatchManager.SetActiveBatchManager(this.gameOverBatchManager);
            this.tInstance = new TimeEventManager(1, 1);
            TimeEventManager.SetActiveTimeEventManager(this.tInstance);
            Simulation.SetState(Simulation.State.Pause);


        }

        public override void Update(float sysTime)
        {
            //InputManager.Update();
            Simulation.Update(sysTime);
            if (Simulation.GetTimeStep() > 0.0f)
            {
                TimeEventManager.ExcuteTimeEvents(Simulation.GetTotalTime());
                GameObjectNodeManager.Update();
                CollisionPairManager.ProcessCollision();
                DelayObjectManager.Proccess();
            }
            transition.Update();
        }

        public override void Entering()
        {
            float current = GlobalTimer.GetTime();
            float timePause = this.timePause;
            float deltaTime = current - timePause;

            SpriteBatchManager.SetActiveBatchManager(this.gameOverBatchManager);
            TimeEventManager.SetActiveTimeEventManager(this.tInstance);

            //load content
            SpriteBatchManager.Add(SpriteBatchNode.GroupTypes.Text, 5, 2, 50);
            InitScoreHeader();
            LoadDisplay();

            //correct time
            TimeEventManager.PauseUpdate(deltaTime);

            
            //Reset everything
            SceneContext.Reset();

            //let screen play
            Simulation.SetState(Simulation.State.RealTime);
            
        }

        public override void Leaving()
        {
            timePause = GlobalTimer.GetTime();
            Simulation.SetState(Simulation.State.Pause);
            SpriteBatchManager.RemoveBatch(SpriteBatchNode.GroupTypes.Text);
        }

        private void InitScoreHeader()
        {
            int highScore;
            if(GameSession.p1Score > GameSession.p2Score)
            {
                highScore = GameSession.p1Score;
            }
            else
            {
                highScore = GameSession.p2Score;
            }
            if(highScore > GameSession.highSchore)
            {
                GameSession.highSchore = highScore;
            }

            FontManager.Add(Font.FontName.p1LabelFinal, SpriteBatchNode.GroupTypes.Text, "SCORE < 1 >", Glyph.GlyphName.Consolas36pt, 50, 770);
            FontManager.Add(Font.FontName.p1ScoreFinal, SpriteBatchNode.GroupTypes.Text, GameSession.p1Score.ToString("0000"), Glyph.GlyphName.Consolas36pt, 100, 740);
            
            FontManager.Add(Font.FontName.highScoreLabel, SpriteBatchNode.GroupTypes.Text, "HighScore", Glyph.GlyphName.Consolas36pt, 500, 770);
            FontManager.Add(Font.FontName.highScore, SpriteBatchNode.GroupTypes.Text, GameSession.highSchore.ToString("0000"), Glyph.GlyphName.Consolas36pt, 550, 740);

            FontManager.Add(Font.FontName.p2LabelFinal, SpriteBatchNode.GroupTypes.Text, "SCORE < 2 >", Glyph.GlyphName.Consolas36pt, 950, 770);
            FontManager.Add(Font.FontName.p2ScoreDisplayFinal, SpriteBatchNode.GroupTypes.Text, GameSession.p2Score.ToString("0000"), Glyph.GlyphName.Consolas36pt, 1000, 740);
        }

        private void LoadDisplay()
        {
            TimeCharFactory.CreateTimeCharEvent("GAME  OVER", 2.0f, 0.10f, 500, 500, 0.9f, 0.9f, 0.9f);
            TimeCharFactory.CreateTimeCharEvent("SE 453", 4.0f, 0.10f, 525, 450, 0.9f, 0.9f, 0.9f);
            TimeCharFactory.CreateTimeCharEvent("Created By: WeiJian Xu", 6.0f, 0.10f, 375, 400, 0.9f, 0.9f, 0.9f);
            TimeCharFactory.CreateTimeCharEvent("Press R TO GO BACK TO SELECT", 8.0f, 0.10f, 325, 350, 0.9f, 0.9f, 0.9f);
        }
    }
}
