using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toto.Web.Spider.SPH
{
    public abstract class SpiderStore<T, R>
    {
        // 已经处理过的链接集
        protected T exist_queue;
        // 等待多线程处理的链接集
        protected R waiting_queue;
        // 急需等待多线程处理的链接集
        protected R hungry_queue;

        // 退出信号
        public bool quit;
        // 工作根目录
        public String folder;
        // 种子链接
        public String seed_uri;
        // 共享帮助器
        public SpiderHelper helper;


        public abstract void Add(String uri);
        public abstract String Remove(bool hungry);
        public abstract void Start(String seed_uri, int tasks, int hungry_tasks);

    }
}
