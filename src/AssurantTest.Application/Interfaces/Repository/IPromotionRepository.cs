using AssurantTest.Application.Entities;

namespace AssurantTest.Application.Interfaces.Repository
{
    public interface IPromotionRepository
    {
        Promotion? GetPromotionData(DateTime orderDate);
        IReadOnlyCollection<Promotion>? GetPromotionList();
    }
}
