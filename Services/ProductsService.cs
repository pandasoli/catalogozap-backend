using CatalogoZap.Services.Interfaces;
using CatalogoZap.DTOs;
using CatalogoZap.Infrastructure.CloudinaryService;
using CatalogoZap.Repositories.Interfaces;
using CatalogoZap.Models;

namespace CatalogoZap.Services;

public class ProductsService : IProductsService
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IProfilesService _profilesService;
    private readonly IProductsRepository _productsRepository;

    public ProductsService(
        ICloudinaryService cloudinaryService,
        IProfilesService profilesService,
        IProductsRepository productsRepository
    ) {
        _cloudinaryService = cloudinaryService;
        _profilesService = profilesService;
        _productsRepository = productsRepository;
    }

    public async Task<int> GetProductsAmountByUserId(Guid userId) {
        return await _productsRepository.GetProductsAmountByUserId(userId);
    }

    public async Task CreateProduct(ProductDTO dto, Guid userId) {
        if (await _profilesService.HasReachedFreeTierLimit(userId))
            throw new Exception("Reached free plan products limit.");

        string photoUrl = await _cloudinaryService.UploadImageAsync(dto.Photo);

        var data = new ProductModel {
            PhotoUrl = photoUrl,
            Name = dto.Name,
            PriceCents = dto.PriceCents,
            StoreId = dto.StoreId,
            Avaliable = dto.Avaliable
        };

        await _productsRepository.CreateProduct(data, userId);
    }
}
