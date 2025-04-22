namespace Domain.Entities
{
    public class Mayorista : User
    {
        public int? CUIT { get; set; }
        public string? Categoria { get; set; } = string.Empty;
        public Mayorista()
        {
            TypeUser = "Mayorista";
        }
    }
}
