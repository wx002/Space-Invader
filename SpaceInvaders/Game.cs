using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpaceInvaders : Azul.Game
    {
        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {
            // Game Window Device setup
            this.SetWindowName("Space Invadar");
            this.SetWidthHeight(1200, 800);
            this.SetClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }

        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------
        public override void LoadContent()
        {
            // Manager init
            Simulation.Create();
            TextureManager.CreateTextureManager(1,1);
            ImageManager.CreateImageManager(1,1);
            SpriteManager.CreateSpriteManager(1,1);
            BoxSpriteManager.CreateSpriteBoxManager(1,1);
            ProxySpriteManager.CreateProxySpriteManager(1,1);
            ProxyBoxSpriteManager.CreateProxyBoxSpriteManager(1,1);
            GlyphManager.CreateGlyphManager(1,1);
            FontManager.CreateFontManager(1,1);
            InputManager.InitController();

            //create the sound engine singleton
            SoundEngine.CreateSoundEngine();

            // Load the supplied Texture files
            string alien = "FinalInvadars.tga";
            string bird = "Birds.tga";
            string font = "Consolas36pt.tga";
            string birdsPlus = "Birds_Ship_Shield.tga";

            TextureManager.Add(Texture.TextureName.Alien, alien);
            TextureManager.Add(Texture.TextureName.Birds, bird);
            TextureManager.Add(Texture.TextureName.Font, font);
            TextureManager.Add(Texture.TextureName.More, birdsPlus);
            TextureManager.Add(Texture.TextureName.MorePlus, "Shield.tga");


            GlyphManager.LoadGlyphFromXML(Glyph.GlyphName.Consolas36pt, "Consolas36pt.xml", Texture.TextureName.Font);

            //Aliens
            ImageManager.Add(Image.ImageName.OpenCrab, Texture.TextureName.Alien, 317, 20, 165, 127);
            ImageManager.Add(Image.ImageName.CloseCrab, Texture.TextureName.Alien, 312, 178, 179, 128);
            ImageManager.Add(Image.ImageName.CloseOctupus, Texture.TextureName.Alien, 609, 177, 128, 125);
            ImageManager.Add(Image.ImageName.OpenOctupus, Texture.TextureName.Alien, 608, 20, 131, 129);
            ImageManager.Add(Image.ImageName.OpenSquid, Texture.TextureName.Alien, 51, 22, 179, 128);
            ImageManager.Add(Image.ImageName.CloseSquid, Texture.TextureName.Alien, 53, 176, 178, 126);
            ImageManager.Add(Image.ImageName.AlienDeath, Texture.TextureName.Alien, 563, 482, 219, 129);
            ImageManager.Add(Image.ImageName.UFO, Texture.TextureName.Alien, 77, 491, 246, 117);

            //Alien Bombs
            ImageManager.Add(Image.ImageName.BombStraight, Texture.TextureName.More, 109, 99, 9, 53);
            ImageManager.Add(Image.ImageName.BombZigZag, Texture.TextureName.More, 129, 97, 26, 54);
            ImageManager.Add(Image.ImageName.BombDagger, Texture.TextureName.More, 217, 103, 22, 50);

            //Shields
            //Shield Images
            ImageManager.Add(Image.ImageName.Brick, Texture.TextureName.MorePlus, 20, 210, 10, 5);
            ImageManager.Add(Image.ImageName.BrickLeftTop0, Texture.TextureName.MorePlus, 15, 180, 10, 5);
            ImageManager.Add(Image.ImageName.BrickLeftTop1, Texture.TextureName.MorePlus, 15, 185, 10, 5);
            ImageManager.Add(Image.ImageName.BrickLeftBottom, Texture.TextureName.MorePlus, 35, 215, 10, 5);
            ImageManager.Add(Image.ImageName.BrickRightTop0, Texture.TextureName.MorePlus, 75, 180, 10, 5);
            ImageManager.Add(Image.ImageName.BrickRightTop1, Texture.TextureName.MorePlus, 75, 185, 10, 5);
            ImageManager.Add(Image.ImageName.BrickRightBottom, Texture.TextureName.MorePlus, 55, 215, 10, 5);

            //Shield Sprites
            SpriteManager.Add(Sprite.SpriteName.Brick, Image.ImageName.Brick, 50, 25, 30, 15);
            SpriteManager.Add(Sprite.SpriteName.BrickLeftTop0, Image.ImageName.BrickLeftTop0, 50, 25, 30, 15);
            SpriteManager.Add(Sprite.SpriteName.BrickLeftTop1, Image.ImageName.BrickLeftTop1, 50, 25, 30, 15);
            SpriteManager.Add(Sprite.SpriteName.BrickLeftBottom, Image.ImageName.BrickLeftBottom, 50, 25, 30, 15);
            SpriteManager.Add(Sprite.SpriteName.BrickRightTop0, Image.ImageName.BrickRightTop0, 50, 25, 30, 15);
            SpriteManager.Add(Sprite.SpriteName.BrickRightTop1, Image.ImageName.BrickRightTop1, 50, 25, 30, 15);
            SpriteManager.Add(Sprite.SpriteName.BrickRightBottom, Image.ImageName.BrickRightBottom, 50, 25, 30, 15);

            //Missile Image
            ImageManager.Add(Image.ImageName.Missile, Texture.TextureName.More, 73, 53, 5, 4);

            //Ship Image
            ImageManager.Add(Image.ImageName.Ship, Texture.TextureName.More, 10, 93, 30, 18);

            //Real sprites

            //Missile - Ship
            SpriteManager.Add(Sprite.SpriteName.Ship, Image.ImageName.Ship, 500, 100, 80, 28);
            SpriteManager.Add(Sprite.SpriteName.Missile, Image.ImageName.Missile, 0, 0, 5, 40);

            //Aliens
            SpriteManager.Add(Sprite.SpriteName.Crab, Image.ImageName.OpenCrab, 100, 500, 50, 50);
            SpriteManager.Add(Sprite.SpriteName.Octupus, Image.ImageName.OpenOctupus, 100, 390, 50, 50);
            SpriteManager.Add(Sprite.SpriteName.Squid, Image.ImageName.OpenSquid, 100, 280, 50, 50);
            SpriteManager.Add(Sprite.SpriteName.AlienDeath, Image.ImageName.AlienDeath, 100, 280, 50, 50);
            SpriteManager.Add(Sprite.SpriteName.UFO, Image.ImageName.UFO, 100, 280, 50, 50).SwapColor(255,0,0);

            //Bombs
            SpriteManager.Add(Sprite.SpriteName.BombStraight, Image.ImageName.BombStraight, 100, 500, 20, 40);
            SpriteManager.Add(Sprite.SpriteName.BombZigZag, Image.ImageName.BombZigZag, 100, 500, 20, 40);
            SpriteManager.Add(Sprite.SpriteName.BombDagger, Image.ImageName.BombDagger, 100, 500, 20, 40);

            SpriteManager.Add(Sprite.SpriteName.GreenCrab, Image.ImageName.OpenCrab, 100, 500, 50, 50).SwapColor(0, 255, 0);
            SpriteManager.Add(Sprite.SpriteName.GreenOctupus, Image.ImageName.OpenOctupus, 100, 390, 50, 50).SwapColor(0, 255, 0);
            SpriteManager.Add(Sprite.SpriteName.GreenSquid, Image.ImageName.OpenSquid, 100, 280, 50, 50).SwapColor(0, 255, 0);
            SpriteManager.Add(Sprite.SpriteName.GreenUFO, Image.ImageName.OpenSquid, 100, 280, 50, 50).SwapColor(0, 255, 0);

            //scene init
            SceneContext.InitSceneContext();
            
        }

        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------

        public override void Update()
        {
            GlobalTimer.Update(this.GetTime());
            if (Simulation.GetTimeStep() > 0.0f)
            {
                TimeEventManager.ExcuteTimeEvents(Simulation.GetTotalTime());
                GameObjectNodeManager.Update();
            }
            SceneContext.GetSceneState().Update(this.GetTime());

        }

        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
            SceneContext.GetSceneState().Draw();
        }

        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {

        }

    }
}

