using AssurantTest.Application.Entities;

namespace AssurantTest.Application.Interfaces.Services
{
    public interface IPromotionService
    {
        Task<IReadOnlyCollection<Promotion>> GetPromotionListAsync();
    }
}
