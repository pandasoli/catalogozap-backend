using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CatalogoZap.Infrastructure.JWT;
using CatalogoZap.DTOs;
using CatalogoZap.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CatalogoZap.Controllers;

[ApiController]
[Route("/api/stores")]
public class StoresController : ControllerBase
{
	private readonly IStoresService _storesService;

	public StoresController(IStoresService storesService)
	{
		_storesService = storesService;
	}

	[HttpGet]
    [Authorize]
	public async Task<IActionResult> GetStores()
	{
		var UserId = TokenService.GetUserId(User);

		try { return Ok(await _storesService.GetStore(UserId)); }
		catch (Exception err) { return BadRequest(err.Message); }
	}
}
