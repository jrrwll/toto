using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Toto.Arith
{
    public static partial class PeculiarInteger
    {

        // 水仙花数 abc = a^3 + b^3 + c^3
        public static List<int> GetDaffodil3()
        {
            var list = new List<int>();
            int a, b, c;
            for (int i = 100; i <= 999; i++)
            {
                a = i / 100;
                Math.DivRem(i, 100, out b);
                b /= 10;
                Math.DivRem(i, 100, out c);
                a = a * a * a;
                b = b * b * b;
                c = c * c * c;
                if ((a + b + c) == i) list.Add(i);
            }
            return list.Count == 0 ? null : list;
        }

        public static List<int> GetDaffodil2()
        {
            var list = new List<int>();
            int a, b;
            for (int i = 10; i <= 99; i++)
            {
                a = i / 10;
                Math.DivRem(i, 10, out b);
                a = a * a;
                b = b * b;
                if ((a + b) == i) list.Add(i);
            }
            return list.Count == 0 ? null : list;
        }

        public static List<int> GetDaffodil4()
        {
            var list = new List<int>();
            int a, b, c, d;
            int count = 90000 / Environment.ProcessorCount + 1;

            Parallel.ForEach(Partitioner.Create(1000, 9999, count), (rang) =>
            {
                for (int i = rang.Item1; i < rang.Item2; i++)
                {
                    a = i / 1000;
                    Math.DivRem(i, 1000, out b);
                    b /= 100;
                    Math.DivRem(i, 100, out c);
                    c /= 10;
                    Math.DivRem(i, 10, out d);

                    a = a * a * a * a;
                    b = b * b * b * b;
                    c = c * c * c * c;
                    d = d * d * d * d;

                    if ((a + b + c + d) == i)
                        list.Add(i);
                }
            });
            return list.Count == 0 ? null : list;
        }

        public static List<int> GetDaffodil5()
        {
            var list = new List<int>();
            int a, b, c, d, e;

            Parallel.ForEach(Partitioner.Create(10000, 99999), (rang) =>
            {
                for (int i = rang.Item1; i < rang.Item2; i++)
                {
                    a = i / 10000;
                    Math.DivRem(i, 10000, out b);
                    b /= 1000;
                    Math.DivRem(i, 1000, out c);
                    c /= 100;
                    Math.DivRem(i, 100, out d);
                    d /= 10;
                    Math.DivRem(i, 10, out e);

                    a = a * a * a * a * a;
                    b = b * b * b * b * b;
                    c = c * c * c * c * c;
                    d = d * d * d * d * d;
                    e = e * e * e * e * e;

                    if ((a + b + c + d + e) == i)
                        list.Add(i);
                }
            });
            return list.Count == 0 ? null : list;
        }

        public static List<int> GetDaffodil(int n)
        {
            var list = new List<int>();
            int[] p = new int[n];

            var item1 = (int)Math.Pow(10, n - 1);
            var item2 = (int)Math.Pow(10, n) - 1;
            int sum = 0;

            Parallel.ForEach(Partitioner.Create(item1, item2), (rang) =>
            {
                for (int i = rang.Item1; i < rang.Item2; i++)
                {
                    for (int j = n - 1; j >= 0; i--)
                    {
                        Math.DivRem(i, (int)Math.Pow(10, j + 1), out p[j]);
                        p[j] /= (int)Math.Pow(10, j);
                        sum += (int)Math.Pow(p[j], n);
                    }
                    if (sum == i)
                        list.Add(i);
                }
            });
            return list.Count == 0 ? null : list;
        }


    }
}
