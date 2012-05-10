using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;

namespace GameTest
{
    class CollisionHashMap
    {

        private class Bucket
        {
            List<ObjectBase> moving = new List<ObjectBase>(16);
            List<ObjectBase> still = new List<ObjectBase>(32);

            void SortObjects(List<ObjectBase> objs)
            {
                foreach (var obj in objs)
                {
                    if (obj.Motion == Vector2.Zero)
                        still.Add(obj);
                    else
                        moving.Add(obj);
                }
            }
        }
    }


}
