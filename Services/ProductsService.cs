using CatalogoZap.Services.Interfaces;
using CatalogoZap.DTOs;
using CatalogoZap.Infrastructure.CloudinaryService;
using CatalogoZap.Repositories.Interfaces;
using CatalogoZap.Models;
using CatalogoZap.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

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
    )
    {
        _cloudinaryService = cloudinaryService;
        _profilesService = profilesService;
        _productsRepository = productsRepository;
    }

    public async Task CreateProduct(ProductDTO dto, Guid userId)
    {
        if (await _profilesService.HasReachedFreeTierLimit(userId))
            throw new Exception("Reached free plan products limit.");

        string photoUrl = await _cloudinaryService.UploadImageAsync(dto.Photo);

        var data = new ProductModel
        {
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
        if (UserId == null)
        {
            return await _productsRepository.GetProducts(storeId);
        }
        else
        {
            return await _productsRepository.GetProductsAdmin(storeId, UserId);
        }
    }

    public async Task ModifyProducts(ModProductsDTO product, Guid UserId)
    {
        var oldProduct = await _productsRepository.GetProductById(product.Id, product.StoreId, UserId) ?? throw new NotFoundException("Product doesnt exists");

        string? photoUrl = product.Photo != null ? await _cloudinaryService.UploadImageAsync(product.Photo) : null;

        var newproduct = new ProductModel
        {
            Id = product.Id,
            UserId = UserId,
            StoreId = product.StoreId,
            Name = product.Name ?? oldProduct.Name,
            PriceCents = product.PriceCents ?? oldProduct.PriceCents,
            PhotoUrl = photoUrl ?? oldProduct.PhotoUrl,
            Avaliable = product.Avaliable ?? oldProduct.Avaliable,
            Created_at = oldProduct.Created_at
        };

        await _productsRepository.ModifyProducts(newproduct);

        if (photoUrl != null)
        {
            int startIndex = oldProduct.PhotoUrl.IndexOf("products/");
            if (startIndex != -1)
            {
                string fullPathWithExtension = oldProduct.PhotoUrl.Substring(startIndex);
                int lastDotIndex = fullPathWithExtension.LastIndexOf('.');
                string PhotoUrlPath = (lastDotIndex != -1) ? fullPathWithExtension.Substring(0, lastDotIndex) : fullPathWithExtension;
                await _cloudinaryService.DeleteImageAsync(PhotoUrlPath);
            }
        }
    }

    public async Task DeleteProduct(Guid Id, Guid UserId, Guid StoreId)
    {
        ProductModel product = await _productsRepository.GetProductById(Id, StoreId, UserId) ?? throw new NotFoundException("Product not found");

        System.Console.WriteLine(product.Name);

        await _productsRepository.DeleteProduct(Id, StoreId, UserId);

        if (!string.IsNullOrWhiteSpace(product.PhotoUrl))
        {
            int startIndex = product.PhotoUrl.IndexOf("products/");
            if (startIndex != -1)
            {
                string fullPathWithExtension = product.PhotoUrl.Substring(startIndex);
                int lastDotIndex = fullPathWithExtension.LastIndexOf('.');
                string PhotoUrlPath = (lastDotIndex != -1) ? fullPathWithExtension.Substring(0, lastDotIndex) : fullPathWithExtension;
                await _cloudinaryService.DeleteImageAsync(PhotoUrlPath);
            }
        }
    }
}
