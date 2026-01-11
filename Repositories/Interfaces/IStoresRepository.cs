using CatalogoZap.DTOs;
using CatalogoZap.Models;

namespace CatalogoZap.Repositories.Interfaces;

public interface IStoresRepository
{
    Task<List<StoreModel>> SelectStores(Guid UserId);
    Task<string> CreateStore(StoreModel store);
    Task<string>ModStore(StoreModel store);
    Task<string> DeleteStore (Guid UserId, Guid StoreId);
    Task<StoreModel?> SelectStoreById(Guid Id);
    
}