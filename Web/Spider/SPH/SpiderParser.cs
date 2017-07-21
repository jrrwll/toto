using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Toto.Web.Spider.SPH
{
    public abstract class SpiderParser<T, R>
    {
        public int number;
        public String current_uri;
        public SpiderStore<T, R> store;
        public SpiderHelper helper;
        public Task task;
        public bool hungry;
        public bool ischecked;

        public SpiderParser(SpiderStore<T, R> store, int num, bool hungry)
        {
            this.store = store;
            this.helper = store.helper;
            this.number = num;
            this.hungry = hungry;
        }
        public SpiderParser(SpiderStore<T, R> store, int num): this(store, num, false){}


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
                    string code = helper.GetSourceCode(current_uri);
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
                helper.DownloadLink(image_link, filename);
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

    }
}
