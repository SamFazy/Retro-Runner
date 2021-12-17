using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Retro_Runner
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        MouseState mouseState;
        Random generator = new Random();

        int randomYValue;
        int randomXValue;
        int randomSize;
        int randomSpeed;

        //Intro Screen
        Texture2D logoTexture;
        Texture2D continueTexture;

        Texture2D starTexture;
        Rectangle starRect;
        Vector2 starSpeed;


        SoundEffect musicTheme;
        SoundEffectInstance musicThemeInstance;

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
            randomXValue = generator.Next(0, _graphics.PreferredBackBufferWidth - 10);
            randomYValue = generator.Next(0, _graphics.PreferredBackBufferHeight - 10);
            randomSize = generator.Next(5, 40);
            randomSpeed = generator.Next(1, 8);
            starRect = new Rectangle(randomXValue, randomYValue, randomSize, randomSize);
            starSpeed = new Vector2(randomSpeed);

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
                if (starRect.Bottom > 900)
                {
                    randomSize = generator.Next(5, 40);
                    randomXValue = generator.Next(0, 1000 - randomSize - 1);
                    randomSpeed = generator.Next(1, 8);
                    starSpeed.Y = starSpeed.Y = randomSpeed;
                    starRect = new Rectangle(randomXValue, -10 - randomSize, randomSize, randomSize);
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
                _spriteBatch.Draw(starTexture, starRect, Color.White);
                _spriteBatch.Draw(logoTexture, new Rectangle(115, 0, 800, 500), Color.White);
                _spriteBatch.Draw(continueTexture, new Rectangle(115, 300, 750, 450), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
