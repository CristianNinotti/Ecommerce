﻿namespace Application.Models.Request
{
    public class OrderItemRequest
    {
        public int OrderId { get; set; } // Ver
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
