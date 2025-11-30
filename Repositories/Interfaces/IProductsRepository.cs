using CatalogoZap.DTO;

namespace CatalogoZap.Repositories.Interfaces
{
    public interface IProductsRepository
    {
        Task PostProducts(PostProductsDTO dto, Guid UserId, string ConnectionString, string imgUrl);
    }
}