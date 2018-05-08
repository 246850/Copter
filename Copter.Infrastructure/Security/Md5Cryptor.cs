using System.Security.Cryptography;
using System.Text;

namespace Copter.Infrastructure.Security
{
    /// <summary>
    /// MD5 加密 工具类
    /// </summary>
    public class Md5Cryptor : ICryptor
    {
        public Md5Cryptor():this(string.Empty)
        {

        }
        public Md5Cryptor(string salt)
        {
            Salt = salt;
        }
        protected string Salt { get; }
        public string Encrypt(string source)
        {
            return Encrypt(source, Encoding.UTF8);
        }

        public string Encrypt(string source, Encoding encoding)
        {
            byte[] byteArray = encoding.GetBytes(string.Concat(source, Salt));
            using (HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider())
            {
                byteArray = hashAlgorithm.ComputeHash(byteArray);
                StringBuilder stringBuilder = new StringBuilder(256);
                foreach (byte item in byteArray)
                    stringBuilder.AppendFormat("{0:x2}", item);
                hashAlgorithm.Clear();
                return stringBuilder.ToString();
            }
        }

        public string Decrypt(string source)
        {
            throw new System.NotImplementedException();
        }

        public string Decrypt(string source, Encoding encoding)
        {
            throw new System.NotImplementedException();
        }
    }
}
