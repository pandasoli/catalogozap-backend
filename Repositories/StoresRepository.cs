using CatalogoZap.Models;
using CatalogoZap.Repositories.Interfaces;
using System.Data;
using Dapper;

namespace CatalogoZap.Repositories;

public class StoresRepository : IStoresRepository
{
    private readonly IDbConnection _conn;
    public StoresRepository(IDbConnection conn)
    {
        _conn = conn;
    }
    public async Task<List<StoreModel>> SelectStores(Guid UserId)
    {
        var query = @"
            SELECT 
                id AS Id,
                name AS Name,
                bio AS Bio,
                logo_url AS LogoUrl
            FROM stores 
            WHERE user_id = @user_id
        ";

        var stores = await _conn.QueryAsync<StoreModel>(query, new
        {
            user_id = UserId
        });

        return stores.ToList();
    }
}