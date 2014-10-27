using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace StoreOnLine.Util.Xml
{
    public static class XmlSerialization<T> where T : class
    {
        public static void Serialize(T o, string file)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            Stream stream = new FileStream(file, FileMode.Append);
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            //xmlSerializer.Serialize(stream, o, ns);
            xmlSerializer.Serialize(stream, o);
            stream.Close();
            stream.Dispose();
        }

        public static Stream SerializeWeb(T o, string file)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            MemoryStream stream = new MemoryStream();
            StreamWriter w = new StreamWriter(stream, Encoding.UTF8);

           // Stream stream = new MemoryStream();
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            //xmlSerializer.Serialize(stream, o, ns);
            xmlSerializer.Serialize(stream, o);
            //stream.Close();
            return stream;
        }

        public static T Deserialize(string file)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            Stream stream = new FileStream(file, FileMode.Open);
            T result = xmlSerializer.Deserialize(stream) as T;
            stream.Close();
            stream.Dispose();
            return result;
        }
    }
}
