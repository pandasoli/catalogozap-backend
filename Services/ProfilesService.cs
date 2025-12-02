using CatalogoZap.Services.Interfaces;

namespace CatalogoZap.Services;

public class ProfilesService : IProfilesService
{
    private readonly IProductsService _productsService;

    public ProfilesService(IProductsService productsService) {
        _productsService = productsService;
    }

    public async Task<bool> HasReachedFreeTierLimit(Guid userId) {
        var productsAmount = await _productsService.GetProductsAmountByUserId(userId);

        return productsAmount >= 10;
    }
}
