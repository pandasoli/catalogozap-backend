using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CatalogoZap.Infrastructure.JWT;
using CatalogoZap.Services.Interfaces;
using CatalogoZap.Models;
using CatalogoZap.DTOs;
using CatalogoZap.Infrastructure.Exceptions;

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

		try { return Ok(await _storesService.GetStores(UserId)); }
		catch (Exception) { return StatusCode(500); }
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> PostStore ([FromForm] StoreDTO newStore)
	{
		var UserId = TokenService.GetUserId(User);

		try{ await _storesService.CreateStore(newStore, UserId); }
		catch (Exception) { return StatusCode(500); }

		return Ok();
	}

	[HttpPatch]
	[Authorize]
	public async Task<IActionResult> PatchStore (ModifyStoreDTO Store)
	{
		var UserId = TokenService.GetUserId(User);

		try { await _storesService.ModifyStore(Store, UserId); } 
		catch (NotFoundException err) { return NotFound(err.Message); }
		catch (Exception) { return StatusCode(500); }

		return Ok();
	}

	[HttpDelete]
	[Authorize]
	public async Task<IActionResult> DeleteStore (Guid StoreId)
	{
		var UserId = TokenService.GetUserId(User);
		
		try { await _storesService.DeleteStore(UserId, StoreId); }
		catch (NotFoundException Error) { return NotFound(Error.Message); }
		catch (Exception) { return StatusCode(500); }

		return Ok();
	}
}
