using System.Diagnostics;

namespace SpaceInvaders
{
    class ScenePlayGame : SceneState
    {
        public ScenePlayGame()
        {
            Initialize();
        }

        private SpriteBatchManager playBatchManager;
        private TimeEventManager tInstance;
        private GameObjectNodeManager activeGameManager;
        private CollisionPairManager collisionManager;
        private ShipManager shipManager;
        private DelayObjectManager delayObjManager;
        private GhostManager ghostManager;
        private Font p2Scorebored;

        public override void Initialize()
        {
            Simulation.SetState(Simulation.State.Pause);
            this.tInstance = new TimeEventManager(1, 1);
            TimeEventManager.SetActiveTimeEventManager(this.tInstance);
            this.playBatchManager = new SpriteBatchManager(3, 1);
            SpriteBatchManager.SetActiveBatchManager(this.playBatchManager);
            this.activeGameManager = new GameObjectNodeManager(5, 2);
            GameObjectNodeManager.SetActiveGameObjectManager(activeGameManager);
            this.collisionManager = new CollisionPairManager(1,1);
            CollisionPairManager.SetActiveManager(this.collisionManager);
            this.delayObjManager = new DelayObjectManager();
            DelayObjectManager.SetActiveManager(delayObjManager);
            this.shipManager = new ShipManager();
            ShipManager.SetActiveShipManager(this.shipManager);
            this.ghostManager = new GhostManager(1,1);
            GhostManager.SetActiveManager(this.ghostManager);
            GameSession.SetActivePlayer(Player.P1);


            // create the batch node
            SpriteBatchManager.Add(SpriteBatchNode.GroupTypes.Boxes, 5, 2, 50);
            SpriteBatchManager.Add(SpriteBatchNode.GroupTypes.Aliens, 5, 2, 50);
            SpriteBatchManager.Add(SpriteBatchNode.GroupTypes.UFO, 5, 2, 50);
            SpriteBatchManager.Add(SpriteBatchNode.GroupTypes.Shields, 5, 2, 50);
            SpriteBatchManager.Add(SpriteBatchNode.GroupTypes.Missiles, 5, 2, 50);
            SpriteBatchManager.Add(SpriteBatchNode.GroupTypes.Text, 5, 2, 50);
            SpriteBatchManager.Add(SpriteBatchNode.GroupTypes.Ship, 1, 1, 50);
            SpriteBatchManager.Add(SpriteBatchNode.GroupTypes.Bombs, 5, 2, 25);            
            //Aliens
            InitAliens();
            //InitUFO();
            InitFonts();
            InitWalls();
            InitMissileBombRoots();
            InitShip();
            InitBumpers();
            InitShields();
            InitShieldCollision();
            InitAlienCollision();
            InitAlienShieldCollisions();
            InitShipBombCollision();
            //InitUFOCollision();

            Simulation.SetState(Simulation.State.Pause);
            //Movement
            HorizontalMoveCommand move = new HorizontalMoveCommand(GameObject.GOName.AlienGrid);
            TimeEventManager.Add(TimeEvent.Event.MovementHorizontal, move, 1);

            //UFO
            //TimeEventManager.Add(TimeEvent.Event.UFOEvent, new SpawnUFOEvent(), 5.0f);

            SpawnBombEventRight bombsRight = new SpawnBombEventRight();
            TimeEventManager.Add(TimeEvent.Event.BombStraightEvent, bombsRight, 5.0f);

            Simulation.SetState(Simulation.State.Pause);


        }

        private void InitBumpers()
        {
            // Bumpers
            BumperRoot bumperRoot = new BumperRoot(GameObject.GOName.BumperRoot, Sprite.SpriteName.NULL, 0.0f, 0.0f);
            BumperLeft bumperLeft = new BumperLeft(GameObject.GOName.BumperLeft, Sprite.SpriteName.NULL, -10, 100, 50, 100);
            bumperLeft.ActivateCollisionSprite(SpriteBatchNode.GroupTypes.Boxes);
            BumperRight bumperRight = new BumperRight(GameObject.GOName.BumperRight, Sprite.SpriteName.NULL, 1210, 100, 50, 100);
            bumperRight.ActivateCollisionSprite(SpriteBatchNode.GroupTypes.Boxes);

            bumperRoot.Add(bumperRight);
            bumperRoot.Add(bumperLeft);
            GameObjectNodeManager.Add(bumperRoot);

            //Bumps vs ship
            CollisionPair shipBumper = CollisionPairManager.Add(CollisionPair.PairName.Ship_Bumper, GameObject.GOName.BumperRoot, GameObject.GOName.ShipRoot);
            shipBumper.AddObserver(new ShipMoveObserver());
        }


