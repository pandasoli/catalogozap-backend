using CatalogoZap.Services.Interfaces;
using CatalogoZap.Repositories.Interfaces;
using CatalogoZap.Infrastructure.JWT;
using CatalogoZap.DTOs;

namespace CatalogoZap.Services;

public class ProfilesService : IProfilesService
{
    private readonly IProductsRepository _productsRepository;
    private readonly IProfilesRepository _profilesRepository;
    private readonly ITokenService _tokenService;

    public ProfilesService(IProductsRepository productsRepository, IProfilesRepository profilesRepository, ITokenService tokenService)
    {
        _productsRepository = productsRepository;
        _profilesRepository = profilesRepository;
        _tokenService = tokenService;
    }

    public async Task<bool> HasReachedFreeTierLimit(Guid userId)
    {
        var productsAmount = await _productsRepository.GetProductsAmountByUserId(userId);

        return productsAmount >= 10;
    }

    public async Task<string> Login(LoginDTO dto)
    {
        var DbData = await _profilesRepository.SelectUser(dto.Email) ?? throw new Exception("User doesnt exits");

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, DbData.Password)) throw new Exception("Incorrect Password");

        return _tokenService.GenerateToken(DbData.Id);
    }
}
