using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toto.Arith
{
    public static partial class EasyExample
    {
        public static IEnumerable< Dictionary<int, int> > QueenArrangement(int size)
        {
             Dictionary<int, int> dict = new Dictionary<int, int>();

            int[] q = new int[size];
            int i, j, x, y, d;

            y = 0;
            q[0] = -1;
            while (true)
            {

                for (x = q[y] + 1; x < size; x++)
                {
                    for (i = 0; i < y; i++)
                    {
                        j = q[i];
                        d = y - i;
                        if (( j == x ) || ( j == x - d ) || ( j == x + d )) break; // 相互攻击
                    }

                    if (i >= y) break;
                }

                //
                if (x == size)
                {
                    if (y == 0) break; //回溯至第一行

                    q[y] = -1;
                    y--;
                } else
                {
                    q[y] = x;
                    y++;

                    if(y<size) q[y] = -1;
                    else {
                        for(i=0; i<size; i++)
                        {
                            for (j = 0; j < size; j++)
                            {
                                if (q[i] != j) continue;
                                else
                                {
                                    dict.Add( i, j );
                                }
                            }
                        }

                        yield return dict;
                    }
                }
            }

            


        }




    }

}
