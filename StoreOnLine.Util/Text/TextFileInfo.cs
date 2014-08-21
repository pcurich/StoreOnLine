using System;
using System.IO;

namespace StoreOnLine.Util.Text
{
    public class TextFileInfo
    {
        public FileInfo FileInf { get; set; }

        public TextFileInfo(String path)
        {
            FileInf = new FileInfo(path);
        }

        public FileStream Open(FileMode mode)
        {
            return FileInf.Open(mode);
        }

        public StreamWriter CreateText()
        {
            return FileInf.CreateText();
        }

        public bool Exists()
        {
            return FileInf.Exists;
        }

        public void Delete()
        {
            FileInf.Delete();
        }

        public void MoveTo(string destFileName)
        {
            FileInf.MoveTo(destFileName);
        }

        public FileInfo CopyTo(string destFileName)
        {
            return FileInf.CopyTo(destFileName);
        }


    }
}
