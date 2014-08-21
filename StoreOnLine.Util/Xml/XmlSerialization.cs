using System;
using System.IO;
using System.Xml.Serialization;

namespace StoreOnLine.Util.Xml
{
    public static class XmlSerialization<T> where T : class 
    {
        public static void Serialize(T o, string file)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            Stream stream = new FileStream(file, FileMode.Create);
            xmlSerializer.Serialize(stream, o);
            stream.Close();
            stream.Dispose();
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

        public static  void Serialize(Object sss)
        {
            // Instanciación e inicialización del objeto  
            var o = new ReplacedField();  
            o.Field = "myField";  
            o.Pattern = "myPattern";  
            o.HasChanged = true;  

            // Creación del serializador XML  
            var serializer = new XmlSerializer(typeof(ReplacedField));

            // Creación de la secuencia 
            Stream stream = new FileStream(@"C:\ReplacedField.xml",FileMode.Create);

            // Serialización del objeto en la secuencia 
            serializer.Serialize(stream, o);
            stream.Close();
        }

       
    }
}
