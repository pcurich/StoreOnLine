using System;
using System.IO;

namespace StoreOnLine.Util.Text
{
    public class TextPath
    {
        public static String ChangeExtension(string path, string extension)
        {
            return Path.ChangeExtension(path, extension);
        }

        public static String Combine(string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }


    }
}
