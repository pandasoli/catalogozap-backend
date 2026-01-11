using Microsoft.AspNetCore.Mvc;
using CatalogoZap.DTOs;
using CatalogoZap.Services.Interfaces;
using CatalogoZap.Infrastructure.Exceptions;

namespace CatalogoZap.Controllers;

[ApiController]
[Route("/api/users/register")]
public class RegisterController : ControllerBase
{
    private readonly IProfilesService _profilesService;

    public RegisterController(IProfilesService profilesService)
    {
        _profilesService = profilesService;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDTO dto)
    {
        try { await _profilesService.Register(dto); }
        catch (UnauthorizedException err) { return Unauthorized(err.Message); }
        catch (Exception) { return StatusCode(500); }

        return Created();
    }
}
