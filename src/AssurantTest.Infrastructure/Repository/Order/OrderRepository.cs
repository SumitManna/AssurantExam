using AssurantTest.Application.Interfaces.Repository;
using AssurantTest.Domain.Models.ResponseModel;
using AssurantTest.Infrastructure.Common.Utility;
using System.Text.Json;

namespace AssurantTest.Infrastructure.Repository.Order
{
    public class OrderRepository : IOrderRepository
    {
        public bool SaveOrderData(OrderResponseModel orderResponseModel)
        {
            var jsonData = JsonSerializer.Serialize(orderResponseModel);
            return SeedData.WriteJsonData(jsonData, $"{orderResponseModel.OrderId}.json");
        }
    }
}
