using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameTest
{
    public class Rock : ObjectBase
    {
        public Rock() : base(Program.Game.LoadSprite("rock"))
        {

        }

        public override void Update()
        {
            // Do what rocks do - absolutely nothing.
        }
    }
}
