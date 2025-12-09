using CatalogoZap.DTOs;

namespace CatalogoZap.Services.Interfaces;

public interface IProductsService
{
    Task CreateProduct(ProductDTO dto, Guid UserId);
}
