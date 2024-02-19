using AssurantTest.Domain.Models.ResponseModel;

namespace AssurantTest.Application.Interfaces.Repository
{
    public interface IOrderRepository
    {
        bool SaveOrderData(OrderResponseModel orderResponseModel);
    }
}
