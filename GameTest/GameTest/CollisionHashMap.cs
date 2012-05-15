using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;

namespace GameTest
{
    public static class CollisionHashMap
    {
        private static Bucket[,] buckets = new Bucket[3, 3];
        private static int firstX;
        private static int secondX;
        private static int firstY;
        private static int secondY;

        public static void Initialize()
        {
            for (int x = 0; x < buckets.GetLength(0); x++)
                for (int y = 0; y < buckets.GetLength(1); y++)
                    buckets[x, y] = new Bucket();
        }

        private static void FillBuckets(IEnumerable<ObjectBase> objs)
        {
            foreach (Bucket bucket in buckets)
            {
                bucket.Clear();
            }

            firstX = MomolikeGame.SCREEN_WIDTH / 3;
            secondX = firstX * 2;
            firstY = MomolikeGame.SCREEN_HEIGHT / 3;
            secondY = firstY * 2;

            foreach (ObjectBase obj in objs)
            {
                float objX = obj.Position.X;
                float objY = obj.Position.Y;
                int hashX = 0;
                int hashY = 0;

                if (objX < secondX && objX > firstX)
                    hashX = 1;
                else if (objX > secondX)
                    hashX = 2;

                if (objY < secondY && objY > firstY)
                    hashX = 1;
                else if (objY > secondY)
                    hashX = 2;

                buckets[hashX, hashY].Add(obj);
            }
        }

        public static void CheckCollisions(IEnumerable<ObjectBase> objs)
        {
            FillBuckets(objs);
            foreach (Bucket bucket in buckets)
            {
                bucket.CheckCollisions();
            }
        }

        private class Bucket
        {
            List<ObjectBase> moving = new List<ObjectBase>(8);
            List<ObjectBase> still = new List<ObjectBase>(16);

            public void Add(ObjectBase obj)
            {
                if (obj.Motion == Vector2.Zero)
                    still.Add(obj);
                else
                    moving.Add(obj);
            }

            public void SortObjects(IEnumerable<ObjectBase> objs)
            {
                foreach (var obj in objs)
                {
                    Add(obj);
                }
            }

            public void Clear()
            {
                moving.Clear();
                still.Clear();
            }

            public void CheckCollisions()
            {
                for (int a = 0; a < moving.Count; a++)
                {
                    ObjectBase objectA = moving[a];

                    for (int b = a + 1; b < moving.Count; b++)
                    {
                        CheckForCollision(objectA, moving[b]);
                    }
                    for (int b = 0; b < still.Count; b++)
                    {
                        CheckForCollision(objectA, still[b]);
                    }
                }
            }

            public void CheckForCollision(ObjectBase objectA, ObjectBase objectB)
            {
                Rectangle a = new Rectangle((int)objectA.Position.X, (int)objectA.Position.Y, objectA.Sprite.Width, objectA.Sprite.Height);
                Rectangle b = new Rectangle((int)objectB.Position.X, (int)objectB.Position.Y, objectB.Sprite.Width, objectB.Sprite.Height);

                int collisionWidth = 0;
                int collisionHeight = 0;
                int collisionX = 0;
                int collisionY = 0;

                if (a.Left < b.Left)
                {
                    collisionWidth = GetCollisionWidth(a, b);
                    collisionX = a.Left;
                }
                else
                {
                    collisionWidth = GetCollisionWidth(b, a);
                    collisionX = b.Left;
                }

                if (a.Top < b.Top)
                {
                    collisionHeight = GetCollisionHeight(a, b);
                    collisionY = a.Top;
                }
                else
                {
                    collisionHeight = GetCollisionHeight(b, a);
                    collisionY = b.Top;
                }

                if (collisionWidth < 0 || collisionHeight < 0)
                    return;

                Rectangle collision = new Rectangle(collisionX, collisionY, collisionWidth, collisionHeight);
                objectA.Collide(objectB, collision);
            }

            private int GetCollisionHeight(Rectangle top, Rectangle bottom)
            {
                int value = top.Width + (top.Top - bottom.Top);
                int bottomTrim = top.Bottom - bottom.Bottom;

                if (bottomTrim < 0)
                    value += bottomTrim;

                return value;
            }

            private int GetCollisionWidth(Rectangle left, Rectangle right)
            {
                int value = left.Width + (left.Left - right.Left);
                int rightTrim = left.Right - right.Right;

                if (rightTrim < 0)
                    value += rightTrim;

                return value;
            }
        }
    }

}
