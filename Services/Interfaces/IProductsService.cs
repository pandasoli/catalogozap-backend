using CatalogoZap.DTO;

namespace CatalogoZap.Services.Interfaces;

public interface IProductsService
{
    Task CreateProduct(PostProductsDTO dto, Guid UserId);
}
