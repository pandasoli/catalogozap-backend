using CatalogoZap.Services.Interfaces;
using CatalogoZap.DTOs;
using CatalogoZap.Infrastructure.CloudinaryService;
using CatalogoZap.Repositories.Interfaces;
using CatalogoZap.Models;

namespace CatalogoZap.Services;

public class ProductsService : IProductsService
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IProductsRepository _productsRepository;

    public ProductsService(ICloudinaryService cloudinaryService, IProductsRepository productsRepository) {
        _cloudinaryService = cloudinaryService;
        _productsRepository = productsRepository;
    }

    public async Task CreateProduct(ProductDTO dto, Guid UserId) {
        string photoUrl = await _cloudinaryService.UploadImageAsync(dto.Photo);

        var data = new ProductModel {
            PhotoUrl = photoUrl,
            Name = dto.Name,
            PriceCents = dto.PriceCents,
            StoreId = dto.StoreId,
            Avaliable = dto.Avaliable
        };

        await _productsRepository.CreateProduct(data, UserId);
    }
}
