using CatalogoZap.Models;

namespace CatalogoZap.Repositories.Interfaces;

public interface IProductsRepository
{
    Task<int> GetProductsAmountByUserId(Guid userId);
    Task CreateProduct(ProductModel data);
    Task<List<ProductModel>> GetProducts(Guid storeId);
    Task<List<ProductModel>> GetProductsAdmin(Guid storeId, Guid? UserId);
}
