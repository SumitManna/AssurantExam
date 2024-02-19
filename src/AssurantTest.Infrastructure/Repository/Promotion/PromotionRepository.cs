using AssurantTest.Application.Exception;
using AssurantTest.Application.Interfaces.Repository;
using AssurantTest.Infrastructure.Common.Utility;
using System.Net;
using System.Text.Json;

namespace AssurantTest.Infrastructure.Repository.Promotion
{
    public class PromotionRepository : IPromotionRepository
    {
        public Application.Entities.Promotion? GetPromotionData(DateTime orderDate)
        {
            var promotionList = GetPromotionList();
            if (promotionList == null)
                return null;
            return promotionList.FirstOrDefault(p => p.ValidFrom <= orderDate && p.ValidTo >= orderDate);
        }

        public IReadOnlyCollection<Application.Entities.Promotion>? GetPromotionList()
        {
            var promotionListString = SeedData.ReadSeedData("Promotions.json");
            if (!string.IsNullOrEmpty(promotionListString))
                return JsonSerializer.Deserialize<IReadOnlyCollection<Application.Entities.Promotion>>(promotionListString)!;

            throw new ApiException((int)HttpStatusCode.InternalServerError, "Product List not found");
        }
    }
}
