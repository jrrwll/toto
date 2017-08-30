using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Toto.Arith
{
    public static partial class ElementaryInteger
    {
        // 阶乘
        public static int Factorial(int num)
        {
            if (num < 2) return 1;
            else return num * Factorial(num - 1);
        }
        public static long Factorial(long num)
        {
            if (num < 2) return 1;
            else return num * Factorial(num - 1);
        }


        // 最大公约数
        public static int GCD(int num1, int num2)
        {
            int a = Math.Max(num1, num2);
            int b = Math.Min(num1, num2);

            int m;
            while (b != 0)
            {
                m = a % b;
                a = b;
                b = m;
            }
            return a;
        }
        public static long GCD(long num1, long num2)
        {
            long a = Math.Max(num1, num2);
            long b = Math.Min(num1, num2);

            long m;
            while (b != 0)
            {
                m = a % b;
                a = b;
                b = m;
            }
            return a;
        }

        // 最小公倍数
        public static int LCM(int num1, int num2)
        {
            return (num1 * num2) / GCD(num1, num2);
        }
        public static long LCM(long num1, long num2)
        {
            return (num1 * num2) / GCD(num1, num2);
        }


        // 素数判定
        public static bool IsPrime(int num)
        {
            if (num <= 1) throw new ArgumentOutOfRangeException("Assert int num >= 2");

            if (num == 2) return true;


            for (int i = 3, y = (int)Math.Sqrt(num) + 1; i < y; i = i + 2)
            {
                if (num % i == 0) return false;
            }
            return true;
        }
        public static bool IsPrime(long num)
        {
            if (num <= (long)Math.Pow(2, 20)) return IsPrime((int)num);

            bool result = true;
            long item2 = (long)Math.Sqrt(num) + 1;

            Parallel.ForEach(Partitioner.Create(3, item2), (rang) =>
            {
                for (long i = rang.Item1; i < rang.Item2; i = i + 2)
                {
                    if (num % i == 0) result = false;
                }
            });
            return result;
        }


    }
}
