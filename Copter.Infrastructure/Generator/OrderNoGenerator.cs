using System;
using System.Text.RegularExpressions;
using System.Threading;
using Copter.Infrastructure.ValueObject;

namespace Copter.Infrastructure.Generator
{
    /// <summary>
    /// 订单编号生成类
    /// </summary>
    public sealed class OrderNoGenerator
    {
        /// <summary>
        /// 自增长种子
        /// </summary>
        private static int _increment;

        static string IncrementNumber
        {
            get
            {
                if (_increment > 9998)
                {
                    _increment = 0;
                }
                int result = Interlocked.Increment(ref _increment);

                return result.ToString().PadLeft(4, '0');
            }
        }
        /// <summary>
        /// 生成订单编号 长度等于 serverCode(默认2位数字字符) + 4位随机数字[1000,10000} + 10位时间戳 + 4位自增长种子
        /// </summary>
        /// <returns></returns>
        public static string GenerateNo() => GenerateNo(String.Empty, "10");

        /// <summary>
        /// 生成订单编号 长度等于 prefix(字符长度) + serverCode([2,3]位数字字符) + 4位随机数字[1000,10000} + 10位时间戳 + 4位自增长种子
        /// </summary>
        /// <param name="prefix">订单编号前缀</param>
        /// <param name="serverCode">服务器编号</param>
        /// <returns>订单编号</returns>
        public static string GenerateNo(string prefix, string serverCode)
        {
            if (!Regex.IsMatch(serverCode, @"^\d{2,3}$")) throw new Exception("服务器编号必须是两位或三位整数字符");

            int randomNumber = CopterRandom.Default.Next(1000, 10000);
            string result = string.Concat(prefix, serverCode, randomNumber, DateTime.Now.ToTimestamp(), IncrementNumber);
            return result;
        }
    }
}
