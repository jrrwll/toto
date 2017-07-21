using System;
using System.IO;
using System.Net;
using System.Text;

namespace Toto.Web.Spider.SPH
{
    public class SpiderHelper
    {
        public int buffer_size;
        public long min_size;
        public int timeout_span;

        public SpiderHelper(): this(1024, 102400L, 15000)
        {
        }
        public SpiderHelper(int buffer_size, long min_size, int timeout_span)
        {
            this.buffer_size = buffer_size;
            this.min_size = min_size;
            this.timeout_span = timeout_span;
        }
        public SpiderHelper(long min_size, int timeout_span): this(1024, min_size, timeout_span)
        {;
        }
        public SpiderHelper(long min_size): this(1024, min_size, 15000)
        {
        }


        public String GetSourceCode(String uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Timeout = timeout_span;
            string encoding = request.TransferEncoding;
            if (encoding == null)
                encoding = Encoding.Default.BodyName;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using ( StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding) ) )
            {
                string buffer = "";
                string line = "";
                line = reader.ReadLine();
                while (line != null)
                {
                    buffer = buffer + line;
                    line = reader.ReadLine();
                }

                if (buffer.Contains("\r")) buffer = buffer.Replace("\r", "");
                if (buffer.Contains("\n")) buffer = buffer.Replace("\n", "");
                return buffer;
            }
            
               

        }

        public bool DownloadLink(String uri, String filename)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Timeout = timeout_span;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if ( SelectResource(response) )
            {
                if( ! FilterFile( filename ) )
                    HandleBinary(response, filename);
                return true;
            } else
            {
                return false;
            }
        }

        public void HandleBinary(HttpWebResponse response, string filename)
        {
            using (BinaryReader reader = new BinaryReader(
                response.GetResponseStream() ) )
            {
                using (FileStream stream = new FileStream(filename, FileMode.Create) )
                {
                    byte[] buffer = new byte[buffer_size];
                    int size = reader.Read(buffer, 0, buffer_size);
                    while ( size != -1)
                    {
                        stream.Write(buffer, 0, size);
                        size = reader.Read(buffer, 0, buffer_size);
                    }
                    stream.Flush();
                }
            }
        }

        public virtual bool SelectResource(HttpWebResponse response)
        {
            if ( response.ContentType.ToLower().StartsWith("text/") )
            {
                return false;
            }
            if (response.ContentLength < min_size)
            {
                return false;
            }
            return true;
        }

        public virtual bool FilterFile(String filename)
        {
            FileInfo file = new FileInfo(filename);
            if (file.Exists && file.Length >= min_size)
            {
                Console.WriteLine( "文件已存在 " + file.FullName );
                return true;
            } else
            {
                return false;
            }
        }
    }

}
