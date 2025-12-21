using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CatalogoZap.Infrastructure.JWT;
using CatalogoZap.DTOs;
using CatalogoZap.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

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
        catch (Exception err) { return Unauthorized(err.Message); }
    }
}
