using System;

namespace AigisCapture.Common
{
    public class Log
    {
        public static void Write(String message, Exception ex = null)
        {
            try
            {
                DateTime time = DateTime.Now;
                String logFolder = System.AppDomain.CurrentDomain.BaseDirectory + "Log";
                System.IO.Directory.CreateDirectory(logFolder);
                String logFile = logFolder + "\\" + time.ToString("yyyy_MM_dd") + ".log";
                String logString = time.ToString("[yyyy/MM/dd] HH:mm:ss") + "\t" + message;
                if (ex != null) { logString += ex.ToString(); }
                System.IO.StreamWriter sw = null;
                try
                {
                    sw = new System.IO.StreamWriter(logFile, true, System.Text.Encoding.GetEncoding("UTF-8"));
                    sw.WriteLine(logString);
                }
                catch { }
                finally
                {
                    if (sw != null) { sw.Close(); }
                }
            }
            catch { }
        }
    }
}
