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
    class ImageMaker
    {
        public void GenerateImageMap(string fileName, MapGen.Map map, RenderArguments args)
        {
            int mapHeight = GetMapHeight(map, args);
            int mapWidth = GetMapWidth(map, args);

            Bitmap bitmap = new Bitmap(mapWidth, mapHeight);
            Graphics g = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Color.DimGray);
            pen.Width = args.RoomBorderHeight;
            SolidBrush background = new SolidBrush(args.BackgroundColor);
            g.FillRectangle(background, 0, 0, mapWidth, mapHeight);
            foreach (Room room in map.Rooms)
            {

            }
            
        }

        public int GetMapHeight(MapGen.Map map, RenderArguments args)
        {
            int roomCountY = map.Rooms.GetLength(1);
            return (args.ImageBorderHeight * 2) + (args.RoomOuterHeight * roomCountY) + (args.RoomMargin * (roomCountY - 1));
        }

        public int GetMapWidth(MapGen.Map map, RenderArguments args)
        {
            int roomCountX = map.Rooms.GetLength(0);
            return (args.ImageBorderHeight * 2) + (args.RoomOuterWidth * roomCountX) + (args.RoomMargin * (roomCountX - 1));
        }
    }
}
