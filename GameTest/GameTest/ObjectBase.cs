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
        public Vector2 Position;
        public Vector2 Motion = Vector2.Zero;
        public Texture2D Sprite;

        protected ObjectBase()
        {

        }

        protected ObjectBase(Texture2D sprite)
        {
            this.Sprite = sprite;
        }

        protected ObjectBase(Texture2D sprite, Vector2 position)
            : this(sprite)
        {
            this.Position = position;
        }


        /// <summary>
        /// Updates this object's state.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Adds this object's data to the spite batch.
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Sprite, this.Position, Color.White);
        }

        protected void EnforceBounds()
        {
            if (this.Position.Y < 0)
                this.Position.Y = 0;
            else if (this.Position.Y + this.Sprite.Height > MomolikeGame.SCREEN_BOUNDS.Height)
                this.Position.Y = MomolikeGame.SCREEN_BOUNDS.Height - this.Sprite.Width;

            if (this.Position.X < 0)
                this.Position.X = 0;
            else if (this.Position.X + this.Sprite.Width > MomolikeGame.SCREEN_BOUNDS.Width)
                this.Position.X = MomolikeGame.SCREEN_BOUNDS.Width - this.Sprite.Width;
        }
    }
}
