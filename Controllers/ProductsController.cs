using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CatalogoZap.Infrastructure.JWT;
using CatalogoZap.DTO;
using CatalogoZap.DTO.Database;
using CatalogoZap.Services.Interfaces;
using CatalogoZap.Rules;

namespace test.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductsController : ControllerBase
{
	private readonly ITokenService _tokenService;
	private readonly IProductsService _productsService;
	private readonly IBussinesRules _businessRules;
	private readonly string _connectionString;

	public ProductsController(ITokenService tokenService, IProductsService productsService, IBussinesRules bussinesRules, DatabaseSettingsDTO DBSettings)
	{
		_tokenService = tokenService;
		_productsService = productsService;
		_businessRules = bussinesRules;
		_connectionString = DBSettings.ConnectionString;
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> PostProduct([FromForm] PostProductsDTO dto)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState);

		var UserIdVar = _tokenService.GetJWTAndDecode(HttpContext);
		if (UserIdVar == null) return BadRequest("JWT is required");

		Guid UserId = Guid.Parse(UserIdVar);

		if (!_businessRules.MaxFreePlan(_connectionString, UserId)) return BadRequest("Maximum number of products registered on the free plan");

		await _productsService.CreateProduct(dto, UserId);

		return Ok();
	}
}
