using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using Copter.Infrastructure.Generator;

namespace Copter.Infrastructure.Media
{
    public sealed class CopterImage
    {

        private static readonly IDictionary<string, ImageFormat> ImageFormats;

        static CopterImage()
        {
            ImageFormats = new ConcurrentDictionary<string, ImageFormat>();
            PropertyInfo[] propertyInfos = typeof (ImageFormat).GetProperties(BindingFlags.Static | BindingFlags.Public);
            foreach (PropertyInfo property in propertyInfos)
            {
                ImageFormat format = (ImageFormat) property.GetValue(null, null);
                if (format != null)
                    ImageFormats.Add(string.Format(".{0}", property.Name), format);
            }
        }

        public static string DefaultExtension
        {
            get { return ".jpeg"; }
        }

        public static Image Parse(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            return Parse(bytes);
        }

        public static Image Parse(byte[] bytes)
        {
            using (MemoryStream memoryStream = new MemoryStream(bytes, 0, bytes.Length))
            {
                Image result = Parse(memoryStream);
                memoryStream.Close();

                return result;
            }
        }

        public static Image Parse(Stream stream)
        {
            Image image = Image.FromStream(stream);
            return image;
        }

        public static string GetExtension(Image image)
        {
            foreach (KeyValuePair<string, ImageFormat> keyValuePair in ImageFormats)
                if (keyValuePair.Value.Guid == image.RawFormat.Guid)
                    return keyValuePair.Key.ToLower();

            return DefaultExtension;
        }

        public static string GetExtension(string mimeType)
        {
            string extension;
            switch (mimeType.ToLower())
            {
                case "image/jpeg":
                case "image/jpg":
                    extension = ".jpg";
                    break;
                case "image/bmp":
                    extension = ".bmp";
                    break;
                case "image/png":
                    extension = ".png";
                    break;
                case "image/gif":
                    extension = ".gif";
                    break;
                default:
                    extension = DefaultExtension;
                    break;
            }
            return extension;
        }

        public static string GetMimeType(string extension)
        {
            string mimeType;
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    mimeType = "image/jpeg";
                    break;
                case ".gif":
                    mimeType = "image/gif";
                    break;
                case ".bmp":
                    mimeType = "image/bmp";
                    break;
                case ".png":
                    mimeType = "image/png";
                    break;
                default:
                    mimeType = "image/jpeg";
                    break;
            }

            return mimeType;
        }

        public static Image GetThumbnailByWidth(Image image, int width)
        {
            int height = width*image.Height/image.Width;
            return image.GetThumbnailImage(width, height, null, IntPtr.Zero);
        }

        public static Image GetThumbnailByHeight(Image image, int height)
        {
            int width = height*image.Width/image.Height;
            return image.GetThumbnailImage(width, height, null, IntPtr.Zero);
        }

        public static Image GetThumbnailBySize(Image image, int size)
        {
            int height,
                width;
            if (image.Height > image.Width)
            {
                height = size;
                width = height*image.Width/image.Height;
            }
            else
            {
                width = size;
                height = width*image.Height/image.Width;
            }
            return image.GetThumbnailImage(width, height, null, IntPtr.Zero);
        }

        public static Image CreateVerifyImage(string verifyCode)
        {
            using (Bitmap image = new Bitmap((int) Math.Ceiling(verifyCode.Length*15.0), 30))
            {
                using (Graphics g = Graphics.FromImage(image))
                {
                    //清空图片背景色
                    g.Clear(Color.White);
                    //画图片的干扰线
                    for (int i = 0; i < 25; i++)
                    {
                        int x1 = CopterRandom.Default.Next(image.Width);
                        int x2 = CopterRandom.Default.Next(image.Width);
                        int y1 = CopterRandom.Default.Next(image.Height);
                        int y2 = CopterRandom.Default.Next(image.Height);
                        g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                    }
                    Font font = new Font("Arial", 15, (FontStyle.Bold | FontStyle.Italic));
                    LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                        Color.Blue, Color.DarkRed, 1.2f, true);
                    g.DrawString(verifyCode, font, brush, 2, 2);

                    //画图片的前景干扰点
                    for (int i = 0; i < 100; i++)
                    {
                        int x = CopterRandom.Default.Next(image.Width);
                        int y = CopterRandom.Default.Next(image.Height);
                        image.SetPixel(x, y, Color.FromArgb(CopterRandom.Default.Next()));
                    }
                    //画图片的边框线
                    g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                    MemoryStream memoryStream = new MemoryStream();
                    image.Save(memoryStream, ImageFormat.Jpeg);
                    return Parse(memoryStream);
                }
            }
        }
    }
}
