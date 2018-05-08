using System;
using System.IO;

namespace Copter.Infrastructure.IO
{
    /// <summary>
    /// 文件目录 工具类
    /// </summary>
    public sealed class CopterDirectory
    {
        /// <summary>
        /// 根据文件绝对路径，创建对应的文件夹目录，如果不存在
        /// </summary>
        /// <param name="filePath"></param>
        public static void CreateDirectory(string filePath)
        {
            string folderPath = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(folderPath) && !Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }

        /// <summary>
        /// 根据文件夹目录相对路径，生成文件夹目录绝对路径
        /// </summary>
        /// <param name="virtualFolderPath"></param>
        /// <returns></returns>
        public static string GenerateDirectory(string virtualFolderPath)
        {
            virtualFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, virtualFolderPath);
            if (!string.IsNullOrEmpty(virtualFolderPath) && !Directory.Exists(virtualFolderPath))
            {
                Directory.CreateDirectory(virtualFolderPath);
                return virtualFolderPath;
            }
            return string.Empty;
        }
    }
}
