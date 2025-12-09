using CatalogoZap.Services.Interfaces;
using CatalogoZap.Repositories.Interfaces;

namespace CatalogoZap.Services;

public class ProfilesService : IProfilesService
{
    private readonly IProductsRepository _productsRepository;

    public ProfilesService(IProductsRepository productsRepository) {
        _productsRepository = productsRepository;
    }

    public async Task<bool> HasReachedFreeTierLimit(Guid userId) {
        var productsAmount = await _productsRepository.GetProductsAmountByUserId(userId);

        return productsAmount >= 10;
    }
}
