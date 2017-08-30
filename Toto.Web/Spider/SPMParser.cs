using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Toto.Web.Spider
{
    public abstract class SPMParser<T, R>
    {
        public static int buffer_size = 1024;
        public static long min_size = 1024_00L;
        public static int timeout_span = 15_000;

        public int number;
        public String current_uri;
        public SPMStore<T, R> store;
        public Task task;
        public bool hungry;
        public bool ischecked;

        public SPMParser(SPMStore<T, R> store, int num, bool hungry)
        {
            this.store = store;
            this.number = num;
            this.hungry = hungry;
        }
        public SPMParser(SPMStore<T, R> store, int num): this(store, num, false){}


        public virtual void Start()
        {
            task = new Task( this.Process );
            task.Start();
        }
        public virtual void Process()
        {
            while (!store.quit)
            {
                current_uri = store.Remove(hungry);

                ischecked = true;
                Check();
                if (ischecked)
                {
                    string code = GetSourceCode(current_uri);
                    if (code != null && code != "")
                    {
                        // 下载资源链接
                        ProcessResource(code);
                        // 如果是子链接，则不需抓取
                        if (hungry) return;
                        // 添加网页链接
                        else ProcessLink(code);
                    }
                }
            }
        }


        // 添加网页链接
        public virtual void ProcessLink(String code)
        {
            HashSet<String> set = ParseLink(code);
            foreach (String link in set)
            {
                if (SelectUri(link))
                {
                    store.Add(link);
                }
            }
        }
        // 下载链接
        public virtual void ProcessResource(String code)
        {
            String filename;
            Dictionary<String, String> dict = ParseResource(code);
            
            foreach ( String image_link in dict.Keys )
            {
                filename = dict[image_link];
                filename += "/" + image_link.Substring(
                        image_link.LastIndexOf('/') + 1);
                DownloadLink(image_link, filename);
            }
        }
        // 筛选链接
        public virtual bool SelectUri(string uri)
        {
            if (! uri.ToLower().StartsWith("http") )
                return false;
            if (! new Uri(uri).Host.ToLower().Equals(
                new Uri(current_uri).Host.ToLower()) )
                return false;
            return true;
        }


        // 解析页面中包含的页面/资源,绝对链接
        public abstract HashSet<String> ParseLink(String code);
        // 链接 & 标题 字典，以便根据标题来决定存放的目录
        public abstract Dictionary<String, String> ParseResource(String code);
        // 返回资源的父文件夹
        public abstract String FormatAlt(String alt);
        /**
         * 检查当前状态，以暂停当前线程
         * if( current_url == null )
         *
         * if( ++count > 10 )
         */
        public abstract void Check();



        public String GetSourceCode(String uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Timeout = timeout_span;
            string encoding = request.TransferEncoding;
            if (encoding == null)
                encoding = Encoding.Default.BodyName;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding)))
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
            if (SelectResource(response))
            {
                if (!FilterFile(filename))
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
                response.GetResponseStream()))
            {
                using (FileStream stream = new FileStream(filename, FileMode.Create))
                {
                    byte[] buffer = new byte[buffer_size];
                    int size = reader.Read(buffer, 0, buffer_size);
                    while (size != -1)
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
            if (response.ContentType.ToLower().StartsWith("text/"))
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
                Console.WriteLine("文件已存在 " + file.FullName);
                return true;
            } else
            {
                return false;
            }
        }
    

    }
}
