using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameTest
{
    public class Animation
    {
        public AnimationFrame[] Frames
        {
            get;
            set;
        }

        public int LoopStartFrame
        {
            get;
            set;
        }

        public Animation()
        {
            
        }

        public Animation Clone()
        {
            var clone = new Animation();
            
            clone.LoopStartFrame = this.LoopStartFrame;
            clone.Frames = new AnimationFrame[this.Frames.Length];
            this.Frames.CopyTo(clone.Frames, 0);
            
            return clone;
        }

        public struct AnimationFrame
        {
            public Rectangle ClippingRectangle;
            public int MillisecondsToDisplay;

            public AnimationFrame(int x, int y, int width, int height, int millisecondsToDisplay)
            {
                ClippingRectangle = new Rectangle(x, y, width, height);
                MillisecondsToDisplay = millisecondsToDisplay;
            }
        }
    }
}