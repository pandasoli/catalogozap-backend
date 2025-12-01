using System.ComponentModel.DataAnnotations;

namespace CatalogoZap.DTO
{
    public class PostProductsDTO
    {
        [Required] public Guid StoreId { get; set; }
        [Required] public required string Name { get; set; }
        [Required] public decimal PriceCents { get; set; }
        [Required] public required IFormFile Photo { get; set; }
        [Required] public bool Avaliable { get; set; }
    }
}
