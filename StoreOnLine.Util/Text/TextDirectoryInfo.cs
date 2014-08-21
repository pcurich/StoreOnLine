using System;
using System.Collections.Generic;
using System.IO;

namespace StoreOnLine.Util.Text
{
    public class TextDirectoryInfo
    {
        private DirectoryInfo DirectoryInfo { get; set; }

        public TextDirectoryInfo(String path)
        {
            DirectoryInfo = new DirectoryInfo(path);
        }

        public void CreateDirectory(string path)
        {
            if (!DirectoryInfo.Exists)
            {
                DirectoryInfo.Create();
            }
        }

        public IEnumerable<DirectoryInfo> EnumerateDirectories(string searchPattern, SearchOption searchOption)
        {
            return DirectoryInfo.EnumerateDirectories(searchPattern, searchOption);
        }





    }
}
