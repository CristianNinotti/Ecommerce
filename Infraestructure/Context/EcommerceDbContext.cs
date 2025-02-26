using Domain.Entities;
using Microsoft.EntityFrameworkCore;




namespace Infraestructure.Context
{
    public class EcommerceDbContext:DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options) { }
        public DbSet<Minorista> Minoristas { get; set; }
        public DbSet<Mayorista> Mayoristas { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
       // {
       //     base.OnModelCreating(modelBuilder);
       // }

        //Data Seed: Agregamos unos datos iniciales

        //    modelBuilder.Entity<Minorista>().HasData(
        //    new ()
        //                           {
        //     Id = 1,
        //                              FirstName = "Facundo",
        //    LastName = "Solari",
        //    NameAccount = "facu",
        //    Password = "facu123",
        //    Email = "facu@hotmail.com",
        //    Dni = 12323456,
        //   PhoneNumber = "+543413500300",
        //   Address = "Santafe 1234",
        //   },
        //     new ()
        //      //                             {
        //      Id = 2,
        //                                 FirstName = "Cristian",
        //      LastName = "Ninotti",
        //      NameAccount = "cris",
        //      Password = "cris123",
        //     Email = "cris@hotmail.com",
        //                                 Dni = 34732713,
        //     PhoneNumber = "+543415155611",
        //                                Address = "San Lorenzo 3624",
        //                             }
        // 
        //               );

        //    }
    }
}