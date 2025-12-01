using CatalogoZap.Repositories.Interfaces;
using System.Data;
using Dapper;
using CatalogoZap.Models;

namespace CatalogoZap.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly IDbConnection _conn;

    public ProductsRepository(IDbConnection connection) {
        _conn = connection;
    }

    public async Task CreateProduct(ProductModel data, Guid userId) {
        var query = @"
            INSERT INTO products
                (user_id, name, price_cents, photo_url, store_id, avaliable)
            VALUES
                (@UserId, @Name, @PriceCents, @PhotoUrl, @StoreId, @Avaliable)
        ";

		await _conn.ExecuteScalarAsync(query, new {
            UserId = userId,
            data.Name,
            data.PriceCents,
            data.PhotoUrl,
            data.StoreId,
            data.Avaliable
        });
    }
}
