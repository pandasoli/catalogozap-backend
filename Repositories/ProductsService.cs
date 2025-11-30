using CatalogoZap.DTO;
using Npgsql;
using CatalogoZap.Repositories.Interfaces;

namespace CatalogoZap.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        public async Task PostProducts(PostProductsDTO dto, Guid UserId, string ConnectionString, string imgUrl)
        {
            using var conn = new NpgsqlConnection(ConnectionString);
            await conn.OpenAsync();

            using var SQL = new NpgsqlCommand(
                "INSERT INTO products (user_id, name, price_cents, photo_url, store_id, avaliable) VALUES (@user_id, @name, @price_cents, @imgUrl, @store_id, @avaliable)",
                conn);

            SQL.Parameters.AddWithValue("@user_id", UserId);
            SQL.Parameters.AddWithValue("@store_id", dto.StoreId);
            SQL.Parameters.AddWithValue("@name", dto.Name);
            SQL.Parameters.AddWithValue("@price_cents", dto.PriceCents);
            SQL.Parameters.AddWithValue("@imgUrl", imgUrl);
            SQL.Parameters.AddWithValue("@avaliable", dto.Avaliable);

            int rowsAffected = await SQL.ExecuteNonQueryAsync();
        }
    }
}