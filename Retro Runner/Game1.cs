using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Retro_Runner
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        KeyboardState keyboardState;
        KeyboardState newState;
        KeyboardState oldState;

        MouseState previousMouseState;
        MouseState mouseState;
        Random generator = new Random();

        Texture2D starTexture;
        Rectangle starRect;
        Vector2 starSpeed;

        SoundEffect musicTheme;
        SoundEffectInstance musicThemeInstance;

        List<Star> stars;

        //Background
        Texture2D backgroundTexture;
        Texture2D background2Texture;

        //Intro Screen
        Texture2D logoTexture;
        Texture2D continueTexture;

        //Main Menu
        Texture2D startTexture;
        Rectangle startRect;

        Texture2D howToPlayTexture;
        Rectangle howToPlayRect;

        Texture2D exitTexture;
        Rectangle exitRect;

        //Player
        Texture2D playerTexture;
        Rectangle playerRect;
        int playerSpeed;

        //Bad Guy
        Texture2D badGuyTexture;
        Rectangle badGuyRect;
        Vector2 badGuySpeed;

        //Hud
        Texture2D hudTexture;
        Rectangle hudRect;

        //Timer
        SpriteFont timeFont;
        float timerSeconds;
        float timerStartTime;

        //Walls
        Texture2D wallTexture;
        List<Rectangle> walls;

        //Lava
        Texture2D lavaTexture;
        List<Rectangle> lava;

        //Door
        Texture2D doorTexture;
        Rectangle doorRect;

        //Coins
        Texture2D coinTexture;
        Rectangle coinRect;
        List<Rectangle> coins;

        //End Goal
        Texture2D endGoalTexture;
        Rectangle endGoalRect;

        //Lives
        Texture2D heartTexture;
        List<Rectangle> lives;
        int hp;

        //Game Over
        Texture2D gameOverTexture;
        Rectangle gameOverRect;

        //The End
        Texture2D theEndTexture;

        Player player;

        float seconds;
        float startTime;
        float cooldowntime = 0;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            screen = Screen.Intro;
        }

        enum Screen
        {
            Intro,
            MainMenu,
            Level1,
            TheEnd,
            GameOver,
        }

        Screen screen;

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            //Hud
            hudRect = new Rectangle(0, 0, 1000, 100);

            //Game Over
            gameOverRect = new Rectangle(10, 10, 500, 500);

            //Lives
            lives = new List<Rectangle>();
            hp = 3;
            
            //Walls
            walls = new List<Rectangle>();

            //Lava
            lava = new List<Rectangle>();

            //Coins
            coins = new List<Rectangle>();

            //Intro
            starTexture = Content.Load<Texture2D>("Player");
            stars = new List<Star>();

            for (int i = 0; i < 45; i++)
            {
                int size = generator.Next(10, 20);
                stars.Add(new Star(starTexture, new Rectangle(generator.Next(_graphics.PreferredBackBufferWidth - size), generator.Next(_graphics.PreferredBackBufferHeight - size), size, size), new Vector2(generator.Next(-4, 4), generator.Next(-4, 4))));
            }

            //Main Menu



            base.Initialize();
            player = new Player(playerTexture, _graphics, 150, 150);

            

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Background
            backgroundTexture = Content.Load<Texture2D>("BlueGridBackground");
            background2Texture = Content.Load<Texture2D>("BlueGridBackground2");
            //Player
            playerTexture = Content.Load<Texture2D>("Player");

            //Bad Guy
            badGuyTexture = Content.Load<Texture2D>("Player");

            //Hud
            hudTexture = Content.Load<Texture2D>("HudBackgroundTexture");

            //Timer
            timeFont = Content.Load<SpriteFont>("Timer");

            //The End
            theEndTexture = Content.Load<Texture2D>("YouWin");

            //Wall
            wallTexture = Content.Load<Texture2D>("Player");

            //Lava
            lavaTexture = Content.Load<Texture2D>("Lava");

            //Game Over
            gameOverTexture = Content.Load<Texture2D>("Over");

            //Door
            doorTexture = Content.Load<Texture2D>("Player");

            //Coin
            coinTexture = Content.Load<Texture2D>("Player");

            //End Goal
            endGoalTexture = Content.Load<Texture2D>("Player");

            //Lives
            heartTexture = Content.Load<Texture2D>("Heart");

            //Lives
            lives.Add(new Rectangle(900, 10, 80, 80));
            lives.Add(new Rectangle(800, 10, 80, 80));
            lives.Add(new Rectangle(700, 10, 80, 80));

            //Intro
            logoTexture = Content.Load<Texture2D>("Retro Runner Logo");
            continueTexture = Content.Load<Texture2D>("Click to Continue");
            starTexture = Content.Load<Texture2D>("Player");

            musicTheme = Content.Load<SoundEffect>("Retro Runner Theme");
            musicThemeInstance = musicTheme.CreateInstance();

            //Main Menu
            startTexture = Content.Load<Texture2D>("StartTexture");
            howToPlayTexture = Content.Load<Texture2D>("How To Play");
            exitTexture = Content.Load<Texture2D>("ExitTexture");

        }

        protected override void Update(GameTime gameTime)
        {
            newState = Keyboard.GetState();
            keyboardState = Keyboard.GetState();

            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            timerSeconds = (float)gameTime.TotalGameTime.TotalSeconds - timerStartTime;


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            bool pressed = KeypressSpace(Keys.Space);

            base.Update(gameTime);


            //Screens

            //Intro
            if (screen == Screen.Intro)
            {
                musicThemeInstance.Play();

                starRect.Y += (int)starSpeed.Y;
                foreach (Star stars in stars)
                {
                    stars.move();
                    if (stars.Bounds.Right > _graphics.PreferredBackBufferWidth || stars.Bounds.Left < 0)
                        stars.bumpSide();
                    if (stars.Bounds.Bottom > _graphics.PreferredBackBufferHeight || stars.Bounds.Top < 0)
                        stars.bumpTopBottom();
                }

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    screen = Screen.MainMenu;

                }

            }
            //Main Menu
            else if (screen == Screen.MainMenu)
            {
                startRect = new Rectangle(355, 55, 320, 96);
                howToPlayRect = new Rectangle(115, 150, 800, 500);
                exitRect = new Rectangle(400, 350, 211, 96);


                starRect.Y += (int)starSpeed.Y;
                foreach (Star stars in stars)
                {
                    stars.move();
                    if (stars.Bounds.Right > _graphics.PreferredBackBufferWidth || stars.Bounds.Left < 0)
                        stars.bumpSide();
                    if (stars.Bounds.Bottom > _graphics.PreferredBackBufferHeight || stars.Bounds.Top < 0)
                        stars.bumpTopBottom();
                }

                if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                {
                    if (exitRect.Contains(mouseState.X, mouseState.Y))
                        Exit();
                }
                if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                {
                    if (startRect.Contains(mouseState.X, mouseState.Y))
                    {
                        timerStartTime = (float)gameTime.TotalGameTime.TotalSeconds;
                        InitializeLevel1();
                    }
                }


            }

            //Level 1
            else if (screen == Screen.Level1)
            {
                

                if (player.Intersects(endGoalRect))
                {
                    playerRect = new Rectangle(50, 50, 30, 30);
                    screen = Screen.TheEnd;
                }



            }

            //TheEnd
            else if (screen == Screen.TheEnd)
            {
                foreach (Star stars in stars)
                {
                    stars.move();
                    if (stars.Bounds.Right > _graphics.PreferredBackBufferWidth || stars.Bounds.Left < 0)
                        stars.bumpSide();
                    if (stars.Bounds.Bottom > _graphics.PreferredBackBufferHeight || stars.Bounds.Top < 0)
                        stars.bumpTopBottom();
                }

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    player = new Player(playerTexture, _graphics, 150, 150);
                    hp = 3;
                    lives.Clear();
                    lives.Add(new Rectangle(900, 10, 80, 80));
                    lives.Add(new Rectangle(800, 10, 80, 80));
                    lives.Add(new Rectangle(700, 10, 80, 80));
                    coins.Clear();
                    coins.Add(new Rectangle(730, 170, 30, 30));
                    coins.Add(new Rectangle(50, 340, 30, 30));
                    coins.Add(new Rectangle(330, 555, 30, 30));
                    coins.Add(new Rectangle(370, 290, 30, 30));
                    screen = Screen.MainMenu;

                }
            }

            //Game Over
            else if (screen == Screen.GameOver)
            {

                foreach (Star stars in stars)
                {
                    stars.move();
                    if (stars.Bounds.Right > _graphics.PreferredBackBufferWidth || stars.Bounds.Left < 0)
                        stars.bumpSide();
                    if (stars.Bounds.Bottom > _graphics.PreferredBackBufferHeight || stars.Bounds.Top < 0)
                        stars.bumpTopBottom();
                }

                if (mouseState.LeftButton == ButtonState.Pressed)
                {

                    hp = 3;
                    lives.Add(new Rectangle(800, 10, 80, 80));
                    lives.Add(new Rectangle(700, 10, 80, 80));
                    screen = Screen.MainMenu;

                }
            }


            if (coins.Count == 0)
            {
                doorRect = new Rectangle(-100, -100, 1, 1);
            }

            if (hp == 0)
            {
                screen = Screen.GameOver;
            }

            //Bad Guy Movement


            badGuyRect.X += (int)badGuySpeed.X;
            badGuyRect.Y += (int)badGuySpeed.Y;
            if (badGuyRect.X < playerRect.X)
            {
                badGuySpeed.X = badGuySpeed.X = 1;
            }
            if (badGuyRect.X > playerRect.X)
            {
                badGuySpeed.X = badGuySpeed.X = -1;
            }
            if (badGuyRect.X == playerRect.X)
            {
                badGuySpeed.X = badGuySpeed.X = 0;
            }
            if (badGuyRect.Y < playerRect.Y)
            {
            badGuySpeed.Y = badGuySpeed.Y = 1;

            }
            if (badGuyRect.Y > playerRect.Y)
            {
            badGuySpeed.Y = badGuySpeed.Y = -1;
            }
            if (badGuyRect.Y == playerRect.Y)
            {
            badGuySpeed.Y = badGuySpeed.Y = 0;
            }

            if (badGuyRect.Intersects(hudRect))
            {
                badGuyRect.Y = 100;
            }

            foreach (Rectangle wall in walls)
                while (badGuyRect.Intersects(wall))
                {

                    if (badGuySpeed.X == 1)
                    {
                        badGuyRect.X = badGuyRect.X - 2;
                    }
                    else if (badGuySpeed.X == -1)
                    {
                        badGuyRect.X = badGuyRect.X + 2;
                    }
                    else if (badGuySpeed.Y == 1)
                    {
                        badGuySpeed.Y = badGuySpeed.Y - 2;
                    }
                    else if (badGuySpeed.Y == -1)
                    {
                        badGuySpeed.Y = badGuySpeed.Y + 2;
                    }



                }

            player.HSpeed = 0;
            player.VSpeed = 0;
            //Player Movememt
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                player.VSpeed = -4;
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                player.VSpeed = 4;
            }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                player.HSpeed = -4;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                player.HSpeed = 4;
            }
            player.Update();

            

            while (player.Intersects(hudRect))
            {
                player.Y = player.Y + 1;
            }
            foreach (Rectangle wall in walls)
                while (player.Intersects(wall))
                {
                    
                    if (keyboardState.IsKeyDown(Keys.Right) == true || keyboardState.IsKeyDown(Keys.D) == true)
                    {
                        player.X = player.X - 1;
                        
                    }
                    else if (keyboardState.IsKeyDown(Keys.Left) == true || keyboardState.IsKeyDown(Keys.A) == true)
                    {
                        player.X = player.X + 1;
                    }
                    if (keyboardState.IsKeyDown(Keys.Up) == true || keyboardState.IsKeyDown(Keys.W) == true)
                    {
                        player.Y = player.Y + 1;
                    }
                    else if (keyboardState.IsKeyDown(Keys.Down) == true || keyboardState.IsKeyDown(Keys.S) == true)
                    {
                        player.Y = player.Y - 1;
                    }

                }

            while (player.Intersects(doorRect))
            {
                if (keyboardState.IsKeyDown(Keys.Right) == true || keyboardState.IsKeyDown(Keys.D) == true)
                {
                    player.X = player.X - 1;
                }
                else if (keyboardState.IsKeyDown(Keys.Left) == true || keyboardState.IsKeyDown(Keys.A) == true)
                {
                    player.X = player.X + 1;
                }
                if (keyboardState.IsKeyDown(Keys.Up) == true || keyboardState.IsKeyDown(Keys.W) == true)
                {
                    player.Y = player.Y + 1;
                }
                else if (keyboardState.IsKeyDown(Keys.Down) == true || keyboardState.IsKeyDown(Keys.S) == true)
                {
                    player.Y = player.Y - 1;
                }
            }

            foreach (Rectangle lava in lava)
                while (player.Intersects(lava))
                {
                    player = new Player(playerTexture, _graphics, 150, 150);
                    badGuyRect = new Rectangle(10, 150, 30, 30);
                    hp = hp - 1;


                    if (lives.Count >= 2)
                    {
                        int i = 1;

                        lives.RemoveAt(i);
                        i--;
                        
                    }
                    

                }

            for (int i = 0; i < coins.Count; i++)
            {

                if (player.Intersects(coins[i]))
                {
                    coins.RemoveAt(i);
                    i--;
                }
            }


            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            cooldowntime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (pressed == true && cooldowntime >= 1)
            {
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                playerSpeed = 8;
                cooldowntime = 0;
            }

            if (seconds >= .20)
            {
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                playerSpeed = 4;
            }


            oldState = newState;
        }

        private bool KeypressSpace(Keys theKey)
        {
            if (newState.IsKeyUp(theKey) && oldState.IsKeyDown(theKey))
                return true;

            return false;

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black); 

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 1000, 620), Color.White);
                foreach (Star stars in stars)
                    _spriteBatch.Draw(stars.Texture, stars.Bounds, Color.White);

                _spriteBatch.Draw(logoTexture, new Rectangle(115, 0, 800, 500), Color.White);
                _spriteBatch.Draw(continueTexture, new Rectangle(115, 300, 750, 450), Color.White);
            }

            else if (screen == Screen.MainMenu)
            {
                _spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 1000, 620), Color.Green);
                foreach (Star stars in stars)
                    _spriteBatch.Draw(stars.Texture, stars.Bounds, Color.White);

                _spriteBatch.Draw(startTexture, startRect, Color.White);
                _spriteBatch.Draw(howToPlayTexture, howToPlayRect, Color.White);
                _spriteBatch.Draw(exitTexture, exitRect, Color.White);
            }

            else if (screen == Screen.Level1)
            {
                _spriteBatch.Draw(background2Texture, new Rectangle(0, 0, 1000, 600), Color.Blue);
                _spriteBatch.Draw(endGoalTexture, endGoalRect, Color.LimeGreen);
                foreach (Rectangle wall in walls)
                    _spriteBatch.Draw(wallTexture, wall, Color.DarkSlateGray);

                foreach (Rectangle lava in lava)
                    _spriteBatch.Draw(lavaTexture, lava, Color.DarkOrange);

                foreach (Rectangle coin in coins)
                    _spriteBatch.Draw(coinTexture, coin, Color.Yellow);

                player.Draw(_spriteBatch);
                _spriteBatch.Draw(badGuyTexture, badGuyRect, Color.Red);
                _spriteBatch.Draw(hudTexture, hudRect, Color.White);
                _spriteBatch.Draw(doorTexture, doorRect, Color.SaddleBrown);
                _spriteBatch.DrawString(timeFont, timerSeconds.ToString("00.00"), new Vector2(420, 10), Color.White);

                foreach (Rectangle lives in lives)
                    _spriteBatch.Draw(heartTexture, lives, Color.White);



            }
            else if (screen == Screen.TheEnd)
            {
                _spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 1000, 620), Color.White);
                foreach (Star stars in stars)
                    _spriteBatch.Draw(stars.Texture, stars.Bounds, Color.White);
                _spriteBatch.Draw(continueTexture, new Rectangle(115, 300, 750, 450), Color.White);
                _spriteBatch.Draw(theEndTexture, new Rectangle(250, -50, 500, 500), Color.White);

            }

            else if (screen == Screen.GameOver)
            {
                _spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 1000, 620), Color.Blue);
                foreach (Star stars in stars)
                    _spriteBatch.Draw(stars.Texture, stars.Bounds, Color.White);
                _spriteBatch.Draw(gameOverTexture, new Rectangle (250, -80, 500, 500), Color.White);
                _spriteBatch.Draw(continueTexture, new Rectangle(115, 300, 750, 450), Color.White);

            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }


        public void InitializeLevel1()
        {
            //Loading in Level 1

            //Bad Guy
            badGuySpeed = new Vector2(0, 0);
            badGuyRect = new Rectangle(10, 150, 30, 30);
            
            //Coins
            coins.Add(new Rectangle(730, 170, 30, 30));
            coins.Add(new Rectangle(50, 340, 30, 30));
            coins.Add(new Rectangle(330, 555, 30, 30));
            coins.Add(new Rectangle(370, 290, 30, 30));

            //Walls
            walls.Add(new Rectangle(0, 230, 700, 50));
            walls.Add(new Rectangle(240, 100, 35, 70));
            walls.Add(new Rectangle(360, 170, 35, 70));
            walls.Add(new Rectangle(480, 100, 35, 70));
            walls.Add(new Rectangle(600, 170, 35, 70));
            walls.Add(new Rectangle(800, 100, 50, 400));
            walls.Add(new Rectangle(650, 425, 50, 400));
            walls.Add(new Rectangle(520, 425, 50, 110));
            walls.Add(new Rectangle(80, 425, 50, 110));
            walls.Add(new Rectangle(80, 485, 480, 50));
            walls.Add(new Rectangle(180, 250, 50, 100));

            //Lava
            lava.Add(new Rectangle(340, 170, 20, 60));
            lava.Add(new Rectangle(460, 100, 20, 70));
            lava.Add(new Rectangle(580, 170, 20, 60));
            lava.Add(new Rectangle(585, 280, 55, 60));
            lava.Add(new Rectangle(180, 350, 50, 60));
            lava.Add(new Rectangle(300, 280, 50, 80));
            lava.Add(new Rectangle(300, 445, 85, 40));
            lava.Add(new Rectangle(425, 280, 50, 100));

            //Door
            doorRect = new Rectangle(800, 500, 50, 400);

            //End Goal
            endGoalRect = new Rectangle(960, 230, 60, 200);

            


            screen = Screen.Level1;
        }
    }
}
