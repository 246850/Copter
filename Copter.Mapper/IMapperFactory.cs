namespace Copter.Mapper
{
    /// <summary>
    /// 类型映射转换 创建工厂接口
    /// </summary>
    public interface IMapperFactory
    {
        /// <summary>
        /// 创建类型映射转换 对象， 默认 MapperType 为：MapperType.AutoMapper
        /// </summary>
        /// <returns>IMapper的实例</returns>
        IMapper Create();
        /// <summary>
        /// 创建类型映射转换 对象
        /// </summary>
        /// <param name="type">MapperType类型</param>
        /// <returns>IMapper的实例</returns>
        IMapper Create(MapperType type);
    }
}
