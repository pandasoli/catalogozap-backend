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

    public async Task CreateProduct(ProductDTO dto, Guid userId) {
        if (await _profilesService.HasReachedFreeTierLimit(userId))
            throw new Exception("Reached free plan products limit.");

        string photoUrl = await _cloudinaryService.UploadImageAsync(dto.Photo);

        var data = new ProductModel {
            Id = userId,
            UserId = userId,
            PhotoUrl = photoUrl,
            Name = dto.Name,
            PriceCents = dto.PriceCents,
            StoreId = dto.StoreId,
            Avaliable = dto.Avaliable,
            Created_at = ""
        };

        await _productsRepository.CreateProduct(data);
    }

    public async Task<List<ProductModel>> GetProducts(Guid storeId, Guid? UserId)
    {
        if(UserId == null)
        {
            return await _productsRepository.GetProducts(storeId);
        }
        else
        {
            return await _productsRepository.GetProductsAdmin(storeId, UserId);
        }
    }

    public async Task<string> ModProducts (Guid StoreId, Guid UserId, ModProductsDTO product)
    {
        var oldProduct = await _productsRepository.GetProducts(StoreId);

        var newproduct = new ProductModel
        {
            Id = oldProduct[0].Id,
            UserId = UserId,
            StoreId = StoreId,
            Name = product.Name ?? oldProduct[0].Name,
            PriceCents = product.PriceCents ?? oldProduct[0].PriceCents,
            PhotoUrl = oldProduct[0].PhotoUrl,
            Avaliable = product.Avaliable ?? oldProduct[0].Avaliable,
            Created_at = oldProduct[0].Created_at
        };

        try
        {
            return await _productsRepository.ModProducts(newproduct);
        } catch
        {
            throw new Exception("Sorry, it seems something went wrong.");
        }
    }

    public async Task<string> DeleteProduct (Guid IdPro, Guid UserId, Guid StoreId)
    {
        try
        {
            return await _productsRepository.DeleteProduct(IdPro, UserId, StoreId);
        } catch
        {
            throw new Exception("Sorry, it seems something went wrong.");
        }
    }
}
