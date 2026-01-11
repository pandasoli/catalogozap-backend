using Microsoft.AspNetCore.Mvc;
using CatalogoZap.Services.Interfaces;
using CatalogoZap.DTOs;
using CatalogoZap.Infrastructure.JWT;
using Microsoft.AspNetCore.Authorization;
using CatalogoZap.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

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
        catch (NotFoundException err) { return NotFound(err.Message); }
        catch (Exception) { return StatusCode(500); }
    }

    [HttpPatch]
    [Authorize]
    public async Task<IActionResult> ModifyProfile(ModifyProfileDTO update)
    {
        var UserId = TokenService.GetUserId(User);

        try { await _profilesService.ModifyProfile(update, UserId); }
        catch (NotFoundException err) { return NotFound(err.Message); }
        catch (Exception) { return StatusCode(500); }

        return Ok();
    }
}
