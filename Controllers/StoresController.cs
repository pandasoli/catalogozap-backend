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
	public async Task<IActionResult> PostStore (StoreDTO newStore)
	{
		try
		{
			return Ok(await _storesService.CreatStore(newStore));
		} catch (Exception Error) { return BadRequest(Error.Message);}
	}

	[HttpPatch]
	public async Task<IActionResult> PatchStore (ModStoreDTO Store)
	{
		try
		{
			return Ok(await _storesService.ModStore(Store));
		} catch (Exception Error)
		{
			return BadRequest(Error.Message);
		}
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteStore (Guid UserId, Guid StoreId)
	{
		try
		{
			return Ok(await _storesService.DeleteStore(UserId, StoreId));
		} catch (Exception Error)
		{
			return BadRequest(Error.Message);
		}
	}
}
