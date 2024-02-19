using AssurantTest.Application.Exception;
using AssurantTest.Application.Interfaces.Repository;
using AssurantTest.Infrastructure.Common.Utility;
using System.Net;
using System.Text.Json;

namespace AssurantTest.Infrastructure.Repository.Product
{
    public class ProductRepository : IProductRepository
    {
        public IReadOnlyCollection<Application.Entities.Product> GetAllProducts()
        {
            var productListString = SeedData.ReadSeedData("Products.json");
            if (!string.IsNullOrEmpty(productListString))
                return JsonSerializer.Deserialize<IReadOnlyCollection<Application.Entities.Product>>(productListString)!;

            throw new ApiException((int)HttpStatusCode.InternalServerError, "Product List not found");
        }

        public Application.Entities.Product? GetProductById(Guid productId)
        {
            var productList = GetAllProducts();
            return productList.FirstOrDefault(c => c.Id == productId);
        }
    }
}
