using CatalogoZap.Models;
using CatalogoZap.Services.Interfaces;
using CatalogoZap.Repositories.Interfaces;
using CatalogoZap.DTOs;

namespace CatalogoZap.Services;

public class StoresService : IStoresService
{  
    private readonly IStoresRepository _storesRepository;

    public StoresService(IStoresRepository storesRepository)
    {
        _storesRepository = storesRepository;
    }

    public async Task<List<StoreModel>> GetStores(Guid UserId)
    {
        return await _storesRepository.SelectStores(UserId);
    }

    public async Task<string> CreatStore (StoreDTO store)
    {
        var newStore = new StoreModel
        {
            Id = store.Id,
            UserId = store.UserId,
            Name = store.Name,
            Bio = store.Bio,
            LogoUrl = store.LogoUrl
        };

        return await _storesRepository.CreatStore(newStore);
    }

    public async Task<string> ModStore (ModStoreDTO store)
    {
        var oldStore = await _storesRepository.SelectStores(store.Id);

        var newStore = new StoreModel
        {
            Id = oldStore[0].Id,
            UserId = oldStore[0].UserId,
            Name = store.Name ?? oldStore[0].Name,
            Bio = store.Bio ?? oldStore[0].Bio,
            LogoUrl = store.LogoUrl ?? oldStore[0].LogoUrl
        };

        return await _storesRepository.ModStore(newStore);
    }

    public async Task<string> DeleteStore (Guid UserId, Guid StoreId)
    {
        return await _storesRepository.DeleteStore(UserId, StoreId);
    }
    
}