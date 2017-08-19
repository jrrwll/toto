using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;

namespace Toto.Arith.Crypto
{
    public static partial class EasyCrypto
    {
        /// <summary>
        /// ROT 旋转字母表
        /// 使用 key 加密，26 - key 解密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key">前进步数</param>
        /// <returns></returns>
        public static char[] RotCrypto( char[] source, int key )
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

        /// <summary>
        /// XOR 异或算法，使用 source ^ key 加密，target ^ key 解密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int XorCrypto (int source, int key)
        {
            return source ^ key;
        }
        public static long XorCrypto (long source, long key)
        {
            return source ^ key;
        }
        public static BigInteger XorCrypto (BigInteger source, BigInteger key)
        {
            return source ^ key;
        }

        /// <summary>
        /// one-time-pad 算法，对每一位对应作异或，使用两次此法以解密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] OneTimePadCrypto (byte[] source, byte[] key)
        {
            if (source.Length != key.Length)
                throw new ArgumentException("Source & Key must have same length!");
            
            byte[] target = new byte[source.Length];
            for (int i = 0; i < target.Length; i++)
            {
                target [i] = (byte) (source [i] ^ key [i]);
            }
            return target;
        }
        
    }
}
