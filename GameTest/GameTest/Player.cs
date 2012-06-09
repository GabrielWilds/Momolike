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
        private Animation _testAnimation;

        private float _acceleration = 0f;
        private float _maxAcceleration = 3.0f;
        private float _baseSpeed = 3.0f;
        private float _currentSpeed = 0.0f;
        private float _maxTotalSpeed = 6.0f;
        private Color color = Color.White;

        public Player()
            : base(Program.Game.LoadSprite("boxsprite"), new Vector2(), 32, 32)
        {
            _testAnimation = new Animation();
            _testAnimation.Frames = new Animation.AnimationFrame[4];
            _testAnimation.LoopStartFrame = 1;

            for (int i = 0; i < 4; i++)
                _testAnimation.Frames[i] = new Animation.AnimationFrame(i * 32, i * 32, 32, 32, 333);

            SetCurrentAnimation(_testAnimation, 0);
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
            base.Draw(spriteBatch);
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
