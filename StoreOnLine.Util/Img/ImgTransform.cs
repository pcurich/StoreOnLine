using System;
using System.Drawing;
using System.IO;

namespace StoreOnLine.Util.Img
{
    public class ImgTransform
    {
        public static byte[] Image2Bytes(String uri)
        {
            var streams = new FileStream(uri, FileMode.Open, FileAccess.Read);
            var reader = new BinaryReader(streams);
            var imagens = reader.ReadBytes((int)streams.Length);
            reader.Close(); streams.Close();
            return imagens;
        }

        public static Image Bytes2Image(byte[] bytes)
        {
            if (bytes == null)
                return null;

            var ms = new MemoryStream(bytes);
            Bitmap bm = null;
            try
            {
                bm = new Bitmap(ms);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return bm;
        }
    }
}
