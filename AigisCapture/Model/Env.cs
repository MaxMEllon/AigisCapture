using AigisCapture.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AigisCapture.Model
{
    public struct Env
    {
        public static readonly string APP_ROOT = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly Size AIGIS_WINDOW_SIZE = new Size(960, 640);
        public static readonly int NAME_AREA_HEIGHT_OF_AIGIS_AREA = 50;

        public static Settings SETTINGS;

        static Env()
        {
            SETTINGS = XMLFileManager.ReadXml<Settings>();
        }
    }
}
