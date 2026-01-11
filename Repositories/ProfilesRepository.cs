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

    public async Task<ProfileModel?> GetProfileById(Guid userId)
    {
        var query = @"
            SELECT 
                username AS Username,
                bio AS Bio,
                phone AS Phone,
                logo_url AS LogoUrl,
                created_at AS CreatedAt,
                email AS Email,
                premium AS Premium,
                password as Password
            FROM profiles 
            WHERE id = @userId
        ";

        var profile = await _conn.QuerySingleOrDefaultAsync<ProfileModel>(query, new { userId });

        return profile;
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

    public async Task ModifyProfile(Guid userId, ProfileModel newdata)
    {
        var query = @"
            UPDATE profiles
            SET
                username = @Username,
                bio = @Bio,
                phone = @Phone,
                logo_url = @LogoUrl,
                email = @Email,
                password = @Password
            where id = @Id
        ";
        await _conn.ExecuteAsync(query, newdata);
    }
}
