using System.ComponentModel.DataAnnotations;
using CatalogoZap.Attributes;

namespace CatalogoZap.DTOs;

public class StoreDTO
{
    [Required] required public string Name { get; set; }
    [Required] required public string Bio { get; set; }

    [Required]
    [MaxFileSize(6 * 1024 * 1024)]
    public required IFormFile Photo { get; set; }
}

public class ModifyStoreDTO
{
    required public Guid StoreId { get; set; }
    public string? Name { get; set; }
    public string? Bio { get; set; }
    public string? LogoUrl { get; set; }
}