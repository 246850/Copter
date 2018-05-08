using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Copter.Infrastructure.Caching;

namespace Copter.Infrastructure.Enum
{
    /// <summary>
    /// 枚举类型 扩展类
    /// </summary>
    public static class EnumExtensions
    {

        static readonly Lazy<ICacheManager> cacheManager = new Lazy<ICacheManager>(() => new MemoryCacheManager());
        static ICacheManager CacheManager
        {
            get
            {
                return cacheManager.Value;
            }
        }
        private static readonly object LockObj1 = new object();

        /// <summary>
        /// 获取枚举 文本属性 DisplayText
        /// </summary>
        /// <typeparam name="TEnum">枚举类型 T</typeparam>
        /// <param name="source">当前实例</param>
        /// <returns>文本字符串</returns>
        public static string ToText<TEnum>(this TEnum source) where TEnum : struct
        {
            Type type = typeof(TEnum);
            if (!type.IsEnum)
                throw new ArgumentException("非枚举类型不能调用ToText()方法", source.ToString());

            string name = source.ToString(),
                key = string.Format("{0}.{1}.Text", type.FullName, name);

            return CacheManager.Get<string>(key, () =>
            {
                if (!CacheManager.Contains(key))
                    lock (LockObj1)
                        if (!CacheManager.Contains(key))
                        {
                            FieldInfo field = source.GetType().GetField(name);
                            if (field != null)
                            {
                                object[] customAttributes = field.GetCustomAttributes(typeof(DisplayTextAttribute), false);
                                if (customAttributes.Any())
                                {
                                    string text = ((DisplayTextAttribute)customAttributes[0]).DisplayText;
                                    CacheManager.Set(key, text);
                                }
                            }
                        }
            });
        }

        /// <summary>
        /// 获取枚举 Value值
        /// </summary>
        /// <typeparam name="TEnum">枚举类型 T</typeparam>
        /// <param name="source">当前实例</param>
        /// <returns>Value值</returns>
        public static int ToValue<TEnum>(this TEnum source) where TEnum : struct
        {
            Type type = typeof(TEnum);
            if (!type.IsEnum)
                throw new ArgumentException("非枚举类型不能调用ToValue()方法", source.ToString());

            string name = source.ToString(),
                key = string.Format("{0}.{1}.Value", type.FullName, name);

            return CacheManager.Get<int>(key, () =>
            {
                if (!CacheManager.Contains(key))
                    lock (LockObj1)
                        if (!CacheManager.Contains(key))
                        {
                            int result = Convert.ToInt32(source);
                            CacheManager.Set(key, result);
                        }
            });
        }

        /// <summary>
        /// 获取枚举 Value值 列表
        /// </summary>
        /// <typeparam name="TEnum">枚举类型 T</typeparam>
        /// <param name="source">当前实例</param>
        /// <returns>Value值 列表</returns>
        public static IList<int> ToValueList<TEnum>(this TEnum source) where TEnum : struct
        {
            Type type = source.GetType();
            if (!type.IsEnum)
                throw new ArgumentException("非枚举类型不能调用ToValueList()方法", source.ToString());

            string name = source.ToString(),
                key = string.Format("{0}.{1}.ValueList", type.FullName, name);
            return CacheManager.Get<IList<int>>(key, () =>
            {
                if (!CacheManager.Contains(key))
                    lock (LockObj1)
                        if (!CacheManager.Contains(key))
                        {
                            Array values = System.Enum.GetValues(type);
                            IList<int> items = new List<int>();
                            foreach (TEnum value in values)
                                items.Add(Convert.ToInt32(value));
                            if (items.Any())
                                CacheManager.Set(key, items);
                        }
            });
        }

        /// <summary>
        /// 枚举类型 转换成 文本-值对象 列表 不包含默认值
        /// </summary>
        /// <typeparam name="TEnum">枚举类型 T</typeparam>
        /// <param name="source">当前实例</param>
        /// <returns>TextValueItem文本-值对象 列表</returns>
        public static IList<TextValueItem> ToTextValueList<TEnum>(this TEnum source) where TEnum : struct
        {
            return ToTextValueList(source, false);
        }

