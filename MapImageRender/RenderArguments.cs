﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Drawing;
using System.Drawing.Imaging;

namespace MapImageRender
{
    /// <summary>
    /// Arguments for the ImageMaker class function to convert a map into a .png output. 
    /// All numerical values are in pixels unless otherwise specified.
    /// </summary>
    public class RenderArguments
    {
        private int _roomInnerX = 12;
        private int _roomInnerY = 12;
        private int _roomBorderX = 6;
        private int _roomBorderY = 6;
        private int _doorWidth = 4;
        private int _imageBorderX = 20;
        private int _imageBorderY = 20;
        private int _imageBorderMargin = 20;
        private int _roomMargin = -4;
        private Color _imageBackgroundColor = Color.DarkSlateBlue;
        private Color _imageBorderColor = Color.CornflowerBlue;
        private Color _borderColor = Color.Black;
        private Color _normalRoomColor = Color.White;
        private Color _normalDoorColor = Color.White;
        private Color _treasureRoomColor = Color.Gold;
        private Color _bossRoomColor = Color.DarkRed;
        private Color _shopRoomColor = Color.ForestGreen;
        private Color _miniBossRoomColor = Color.PaleVioletRed;
        private Color _secretRoomColor = Color.DarkSlateGray;

        public int RoomInnerWidth
        {
            get { return _roomInnerX; }
            set { _roomInnerX = value; }
        }

        public int RoomInnerHeight
        {
            get { return _roomInnerY; }
            set { _roomInnerY = value; }
        }

        public int RoomBorderWidth
        {
            get { return _roomBorderX; }
            set { _roomBorderX = value; }
        }

        public int RoomBorderHeight
        {
            get { return _roomBorderY; }
            set { _roomBorderY = value; }
        }

        public int RoomOuterHeight
        {
            get { return RoomInnerHeight + (RoomBorderHeight * 2) + (RoomMargin * 2); }
        }

        public int RoomOuterWidth
        {
            get { return RoomInnerWidth + (RoomBorderWidth * 2) + (RoomMargin * 2); }
        }

        public int DoorWidth
        {
            get { return _doorWidth; }
            set { _doorWidth = value; }
        }

        public int RoomMargin
        {
            get { return _roomMargin; }
            set { _roomMargin = value; }
        }

        public int ImageBorderHeight
        {
            get { return _imageBorderY; }
            set { _imageBorderY = value; }
        }

        public int ImageBorderWidth
        {
            get { return _imageBorderX; }
            set { _imageBorderX = value; }
        }

        public int ImageBorderMargin
        {
            get { return _imageBorderMargin; }
            set { _imageBorderMargin = value; }
        }

        public Color ImageBackgroundColor
        {
            get { return _imageBackgroundColor; }
            set { _imageBackgroundColor = value; }
        }

        public Color ImageBorderColor
        {
            get { return _imageBorderColor; }
            set { _imageBorderColor = value; }
        }

        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; }
        }

        public Color RoomColor
        {
            get { return _normalRoomColor; }
            set { _normalRoomColor = value; }
        }

        public Color DoorColor
        {
            get { return _normalDoorColor; }
            set { _normalDoorColor = value; }
        }

        public Color TreasureRoomColor
        {
            get { return _treasureRoomColor; }
            set { _treasureRoomColor = value; }
        }

        public Color BossRoomColor
        {
            get { return _bossRoomColor; }
            set { _bossRoomColor = value; }
        }

        public Color MiniBossRoomColor
        {
            get { return _miniBossRoomColor; }
            set { _bossRoomColor = value; }
        }

        public Color ShopRoomColor
        {
            get { return _shopRoomColor; }
            set { _shopRoomColor = value; }
        }

        public Color SecretRoomColor
        {
            get { return _secretRoomColor; }
            set { _secretRoomColor = value; }
        }
    }
}
