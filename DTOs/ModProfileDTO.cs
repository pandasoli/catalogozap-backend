namespace CatalogoZap.DTOs
{
    public class ModProfileDTO
    {
        required public Guid UserId { get; set; }
        public string? Name {get; set;}

        public string? Bio {get; set;}

        public string? Phone {get; set;}

        public string? Email {get; set;}

        public string? Password {get; set;}

    }
}