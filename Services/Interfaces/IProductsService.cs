using CatalogoZap.DTOs;
using CatalogoZap.Models;

namespace CatalogoZap.Services.Interfaces;

public interface IProductsService
{
    Task CreateProduct(ProductDTO dto, Guid UserId);
    Task<List<ProductModel>> GetProducts(Guid storeId, Guid? UserId);

    Task<string> ModProducts (ModProductsDTO Product, Guid UserID);
    Task DeleteProduct (Guid IdPro, Guid UserId, Guid StoreId);
}
