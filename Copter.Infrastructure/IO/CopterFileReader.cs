using System.IO;
using System.Text;

namespace Copter.Infrastructure.IO
{
    /// <summary>
    /// 文件读取 工具类
    /// </summary>
    public sealed class CopterFileReader
    {
        public static string Read(string filePath)
        {
            return Read(filePath, Encoding.UTF8);
        }

        public static string Read(string filePath, Encoding encoding)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, encoding))
                {
                    string result = streamReader.ReadToEnd();
                    streamReader.Close();
                    fileStream.Close();

                    return result;
                }
            }
        }
    }
}
