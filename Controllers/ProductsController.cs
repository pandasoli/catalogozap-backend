using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CatalogoZap.Infrastructure.JWT;
using CatalogoZap.DTOs;
using CatalogoZap.Services.Interfaces;

namespace CatalogoZap.Controllers;

[ApiController]
[Route("/api/[controller]")]
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
        var userId = TokenService.GetUserId(User);

        // if (!_bussinesRules.MaxFreePlan("", userId))
        //     return BadRequest("Maximum number of products registered on the free plan");

		await _productsService.CreateProduct(dto, userId);

		return Ok();
	}
}
