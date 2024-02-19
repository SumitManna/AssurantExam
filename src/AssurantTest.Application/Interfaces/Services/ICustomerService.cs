using AssurantTest.Application.Entities;

namespace AssurantTest.Application.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<IReadOnlyCollection<Customer>> GetCustomerListAsync();
    }
}
