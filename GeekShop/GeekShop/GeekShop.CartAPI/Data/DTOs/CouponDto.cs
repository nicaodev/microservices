namespace GeekShop.CartAPI.Data.DTOs;

public class CouponDto
{
    public long Id { get; set; }

    public string CouponCode { get; set; }

    public decimal Discount { get; set; }
}