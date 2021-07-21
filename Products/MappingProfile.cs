using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Products
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(p => p.Category, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(p => p.Provider, opt => opt.MapFrom(x => x.Provider.Name));

            CreateMap<Provider, ProviderDto>();
            CreateMap<Provider, ProviderDto>().ReverseMap();

            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryDto>().ReverseMap();


            CreateMap<ProductForManipulationDto, Product>();
            CreateMap<ProductForManipulationDto, Product>().ReverseMap();

            CreateMap<ProviderForManipulationDto, Provider>();

            CreateMap<CategoryForManipulationDto, Category>();


            CreateMap<UserForRegistrationDto, User>();

            CreateMap<UserForManipulationDto, User>();
        }
    }
}
