using CatalogoZap.Attributes;

namespace CatalogoZap.DTOs;

    public class ModProductsDTO
    {
        public string? Name { get; set; }

        public decimal? PriceCents { get; set; }

        public bool? Avaliable { get; set; }
        
        [MaxFileSize(6 * 1024 *1024)]
        public IFormFile? Photo { get; set; }
    }
