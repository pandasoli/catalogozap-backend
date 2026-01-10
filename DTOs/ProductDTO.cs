using System.ComponentModel.DataAnnotations;
using CatalogoZap.Attributes;

namespace CatalogoZap.DTOs;

public class ProductDTO
{
    [Required] public Guid StoreId { get; set; }
    [Required] public required string Name { get; set; }
    [Required] public decimal PriceCents { get; set; }
    [Required] public bool Avaliable { get; set; }

    [Required]
    [MaxFileSize(6 * 1024 * 1024)]
    public required IFormFile Photo { get; set; }

}

public class ModProductsDTO
{
    public string? Name { get; set; }

    public decimal? PriceCents { get; set; }

    public bool? Avaliable { get; set; }
    
    [MaxFileSize(6 * 1024 *1024)]
    public IFormFile? Photo { get; set; }
}