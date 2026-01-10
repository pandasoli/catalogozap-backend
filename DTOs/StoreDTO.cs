namespace CatalogoZap.DTOs;

public class StoreDTO
{
    required public Guid Id {get; set;}
    required public Guid UserId { get; set; }
    required public string Name { get; set; }
    required public string Bio { get; set; }
    required public string LogoUrl { get; set; }
}

public class ModStoreDTO
{
    required public Guid StoreId { get; set; }
    required public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? Bio { get; set; }
    public string? LogoUrl { get; set; }
}