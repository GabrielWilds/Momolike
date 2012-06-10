using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameTest
{
    public abstract class ObjectBase
    {
        // Private Fields
        private Rectangle _rectangle;
        

        // Public Fields & Properties
        public Vector2 Position;

        public Vector2 Motion = Vector2.Zero;

        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            }
        }

        public Vector2 Center
        {
            get { return new Vector2(Position.X + (Width / 2), Position.Y + (Height / 2)); }
        }

        public int Height { get; set; }

        public int Width { get; set; }

        protected AnimationManager AnimationManager { get; private set; }


        // Constructors
        protected ObjectBase()
        {
            this.AnimationManager = new AnimationManager();
        }

        protected ObjectBase(Texture2D spriteSheet) : this()
        {
            AnimationManager.SpriteSheet = spriteSheet;
        }

        protected ObjectBase(Texture2D sprite, Vector2 position, int height, int width)
            : this(sprite)
        {
            this.Position = position;
            this.Height = height;
            this.Width = width;
        }



        // Methods
        /// <summary>
        /// Updates this object's state.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Adds this object's data to the spite batch.
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            AnimationManager.BlitCurrentAnimationFromSheet(spriteBatch, Position);
            //spriteBatch.Draw(MomolikeGame.RED, this.CollisionRectangle, Color.White);
        }

        public void EnforceBounds()
        {
            if (this.Position.Y < 0)
                this.Position.Y = 0;
            else if (this.Position.Y + this.Height > MomolikeGame.SCREEN_BOUNDS.Height)
                this.Position.Y = MomolikeGame.SCREEN_BOUNDS.Height - this.Width;

            if (this.Position.X < 0)
                this.Position.X = 0;
            else if (this.Position.X + this.Width > MomolikeGame.SCREEN_BOUNDS.Width)
                this.Position.X = MomolikeGame.SCREEN_BOUNDS.Width - this.Width;
        }

        public virtual void Collide(ObjectBase obj, Rectangle collision)
        {

        }



        
    }
}
