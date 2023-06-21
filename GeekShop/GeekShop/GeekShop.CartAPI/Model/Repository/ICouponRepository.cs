using GeekShop.CartAPI.Data.DTOs;

namespace GeekShop.CartAPI.Model.Repository;

public interface ICouponRepository
{
    Task<CouponDto> GetCoupon(string couponCode, string token="");
}