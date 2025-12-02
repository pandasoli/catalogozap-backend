namespace CatalogoZap.Services.Interfaces;

public interface IProfilesService
{
    Task<bool> HasReachedFreeTierLimit(Guid userId);
}
