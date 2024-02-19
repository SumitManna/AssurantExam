using AssurantTest.Application.Entities;
using AssurantTest.Application.Exception;
using AssurantTest.Application.Interfaces.Repository;
using AssurantTest.Application.Interfaces.Services;
using AssurantTest.Application.Model;
using AssurantTest.Domain.Models.RequestModel;
using AssurantTest.Domain.Models.ResponseModel;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AssurantTest.Infrastructure.Services
{
    public class OrderService(
        IStateRepository stateRepository,
        IProductRepository productRepository,
        ICouponRepository couponRepository,
        IPromotionRepository promotionRepository,
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        ITaxCalculationService taxCalculationService,
        ILogger<OrderService> logger
        ) : IOrderService
    {
        public async Task<OrderResponseModel> SaveOrderDataAsync(List<OrderRequestModel> orderRequestModels, Guid customerId)
        {
            DateTime orderDate = DateTime.Now;
            var orderData = GetOrderDataModel(orderRequestModels, customerId, orderDate);

            var promotionData = promotionRepository.GetPromotionData(orderDate);

            var orderResponseModel = await CalculateTotalCostAsync(orderData, promotionData, customerId);

            if (!orderRepository.SaveOrderData(orderResponseModel))
            {
                throw new ApiException((int)HttpStatusCode.InternalServerError, "Something went wrong!!");
            }
            return orderResponseModel;
        }

        public OrderDataModel GetOrderDataModel(List<OrderRequestModel> orderRequestModels, Guid customerId, DateTime orderDate)
        {
            var customer = customerRepository.GetCustomer(customerId) ?? throw new ApiException((int)HttpStatusCode.NotFound, "Customern not found");
            var stateData = stateRepository.GetStateData(customer.StateId) ?? throw new ApiException((int)HttpStatusCode.NotFound, "State not found");

            var productData = new List<ProductDataModel>();
            foreach (var item in orderRequestModels)
            {
                var productDetails = productRepository.GetProductById(item.ProductId);
                if (productDetails != null)
                {
                    var coupon = couponRepository.GetCoupon(item.ProductId, orderDate);
                    productData.Add(new ProductDataModel(productDetails, item.Quantity, coupon));
                }
            }

            return new OrderDataModel(orderDate, stateData, productData);
        }

        public async Task<OrderResponseModel> CalculateTotalCostAsync(OrderDataModel order, Promotion? promotion, Guid customerId)
        {
            try
            {
                var tasks = new List<Task<decimal>>
                {
                    taxCalculationService.CalculatePreTaxCostAsync(order),
                    taxCalculationService.CalculateTaxAmountAsync(order,promotion),
                    taxCalculationService.CalculateDiscountAmountAsync(order,promotion)
                };
                var results = await Task.WhenAll(tasks);

                decimal totalCost = results[0];
                decimal taxAmount = results[1];
                decimal discountAmount = results[2];

                decimal preTaxCost = totalCost;
                totalCost += taxAmount;
                totalCost -= discountAmount;

                return new OrderResponseModel(Guid.NewGuid(), order.OrderDate, customerId, totalCost, preTaxCost, taxAmount, discountAmount);
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception: {ex.Message}");
                throw new ApiException((int)HttpStatusCode.InternalServerError, "Something went wrong!!");
            }
        }
    }
}
