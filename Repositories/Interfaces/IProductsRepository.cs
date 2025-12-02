using CatalogoZap.Models;

namespace CatalogoZap.Repositories.Interfaces;

public interface IProductsRepository
{
    Task<int> GetProductsAmountByUserId(Guid userId);
    Task CreateProduct(ProductModel data, Guid userId);
}
