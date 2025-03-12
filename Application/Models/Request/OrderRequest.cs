namespace Application.Models.Request
{
    public class OrderRequest
    {
        public int UserId { get; set; }

        // Me va a permitir modificarla en el futuro para "Payment cuando pague el cliente"
        public bool OrderStatus { get; set; }

        // Lista de los Request de Order Item
        public List<OrderItemRequest> OrderItems { get; set; } = new List<OrderItemRequest>();

    }
}
