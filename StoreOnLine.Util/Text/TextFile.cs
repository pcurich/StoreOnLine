using System.IO;

namespace StoreOnLine.Util.Text
{
    public class TextFile
    {
        public FileStream FileStream { get; set; }

        public FileStream Open(string path, FileMode mode)
        {
            return File.Open(path, mode);
        }

        public StreamWriter CreateText(string path)
        {
            return File.CreateText(path);
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }

        public void MoveTo(string sourceFileName, string destFileName)
        {
            File.Move(sourceFileName, destFileName);
        }

        public void CopyTo(string sourceFileName, string destFileName)
        {
            File.Copy(sourceFileName, destFileName);
        }
    }
}
