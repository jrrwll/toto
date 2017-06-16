using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Toto.Arith.Crypto
{
    public static partial class EasyCrypto
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns></returns>
        public static byte[] Encrypt( byte[] source, byte[] key, byte[] iv, ICryptoTransform transform )
        {
            using (MemoryStream mem = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(mem, transform, CryptoStreamMode.Write))
                {
                    cs.Write(source, 0, source.Length);
                    cs.FlushFinalBlock();
                    return mem.ToArray();
                }
            }
        }
        public static Stream Encrypt( Stream stream, byte[] key, byte[] iv, ICryptoTransform transform )
        {
            byte[] source = new byte[stream.Length];
            stream.Read(source, 0, source.Length);
            return new MemoryStream( Encrypt( source, key, iv, transform) );
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns></returns>
        public static byte[] Decrypt( byte[] source, byte[] key, byte[] iv, ICryptoTransform transform )
        {
            using (CryptoStream cs = new CryptoStream(
                new MemoryStream(source), transform, CryptoStreamMode.Read ) )
            {
                byte[] target = new byte[cs.Length];
                cs.Read(target, 0, target.Length);
                return target;
            }
        }
        public static Stream Decrypt( Stream stream, byte[] key, byte[] iv, ICryptoTransform transform )
        {
            byte[] source = new byte[stream.Length];
            stream.Read(source, 0, source.Length);
            return new MemoryStream( Decrypt(source, key, iv, transform) );
        }

        //--------------------------------  --------------------------------
        //--------------------------------  --------------------------------

        /// <summary>
        /// Rijndael对称算法加密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] RijndaelEncrypt( byte[] source, byte[] key, byte[] iv )
        {
            RijndaelManaged m = new RijndaelManaged();
            ICryptoTransform tranform = m.CreateEncryptor(key, iv);
            return Encrypt(source, key, iv, tranform);
        }
        public static Stream RijndaelEncrypt(Stream stream, byte[] key, byte[] iv)
        {
            byte[] source = new byte[stream.Length];
            stream.Read(source, 0, source.Length);
            return new MemoryStream(RijndaelEncrypt(source, key, iv));
        }

        /// <summary>
        /// Rijndael对称算法解密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] RijndaelDecrypt(byte[] source, byte[] key, byte[] iv)
        {
            RijndaelManaged m = new RijndaelManaged();
            ICryptoTransform tranform = m.CreateEncryptor(key, iv);
            return Decrypt(source, key, iv, tranform);
        }
        public static Stream RijndaelDecrypt(Stream stream, byte[] key, byte[] iv)
        {
            byte[] source = new byte[stream.Length];
            stream.Read(source, 0, source.Length);
            return new MemoryStream(RijndaelDecrypt(source, key, iv));
        }

        //--------------------------------  --------------------------------

        /// <summary>
        /// RC2算法加密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] RC2Encrypt(byte[] source, byte[] key, byte[] iv)
        {
            RC2CryptoServiceProvider m = new RC2CryptoServiceProvider();
            ICryptoTransform tranform = m.CreateEncryptor(key, iv);
            return Encrypt(source, key, iv, tranform);
        }
        public static Stream RC2Encrypt(Stream stream, byte[] key, byte[] iv)
        {
            byte[] source = new byte[stream.Length];
            stream.Read(source, 0, source.Length);
            return new MemoryStream(RC2Encrypt(source, key, iv));
        }

        /// <summary>
        /// RC2算法解密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] RC2Decrypt(byte[] source, byte[] key, byte[] iv)
        {
            RC2CryptoServiceProvider m = new RC2CryptoServiceProvider();
            ICryptoTransform tranform = m.CreateEncryptor(key, iv);
            return Decrypt(source, key, iv, tranform);
        }
        public static Stream RC2Decrypt(Stream stream, byte[] key, byte[] iv)
        {
            byte[] source = new byte[stream.Length];
            stream.Read(source, 0, source.Length);
            return new MemoryStream(RC2Decrypt(source, key, iv));
        }

        //--------------------------------  --------------------------------

        /// <summary>
        /// DES算法加密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] DESEncrypt(byte[] source, byte[] key, byte[] iv)
        {
            DESCryptoServiceProvider m = new DESCryptoServiceProvider();
            ICryptoTransform tranform = m.CreateEncryptor(key, iv);
            return Encrypt(source, key, iv, tranform);
        }
        public static Stream DESEncrypt(Stream stream, byte[] key, byte[] iv)
        {
            byte[] source = new byte[stream.Length];
            stream.Read(source, 0, source.Length);
            return new MemoryStream(DESEncrypt(source, key, iv));
        }

        /// <summary>
        /// DES算法解密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] DESDecrypt(byte[] source, byte[] key, byte[] iv)
        {
            DESCryptoServiceProvider m = new DESCryptoServiceProvider();
            ICryptoTransform tranform = m.CreateEncryptor(key, iv);
            return Decrypt(source, key, iv, tranform);
        }
        public static Stream DESDecrypt(Stream stream, byte[] key, byte[] iv)
        {
            byte[] source = new byte[stream.Length];
            stream.Read(source, 0, source.Length);
            return new MemoryStream(DESDecrypt(source, key, iv));
        }

        //--------------------------------  --------------------------------

        /// <summary>
        /// Aes算法加密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] AesEncrypt(byte[] source, byte[] key, byte[] iv)
        {
            AesCryptoServiceProvider m = new AesCryptoServiceProvider();
            ICryptoTransform tranform = m.CreateEncryptor(key, iv);
            return Encrypt(source, key, iv, tranform);
        }
        public static Stream AesEncrypt(Stream stream, byte[] key, byte[] iv)
        {
            byte[] source = new byte[stream.Length];
            stream.Read(source, 0, source.Length);
            return new MemoryStream(AesEncrypt(source, key, iv));
        }

        /// <summary>
        /// Aes算法解密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] AesDecrypt(byte[] source, byte[] key, byte[] iv)
        {
            AesCryptoServiceProvider m = new AesCryptoServiceProvider();
            ICryptoTransform tranform = m.CreateEncryptor(key, iv);
            return Decrypt(source, key, iv, tranform);
        }
        public static Stream AesDecrypt(Stream stream, byte[] key, byte[] iv)
        {
            byte[] source = new byte[stream.Length];
            stream.Read(source, 0, source.Length);
            return new MemoryStream(AesDecrypt(source, key, iv));
        }

        //--------------------------------  --------------------------------

        /// <summary>
        /// TripleDES算法加密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] TripleDESEncrypt(byte[] source, byte[] key, byte[] iv)
        {
            TripleDESCryptoServiceProvider m = new TripleDESCryptoServiceProvider();
            ICryptoTransform tranform = m.CreateEncryptor(key, iv);
            return Encrypt(source, key, iv, tranform);
        }
        public static Stream TripleDESEncrypt(Stream stream, byte[] key, byte[] iv)
        {
            byte[] source = new byte[stream.Length];
            stream.Read(source, 0, source.Length);
            return new MemoryStream(TripleDESEncrypt(source, key, iv));
        }

        /// <summary>
        /// TripleDES算法解密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] TripleDESDecrypt(byte[] source, byte[] key, byte[] iv)
        {
            TripleDESCryptoServiceProvider m = new TripleDESCryptoServiceProvider();
            ICryptoTransform tranform = m.CreateEncryptor(key, iv);
            return Decrypt(source, key, iv, tranform);
        }
        public static Stream TripleDESDecrypt(Stream stream, byte[] key, byte[] iv)
        {
            byte[] source = new byte[stream.Length];
            stream.Read(source, 0, source.Length);
            return new MemoryStream(TripleDESDecrypt(source, key, iv));
        }

        //--------------------------------  --------------------------------
        //--------------------------------  --------------------------------

        /// <summary>
        /// Hash摘要
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static byte[] HashDigest(this byte[] source, string method="MD5")
        {
            HashAlgorithm a; 
            switch ( method.ToUpper())
            {
                case "MD5":
                    a = new MD5CryptoServiceProvider(); break;
                default:
                    throw new ArgumentException("Invalid hash digest method name: " + method);

                case "SHA1":
                    a = new SHA1CryptoServiceProvider(); break;
                case "SHA256":
                    a = new SHA256CryptoServiceProvider(); break;
                case "SHA384":
                    a = new SHA384CryptoServiceProvider(); break;
                case "SHA512":
                    a = new SHA512CryptoServiceProvider(); break;
            }

            byte[] target = a.ComputeHash(source);
            a.Clear();
            return target;
        }


    }
}
