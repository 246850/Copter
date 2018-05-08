namespace Copter.Infrastructure.Generator
{
    public sealed class VerifyCodeGenerator
    {
        /// <summary>
        /// 生成验证码 默认 6个字符串
        /// </summary>
        /// <returns></returns>
        public static string Generate()
        {
            return Generate(6);
        }
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="length">验证码长度</param>
        /// <returns></returns>
        public static string Generate(int length)
        {
            return NonceGenerator.GenerateString(length);
        }
    }
}
