using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using CatalogoZap.Infrastructure.JWT;
using CatalogoZap.Infrastructure.Swagger;
using CatalogoZap.Services.Interfaces;
using CatalogoZap.Services;
using CatalogoZap.Repositories;
using CatalogoZap.Repositories.Interfaces;
using System.Data;
using Npgsql;
using CatalogoZap.Infrastructure.CloudinaryService;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment()) {
    var connectionString = builder.Configuration.GetConnectionString("Default");

    builder.Configuration.GetSection("ConnectionStrings")["Default"] = connectionString;
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(options => {
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
	options.TokenValidationParameters = TokenService.GetValidationParameters(builder.Configuration);
});

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c => {
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header
	});

	c.OperationFilter<AuthorizeCheckOperationFilter>();
	c.OperationFilter<AutoTagOperationFilter>();
	c.EnableAnnotations();
});

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IProfilesService, ProfilesService>();
builder.Services.AddScoped<IProfilesRepository, ProfilesRepository>();
builder.Services.AddScoped<IStoresService, StoresService>();
builder.Services.AddScoped<IStoresRepository, StoresRepository>();

builder.Services.AddScoped<IDbConnection>(sp =>
	new NpgsqlConnection(builder.Configuration.GetConnectionString("Default"))
);

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
