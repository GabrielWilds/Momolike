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
        // Animation Key Constants
        private const string ANIMATION_KEY_MOVE_UP = "moveUp";
        private const string ANIMATION_KEY_MOVE_DOWN = "moveDown";
        private const string ANIMATION_KEY_MOVE_RIGHT = "moveRight";
        private const string ANIMATION_KEY_MOVE_LEFT = "moveLeft";

        private const string ANIMATION_KEY_IDLE_UP = "idleUp";
        private const string ANIMATION_KEY_IDLE_DOWN = "idleDown";
        private const string ANIMATION_KEY_IDLE_RIGHT = "idleRight";
        private const string ANIMATION_KEY_IDLE_LEFT = "idleLeft";


        // Private Fields
        private float _acceleration = 0f;
        private float _maxAcceleration = 3.0f;
        private float _baseSpeed = 3.0f;
        private float _currentSpeed = 0.0f;
        private float _maxTotalSpeed = 6.0f;
        private Color _tint = Color.White; // White is "none" (XNA spec)


        // Public Properties & Fields
        public Direction Facing
        {
            get;
            set;
        }


        // Constructors
        public Player()
            : base(Program.Game.LoadSprite("boxsprite"), new Vector2(), 32, 32)
        {
            CreateTestAnimations();
            AnimationManager.SetCurrentAnimation(ANIMATION_KEY_IDLE_DOWN);
        }


        // Methods
        public override void Update()
        {
            MovePlayer();
            EnforceBounds();
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            Direction currentDirection = Direction.None;
            string animationKey = null;

            if (Motion.X < 0)
                currentDirection = Direction.Left;
            else if (Motion.X > 0)
                currentDirection = Direction.Right;
            else if (Motion.Y < 0)
                currentDirection = Direction.Up;
            else if (Motion.Y > 0)
                currentDirection = Direction.Down;

            if (currentDirection == Direction.None)
                switch (Facing)
                {
                    case (Direction.Left):
                        animationKey = ANIMATION_KEY_IDLE_LEFT;
                        break;

                    case (Direction.Right):
                        animationKey = ANIMATION_KEY_IDLE_RIGHT;
                        break;

                    case (Direction.Up):
                        animationKey = ANIMATION_KEY_IDLE_UP;
                        break;

                    case (Direction.Down):
                        animationKey = ANIMATION_KEY_IDLE_DOWN;
                        break;
                }
            else
                switch (currentDirection)
                {
                    case (Direction.Left):
                        animationKey = ANIMATION_KEY_MOVE_LEFT;
                        break;

                    case (Direction.Right):
                        animationKey = ANIMATION_KEY_MOVE_RIGHT;
                        break;

                    case (Direction.Up):
                        animationKey = ANIMATION_KEY_MOVE_UP;
                        break;

                    case (Direction.Down):
                        animationKey = ANIMATION_KEY_MOVE_DOWN;
                        break;
                }

            if (currentDirection != Facing)
                AnimationManager.SetCurrentAnimation(animationKey);

            if(currentDirection != Direction.None)
                Facing = currentDirection;

            AnimationManager.AnimationTimeScale = 1 + (2 * (_acceleration / _maxAcceleration));
            AnimationManager.AdvanceCurrentAnimation();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Collide(ObjectBase obj, Rectangle collision)
        {
            _tint = Color.Red;

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

        private void MovePlayer()
        {
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

            double currentSpeed = _baseSpeed + _acceleration;

            if (currentSpeed > _maxTotalSpeed)
                currentSpeed = _maxTotalSpeed;

            double horizontalSpeed = currentSpeed * inputWeightX;
            double verticalSpeed = currentSpeed * inputWeightY;
            double excessSpeed = Math.Abs(Math.Pow(horizontalSpeed, 2)) + Math.Abs(Math.Pow(verticalSpeed, 2)) - Math.Pow(currentSpeed, 2);

            if (excessSpeed > 0)
            {
                horizontalSpeed = Math.Sqrt(Math.Pow(horizontalSpeed, 2) - (Math.Abs(inputWeightX) / totalWeight) * excessSpeed);
                verticalSpeed = Math.Sqrt(Math.Pow(verticalSpeed, 2) - (Math.Abs(inputWeightY) / totalWeight) * excessSpeed);

                if (inputWeightX < 0)
                    horizontalSpeed *= -1;
                if (inputWeightY < 0)
                    verticalSpeed *= -1;
            }

            this.Motion.X = (float)horizontalSpeed;
            this.Motion.Y = (float)verticalSpeed;
            this.Position += this.Motion;
        }

        private void CreateTestAnimations()
        {
            const int frameHeight = 32;
            const int frameWidth = 32;
            const int frameDuration = 200;

            var walkDown = new Animation();
            walkDown.Frames = new Animation.AnimationFrame[] 
                              {
                                  new Animation.AnimationFrame(0,0,frameHeight,frameWidth,frameDuration),
                                  new Animation.AnimationFrame(32,0,frameHeight,frameWidth,frameDuration),
                                  new Animation.AnimationFrame(64,0,frameHeight,frameWidth,frameDuration),
                                  new Animation.AnimationFrame(96,0,frameHeight,frameWidth,frameDuration),
                              };

            var walkUp = new Animation();
            walkUp.Frames = new Animation.AnimationFrame[] 
                              {
                                  new Animation.AnimationFrame(0,32,frameHeight,frameWidth,frameDuration),
                                  new Animation.AnimationFrame(32,32,frameHeight,frameWidth,frameDuration),
                                  new Animation.AnimationFrame(64,32,frameHeight,frameWidth,frameDuration),
                                  new Animation.AnimationFrame(96,32,frameHeight,frameWidth,frameDuration),
                              };

            var walkRight = new Animation();
            walkRight.Frames = new Animation.AnimationFrame[] 
                              {
                                  new Animation.AnimationFrame(0,64,frameHeight,frameWidth,frameDuration),
                                  new Animation.AnimationFrame(32,64,frameHeight,frameWidth,frameDuration),
                                  new Animation.AnimationFrame(64,64,frameHeight,frameWidth,frameDuration),
                                  new Animation.AnimationFrame(96,64,frameHeight,frameWidth,frameDuration),
                              };

            var walkLeft = new Animation();
            walkLeft.Frames = new Animation.AnimationFrame[] 
                              {
                                  new Animation.AnimationFrame(0,96,frameHeight,frameWidth,frameDuration),
                                  new Animation.AnimationFrame(32,96,frameHeight,frameWidth,frameDuration),
                                  new Animation.AnimationFrame(64,96,frameHeight,frameWidth,frameDuration),
                                  new Animation.AnimationFrame(96,96,frameHeight,frameWidth,frameDuration),
                              };

            var idleDown = new Animation();
            idleDown.Frames = new Animation.AnimationFrame[] 
                              {
                                  new Animation.AnimationFrame(0,0,frameHeight,frameWidth,0),
                              };

            var idleUp = new Animation();
            idleUp.Frames = new Animation.AnimationFrame[] 
                              {
                                  new Animation.AnimationFrame(0,32,frameHeight,frameWidth,0),
                              };

            var idleRight = new Animation();
            idleRight.Frames = new Animation.AnimationFrame[] 
                              {
                                  new Animation.AnimationFrame(96,64,frameHeight,frameWidth,0),
                              };

            var idleLeft = new Animation();
            idleLeft.Frames = new Animation.AnimationFrame[] 
                              {
                                  new Animation.AnimationFrame(0,96,frameHeight,frameWidth,0),
                              };


            AnimationManager.AddAnimation(ANIMATION_KEY_MOVE_DOWN, walkDown);
            AnimationManager.AddAnimation(ANIMATION_KEY_MOVE_UP, walkUp);
            AnimationManager.AddAnimation(ANIMATION_KEY_MOVE_RIGHT, walkRight);
            AnimationManager.AddAnimation(ANIMATION_KEY_MOVE_LEFT, walkLeft);

            AnimationManager.AddAnimation(ANIMATION_KEY_IDLE_DOWN, idleDown);
            AnimationManager.AddAnimation(ANIMATION_KEY_IDLE_UP, idleUp);
            AnimationManager.AddAnimation(ANIMATION_KEY_IDLE_RIGHT, idleRight);
            AnimationManager.AddAnimation(ANIMATION_KEY_IDLE_LEFT, idleLeft);
        }
    }
}
