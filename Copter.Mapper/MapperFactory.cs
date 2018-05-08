using System;

namespace Copter.Mapper
{
    public class MapperFactory:IMapperFactory
    {
        public IMapper Create() => Create(MapperType.AutoMapper);

        public IMapper Create(MapperType type)
        {
            IMapper mapper = null;
            switch (type)
            {
                case MapperType.AutoMapper:
                    mapper = new InternalAutoMapper();
                    break;
                case MapperType.EmitMapper:
                    mapper = new InternalEmitMapper();
                    break;
                case MapperType.TinyMapper:
                    mapper = new InternalTinyMapper();
                    break;
                case MapperType.JsonNetMapper:
                    mapper = new InternalJsonNetMapper();
                    break;
                case MapperType.NLiteMapper:
                    mapper = new InternalNLiteMapper();
                    break;
                default:
                    throw new ArgumentException("不支持此类型的类型映射转换", type.ToString());
            }
            return mapper;
        }
    }
}
