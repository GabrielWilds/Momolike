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

            SolidBrush imageBorder = new SolidBrush(Color.CornflowerBlue);
            SolidBrush background = new SolidBrush(args.BackgroundColor);
            g.FillRectangle(imageBorder, 0, 0, mapWidth, mapHeight);
            g.FillRectangle(background, args.ImageBorderWidth, args.ImageBorderHeight, mapWidth - args.ImageBorderWidth * 2, mapHeight - args.ImageBorderHeight * 2);

            SolidBrush inner = new SolidBrush(args.RoomColor);
            SolidBrush border = new SolidBrush(args.BorderColor);
            SolidBrush door = new SolidBrush(Color.RosyBrown);

            foreach (Room room in map.Rooms)
            {
                if (room == null)
                    continue;

                int margin = args.RoomMargin;
                int outerX = (room.Location.X * (args.RoomInnerWidth + (args.RoomBorderWidth * 2) + margin)) + args.ImageBorderWidth + args.ImageBorderMargin;
                int outerY = (room.Location.Y * (args.RoomInnerHeight + (args.RoomBorderHeight * 2) + margin)) + args.ImageBorderHeight + args.ImageBorderMargin;
                int borderX = outerX;
                int borderY = outerY;

                if (margin > 0)
                {
                    borderX += margin;
                    borderY += margin;
                }



                int borderSizeX = args.RoomInnerWidth + (args.RoomBorderWidth * 2);
                int borderSizeY = args.RoomInnerHeight + (args.RoomBorderHeight * 2);
                int innerX = borderX + args.RoomBorderWidth;
                int innerY = borderY + args.RoomBorderHeight;


                g.FillRectangle(border, borderX, borderY, borderSizeX, borderSizeY);
                g.FillRectangle(inner, innerX, innerY, args.RoomInnerWidth, args.RoomInnerHeight);

                if (room.NorthExit != null)
                {
                    int startX = innerX + ((args.RoomInnerWidth - args.DoorWidth) / 2);
                    int startY = borderY;
                    g.FillRectangle(door, startX, startY, args.DoorWidth, args.RoomBorderHeight);
                }
                if (room.SouthExit != null)
                {
                    int startX = innerX + ((args.RoomInnerWidth - args.DoorWidth) / 2);
                    int startY = innerY + args.RoomInnerHeight;
                    g.FillRectangle(door, startX, startY, args.DoorWidth, args.RoomBorderHeight);
                }
                if (room.WestExit != null)
                {
                    int startX = borderX;
                    int startY = innerY + ((args.RoomInnerHeight - args.DoorWidth) / 2);
                    g.FillRectangle(door, startX, startY, args.RoomBorderWidth, args.DoorWidth);
                }
                if (room.EastExit != null)
                {
                    int startX = borderX + borderSizeX - args.RoomBorderWidth;
                    int startY = borderY + args.RoomBorderHeight + ((args.RoomInnerHeight - args.DoorWidth) / 2);
                    g.FillRectangle(door, startX, startY, args.RoomBorderWidth, args.DoorWidth);
                }
            }
            bitmap.Save(fileName, ImageFormat.Png);
        }

        public static int GetMapHeight(MapGen.Map map, RenderArguments args)
        {
            int roomCountY = map.Rooms.GetLength(1);
            int outer = args.RoomOuterHeight;

            outer += -args.RoomMargin;

            return (args.ImageBorderHeight * 2) + (args.ImageBorderMargin * 2) + (outer * roomCountY) + Math.Abs(args.RoomMargin);
        }

        public static int GetMapWidth(MapGen.Map map, RenderArguments args)
        {
            int roomCountX = map.Rooms.GetLength(0);
            int outer = args.RoomOuterWidth;

            outer += -args.RoomMargin;

            return (args.ImageBorderHeight * 2) + (args.ImageBorderMargin * 2) + (outer * roomCountX) + Math.Abs(args.RoomMargin);
        }
    }
}
