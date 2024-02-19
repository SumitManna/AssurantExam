using AssurantTest.Domain.Models.RequestModel;
using AssurantTest.Domain.Models.ResponseModel;

namespace AssurantTest.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderResponseModel> SaveOrderDataAsync(List<OrderRequestModel> orderRequestModels, Guid customerId);
    }
}