        /// <summary>
        /// 枚举类型 转换成 文本-值对象 列表
        /// </summary>
        /// <typeparam name="TEnum">枚举类型 T</typeparam>
        /// <param name="source">当前实例</param>
        /// <param name="defaulted">是否添加一条默认的数据</param>
        /// <returns>TextValueItem文本-值对象 列表</returns>
        public static IList<TextValueItem> ToTextValueList<TEnum>(this TEnum source, bool defaulted) where TEnum : struct
        {
            Type type = source.GetType();
            if (!type.IsEnum)
                throw new ArgumentException("非枚举类型不能调用ToTextValueList()方法", source.ToString());

            string name = source.ToString(),
                key = string.Format("{0}.{1}.{2}.TextValueList", type.FullName, name, defaulted);
            return CacheManager.Get<IList<TextValueItem>>(key, () =>
            {
                if (!CacheManager.Contains(key))
                    lock (LockObj1)
                        if (!CacheManager.Contains(key))
                        {
                            List<TextValueItem> items = new List<TextValueItem>();
                            if (defaulted)
                            {
                                items.Add(new TextValueItem
                                {
                                    Value = -1,
                                    Text = "请选择",
                                    Checked = true
                                });
                            }

                            Array values = System.Enum.GetValues(type);
                            int current = Convert.ToInt32(source);
                            foreach (TEnum value in values)
                            {
                                int temp = Convert.ToInt32(value);
                                TextValueItem item = new TextValueItem
                                {
                                    Text = value.ToText(),
                                    Value = temp
                                };
                                if (!defaulted) item.Checked = temp == current;
                                items.Add(item);
                            }
                            if (items.Count > 0)
                                CacheManager.Set(key, items);
                        }
            });
        }

        /// <summary>
        /// 枚举类型 转换成 SelectListItem 列表 - 不包含默认值
        /// </summary>
        /// <typeparam name="TEnum">枚举类型 T</typeparam>
        /// <param name="source">当前实例</param>
        /// <returns>SelectListItem 列表</returns>
        public static IList<SelectListItem> ToSelectList<TEnum>(this TEnum source) where TEnum : struct
        {
            return ToSelectList(source, false);
        }

        /// <summary>
        /// 枚举类型 转换成 SelectListItem 列表
        /// </summary>
        /// <typeparam name="TEnum">枚举类型 T</typeparam>
        /// <param name="source">当前实例</param>
        /// <param name="defaulted">是否添加一条默认的数据</param>
        /// <returns>SelectListItem 列表</returns>
        public static IList<SelectListItem> ToSelectList<TEnum>(this TEnum source, bool defaulted) where TEnum : struct
        {
            Type type = source.GetType();
            if (!type.IsEnum)
                throw new Exception("非枚举类型不能调用ToSelectItemList()方法");

            string name = source.ToString(),
                key = string.Format("{0}.{1}.{2}.SelectItemList", type.FullName, name, defaulted);
            return CacheManager.Get<IList<SelectListItem>>(key, () =>
            {
                if (!CacheManager.Contains(key))
                    lock (LockObj1)
                        if (!CacheManager.Contains(key))
                        {
                            List<SelectListItem> items = new List<SelectListItem>();
                            if (defaulted)
                            {
                                items.Add(new SelectListItem
                                {
                                    Value = "-1",
                                    Text = "请选择",
                                    Selected = true
                                });
                            }

                            Array values = System.Enum.GetValues(type);
                            string current = Convert.ToInt32(source).ToString();
                            foreach (TEnum value in values)
                            {
                                string temp = Convert.ToInt32(value).ToString();
                                SelectListItem item = new SelectListItem
                                {
                                    Text = value.ToText(),
                                    Value = temp
                                };
                                if (!defaulted) item.Selected = temp == current;
                                items.Add(item);
                            }
                            if (items.Count > 0)
                                CacheManager.Set(key, items);
                        }
            });
        }

        /// <summary>
        /// 枚举类型 转 Dictionary
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDictionary<int, string> ToDictionary<TEnum>(this TEnum source) where TEnum : struct
        {
            IDictionary<int, string> dictionary = new Dictionary<int, string>();
            Type type = source.GetType();
            if (!type.IsEnum)
                return dictionary;

            Array values = System.Enum.GetValues(type);
            foreach (TEnum key in values)
                dictionary.Add(Convert.ToInt32(key), key.ToText());
            return dictionary;
        }
    }
}
