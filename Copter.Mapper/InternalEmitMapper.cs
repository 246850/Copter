using EmitMapper;
namespace Copter.Mapper
{
    internal class InternalEmitMapper: IMapper
    {
        public TTarget Map<TSource, TTarget>(TSource source) where TSource : class where TTarget : class
        {
            ObjectsMapper<TSource, TTarget> mapper = ObjectMapperManager.DefaultInstance.GetMapper<TSource, TTarget>();
            return mapper.Map(source);
        }
    }
}
