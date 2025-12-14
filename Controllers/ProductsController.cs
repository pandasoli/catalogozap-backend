using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CatalogoZap.Infrastructure.JWT;
using CatalogoZap.DTOs;
using CatalogoZap.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CatalogoZap.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductsController : ControllerBase
{
	private readonly IProductsService _productsService;

	public ProductsController(IProductsService productsService)
	{
		_productsService = productsService;
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> PostProduct([FromForm] ProductDTO dto)
	{
        Guid userId = TokenService.GetUserId(User);

        try { await _productsService.CreateProduct(dto, userId); }
        catch (Exception err) { Console.WriteLine(err); return BadRequest(err.Message); }

		return Ok();
	}

	[HttpGet("{storeId}")]
	public async Task<IActionResult> GetProduct(Guid storeId)
	{
		//It will be null if it is not admin acess
		var UserId = TokenService.TryGetUserId(User);

		try { return Ok(await _productsService.GetProducts(storeId, UserId)); }
		catch (Exception err) { Console.WriteLine(err); return BadRequest(err.Message); }
	}
}
