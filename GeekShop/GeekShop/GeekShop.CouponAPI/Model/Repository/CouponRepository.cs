using AutoMapper;
using GeekShop.CouponAPI.Data.DTOs;
using GeekShop.CouponAPI.Model.Base.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShop.CouponAPI.Model.Repository;

public class CouponRepository : ICouponRepository
{
    private readonly MySQLContext _context;
    private IMapper _mapper;

    public CouponRepository(MySQLContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CouponDto> GetCouponByCoupoCode(string couponCode)
    {
        var coupon = await _context.Coupons.FirstOrDefaultAsync(x => x.CouponCode == couponCode);
        return _mapper.Map<CouponDto>(coupon);
    }
}