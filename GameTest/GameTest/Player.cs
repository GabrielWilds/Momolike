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

            float inputWeightX = 0;
            float inputWeightY = 0;

            if (InputHandler.KeyDown(Keys.A))
                inputWeightX = -1;
            else if (InputHandler.KeyDown(Keys.D))
                inputWeightX = 1;

            if (InputHandler.KeyDown(Keys.S))
                inputWeightY = 1;
            else if (InputHandler.KeyDown(Keys.W))
                inputWeightY = -1;


            float totalWeight = Math.Abs(inputWeightX) + Math.Abs(inputWeightY);
            if (totalWeight == 0)
                _acceleration = 0;
            else
            {
                _acceleration += 0.05f;

                if (_acceleration > _maxAcceleration)
                    _acceleration = _maxAcceleration;
            }

            double currentAcceleration = _baseSpeed + _acceleration;
            double horizontalSpeed = currentAcceleration * inputWeightX;
            double verticalSpeed = currentAcceleration * inputWeightY;
            double excessSpeed = Math.Abs(Math.Pow(horizontalSpeed, 2)) + Math.Abs(Math.Pow(verticalSpeed, 2)) - Math.Pow(_maxTotalSpeed, 2);

            if (excessSpeed > 0)
            {
                horizontalSpeed = Math.Sqrt(Math.Pow(horizontalSpeed, 2) - (Math.Abs(inputWeightX) / totalWeight) * excessSpeed);
                verticalSpeed = Math.Sqrt(Math.Pow(verticalSpeed, 2) - (Math.Abs(inputWeightY) / totalWeight) * excessSpeed);

                if (inputWeightX < 0)
                    horizontalSpeed *= -1;
                if (inputWeightY < 0)
                    verticalSpeed *= -1;
            }

            this.Motion = Vector2.Zero;
            this.Motion.X = (float)horizontalSpeed;
            this.Motion.Y = (float)verticalSpeed;
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
