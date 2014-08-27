using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace StoreOnLine.Util.Xml
{
    public class XmlFile
    {
        public static XDocument Load(String file)
        {
            return XDocument.Load(file);
        }

        public static IEnumerable<XElement> Elements(XDocument root, String rootString)
        {
            return root.Elements(rootString);
        }

    }
}
