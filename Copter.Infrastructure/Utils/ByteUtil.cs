namespace Copter.Infrastructure.Utils
{
    /// <summary>
    /// Byte工具类
    /// </summary>
    public sealed class ByteUtil
    {
        /// <summary>
        /// 二进制 To 16进制
        /// </summary>
        /// <param name="data">byte[]数据</param>
        /// <returns>字符串</returns>
        public static string BinaryToHex(byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            char[] chArray = new char[data.Length * 2];
            for (int i = 0; i < data.Length; i++)
            {
                byte num2 = data[i];
                chArray[2 * i] = NibbleToHex((byte)(num2 >> 4));
                chArray[(2 * i) + 1] = NibbleToHex((byte)(num2 & 15));
            }
            return new string(chArray);
        }

        /// <summary>
        /// 16进制 To 二进制
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>byte[]数据</returns>
        public static byte[] HexToBinary(string data)
        {
            if ((data == null) || ((data.Length % 2) != 0))
            {
                return null;
            }
            byte[] buffer = new byte[data.Length / 2];
            for (int i = 0; i < buffer.Length; i++)
            {
                int num2 = CharUtil.HexToInt(data[2 * i]);
                int num3 = CharUtil.HexToInt(data[(2 * i) + 1]);
                if ((num2 == -1) || (num3 == -1))
                {
                    return null;
                }
                buffer[i] = (byte)((num2 << 4) | num3);
            }
            return buffer;
        }

        private static char NibbleToHex(byte nibble)
        {
            return ((nibble < 10) ? ((char)(nibble + 0x30)) : ((char)((nibble - 10) + 0x41)));
        }
    }
}
