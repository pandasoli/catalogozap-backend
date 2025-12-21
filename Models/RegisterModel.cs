namespace CatalogoZap.Models;

public class RegisterModel
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string HashPassword { get; set; }
}