using CatalogoZap.DTO;

namespace CatalogoZap.Services.Interfaces
{
    public interface IProductsService
    {
        Task PostProducts(PostProductsDTO dto, Guid UserId, string ConnectionString);
    }
}