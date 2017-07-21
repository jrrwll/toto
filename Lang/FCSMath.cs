using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang
{
    public class FCSMath
    {
        /// <summary>
        /// 无限序列，TakeWhile可限制迭代数
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<int> FibonacciSequence()
        {
            int first = 0;
            int second = 1;
            int temp;

            while (true)
            {
                yield return first;

                temp = first;
                first = second;
                second += temp;
            }
        }

        /// <summary>
        /// 复合函数
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="func1"></param>
        /// <param name="func2"></param>
        /// <returns></returns>
 


    }
}
