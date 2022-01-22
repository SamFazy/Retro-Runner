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
        //int badGuySpeed;

        int badGuyTop;
        int badGuyBottom;
        int badGuyRight;
        int badGuyLeft;

        //Walls
        Texture2D wallTexture;
        List<Rectangle> walls;

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

            //Player
            playerSpeed = 4;
            playerRect = new Rectangle(50, 50, 30, 30);

            //Bad Guy
            badGuySpeed = new Vector2(0, 0);
            //badGuySpeed = 1;
            badGuyRect = new Rectangle(400, 400, 30, 30);

            //Walls
            walls = new List<Rectangle>();

            //End Goal
            endGoalRect = new Rectangle(960, 130, 60, 300);

            //Intro
            starTexture = Content.Load<Texture2D>("Player");
            stars = new List<Star>();

            for (int i = 0; i < 45; i++)
            {
                int size = generator.Next(10, 20);
                stars.Add(new Star(starTexture, new Rectangle(generator.Next(_graphics.PreferredBackBufferWidth - size), generator.Next(_graphics.PreferredBackBufferHeight - size), size, size), new Vector2(generator.Next(-4, 4), generator.Next(-4, 4))));
            }

            //Main Menu
            startRect = new Rectangle(355, 55, 320, 96);
            howToPlayRect = new Rectangle(115, 150, 800, 500);
            exitRect = new Rectangle(400, 350, 211, 96);

            //Level 1
            walls.Add(new Rectangle(0, 250, 350, 75));
            walls.Add(new Rectangle(450, 250, 350, 75));


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

            //Wall
            wallTexture = Content.Load<Texture2D>("Player");

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

            //Level 1

            //Lebel 2

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
            else if (screen == Screen.MainMenu)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (exitRect.Contains(mouseState.X, mouseState.Y))
                        Exit();
                }
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if(startRect.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.Level1;
                    }
                }


                starRect.Y += (int)starSpeed.Y;
                foreach (Star stars in stars)
                {
                    stars.move();
                    if (stars.Bounds.Right > _graphics.PreferredBackBufferWidth || stars.Bounds.Left < 0)
                        stars.bumpSide();
                    if (stars.Bounds.Bottom > _graphics.PreferredBackBufferHeight || stars.Bounds.Top < 0)
                        stars.bumpTopBottom();
                }

                
            }
            else if (screen == Screen.Level1)
            {
                if (playerRect.Intersects(endGoalRect))
                {
                    playerRect = new Rectangle(50, 50, 30, 30);
                    screen = Screen.Level2;
                }

            }
            else if (screen == Screen.Level2)
            {
                
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
                //badGuyRect.X -= badGuySpeed;
            }
            if (badGuyRect.X == playerRect.X)
            {
                badGuySpeed.X = badGuySpeed.X = 0;
            }

                if (badGuyRect.Y < playerRect.Y)
                {
                    badGuySpeed.Y = badGuySpeed.Y = 1;
                    //badGuyRect.Y += badGuySpeed;
                }
                if (badGuyRect.Y > playerRect.Y)
                {
                    badGuySpeed.Y = badGuySpeed.Y = -1;
                    //badGuyRect.Y -= badGuySpeed;
                }
                if (badGuyRect.Y == playerRect.Y)
                {
                    badGuySpeed.Y = badGuySpeed.Y = 0;
                }

            badGuyTop = badGuyRect.Right;
            badGuyBottom = badGuyRect.Right;
            badGuyRight = badGuyRect.Right;
            badGuyLeft = badGuyRect.Right;


            foreach (Rectangle wall in walls)
                while (badGuyRect.Intersects(wall))
                {

                    if(badGuySpeed.X == 1)
                    {
                       badGuyRect.X = badGuyRect.X - 1;
                    }
                    else if(badGuySpeed.X == -1)
                    {
                        badGuyRect.X = badGuyRect.X + 1;
                    }
                    else if(badGuySpeed.Y == 1)
                    {
                        badGuySpeed.Y = badGuySpeed.Y - 1;
                    }
                    else if(badGuySpeed.Y == -1)
                    {
                        badGuySpeed.Y = badGuySpeed.Y + 1;
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
                _spriteBatch.Draw(playerTexture, playerRect, Color.DeepSkyBlue);
                _spriteBatch.Draw(badGuyTexture, badGuyRect, Color.Red);


            }
            else if (screen == Screen.Level2)
            {
                _spriteBatch.Draw(endGoalTexture, endGoalRect, Color.LimeGreen);
                foreach (Rectangle wall in walls)
                    _spriteBatch.Draw(wallTexture, wall, Color.White);
                _spriteBatch.Draw(playerTexture, playerRect, Color.DeepSkyBlue);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
