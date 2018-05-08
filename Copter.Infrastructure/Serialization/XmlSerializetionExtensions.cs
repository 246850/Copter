using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Copter.Infrastructure.Serialization
{
    public static class XmlSerializetionExtensions
    {
        public static string SerializeXml(this object source)
        {
            return SerializeXml(source, Encoding.UTF8);
        }

        public static string SerializeXml(this object source, Encoding encoding)
        {
            if (source == null) throw new ArgumentNullException("source", "参数名source为null！");
            if (encoding == null) throw new ArgumentNullException("encoding", "encoding！");

            using (MemoryStream memoryStream = new MemoryStream())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);
                xmlSerializer.Serialize(memoryStream, source, namespaces);
                string result = encoding.GetString(memoryStream.GetBuffer());
                memoryStream.Close();
                return result;
            }
        }

        public static T DeserializeXml<T>(this string source) where T : class, new()
        {
            return DeserializeXml<T>(source, Encoding.UTF8);
        }

        public static T DeserializeXml<T>(this string source, Encoding encoding) where T : class, new()
        {
            if (source == null) throw new ArgumentNullException("source", "参数名source为null！");
            if (encoding == null) throw new ArgumentNullException("encoding", "encoding！");

            using (MemoryStream memoryStream = new MemoryStream(encoding.GetBytes(source)))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                T result = (T)xmlSerializer.Deserialize(memoryStream);
                memoryStream.Close();

                return result;
            }
        }
    }
}
