using AssurantTest.Application.Entities;

namespace AssurantTest.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IReadOnlyCollection<Product>> GetProductListAsync();
    }
}
