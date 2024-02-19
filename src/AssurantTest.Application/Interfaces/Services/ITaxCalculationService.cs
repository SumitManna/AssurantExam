using AssurantTest.Application.Entities;
using AssurantTest.Application.Model;

namespace AssurantTest.Application.Interfaces.Services
{
    public interface ITaxCalculationService
    {
        Task<decimal> CalculatePreTaxCostAsync(OrderDataModel order);
        Task<decimal> CalculateDiscountAmountAsync(OrderDataModel order, Promotion? promotion);
        Task<decimal> CalculateTaxAmountAsync(OrderDataModel order, Promotion? promotion);
    }
}
