using System;
using System.Text;
using Copter.Infrastructure.ValueObject;

namespace Copter.Infrastructure.Generator
{
    /// <summary>
    /// 文件名称 生成类
    /// </summary>
    public sealed class FileNameGenerator
    {
        /// <summary>
        /// 随机生成不重复的 文件名 格式：prefix + Guid + DateTime.Ticks + 随机数 + extension
        /// </summary>
        /// <param name="extension">文件扩展名</param>
        /// <returns>合法的文件名</returns>
        public static string GenerateName(string extension)
        {
            return GenerateName(string.Empty, extension);
        }

        /// <summary>
        /// 随机生成不重复的 文件名 格式：prefix + Guid + DateTime.Ticks + 随机数 + extension
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <param name="extension">文件扩展名</param>
        /// <returns>合法的文件名</returns>
        public static string GenerateName(string prefix, string extension)
        {
            StringBuilder fileName = new StringBuilder();
            if (!string.IsNullOrEmpty(prefix))
                fileName.Append(prefix);

            fileName.AppendFormat("{0}{1}_{2}{3}", Guid.NewGuid().ToGuidString(), DateTime.Now.Ticks.ToString("x"), CopterRandom.Default.Next(100, 999), extension);

            return fileName.ToString();
        }
    }
}
