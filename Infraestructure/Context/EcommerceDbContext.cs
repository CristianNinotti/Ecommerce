using Domain.Entities;



namespace Infraestructure.Context
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
        {
        }
        public DbSet<Minorista> Minoristas { get; set; }
    }
}