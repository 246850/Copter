using System;

namespace Copter.Infrastructure.Enum
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DisplayTextAttribute : Attribute
    {
        /// <summary>
        /// 文本字符串
        /// </summary>
        public string DisplayText { get; private set; }

        public DisplayTextAttribute() : this(string.Empty) { }

        public DisplayTextAttribute(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new Exception("text不能为空或null");

            DisplayText = text;
        }
    }
}
