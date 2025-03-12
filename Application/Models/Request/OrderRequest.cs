namespace Application.Models.Request
{
    public class OrderRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        // Me va a permitir modificarla en el futuro para "Payment cuando pague el cliente"
        public bool OrderStatus { get; set; } 

    }
}
