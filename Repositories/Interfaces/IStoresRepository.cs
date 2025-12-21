using CatalogoZap.Models;

namespace CatalogoZap.Repositories.Interfaces;

public interface IStoresRepository
{
    Task<List<StoreModel>> SelectStores(Guid UserId);
}