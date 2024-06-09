using AutoMapper;
using item.Domain;
using item.Web.ViewModels.Product;

namespace item.Web.Mapping
{
    public class ProductProfile : Profile
    {

        public ProductProfile()
        {
            CreateMap<CreateProductViewModel, Product>();
            CreateMap<UpdateProductViewModel, Product>();
        }
    }
}
