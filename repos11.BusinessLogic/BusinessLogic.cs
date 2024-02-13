using AutoMapper;

namespace repos11.BusinessLogic
{
    public class BusinessLogic<ModelDto, Entity>
    {
        public static IMapper Mapper;
        public BusinessLogic()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entity, ModelDto>().ReverseMap();
            }).CreateMapper();
        }
    }
}
