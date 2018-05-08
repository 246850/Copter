using Nelibur.ObjectMapper;
namespace Copter.Mapper
{
    internal class InternalTinyMapper:IMapper
    {
        public TTarget Map<TSource, TTarget>(TSource source) where TSource : class where TTarget : class
        {
            return TinyMapper.Map<TSource, TTarget>(source);
        }
    }
}
