using AssurantTest.Application.Entities;

namespace AssurantTest.Application.Interfaces.Repository
{
    public interface IProductRepository
    {
        Product? GetProductById(Guid productId);
        IReadOnlyCollection<Product> GetAllProducts();
    }
}
