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
        MouseState mouseState;
        Random generator = new Random();

        Texture2D starTexture;
        Rectangle starRect;
        Vector2 starSpeed;

        SoundEffect musicTheme;
        SoundEffectInstance musicThemeInstance;

        List<Star> stars;

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
            Level2,
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
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //Player
            playerTexture = Content.Load<Texture2D>("Player");

            //Bad Guy
            badGuyTexture = Content.Load<Texture2D>("Player");

            //Hud
            hudTexture = Content.Load<Texture2D>("Player");

            //Wall
            wallTexture = Content.Load<Texture2D>("Player");

            //Lava
            lavaTexture = Content.Load<Texture2D>("Player");

            //Door
            doorTexture = Content.Load<Texture2D>("Player");

            //Coin
            coinTexture = Content.Load<Texture2D>("Player");

            //End Goal
            endGoalTexture = Content.Load<Texture2D>("Player");

            //Intro
            logoTexture = Content.Load<Texture2D>("Retro Runner Logo");
            continueTexture = Content.Load<Texture2D>("Click to Continue");
            starTexture = Content.Load<Texture2D>("Player");

            musicTheme = Content.Load<SoundEffect>("Retro Runner Theme");
            musicThemeInstance = musicTheme.CreateInstance();

            //Main Menu
            startTexture = Content.Load<Texture2D>("Start");
            howToPlayTexture = Content.Load<Texture2D>("How To Play");
            exitTexture = Content.Load<Texture2D>("Exit");

        }

        protected override void Update(GameTime gameTime)
        {
            newState = Keyboard.GetState();
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();


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

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (exitRect.Contains(mouseState.X, mouseState.Y))
                        Exit();
                }
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (startRect.Contains(mouseState.X, mouseState.Y))
                    {
                        //Loading in Level 1

                        //Player
                        playerSpeed = 4;
                        playerRect = new Rectangle(150, 150, 30, 30);

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

            //Level 1
            else if (screen == Screen.Level1)
            {

                if (playerRect.Intersects(endGoalRect))
                {
                    playerRect = new Rectangle(50, 50, 30, 30);
                    screen = Screen.Level2;
                }

                foreach (Rectangle lava in lava)
                    while (playerRect.Intersects(lava))
                    {
                        playerRect = new Rectangle(150, 150, 30, 30);
                        badGuyRect = new Rectangle(10, 150, 30, 30);
                    }


            }

            //Level 2
            else if (screen == Screen.Level2)
            {
                walls.Clear();
            }


            if (coins.Count == 0)
            {
                doorRect = new Rectangle(-100, -100, 1, 1);
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

            //Player Movememt
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                playerRect.Y -= playerSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                playerRect.Y += playerSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                playerRect.X -= playerSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                playerRect.X += playerSpeed;
            }


            if (playerRect.Left < 0)
            {
                playerRect.X = 0;
            }
            if (playerRect.Top < 0)
            {
                playerRect.Y = 0;
            }

            if (playerRect.Bottom >= _graphics.PreferredBackBufferHeight)
            {
                playerRect.Y = _graphics.PreferredBackBufferHeight - 30;
            }

            if (playerRect.Right >= _graphics.PreferredBackBufferWidth)
            {
                playerRect.X = _graphics.PreferredBackBufferWidth - 30;
            }

            if (playerRect.Intersects(hudRect))
            {
                playerRect.Y = 100;
            }

            foreach (Rectangle wall in walls)
                while (playerRect.Intersects(wall))
                {
                    if (keyboardState.IsKeyDown(Keys.Right) == true || keyboardState.IsKeyDown(Keys.D) == true)
                    {
                        playerRect.X = playerRect.X - 1;
                    }
                    if (keyboardState.IsKeyDown(Keys.Left) == true || keyboardState.IsKeyDown(Keys.A) == true)
                    {
                        playerRect.X = playerRect.X + 1;
                    }
                    if (keyboardState.IsKeyDown(Keys.Up) == true || keyboardState.IsKeyDown(Keys.W) == true)
                    {
                        playerRect.Y = playerRect.Y + 1;
                    }
                    if (keyboardState.IsKeyDown(Keys.Down) == true || keyboardState.IsKeyDown(Keys.S) == true)
                    {
                        playerRect.Y = playerRect.Y - 1;
                    }
                }

            while (playerRect.Intersects(doorRect))
            {
                if (keyboardState.IsKeyDown(Keys.Right) == true || keyboardState.IsKeyDown(Keys.D) == true)
                {
                    playerRect.X = playerRect.X - 1;
                }
                if (keyboardState.IsKeyDown(Keys.Left) == true || keyboardState.IsKeyDown(Keys.A) == true)
                {
                    playerRect.X = playerRect.X + 1;
                }
                if (keyboardState.IsKeyDown(Keys.Up) == true || keyboardState.IsKeyDown(Keys.W) == true)
                {
                    playerRect.Y = playerRect.Y + 1;
                }
                if (keyboardState.IsKeyDown(Keys.Down) == true || keyboardState.IsKeyDown(Keys.S) == true)
                {
                    playerRect.Y = playerRect.Y - 1;
                }
            }

            for (int i = 0; i < coins.Count; i++)
            {

                if (playerRect.Intersects(coins[i]))
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
                foreach (Star stars in stars)
                    _spriteBatch.Draw(stars.Texture, stars.Bounds, Color.White);

                _spriteBatch.Draw(logoTexture, new Rectangle(115, 0, 800, 500), Color.White);
                _spriteBatch.Draw(continueTexture, new Rectangle(115, 300, 750, 450), Color.White);
            }

            else if (screen == Screen.MainMenu)
            {
                foreach (Star stars in stars)
                    _spriteBatch.Draw(stars.Texture, stars.Bounds, Color.White);

                _spriteBatch.Draw(startTexture, startRect, Color.White);
                _spriteBatch.Draw(howToPlayTexture, howToPlayRect, Color.White);
                _spriteBatch.Draw(exitTexture, exitRect, Color.White);
            }

            else if (screen == Screen.Level1)
            {
                _spriteBatch.Draw(endGoalTexture, endGoalRect, Color.LimeGreen);
                foreach (Rectangle wall in walls)
                    _spriteBatch.Draw(wallTexture, wall, Color.White);

                foreach (Rectangle lava in lava)
                    _spriteBatch.Draw(lavaTexture, lava, Color.DarkOrange);

                foreach (Rectangle coin in coins)
                    _spriteBatch.Draw(coinTexture, coin, Color.Yellow);

                _spriteBatch.Draw(playerTexture, playerRect, Color.DeepSkyBlue);
                _spriteBatch.Draw(badGuyTexture, badGuyRect, Color.Red);
                _spriteBatch.Draw(hudTexture, hudRect, Color.DarkGray);
                _spriteBatch.Draw(doorTexture, doorRect, Color.SaddleBrown);



            }
            else if (screen == Screen.Level2)
            {
                _spriteBatch.Draw(endGoalTexture, endGoalRect, Color.LimeGreen);
                foreach (Rectangle wall in walls)
                    _spriteBatch.Draw(wallTexture, wall, Color.White);
                _spriteBatch.Draw(playerTexture, playerRect, Color.DeepSkyBlue);
                _spriteBatch.Draw(hudTexture, hudRect, Color.DarkGray);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
