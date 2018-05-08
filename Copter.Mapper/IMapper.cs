namespace Copter.Mapper
{
    /// <summary>
    /// 类型对象 映射 转换接口
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// 类型对象 映射 转换
        /// </summary>
        /// <typeparam name="TSource">源 T</typeparam>
        /// <typeparam name="TTarget">目标 T</typeparam>
        /// <param name="source">源对象</param>
        /// <returns>目标对象</returns>
        TTarget Map<TSource, TTarget>(TSource source) 
            where TSource : class 
            where TTarget : class;
    }
}
