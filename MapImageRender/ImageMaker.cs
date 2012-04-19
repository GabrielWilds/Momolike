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
            SolidBrush testMark = new SolidBrush(Color.Green);
            g.FillRectangle(background, 0, 0, mapWidth, mapHeight);
            g.FillRectangle(testMark, 0, 0, 10, 20);
            SolidBrush inner = new SolidBrush(args.RoomColor);
            SolidBrush border = new SolidBrush(args.BorderColor);

            foreach (Room room in map.Rooms)
            {
                if (room != null)
                {
                    int X = (room.Location.X + 1) * (args.RoomOuterWidth);
                    int Y = (room.Location.Y + 1) * (args.RoomOuterHeight);
                    g.FillRectangle(border, X + args.RoomMargin, Y + args.RoomMargin, (args.RoomOuterWidth - args.RoomMargin), (args.RoomOuterHeight - args.RoomMargin));
                    g.FillRectangle(inner, X + args.RoomMargin + args.RoomBorderWidth, Y + args.RoomMargin + args.RoomBorderHeight, args.RoomInnerWidth, args.RoomInnerHeight);
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
