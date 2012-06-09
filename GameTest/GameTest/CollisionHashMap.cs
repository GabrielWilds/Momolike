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
        private const int BUCKETS_PER_ROW = 3;
        private const int BUCKETS_PER_COLUMN = 3;
        private static readonly Bucket[,] BUCKETS = new Bucket[BUCKETS_PER_ROW, BUCKETS_PER_COLUMN];

        static CollisionHashMap()
        {
            int divisionSizeX = MomolikeGame.SCREEN_WIDTH / BUCKETS_PER_ROW;
            int divisionSizeY = MomolikeGame.SCREEN_HEIGHT / BUCKETS_PER_COLUMN;

            // Initialize partition coordinates
            for (int x = 0; x < BUCKETS_PER_ROW; x++)
                for (int y = 0; y < BUCKETS_PER_COLUMN; y++)
                {
                    BUCKETS[x, y] = new Bucket();
                    BUCKETS[x, y].BucketArea = new Rectangle(x * divisionSizeX, y * divisionSizeY, divisionSizeX, divisionSizeY);
                }

        }

        public static void Initialize()
        {
            // do absolutely nothing - calling this function forces the static constructor to fire
        }

        private static void FillBuckets(IEnumerable<ObjectBase> objs)
        {
            foreach (Bucket bucket in BUCKETS)
                bucket.Clear();

            foreach (ObjectBase obj in objs)
                foreach (Bucket b in BUCKETS)
                    if (b.BucketArea.Intersects(obj.CollisionRectangle))
                        b.Add(obj);
        }

        public static void CheckCollisions(IEnumerable<ObjectBase> objs)
        {
            FillBuckets(objs);
            foreach (Bucket bucket in BUCKETS)
                bucket.CheckCollisions();
        }

        private class Bucket
        {
            private List<ObjectBase> moving = new List<ObjectBase>(8);
            private List<ObjectBase> still = new List<ObjectBase>(16);

            public Rectangle BucketArea;

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
                Rectangle a = objectA.CollisionRectangle;
                Rectangle b = objectB.CollisionRectangle;

                int collisionWidth = 0;
                int collisionHeight = 0;
                int collisionX = 0;
                int collisionY = 0;

                if (a.Left < b.Left)
                {
                    collisionWidth = GetCollisionWidth(a, b);
                    collisionX = b.Left;
                }
                else
                {
                    collisionWidth = GetCollisionWidth(b, a);
                    collisionX = a.Left;
                }

                if (a.Top < b.Top)
                {
                    collisionHeight = GetCollisionHeight(a, b);
                    collisionY = b.Top;
                }
                else
                {
                    collisionHeight = GetCollisionHeight(b, a);
                    collisionY = a.Top;
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

                if (bottomTrim > 0)
                    value += bottomTrim;

                return value;
            }

            private int GetCollisionWidth(Rectangle left, Rectangle right)
            {
                int value = left.Width + (left.Left - right.Left);
                int rightTrim = left.Right - right.Right;

                if (rightTrim > 0)
                    value += rightTrim;

                return value;
            }
        }
    }

}
