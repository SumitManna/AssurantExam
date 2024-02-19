using AssurantTest.Application.Entities;
using AssurantTest.Application.Exception;
using AssurantTest.Application.Interfaces.Repository;
using AssurantTest.Application.Interfaces.Services;
using System.Net;

namespace AssurantTest.Infrastructure.Services
{
    public class PromotionService(IPromotionRepository promotionRepository) : IPromotionService
    {
        public async Task<IReadOnlyCollection<Promotion>> GetPromotionListAsync()
        {
            var data = promotionRepository.GetPromotionList() ?? throw new ApiException((int)HttpStatusCode.NotFound, "Promotion List not found");
            return await Task.FromResult(data);
        }
    }
}
