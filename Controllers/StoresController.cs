using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CatalogoZap.Infrastructure.JWT;
using CatalogoZap.Services.Interfaces;
using CatalogoZap.Models;
using CatalogoZap.DTOs;

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
		catch (Exception err) { return BadRequest(err.Message); }
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> PostStore ([FromForm] StoreDTO newStore)
	{
		var UserId = TokenService.GetUserId(User);

		try{ await _storesService.CreatStore(newStore, UserId); }
		catch (Exception Error) { return BadRequest(Error.Message);}

		return Ok();
	}

	[HttpPatch]
	[Authorize]
	public async Task<IActionResult> PatchStore (ModifyStoreDTO Store)
	{
		var UserId = TokenService.GetUserId(User);

		try { await _storesService.ModStore(Store, UserId); } 
		catch (Exception Error) { return BadRequest(Error.Message); }

		return Ok();
	}

	[HttpDelete]
	[Authorize]
	public async Task<IActionResult> DeleteStore (Guid StoreId)
	{
		var UserId = TokenService.GetUserId(User);
		
		try { await _storesService.DeleteStore(UserId, StoreId); }
		catch (Exception Error) { return BadRequest(Error.Message); }

		return Ok();
	}
}
