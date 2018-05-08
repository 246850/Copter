using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Copter.Infrastructure.Media
{
    /// <summary>
    /// 图片 扩展类
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// Image 转 字节 - 默认格式 jpeg
        /// </summary>
        /// <param name="source">源</param>
        /// <returns>字节 数组</returns>
        public static byte[] Tobytes(this Image source)
        {
            return ToBytes(source, ImageFormat.Jpeg);
        }

        /// <summary>
        /// Image 转 字节
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="format">图片格式</param>
        /// <returns>字节 数组</returns>
        public static byte[] ToBytes(this Image source, ImageFormat format)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                source.Save(memoryStream, format);
                //byte[] bytes = new byte[memoryStream.Length];
                //memoryStream.Seek(0, SeekOrigin.Begin);
                //memoryStream.Read(bytes, 0, bytes.Length);
                //memoryStream.Close();
                byte[] bytes = memoryStream.ToArray();
                return bytes;
            }
        }

        /// <summary>
        /// Image 转 base64 - 默认格式 jpeg
        /// </summary>
        /// <param name="source">源</param>
        /// <returns>base64 字符串</returns>
        public static string ToBase64(this Image source)
        {
            return ToBase64(source, ImageFormat.Jpeg);
        }

        /// <summary>
        /// Image 转 base64
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="format">图片格式</param>
        /// <returns>base64 字符串</returns>
        public static string ToBase64(this Image source, ImageFormat format)
        {
            byte[] bytes = source.ToBytes(format);
            string result = Convert.ToBase64String(bytes);
            return result;
        }

        /// <summary>
        /// 图片 转 Steam
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="format">图片格式</param>
        /// <returns>Stream 流</returns>
        public static MemoryStream ToStream(this Image source, ImageFormat format)
        {
            MemoryStream memoryStream = new MemoryStream();
            source.Save(memoryStream, format);
            return memoryStream;
        }
    }
}
