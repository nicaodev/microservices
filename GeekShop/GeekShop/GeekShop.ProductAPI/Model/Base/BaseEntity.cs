﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShop.ProductAPI.Model.Context;

public class BaseEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
}