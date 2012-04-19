using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.ObjectModel;
using MapGen;

namespace MapImageRender
{
    public static class ImageMaker
    {
        public static void GenerateImageMap(string fileName, MapGen.Map map, RenderArguments args)
        {
            int mapHeight = GetMapHeight(map, args);
            int mapWidth = GetMapWidth(map, args);

            Bitmap bitmap = new Bitmap(mapWidth, mapHeight);
            Graphics g = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Color.DimGray);
            pen.Width = args.RoomBorderHeight;
            SolidBrush background = new SolidBrush(args.BackgroundColor);
            //SolidBrush testMark = new SolidBrush(Color.Green);
            g.FillRectangle(background, 0, 0, mapWidth, mapHeight);
            //g.FillRectangle(testMark, 0, 0, 10, 20);
            SolidBrush inner = new SolidBrush(args.RoomColor);
            SolidBrush border = new SolidBrush(args.BorderColor);
            SolidBrush door = new SolidBrush(Color.RosyBrown);

            foreach (Room room in map.Rooms)
            {
                if (room != null)
                {
                    int outerX = (room.Location.X) * (args.RoomOuterWidth) + args.ImageBorderWidth;
                    int outerY = (room.Location.Y) * (args.RoomOuterHeight) + args.ImageBorderHeight;
                    int borderX = outerX + args.RoomMargin;
                    int borderY = outerY + args.RoomMargin;
                    int borderSizeX = args.RoomInnerWidth + (args.RoomBorderWidth * 2);
                    int borderSizeY = args.RoomInnerHeight + (args.RoomBorderHeight * 2);
                    int innerX = borderX + args.RoomBorderWidth;
                    int innerY = borderY + args.RoomBorderHeight;


                    g.FillRectangle(border, borderX, borderY, borderSizeX, borderSizeY);
                    g.FillRectangle(inner, innerX, innerY, args.RoomInnerWidth, args.RoomInnerHeight);

                    if (room.NorthExit != null)
                    {
                        int startX = borderX + args.RoomBorderWidth;
                        int startY = borderY;
                        g.FillRectangle(door, startX, startY, args.RoomInnerWidth, args.DoorDepth);
                    }
                    if (room.SouthExit != null)
                    {
                        int startX = innerX;
                        int startY = innerY + args.RoomInnerHeight;
                        g.FillRectangle(door, startX, startY, args.RoomInnerWidth, args.DoorDepth);
                    }
                    if (room.WestExit != null)
                    {
                        int startX = borderX;
                        int startY = innerY;
                        g.FillRectangle(door, startX, startY, args.DoorDepth, args.RoomInnerHeight);
                    }
                    if (room.EastExit != null)
                    {
                        int startX = borderX + borderSizeX - args.RoomBorderWidth;
                        int startY = borderY + args.RoomBorderHeight;
                        g.FillRectangle(door, startX, startY, args.DoorDepth, args.RoomInnerHeight);
                    }

                }
            }
            bitmap.Save(fileName, ImageFormat.Png);
        }

        public static int GetMapHeight(MapGen.Map map, RenderArguments args)
        {
            int roomCountY = map.Rooms.GetLength(1);
            return (args.ImageBorderHeight * 2) + (args.RoomOuterHeight * roomCountY);
        }

        public static int GetMapWidth(MapGen.Map map, RenderArguments args)
        {
            int roomCountX = map.Rooms.GetLength(0);
            return (args.ImageBorderHeight * 2) + (args.RoomOuterWidth * roomCountX);
        }
    }
}
