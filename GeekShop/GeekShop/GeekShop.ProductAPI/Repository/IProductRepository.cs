using GeekShop.ProductAPI.Data.ValueObjects;

namespace GeekShop.ProductAPI.Repository;

public interface IProductRepository
{
    Task<IEnumerable<ProductVO>> FindAll();
    Task<ProductVO> FindById(long id);
    Task<ProductVO> Create(ProductVO dto);
    Task<ProductVO> Update(ProductVO dto);
    Task<bool> Delete(long id);
}