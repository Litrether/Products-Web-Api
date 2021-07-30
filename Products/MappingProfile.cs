using AutoMapper;
using Entities.DataTransferObjects.Incoming;
using Entities.DataTransferObjects.Outcoming;
using Entities.Models;

namespace Products
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductOutgoingDto>()
                .ForMember(p => p.Category, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(p => p.Provider, opt => opt.MapFrom(x => x.Provider.Name));
            CreateMap<ProductIncomingDto, Product>().ReverseMap();

            CreateMap<Provider, ProviderOutgoingDto>();
            CreateMap<ProviderIncomingDto, Provider>();

            CreateMap<Category, CategoryOutgoingDto>();
            CreateMap<CategoryIncomingDto, Category>();

            CreateMap<UserRegistrationDto, UserValidationDto>();
            CreateMap<UserRegistrationDto, User>();
            CreateMap<UserValidationDto, User>();
        }
    }
}
