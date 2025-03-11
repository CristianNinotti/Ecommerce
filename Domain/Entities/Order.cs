using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount {  get; set; }
        public bool OrderStatus { get; set; }
        public int CustomerId {  get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


    }
}
