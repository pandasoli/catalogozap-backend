using CatalogoZap.DTOs;

namespace CatalogoZap.Services.Interfaces;

public interface IProductsService
{
    Task<int> GetProductsAmountByUserId(Guid userId);
    Task CreateProduct(ProductDTO dto, Guid UserId);
}
