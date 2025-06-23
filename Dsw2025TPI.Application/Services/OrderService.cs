using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025TPI.Application.DTOs;
using Dsw2025TPI.Application.Helpers;
using Dsw2025TPI.Domain.Entities;
using Dsw2025TPI.Domain.Exceptions;
using Dsw2025TPI.Domain.Interfaces;

namespace Dsw2025TPI.Application.Services
{
    public class OrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly OrderValidationHelper _validationHelper;

        public OrderService(
            IRepository<Order> orderRepository,
            IRepository<Product> productRepository,
            OrderValidationHelper validationHelper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _validationHelper = validationHelper;
        }

        public async Task<OrderResponse> CreateAsync(OrderRequest request)
        {

            await _validationHelper.ValidateAsync(request);


            var productIds = request.Items.Select(i => i.ProductId).ToHashSet();
            var products = await _productRepository.GetByIdsAsync(productIds);


            var orderItems = request.Items.Select(item =>
            {
                var product = products.First(p => p.Id == item.ProductId);

                product.DecreaseStock(item.Quantity);

                return new OrderItem(
                    ProductId: item.ProductId,
                    quantity: item.Quantity,
                    unitPrice: item.UnitPrice
                );
            }).ToList();


            var total = orderItems.Sum(i => i.UnitPrice * i.Quantity);


            var order = new Order(
                customerId: request.CustomerId,
                shippingAddress: request.ShippingAddress,
                billingAddress: request.BillingAddress,
                items: orderItems,
                totalAmount: total
            );


            await _orderRepository.CreateAsync(order);

 
            return new OrderResponse
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                TotalAmount = order.TotalAmount,
                CreatedAt = order.Date,
                Items = order.OrderItems.Select(i => new OrderItemResponse
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }

        public async Task<OrderResponse> GetByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id, "OrderItems");

            if (order is null)
                throw new EntityNotFoundException($"No se encontró la orden con ID {id}");

            return new OrderResponse
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                TotalAmount = order.TotalAmount,
                CreatedAt = order.Date,
                Items = order.OrderItems.Select(oi => new OrderItemResponse
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };
        }

    }

}
