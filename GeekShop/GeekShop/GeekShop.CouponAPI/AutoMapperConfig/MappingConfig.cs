using AutoMapper;
using GeekShop.CouponAPI.Data.DTOs;
using GeekShop.CouponAPI.Model;

namespace GeekShop.CouponAPI.AutoMapperConfig;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<CouponDto, Coupon>().ReverseMap();
        });

        return mappingConfig;
    }
}