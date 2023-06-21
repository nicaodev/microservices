using GeekShop.CartAPI.Data.DTOs;
using System.Net;
using System.Text.Json;

namespace GeekShop.CartAPI.Model.Repository;

public class CouponRepository : ICouponRepository
{
    private readonly HttpClient _httpClient;

    public CouponRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CouponDto> GetCoupon(string couponCode, string tokent = "")
    {
        var response = await _httpClient.GetAsync($"/api/coupon/{couponCode}");

        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode != HttpStatusCode.OK) return new CouponDto();

        return JsonSerializer.Deserialize<CouponDto>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }
}