using System;
using System.Xml.Linq;

namespace StoreOnLine.Util.Xml
{
    public class XmlFile
    {
        public static XDocument Load(String file)
        {
            return XDocument.Load(file);
        }
    }
}
