﻿namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public List<string>? Photos { get; set; }
        public Category? Categoria { get; set; }

        // Relación con OrderItem
        public List<OrderItem> OrderItems { get; set; } = new(); // ⬅ Un producto puede estar en varios OrderItem
    }
}



