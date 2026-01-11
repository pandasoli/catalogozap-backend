using CatalogoZap.Models;
using CatalogoZap.Services.Interfaces;
using CatalogoZap.Repositories.Interfaces;
using CatalogoZap.DTOs;
using CatalogoZap.Infrastructure.CloudinaryService;

namespace CatalogoZap.Services;

public class StoresService : IStoresService
{
    private readonly IStoresRepository _storesRepository;
    private readonly ICloudinaryService _cloudinaryService;

    public StoresService(IStoresRepository storesRepository, ICloudinaryService cloudinaryService)
    {
        _storesRepository = storesRepository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<List<StoreModel>> GetStores(Guid UserId)
    {
        return await _storesRepository.SelectStores(UserId);
    }

    public async Task CreateStore(StoreDTO store, Guid UserId)
    {
        string logoUrl = await _cloudinaryService.UploadImageAsync(store.Photo);

        var newStore = new StoreModel
        {
            UserId = UserId,
            Name = store.Name,
            Bio = store.Bio,
            LogoUrl = logoUrl
        };

        await _storesRepository.CreateStore(newStore);
    }

    public async Task ModStore(ModifyStoreDTO store, Guid UserId)
    {
        var oldStore = await _storesRepository.SelectStores(store.StoreId);

        var newStore = new StoreModel
        {
            Id = store.StoreId,
            UserId = UserId,
            Name = store.Name ?? oldStore[0].Name,
            Bio = store.Bio ?? oldStore[0].Bio,
            LogoUrl = store.LogoUrl ?? oldStore[0].LogoUrl
        };

        await _storesRepository.ModStore(newStore);
    }

    public async Task DeleteStore(Guid UserId, Guid StoreId)
    {
        StoreModel store = await _storesRepository.SelectStoreById(StoreId) ?? throw new Exception("Store not found");

        await _storesRepository.DeleteStore(UserId, StoreId);

        if (!string.IsNullOrWhiteSpace(store.LogoUrl))
        {
            int startIndex = store.LogoUrl.IndexOf("products/");
            if (startIndex != -1)
            {
                string fullPathWithExtension = store.LogoUrl.Substring(startIndex);
                int lastDotIndex = fullPathWithExtension.LastIndexOf('.');
                string PhotoUrlPath = (lastDotIndex != -1) ? fullPathWithExtension.Substring(0, lastDotIndex) : fullPathWithExtension;
                await _cloudinaryService.DeleteImageAsync(PhotoUrlPath);
            }
        }
    }

}