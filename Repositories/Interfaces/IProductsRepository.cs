using CatalogoZap.DTO;
using CatalogoZap.Models;

namespace CatalogoZap.Repositories.Interfaces;

public interface IProductsRepository
{
    Task CreateProduct(ProductModel data, Guid userId);
}
