using CatalogoZap.Services.Interfaces;
using CatalogoZap.Repositories.Interfaces;
using CatalogoZap.Infrastructure.JWT;
using CatalogoZap.DTOs;
using CatalogoZap.Models;
using CatalogoZap.Infrastructure.Exceptions;
using CatalogoZap.Infrastructure.CloudinaryService;

namespace CatalogoZap.Services;

public class ProfilesService : IProfilesService
{
    private readonly IProductsRepository _productsRepository;
    private readonly IProfilesRepository _profilesRepository;
    private readonly ITokenService _tokenService;
    private readonly ICloudinaryService _cloudinaryService;

    public ProfilesService(IProductsRepository productsRepository, IProfilesRepository profilesRepository, ITokenService tokenService, ICloudinaryService cloudinaryService)
    {
        _productsRepository = productsRepository;
        _profilesRepository = profilesRepository;
        _tokenService = tokenService;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<bool> HasReachedFreeTierLimit(Guid userId)
    {
        var productsAmount = await _productsRepository.GetProductsAmountByUserId(userId);

        return productsAmount >= 10;
    }

    public async Task<string> Login(LoginDTO dto)
    {
        var DbData = await _profilesRepository.GetProfileByEmail(dto.Email) ?? throw new UnauthorizedException("User doesnt exist");

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, DbData.Password)) throw new UnauthorizedException("Incorrect Password");

        return _tokenService.GenerateToken(DbData.Id);
    }

    public async Task Register(RegisterDTO dto)
    {
        if (await _profilesRepository.GetProfileByEmail(dto.Email) != null) throw new UnauthorizedException("User already exist");

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
        return await _profilesRepository.PublicGetProfileById(UserId) ?? throw new NotFoundException("Profile not found");
    }

    public async Task ModifyProfile(ModifyProfileDTO update, Guid UserId)
    {
        var oldProfile = await _profilesRepository.GetProfileById(UserId) ?? throw new NotFoundException("Profile doesnt exists");

        string? photoUrl = update.Photo != null ? await _cloudinaryService.UploadImageAsync(update.Photo) : null;

        string? password = update.Password != null ? BCrypt.Net.BCrypt.HashPassword(update.Password, workFactor: 12) : null;

        var newdata = new ProfileModel
        {
            Id = UserId,
            Username = update.Name ?? oldProfile.Username,
            Bio = update.Bio ?? oldProfile.Bio,
            Phone = update.Phone ?? oldProfile.Phone,
            LogoUrl = photoUrl ?? oldProfile.LogoUrl,
            CreatedAt = oldProfile.CreatedAt,
            Email = update.Email ?? oldProfile.Email,
            Premium = oldProfile.Premium,
            Password = password ?? oldProfile.Password
        };

        await _profilesRepository.ModifyProfile(UserId, newdata);

        if (photoUrl != null)
        {
            int startIndex = oldProfile.LogoUrl.IndexOf("products/");
            if (startIndex != -1)
            {
                string fullPathWithExtension = oldProfile.LogoUrl.Substring(startIndex);
                int lastDotIndex = fullPathWithExtension.LastIndexOf('.');
                string PhotoUrlPath = (lastDotIndex != -1) ? fullPathWithExtension.Substring(0, lastDotIndex) : fullPathWithExtension;
                await _cloudinaryService.DeleteImageAsync(PhotoUrlPath);
            }
        }
    }
}
