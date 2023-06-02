using AutoMapper;
using GeekShop.ProductAPI.Data.ValueObjects;
using GeekShop.ProductAPI.Model;
using GeekShop.ProductAPI.Model.Base.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShop.ProductAPI.Repository;

public class ProductRepository : IProductRepository
{
    private readonly MySQLContext _context;
    private IMapper _mapper;

    public ProductRepository(MySQLContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductVO>> FindAllAsync()
    {
        List<Product> products = await _context.Products.ToListAsync();
        return _mapper.Map<List<ProductVO>>(products);
    }

    public async Task<ProductVO> FindByIdAsync(long id)
    {
        Product product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync() ?? new Product();
        return _mapper.Map<ProductVO>(product);
    }

    public async Task<ProductVO> CreateAsync(ProductVO dto)
    {
        Product product = _mapper.Map<Product>(dto);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return _mapper.Map<ProductVO>(product);
    }

    public async Task<ProductVO> UpdateAsync(ProductVO dto)
    {
        Product product = _mapper.Map<Product>(dto);
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return _mapper.Map<ProductVO>(product);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            Product product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync() ?? new Product();

            if (product.Id <= 0) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}