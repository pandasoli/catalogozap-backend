using CatalogoZap.Models;
using CatalogoZap.Services.Interfaces;
using CatalogoZap.Repositories.Interfaces;

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
}