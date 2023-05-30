﻿using GeekShop.ProductAPI.Model.Context;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShop.ProductAPI.Model;

[Table("product")]
public class Product : BaseEntity
{
    [Column("name")]
    [Required]
    [StringLength(180)]
    public string Name { get; set; }

    [Column("price")]
    [Required]
    [Range(1, 1000)]
    public decimal Price { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string Description { get; set; }

    [Column("category_name")]
    [StringLength(50)]
    public string CategoryName { get; set; }

    [Column("image_url")]
    [StringLength(300)]
    public string Image_Url { get; set; }
}