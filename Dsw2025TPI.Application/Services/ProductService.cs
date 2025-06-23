using Dsw2025Ej15.Application.Dtos;
using Dsw2025TPI.Domain.Entities;
using Dsw2025TPI.Domain.Exceptions;
using Dsw2025TPI.Domain.Interfaces;

namespace Dsw2025TPI.Api.Services
{
    public class ProductService
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ProductModel.Response?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("El ID proporcionado no es válido");

            var product = await _repository.GetByIdAsync(id);

            return product is null
                ? null
                : new ProductModel.Response(
                    product.Id,
                    product.Sku,
                    product.InternalCode,
                    product.Name,
                    product.Description,
                    product.CurrentUnitPrice,
                    product.StockQuantity);
        }


        private async Task ValidateProductRequest(ProductModel.Request request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Sku) ||
                string.IsNullOrWhiteSpace(request.Name) ||
                request.CurrentUnitPrice < 0)
            {
                throw new ArgumentException("Valores para el producto no válidos");
            }

            var exist = await _repository.FirstAsync(p => p.Sku == request.Sku);
            if (exist is not null)
                throw new DuplicatedEntityException($"Ya existe un producto con el Sku {request.Sku}");
        }

        public async Task<ProductModel.Response> CreateAsync(ProductModel.Request request)
        {
            await ValidateProductRequest(request);

            var product = new Product(
                Guid.NewGuid(),
                request.Sku,
                request.InternalCode,
                request.Name,
                request.Description,
                request.CurrentUnitPrice,
                request.StockQuantity
            );

            await _repository.CreateAsync(product);

            return new ProductModel.Response(
                product.Id,
                product.Sku,
                product.InternalCode,
                product.Name,
                product.Description,
                product.CurrentUnitPrice,
                product.StockQuantity
            );
        }

        public async Task<ProductModel.Response> UpdateAsync(Guid id, ProductModel.Request request)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID inválido");

            if (request == null ||
                string.IsNullOrWhiteSpace(request.Sku) ||
                string.IsNullOrWhiteSpace(request.Name) ||
                request.CurrentUnitPrice < 0)
                throw new ArgumentException("Datos inválidos");

            var existing = await _repository.GetByIdAsync(id);
            if (existing is null)
                throw new EntityNotFoundException($"No se encontró un producto con ID {id}");

            var duplicate = await _repository.FirstAsync(p => p.Sku == request.Sku && p.Id != id);
            if (duplicate is not null)
                throw new DuplicatedEntityException($"Ya existe otro producto con el Sku {request.Sku}");

            // Actualizar campos
            existing.Sku = request.Sku;
            existing.InternalCode = request.InternalCode;
            existing.Name = request.Name;
            existing.Description = request.Description;
            existing.CurrentUnitPrice = request.CurrentUnitPrice;
            existing.StockQuantity = request.StockQuantity;

            await _repository.UpdateAsync(existing);

            return new ProductModel.Response(
                existing.Id,
                existing.Sku,
                existing.InternalCode,
                existing.Name,
                existing.Description,
                existing.CurrentUnitPrice,
                existing.StockQuantity
            );
        }
        public async Task DeactivateAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID inválido");

            var product = await _repository.GetByIdAsync(id);
            if (product is null || !product.IsActive)
                throw new EntityNotFoundException($"No se encontró un producto activo con ID {id}");

            product.Deactivate();
            await _repository.UpdateAsync(product);
        }

    }
}
