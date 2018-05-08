using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Copter.Infrastructure.Serialization
{
    /// <summary>
    /// json序列化 扩展类
    /// </summary>
    public static class JsonSerializtionExtensions
    {
        /// <summary>
        /// 对象 序列化 json
        /// </summary>
        /// <param name="source">源对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(this object source)
        {
#if DEBUG
            return SerializeObject(source, Formatting.Indented);
#else
            return SerializeObject(source, Formatting.None);
#endif
        }

        /// <summary>
        /// 对象 序列化 json 带默认时间格式yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="formatting">[None|Indented]</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(this object source, Formatting formatting)
        {
            return SerializeObject(source, formatting, new IsoDateTimeConverter
            {
                Culture = CultureInfo.InvariantCulture,
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            });
        }

        /// <summary>
        /// 对象 序列化 json
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="formatting">[None|Indented]</param>
        /// <param name="converter">时间格式</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(this object source, Formatting formatting, IsoDateTimeConverter converter)
        {
            string result = JsonConvert.SerializeObject(source, formatting, converter);
            return result;
        }

        /// <summary>
        /// json 反序列化 对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="source">源json</param>
        /// <returns>T类型的对象</returns>
        public static T DeserializeObject<T>(this string source)
        {
            T result = JsonConvert.DeserializeObject<T>(source);
            return result;
        }
    }
}
