using CatalogoZap.Services.Interfaces;
using CatalogoZap.DTO;
using CatalogoZap.Infrastructure.CloudinaryService;
using CatalogoZap.Repositories;
using CatalogoZap.Repositories.Interfaces;

namespace CatalogoZap.Services
{
    public class ProductsService : IProductsService
    {   
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IProductsRepository _productsRepository;
        public ProductsService(ICloudinaryService cloudinaryService, IProductsRepository productsRepository)
        {
            _cloudinaryService = cloudinaryService;
            _productsRepository = productsRepository;
        }
        public async Task PostProducts(PostProductsDTO dto, Guid UserId, string ConnectionString)
        {
            string imgUrl = await _cloudinaryService.UploadImageAsync(dto.Photo);

            await _productsRepository.PostProducts(dto, UserId, ConnectionString, imgUrl);
        }
    }
}