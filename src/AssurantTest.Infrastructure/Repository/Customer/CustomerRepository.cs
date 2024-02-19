using AssurantTest.Application.Exception;
using AssurantTest.Application.Interfaces.Repository;
using AssurantTest.Infrastructure.Common.Utility;
using System.Net;
using System.Text.Json;

namespace AssurantTest.Infrastructure.Repository.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        public IReadOnlyCollection<Application.Entities.Customer> GetAllCustomer()
        {
            var customerListString = SeedData.ReadSeedData("Customers.json");
            if (!string.IsNullOrEmpty(customerListString))
                return JsonSerializer.Deserialize<IReadOnlyCollection<Application.Entities.Customer>>(customerListString)!;

            throw new ApiException((int)HttpStatusCode.InternalServerError, "Customer List not found");
        }

        public Application.Entities.Customer? GetCustomer(Guid customerId)
        {
            var customerList = GetAllCustomer();
            return customerList.FirstOrDefault(c => c.Id == customerId);
        }
    }
}
