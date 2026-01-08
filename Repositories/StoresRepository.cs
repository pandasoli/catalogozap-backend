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

    public async Task<string> CreatStore (StoreModel store)
    {
        var query = @"
        INSERT INTO stores
            (user_id, name, bio, logo_url)
        value 
            (@UserId, @Name, @Bio, @LogoUrl)";

        await _conn.QueryAsync(query, store);
        
        return "New store successfully created.";
    }

    public async Task<string> ModStore (StoreModel store)
    {
        var query = @"
            UPDATE store
            SET 
                name = @Name,
                bio = @Bio,
                logo_url = @LogoUrl
            WHERE id = @Id AND user_id = @UserId";
        
        await _conn.QueryAsync(query, store);

        return "store updated successfully";
    }

    public async Task<string> DeleteStore (Guid storeId, Guid userId)
    {
        var query = @"
            DELETE FROM store WHERE id = @StoreId AND user_id = @UserID";

        await _conn.QueryAsync(query, new {
            UserId = userId,
            StoreId = storeId
        });
        
        return "Store successfully deleted.";
    }
}