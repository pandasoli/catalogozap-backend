namespace CatalogoZap.Models;

public class ProfileModel
{
    public required string Username { get; set; }
    public required string Bio { get; set; }
    public required string Phone { get; set; }
    public required string LogoUrl { get; set; }
    public required string CreatedAt { get; set; }
    public required string Email { get; set; }
    public bool Premium { get; set; }
}
