using System;
using System.Security.Cryptography;
using System.Text;

namespace Copter.Infrastructure.Security
{
    /// <summary>
    /// Aes 加解密 工具类
    /// </summary>
    public class AesCryptor : ICryptor
    {
        public AesCryptor():this("&(*#$@)$@(*@#$@&!)($@#*")
        {
            
        }
        public AesCryptor(string aesKey)
        {
            AesKey = aesKey;
        }
        public string AesKey { get; }

        static byte[] GetAesKey(string key, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key", "Aes密钥不能为空");
            if (key.Length < 32)
                key = key.PadRight(32, '0');
            if (key.Length > 32)
                key = key.Substring(0, 32);
            return encoding.GetBytes(key);
        }

        /// <summary>
        /// Aes加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">aes密钥，长度必须32位</param>
        /// <param name="model">运算模式</param>
        /// <param name="padding">填充模式</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>加密后的字符串</returns>
        public string Encrypt(string source, string key, CipherMode model, PaddingMode padding, Encoding encoding)
        {
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.Key = GetAesKey(key, encoding);
                aesProvider.Mode = model;
                aesProvider.Padding = padding;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor())
                {
                    byte[] inputBuffers = encoding.GetBytes(source),
                        results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }

        /// <summary>
        /// Aes解密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">aes密钥，长度必须32位</param>
        /// <param name="model">运算模式</param>
        /// <param name="padding">填充模式</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string source, string key, CipherMode model, PaddingMode padding, Encoding encoding)
        {
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.Key = GetAesKey(key, encoding);
                aesProvider.Mode = model;
                aesProvider.Padding = padding;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor())
                {
                    byte[] inputBuffers = Convert.FromBase64String(source),
                        results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    return encoding.GetString(results);
                }
            }
        }

        #region 实现
        public string Encrypt(string source)
        {
            return Encrypt(source, Encoding.UTF8);
        }

        public string Encrypt(string source, Encoding encoding)
        {
            return Encrypt(source, AesKey, CipherMode.ECB, PaddingMode.PKCS7, encoding);
        }

        public string Decrypt(string source)
        {
            return Decrypt(source, Encoding.UTF8);
        }

        public string Decrypt(string source, Encoding encoding)
        {
            return Decrypt(source, AesKey, CipherMode.ECB, PaddingMode.PKCS7, encoding);
        }
        #endregion
    }
}
