namespace Copter.Mapper
{
    internal class InternalAutoMapper:IMapper
    {
        public TTarget Map<TSource, TTarget>(TSource source) where TSource : class where TTarget : class
        {
            return AutoMapper.Mapper.Map<TTarget>(source);
        }
    }
}
