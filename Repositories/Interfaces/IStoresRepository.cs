using CatalogoZap.Models;

namespace CatalogoZap.Repositories.Interfaces;

public interface IStoresRepository
{
    Task<StoreModel> SelectStores(Guid UserId);
}