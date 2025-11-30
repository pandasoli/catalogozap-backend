using System.Text.RegularExpressions;

namespace CatalogoZap.Infrastructure.Database;

public interface IConvertToConnectionString
{
    string convert(string _databaseUrl);
}
public class ConvertToConnectionString : IConvertToConnectionString
{
    public string convert(string _databaseUrl)
    {
        var match = Regex.Match(_databaseUrl,
            @"^postgres(ql)?:\/\/(?<user>[^:]+):(?<pass>[^@]+)@(?<host>[^\/]+?)(:)?(?<port>\d+)?\/(?<db>[^\?]+)");

        if (!match.Success)
        {
            throw new FormatException("The DATABASE_URL is not in the format 'postgresql://user:pass@host[/port]/db'.");
        }

        string port = match.Groups["port"].Success ? match.Groups["port"].Value : "5432";

        string connectionString = $"Host={match.Groups["host"].Value};" +
                                 $"Port={port};" +
                                 $"Username={match.Groups["user"].Value};" +
                                 $"Password={match.Groups["pass"].Value};" +
                                 $"Database={match.Groups["db"].Value};" +
                                 "SSL Mode=Require;Trust Server Certificate=true";

        return connectionString;
    }
}
