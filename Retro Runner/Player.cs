using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Retro_Runner
{
    public class Player
    {
        private Rectangle _location;
        private Texture2D _texture;
        private Vector2 _speed;
        private GraphicsDeviceManager _graphics;

        public Player(Texture2D texture, GraphicsDeviceManager graphics, int x, int y)
        {
            _graphics = graphics;
            _location = new Rectangle(x, y, 30, 30);
            _texture = texture;
            _speed = new Vector2();

        }

        public float HSpeed
        {
            get { return _speed.X; }
            set { _speed.X = value; }
        }

        public float VSpeed
        {
            get { return _speed.Y; }
            set { _speed.Y = value; }
        }

        public void Update()
        {
            _location.X += (int)_speed.X;


            _location.Y += (int)_speed.Y;


            if (_location.Left < 0)
            {
                _location.X = 0;
            }
            if (_location.Top < 0)
            {
                _location.Y = 0;
            }

            if (_location.Bottom >= _graphics.PreferredBackBufferHeight)
            {
                _location.Y = _graphics.PreferredBackBufferHeight - 30;
            }

            if (_location.Right >= _graphics.PreferredBackBufferWidth)
            {
                _location.X = _graphics.PreferredBackBufferWidth - 30;
            }

        }

        public bool Intersects(Rectangle value)
        {
            return _location.Intersects(value);

        }

        public int X
        {
            get { return _location.X; }
            set { _location.X = value; }
        }

        public int Y
        {
            get { return _location.Y; }
            set { _location.Y = value; }
        }


        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_texture, _location, Color.DeepSkyBlue);
        }


    }

    
}

