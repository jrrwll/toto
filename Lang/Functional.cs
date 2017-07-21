using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class Functional
    {
        public static Func<S, R> Compose<S, T, R>(Func<S, T> func1, Func<T, R> func2)
        {
            return p => func2(func1(p));
        }
    }
}
