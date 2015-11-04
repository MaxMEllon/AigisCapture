using AigisCapture.Model;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AigisCapture.Model
{

    public class ImageSupporter
    {
        private static readonly Bitmap template;

        static ImageSupporter()
        {
            template = Properties.Resources.Home;
        }

        public string ScreanShot(Point pos, bool noNameFlag)
        {
            Size size = new Size(Env.AIGIS_WINDOW_SIZE.Width, Env.AIGIS_WINDOW_SIZE.Height);
            string timeStanp = DateTime.Now.ToString("yyyy_MMdd_HHmmss");
            string fileNeme = "Aigis_" + timeStanp + ".png";
            string filePath = Env.SETTINGS.SaveDirectory + "\\" + fileNeme;
            Bitmap bmp = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(new Point(pos.X, pos.Y), new Point(0, 0), bmp.Size);
                Rectangle rectangle = new Rectangle(Env.NAME_AREA_LOCATION, Env.NAME_AREA_SIZE);
                if (noNameFlag) { g.FillRectangle(Brushes.White, rectangle); }
            }
            bmp.Save(filePath, ImageFormat.Png);

            return fileNeme;
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