        private void InitShip()
        {
            //Ship
            ShipRoot shipRoot = new ShipRoot(GameObject.GOName.ShipRoot, Sprite.SpriteName.NULL, 0, 0);
            GameObjectNodeManager.Add(shipRoot);
        }

        private void InitShipBombCollision()
        {
            //Bomb vs ship
            CollisionPair bombShipCp = CollisionPairManager.Add(CollisionPair.PairName.Bomb_Ship, GameObject.GOName.BombRoot, GameObject.GOName.ShipRoot);
            bombShipCp.AddObserver(new RemoveBombObserver());
            bombShipCp.AddObserver(new RemoveShipObserver());
        }

        private void InitMissileBombRoots()
        {
            // Missile
            MissileGroup missileGroup = new MissileGroup();
            GameObjectNodeManager.Add(missileGroup);

            //Bombs
            BombRoot bombRoot = new BombRoot(GameObject.GOName.BombRoot, Sprite.SpriteName.NULL, 5000.0f, 5000.0f);
            GameObjectNodeManager.Add(bombRoot);

            //Missile vs Wall Collision
            CollisionPair wallMissileCp = CollisionPairManager.Add(CollisionPair.PairName.Missile_Wall, GameObject.GOName.MissileGroup, GameObject.GOName.WallGroup);
            wallMissileCp.AddObserver(new ShipRemoveMissileObserver());
            wallMissileCp.AddObserver(new ShipMissileReadyObserver());

            //Bomb vs Wall
            CollisionPair wallBombCp = CollisionPairManager.Add(CollisionPair.PairName.Erase_Wall, GameObject.GOName.BombRoot, GameObject.GOName.WallGroup);
            wallBombCp.AddObserver(new RemoveBombObserver());

            // Bomb vs Missile
            CollisionPair missileBombCp = CollisionPairManager.Add(CollisionPair.PairName.Missile_Bomb, missileGroup, bombRoot);
            missileBombCp.AddObserver(new ShipMissileReadyObserver());
            missileBombCp.AddObserver(new ShipRemoveMissileObserver());
            missileBombCp.AddObserver(new RemoveBombObserverMissile());
        }

        private void InitAlienShieldCollisions()
        {
            CollisionPair alienShield1 = CollisionPairManager.Add(CollisionPair.PairName.Alien_Shield1, GameObject.GOName.AlienGrid,
                GameObject.GOName.Shield1);
            alienShield1.AddObserver(new RemoveBrickObserver());

            CollisionPair alienShield2 = CollisionPairManager.Add(CollisionPair.PairName.Alien_Shield2, GameObject.GOName.AlienGrid,
                GameObject.GOName.Shield2);
            alienShield2.AddObserver(new RemoveBrickObserver());

            CollisionPair alienShield3 = CollisionPairManager.Add(CollisionPair.PairName.Alien_Shield3, GameObject.GOName.AlienGrid,
                GameObject.GOName.Shield3);
            alienShield3.AddObserver(new RemoveBrickObserver());

            CollisionPair alienShield4 = CollisionPairManager.Add(CollisionPair.PairName.Alien_Shield4, GameObject.GOName.AlienGrid,
                GameObject.GOName.Shield4);
            alienShield4.AddObserver(new RemoveBrickObserver());
        }

