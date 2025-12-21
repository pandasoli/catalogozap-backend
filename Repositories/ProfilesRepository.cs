using CatalogoZap.Repositories.Interfaces;
using System.Data;
using Dapper;
using CatalogoZap.Models;

namespace CatalogoZap.Repositories;

public class ProfilesRepository : IProfilesRepository
{
    private readonly IDbConnection _conn;

    public ProfilesRepository(IDbConnection connection)
    {
        _conn = connection;
    }

    public async Task<ProfileModel> GetProfileById(Guid userId)
    {
        var query = @"
            SELECT * FROM profiles WHERE id = @userId
        ";

        return await _conn.QuerySingleAsync<ProfileModel>(query, new { userId });
    }

    public async Task<LoginModel?> SelectUser(string Email)
    {
        var query = @"
            SELECT 
                username AS Username,
                password AS Password,
                id AS Id
            FROM profiles
            WHERE email = @email";

        return await _conn.QuerySingleOrDefaultAsync<LoginModel>(query, new
        {
            email = Email
        });
    }

    public async Task InsertUser(RegisterModel register)
    {
        var query = @"
            INSERT INTO profiles(username, email, password) VALUES(@username, @email, @password)
        ";

        await _conn.ExecuteAsync(query, new
        {
            username = register.Username,
            email = register.Email,
            password = register.HashPassword
        });
    }
}
