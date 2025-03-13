using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Interfaces;
using System.Threading.Tasks;
using Application.Models.Response;
using Application.Models.Request;
using Application.Mappings;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        public PaymentService(IPaymentRepository paymentRepository,IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IProductRepository productRepository)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
        }

        public List<PaymentResponse> GetAllPayments()
        {
            var payments = _paymentRepository.GetAllPayments();
            return PaymentProfile.ToPaymentsResponse(payments);
        }

        public PaymentResponse? GetPaymentById(int id)
        {
            var payment = _paymentRepository.GetPaymentById(id);
            if (payment != null)
            {
                return PaymentProfile.ToPaymentResponse(payment);
            }
            return null;
        }

        public bool CreatePayment(PaymentRequest payment)
        {
            try
            {
                var paymentEntity = PaymentProfile.ToPaymentEntity(payment);
                if (paymentEntity == null)
                {
                    Console.WriteLine("Error: Payment entity is null.");
                    return false;
                }
                var order = _orderRepository.GetOrderByIdRepository(payment.OrderId);
                var orderItems = _orderItemRepository.GetOrderItemsByOrderIdRepository(payment.OrderId);
                if (order == null || orderItems == null || !orderItems.Any())
                {
                    Console.WriteLine("Error: Order or OrderItems are missing.");
                    return false;
                }
                Console.WriteLine($"Payment Amount: {payment.Amount}, Order Total: {order.TotalAmount}");
                if (payment.Amount != order.TotalAmount)
                {
                    Console.WriteLine("Error: Payment amount does not match order total.");
                    return false;
                }
                foreach (var orderItem in orderItems)
                {
                    var product = _productRepository.GetProductByIdRepository(orderItem.ProductId);
                    if (product != null && product.Stock >= orderItem.Quantity)
                    {
                        Console.WriteLine($"Updating product {product.Id}. Stock: {product.Stock}, Quantity: {orderItem.Quantity}");
                        Console.WriteLine($"OrderItem: ProductId={product.Id}, Quantity={product.Stock}, Price={product.Price}, TotalPrice={orderItem.TotalPrice}");
                        product.Stock -= orderItem.Quantity;
                        _productRepository.UpdateProductRepository(product);
                        _orderItemRepository.UpdateOrderItemRepository(orderItem);
                    }
                    else
                    {
                        Console.WriteLine($"Error: Product {orderItem.ProductId} does not have enough stock.");
                        return false;
                    }
                }
                order.OrderStatus = false;
                Console.WriteLine("Updating order status to false.");
                _orderRepository.UpdateOrderRepository(order);
                Console.WriteLine("Creating payment...");
                _paymentRepository.CreatePayment(paymentEntity);
                Console.WriteLine("Payment processed successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public bool ToUpdatePayment(int id, PaymentRequest request)
        {
            var paymentEntity = _paymentRepository.GetPaymentById(id);
            if (paymentEntity != null)
            {
                PaymentProfile.ToPaymentUpdate(paymentEntity, request);
                _paymentRepository.UpdatePayment(paymentEntity);
                return true;
            }
            return false;
        }

        public bool DeletePayment(int id)
        {
            var paymentEntity = _paymentRepository.GetPaymentById(id);
            if (paymentEntity != null)
            {
                _paymentRepository.DeletePayment(paymentEntity);
                return true;
            }
            return false;
        }
    }
}