        private void InitWalls()
        {
            WallGroup wallgrp = new WallGroup(GameObject.GOName.WallGroup, Sprite.SpriteName.NULL, 0, 0);

            WallTop wallTop = new WallTop(GameObject.GOName.TopWall, Sprite.SpriteName.NULL, 600, 770, 1190, 30);
            wallTop.ActivateCollisionSprite(SpriteBatchNode.GroupTypes.Boxes);

            WallBottom wallBottom = new WallBottom(GameObject.GOName.TopWall, Sprite.SpriteName.NULL, 600, 30, 1190, 30);
            wallBottom.ActivateCollisionSprite(SpriteBatchNode.GroupTypes.Boxes);

            WallLeft wallLeft = new WallLeft(GameObject.GOName.LeftWall, Sprite.SpriteName.NULL, -10, 400, 30, 800);
            wallLeft.ActivateCollisionSprite(SpriteBatchNode.GroupTypes.Boxes);
            WallRight wallRight = new WallRight(GameObject.GOName.RightWall, Sprite.SpriteName.NULL, 1210, 400, 30, 800);
            wallRight.ActivateCollisionSprite(SpriteBatchNode.GroupTypes.Boxes);

            wallgrp.Add(wallTop);
            wallgrp.Add(wallBottom);
            wallgrp.Add(wallLeft);
            wallgrp.Add(wallRight);
            GameObjectNodeManager.Add(wallgrp);

            //Walls collision
            //Grid vs Walls
            CollisionPairManager.Add(CollisionPair.PairName.Grid_Wall,
                GameObject.GOName.AlienGrid, GameObject.GOName.WallGroup);
        }

        public override void Draw()
        {
            SpriteBatchManager.Draw();
        }




        public override void Update(float sysTime)
        {
            InputManager.GetInputController().Update();
            Simulation.Update(sysTime);
            if (Simulation.GetTimeStep() > 0.0f)
            {
                TimeEventManager.ExcuteTimeEvents(Simulation.GetTotalTime());
                CollisionPairManager.ProcessCollision();
                GameObjectNodeManager.Update();
                DelayObjectManager.Proccess();
            }
        }

        public override void Entering()
        {
            Simulation.SetState(Simulation.State.Pause);
            float current = GlobalTimer.GetTime();
            float timePause = this.timePause;
            float deltaTime = current - timePause;
            GameSession.SetActivePlayer(Player.P1);
            SpriteBatchManager.SetActiveBatchManager(this.playBatchManager);
            TimeEventManager.SetActiveTimeEventManager(this.tInstance);
            TimeEventManager.PauseUpdate(deltaTime);
            GameObjectNodeManager.SetActiveGameObjectManager(this.activeGameManager);
            CollisionPairManager.SetActiveManager(this.collisionManager);
            DelayObjectManager.SetActiveManager(delayObjManager);
            ShipManager.SetActiveShipManager(this.shipManager);
            GhostManager.SetActiveManager(this.ghostManager);           
            
            
            p2Scorebored.UpdateMessage(GameSession.p2Score.ToString("0000"));
            Simulation.SetState(Simulation.State.RealTime);
            ShipManager.ActivateShip();
        }

        public void InitFonts()
        {
            //Score
            FontManager.Add(Font.FontName.p1Label, SpriteBatchNode.GroupTypes.Text, "SCORE < 1 >", Glyph.GlyphName.Consolas36pt, 50, 770);
            FontManager.Add(Font.FontName.p1ScoreDisplayP1, SpriteBatchNode.GroupTypes.Text, GameSession.p1Score.ToString("0000"), Glyph.GlyphName.Consolas36pt, 100, 740);

            FontManager.Add(Font.FontName.highScoreLabel, SpriteBatchNode.GroupTypes.Text, "HighScore", Glyph.GlyphName.Consolas36pt, 500, 770);
            FontManager.Add(Font.FontName.highScore, SpriteBatchNode.GroupTypes.Text, GameSession.highSchore.ToString("0000"), Glyph.GlyphName.Consolas36pt, 550, 740);

            FontManager.Add(Font.FontName.p2Label, SpriteBatchNode.GroupTypes.Text, "SCORE < 2 >", Glyph.GlyphName.Consolas36pt, 950, 770);
            this.p2Scorebored = FontManager.Add(Font.FontName.p2ScoreDisplayP1, SpriteBatchNode.GroupTypes.Text, GameSession.p2Score.ToString("0000"), Glyph.GlyphName.Consolas36pt, 1000, 740);

            FontManager.Add(Font.FontName.NumLivesLabel, SpriteBatchNode.GroupTypes.Text, "Ship Lives <1>: ", Glyph.GlyphName.Consolas36pt, 0, 15);
            FontManager.Add(Font.FontName.NumLivesP1, SpriteBatchNode.GroupTypes.Text, GameSession.p1NumLives.ToString(), Glyph.GlyphName.Consolas36pt, 300, 15);
        }

