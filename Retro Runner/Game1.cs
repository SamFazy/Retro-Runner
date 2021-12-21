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
        MouseState mouseState;
        Random generator = new Random();

        //Intro Screen
        Texture2D logoTexture;
        Texture2D continueTexture;

        Texture2D starTexture;
        Rectangle starRect;
        Vector2 starSpeed;


        SoundEffect musicTheme;
        SoundEffectInstance musicThemeInstance;

        List<Star> stars;

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
        }

        Screen screen;

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            //Intro

            starTexture = Content.Load<Texture2D>("Player");
            stars = new List<Star>();

            for (int i = 0; i < 45; i++)
            {
                int size = generator.Next(10, 20);
                stars.Add(new Star(starTexture, new Rectangle(generator.Next(_graphics.PreferredBackBufferWidth - size), generator.Next(_graphics.PreferredBackBufferHeight - size), size, size), new Vector2(generator.Next(-4, 4), generator.Next(-4, 4))));
            }


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Intro
            logoTexture = Content.Load<Texture2D>("Retro Runner Logo");
            continueTexture = Content.Load<Texture2D>("Click to Continue");
            starTexture = Content.Load<Texture2D>("Player");

            musicTheme = Content.Load<SoundEffect>("Retro Runner Theme");
            musicThemeInstance = musicTheme.CreateInstance();
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                //_spriteBatch.Draw(starTexture, starRect, Color.White);
                foreach (Star stars in stars)
                    _spriteBatch.Draw(stars.Texture, stars.Bounds, Color.White);

                _spriteBatch.Draw(logoTexture, new Rectangle(115, 0, 800, 500), Color.White);
                _spriteBatch.Draw(continueTexture, new Rectangle(115, 300, 750, 450), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
