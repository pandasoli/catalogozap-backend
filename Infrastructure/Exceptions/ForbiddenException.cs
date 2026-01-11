namespace CatalogoZap.Infrastructure.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string message) : base(message) {}
}
