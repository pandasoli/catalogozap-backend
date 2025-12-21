using CatalogoZap.Models;

namespace CatalogoZap.Services.Interfaces;

public interface IStoresService
{
    Task<StoreModel> GetStore(Guid UserId);
}