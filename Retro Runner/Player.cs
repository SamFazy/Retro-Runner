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

        public Player(Texture2D texture, int x, int y)
        {
            _location = new Rectangle(x, y, 30, 30);
            _texture = texture;
            _speed = new Vector2();

        }

        public void Update()
        {
            _location.X += (int)_speed.X;
            _location.Y += (int)_speed.Y;

        }


        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_texture, _location, Color.DeepSkyBlue);
        }


    }

    
}

