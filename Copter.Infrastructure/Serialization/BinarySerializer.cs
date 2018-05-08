
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Copter.Infrastructure.IO;

namespace Copter.Infrastructure.Serialization
{
    /// <summary>
    /// 对象二进制序列化
    /// </summary>
    public static class BinarySerializer
    {
        public static void SerializeBinary(this object source, string filePath)
        {
            CopterDirectory.CreateDirectory(filePath);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, source);
            }
        }

        public static T DeserializeBinary<T>(string filePath) where T : class, new()
        {
            T result;
            if (!File.Exists(filePath))
            {
                result = default(T);
            }
            else
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    result = (T)formatter.Deserialize(fileStream);
                }
            }
            return result;
        }
    }
}
