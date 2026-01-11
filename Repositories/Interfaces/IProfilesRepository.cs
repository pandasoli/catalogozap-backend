using CatalogoZap.Models;

namespace CatalogoZap.Repositories.Interfaces;

public interface IProfilesRepository
{
    Task<ProfileModel?> GetProfileById(Guid userId);
    Task<LoginModel?> SelectUser(string Email);
    Task InsertUser(RegisterModel register);
    Task ModifyProfile(Guid userID, ProfileModel update);
}
