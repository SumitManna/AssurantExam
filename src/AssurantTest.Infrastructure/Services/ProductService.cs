using AssurantTest.Application.Entities;
using AssurantTest.Application.Interfaces.Repository;
using AssurantTest.Application.Interfaces.Services;

namespace AssurantTest.Infrastructure.Services
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        public async Task<IReadOnlyCollection<Product>> GetProductListAsync()
        {
            return await Task.FromResult(productRepository.GetAllProducts());
        }
    }
}
