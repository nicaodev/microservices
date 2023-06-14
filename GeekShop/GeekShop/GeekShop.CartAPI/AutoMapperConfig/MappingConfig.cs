using AutoMapper;

namespace GeekShop.CartAPI.AutoMapperConfig;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            //config.CreateMap<ProductVO, Product>().ReverseMap();
            //config.CreateMap<Product, ProductVO>();
        });

        return mappingConfig;
    }
}