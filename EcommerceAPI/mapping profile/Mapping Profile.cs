using AutoMapper;
using Ecommerce.Core.DTO;
using Ecommerce.Core.Models;

namespace Ecommerce.API.mapping_profile
{
    public class Mapping_Profile : Profile
    {
        public Mapping_Profile()
        {
            CreateMap<Product, ProductDTO>().ForMember(To => To.category_Name, From => From.MapFrom(x => x.categories != null ? x.categories.Name : null));
            CreateMap<Create_UpdateProductDTO, Product>()
               //.ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
               //.ForMember(dest => dest.categories, opt => opt.Ignore());
        }
    }
}
