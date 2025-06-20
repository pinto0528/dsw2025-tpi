using Dsw2025TPI.Domain.Entities;
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

        public async Task<Product> CreateAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            await _repository.CreateAsync(product);
            return product;
        }

    }
}
