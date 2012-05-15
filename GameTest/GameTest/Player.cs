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
    public class Player : ObjectBase
    {
        private float _acceleration = 0f;
        private float _maxAcceleration = 3.0f;
        private float _baseSpeed = 3.0f;
        private float _currentSpeed = 0.0f;
        private float _maxTotalSpeed = 6.0f;
        private Color color = Color.White;

        public Player() : base(Program.Game.LoadSprite("bunnySprite"))
        {

        }

        public override void Update()
        {
            color = Color.White;

            this.Motion = Vector2.Zero;
            if (InputHandler.KeyDown(Keys.A))
                this.Motion.X = -1;
            else if (InputHandler.KeyDown(Keys.D))
                this.Motion.X = 1;

            if (InputHandler.KeyDown(Keys.S))
                this.Motion.Y = 1;
            else if (InputHandler.KeyDown(Keys.W))
                this.Motion.Y = -1;



            if (this.Motion != Vector2.Zero)
            {
                _acceleration += 0.05f;

                if (_acceleration > _maxAcceleration)
                    _acceleration = _maxAcceleration;
            }



            this.Motion.X *= _baseSpeed + _acceleration;
            this.Motion.Y *= _baseSpeed + _acceleration;
            this.Position += this.Motion;

            EnforceBounds();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Sprite, this.Position, color);
        }

        public override void Collide(ObjectBase obj, Rectangle collision)
        {
            color = Color.Red;

            bool ejectHorizontal = collision.Width < collision.Height;

            if (ejectHorizontal)
            {
                if (this.Position.X <= collision.Left)
                    this.Position.X -= collision.Width - 1;
                else
                    this.Position.X += collision.Width + 1;
            }
            else
            {
                if (this.Position.Y <= collision.Top)
                    this.Position.Y -= collision.Height - 1;
                else
                    this.Position.Y += collision.Height + 1;
            }
        }
    }
}
