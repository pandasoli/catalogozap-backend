using Microsoft.AspNetCore.Mvc;
using CatalogoZap.DTOs;
using CatalogoZap.Services.Interfaces;

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
        catch (Exception err) { return Unauthorized(err.Message); }

        return Ok();
    }
}
