using GeekShop.ProductAPI.Data.ValueObjects;

namespace GeekShop.ProductAPI.Repository;

public interface IProductRepository
{
    Task<IEnumerable<ProductVO>> FindAllAsync();
    Task<ProductVO> FindByIdAsync(long id);
    Task<ProductVO> CreateAsync(ProductVO dto);
    Task<ProductVO> UpdateAsync(ProductVO dto);
    Task<bool> DeleteAsync(long id);
}