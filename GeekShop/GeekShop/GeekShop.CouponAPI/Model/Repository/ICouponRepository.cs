using GeekShop.CouponAPI.Data.DTOs;

namespace GeekShop.CouponAPI.Model.Repository;

public interface ICouponRepository
{
    Task<CouponDto> GetCouponByCoupoCode(string couponCode);
}
