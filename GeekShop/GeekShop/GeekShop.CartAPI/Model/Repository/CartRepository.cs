using AutoMapper;
using GeekShop.CartAPI.Data.DTOs;
using GeekShop.CartAPI.Model.Base.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShop.CartAPI.Model.Repository;

public class CartRepository : ICartRepository
{
    private readonly MySQLContext _context;
    private IMapper _mapper;

    public CartRepository(MySQLContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> ApplyCoupon(string userId, string couponCode)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ClearCart(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<CartDto> FindCartByUserId(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveCoupon(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveFromCart(long cartDetailsId)
    {
        throw new NotImplementedException();
    }

    public async Task<CartDto> SaveOrUpdateCart(CartDto cartDto)
    {
        Cart cart = _mapper.Map<Cart>(cartDto);

        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == cartDto.CartDetails.FirstOrDefault().ProductId);

        if (product is null)
        {
            _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
            await _context.SaveChangesAsync();
        }

        var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == cart.CartHeader.UserId);

        if (cartHeader is null)
        {
            _context.CartHeaders.Add(cart.CartHeader);
            await _context.SaveChangesAsync();

            cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
            cart.CartDetails.FirstOrDefault().Product = null; // Null pq já foi inserido no contexto o product, acima.
            _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());

            await _context.SaveChangesAsync();
        }
        else
        {
            var cartDetail = await _context.CartDetails.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductId == cart.CartDetails.FirstOrDefault().ProductId && x.CartHeaderId == cartHeader.Id);

            if (cartDetail is null)
            {
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null; // Null pq já foi inserido no contexto o product, acima.
                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());

                await _context.SaveChangesAsync();
            }
            else
            {
                cart.CartDetails.FirstOrDefault().Product = null;
                cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;

                _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());

                await _context.SaveChangesAsync();
            }
        }

        return _mapper.Map<CartDto>(cart);
    }
}