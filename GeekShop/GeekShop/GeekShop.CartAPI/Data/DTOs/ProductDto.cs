﻿using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShop.CartAPI.Data.DTOs;


public class ProductDto
{
    public long Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }

    public string CategoryName { get; set; }

    public string Image_Url { get; set; }
}