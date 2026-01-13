using Microsoft.AspNetCore.Mvc;
using CatalogoZap.DTOs;
using CatalogoZap.Services.Interfaces;
using CatalogoZap.Infrastructure.Exceptions;

namespace CatalogoZap.Controllers;

[ApiController]
[Route("/api/users/login")]
public class LoginController : ControllerBase
{
    private readonly IProfilesService _profilesService;

    public LoginController(IProfilesService profilesService)
    {
        _profilesService = profilesService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO dto)
    {
        try { return Ok(await _profilesService.Login(dto)); }
        catch (UnauthorizedException err) { return Unauthorized(err.Message); }
        catch (Exception err) { Console.WriteLine(err); return StatusCode(500); }
    }
}
