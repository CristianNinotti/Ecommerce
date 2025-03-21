﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Context
{
    public class EcommerceDbContext:DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options) { }
        public DbSet<Minorista> Minoristas { get; set; }
        public DbSet<Mayorista> Mayoristas { get; set; }
        public DbSet<SuperAdmin> SuperAdmins { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders {  get; set; }
        public DbSet <Payment> Payments { get; set; }
    }
}