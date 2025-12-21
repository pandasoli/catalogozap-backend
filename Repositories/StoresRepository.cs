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
    public async Task<StoreModel> SelectStores(Guid UserId)
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

        return await _conn.QuerySingleAsync<StoreModel>(query, new { UserId });
    }
}