using Microsoft.AspNetCore.Mvc;
using CatalogoZap.Services.Interfaces;
using CatalogoZap.DTOs;
using CatalogoZap.Infrastructure.JWT;
using Microsoft.AspNetCore.Authorization;

namespace CatalogoZap.Controllers;

[ApiController]
[Route("/api/profiles")]
public class ProfilesController : ControllerBase
{
    private readonly IProfilesService _profilesService;

    public ProfilesController(IProfilesService profilesService)
    {
        _profilesService = profilesService;
    }

    [HttpGet("userId")]
    public async Task<IActionResult> GetProfiles(Guid UserId)
    {
        try { return Ok(await _profilesService.GetProfiles(UserId)); }
        catch (Exception err) { return NotFound(err.Message); }
    }

    [HttpPatch]
    [Authorize]
    public async Task<IActionResult> ModProfile(ModifyProfileDTO update)
    {
        var UserId = TokenService.GetUserId(User);

        try { await _profilesService.ModProfile(update, UserId); }
        catch (Exception err) { return NotFound(err.Message); }

        return Ok();
    }
}
