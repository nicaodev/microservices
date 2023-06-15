using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShop.CouponAPI.Data.DTOs;


public class CouponDto
{
    public long Id { get; set; }

    public string CouponCode { get; set; }

    public decimal Discount { get; set; }


}