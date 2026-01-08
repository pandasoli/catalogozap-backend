using CatalogoZap.Services.Interfaces;
using CatalogoZap.Repositories.Interfaces;
using CatalogoZap.Infrastructure.JWT;
using CatalogoZap.DTOs;
using CatalogoZap.Models;

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
        var DbData = await _profilesRepository.SelectUser(dto.Email) ?? throw new Exception("User doesnt exist");

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, DbData.Password)) throw new Exception("Incorrect Password");

        return _tokenService.GenerateToken(DbData.Id);
    }

    public async Task Register(RegisterDTO dto)
    {
        if(await _profilesRepository.SelectUser(dto.Email) != null) throw new Exception("User already exist");

        string HashPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password, workFactor: 12);

        var register = new RegisterModel()
        {
            Username = dto.Username,
            Email = dto.Email,
            HashPassword = HashPassword
        };

        await _profilesRepository.InsertUser(register);
    }

    public async Task<ProfileModel> GetProfiles(Guid UserId)
    {
        return await _profilesRepository.GetProfileById(UserId) ?? throw new Exception("Profile not found");
    }

    public async Task<string> ModProfile(Guid userId, ModProfileDTO update)
    {
        var oldProfile = await _profilesRepository.GetProfileById(userId);
        var newdata = new ProfileModel
        {
          Id = userId,
          Username = update.Name ?? oldProfile.Username,
          Bio = update.Bio ?? oldProfile.Bio,
          Phone = update.Phone ?? oldProfile.Phone,
          LogoUrl = oldProfile.LogoUrl,
          CreatedAt = oldProfile.CreatedAt,
          Email = update.Email ?? oldProfile.Email,
          Premium = oldProfile.Premium,
          Password = update.Password ?? oldProfile.Password

        };
        
        return await _profilesRepository.ModProfile(userId, newdata) ?? throw new Exception("Profile not found");
    }
}
