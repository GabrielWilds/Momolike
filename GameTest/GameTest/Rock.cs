using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameTest
{
    public class Rock : ObjectBase
    {
        public Rock() : base(Program.Game.LoadSprite("rock"))
        {
        }

        public Rock(Vector2 position) : base(Program.Game.LoadSprite("rock"), position)
        {
        }

        public override void Update()
        {
            // Do what rocks do - absolutely nothing.
        }
    }
}
