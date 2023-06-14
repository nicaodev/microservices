﻿using GeekShop.CartAPI.Model.Context;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShop.CartAPI.Model;

[Table("cart_header")]
public class CartHeader : BaseEntity
{
    [Column("user_id")]
    public string UserId { get; set; }

    [Column("coupon_code")]
    public string CouponCode { get; set; }
}