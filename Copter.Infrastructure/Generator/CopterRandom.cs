using System;
using System.Security.Cryptography;

namespace Copter.Infrastructure.Generator
{
    /// <summary>
    /// 强 随机数 类
    /// </summary>
    public sealed class CopterRandom
    {
        /// <summary>
        /// 通过RNGCryptoServiceProvider 创建强随机数生成对象
        /// </summary>
        public static Random Default =>  CreateRandom();

        /// <summary>
        /// 通过RNGCryptoServiceProvider 创建强随机数生成对象 
        /// </summary>
        /// <returns>强随机数生成对象</returns>
        public static Random CreateRandom()
        {
            using (RandomNumberGenerator generator = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[10];
                generator.GetBytes(data);
                int seedInt = BitConverter.ToInt32(data, 0);
                return new Random(seedInt);
            }
        }
    }
}
