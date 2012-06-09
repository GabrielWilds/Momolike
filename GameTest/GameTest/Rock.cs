using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameTest
{
    public class Rock : ObjectBase
    {
        public Rock(Vector2 position) : base(Program.Game.LoadSprite("rock"), position, 300, 300)
        {
            Animation a = new Animation();
            a.Frames = new Animation.AnimationFrame[1];
            a.Frames[0] = new Animation.AnimationFrame(0, 0, 40, 40, 0);
            a.LoopStartFrame = 0;

            SetCurrentAnimation(a, 0);
        }

        public override void Update()
        {
            // Do what rocks do - absolutely nothing.
        }
    }
}
