using System.Collections.Generic;
using System.IO;

namespace StoreOnLine.Util.Text
{
    public class TextDirectory
    {
        public static DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }

        public static void Delete(string emptyPath)
        {
            Directory.Delete(emptyPath);
        }

        public static bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public static void Move(string sourceDirName, string destDirName)
        {
            Directory.Move(sourceDirName, destDirName);
        }

        public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateDirectories(path, searchPattern, searchOption);
        }

        public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateFiles(path, searchPattern, searchOption);
        }

    }
}
