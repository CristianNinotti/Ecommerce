﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Ver
        public int ProductId {  get; set; }
        public int Quantity {  get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        // Relación con Product
        public Product? Product { get; set; }
    }
}
