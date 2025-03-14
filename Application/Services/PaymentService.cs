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
                var paymentEntity = PaymentProfile.ToPaymentEntity(payment);
                if (paymentEntity == null)
                {
                    return false;
                }
                var order = _orderRepository.GetOrderByIdRepository(payment.OrderId);
                if (order == null || order.OrderItems == null || !order.OrderItems.Any())
                {
                    return false;
                }
                if (payment.Amount != order.TotalAmount)
                {
                    return false;
                }
                foreach (var orderItem in order.OrderItems)
                {
                    var product = _productRepository.GetProductByIdRepository(orderItem.ProductId);
                    if (product == null || product.Stock < orderItem.Quantity)
                    {
                        return false;
                    }
                    product.Stock -= orderItem.Quantity;
                    _productRepository.UpdateProductRepository(product);
                    _orderItemRepository.UpdateOrderItemRepository(orderItem);
                }
                order.OrderStatus = false;
                order.Payment = paymentEntity;
                
            _paymentRepository.CreatePayment(paymentEntity);
            _orderRepository.UpdateOrderRepository(order);
            return true;
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
