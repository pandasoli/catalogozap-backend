namespace CatalogoZap.Models;

public class LoginModel
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public Guid Id { get; set; }
}