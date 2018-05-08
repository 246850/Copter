using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Copter.Infrastructure.Security
{
    /// <summary>
    /// MD5 加解密 工具类
    /// </summary>
    public class DesCryptor : ICryptor
    {
        public string DesIv { get; }
        public string DesKey { get; }

        public DesCryptor():this("Q^_3%@k(", "P@*$m7)%")
        {
            
        }

        public DesCryptor(string desIv, string desKey)
        {
            DesIv = desIv;
            DesKey = desKey;
        }

        static byte[] GetDesKey(string key, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key", "Des密钥不能为空");
            if (key.Length > 8)
                key = key.Substring(0, 8);
            if (key.Length < 8)
                key = key.PadRight(8, '0');
            return encoding.GetBytes(key);
        }
        public string Encrypt(string source, string key, string iv, Encoding encoding)
        {
            byte[] rgbKeys = GetDesKey(key, encoding),
                    rgbIvs = GetDesKey(iv, encoding),
                    inputByteArray = encoding.GetBytes(source);
            using (DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider())
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desProvider.CreateEncryptor(rgbKeys, rgbIvs), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                        cryptoStream.FlushFinalBlock();
                        cryptoStream.Close();
                        memoryStream.Flush();
                        memoryStream.Close();
                        desProvider.Clear();
                        string result = Convert.ToBase64String(memoryStream.ToArray());
                        return result;
                    }
                }
            }
        }
        public string Decrypt(string source, string key, string iv, Encoding encoding)
        {
            byte[] rgbKeys = GetDesKey(key, encoding),
                    rgbIvs = GetDesKey(iv, encoding),
                    inputByteArray = Convert.FromBase64String(source);
            using (DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider())
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, desProvider.CreateDecryptor(rgbKeys, rgbIvs), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                        cryptoStream.FlushFinalBlock();
                        cryptoStream.Close();
                        memoryStream.Flush();
                        memoryStream.Close();
                        desProvider.Clear();
                        byte[] result = memoryStream.ToArray();
                        return encoding.GetString(result);
                    }
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
            return Encrypt(source, DesKey, DesIv, encoding);
        }

        public string Decrypt(string source)
        {
            return Decrypt(source, Encoding.UTF8);
        }

        public string Decrypt(string source, Encoding encoding)
        {
            return Decrypt(source, DesKey, DesIv, encoding);
        }
        #endregion
    }
}
