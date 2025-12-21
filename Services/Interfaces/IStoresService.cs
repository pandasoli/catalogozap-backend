using CatalogoZap.Models;

namespace CatalogoZap.Services.Interfaces;

public interface IStoresService
{
    Task<List<StoreModel>> GetStore(Guid UserId);
}