using CatalogoZap.Models;

namespace CatalogoZap.Repositories.Interfaces;

public interface IProfilesRepository
{
    Task<ProfileModel> GetProfileById(Guid userId);
}
