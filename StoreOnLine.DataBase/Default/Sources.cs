using System;
using System.Collections.Generic;
using System.Xml;

namespace StoreOnLine.DataBase.Default
{
    public class Sources
    {
        private readonly IDictionary<String, Byte[]> _imagenes = new Dictionary<String, Byte[]>();
        private IDictionary<String, XmlDocument> _xmlConfig = new Dictionary<String, XmlDocument>();

        private static Sources _instance = null;

        private Sources()
        {
            var xml = Util.Xml.XmlFile.Load("Config.xml");
            if (xml.Root != null)
            {
                var xElements = xml.Root.Elements("img");
                foreach (var element in xElements)
                {
                    var keyAttribute = element.Attribute("key");
                    var valueAttribute = element.Attribute("value");
                    _imagenes.Add(keyAttribute.Value, Util.Img.ImgTransform.Image2Bytes(valueAttribute.Value));
                }
            }
        }

        public static Sources Instance()
        {
            return _instance ?? (_instance = new Sources());
        }

        public static Byte[] GetImgen(String key)
        {
            return Instance()._imagenes[key];
        }
    }
}
