namespace CatalogoZap.Models;

public class StoreModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public required string Bio { get; set; }
    public required string LogoUrl { get; set; }
}