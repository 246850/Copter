using System.Text;

namespace Copter.Infrastructure.Generator
{
    /// <summary>
    /// 随机字符串类
    /// </summary>
    public sealed class NonceGenerator
    {
        /// <summary>
        /// 生成随机字符串[a-zA-Z0-9] 默认长度 10
        /// </summary>
        /// <returns>随机字符串</returns>
        public static string GenerateString()
        {
            return GenerateString(10);
        }

        /// <summary>
        /// 生成随机字符串[a-zA-Z0-9]
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <returns>随机字符串</returns>
        public static string GenerateString(int length)
        {
            string[] alphabets = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            StringBuilder nonceString = new StringBuilder(50);
            
            while (nonceString.Length < length)
            {
                int index = CopterRandom.Default.Next(alphabets.Length);
                nonceString.Append(alphabets[index]);
            }
            return nonceString.ToString();
        }
    }
}
