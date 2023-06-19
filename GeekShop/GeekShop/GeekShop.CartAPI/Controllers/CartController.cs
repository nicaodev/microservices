using GeekShop.CartAPI.Data.DTOs;
using GeekShop.CartAPI.Messages;
using GeekShop.CartAPI.Model.Repository;
using GeekShop.CartAPI.RabbitMQSender;
using Microsoft.AspNetCore.Mvc;

namespace GeekShop.CartAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartRepository _repository;
    private readonly IRabbitMQSender _rabbitMQSender;

    public CartController(ICartRepository repository, IRabbitMQSender rabbitMQSender)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _rabbitMQSender = rabbitMQSender ?? throw new ArgumentNullException(nameof(rabbitMQSender));
    }

    [HttpGet("find-cart/{id}")]
    public async Task<ActionResult<CartDto>> FindById(string id)
    {
        var cart = await _repository.FindCartByUserId(id);

        if (cart is null) return NotFound();
        return Ok(cart);
    }

    [HttpPost("add-cart")]
    public async Task<ActionResult<CartDto>> AddCart(CartDto cartDto)
    {
        var cart = await _repository.SaveOrUpdateCart(cartDto);

        if (cart is null) return NotFound();
        return Ok(cart);
    }

    [HttpPut("update-cart")]
    public async Task<ActionResult<CartDto>> UpdateCart(CartDto cartDto)
    {
        var cart = await _repository.SaveOrUpdateCart(cartDto);

        if (cart is null) return NotFound();
        return Ok(cart);
    }

    [HttpDelete("remove-cart/{id}")]
    public async Task<ActionResult<CartDto>> RemoveCart(int id)
    {
        var status = await _repository.RemoveFromCart(id);

        if (!status) return BadRequest();
        return Ok(status);
    }

    [HttpPost("apply-coupon")]
    public async Task<ActionResult<CartDto>> ApplyCoupon(CartDto cartDto)
    {
        var status = await _repository.ApplyCoupon(cartDto.CartHeader.UserId, cartDto.CartHeader.CouponCode);

        if (!status) return NotFound();
        return Ok(status);
    }

    [HttpDelete("remove-coupon/{userId}")]
    public async Task<ActionResult<CartDto>> RemoveCoupon(string userId)
    {
        var status = await _repository.RemoveCoupon(userId);

        if (!status) return NotFound();
        return Ok(status);
    }

    [HttpPost("checkout")]
    public async Task<ActionResult<CheckoutHeaderDto>> Checkout(CheckoutHeaderDto dto)
    {
        if (dto?.UserId is null) return BadRequest();

        var cart = await _repository.FindCartByUserId(dto.UserId);

        if (cart is null) return NotFound();

        dto.CartDetails = cart.CartDetails;

        //RABBITMQ

        _rabbitMQSender.SendMessage(dto, "checkoutqueue");

        return Ok(dto);
    }
}