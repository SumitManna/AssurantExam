using AssurantTest.Application.Entities;
using AssurantTest.Application.Interfaces.Repository;
using AssurantTest.Application.Interfaces.Services;

namespace AssurantTest.Infrastructure.Services
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        public async Task<IReadOnlyCollection<Customer>> GetCustomerListAsync()
        {
            return await Task.FromResult(customerRepository.GetAllCustomer());
        }
    }
}
