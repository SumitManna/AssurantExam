using AssurantTest.Application.Entities;

namespace AssurantTest.Application.Interfaces.Repository
{
    public interface ICustomerRepository
    {
        Customer? GetCustomer(Guid customerId);
        IReadOnlyCollection<Customer> GetAllCustomer();
    }
}
