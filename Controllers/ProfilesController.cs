using Microsoft.AspNetCore.Mvc;
using CatalogoZap.Services.Interfaces;
using CatalogoZap.DTOs;

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
    public async Task<IActionResult> ModProfile(Guid UserId, ModProfileDTO update)
    {
        try { 

            return Ok(await _profilesService.ModProfile(UserId, update));
            } 
            catch (Exception err) { return NotFound(err.Message);}
    }
}
