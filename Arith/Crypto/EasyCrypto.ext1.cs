using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Toto.Arith.Crypto
{
    public static partial class EasyCrypto
    {
        /// <summary>
        /// ROT 旋转字母表
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key">前进步数</param>
        /// <returns></returns>
        public static char[] ROTNCrypto( char[] source, int key )
        {
            int len = source.Length, n = 0;

            char[] target = new char[len];
            for(int i = 0; i < len; i++)
            {
                int b = source[i];
                if (b >= 97 && b <= 122) n = 97; // 小写字母
                else if (b >= 65 && b <= 90) n = 65; // 大写字母

                if (n != 0)
                {
                    target[i] = (char)( ( source[i] - n + key ) % 26 );
                } else target[i] = source[i];

            }

            return target;
        }



    }
}
