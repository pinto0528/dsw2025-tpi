using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025TPI.Application.DTOs;
using Dsw2025TPI.Domain.Entities;
using Dsw2025TPI.Domain.Exceptions;
using Dsw2025TPI.Domain.Interfaces;

namespace Dsw2025TPI.Application.Helpers
{
    public class OrderValidationHelper
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Product> _productRepository;

        public OrderValidationHelper(
            IRepository<Customer> customerRepository,
            IRepository<Product> productRepository)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public async Task ValidateAsync(OrderRequest request)
        {
            if (request == null)
                throw new ArgumentException("La orden no puede ser nula.");

            if (request.Items == null || !request.Items.Any())
                throw new InvalidOperationException("Debe haber al menos un producto en la orden.");

            // Validar cliente
            // Validación omitida: se asume que cualquier clienteId es válido (por consigna)
            /*var customerExists = await _customerRepository.ExistsAsync(c => c.Id == request.CustomerId);
            if (!customerExists)
                throw new EntityNotFoundException($"No se encontró el cliente con ID {request.CustomerId}");*/

            // Validar productos
            var productIds = request.Items.Select(i => i.ProductId).ToHashSet();
            var products = await _productRepository.GetByIdsAsync(productIds);

            foreach (var item in request.Items)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);

                if (product == null)
                    throw new EntityNotFoundException($"Producto con ID {item.ProductId} no encontrado.");

                if (!product.IsActive)
                    throw new InvalidOperationException($"El producto '{product.Name}' está inactivo.");

                if (item.Quantity > product.StockQuantity)
                    throw new InvalidOperationException($"Stock insuficiente para '{product.Name}'.");

                if (item.UnitPrice != product.CurrentUnitPrice)
                    throw new InvalidOperationException($"Precio incorrecto para '{product.Name}'. Se esperaba {product.CurrentUnitPrice}");
            }

            // Validar duplicados
            var duplicado = request.Items.GroupBy(i => i.ProductId)
                                         .FirstOrDefault(g => g.Count() > 1);
            if (duplicado != null)
                throw new InvalidOperationException($"Producto repetido en la orden: {duplicado.Key}");
        }
    }
}

