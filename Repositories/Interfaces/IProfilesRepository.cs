using CatalogoZap.Models;

namespace CatalogoZap.Repositories.Interfaces;

public interface IProfilesRepository
{
    Task<ProfileModel?> GetProfileById(Guid userId);
    Task<ProfileModel?> PublicGetProfileById(Guid userId);
    Task<LoginModel?> GetProfileByEmail(string Email);
    Task InsertUser(RegisterModel register);
    Task ModifyProfile(Guid userID, ProfileModel update);
}
