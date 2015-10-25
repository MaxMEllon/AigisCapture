using AigisCapture.Model;
using AigisCapture.ViewModel;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AigisCapture.Common
{

    public class ImageSupporter
    {
        private static readonly Bitmap template;

        static ImageSupporter()
        {
            template = Properties.Resources.Home;
        }

        public void ScreanShot(Point pos, bool noNameFlag)
        {
            Size size = new Size(Env.AIGIS_WINDOW_SIZE.Width, Env.AIGIS_WINDOW_SIZE.Height);
            if (noNameFlag)
            {
                pos.Y += Env.NAME_AREA_HEIGHT_OF_AIGIS_AREA;
                size.Height -= Env.NAME_AREA_HEIGHT_OF_AIGIS_AREA;
            }
            string timeStanp = DateTime.Now.ToString("yyyy_MM_dd_ss");
            string filePath = Env.SETTINGS.SaveDirectory + "\\Aigis_" + timeStanp + ".bmp"; 
            Bitmap bmp = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(new Point(pos.X, pos.Y), new Point(0, 0), bmp.Size);
            }
            bmp.Save(filePath, ImageFormat.Bmp);
        }

        public Point OverlapLocation
        {
            get
            {
                Bitmap screanBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                                 Screen.PrimaryScreen.Bounds.Height);
                using (Graphics g = Graphics.FromImage(screanBitmap))
                {
                    g.CopyFromScreen(0, 0, 0, 0, screanBitmap.Size);
                }
                return GetOverlapLocation(screanBitmap);
            }
        }

        private Point GetOverlapLocation(Bitmap screan)
        {
            IplImage ipltemplate = BitmapConverter.ToIplImage(template);
            IplImage iplScrean = BitmapConverter.ToIplImage(screan);

            CvSize resSize = new CvSize(iplScrean.Width - ipltemplate.Width + 1,
                                        iplScrean.Height - ipltemplate.Height + 1);
            IplImage resImg = Cv.CreateImage(resSize, BitDepth.F32, 1);

            Cv.MatchTemplate(iplScrean, ipltemplate, resImg, MatchTemplateMethod.CCorrNormed);

            double minVal;
            double maxVal;
            CvPoint minLoc;
            CvPoint maxLoc;
            Cv.MinMaxLoc(resImg, out minVal, out maxVal, out minLoc, out maxLoc);
            return maxVal >= 0.99 ? new Point(maxLoc.X, maxLoc.Y) : new Point(0, 0);
        }
    }
}
