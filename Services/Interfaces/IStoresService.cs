using CatalogoZap.DTOs;
using CatalogoZap.Models;

namespace CatalogoZap.Services.Interfaces;

public interface IStoresService
{
    Task<List<StoreModel>> GetStores(Guid UserId);
    Task<string> CreatStore (StoreDTO newStore);
    Task<string> ModStore (ModStoreDTO store);
    Task<string> DeleteStore (Guid UserID, Guid StoreId);
}