using CatalogoZap.DTOs;
using CatalogoZap.Models;

namespace CatalogoZap.Services.Interfaces;

public interface IProductsService
{
    Task CreateProduct(ProductDTO dto, Guid UserId);
    Task<List<ProductModel>> GetProducts(Guid storeId, Guid? UserId);

    Task<string> ModProducts (Guid UserId, Guid StoreId, ModProductsDTO Product);
    Task<string> DeleteProduct (Guid IdPro, Guid UserId, Guid StoreId);
}