        private void InitAlienCollision()
        {
            //Missile vs Alien Collision
            CollisionPair missileAlien = CollisionPairManager.Add(CollisionPair.PairName.Alien_Missile, GameObject.GOName.MissileGroup, GameObject.GOName.AlienGrid);
            missileAlien.AddObserver(new ShipMissileReadyObserver());
            missileAlien.AddObserver(new RemoveAlienObserver());
            missileAlien.AddObserver(new DeathSoundObserver());
            missileAlien.AddObserver(new ShipRemoveMissileObserver());
        }

        private void InitShields()
        {

            //Shields
            ShieldFactory.SetBatchnodes(SpriteBatchNode.GroupTypes.Shields, SpriteBatchNode.GroupTypes.Boxes);
            ShieldRoot shield1 = (ShieldRoot)ShieldFactory.CreateSingleShield(GameObject.GOName.Shield1,75, 150);
            GameObjectNodeManager.Add(shield1);
            ShieldRoot shield2 = (ShieldRoot)ShieldFactory.CreateSingleShield(GameObject.GOName.Shield2, 375, 150);
            GameObjectNodeManager.Add(shield2);
            ShieldRoot shield3 = (ShieldRoot)ShieldFactory.CreateSingleShield(GameObject.GOName.Shield3, 675, 150);
            GameObjectNodeManager.Add(shield3);
            ShieldRoot shield4 = (ShieldRoot)ShieldFactory.CreateSingleShield(GameObject.GOName.Shield4, 975, 150);
            GameObjectNodeManager.Add(shield4);            
        }

        private void InitShieldCollision()
        {
            CollisionPair shieldMissile1 = CollisionPairManager.Add(CollisionPair.PairName.Missile_Shield, GameObject.GOName.MissileGroup, GameObject.GOName.Shield1);
            shieldMissile1.AddObserver(new RemoveBrickObserver());
            shieldMissile1.AddObserver(new ShipRemoveMissileObserver());
            shieldMissile1.AddObserver(new ShipMissileReadyObserver());
            
            CollisionPair shieldMissile2 = CollisionPairManager.Add(CollisionPair.PairName.Missile_Shield, GameObject.GOName.MissileGroup, GameObject.GOName.Shield2);
            shieldMissile2.AddObserver(new RemoveBrickObserver());
            shieldMissile2.AddObserver(new ShipRemoveMissileObserver());
            shieldMissile2.AddObserver(new ShipMissileReadyObserver());
            
            CollisionPair shieldMissile3 = CollisionPairManager.Add(CollisionPair.PairName.Missile_Shield, GameObject.GOName.MissileGroup, GameObject.GOName.Shield3);
            shieldMissile3.AddObserver(new RemoveBrickObserver());
            shieldMissile3.AddObserver(new ShipRemoveMissileObserver());
            shieldMissile3.AddObserver(new ShipMissileReadyObserver());
            
            CollisionPair shieldMissile4 = CollisionPairManager.Add(CollisionPair.PairName.Missile_Shield, GameObject.GOName.MissileGroup, GameObject.GOName.Shield4);
            shieldMissile4.AddObserver(new RemoveBrickObserver());
            shieldMissile4.AddObserver(new ShipRemoveMissileObserver());
            shieldMissile4.AddObserver(new ShipMissileReadyObserver());
            
            CollisionPair BombShield = CollisionPairManager.Add(CollisionPair.PairName.Bomb_Shield, GameObject.GOName.BombRoot, GameObject.GOName.Shield1);
            BombShield.AddObserver(new RemoveBombObserver());
            BombShield.AddObserver(new RemoveBrickObserver());
            BombShield.AddObserver(new BombSoundObserver());

            CollisionPair BombShield2 = CollisionPairManager.Add(CollisionPair.PairName.Bomb_Shield, GameObject.GOName.BombRoot, GameObject.GOName.Shield2);
            BombShield2.AddObserver(new RemoveBombObserver());
            BombShield2.AddObserver(new RemoveBrickObserver());
            BombShield2.AddObserver(new BombSoundObserver());

            CollisionPair BombShield3 = CollisionPairManager.Add(CollisionPair.PairName.Bomb_Shield, GameObject.GOName.BombRoot, GameObject.GOName.Shield3);
            BombShield3.AddObserver(new RemoveBombObserver());
            BombShield3.AddObserver(new RemoveBrickObserver());
            BombShield3.AddObserver(new BombSoundObserver());

            CollisionPair BombShield4 = CollisionPairManager.Add(CollisionPair.PairName.Bomb_Shield, GameObject.GOName.BombRoot, GameObject.GOName.Shield4);
            BombShield4.AddObserver(new RemoveBombObserver());
            BombShield4.AddObserver(new RemoveBrickObserver());
            BombShield4.AddObserver(new BombSoundObserver());
        }

