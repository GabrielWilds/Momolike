using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Input;

namespace GameTest
{
    public class Player
    {
        Texture2D _sprite;
        Vector2 _position;
        Vector2 _motion;
        float _accel = 0f;
        float _playerSpeed = 6f;
        Rectangle _screenBounds;

        public Player(Texture2D sprite, Rectangle screenBounds)
        {
            _sprite = sprite;
            _screenBounds = screenBounds;
            _position = new Vector2(screenBounds.X / 2, screenBounds.Y / 2);
        }

        public void Update()
        {
            _motion = Vector2.Zero;
            if (InputHandler.KeyDown(Keys.Left))
                _motion.X = -1;
            if (InputHandler.KeyDown(Keys.Right))
                _motion.X = 1;
            if (InputHandler.KeyDown(Keys.Down))
                _motion.Y = 1;
            if (InputHandler.KeyDown(Keys.Up))
                _motion.Y = -1;

            if (_motion != Vector2.Zero && _accel < 6)
            {
                _accel += 0.5f;
            }
            else
            {
                _accel = 0;
            }

            _motion.X *= _playerSpeed + _accel;
            _motion.Y *= _playerSpeed + _accel;
            _position += _motion;
            EnforceBounds();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, _position, Color.White);
        }

        public void EnforceBounds()
        {
            if (_position.X < 0)
                _position.X = 0;
            if (_position.X + _sprite.Width > _screenBounds.Width)
                _position.X = _screenBounds.Width - _sprite.Width;
            if (_position.Y < 0)
                _position.Y = 0;
            if (_position.Y + _sprite.Height > _screenBounds.Height)
                _position.Y = _screenBounds.Height - _sprite.Width;
        }
    }
}
