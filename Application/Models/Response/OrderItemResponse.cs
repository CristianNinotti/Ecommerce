namespace Application.Models.Response
{
    public class OrderItemResponse
    {
        public int Id { get; set; } // Ver
        public int OrderId { get; set; } // Ver
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; } // Calcula el total de cada OrderItem //

    }
}