        public override void Leaving()
        {
            timePause = GlobalTimer.GetTime();
            Simulation.SetState(Simulation.State.Pause);
            
        }

        private void InitAliens()
        {
            
            AlienFactory.InitGrid(50.0f, 700.0f, SpriteBatchNode.GroupTypes.Aliens, SpriteBatchNode.GroupTypes.Boxes);
            AnimationCommand crabAnim = new AnimationCommand(Sprite.SpriteName.Crab);
            AnimationCommand squidAnim = new AnimationCommand(Sprite.SpriteName.Squid);
            AnimationCommand octupusAnim = new AnimationCommand(Sprite.SpriteName.Octupus);
            crabAnim.AddImage(Image.ImageName.CloseCrab);
            crabAnim.AddImage(Image.ImageName.OpenCrab);
            squidAnim.AddImage(Image.ImageName.CloseSquid);
            squidAnim.AddImage(Image.ImageName.OpenSquid);
            octupusAnim.AddImage(Image.ImageName.CloseOctupus);
            octupusAnim.AddImage(Image.ImageName.OpenOctupus);
            //add the anime
            TimeEventManager.Add(TimeEvent.Event.SpriteAnimation, crabAnim, 1.0f);
            TimeEventManager.Add(TimeEvent.Event.SpriteAnimation, squidAnim, 1.0f);
            TimeEventManager.Add(TimeEvent.Event.SpriteAnimation, octupusAnim, 1.0f);
        }


        public override void Handle()
        {
            //P1 Dies
            
            //check game mode
            if(GameSession.gameMode == GameMode.Single)
            {
                if(GameSession.GetLives(Player.P1) > 0) { 
                    SceneContext.SetSceneState(SceneContext.SceneTypes.Play);
                }
                else
                {
                    SceneContext.SetSceneState(SceneContext.SceneTypes.GameOver);
                }
            }
            else
            {
                if (GameSession.GetLives(Player.P2) > 0)
                {
                    SceneContext.SetSceneState(SceneContext.SceneTypes.PlayP2);
                }
                else if (GameSession.GetLives(Player.P2) <= 0 && GameSession.GetLives(Player.P1) > 0)
                {
                    SceneContext.SetSceneState(SceneContext.SceneTypes.Play);
                }
            }
        }

        private void InitUFO()
        {
            UFOBox ufoBox = new UFOBox(GameObject.GOName.UFOBox, Sprite.SpriteName.NULL, 5000, 5000);
            GameObjectNodeManager.Add(ufoBox);
        }

        private void InitUFOCollision()
        {
            CollisionPair ufoPair = CollisionPairManager.Add(CollisionPair.PairName.UFO_Missile, GameObject.GOName.MissileGroup, GameObject.GOName.UFOBox);
            ufoPair.AddObserver(new RemoveAlienObserver());
            ufoPair.AddObserver(new ShipRemoveMissileObserver());
            ufoPair.AddObserver(new ShipMissileReadyObserver());

            CollisionPair ufoWall = CollisionPairManager.Add(CollisionPair.PairName.UFO_Wall, GameObject.GOName.UFOBox, GameObject.GOName.WallGroup);
            ufoWall.AddObserver(new RemoveUFOObserver());
        }
    }
}
