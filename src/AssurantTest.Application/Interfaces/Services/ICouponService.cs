using AssurantTest.Application.Entities;

namespace AssurantTest.Application.Interfaces.Services
{
    public interface ICouponService
    {
        Task<IReadOnlyCollection<Coupon>> GetCouponsListAsync();
    }
}
