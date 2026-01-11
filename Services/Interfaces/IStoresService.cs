using CatalogoZap.DTOs;
using CatalogoZap.Models;

namespace CatalogoZap.Services.Interfaces;

public interface IStoresService
{
    Task<List<StoreModel>> GetStores(Guid UserId);
    Task CreateStore (StoreDTO newStore, Guid UserId);
    Task ModStore (ModifyStoreDTO store, Guid UserId);
    Task DeleteStore (Guid UserID, Guid StoreId);
}