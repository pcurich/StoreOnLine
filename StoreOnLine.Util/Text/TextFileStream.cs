using System.IO;

namespace StoreOnLine.Util.Text
{
    public class TextFileStream
    {
        public void example()
        {
            var stream = new FileStream(@"C:\archivo.dat",
                                     FileMode.OpenOrCreate,
                                     FileAccess.ReadWrite,
                                     FileShare.None | FileShare.Delete);

            stream = File.Open(@"C:\archivo.dat", FileMode.Create);
            stream = File.OpenRead(@"C:\archivo.dat");
            stream = File.OpenWrite(@"C:\archivo.dat");

            int nextByte = stream.ReadByte();
            byte[] bytes = new byte[10];
            int bytesRead = stream.Read(bytes, 0, 10);
        }
    }
}
