using AigisCapture.Common;
using System;
using System.Drawing;

namespace AigisCapture.Model
{
    public struct Env
    {
        public static readonly string APP_ROOT = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly Size AIGIS_WINDOW_SIZE = new Size(960, 640);
        public static readonly Size NAME_AREA_SIZE = new Size(50, 220);
        public static readonly Point NAME_AREA_LOCATION = new Point(60, 0);
        public static Settings SETTINGS { get; set; }
    }
}
