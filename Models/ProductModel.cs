namespace CatalogoZap.Models;

public class ProductModel
{
    public Guid UserId { get; set; }
    public Guid StoreId { get; set; }
    public required string Name { get; set; }
    public decimal PriceCents { get; set; }
    public required string PhotoUrl { get; set; }
    public bool Avaliable { get; set; }
}
