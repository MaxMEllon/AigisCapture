using AigisCapture.Model;
using System.IO;
using System.Xml.Serialization;

namespace AigisCapture.Common
{
    public class XMLFileManager
    {
        private static readonly string fileName;

        static XMLFileManager()
        {
            fileName = "AigisCapture.xml";
        }

        public static T ReadXml<T>() where T : new()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StreamReader stream;
            T obj;
            try
            {
                stream = new StreamReader(Env.APP_ROOT + fileName);
                obj = (T)serializer.Deserialize(stream);
                stream.Close();
            }
            catch (System.Exception)
            {
                obj = new T();
            }
            return obj;
        }

        public static void WriteXml<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StreamWriter stream = new StreamWriter(Env.APP_ROOT + fileName);
            System.Console.WriteLine(Env.APP_ROOT + fileName);
            serializer.Serialize(stream, obj);
            stream.Close();
        }
    }
}
