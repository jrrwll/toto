using System;

namespace Toto.Text
{
    public class EasyFormat
    {
        // 字节
        public static string BytesConversion(long size)
        {
            long k = 1024L;
            long m = k * k;
            long g = m * k;
            long t = g * k;

            if (size / t >= 1L) return Math.Round(size / (float)t, 2) + "TB";
            if (size / g >= 1L) return Math.Round(size / (float)g, 2) + "GB";
            if (size / m >= 1L) return Math.Round(size / (float)m, 2) + "MB";
            if (size / k >= 1L) return Math.Round(size / (float)k, 2) + "KB";
            return size + "Bytes";

        }
    }
}
