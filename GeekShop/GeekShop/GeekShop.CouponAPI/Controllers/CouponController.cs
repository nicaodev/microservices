using GeekShop.CouponAPI.Data.DTOs;
using GeekShop.CouponAPI.Model.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShop.CouponAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponController : ControllerBase
{
    private readonly ICouponRepository _couponRepository;

    public CouponController(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
    }

    [HttpGet("{couponCode}")]
    public async Task<ActionResult<CouponDto>> GetCouponByCoupoCode(string couponCode)
    {
        var coupon = await _couponRepository.GetCouponByCoupoCode(couponCode);
        if (coupon is null) return NotFound();

        return Ok(coupon);
    }
}