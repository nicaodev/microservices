using GeekShop.CouponAPI.Model.Context;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShop.CouponAPI.Model;

[Table("coupon")]
public class Coupon : BaseEntity
{
    [Column("coupon_Code")]
    [Required]
    [StringLength(30)]
    public string CouponCode { get; set; }

    [Column("discount")]
    [Required]
    public decimal Discount { get; set; }
}