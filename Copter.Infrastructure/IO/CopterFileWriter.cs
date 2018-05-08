using System.IO;
using System.Text;

namespace Copter.Infrastructure.IO
{
    public sealed class CopterFileWriter
    {
        public static void Write(Stream stream, string filePath)
        {
            CopterDirectory.CreateDirectory(filePath);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                const int size = 1024 * 1024 * 1;
                byte[] buffer = new byte[size];

                int count = stream.Read(buffer, 0, buffer.Length);
                while (count > 0)
                {
                    fileStream.Write(buffer, 0, count);
                    count = stream.Read(buffer, 0, buffer.Length);
                }

                fileStream.Close();
            }
        }

        public static void Write(string filePath, string content)
        {
            Write(filePath, content, false);
        }

        public static void Write(string filePath, string content, bool append)
        {
            Write(filePath, content, append, Encoding.UTF8);
        }

        public static void Write(string filePath, string content, bool append, Encoding encoding)
        {
            CopterDirectory.CreateDirectory(filePath);

            using (FileStream fileStream = new FileStream(filePath, append ? FileMode.Append : FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream, encoding))
                {
                    streamWriter.WriteLine(content);
                    streamWriter.Flush();
                    streamWriter.Close();
                    fileStream.Close();
                }
            }
        }
    }
}
