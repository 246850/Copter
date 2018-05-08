using System.Text;

namespace Copter.Infrastructure.Security
{
    /// <summary>
    /// 加解密 接口
    /// </summary>
    public interface ICryptor
    {
        /// <summary>
        /// 加密字符串 - 默认采用 UT8 编码
        /// </summary>
        /// <param name="source">源 字符串</param>
        /// <returns>加密后字符串</returns>
        string Encrypt(string source);
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="source">源 字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>加密后字符串</returns>
        string Encrypt(string source, Encoding encoding);

        /// <summary>
        /// 解密字符串 - 默认采用 UT8 编码
        /// </summary>
        /// <param name="source">源 字符串</param>
        /// <returns>解密后的字符串</returns>
        string Decrypt(string source);

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="source">源 字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>解密后的字符串</returns>
        string Decrypt(string source, Encoding encoding);
    }
}
