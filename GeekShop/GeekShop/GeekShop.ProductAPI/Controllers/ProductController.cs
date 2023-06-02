using GeekShop.ProductAPI.Data.ValueObjects;
using GeekShop.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShop.ProductAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
    {
        var products = await _repository.FindAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductVO>> FindById(long id)
    {
        var product = await _repository.FindByIdAsync(id);
        if (product.Id <= 0) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductVO>> Create(ProductVO dto)
    {
        if (dto is null) return BadRequest();
        var product = await _repository.CreateAsync(dto);
        return Ok(product);
    }

    [HttpPut]
    public async Task<ActionResult<ProductVO>> Update(ProductVO dto)
    {
        if (dto is null) return BadRequest();
        var product = await _repository.UpdateAsync(dto);
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        var status = await _repository.DeleteAsync(id);
        if (!status) return BadRequest();
        return Ok(status);
    }
}