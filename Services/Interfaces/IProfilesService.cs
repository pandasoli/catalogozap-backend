using CatalogoZap.DTOs;

namespace CatalogoZap.Services.Interfaces;

public interface IProfilesService
{
    Task<bool> HasReachedFreeTierLimit(Guid userId);
    Task<string> Login(LoginDTO dto);
}
