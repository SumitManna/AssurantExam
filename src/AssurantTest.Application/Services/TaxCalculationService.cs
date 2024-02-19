using AssurantTest.Application.Entities;
using AssurantTest.Application.Exception;
using AssurantTest.Application.Interfaces.Services;
using AssurantTest.Application.Model;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace AssurantTest.Application.Services
{
    public class TaxCalculationService(IConfiguration configuration) : ITaxCalculationService
    {
        public async Task<decimal> CalculateDiscountAmountAsync(OrderDataModel order, Promotion? promotion)
        {
            decimal discountAmount = 0;
            if (promotion != null)
            {
                discountAmount += await CalculatePromotionDiscountAsync(order, promotion);
            }

            foreach (var item in order.Products)
            {
                if (item.Coupon != null)
                {
                    discountAmount += CalculateCouponDiscount(item.Product.Price, item.Quantity, item.Coupon);
                }
            }

            return discountAmount;
        }

        public async Task<decimal> CalculatePreTaxCostAsync(OrderDataModel order)
        {
            decimal perTaxCost = 0;
            foreach (var item in order.Products)
            {
                perTaxCost += item.Product.Price * item.Quantity;
            }
            return await Task.FromResult(perTaxCost);
        }

        public async Task<decimal> CalculateTaxAmountAsync(OrderDataModel order, Promotion? promotion)
        {
            var luxurySection = configuration.GetSection("LuxuryTaxRate") ?? throw new ApiException((int)HttpStatusCode.InternalServerError, "Something went wrong!!");
            int luxuryRate = Convert.ToInt32(luxurySection.Value);

            decimal taxAmount = 0;
            foreach (var item in order.Products)
            {
                decimal itemPrice = item.Product.Price * item.Quantity;
                if (item.Product.IsLuxuryProduct && order.State.ApplyLuxuryTax)
                {
                    taxAmount += itemPrice * (order.State.TaxRates * luxuryRate);
                }
                else
                {
                    taxAmount += itemPrice * order.State.TaxRates;
                }
            }

            var stateSection = configuration.GetSection("TaxCalculateBeforeDiscountStateList").GetChildren() ?? throw new ApiException((int)HttpStatusCode.InternalServerError, "Something went wrong!!");
            var stateList = stateSection.Select(x => x.Value).ToList();

            if (stateList.Contains(order.State.Code.ToUpper()))
            {
                return taxAmount;
            }
            else
            {
                decimal discountAmount = await CalculateDiscountAmountAsync(order, promotion);
                decimal discountedTotal = await CalculatePreTaxCostAsync(order) - discountAmount;
                decimal discountedTaxAmount = discountedTotal * order.State.TaxRates;
                return discountedTaxAmount;
            }

        }

        public async Task<decimal> CalculatePromotionDiscountAsync(OrderDataModel order, Promotion promotion)
        {
            decimal totalCost = await CalculatePreTaxCostAsync(order);
            return totalCost * promotion.DiscountPercentage;
        }

        public decimal CalculateCouponDiscount(decimal itemPrice, int quantity, Coupon coupon)
        {
            decimal discountPerItem = itemPrice * coupon.DiscountPercentage;
            return discountPerItem * quantity;
        }
    }
}
